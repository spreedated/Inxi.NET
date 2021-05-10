
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

Imports Extensification.DictionaryExts
Imports Extensification.External.Newtonsoft.Json.JPropertyExts
Imports System.Management
Imports Newtonsoft.Json.Linq
Imports Claunia.PropertyList

Module GraphicsParser

    ''' <summary>
    ''' Parses graphics cards
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseGraphics(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, Graphics)
        Dim GPUParsed As New Dictionary(Of String, Graphics)
        Dim GPU As Graphics

        'GPU information fields
        Dim GPUName As String
        Dim GPUDriver As String
        Dim GPUDriverVersion As String

        If IsUnix() Then
            If IsMacOS() Then
                'TODO: GPU Driver and driver version not implemented (maybe kexts (kernel extensions) provide this information).
                'Check for data type
                Debug("Checking for data type...")
                Debug("TODO: GPU Driver and driver version not implemented (maybe kexts (kernel extensions) provide this information).")
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPDisplaysDataType" Then
                        Debug("DataType found: SPDisplaysDataType...")

                        'Get information of a graphics card
                        Dim GraphicsEnum As NSArray = DataType("_items")
                        Debug("Enumerating graphics cards...")
                        For Each GraphicsDict As NSDictionary In GraphicsEnum
                            GPUName = GraphicsDict("spdisplays_device-id").ToObject
                            Debug("Got information. GPUName: {0}", GPUName)

                            'Create an instance of graphics class
                            GPU = New Graphics(GPUName, "", "")
                            GPUParsed.Add(GPUName, GPU)
                            Debug("Added {0} to the list of parsed GPUs.", GPUName)
                        Next
                    End If
                Next
            Else
                Debug("Selecting the Graphics token...")
                For Each InxiGPU In InxiToken.SelectTokenKeyEndingWith("Graphics")
                    If InxiGPU.SelectTokenKeyEndingWith("Device") IsNot Nothing Then
                        'Get information of a graphics card
                        GPUName = InxiGPU.SelectTokenKeyEndingWith("Device")
                        GPUDriver = InxiGPU.SelectTokenKeyEndingWith("driver")
                        GPUDriverVersion = InxiGPU.SelectTokenKeyEndingWith("v")
                        Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}", GPUName, GPUDriver, GPUDriverVersion)

                        'Create an instance of graphics class
                        GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion)
                        GPUParsed.Add(GPUName, GPU)
                        Debug("Added {0} to the list of parsed GPUs.", GPUName)
                    End If
                Next
            End If
        Else
            Debug("Selecting entries from Win32_VideoController...")
            Dim GraphicsCards As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")

            'Get information of graphics cards
            Debug("Getting the base objects...")
            For Each Graphics As ManagementBaseObject In GraphicsCards.Get
                Try
                    'Get information of a graphics card
                    GPUName = Graphics("Caption")
                    GPUDriver = Graphics("InstalledDisplayDrivers")
                    GPUDriverVersion = Graphics("DriverVersion")
                    Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}", GPUName, GPUDriver, GPUDriverVersion)

                    'Create an instance of graphics class
                    GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion)
                    GPUParsed.AddIfNotFound(GPUName, GPU)
                    Debug("Added {0} to the list of parsed GPUs.", GPUName)
                Catch ex As Exception
                    Debug("Error: {0}", ex.Message)
                    Continue For
                End Try
            Next
        End If

        Return GPUParsed
    End Function

End Module
