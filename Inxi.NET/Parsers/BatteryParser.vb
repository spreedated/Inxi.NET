
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
Imports Claunia.PropertyList
Imports Newtonsoft.Json.Linq

Class BatteryParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses battery info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function ParseAllToList(InxiToken As JToken, SystemProfilerToken As NSArray) As List(Of HardwareBase)
        Dim Batteries As List(Of HardwareBase)

        If IsUnix() Then
            If IsMacOS() Then
                Batteries = ParseAllToListMacOS(SystemProfilerToken)
            Else
                Batteries = ParseAllToListLinux(InxiToken)
            End If
        Else
            Debug("Selecting entries from Win32_OperatingSystem...")
            Dim WMISystem As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            Batteries = ParseAllToListWindows(WMISystem)
        End If

        Return Batteries
    End Function
    Overrides Function ParseAllToListLinux(InxiToken As JToken) As List(Of HardwareBase)
        Dim Batteries As New List(Of HardwareBase)
        Dim Battery As Battery

        Debug("Selecting the Battery token...")
        For Each InxiSys In InxiToken.SelectTokenKeyEndingWith("Battery")
            'Get information of battery
            Dim Name As String = InxiSys.SelectTokenKeyEndingWith("ID")
            Dim Charge As Integer = Convert.ToInt32(InxiSys.SelectTokenKeyEndingWith("charge").ToString().Replace("%", ""))
            Dim Condition As String = InxiSys.SelectTokenKeyEndingWith("condition")
            Dim Volts As String = InxiSys.SelectTokenKeyEndingWith("volts")
            Dim Model As String = InxiSys.SelectTokenKeyEndingWith("model")
            Dim Status As String = InxiSys.SelectTokenKeyEndingWith("status")
            Debug("Got information. Name: {0}, Charge: {1}, Condition: {2}, Volts: {3}, Model: {4}, Status: {5}", Name, Charge, Condition, Volts, Model, Status)

            'Create an instance of battery class
            Battery = New Battery(Name, Charge, Condition, Volts, Model, Status)
            Batteries.Add(Battery)
        Next

#Disable Warning BC42104
        Return Batteries
#Enable Warning BC42104
    End Function

    Overrides Function ParseAllToListMacOS(SystemProfilerToken As NSArray) As List(Of HardwareBase)
        Dim Batteries As New List(Of HardwareBase)

        'TODO: Battery not implemented in macOS.
        Debug("TODO: Battery not implemented in macOS.")

        'Create an instance of battery class
        Dim Battery As New Battery("Battery", 100, "", "", "", "Not charging")
        Batteries.Add(Battery)

#Disable Warning BC42104
        Return Batteries
#Enable Warning BC42104
    End Function

    Overrides Function ParseAllToListWindows(WMISearcher As ManagementObjectSearcher) As List(Of HardwareBase)
        Dim Batteries As New List(Of HardwareBase)

        'TODO: Battery not implemented in Windows.
        Debug("TODO: Battery not implemented in Windows.")

        'Create an instance of battery class
        Dim Battery As New Battery("Battery", 100, "", "", "", "Not charging")
        Batteries.Add(Battery)

#Disable Warning BC42104
        Return Batteries
#Enable Warning BC42104
    End Function

End Class
