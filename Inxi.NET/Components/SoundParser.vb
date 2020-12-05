﻿
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

Imports Newtonsoft.Json.Linq

Module SoundParser

    ''' <summary>
    ''' Parses sound cards
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParseSound(InxiToken As JToken) As Dictionary(Of String, Sound)
        Dim SPUParsed As New Dictionary(Of String, Sound)
        Dim SPU As Sound

        'SPU information fields
        Dim SPUName As String
        Dim SPUVendor As String
        Dim SPUDriver As String

        For Each InxiSPU In InxiToken.SelectToken("005#Audio")
            If InxiSPU("001#Device") IsNot Nothing Then
                'Get information of a sound card
                SPUName = InxiSPU("001#Device")
                SPUVendor = InxiSPU("002#vendor")
                SPUDriver = InxiSPU("003#driver")

                'Create an instance of sound class
                SPU = New Sound(SPUName, SPUVendor, SPUDriver)
                SPUParsed.Add(SPUName, SPU)
            End If
        Next

        Return SPUParsed
    End Function

End Module
