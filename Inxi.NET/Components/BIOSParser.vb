
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

Module BIOSParser

    ''' <summary>
    ''' Parses BIOS info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseBIOS(InxiToken As JToken, SystemProfilerToken As NSArray) As BIOS
        Dim BIOSInfo As BIOS
        If IsUnix() Then
            If IsMacOS() Then
                'TODO: Not implemented.
                BIOSInfo = New BIOS("Apple", "5/23/2018", "1.0")
            Else
                For Each InxiMachine In InxiToken.SelectTokenKeyEndingWith("Machine")
                    'Get information of system
                    Dim BIOS As String = InxiMachine.SelectTokenKeyEndingWith("BIOS")
                    Dim [Date] As String = InxiMachine.SelectTokenKeyEndingWith("date")
                    Dim Version As String = InxiMachine.SelectTokenKeyEndingWith("v")

                    'Create an instance of system class
                    BIOSInfo = New BIOS(BIOS, [Date], Version)
                Next
            End If
        Else
            Dim WMIBIOS As New ManagementObjectSearcher("SELECT * FROM Win32_BIOS")

            'Get information of system
            For Each BIOSBase As ManagementBaseObject In WMIBIOS.Get
                Dim BIOS As String = BIOSBase("Caption")
                Dim [Date] As String = BIOSBase("ReleaseDate")
                Dim Version As String = BIOSBase("Version")

                'Create an instance of system class
                BIOSInfo = New BIOS(BIOS, [Date], Version)
            Next
        End If

#Disable Warning BC42104
        Return BIOSInfo
#Enable Warning BC42104
    End Function

End Module
