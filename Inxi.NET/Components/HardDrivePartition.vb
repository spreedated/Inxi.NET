Public Class Partition

    ''' <summary>
    ''' The udev ID of drive partition
    ''' </summary>
    Public ReadOnly ID As String
    ''' <summary>
    ''' The filesystem of partition
    ''' </summary>
    Public ReadOnly FileSystem As String
    ''' <summary>
    ''' The size of partition
    ''' </summary>
    Public ReadOnly Size As String
    ''' <summary>
    ''' The used size of partition
    ''' </summary>
    Public ReadOnly Used As String

    ''' <summary>
    ''' Installs specified values parsed by Inxi to the class
    ''' </summary>
    Friend Sub New(ID As String, FileSystem As String, Size As String, Used As String)
        Me.ID = ID
        Me.FileSystem = FileSystem
        Me.Size = Size
        Me.Used = Used
    End Sub

End Class
