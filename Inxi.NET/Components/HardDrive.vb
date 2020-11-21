Public Class HardDrive

    ''' <summary>
    ''' The udev ID of hard drive
    ''' </summary>
    Public ReadOnly ID As String
    ''' <summary>
    ''' The size of the drive
    ''' </summary>
    Public ReadOnly Size As String
    ''' <summary>
    ''' The model of the drive
    ''' </summary>
    Public ReadOnly Model As String
    ''' <summary>
    ''' The make of the drive
    ''' </summary>
    Public ReadOnly Vendor As String
    ''' <summary>
    ''' List of partitions
    ''' </summary>
    Public ReadOnly Partitions As New Dictionary(Of String, Partition)

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(ID As String, Size As String, Model As String, Vendor As String, Partitions As Dictionary(Of String, Partition))
        Me.ID = ID
        Me.Size = Size
        Me.Model = Model
        Me.Vendor = Vendor
        Me.Partitions = Partitions
    End Sub

End Class