
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
Imports Extensification.External.Newtonsoft.Json.JPropertyExts
Imports Claunia.PropertyList
Imports Newtonsoft.Json.Linq

Class MachineParser
    Inherits HardwareParserBase
    Implements IHardwareParser

    ''' <summary>
    ''' Parses machine info
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token. Ignored in Windows.</param>
    Overrides Function Parse(InxiToken As JToken, SystemProfilerToken As NSArray) As HardwareBase
        Dim MachInfo As HardwareBase

        If IsUnix() Then
            If IsMacOS() Then
                MachInfo = ParseMacOS(SystemProfilerToken)
            Else
                MachInfo = ParseLinux(InxiToken)
            End If
        Else
            Debug("Selecting entries from Win32_ComputerSystem...")
            Dim WMIMachine As New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")
            MachInfo = ParseWindows(WMIMachine)
        End If

        Return MachInfo
    End Function

    Overrides Function ParseLinux(InxiToken As JToken) As HardwareBase
        Dim MachInfo As HardwareBase

        Debug("Selecting the Machine token...")
        For Each InxiSys In InxiToken.SelectTokenKeyEndingWith("Machine")
            'Get information of system
            Dim Type As String = InxiSys.SelectTokenKeyEndingWith("Type")
            Dim Product As String = InxiSys.SelectTokenKeyEndingWith("product")
            Dim System As String = InxiSys.SelectTokenKeyEndingWith("System")
            Dim Chassis As String = InxiSys.SelectTokenKeyEndingWith("Chassis")
            Dim MoboManufacturer As String = InxiSys.SelectTokenKeyEndingWith("Mobo")
            Dim MoboModel As String = InxiSys.SelectTokenKeyEndingWith("model")
            Debug("Got information. Type: {0}, Product: {1}, System: {2}, Chassis: {3}, MoboManufacturer: {4}, MoboModel: {5}", Type, Product, System, Chassis, MoboManufacturer, MoboModel)

            'Create an instance of system class
            MachInfo = New MachineInfo(Type, Product, System, Chassis, MoboManufacturer, MoboModel)
        Next

#Disable Warning BC42104
        Return MachInfo
#Enable Warning BC42104
    End Function

    Overrides Function ParseMacOS(SystemProfilerToken As NSArray) As HardwareBase
        Dim MachInfo As HardwareBase

        'Check for data type
        Debug("Checking for data type...")
        For Each DataType As NSDictionary In SystemProfilerToken
            If DataType("_dataType").ToObject = "SPHardwareDataType" Then
                Debug("DataType found: SPHardwareDataType...")

                'Get information of a machine
                Dim SoftwareEnum As NSArray = DataType("_items")
                Debug("Enumerating machines...")
                For Each SoftwareDict As NSDictionary In SoftwareEnum
                    'Get information of machine
                    Dim Type As String = If(SoftwareDict("machine_name").ToObject.ToString.Contains("MacBook"), "Laptop", "Desktop")
                    Dim Product As String = SoftwareDict("machine_name").ToObject
                    Dim System As String = "macOS"
                    Dim Chassis As String = "Apple"
                    Dim MoboManufacturer As String = "Apple"
                    Dim MoboModel As String = SoftwareDict("machine_model").ToObject
                    Debug("Got information. Type: {0}, Product: {1}, System: {2}, Chassis: {3}, MoboManufacturer: {4}, MoboModel: {5}", Type, Product, System, Chassis, MoboManufacturer, MoboModel)

                    'Create an instance of machine class
                    MachInfo = New MachineInfo(Type, Product, System, Chassis, MoboManufacturer, MoboModel)
                Next
            End If
        Next

#Disable Warning BC42104
        Return MachInfo
#Enable Warning BC42104
    End Function

    Overrides Function ParseWindows(WMISearcher As ManagementObjectSearcher) As HardwareBase
        Dim MachInfo As HardwareBase
        Dim WMIMachine As ManagementObjectSearcher = WMISearcher

        Debug("Selecting entries from Win32_BaseBoard...")
        Dim WMIBoard As New ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")
        Debug("Selecting entries from Win32_OperatingSystem...")
        Dim WMISystem As New ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")

        'Get information of system and motherboard
        Dim [Type] As String = ""
        Dim Product As String = ""
        Dim System As String = ""
        Dim Chassis As String = ""
        Dim MoboModel As String = ""
        Dim MoboManufacturer As String = ""

        'Get information for ChassisSKUNumber
        Debug("Getting the base objects...")
        For Each WMISystemBase As ManagementBaseObject In WMISystem.Get
            If WMISystemBase("Version").StartsWith("10") And Environment.OSVersion.Platform = PlatformID.Win32NT Then 'If running on Windows 10
                Debug("Target is running Windows 10/11.")
                For Each MachineBase As ManagementBaseObject In WMIMachine.Get
                    [Type] = MachineBase("ChassisSKUNumber")
                Next
            End If
        Next

        'TODO: Chassis not implemented in Windows
        'Get informaiton for machine model and family
        Debug("Getting the base objects...")
        Debug("TODO: Chassis not implemented in Windows")
        For Each MachineBase As ManagementBaseObject In WMIMachine.Get
            Product = MachineBase("Model")
            System = MachineBase("SystemFamily")
        Next

        'Get information for model and manufacturer
        Debug("Getting the base objects...")
        For Each MoboBase As ManagementBaseObject In WMIBoard.Get
            MoboModel = MoboBase("Model")
            MoboManufacturer = MoboBase("Manufacturer")
        Next
        Debug("Got information. Type: {0}, Product: {1}, System: {2}, Chassis: {3}, MoboManufacturer: {4}, MoboModel: {5}", Type, Product, System, Chassis, MoboManufacturer, MoboModel)

        'Create an instance of system class
        MachInfo = New MachineInfo(Type, Product, System, Chassis, MoboManufacturer, MoboModel)
        Return MachInfo
    End Function

End Class
