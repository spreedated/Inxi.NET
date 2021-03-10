
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

Public Class Processor

    ''' <summary>
    ''' Name of processor
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' Core Topology
    ''' </summary>
    Public ReadOnly Topology As String
    ''' <summary>
    ''' Processor type
    ''' </summary>
    Public ReadOnly Type As String
    ''' <summary>
    ''' Processor milestone (Kaby Lake, Coffee Lake, ...)
    ''' </summary>
    Public ReadOnly Milestone As String
    ''' <summary>
    ''' Processor features
    ''' </summary>
    Public ReadOnly Flags() As String
    ''' <summary>
    ''' Processor bits
    ''' </summary>
    Public ReadOnly Bits As Integer
    ''' <summary>
    ''' L2 Cache
    ''' </summary>
    Public ReadOnly L2 As String
    ''' <summary>
    ''' L3 Cache
    ''' </summary>
    Public ReadOnly L3 As Integer
    ''' <summary>
    ''' CPU Rev
    ''' </summary>
    Public ReadOnly CPURev As String
    ''' <summary>
    ''' CPU BogoMips
    ''' </summary>
    Public ReadOnly CPUBogoMips As Integer
    ''' <summary>
    ''' Processor speed
    ''' </summary>
    Public ReadOnly Speed As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Topology As String, Type As String, Bits As Integer, Milestone As String, Flags() As String, L2 As String, L3 As Integer, CPURev As String, CPUBogoMips As Integer, Speed As String)
        Me.Name = Name
        Me.Topology = Topology
        Me.Type = Type
        Me.Bits = Bits
        Me.Milestone = Milestone
        Me.Flags = Flags
        Me.L2 = L2
        Me.L3 = L3
        Me.CPURev = CPURev
        Me.CPUBogoMips = CPUBogoMips
        Me.Speed = Speed
    End Sub

End Class