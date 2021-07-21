
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

Public Class WindowsLogicalPartition

    ''' <summary>
    ''' The drive letter of drive partition
    ''' </summary>
    Public ReadOnly Letter As String
    ''' <summary>
    ''' The filesystem of partition
    ''' </summary>
    Public ReadOnly FileSystem As String
    ''' <summary>
    ''' The size of partition
    ''' </summary>
    Public ReadOnly Size As String
    ''' <summary>
    ''' The used size of partition
    ''' </summary>
    Public ReadOnly Used As String

    ''' <summary>
    ''' Installs specified values parsed to the class
    ''' </summary>
    Friend Sub New(Letter As String, FileSystem As String, Size As String, Used As String)
        Me.Letter = Letter
        Me.FileSystem = FileSystem
        Me.Size = Size
        Me.Used = Used
    End Sub

End Class