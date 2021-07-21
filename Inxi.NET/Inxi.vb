
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
Imports System.Reflection.Assembly

Public Class Inxi

    ''' <summary>
    ''' Hardware information
    ''' </summary>
    Public ReadOnly Hardware As HardwareInfo

    ''' <summary>
    ''' Intializes the new instance of Inxi class and parses hardware
    ''' </summary>
    Public Sub New()
        Me.New("/usr/bin/inxi", "/usr/bin/cpanel_json_xs", [Enum].GetValues(GetType(InxiHardwareType)).Cast(Of Integer)().Sum)
    End Sub

    ''' <summary>
    ''' Intializes the new instance of Inxi class and parses hardware
    ''' </summary>
    ''' <param name="HardwareTypes">Hardware types to parse</param>
    Public Sub New(HardwareTypes As InxiHardwareType)
        Me.New("/usr/bin/inxi", "/usr/bin/cpanel_json_xs", HardwareTypes)
    End Sub

    ''' <summary>
    ''' Initializes the new instance of Inxi class with specified path and parses hardware
    ''' </summary>
    ''' <param name="InxiPath">Path to Inxi executable. It's usually /usr/bin/inxi. Ignored in Windows.</param>
    ''' <param name="CpanelJsonXsPath">Path to CPanelJsonXS executable. It's usually /usr/bin/cpanel_json_xs. Ignored in Windows.</param>
    ''' <param name="HardwareTypes">Hardware types to parse</param>
    Public Sub New(InxiPath As String, CpanelJsonXsPath As String, HardwareTypes As InxiHardwareType)
        If IsUnix() Then
            Debug("Inxi.NET {0} running on Unix.", GetExecutingAssembly().GetName().Version.ToString())
            Debug("Inxi parse flags: {0}", HardwareTypes)
            If IsMacOS() Then
                Debug("Type: macOS")
                Hardware = New HardwareInfo(InxiPath, HardwareTypes)
            Else
                Debug("Type: Unix")
                Debug("Looking for {0} and {1}...", InxiPath, CpanelJsonXsPath)
                If File.Exists(InxiPath) And File.Exists(CpanelJsonXsPath) Then
                    Debug("They're installed.")
                    Hardware = New HardwareInfo(InxiPath, HardwareTypes)
                Else
                    Debug("They're not installed.")
                    Throw New InvalidOperationException("You must have Inxi and libcpanel-json-xs-perl installed. (Could not find """ + InxiPath + """ and """ + CpanelJsonXsPath + """.)")
                End If
            End If
        Else
            Debug("Inxi.NET {0} running on Windows.", GetExecutingAssembly().GetName().Version.ToString())
            Hardware = New HardwareInfo(InxiPath, HardwareTypes)
        End If
    End Sub

End Class