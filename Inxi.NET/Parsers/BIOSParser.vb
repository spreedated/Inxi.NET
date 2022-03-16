
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
Imports Claunia.PropertyList
Imports Newtonsoft.Json.Linq
Imports Extensification.External.Newtonsoft.Json.JPropertyExts

Class BIOSParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses BIOS info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function Parse(InxiToken As JToken, SystemProfilerToken As NSArray) As HardwareBase
        Dim BIOSInfo As BIOS

        If IsUnix() Then
            If IsMacOS() Then
                BIOSInfo = ParseMacOS(SystemProfilerToken)
            Else
                BIOSInfo = ParseLinux(InxiToken)
            End If
        Else
            Debug("Selecting entries from Win32_BIOS...")
            Dim WMIBIOS As New ManagementObjectSearcher("SELECT * FROM Win32_BIOS")
            BIOSInfo = ParseWindows(WMIBIOS)
        End If

        Return BIOSInfo
    End Function

    Overrides Function ParseLinux(InxiToken As JToken) As HardwareBase
        Dim BIOSInfo As BIOS

        Debug("Selecting the Machine token...")
        For Each InxiMachine In InxiToken.SelectTokenKeyEndingWith("Machine")
            'Get information of system
            Dim BIOS As String = InxiMachine.SelectTokenKeyEndingWith("BIOS")
            Dim [Date] As String = InxiMachine.SelectTokenKeyEndingWith("date")
            Dim Version As String = InxiMachine.SelectTokenKeyEndingWith("v")
            Debug("Got information. BIOS: {0}, Date: {1}, Version: {2}", BIOS, [Date], Version)

            'Create an instance of system class
            BIOSInfo = New BIOS(BIOS, [Date], Version)
        Next

#Disable Warning BC42104
        Return BIOSInfo
#Enable Warning BC42104
    End Function

    Overrides Function ParseMacOS(SystemProfilerToken As NSArray) As HardwareBase
        Dim BIOSInfo As BIOS

        'TODO: Not implemented.
        Debug("TODO: Not implemented")
        BIOSInfo = New BIOS("Apple", "5/23/2018", "1.0")
        Return BIOSInfo
    End Function

    Overrides Function ParseWindows(WMISearcher As ManagementObjectSearcher) As HardwareBase
        Dim BIOSInfo As BIOS
        Dim WMIBIOS As ManagementObjectSearcher = WMISearcher

        'Get information of system
        Debug("Getting the base objects...")
        For Each BIOSBase As ManagementBaseObject In WMIBIOS.Get
            Dim BIOS As String = BIOSBase("Caption")
            Dim [Date] As String = BIOSBase("ReleaseDate")
            Dim Version As String = BIOSBase("Version")
            Debug("Got information. BIOS: {0}, Date: {1}, Version: {2}", BIOS, [Date], Version)

            'Create an instance of system class
            BIOSInfo = New BIOS(BIOS, [Date], Version)
        Next

#Disable Warning BC42104
        Return BIOSInfo
#Enable Warning BC42104
    End Function

End Class
