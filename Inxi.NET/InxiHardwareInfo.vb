
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
    Sub New(ByVal InxiPath As String, ParseFlags As InxiParseFlags)
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
        Dim HDDParsed As Dictionary(Of String, HardDrive)
        Dim Logicals As Dictionary(Of String, WindowsLogicalPartition)
        Dim CPUParsed As Dictionary(Of String, Processor)
        Dim GPUParsed As Dictionary(Of String, Graphics)
        Dim SoundParsed As Dictionary(Of String, Sound)
        Dim NetParsed As Dictionary(Of String, Network)
        Dim RAMParsed As PCMemory
        Dim BIOSParsed As BIOS
        Dim SystemParsed As SystemInfo
        Dim MachineParsed As MachineInfo

        'Parse hardware starting from HDD
        If ParseFlags.HasFlag(InxiParseFlags.HardDrive) Then
            Debug("Parsing HDD...")
            HDDParsed = ParseHardDrives(InxiToken, SystemProfilerToken)
        End If

        'Logical partitions
        If Not IsUnix() And ParseFlags.HasFlag(InxiParseFlags.HardDriveLogical) Then
            Debug("Parsing logical partitions...")
            Logicals = ParsePartitions(New ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk"))
        End If

        'Processor
        If ParseFlags.HasFlag(InxiParseFlags.Processor) Then
            Debug("Parsing CPU...")
            CPUParsed = ParseProcessors(InxiToken, SystemProfilerToken)
        End If

        'Graphics
        If ParseFlags.HasFlag(InxiParseFlags.Graphics) Then
            Debug("Parsing GPU...")
            GPUParsed = ParseGraphics(InxiToken, SystemProfilerToken)
        End If

        'Sound
        If ParseFlags.HasFlag(InxiParseFlags.Sound) Then
            Debug("Parsing sound...")
            SoundParsed = ParseSound(InxiToken, SystemProfilerToken)
        End If

        'Network
        If ParseFlags.HasFlag(InxiParseFlags.Network) Then
            Debug("Parsing network...")
            NetParsed = ParseNetwork(InxiToken, SystemProfilerToken)
        End If

        'PC Memory
        If ParseFlags.HasFlag(InxiParseFlags.PCMemory) Then
            Debug("Parsing RAM...")
            RAMParsed = ParsePCMemory(InxiToken, SystemProfilerToken)
        End If

        'BIOS
        If ParseFlags.HasFlag(InxiParseFlags.BIOS) Then
            Debug("Parsing BIOS...")
            BIOSParsed = ParseBIOS(InxiToken, SystemProfilerToken)
        End If

        'System
        If ParseFlags.HasFlag(InxiParseFlags.System) Then
            Debug("Parsing system...")
            SystemParsed = ParseSystem(InxiToken, SystemProfilerToken)
        End If

        'Machine
        If ParseFlags.HasFlag(InxiParseFlags.Machine) Then
            Debug("Parsing machine...")
            MachineParsed = ParseMachine(InxiToken, SystemProfilerToken)
        End If

        'Install parsed information to current instance
#Disable Warning BC42104
        HDD = HDDParsed
        If Not IsUnix() Then LogicalParts = Logicals
        CPU = CPUParsed
        GPU = GPUParsed
        Sound = SoundParsed
        Network = NetParsed
        RAM = RAMParsed
        BIOS = BIOSParsed
        System = SystemParsed
        Machine = MachineParsed
        Debug("Parsed information installed.")
#Enable Warning BC42104
    End Sub

End Class
