
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

Imports System.Management
Imports Claunia.PropertyList
Imports Newtonsoft.Json.Linq

Public Class HardwareInfo

    ''' <summary>
    ''' List of hard drives detected
    ''' </summary>
    Public ReadOnly HDD As New Dictionary(Of String, HardDrive)
    ''' <summary>
    ''' List of logical hard drive partitions detected
    ''' </summary>
    Public ReadOnly LogicalParts As New Dictionary(Of String, WindowsLogicalPartition)
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
    ''' System information
    ''' </summary>
    Public ReadOnly System As SystemInfo
    ''' <summary>
    ''' Machine information
    ''' </summary>
    Public ReadOnly Machine As MachineInfo
    ''' <summary>
    ''' BIOS information
    ''' </summary>
    Public ReadOnly BIOS As BIOS
    ''' <summary>
    ''' RAM information
    ''' </summary>
    Public ReadOnly RAM As PCMemory

    ''' <summary>
    ''' Inxi token used for hardware probe
    ''' </summary>
    Friend InxiToken As JToken
    ''' <summary>
    ''' SystemProfiler token used for hardware probe
    ''' </summary>
    Friend SystemProfilerToken As NSArray

    ''' <summary>
    ''' Initializes a new instance of hardware info
    ''' </summary>
    Sub New(InxiPath As String, ParseFlags As InxiHardwareType)
        If IsUnix() Then
            If IsMacOS() Then
                'Start the SystemProfiler process
                Dim SystemProfilerProcess As New Process
                Dim SystemProfilerProcessInfo As New ProcessStartInfo With {.FileName = "/usr/sbin/system_profiler", .Arguments = "SPSoftwareDataType SPAudioDataType SPHardwareDataType SPNetworkDataType SPStorageDataType SPDisplaysDataType -xml",
                                                                            .CreateNoWindow = True,
                                                                            .UseShellExecute = False,
                                                                            .WindowStyle = ProcessWindowStyle.Hidden,
                                                                            .RedirectStandardOutput = True}
                SystemProfilerProcess.StartInfo = SystemProfilerProcessInfo
                Debug("Starting system_profiler with ""SPSoftwareDataType SPAudioDataType SPHardwareDataType SPNetworkDataType SPStorageDataType SPDisplaysDataType -xml""...")
                SystemProfilerProcess.Start()
                SystemProfilerProcess.WaitForExit(10000)
                SystemProfilerToken = PropertyListParser.Parse(Text.Encoding.Default.GetBytes(SystemProfilerProcess.StandardOutput.ReadToEnd))
                Debug("Token parsed.")
            Else
                'Start the Inxi process
                Dim InxiProcess As New Process
                Dim InxiProcessInfo As New ProcessStartInfo With {.FileName = InxiPath, .Arguments = "-Fxx --output json --output-file print",
                                                                  .CreateNoWindow = True,
                                                                  .UseShellExecute = False,
                                                                  .WindowStyle = ProcessWindowStyle.Hidden,
                                                                  .RedirectStandardOutput = True}
                InxiProcess.StartInfo = InxiProcessInfo
                Debug("Starting inxi with ""-Fxx --output json --output-file print""...")
                InxiProcess.Start()
                InxiProcess.WaitForExit()
                InxiToken = JToken.Parse(InxiProcess.StandardOutput.ReadToEnd)
                Debug("Token parsed.")
            End If
        End If

        'Ready variables
        Dim HDDParsed As New Dictionary(Of String, HardwareBase)
        Dim Logicals As New Dictionary(Of String, WindowsLogicalPartition)
        Dim CPUParsed As New Dictionary(Of String, HardwareBase)
        Dim GPUParsed As New Dictionary(Of String, HardwareBase)
        Dim SoundParsed As New Dictionary(Of String, HardwareBase)
        Dim NetParsed As New Dictionary(Of String, HardwareBase)
        Dim RAMParsed As PCMemory
        Dim BIOSParsed As BIOS
        Dim SystemParsed As SystemInfo
        Dim MachineParsed As MachineInfo

        'Parse hardware starting from HDD
        If ParseFlags.HasFlag(InxiHardwareType.HardDrive) Then
            Debug("Parsing HDD...")
            Dim BaseParser As New HardDriveParser
            HDDParsed = BaseParser.ParseAll(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.HardDrive)
        End If

        'Logical partitions
        If Not IsUnix() And ParseFlags.HasFlag(InxiHardwareType.HardDriveLogical) Then
            Debug("Parsing logical partitions...")
            Logicals = ParsePartitions(New ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk"))
            RaiseParsedEvent(InxiHardwareType.HardDriveLogical)
        End If

        'Processor
        If ParseFlags.HasFlag(InxiHardwareType.Processor) Then
            Debug("Parsing CPU...")
            Dim BaseParser As New ProcessorParser
            CPUParsed = BaseParser.ParseAll(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.Processor)
        End If

        'Graphics
        If ParseFlags.HasFlag(InxiHardwareType.Graphics) Then
            Debug("Parsing GPU...")
            Dim BaseParser As New GraphicsParser
            GPUParsed = BaseParser.ParseAll(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.Graphics)
        End If

        'Sound
        If ParseFlags.HasFlag(InxiHardwareType.Sound) Then
            Debug("Parsing sound...")
            Dim BaseParser As New SoundParser
            SoundParsed = BaseParser.ParseAll(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.Sound)
        End If

        'Network
        If ParseFlags.HasFlag(InxiHardwareType.Network) Then
            Debug("Parsing network...")
            Dim BaseParser As New NetworkParser
            NetParsed = BaseParser.ParseAll(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.Network)
        End If

        'PC Memory
        If ParseFlags.HasFlag(InxiHardwareType.PCMemory) Then
            Debug("Parsing RAM...")
            Dim BaseParser As New PCMemoryParser
            RAMParsed = BaseParser.Parse(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.PCMemory)
        End If

        'BIOS
        If ParseFlags.HasFlag(InxiHardwareType.BIOS) Then
            Debug("Parsing BIOS...")
            Dim BaseParser As New BIOSParser
            BIOSParsed = BaseParser.Parse(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.BIOS)
        End If

        'System
        If ParseFlags.HasFlag(InxiHardwareType.System) Then
            Debug("Parsing system...")
            Dim BaseParser As New SystemParser
            SystemParsed = BaseParser.Parse(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.System)
        End If

        'Machine
        If ParseFlags.HasFlag(InxiHardwareType.Machine) Then
            Debug("Parsing machine...")
            Dim BaseParser As New MachineParser
            MachineParsed = BaseParser.Parse(InxiToken, SystemProfilerToken)
            RaiseParsedEvent(InxiHardwareType.Machine)
        End If

        'Add the base to the correct type
        Dim HDDProcessed As New Dictionary(Of String, HardDrive)
        Dim CPUProcessed As New Dictionary(Of String, Processor)
        Dim GPUProcessed As New Dictionary(Of String, Graphics)
        Dim SoundProcessed As New Dictionary(Of String, Sound)
        Dim NetProcessed As New Dictionary(Of String, Network)

        'Hard drive
        For Each Parsed As String In HDDParsed.Keys
            Dim FinalHardware As HardDrive = HDDParsed(Parsed)
            HDDProcessed.Add(Parsed, FinalHardware)
        Next

        'Processor
        For Each Parsed As String In CPUParsed.Keys
            Dim FinalHardware As Processor = CPUParsed(Parsed)
            CPUProcessed.Add(Parsed, FinalHardware)
        Next

        'Graphics
        For Each Parsed As String In GPUParsed.Keys
            Dim FinalHardware As Graphics = GPUParsed(Parsed)
            GPUProcessed.Add(Parsed, FinalHardware)
        Next

        'Sound
        For Each Parsed As String In SoundParsed.Keys
            Dim FinalHardware As Sound = SoundParsed(Parsed)
            SoundProcessed.Add(Parsed, FinalHardware)
        Next

        'Network
        For Each Parsed As String In NetParsed.Keys
            Dim FinalHardware As Network = NetParsed(Parsed)
            NetProcessed.Add(Parsed, FinalHardware)
        Next

        'Install parsed information to current instance
        HDD = HDDProcessed
        If Not IsUnix() Then LogicalParts = Logicals
        CPU = CPUProcessed
        GPU = GPUProcessed
        Sound = SoundProcessed
        Network = NetProcessed
#Disable Warning BC42104
        RAM = RAMParsed
        BIOS = BIOSParsed
        System = SystemParsed
        Machine = MachineParsed
#Enable Warning BC42104
        Debug("Parsed information installed.")
    End Sub

End Class
