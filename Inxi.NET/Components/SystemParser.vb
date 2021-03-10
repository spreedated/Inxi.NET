
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

Module SystemParser

    ''' <summary>
    ''' Parses system info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseSystem(InxiToken As JToken) As SystemInfo
        Dim SysInfo As SystemInfo
        If IsUnix() Then
            For Each InxiSys In InxiToken.SelectToken("000#System")
                'Get information of system
                Dim Hostname As String = InxiSys("000#Host")
                Dim Version As String = InxiSys("001#Kernel")
                Dim Bits As Integer = InxiSys("002#bits")
                Dim Distro As String = InxiSys("008#Distro")
                Dim DesktopMan As String = InxiSys("005#Desktop")
                Dim WindowMan As String = InxiSys("006#wm")
                Dim DisplayMan As String = InxiSys("007#dm")

                'Create an instance of system class
                SysInfo = New SystemInfo(Hostname, Version, Bits, Distro, DesktopMan, WindowMan, DisplayMan)
            Next
        Else
            Dim WMISystem As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")

            'Get information of system
            For Each SystemBase As ManagementBaseObject In WMISystem.Get
                Dim Hostname As String = Net.Dns.GetHostName
                Dim Version As String = SystemBase("Version")
                Dim Bits As Integer = SystemBase("OSArchitecture").ToString.Replace("-bit", "")
                Dim Distro As String = SystemBase("Caption")

                'Create an instance of system class
                'TODO: Desktop Manager, Window Manager, and Display Manager are not implemented on Windows.
                SysInfo = New SystemInfo(Hostname, Version, Bits, Distro, "", "", "")
            Next
        End If

#Disable Warning BC42104
        Return SysInfo
#Enable Warning BC42104
    End Function

End Module
