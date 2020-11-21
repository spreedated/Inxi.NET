Public Class Graphics

    ''' <summary>
    ''' Name of graphics card
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' Driver software used for graphics card
    ''' </summary>
    Public ReadOnly Driver As String
    ''' <summary>
    ''' Driver version
    ''' </summary>
    Public ReadOnly DriverVersion As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Driver As String, DriverVersion As String)
        Me.Name = Name
        Me.Driver = Driver
        Me.DriverVersion = DriverVersion
    End Sub

End Class