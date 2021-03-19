
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
                'Check for data type
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPDisplaysDataType" Then
                        'Get information of a graphics card
                        'TODO: GPU Driver and driver version not implemented (maybe kexts (kernel extensions) provide this information).
                        Dim GraphicsEnum As NSArray = DataType("_items")
                        For Each GraphicsDict As NSDictionary In GraphicsEnum
                            GPUName = GraphicsDict("spdisplays_device-id").ToObject

                            'Create an instance of graphics class
                            GPU = New Graphics(GPUName, "", "")
                            GPUParsed.Add(GPUName, GPU)
                        Next
                    End If
                Next
            Else
                For Each InxiGPU In InxiToken.SelectToken("004#Graphics")
                    If InxiGPU("001#Device") IsNot Nothing Then
                        'Get information of a graphics card
                        GPUName = InxiGPU("001#Device")
                        GPUDriver = InxiGPU("002#driver")
                        GPUDriverVersion = InxiGPU("003#v")

                        'Create an instance of graphics class
                        GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion)
                        GPUParsed.Add(GPUName, GPU)
                    End If
                Next
            End If
        Else
            Dim GraphicsCards As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")
            For Each Graphics As ManagementBaseObject In GraphicsCards.Get
                Try
                    GPU = New Graphics(Graphics("Caption"), Graphics("InstalledDisplayDrivers"), Graphics("DriverVersion"))
                    GPUParsed.AddIfNotFound(Graphics("Caption"), GPU)
                Catch ex As Exception
                    Continue For
                End Try
            Next
        End If

        Return GPUParsed
    End Function

End Module
