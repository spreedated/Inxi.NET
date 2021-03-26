
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
Imports System.Management
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq
Imports Claunia.PropertyList

Module ProcessorParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Function ParseProcessors(InxiToken As JToken, SystemProfilerToken As NSArray) As Dictionary(Of String, Processor)
        Dim CPUParsed As New Dictionary(Of String, Processor)
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

        If IsUnix() Then
            If IsMacOS() Then
                'Check for data type
                For Each DataType As NSDictionary In SystemProfilerToken
                    If DataType("_dataType").ToObject = "SPHardwareDataType" Then
                        'Get information of a drive
                        'TODO: L2, L3, and speed only done in macOS
                        Dim HardwareEnum As NSArray = DataType("_items")
                        For Each HardwareDict As NSDictionary In HardwareEnum
                            CPUL2Size = HardwareDict("l2_cache").ToObject
                            CPUL3Size = HardwareDict("l3_cache").ToObject.ToString.Replace(" MB", "")
                            CPUSpeed = HardwareDict("current_processor_speed").ToObject
                        Next
                    End If
                Next
            Else
                For Each InxiCPU In InxiToken.SelectTokenKeyEndingWith("CPU")
                    If Not CPUSpeedReady Then
                        'Get information of a processor
                        'TODO: L3 cache is not implemented in Linux
                        'TODO: L2 cache probing is currently disabled as a side effect of fixing regression introduced in Inxi versions >= 3.1.03 due to an unexpected character. We'll try to fix this.
                        CPUName = InxiCPU.SelectTokenKeyEndingWith("model")
                        CPUTopology = InxiCPU.SelectTokenKeyEndingWith("Topology")
                        If String.IsNullOrEmpty(CPUTopology) Then CPUTopology = InxiCPU.SelectTokenKeyEndingWith("Info")
                        CPUType = InxiCPU.SelectTokenKeyEndingWith("type")
                        CPUBits = InxiCPU.SelectTokenKeyEndingWith("bits")
                        CPUMilestone = InxiCPU.SelectTokenKeyEndingWith("arch")
                        'CPUL2Size = InxiCPU.SelectTokenKeyContaining("L2")
                        CPURev = InxiCPU.SelectTokenKeyEndingWith("rev")
                        CPUSpeedReady = True
                    ElseIf InxiCPU.SelectTokenKeyEndingWith("flags") IsNot Nothing Then
                        CPUFlags = CStr(InxiCPU.SelectTokenKeyEndingWith("flags")).Split(" "c)
                        CPUBogoMips = InxiCPU.SelectTokenKeyEndingWith("bogomips")
                    Else
                        CPUSpeed = InxiCPU.SelectTokenKeyEndingWith("Speed")
                    End If
                Next
            End If
        Else
            Dim CPUClass As New ManagementObjectSearcher("SELECT * FROM Win32_Processor")
            Dim System As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")

            For Each CPUManagement As ManagementBaseObject In CPUClass.Get
                'TODO: Topology, Rev, BogoMips, and Milestone not implemented in Windows
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
            Next
        End If

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUFlags, CPUL2Size, CPUL3Size, CPURev, CPUBogoMips, CPUSpeed)
        CPUParsed.AddIfNotFound(CPUName, CPU)

        Return CPUParsed
    End Function

End Module

Module CPUFeatures

    ''' <summary>
    ''' [Windows] Check for specific processor feature. More info: https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-isprocessorfeaturepresent
    ''' </summary>
    ''' <param name="processorFeature">An SSE version</param>
    ''' <returns>True if supported, false if not supported</returns>
    <DllImport("kernel32.dll")>
    Friend Function IsProcessorFeaturePresent(ByVal processorFeature As SSEnum) As <MarshalAs(UnmanagedType.Bool)> Boolean
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