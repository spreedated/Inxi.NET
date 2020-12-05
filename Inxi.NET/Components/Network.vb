
'    Inxi.NET  Copyright (C) 2020  EoflaOE
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

Public Class Network

    ''' <summary>
    ''' Name of network card
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' Driver software used for network card
    ''' </summary>
    Public ReadOnly Driver As String
    ''' <summary>
    ''' Driver version
    ''' </summary>
    Public ReadOnly DriverVersion As String
    ''' <summary>
    ''' Duplex type (usually full)
    ''' </summary>
    Public ReadOnly Duplex As String
    ''' <summary>
    ''' Maximum speed that the device can handle
    ''' </summary>
    Public ReadOnly Speed As String
    ''' <summary>
    ''' State of network card
    ''' </summary>
    Public ReadOnly State As String
    ''' <summary>
    ''' MAC Address
    ''' </summary>
    Public ReadOnly MacAddress As String
    ''' <summary>
    ''' Device identifier
    ''' </summary>
    Public ReadOnly DeviceID As String


    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Driver As String, DriverVersion As String, Duplex As String, Speed As String, State As String, MacAddress As String, DeviceID As String)
        Me.Name = Name
        Me.Driver = Driver
        Me.DriverVersion = DriverVersion
        Me.Duplex = Duplex
        Me.Speed = Speed
        Me.State = State
        Me.MacAddress = MacAddress
        Me.DeviceID = DeviceID
    End Sub

End Class