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
        Dim CPUName As String
        Dim CPUTopology As String
        Dim CPUType As String
        Dim CPUBits As Integer
        Dim CPUL2Size As String
        Dim CPUSpeed As String

        For Each InxiCPU In InxiToken.SelectToken("003#CPU")
            If Not CPUSpeedReady Then
                'Get information of a processor
                CPUName = InxiCPU("001#model")
                CPUTopology = InxiCPU("000#Topology")
                CPUType = InxiCPU("003#type")
                CPUBits = InxiCPU("002#bits")
                CPUL2Size = InxiCPU("004#L2 cache")
                CPUSpeedReady = True
            Else
                CPUSpeed = InxiCPU("005#Speed")
            End If
        Next

        'Create an instance of processor class
        CPU = New Processor(CPUName, CPUTopology, CPUType, CPUBits, CPUL2Size, CPUSpeed)
        CPUParsed.Add(CPUName, CPU)

        Return CPUParsed
    End Function

End Module
