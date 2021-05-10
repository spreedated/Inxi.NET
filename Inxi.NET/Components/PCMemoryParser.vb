
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

Module PCMemoryParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParsePCMemory(InxiToken As JToken, SystemProfilerToken As NSArray) As PCMemory
        Dim Mem As PCMemory
        If IsUnix() Then
            If IsMacOS() Then
                'TODO: Used memory and free memory not implemented in macOS.
                'Check for data type
                Debug("Checking for data type...")
                Debug("TODO: Used memory and free memory not implemented in macOS.")
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPHardwareDataType" Then
                        Debug("DataType found: SPHardwareDataType...")

                        'Get information of a memory
                        Dim HardwareEnum As NSArray = DataType("_items")
                        Debug("Enumerating memory information...")
                        For Each HardwareDict As NSDictionary In HardwareEnum
                            'Get information of memory
                            Dim TotalMem As String = HardwareDict("physical_memory").ToObject
                            Debug("Got information. TotalMem: {0}", TotalMem)

                            'Create an instance of memory class
                            Mem = New PCMemory(TotalMem, "", "")
                        Next
                    End If
                Next
            Else
                'TODO: Free memory is not implemented in Inxi.
                Debug("TODO: Free memory is not implemented in Inxi.")
                Debug("Selecting the Info token...")
                For Each InxiMem In InxiToken.SelectTokenKeyEndingWith("Info")
                    'Get information of memory
                    Dim TotalMem As String = InxiMem.SelectTokenKeyEndingWith("Memory")
                    Dim UsedMem As String = InxiMem.SelectTokenKeyEndingWith("used")
                    Debug("Got information. TotalMem: {0}, UsedMem: {1}", TotalMem, UsedMem)

                    'Create an instance of memory class
                    Mem = New PCMemory(TotalMem, UsedMem, "")
                Next
            End If
        Else
            Debug("Selecting entries from Win32_OperatingSystem...")
            Dim System As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            Dim TotalMem As Long
            Dim UsedMem As Long
            Dim FreeMem As Long

            'Get memory
            Debug("Getting the base objects...")
            For Each OS As ManagementBaseObject In System.Get
                TotalMem = OS("TotalVisibleMemorySize")
                UsedMem = TotalMem - OS("FreePhysicalMemory")
                FreeMem = OS("FreePhysicalMemory")
                Debug("Got information. TotalMem: {0}, UsedMem: {1}, FreeMem: {2}", TotalMem, UsedMem, FreeMem)
            Next

            'Create an instance of memory class
            Mem = New PCMemory(TotalMem, UsedMem, FreeMem)
        End If

#Disable Warning BC42104
        Return Mem
#Enable Warning BC42104
    End Function

End Module
