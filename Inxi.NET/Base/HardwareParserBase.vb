﻿
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

Imports Claunia.PropertyList
Imports Newtonsoft.Json.Linq

Public MustInherit Class HardwareParserBase
    Implements IHardwareParser

    Public Overridable Function Parse(InxiToken As JToken, SystemProfilerToken As NSArray) As HardwareBase Implements IHardwareParser.Parse
        Throw New NotImplementedException("This hardware parser is not implemented yet!")
    End Function

    Public Overridable Function ParseAll(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase) Implements IHardwareParser.ParseAll
        Throw New NotImplementedException("This hardware parser is not implemented yet!")
    End Function

End Class
