Public Enum MailFormat
    Text = 0
    HTML = 1
End Enum

Public Enum SendType
    Directly = 0
    SendTo_BDMail = 1
End Enum

Public Enum RequestMessage
    None = 0
    Sent = 1
    Read = 2
End Enum

Public Class OutookTools

#Region "Variables"
    Private _OutlookApp As Object 'Outlook objetc
    Private _Recipient As New List(Of String)
    Private _CCRecipient As New List(Of String)
    Private _Attach As New List(Of String)
    Private _MailFormat As MailFormat
    Private _BobyMail As String
    Private _Subject As String
    Private _OnBehalfOf As String
    Private _SentType As SendType = SendType.Directly 'This works with Office 2007
    Private _RequestMessage As New RequestMessage

    Private objOutlook As New Microsoft.Office.Interop.Outlook.Application
    Private myNameSpace As Microsoft.Office.Interop.Outlook.NameSpace = objOutlook.GetNamespace("MAPI")
    Private myTempFolder As Microsoft.Office.Interop.Outlook.MAPIFolder = myNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderDrafts)
    Private myDestFolder As Microsoft.Office.Interop.Outlook.MAPIFolder = myTempFolder.Folders("DBMail")
    Private objOutlookMsg As Microsoft.Office.Interop.Outlook.MailItem = objOutlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)

    Dim objOutlookRecip As Microsoft.Office.Interop.Outlook.Recipient
    Dim objOutlookAttach As Microsoft.Office.Interop.Outlook.Attachment

    Dim TheAddress As String

#End Region

#Region "Properties"
    Public Property SentType() As SendType
        Get
            Return _SentType
        End Get
        Set(ByVal value As SendType)
            _SentType = value
        End Set
    End Property

    Public Property OnBehalfOf() As String
        Get
            Return _OnBehalfOf
        End Get
        Set(ByVal value As String)
            _OnBehalfOf = value
        End Set
    End Property

    Public ReadOnly Property Recipient() As List(Of String)
        Get
            Return _Recipient
        End Get
    End Property

    Public ReadOnly Property CCRecipient() As List(Of String)
        Get
            Return _CCRecipient
        End Get

    End Property

    Public Property Attach() As List(Of String)
        Get
            Return _Attach
        End Get
        Set(ByVal value As List(Of String))
            _Attach = value
        End Set
    End Property

    Public Property MailFormat() As MailFormat
        Get
            Return _MailFormat

        End Get
        Set(ByVal value As MailFormat)
            _MailFormat = value
        End Set
    End Property

    Public Property BodyMail() As String
        Get
            Return _BobyMail
        End Get
        Set(ByVal value As String)
            _BobyMail = value
        End Set
    End Property

    Public Property Subjet() As String
        Get
            Return _Subject
        End Get
        Set(ByVal value As String)
            _Subject = value
        End Set
    End Property

    Public Property RequestMessage() As RequestMessage
        Get
            Return _RequestMessage
        End Get
        Set(ByVal value As RequestMessage)
            _RequestMessage = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Create a new Outlook mail
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CreateMessage()
        _Recipient.Clear()
        _CCRecipient.Clear()
        _Attach.Clear()
        _MailFormat = Outlook.MailFormat.Text
        _BobyMail = ""
        _Subject = ""
        _RequestMessage = RequestMessage.None
    End Sub

    ''' <summary>
    ''' Add a new recipient
    ''' </summary>
    ''' <param name="pRecipient"></param>
    ''' <remarks></remarks>
    Public Sub AddRecipient(ByVal pRecipient As String)
        _Recipient.Add(pRecipient)
    End Sub

    ''' <summary>
    ''' Add a new CC Recipient
    ''' </summary>
    ''' <param name="pCCRecipient"></param>
    ''' <remarks></remarks>
    Public Sub AddCCRecipient(ByVal pCCRecipient As String)
        _CCRecipient.Add(pCCRecipient)
    End Sub

    ''' <summary>
    ''' Send the mail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SendMail() As Boolean
        Dim MyolApp
        Dim myNameSpace
        Dim objMail
        'Dim SMI
        Dim A As String

        '************************************************
        '   Variables para el envío
        '************************************************
        Dim RecList As String = ""

        Try
            MyolApp = CreateObject("Outlook.Application")
            myNameSpace = MyolApp.GetNamespace("MAPI")
            objMail = MyolApp.CreateItem(0)

            With objMail
                .Subject = _Subject
                .HTMLBody = _BobyMail

                For Each lsRecip In _Recipient
                    RecList = RecList & ";" & lsRecip
                Next

                .To = RecList
                .SentOnBehalfOfName = _OnBehalfOf

                If Not _Attach Is Nothing Then
                    For Each A In _Attach
                        If A <> "" Then
                            .Attachments.Add(CStr(A))
                        End If
                    Next
                End If


            End With

            'SMI = CreateObject("Redemption.SafeMailItem")
            'SMI.Item = objMail
            'SMI.Send()

            objMail.Send()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Outlook Error")
        End Try
    End Function

#End Region

End Class
