
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

Public Class Sound

    ''' <summary>
    ''' Name of sound card
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' The maker of sound card
    ''' </summary>
    Public ReadOnly Vendor As String
    ''' <summary>
    ''' Driver software used for sound card
    ''' </summary>
    Public ReadOnly Driver As String
    ''' <summary>
    ''' Device chip ID
    ''' </summary>
    Public ReadOnly ChipID As String
    ''' <summary>
    ''' Device bus ID
    ''' </summary>
    Public ReadOnly BusID As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Vendor As String, Driver As String, ChipID As String, BusID As String)
        Me.Name = Name
        Me.Vendor = Vendor
        Me.Driver = Driver
        Me.ChipID = ChipID
        Me.BusID = BusID
    End Sub

End Class