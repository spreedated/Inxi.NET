
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

Imports System.Runtime.CompilerServices
Imports Newtonsoft.Json.Linq

'TODO: Transfer those to Extensification once this version is released.
Public Module PropertyExtensions

    ''' <summary>
    ''' Gets a property name that ends with specified string
    ''' </summary>
    ''' <param name="Token">JSON token</param>
    ''' <param name="Containing">String to find at the end of string</param>
    ''' <returns>A property name if found; nothing if not found</returns>
    <Extension>
    Public Function GetPropertyNameEndingWith(ByVal Token As JToken, ByVal Containing As String) As String
        For Each TokenProperty As JProperty In Token
            If TokenProperty.Name.EndsWith(Containing) Then
                Return TokenProperty.Name
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' Gets a property name that contains the specified string
    ''' </summary>
    ''' <param name="Token">JSON token</param>
    ''' <param name="Containing">String to find in string</param>
    ''' <returns>A property name if found; nothing if not found</returns>
    <Extension>
    Public Function GetPropertyNameContaining(ByVal Token As JToken, ByVal Containing As String) As String
        For Each TokenProperty As JProperty In Token
            If TokenProperty.Name.Contains(Containing) Then
                Return TokenProperty.Name
            End If
        Next
        Return ""
    End Function

    ''' <summary>
    ''' Selects a token that has its key containing the specified string
    ''' </summary>
    ''' <param name="Token">JSON token</param>
    ''' <param name="Containing">String to find in the key string</param>
    ''' <returns>A token if found; nothing if not found</returns>
    <Extension>
    Public Function SelectTokenKeyContaining(ByVal Token As JToken, ByVal Containing As String) As JToken
        Dim PropertyName As String = Token.GetPropertyNameContaining(Containing)
        If Not String.IsNullOrEmpty(PropertyName) Then
            Return Token.SelectToken(PropertyName)
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Selects a token that has its key ending with the specified string
    ''' </summary>
    ''' <param name="Token">JSON token</param>
    ''' <param name="Containing">String to find at the end of key string</param>
    ''' <returns>A token if found; nothing if not found</returns>
    <Extension>
    Public Function SelectTokenKeyEndingWith(ByVal Token As JToken, ByVal Containing As String) As JToken
        Dim PropertyName As String = Token.GetPropertyNameEndingWith(Containing)
        If Not String.IsNullOrEmpty(PropertyName) Then
            Return Token.SelectToken(PropertyName)
        Else
            Return Nothing
        End If
    End Function

End Module
