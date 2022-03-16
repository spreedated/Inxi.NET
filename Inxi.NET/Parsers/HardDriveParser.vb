
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
Imports Extensification.External.Newtonsoft.Json.JPropertyExts
Imports Newtonsoft.Json.Linq
Imports Claunia.PropertyList

Class HardDriveParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses hard drives
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function ParseAll(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        'Variables
        Dim HDDParsed As Dictionary(Of String, HardwareBase)

        'If the system is Unix, use Inxi. If on Windows, use WMI. If on macOS, use system_profiler.
        If IsUnix() Then
            If IsMacOS() Then
                HDDParsed = ParseAllMacOS(SystemProfilerToken)
            Else
                HDDParsed = ParseAllLinux(InxiToken)
            End If
        Else 'on Windows
            Dim HardDisks As New ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
            HDDParsed = ParseAllWindows(HardDisks)
        End If

        Return HDDParsed
    End Function

    Overrides Function ParseAllLinux(InxiToken As JToken) As Dictionary(Of String, HardwareBase)
        'Variables
        Dim HDDParsed As New Dictionary(Of String, HardwareBase)
        Dim DriveParts As New Dictionary(Of String, Partition)
        Dim Drive As HardDrive
        Dim DrivePart As Partition
        Dim InxiDriveReady As Boolean = False

        'Enumerate each drive
        Debug("Selecting the Drives token...")
        For Each InxiDrive In InxiToken.SelectTokenKeyEndingWith("Drives")
            If InxiDriveReady Then
                'Get information of a drive
                Dim DriveID As String = InxiDrive.SelectTokenKeyEndingWith("ID")
                Dim DriveSize As String = InxiDrive.SelectTokenKeyEndingWith("size")
                Dim DriveModel As String = InxiDrive.SelectTokenKeyEndingWith("model")
                Dim DriveVendor As String = InxiDrive.SelectTokenKeyEndingWith("vendor")
                Dim DriveSerial As String = InxiDrive.SelectTokenKeyEndingWith("serial")
                Dim DriveSpeed As String = InxiDrive.SelectTokenKeyEndingWith("speed")
                If DriveVendor = "" Then
                    DriveSize = InxiDrive.SelectTokenKeyEndingWith("size")
                    DriveModel = InxiDrive.SelectTokenKeyEndingWith("model")
                End If
                Debug("Got information. DriveSize: {0}, DriveModel: {1}, DriveVendor: {2}, DriveSerial: {3}, DriveSpeed: {4}, DriveID: {5}", DriveSize, DriveModel, DriveVendor, DriveSerial, DriveSpeed, DriveID)

                'Get partitions
                Debug("Selecting the Partition token...")
                Dim DrivePartToken As JToken = InxiToken.SelectTokenKeyEndingWith("Partition")
                If DrivePartToken IsNot Nothing Then
                    For Each DrivePartition In DrivePartToken
                        If DrivePartition.SelectTokenKeyEndingWith("dev") Is Nothing Then
                            Dim DrvDevPath As String = DrivePartition.SelectTokenKeyEndingWith("dev").ToString
                            Dim TarDevPath As String = InxiDrive.SelectTokenKeyEndingWith("ID").ToString
                            Dim DrvDevChar As Char
                            Dim CurrDrvChar As Char
                            Dim PartitionFilesystem As String = DrivePartition.SelectTokenKeyEndingWith("fs")
                            Dim PartitionSize As String = DrivePartition.SelectTokenKeyEndingWith("size")
                            Dim PartitionUsed As String = DrivePartition.SelectTokenKeyEndingWith("used")

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
                            Debug("Got information. DrvDevPath: {0}, TarDevPath: {1}, DrvDevChar: {2}, CurrDrvChar: {3}, PartitionFilesystem: {4}, PartitionSize: {5}, PartitionUsed: {6}", DrvDevPath, TarDevPath, DrvDevChar, CurrDrvChar, PartitionFilesystem, PartitionSize, PartitionUsed)

                            If CurrDrvChar = DrvDevChar Then
                                DrivePart = New Partition(DrvDevPath, DrivePartition.SelectTokenKeyEndingWith("fs"), DrivePartition.SelectTokenKeyEndingWith("size"), DrivePartition.SelectTokenKeyEndingWith("used"))
                                DriveParts.Add(DrvDevPath, DrivePart)
                                Debug("Added {0} to the list of {1}'s partitions.", DrvDevPath, TarDevPath)
                            End If
                        End If
                    Next
                End If

                'Create an instance of hard drive class
                Drive = New HardDrive(DriveID, DriveSize, DriveModel, DriveVendor, DriveSpeed, DriveSerial, DriveParts)
                HDDParsed.Add(DriveID, Drive)
                Debug("Added {0} to the list of parsed drives.", DriveID)
            Else
                InxiDriveReady = True
            End If
        Next
        Return HDDParsed
    End Function

    Overrides Function ParseAllMacOS(SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        Dim HDDParsed As New Dictionary(Of String, HardwareBase)
        Dim DriveParts As New Dictionary(Of String, Partition)
        Dim Drive As HardDrive

        'TODO: Drive vendor and speed not implemented in macOS
        'Check for data type
        Debug("Checking for data type...")
        Debug("TODO: Drive vendor and speed not implemented in macOS")
        For Each DataType As NSDictionary In SystemProfilerToken
            If DataType("_dataType").ToObject = "SPStorageDataType" Then
                Debug("DataType found: SPStorageDataType...")

                'Get information of a drive
                Dim DriveEnum As NSArray = DataType("_items")
                Debug("Enumerating drives...")
                For Each DriveDict As NSDictionary In DriveEnum
                    Dim DriveSize As String = DriveDict("size_in_bytes").ToObject
                    Dim DriveModel As String = TryCast(DriveDict("physical_drive"), NSDictionary)("device_name").ToObject
                    Dim DriveSerial As String = DriveDict("volume_uuid").ToObject
                    Dim DriveBsdName As String = DriveDict("bsd_name").ToObject
                    Debug("Got information. DriveSize: {0}, DriveModel: {1}, DriveSerial: {2}, DriveBsdName: {3}", DriveSize, DriveModel, DriveSerial, DriveBsdName)

                    'Create an instance of hard drive class
                    Drive = New HardDrive(DriveBsdName, DriveSize, DriveModel, "", "", DriveSerial, DriveParts)
                    HDDParsed.Add(DriveModel, Drive)
                    Debug("Added {0} to the list of parsed drives.", DriveModel)
                Next
            End If
        Next
        Return HDDParsed
    End Function

    Overrides Function ParseAllWindows(WMISearcher As ManagementObjectSearcher) As Dictionary(Of String, HardwareBase)
        'Variables
        Dim HDDParsed As New Dictionary(Of String, HardwareBase)
        Dim DriveParts As New Dictionary(Of String, Partition)
        Dim Drive As HardDrive
        Dim DrivePart As Partition
        Debug("Selecting entries from Win32_DiskDrive...")
        Dim HardDisks As ManagementObjectSearcher = WMISearcher
        Debug("Selecting entries from Win32_DiskPartition...")
        Dim DiskPartitions As New ManagementObjectSearcher("SELECT * FROM Win32_DiskPartition")

        'TODO: Used not implemented in Windows
        'HDD Prober
        Debug("Getting the base objects...")
        Debug("TODO: Used not implemented in Windows")
        For Each Hdd As ManagementBaseObject In HardDisks.Get
            Try
                Dim DiskIndexHdd As Integer = Hdd("Index")
                Dim DeviceID As String = Hdd("DeviceID")
                Dim DeviceSize As String = Hdd("Size")
                Dim DeviceModel As String = Hdd("Model")
                Dim DeviceManufacturer As String = Hdd("Manufacturer")
                Dim DeviceSerialNumber As String = Hdd("SerialNumber")
                Debug("Got information. DiskIndexHdd: {0}, DeviceID: {1}, DeviceSize: {2}, DeviceModel: {3}, DeviceManufacturer: {4}", DiskIndexHdd, DeviceID, DeviceSize, DeviceModel, DeviceManufacturer)

                Dim DriveNo As Integer
                Debug("Getting the partiton base objects...")
                For Each Manage As ManagementBaseObject In DiskPartitions.Get
                    Try
                        Dim PartitionDeviceID As String = Manage("DeviceID")
                        Dim PartitionFilesystem As String = Manage("Type")
                        Dim PartitionSize As String = Manage("Size")
                        Dim PartitionIndex As String = Manage("Index")
                        DriveNo = Manage("DiskIndex")
                        Debug("Got information. PartitionDeviceID: {0}, PartitionFilesystem: {1}, PartitionSize: {2}, PartitionIndex: {3}, DriveNo: {4}", PartitionDeviceID, PartitionFilesystem, PartitionSize, PartitionIndex, DriveNo)

                        If DiskIndexHdd = DriveNo Then
                            DrivePart = New Partition(PartitionDeviceID, PartitionFilesystem, PartitionSize, "0")
                            DriveParts.Add("Physical partition in " & Hdd("Model") & " (" & DiskIndexHdd & ") : " & PartitionIndex, DrivePart)
                            Debug("Added partition {0} to the list of drive {1}'s partitions.", PartitionIndex, DiskIndexHdd)
                        End If
                    Catch ex As Exception
                        Debug("Error: {0}", ex.Message)
                        Continue For
                    End Try
                Next

                'TODO: Speed not implemented in Windows
                Debug("TODO: Speed not implemented in Windows")
                Drive = New HardDrive(DeviceID, DeviceSize, DeviceModel, DeviceManufacturer, "", DeviceSerialNumber, DriveParts)
                HDDParsed.Add(DeviceModel & " (" & DiskIndexHdd & ")", Drive)
                Debug("Added {0} to the list of parsed drives.", DeviceModel)
            Catch ex As Exception
                Debug("Error: {0}", ex.Message)
                Continue For
            End Try
        Next
        Return HDDParsed
    End Function

End Class
