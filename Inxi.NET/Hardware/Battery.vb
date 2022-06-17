
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

Public Class Battery
    Inherits HardwareBase

    ''' <summary>
    ''' Battery ID
    ''' </summary>
    Public Overrides ReadOnly Property Name As String
    ''' <summary>
    ''' The battery charge percentage
    ''' </summary>
    Public ReadOnly Property Charge As Integer
    ''' <summary>
    ''' Battery condition
    ''' </summary>
    Public ReadOnly Property Condition As String
    ''' <summary>
    ''' Battery voltage
    ''' </summary>
    Public ReadOnly Property Volts As String
    ''' <summary>
    ''' Battery model
    ''' </summary>
    Public ReadOnly Property Model As String
    ''' <summary>
    ''' Battery status
    ''' </summary>
    Public ReadOnly Property Status As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Charge As Integer, Condition As String, Volts As String, Model As String, Status As String)
        Me.Name = Name
        Me.Charge = Charge
        Me.Condition = Condition
        Me.Volts = Volts
        Me.Model = Model
        Me.Status = Status
    End Sub

End Class