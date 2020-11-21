Public Class Processor

    ''' <summary>
    ''' Name of processor
    ''' </summary>
    Public ReadOnly Name As String
    ''' <summary>
    ''' Core Topology
    ''' </summary>
    Public ReadOnly Topology As String
    ''' <summary>
    ''' Processor type
    ''' </summary>
    Public ReadOnly Type As String
    ''' <summary>
    ''' Processor bits
    ''' </summary>
    Public ReadOnly Bits As Integer
    ''' <summary>
    ''' L2 Cache
    ''' </summary>
    Public ReadOnly L2 As String
    ''' <summary>
    ''' Processor speed
    ''' </summary>
    Public ReadOnly Speed As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(Name As String, Topology As String, Type As String, Bits As Integer, L2 As String, Speed As String)
        Me.Name = Name
        Me.Topology = Topology
        Me.Type = Type
        Me.Bits = Bits
        Me.L2 = L2
        Me.Speed = Speed
    End Sub

End Class