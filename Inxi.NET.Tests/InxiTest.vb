
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

Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class InxiTest

    ''' <summary>
    ''' Tests getting hardware information
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformation()
        Dim InxiInstance As New Inxi()
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.CPU)
        Assert.IsNotNull(HardwareInfo.GPU)
        Assert.IsNotNull(HardwareInfo.RAM)
        Assert.IsNotNull(HardwareInfo.HDD)
        Assert.IsNotNull(HardwareInfo.Sound)
        Assert.IsNotNull(HardwareInfo.System)
        Assert.IsNotNull(HardwareInfo.BIOS)
        Assert.IsNotNull(HardwareInfo.Machine)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (BIOS)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveBIOS()
        Dim InxiInstance As New Inxi(InxiParseFlags.BIOS)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.BIOS)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (Graphics)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveGraphics()
        Dim InxiInstance As New Inxi(InxiParseFlags.Graphics)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.GPU)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (HardDrive)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveHardDrive()
        Dim InxiInstance As New Inxi(InxiParseFlags.HardDrive)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.HDD)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (Machine)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveMachine()
        Dim InxiInstance As New Inxi(InxiParseFlags.Machine)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.Machine)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (Network)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveNetwork()
        Dim InxiInstance As New Inxi(InxiParseFlags.Network)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.Network)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (PCMemory)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectivePCMemory()
        Dim InxiInstance As New Inxi(InxiParseFlags.PCMemory)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.RAM)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (Processor)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveProcessor()
        Dim InxiInstance As New Inxi(InxiParseFlags.Processor)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.CPU)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (Sound)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveSound()
        Dim InxiInstance As New Inxi(InxiParseFlags.Sound)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.Sound)
    End Sub

    ''' <summary>
    ''' Tests getting hardware information selectively (System)
    ''' </summary>
    <TestMethod>
    Sub TestGetHardwareInformationSelectiveSystem()
        Dim InxiInstance As New Inxi(InxiParseFlags.System)
        Dim HardwareInfo As HardwareInfo = InxiInstance.Hardware
        Assert.IsNotNull(InxiInstance)
        Assert.IsNotNull(HardwareInfo)
        Assert.IsNotNull(HardwareInfo.System)
    End Sub

End Class
