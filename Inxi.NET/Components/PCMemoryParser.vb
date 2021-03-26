
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

Module PCMemoryParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParsePCMemory(InxiToken As JToken, SystemProfilerToken As NSArray) As PCMemory
        Dim Mem As PCMemory
        If IsUnix() Then
            If IsMacOS() Then
                'Check for data type
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPHardwareDataType" Then
                        'Get information of a drive
                        'TODO: Used memory and free memory not implemented in macOS.
                        Dim HardwareEnum As NSArray = DataType("_items")
                        For Each HardwareDict As NSDictionary In HardwareEnum
                            'Get information of memory
                            Dim TotalMem As String = HardwareDict("physical_memory").ToObject

                            'Create an instance of memory class
                            Mem = New PCMemory(TotalMem, "", "")
                        Next
                    End If
                Next
            Else
                For Each InxiMem In InxiToken.SelectTokenKeyEndingWith("Info")
                    'Get information of memory
                    'TODO: Free memory is not implemented in Inxi.
                    Dim TotalMem As String = InxiMem.SelectTokenKeyEndingWith("Memory")
                    Dim UsedMem As String = InxiMem.SelectTokenKeyEndingWith("used")

                    'Create an instance of memory class
                    Mem = New PCMemory(TotalMem, UsedMem, "")
                Next
            End If
        Else
            Dim System As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            Dim TotalMem As Long
            Dim UsedMem As Long
            Dim FreeMem As Long

            'Get memory
            For Each OS As ManagementBaseObject In System.Get
                TotalMem = OS("TotalVisibleMemorySize")
                UsedMem = TotalMem - OS("FreePhysicalMemory")
                FreeMem = OS("FreePhysicalMemory")
            Next

            'Create an instance of memory class
            Mem = New PCMemory(TotalMem, UsedMem, FreeMem)
        End If

#Disable Warning BC42104
        Return Mem
#Enable Warning BC42104
    End Function

End Module
