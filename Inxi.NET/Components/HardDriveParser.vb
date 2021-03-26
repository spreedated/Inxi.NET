
'    Inxi.NET  Copyright (C) 2020-2021  EoflaOE
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
Imports Claunia.PropertyList

Module HardDriveParser

    ''' <summary>
    ''' Parses hard drives
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseHardDrives(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, HardDrive)
        'Variables (global)
        Dim HDDParsed As New Dictionary(Of String, HardDrive)
        Dim DriveParts As New Dictionary(Of String, Partition)
        Dim Drive As HardDrive
        Dim DrivePart As Partition

        'If the system is Unix, use Inxi. If on Windows, use WMI. If on macOS, use system_profiler.
        If IsUnix() Then
            If IsMacOS() Then
                'Check for data type
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPStorageDataType" Then
                        'Get information of a drive
                        Dim DriveEnum As NSArray = DataType("_items")
                        For Each DriveDict As NSDictionary In DriveEnum
                            Dim DriveSize As String = DriveDict("size_in_bytes").ToObject
                            Dim DriveModel As String = TryCast(DriveDict("physical_drive"), NSDictionary)("device_name").ToObject
                            Dim DriveSerial As String = DriveDict("volume_uuid").ToObject

                            'Create an instance of hard drive class
                            Drive = New HardDrive(DriveDict("bsd_name").ToObject, DriveSize, DriveModel, "", "", DriveSerial, DriveParts)
                            HDDParsed.Add(DriveModel, Drive)
                        Next
                    End If
                Next
            Else
                'Variables (Inxi)
                Dim InxiDriveReady As Boolean = False

                'Enumerate each drive
                For Each InxiDrive In InxiToken.SelectTokenKeyEndingWith("Drives")
                    If InxiDriveReady Then
                        'Get information of a drive
                        Dim DriveSize As String = InxiDrive.SelectTokenKeyEndingWith("size")
                        Dim DriveModel As String = InxiDrive.SelectTokenKeyEndingWith("model")
                        Dim DriveVendor As String = InxiDrive.SelectTokenKeyEndingWith("vendor")
                        Dim DriveSerial As String = InxiDrive.SelectTokenKeyEndingWith("serial")
                        Dim DriveSpeed As String = InxiDrive.SelectTokenKeyEndingWith("speed")
                        If DriveVendor = "" Then
                            DriveSize = InxiDrive.SelectTokenKeyEndingWith("size")
                            DriveModel = InxiDrive.SelectTokenKeyEndingWith("model")
                        End If

                        'Get partitions
                        Dim DrivePartToken As JToken = InxiToken.SelectTokenKeyEndingWith("Partition")
                        If DrivePartToken IsNot Nothing Then
                            For Each DrivePartition In DrivePartToken
                                If DrivePartition.SelectTokenKeyEndingWith("dev") Is Nothing Then
                                    Dim DrvDevPath As String = DrivePartition.SelectTokenKeyEndingWith("dev").ToString
                                    Dim TarDevPath As String = InxiDrive.SelectTokenKeyEndingWith("ID").ToString
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
                                        DrivePart = New Partition(DrvDevPath, DrivePartition.SelectTokenKeyEndingWith("fs"), DrivePartition.SelectTokenKeyEndingWith("size"), DrivePartition.SelectTokenKeyEndingWith("used"))
                                        DriveParts.Add(DrvDevPath, DrivePart)
                                    End If
                                End If
                            Next
                        End If

                        'Create an instance of hard drive class
                        Drive = New HardDrive(InxiDrive.SelectTokenKeyEndingWith("ID"), DriveSize, DriveModel, DriveVendor, DriveSpeed, DriveSerial, DriveParts)
                        HDDParsed.Add(InxiDrive.SelectTokenKeyEndingWith("ID"), Drive)
                    Else
                        InxiDriveReady = True
                    End If
                Next
            End If
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
                    'TODO: Speed not implemented in Windows
                    Drive = New HardDrive(Hdd("DeviceID"), Hdd("Size"), Hdd("Model"), Hdd("Manufacturer"), "", Hdd("SerialNumber"), DriveParts)
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