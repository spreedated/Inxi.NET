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
