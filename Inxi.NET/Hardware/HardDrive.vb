
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

Public Class HardDrive
    Inherits HardwareBase

    ''' <summary>
    ''' The name of hard drive
    ''' </summary>
    Public Overrides ReadOnly Property Name As String
    ''' <summary>
    ''' The udev ID of hard drive
    ''' </summary>
    Public ReadOnly Property ID As String
    ''' <summary>
    ''' The size of the drive
    ''' </summary>
    Public ReadOnly Property Size As String
    ''' <summary>
    ''' The model of the drive
    ''' </summary>
    Public ReadOnly Property Model As String
    ''' <summary>
    ''' The make of the drive
    ''' </summary>
    Public ReadOnly Property Vendor As String
    ''' <summary>
    ''' The speed of the drive
    ''' </summary>
    Public ReadOnly Property Speed As String
    ''' <summary>
    ''' The serial number
    ''' </summary>
    Public ReadOnly Property Serial As String
    ''' <summary>
    ''' List of partitions
    ''' </summary>
    Public ReadOnly Property Partitions As New Dictionary(Of String, Partition)

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(ID As String, Size As String, Model As String, Vendor As String, Speed As String, Serial As String, Partitions As Dictionary(Of String, Partition))
        Me.ID = ID
        Me.Size = Size
        Me.Model = Model
        Me.Vendor = Vendor
        Name = $"{Vendor} {Model}"
        Me.Speed = Speed
        Me.Serial = Serial
        Me.Partitions = Partitions
    End Sub

End Class