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
        Dim RAMParsed As PCMemory

        'Parse hardware
        HDDParsed = ParseHardDrives(InxiToken)
        CPUParsed = ParseProcessors(InxiToken)
        GPUParsed = ParseGraphics(InxiToken)
        SoundParsed = ParseSound(InxiToken)
        RAMParsed = ParsePCMemory(InxiToken)

        'Install parsed information to current instance
        HDD = HDDParsed
        CPU = CPUParsed
        GPU = GPUParsed
        Sound = SoundParsed
        RAM = RAMParsed
    End Sub

End Class
