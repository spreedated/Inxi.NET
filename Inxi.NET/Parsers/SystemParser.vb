
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

Class SystemParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses system info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function Parse(InxiToken As JToken, SystemProfilerToken As NSArray) As HardwareBase
        Dim SysInfo As HardwareBase
        If IsUnix() Then
            If IsMacOS() Then
                'TODO: Bits, DE, WM, and DM not implemented in macOS.
                'Check for data type
                Debug("Checking for data type...")
                Debug("TODO: Bits, DE, WM, and DM not implemented in macOS.")
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPSoftwareDataType" Then
                        Debug("DataType found: SPSoftwareDataType...")

                        'Get information of the system
                        Dim SoftwareEnum As NSArray = DataType("_items")
                        Debug("Enumerating system information...")
                        For Each SoftwareDict As NSDictionary In SoftwareEnum
                            'Get information of memory
                            Dim Hostname As String = SoftwareDict("local_host_name").ToObject
                            Dim Version As String = SoftwareDict("kernel_version").ToObject
                            Dim Distro As String = SoftwareDict("os_version").ToObject
                            Debug("Got information. Hostname: {0}, Version: {1}, Distro: {2}", Hostname, Version, Distro)

                            'Create an instance of system class
                            SysInfo = New SystemInfo(Hostname, Version, 64, Distro, "", "", "")
                        Next
                    End If
                Next
            Else
                Debug("Selecting the System token...")
                For Each InxiSys In InxiToken.SelectTokenKeyEndingWith("System")
                    'Get information of system
                    Dim Hostname As String = InxiSys.SelectTokenKeyEndingWith("Host")
                    Dim Version As String = InxiSys.SelectTokenKeyEndingWith("Kernel")
                    Dim Bits As Integer = InxiSys.SelectTokenKeyEndingWith("bits")
                    Dim Distro As String = InxiSys.SelectTokenKeyEndingWith("Distro")
                    Dim DesktopMan As String = InxiSys.SelectTokenKeyEndingWith("Desktop")
                    Dim WindowMan As String = InxiSys.SelectTokenKeyEndingWith("WM")
                    Dim DisplayMan As String = InxiSys.SelectTokenKeyEndingWith("dm")
                    If String.IsNullOrEmpty(WindowMan) Then WindowMan = InxiSys.SelectTokenKeyEndingWith("wm")
                    Debug("Got information. Hostname: {0}, Version: {1}, Distro: {2}, Bits: {3}, DesktopMan: {4}, WindowMan: {5}, DisplayMan: {6}", Hostname, Version, Distro, Bits, DesktopMan, WindowMan, DisplayMan)

                    'Create an instance of system class
                    SysInfo = New SystemInfo(Hostname, Version, Bits, Distro, DesktopMan, WindowMan, DisplayMan)
                Next
            End If
        Else
            Debug("Selecting entries from Win32_OperatingSystem...")
            Dim WMISystem As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")

            'Get information of system
            'TODO: Desktop Manager and Display Manager are not implemented on Windows.
            Debug("Getting the base objects...")
            Debug("TODO: Desktop Manager and Display Manager are not implemented on Windows.")
            For Each SystemBase As ManagementBaseObject In WMISystem.Get
                Dim Hostname As String = Net.Dns.GetHostName
                Dim Version As String = SystemBase("Version")
                Dim Bits As Integer = SystemBase("OSArchitecture").ToString.Replace("-bit", "")
                Dim Distro As String = SystemBase("Caption")
                Dim WM As String = If(Process.GetProcessesByName("dwm").Length > 0, "DWM", "Basic Window Manager")
                Debug("Got information. Hostname: {0}, Version: {1}, Distro: {2}, Bits: {3}, WM: {4}", Hostname, Version, Distro, Bits, WM)

                'Create an instance of system class
                SysInfo = New SystemInfo(Hostname, Version, Bits, Distro, "", WM, "")
            Next
        End If

#Disable Warning BC42104
        Return SysInfo
#Enable Warning BC42104
    End Function

End Class
