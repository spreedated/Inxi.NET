Imports Newtonsoft.Json.Linq

Module PCMemoryParser

    ''' <summary>
    ''' Parses processors
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParsePCMemory(InxiToken As JToken) As PCMemory
        Dim Mem As PCMemory
        For Each InxiMem In InxiToken.SelectToken("011#Info")
            'Get information of memory
            Dim TotalMem As String = InxiMem("002#Memory")
            Dim UsedMem As String = InxiMem("003#used")

            'Create an instance of memory class
            Mem = New PCMemory(TotalMem, UsedMem)
        Next

        Return Mem
    End Function

End Module
