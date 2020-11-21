Imports Newtonsoft.Json.Linq

Module SoundParser

    ''' <summary>
    ''' Parses sound cards
    ''' </summary>
    ''' <param name="InxiToken">Inxi JSON token</param>
    Function ParseSound(InxiToken As JToken) As Dictionary(Of String, Sound)
        Dim SPUParsed As New Dictionary(Of String, Sound)
        Dim SPU As Sound

        'SPU information fields
        Dim SPUName As String
        Dim SPUDriver As String

        For Each InxiSPU In InxiToken.SelectToken("005#Audio")
            If InxiSPU("001#Device") IsNot Nothing Then
                'Get information of a sound card
                SPUName = InxiSPU("001#Device")
                SPUDriver = InxiSPU("002#driver")

                'Create an instance of sound class
                SPU = New Sound(SPUName, SPUDriver)
                SPUParsed.Add(SPUName, SPU)
            End If
        Next

        Return SPUParsed
    End Function

End Module
