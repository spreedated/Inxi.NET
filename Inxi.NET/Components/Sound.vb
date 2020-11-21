Public Class Sound

    ''' <summary>
    ''' Name of sound card
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' Driver software used for sound card
    ''' </summary>
    Public ReadOnly Driver As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Driver As String)
        Me.Name = Name
        Me.Driver = Driver
    End Sub

End Class