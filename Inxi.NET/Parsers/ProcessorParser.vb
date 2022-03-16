
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

Imports Extensification.ArrayExts
Imports Extensification.DictionaryExts
Imports Extensification.External.Newtonsoft.Json.JPropertyExts
Imports System.Management
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq
Imports Claunia.PropertyList

Class ProcessorParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function ParseAll(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        Dim CPUParsed As Dictionary(Of String, HardwareBase)

        If IsUnix() Then
            If IsMacOS() Then
                CPUParsed = ParseAllMacOS(SystemProfilerToken)
            Else
                CPUParsed = ParseAllLinux(InxiToken)
            End If
        Else
            Debug("Selecting entries from Win32_Processor...")
            Dim CPUClass As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
            CPUParsed = ParseAllWindows(CPUClass)
        End If

        Return CPUParsed
    End Function

    Overrides Function ParseAllLinux(InxiToken As JToken) As Dictionary(Of String, HardwareBase)
        Dim CPUParsed As New Dictionary(Of String, HardwareBase)
        Dim CPU As Processor
        Dim CPUSpeedReady As Boolean

        'CPU information fields
        Dim CPUName As String = ""
        Dim CPUTopology As String = ""
        Dim CPUType As String = ""
        Dim CPUBits As Integer
        Dim CPUMilestone As String = ""
#If Not NET45 Then
        Dim CPUFlags As String() = Array.Empty(Of String)
#Else
        Dim CPUFlags As String() = {}
#End If
        Dim CPUL2Size As String = ""
        Dim CPUL3Size As Integer = 0
        Dim CPUSpeed As String = ""
        Dim CPURev As String = ""
        Dim CPUBogoMips As Integer = 0

        'TODO: L3 cache is not implemented in Linux
        Debug("TODO: L3 cache is not implemented in Linux.")
        Debug("Selecting the CPU token...")
        For Each InxiCPU In InxiToken.SelectTokenKeyEndingWith("CPU")
            If Not CPUSpeedReady Then
                'Get information of a processor
                CPUName = InxiCPU.SelectTokenKeyEndingWith("model")
                CPUTopology = InxiCPU.SelectTokenKeyEndingWith("Topology")
                If String.IsNullOrEmpty(CPUTopology) Then CPUTopology = InxiCPU.SelectTokenKeyEndingWith("Info")
                CPUType = InxiCPU.SelectTokenKeyEndingWith("type")
                CPUBits = InxiCPU.SelectTokenKeyEndingWith("bits")
                CPUMilestone = InxiCPU.SelectTokenKeyEndingWith("arch")
                CPUL2Size = InxiCPU.SelectTokenKeyContaining("L2")
                CPURev = InxiCPU.SelectTokenKeyEndingWith("rev")
                CPUSpeedReady = True
            ElseIf InxiCPU.SelectTokenKeyEndingWith("flags") IsNot Nothing Then
                CPUFlags = CStr(InxiCPU.SelectTokenKeyEndingWith("flags")).Split(" "c)
                CPUBogoMips = InxiCPU.SelectTokenKeyEndingWith("bogomips")
            Else
                CPUSpeed = InxiCPU.SelectTokenKeyEndingWith("Speed")
            End If
            Debug("Got information. CPUName: {0}, CPUTopology: {1}, CPUType: {2}, CPUBits: {3}, CPUMilestone: {4}, CPUL2Size: {5}, CPURev: {6}, CPUFlags: {7}, CPUBogoMips: {8}, CPUSpeed: {9}", CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUL2Size, CPURev, CPUFlags.Length, CPUBogoMips, CPUSpeed)
        Next

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUFlags, CPUL2Size, CPUL3Size, CPURev, CPUBogoMips, CPUSpeed)
        CPUParsed.AddIfNotFound(CPUName, CPU)
        Debug("Added {0} to the list of parsed processors.", CPUName)
        Return CPUParsed
    End Function

    Overrides Function ParseAllMacOS(SystemProfilerToken As NSArray) As Dictionary(Of String, HardwareBase)
        Dim CPUParsed As New Dictionary(Of String, HardwareBase)
        Dim CPU As Processor

        'CPU information fields
        Dim CPUName As String = ""
        Dim CPUTopology As String = ""
        Dim CPUType As String = ""
        Dim CPUBits As Integer
        Dim CPUMilestone As String = ""
#If Not NET45 Then
        Dim CPUFlags As String() = Array.Empty(Of String)
#Else
        Dim CPUFlags As String() = {}
