Imports Newtonsoft.Json.Linq

Module GraphicsParser

    ''' <summary>
    ''' Parses graphics cards
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParseGraphics(InxiToken As JToken) As Dictionary(Of String, Graphics)
        Dim GPUParsed As New Dictionary(Of String, Graphics)
        Dim GPU As Graphics

        'GPU information fields
        Dim GPUName As String
        Dim GPUDriver As String
        Dim GPUDriverVersion As String

        For Each InxiGPU In InxiToken.SelectToken("004#Graphics")
            If InxiGPU("001#Device") IsNot Nothing Then
                'Get information of a graphics card
                GPUName = InxiGPU("001#Device")
                GPUDriver = InxiGPU("002#driver")
                GPUDriverVersion = InxiGPU("003#v")

                'Create an instance of graphics class
                GPU = New Graphics(GPUName, GPUDriver, GPUDriverVersion)
                GPUParsed.Add(GPUName, GPU)
            End If
        Next

        Return GPUParsed
    End Function

End Module
