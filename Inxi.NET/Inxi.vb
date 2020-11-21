Imports System.IO

Public Class Inxi

    Public ReadOnly Hardware As HardwareInfo

    ''' <summary>
    ''' Intializes the new instance of Inxi class and parses hardware
    ''' </summary>
    Public Sub New()
        Me.New("/usr/bin/inxi", "/usr/bin/cpanel_json_xs")
    End Sub

    ''' <summary>
    ''' Initializes the new instance of Inxi class with specified path and parses hardware
    ''' </summary>
    ''' <param name="InxiPath">Path to Inxi executable. It's usually /usr/bin/inxi.</param>
    ''' <param name="CpanelJsonXsPath">Path to CPanelJsonXS executable. It's usually /usr/bin/cpanel_json_xs.</param>
    Public Sub New(InxiPath As String, CpanelJsonXsPath As String)
        If Environment.OSVersion.Platform = PlatformID.Unix Then
            If File.Exists(InxiPath) And File.Exists(CpanelJsonXsPath) Then
                Hardware = New HardwareInfo(InxiPath)
            Else
                Throw New InvalidOperationException("You must have Inxi and libcpanel-json-xs-perl installed. (Could not find """ + InxiPath + """ and """ + CpanelJsonXsPath + """.)")
            End If
        Else
            Throw New PlatformNotSupportedException("You must be running Linux.")
        End If
    End Sub

End Class