#End If
        Dim CPUL2Size As String = ""
        Dim CPUL3Size As Integer = 0
        Dim CPUSpeed As String = ""
        Dim CPURev As String = ""
        Dim CPUBogoMips As Integer = 0

        'TODO: L2, L3, and speed only done in macOS
        'Check for data type
        Debug("Checking for data type...")
        Debug("TODO: L2, L3, and speed only done in macOS.")
        For Each DataType As NSDictionary In SystemProfilerToken
            If DataType("_dataType").ToObject = "SPHardwareDataType" Then
                Debug("DataType found: SPHardwareDataType...")

                'Get information of a drive
                Dim HardwareEnum As NSArray = DataType("_items")
                For Each HardwareDict As NSDictionary In HardwareEnum
                    CPUL2Size = HardwareDict("l2_cache").ToObject
                    CPUL3Size = HardwareDict("l3_cache").ToObject.ToString.Replace(" MB", "")
                    CPUSpeed = HardwareDict("current_processor_speed").ToObject
                    Debug("Got information. CPUL2Size: {0}, CPUL3Size: {1}, CPUSpeed: {2}", CPUL2Size, CPUL3Size, CPUSpeed)
                Next
            End If
        Next

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUFlags, CPUL2Size, CPUL3Size, CPURev, CPUBogoMips, CPUSpeed)
        CPUParsed.AddIfNotFound(CPUName, CPU)
        Debug("Added {0} to the list of parsed processors.", CPUName)
        Return CPUParsed
    End Function

    Overrides Function ParseAllWindows(WMISearcher As ManagementObjectSearcher) As Dictionary(Of String, HardwareBase)
        Dim CPUParsed As New Dictionary(Of String, HardwareBase)
        Debug("Selecting entries from Win32_OperatingSystem...")
        Dim System As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
        Dim CPUClass As ManagementObjectSearcher = WMISearcher
        Dim CPU As Processor

        'CPU information fields
        Dim CPUName As String = ""
        Dim CPUTopology As String = ""
        Dim CPUType As String = ""
        Dim CPUBits As Integer
        Dim CPUMilestone As String = ""
#If Not NET45 Then
        Dim CPUFlags As String() = Array.Empty(Of String)
#Else
        Dim CPUFlags As String() = {}
#End If
        Dim CPUL2Size As String = ""
        Dim CPUL3Size As Integer = 0
        Dim CPUSpeed As String = ""
        Dim CPURev As String = ""
        Dim CPUBogoMips As Integer = 0

        'TODO: Topology, Rev, BogoMips, and Milestone not implemented in Windows
        'Get information of processors
        Debug("Getting the base objects...")
        Debug("TODO: Topology, Rev, BogoMips, and Milestone not implemented in Windows.")
        For Each CPUManagement As ManagementBaseObject In CPUClass.Get
            CPUName = CPUManagement("Name")
            CPUType = CPUManagement("ProcessorType")
            CPUBits = CPUManagement("DataWidth")
            CPUL2Size = CPUManagement("L2CacheSize")
            CPUL3Size = CPUManagement("L3CacheSize")
            CPUSpeed = CPUManagement("CurrentClockSpeed")
            For Each CPUFeature As SSEnum In [Enum].GetValues(GetType(SSEnum))
                If IsProcessorFeaturePresent(CPUFeature) Then
                    CPUFlags.Add(CPUFeature.ToString.ToLower)
                End If
            Next
            Debug("Got information. CPUName: {0}, CPUType: {1}, CPUBits: {2}, CPUL2Size: {3}, CPUFlags: {4}, CPUL3Size: {5}, CPUSpeed: {6}", CPUName, CPUType, CPUBits, CPUL2Size, CPUFlags.Length, CPUL3Size, CPUSpeed)
        Next

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUFlags, CPUL2Size, CPUL3Size, CPURev, CPUBogoMips, CPUSpeed)
        CPUParsed.AddIfNotFound(CPUName, CPU)
        Debug("Added {0} to the list of parsed processors.", CPUName)
        Return CPUParsed
    End Function

End Class

Module CPUFeatures

    ''' <summary>
    ''' [Windows] Check for specific processor feature. More info: https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-isprocessorfeaturepresent
    ''' </summary>
    ''' <param name="processorFeature">An SSE version</param>
    ''' <returns>True if supported, false if not supported</returns>
    <DllImport("kernel32.dll")>
    Friend Function IsProcessorFeaturePresent(processorFeature As SSEnum) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    ''' <summary>
    ''' [Windows] Collection of SSE versions
    ''' </summary>
    Friend Enum SSEnum As UInteger
        ''' <summary>
        ''' [Windows] The SSE instruction set is available.
        ''' </summary>
        SSE = 6
        ''' <summary>
        ''' [Windows] The SSE2 instruction set is available. (This is used in most apps nowadays, since recent processors have this capability.)
        ''' </summary>
        SSE2 = 10
        ''' <summary>
        ''' [Windows] The SSE3 instruction set is available.
        ''' </summary>
        SSE3 = 13
    End Enum

End Module