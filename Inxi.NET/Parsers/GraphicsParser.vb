
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

Class GraphicsParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses graphics cards
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function ParseAll(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        Dim GPUParsed As Dictionary(Of String, HardwareBase)

        If IsUnix() Then
            If IsMacOS() Then
                GPUParsed = ParseAllMacOS(SystemProfilerToken)
            Else
                GPUParsed = ParseAllLinux(InxiToken)
            End If
        Else
            Debug("Selecting entries from Win32_VideoController...")
            Dim GraphicsCards As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")
            GPUParsed = ParseAllWindows(GraphicsCards)
        End If

        Return GPUParsed
    End Function

    Overrides Function ParseAllLinux(InxiToken As JToken) As Dictionary(Of String, HardwareBase)
        Dim GPUParsed As New Dictionary(Of String, HardwareBase)
        Dim GPU As Graphics

        'GPU information fields
        Dim GPUName As String
        Dim GPUDriver As String
        Dim GPUDriverVersion As String
        Dim GPUBusID As String
        Dim GPUChipID As String

        Debug("Selecting the Graphics token...")
        For Each InxiGPU In InxiToken.SelectTokenKeyEndingWith("Graphics")
            If InxiGPU.SelectTokenKeyEndingWith("Device") IsNot Nothing Then
                'Get information of a graphics card
                GPUName = InxiGPU.SelectTokenKeyEndingWith("Device")
                GPUDriver = InxiGPU.SelectTokenKeyEndingWith("driver")
                GPUDriverVersion = InxiGPU.SelectTokenKeyEndingWith("v")
                GPUChipID = InxiGPU.SelectTokenKeyEndingWith("chip ID")
                GPUBusID = InxiGPU.SelectTokenKeyEndingWith("bus ID")
                Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID)

                'Create an instance of graphics class
                GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID)
                GPUParsed.Add(GPUName, GPU)
                Debug("Added {0} to the list of parsed GPUs.", GPUName)
            End If
        Next
        Return GPUParsed
    End Function

    Overrides Function ParseAllMacOS(SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        Dim GPUParsed As New Dictionary(Of String, HardwareBase)
        Dim GPU As Graphics

        'GPU information fields
        Dim GPUName As String
        Dim GPUChipID As String

        'TODO: GPU Driver, bus ID, and driver version not implemented in macOS (maybe kexts (kernel extensions) provide this information)
        'Check for data type
        Debug("Checking for data type...")
        Debug("TODO: GPU Driver, bus ID, and driver version not implemented in macOS (maybe kexts (kernel extensions) provide this information).")
        For Each DataType As NSDictionary In SystemProfilerToken
            If DataType("_dataType").ToObject = "SPDisplaysDataType" Then
                Debug("DataType found: SPDisplaysDataType...")

                'Get information of a graphics card
                Dim GraphicsEnum As NSArray = DataType("_items")
                Debug("Enumerating graphics cards...")
                For Each GraphicsDict As NSDictionary In GraphicsEnum
                    GPUName = GraphicsDict("spdisplays_device-id").ToObject
                    GPUChipID = GraphicsDict("spdisplays_vendor-id").ToObject
                    Debug("Got information. GPUName: {0}, GPUChipID: {1}", GPUName)

                    'Create an instance of graphics class
                    GPU = New Graphics(GPUName, "", "", GPUChipID, "")
                    GPUParsed.Add(GPUName, GPU)
                    Debug("Added {0} to the list of parsed GPUs.", GPUName)
                Next
            End If
        Next
        Return GPUParsed
    End Function

    Overrides Function ParseAllWindows(WMISearcher As ManagementObjectSearcher) As Dictionary(Of String, HardwareBase)
        Dim GPUParsed As New Dictionary(Of String, HardwareBase)
        Dim GPU As Graphics
        Dim GraphicsCards As ManagementObjectSearcher = WMISearcher

        'GPU information fields
        Dim GPUName As String
        Dim GPUDriver As String
        Dim GPUDriverVersion As String
        Dim GPUBusID As String
        Dim GPUChipID As String

        'TODO: Bus ID not implemented in Windows
        'Get information of sound cards
        Debug("Getting the base objects...")
        Debug("TODO: Bus ID not implemented in Windows.")
        For Each Graphics As ManagementBaseObject In GraphicsCards.Get
            Try
                'Get information of a graphics card
                GPUName = Graphics("Caption")
                GPUDriver = Graphics("InstalledDisplayDrivers")
                GPUDriverVersion = Graphics("DriverVersion")
                GPUChipID = Graphics("PNPDeviceID")
                GPUBusID = ""
                Debug("Got information. GPUName: {0}, GPUDriver: {1}, GPUDriverVersion: {2}, GPUChipID: {3}, GPUBusID: {4}", GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID)

                'Create an instance of graphics class
                GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion, GPUChipID, GPUBusID)
                GPUParsed.AddIfNotFound(GPUName, GPU)
                Debug("Added {0} to the list of parsed GPUs.", GPUName)
            Catch ex As Exception
                Debug("Error: {0}", ex.Message)
                Continue For
            End Try
        Next
        Return GPUParsed
    End Function

End Class
