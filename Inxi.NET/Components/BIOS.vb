
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

Public Class BIOS

    ''' <summary>
    ''' BIOS (American Megatrends, Award, etc.)
    ''' </summary>
    Public ReadOnly BIOS As String
    ''' <summary>
    ''' BIOS Date
    ''' </summary>
    Public ReadOnly [Date] As String
    ''' <summary>
    ''' BIOS Version
    ''' </summary>
    Public ReadOnly Version As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(BIOS As String, [Date] As String, Version As String)
        Me.BIOS = BIOS
        Me.[Date] = [Date]
        Me.Version = Version
    End Sub

End Class
