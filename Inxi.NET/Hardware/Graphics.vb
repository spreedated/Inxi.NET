
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

Public Class Graphics
    Inherits HardwareBase

    ''' <summary>
    ''' Name of graphics card
    ''' </summary>
    Public Overrides ReadOnly Property Name As String
    ''' <summary>
    ''' Driver software used for graphics card
    ''' </summary>
    Public ReadOnly Property Driver As String
    ''' <summary>
    ''' Driver version
    ''' </summary>
    Public ReadOnly Property DriverVersion As String
    ''' <summary>
    ''' Device chip ID
    ''' </summary>
    Public ReadOnly Property ChipID As String
    ''' <summary>
    ''' Device bus ID
    ''' </summary>
    Public ReadOnly Property BusID As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Driver As String, DriverVersion As String, ChipID As String, BusID As String)
        Me.Name = Name
        Me.Driver = Driver
        Me.DriverVersion = DriverVersion
        Me.ChipID = ChipID
        Me.BusID = BusID
    End Sub

End Class