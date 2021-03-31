
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

Module SystemParser

    ''' <summary>
    ''' Parses system info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseSystem(InxiToken As JToken, SystemProfilerToken As NSArray) As SystemInfo
        Dim SysInfo As SystemInfo
        If IsUnix() Then
            If IsMacOS() Then
                'Check for data type
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPSoftwareDataType" Then
                        'Get information of a drive
                        'TODO: Bits, DE, WM, and DM not implemented in macOS.
                        Dim SoftwareEnum As NSArray = DataType("_items")
                        For Each SoftwareDict As NSDictionary In SoftwareEnum
                            'Get information of memory
                            Dim Hostname As String = SoftwareDict("local_host_name").ToObject
                            Dim Version As String = SoftwareDict("kernel_version").ToObject
                            Dim Distro As String = SoftwareDict("os_version").ToObject

                            'Create an instance of system class
                            SysInfo = New SystemInfo(Hostname, Version, 64, Distro, "", "", "")
                        Next
                    End If
                Next
            Else
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

                    'Create an instance of system class
                    SysInfo = New SystemInfo(Hostname, Version, Bits, Distro, DesktopMan, WindowMan, DisplayMan)
                Next
            End If
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
