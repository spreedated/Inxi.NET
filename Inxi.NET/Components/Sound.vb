Public Class Sound

    ''' <summary>
    ''' Name of sound card
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' The maker of sound card
    ''' </summary>
    Public ReadOnly Vendor As String
    ''' <summary>
    ''' Driver software used for sound card
    ''' </summary>
    Public ReadOnly Driver As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Vendor As String, Driver As String)
        Me.Name = Name
        Me.Vendor = Vendor
        Me.Driver = Driver
    End Sub

End Class