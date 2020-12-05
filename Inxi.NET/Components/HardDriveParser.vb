
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

Imports Newtonsoft.Json.Linq

Module HardDriveParser

    ''' <summary>
    ''' Parses hard drives
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParseHardDrives(InxiToken As JToken) As Dictionary(Of String, HardDrive)
        Dim HDDParsed As New Dictionary(Of String, HardDrive)
        Dim InxiDriveReady As Boolean = False
        Dim Drive As HardDrive
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
                Dim DrivePart As Partition
                Dim DriveParts As New Dictionary(Of String, Partition)
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

        Return HDDParsed
    End Function

End Module
