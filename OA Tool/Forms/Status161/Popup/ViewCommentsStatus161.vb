
Imports Shared_Functions

Public Class ViewCommentsStatus161

    Private FF As New BI_Functions
    Private SQL As New MyFunctions_Class
    Private CS As String = DB_CS
    Private Record As String = Nothing
    Friend Comment As String
    Friend Comment_AP As String

    Private Sub ViewCommentsStatus161_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Record = InvoiceRow_Global.Cells("Record").Value.ToString()

            'Cargamos los controles
            RichText.Text = LoadComments()
            RichText2.Text = LoadComments_AP()

            Me.Text = Me.Text & " [" & Record & "]"
        Catch ex As Exception
            MsgBox("Error" & ex.Message, MsgBoxStyle.Critical)
            Me.Hide()
        End Try
    End Sub

    Private Sub Btn_Save(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSAVE.Click

        If SaveComments() <> Nothing Then
            MsgBox("Error to Save Comments/Record:" & Record, MsgBoxStyle.Critical)
        Else
            Me.Comment = RichText.Text
            Me.Comment_AP = RichText2.Text
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Function LoadComments() As String
        Try
            Dim Filter As String = "Record=" & Record
            LoadComments = FF.ReturnDescription(CS, "Status161_Data", "Comment", Filter)
        Catch ex As Exception
            LoadComments = "Error to Load Comments"
        End Try
    End Function

    Private Function LoadComments_AP() As String
        Try
            Dim Filter As String = "Record=" & Record
            LoadComments_AP = FF.ReturnDescription(CS, "Status161_Data", "Comment_AP", Filter)
        Catch ex As Exception
            LoadComments_AP = "Error to Load Comments AP"
        End Try
    End Function

    Private Function SaveComments() As String
        SaveComments = SQL.SQL_Execute_NQ(CS, "Update Status161_Data Set Comment='" & RichText.Text & "',Comment_AP ='" & RichText2.Text & "' Where Record=" & Record)
    End Function


End Class