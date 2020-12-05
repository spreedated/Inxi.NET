
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

Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass>
Public Class InxiTest

    <TestInitialize>
    Sub InitializeTest()
        If Environment.OSVersion.Platform <> PlatformID.Unix Then
            Assert.Fail("Inxi expects that you're running Linux. You're running on {0}.", Environment.OSVersion.Platform)
        End If
    End Sub

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
    End Sub

End Class
