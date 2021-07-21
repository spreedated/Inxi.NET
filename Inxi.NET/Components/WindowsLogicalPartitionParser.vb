
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

Module WindowsLogicalPartitionParser

    Function ParsePartitions(WMIObject As ManagementObjectSearcher) As Dictionary(Of String, WindowsLogicalPartition)
        Dim DriveParts As New Dictionary(Of String, WindowsLogicalPartition)
        Dim DrivePart As WindowsLogicalPartition

        'Get information of logical partitions
        Debug("Getting the base objects...")
        For Each Part As ManagementBaseObject In WMIObject.Get
            Try
                'Get information of a logical partition
                Dim LogicalID As String = Part("DeviceID")
                Dim LogicalFileSystem As String = Part("FileSystem")
                Dim LogicalSize As String = Part("Size")
                Dim LogicalUsed As String = CULng(Part("Size") - Part("FreeSpace"))
                Debug("Got information. LogicalID: {0}, LogicalFileSystem: {1}, LogicalSize: {2}, LogicalUsed: {3}", LogicalID, LogicalFileSystem, LogicalSize, LogicalUsed)

                'Create an instance of logical partition class
                DrivePart = New WindowsLogicalPartition(LogicalID, LogicalFileSystem, LogicalSize, LogicalUsed)
                DriveParts.Add("Logical partition " & LogicalID, DrivePart)
            Catch ex As Exception
                Debug("Error: {0}", ex.Message)
                Continue For
            End Try
        Next

        Return DriveParts
    End Function

End Module