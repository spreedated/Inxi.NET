
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

Public Class SystemInfo

    ''' <summary>
    ''' Host name
    ''' </summary>
    Public ReadOnly Hostname As String
    ''' <summary>
    ''' Linux kernel version or Windows NT kernel version
    ''' </summary>
    Public ReadOnly SystemVersion As String
    ''' <summary>
    ''' System bits
    ''' </summary>
    Public ReadOnly SystemBits As Integer
    ''' <summary>
    ''' System name
    ''' </summary>
    Public ReadOnly SystemDistro As String
    ''' <summary>
    ''' Desktop manager
    ''' </summary>
    Public ReadOnly DesktopManager As String
    ''' <summary>
    ''' Window manager
    ''' </summary>
    Public ReadOnly WindowManager As String
    ''' <summary>
    ''' Display manager
    ''' </summary>
    Public ReadOnly DisplayManager As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Hostname As String, SystemVersion As String, SystemBits As Integer, SystemDistro As String, DesktopManager As String, WindowManager As String, DisplayManager As String)
        Me.Hostname = Hostname
        Me.SystemVersion = SystemVersion
        Me.SystemBits = SystemBits
        Me.SystemDistro = SystemDistro
        Me.DesktopManager = DesktopManager
        Me.WindowManager = WindowManager
        Me.DisplayManager = DisplayManager
    End Sub

End Class
