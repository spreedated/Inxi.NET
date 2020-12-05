
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

Public Class HardwareInfo

    ''' <summary>
    ''' List of hard drives detected
    ''' </summary>
    Public ReadOnly HDD As New Dictionary(Of String, HardDrive)
    ''' <summary>
    ''' List of processors detected
    ''' </summary>
    Public ReadOnly CPU As New Dictionary(Of String, Processor)
    ''' <summary>
    ''' List of graphics cards detected
    ''' </summary>
    Public ReadOnly GPU As New Dictionary(Of String, Graphics)
    ''' <summary>
    ''' List of sound cards detected
    ''' </summary>
    Public ReadOnly Sound As New Dictionary(Of String, Sound)
    ''' <summary>
    ''' List of network cards detected
    ''' </summary>
    Public ReadOnly Network As New Dictionary(Of String, Network)
    ''' <summary>
    ''' RAM information
    ''' </summary>
    Public ReadOnly RAM As PCMemory

    ''' <summary>
    ''' Inxi token used for hardware probe
    ''' </summary>
    Friend InxiToken As JToken

    ''' <summary>
    ''' Initializes a new instance of hardware info
    ''' </summary>
    Sub New(ByVal InxiPath As String)
        'Start the Inxi process
        Dim InxiProcess As New Process
        Dim InxiProcessInfo As New ProcessStartInfo With {.FileName = InxiPath, .Arguments = "-Fx --output json --output-file print",
                                                          .CreateNoWindow = True,
                                                          .UseShellExecute = False,
                                                          .WindowStyle = ProcessWindowStyle.Hidden,
                                                          .RedirectStandardOutput = True}
        InxiProcess.StartInfo = InxiProcessInfo
        InxiProcess.Start()
        InxiProcess.WaitForExit()
        InxiToken = JToken.Parse(InxiProcess.StandardOutput.ReadToEnd)

        'Ready variables
        Dim HDDParsed As Dictionary(Of String, HardDrive)
        Dim CPUParsed As Dictionary(Of String, Processor)
        Dim GPUParsed As Dictionary(Of String, Graphics)
        Dim SoundParsed As Dictionary(Of String, Sound)
        Dim NetParsed As Dictionary(Of String, Network)
        Dim RAMParsed As PCMemory

        'Parse hardware
        HDDParsed = ParseHardDrives(InxiToken)
        CPUParsed = ParseProcessors(InxiToken)
        GPUParsed = ParseGraphics(InxiToken)
        SoundParsed = ParseSound(InxiToken)
        NetParsed = ParseNetwork(InxiToken)
        RAMParsed = ParsePCMemory(InxiToken)

        'Install parsed information to current instance
        HDD = HDDParsed
        CPU = CPUParsed
        GPU = GPUParsed
        Sound = SoundParsed
        Network = NetParsed
        RAM = RAMParsed
    End Sub

End Class
