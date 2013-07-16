
Public Class frm056

    Dim _This_Item As Boolean = False

    ''' <summary>
    ''' Doument Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Document() As String
        Get
            Return txtDocument.Text
        End Get
        Set(ByVal value As String)
            txtDocument.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Item Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Item() As String
        Get
            Return txtItem.Text
        End Get
        Set(ByVal value As String)
            txtItem.Text = value
        End Set
    End Property

    Public ReadOnly Property This_Item() As Boolean
        Get
            Return _This_Item
        End Get
    End Property

    Public ReadOnly Property Comment() As String
        Get
            Return txtComment.Text
        End Get
    End Property

    Private Sub frm056_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub optOne_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optOne.CheckedChanged
        lblDocument.Visible = Not lblDocument.Visible
        lblItem.Visible = Not lblItem.Visible
        txtDocument.Visible = Not txtDocument.Visible
        txtItem.Visible = Not txtItem.Visible
        _This_Item = Not This_Item
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Hide()
    End Sub
End Class