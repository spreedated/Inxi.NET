
'    Inxi.NET  Copyright (C) 2020  EoflaOE
'
'    This file is part of Inxi.NET
'
'    Inxi.NET is free software: you can redistribute it and/or modify
'    it under the terms of the GNU General Public License as published by
'    the Free Software Foundation, either version 3 of the License, or
'    (at your option) any later version.
'
'    Inxi.NET is distributed in the hope that it will be useful,
'    but WITHOUT ANY WARRANTY; without even the implied warranty of
'    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'    GNU General Public License for more details.
'
'    You should have received a copy of the GNU General Public License
'    along with this program.  If not, see <https://www.gnu.org/licenses/>.

Imports System.Management
Imports Newtonsoft.Json.Linq

Module HardDriveParser

    ''' <summary>
    ''' Parses hard drives
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseHardDrives(InxiToken As JToken) As Dictionary(Of String, HardDrive)
        'Variables (global)
        Dim HDDParsed As New Dictionary(Of String, HardDrive)
        Dim DriveParts As New Dictionary(Of String, Partition)
        Dim Drive As HardDrive
        Dim DrivePart As Partition

        'If the system is Unix, use Inxi. If on Windows, use WMI. macOS, anyone?
        If IsUnix() Then
            'Variables (Inxi)
            Dim InxiDriveReady As Boolean = False

            'Enumerate each drive
            For Each InxiDrive In InxiToken.SelectToken("007#Drives")
                If InxiDriveReady Then
                    'Get information of a drive
                    Dim DriveSize As String = InxiDrive("004#size")
                    Dim DriveModel As String = InxiDrive("003#model")
                    Dim DriveVendor As String = InxiDrive("002#vendor")
                    If DriveVendor = "" Then
                        DriveSize = InxiDrive("003#size")
                        DriveModel = InxiDrive("002#model")
                    End If

                    'Get partitions
                    Dim DrivePartToken As JToken = InxiToken.SelectToken("009#Partition")
                    If Not DrivePartToken Is Nothing Then
                        For Each DrivePartition In DrivePartToken
                            If DrivePartition("006#dev") Is Nothing Then
                                Dim DrvDevPath As String = DrivePartition("005#dev").ToString
                                Dim TarDevPath As String = InxiDrive("001#ID").ToString
                                Dim DrvDevChar As Char
                                Dim CurrDrvChar As Char

                                If DrvDevPath.Contains("hd") Or DrvDevPath.Contains("sd") Or DrvDevPath.Contains("vd") Then '/dev/hdX, /dev/sdX, /dev/vdX
                                    CurrDrvChar = DrvDevPath.Replace("/dev/sd", "").Replace("/dev/hd", "").Replace("/dev/vd", "").Chars(0)
                                    DrvDevChar = TarDevPath.Replace("/dev/sd", "").Replace("/dev/hd", "").Replace("/dev/vd", "").Chars(0)
                                ElseIf DrvDevPath.Contains("mmcblk") Then '/dev/mmcblkXpY
                                    CurrDrvChar = DrvDevPath.Replace("/dev/mmcblk", "").Chars(0)
                                    DrvDevChar = TarDevPath.Replace("/dev/mmcblk", "").Chars(0)
                                ElseIf DrvDevPath.Contains("nvme") Then '/dev/nvmeXnY
                                    CurrDrvChar = DrvDevPath.Replace("/dev/nvme", "").Chars(0)
                                    DrvDevChar = TarDevPath.Replace("/dev/nvme", "").Chars(0)
                                End If

                                If CurrDrvChar = DrvDevChar Then
                                    DrivePart = New Partition(DrvDevPath, DrivePartition("004#fs"), DrivePartition("002#size"), DrivePartition("003#used"))
                                    DriveParts.Add(DrvDevPath, DrivePart)
                                End If
                            End If
                        Next
                    End If

                    'Create an instance of hard drive class
                    Drive = New HardDrive(InxiDrive("001#ID"), DriveSize, DriveModel, DriveVendor, DriveParts)
                    HDDParsed.Add(InxiDrive("001#ID"), Drive)
                Else
                    InxiDriveReady = True
                End If
            Next
        Else 'on Windows
            'Variables (WMI)
            Dim HardDisks As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
            Dim DiskPartitions As New ManagementObjectSearcher("SELECT * FROM Win32_DiskPartition")

            'HDD Prober
            For Each Hdd As ManagementBaseObject In HardDisks.Get
                Try
                    Dim DiskIndexHdd As Integer = Hdd("Index")
                    Dim DriveNo As Integer
                    For Each Manage As ManagementBaseObject In DiskPartitions.Get
                        Try
                            DriveNo = Manage("DiskIndex")
                            If DiskIndexHdd = DriveNo Then
                                DrivePart = New Partition(Manage("DeviceID"), Manage("Type"), Manage("Size"), "0")
                                DriveParts.Add("Physical partition in " & Hdd("Model") & " (" & DiskIndexHdd & ") : " & Manage("Index"), DrivePart)
                            End If
                        Catch ex As Exception
                            Continue For
                        End Try
                    Next
                    Drive = New HardDrive(Hdd("DeviceID"), Hdd("Size"), Hdd("Model"), Hdd("Manufacturer"), DriveParts)
                    HDDParsed.Add(Hdd("Model") & " (" & DiskIndexHdd & ")", Drive)
                Catch ex As Exception
                    Continue For
                End Try
            Next
        End If

        Return HDDParsed
    End Function

End Module

Module WindowsLogicalPartitionParser

    Function ParsePartitions(WMIObject As ManagementObjectSearcher) As Dictionary(Of String, WindowsLogicalPartition)
        Dim DriveParts As New Dictionary(Of String, WindowsLogicalPartition)
        Dim DrivePart As WindowsLogicalPartition
        For Each Part As ManagementBaseObject In WMIObject.Get
            Try
                DrivePart = New WindowsLogicalPartition(Part("DeviceID"), Part("FileSystem"), Part("Size"), CULng(Part("Size") - Part("FreeSpace")))
                DriveParts.Add("Logical partition " & Part("DeviceID"), DrivePart)
            Catch ex As Exception
                Continue For
            End Try
        Next

        Return DriveParts
    End Function

End Module