
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

Imports System.IO

Public Class Inxi

    Public ReadOnly Hardware As HardwareInfo

    ''' <summary>
    ''' Intializes the new instance of Inxi class and parses hardware
    ''' </summary>
    Public Sub New()
        Me.New("/usr/bin/inxi", "/usr/bin/cpanel_json_xs")
    End Sub

    ''' <summary>
    ''' Initializes the new instance of Inxi class with specified path and parses hardware
    ''' </summary>
    ''' <param name="InxiPath">Path to Inxi executable. It's usually /usr/bin/inxi. Ignored in Windows.</param>
    ''' <param name="CpanelJsonXsPath">Path to CPanelJsonXS executable. It's usually /usr/bin/cpanel_json_xs. Ignored in Windows.</param>
    Public Sub New(InxiPath As String, CpanelJsonXsPath As String)
        If IsUnix() Then
            If IsMacOS() Then
                Hardware = New HardwareInfo(InxiPath)
            Else
                If File.Exists(InxiPath) And File.Exists(CpanelJsonXsPath) Then
                    Hardware = New HardwareInfo(InxiPath)
                Else
                    Throw New InvalidOperationException("You must have Inxi and libcpanel-json-xs-perl installed. (Could not find """ + InxiPath + """ and """ + CpanelJsonXsPath + """.)")
                End If
            End If
        Else
            Hardware = New HardwareInfo(InxiPath)
        End If
    End Sub

End Class

Module InxiInternalUtils

    ''' <summary>
    ''' Is the platform Unix?
    ''' </summary>
    Friend Function IsUnix()
        Return Environment.OSVersion.Platform = PlatformID.Unix
    End Function

    ''' <summary>
    ''' Is the Unix platform macOS?
    ''' </summary>
    Friend Function IsMacOS()
        If IsUnix() Then
            Dim UnameS As New Process
            Dim UnameSInfo As New ProcessStartInfo With {.FileName = "/usr/bin/uname", .Arguments = "-s",
                                                         .CreateNoWindow = True,
                                                         .UseShellExecute = False,
                                                         .WindowStyle = ProcessWindowStyle.Hidden,
                                                         .RedirectStandardOutput = True}
            UnameS.StartInfo = UnameSInfo
            UnameS.Start()
            UnameS.WaitForExit()
            Dim System As String = UnameS.StandardOutput.ReadToEnd
            Return System.Contains("Darwin")
        Else
            Return False
        End If
    End Function

End Module