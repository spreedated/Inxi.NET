Public Class PCMemory

    ''' <summary>
    ''' Total memory installed
    ''' </summary>
    Public ReadOnly TotalMemory As String
    ''' <summary>
    ''' Used memory
    ''' </summary>
    Public ReadOnly UsedMemory As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(TotalMemory As String, UsedMemory As String)
        Me.TotalMemory = TotalMemory
        Me.UsedMemory = UsedMemory
    End Sub

End Class