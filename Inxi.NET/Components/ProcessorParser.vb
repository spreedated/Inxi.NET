Imports Newtonsoft.Json.Linq

Module ProcessorParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParseProcessors(InxiToken As JToken) As Dictionary(Of String, Processor)
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
        Dim CPUSpeed As String = ""

        For Each InxiCPU In InxiToken.SelectToken("003#CPU")
            If Not CPUSpeedReady Then
                'Get information of a processor
                CPUName = InxiCPU("001#model")
                CPUTopology = InxiCPU("000#Topology")
                CPUType = InxiCPU("003#type")
                CPUBits = InxiCPU("002#bits")
                CPUMilestone = InxiCPU("004#arch")
                CPUL2Size = InxiCPU("006#L2 cache")
                CPUSpeedReady = True
            ElseIf InxiCPU("007#flags") IsNot Nothing Then
                CPUFlags = CStr(InxiCPU("007#flags")).Split(" "c)
            Else
                CPUSpeed = InxiCPU("009#Speed")
            End If
        Next

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUMilestone, CPUFlags, CPUL2Size, CPUSpeed)
        CPUParsed.Add(CPUName, CPU)

        Return CPUParsed
    End Function

End Module
