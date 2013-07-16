Imports System.Windows.Forms

Public Class frm064
    Private Data As New DataTable
    Private cn As New OAConnection.Connection

    Private Sub frm064_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Data = cn.RunSentence("Select TICKET,BOX,PO,CC,VENDOR,[VENDOR NAME],Day From BI_Tikets Where TICKET = 0").Tables(0)
        dtgTicket.DataSource = Data
    End Sub

    Private Sub dtgTicket_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgTicket.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgTicket, Data)
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If MsgBox("Are you sure to replace current report?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Replace Ticked Report?") = MsgBoxResult.Yes Then
            cn.ExecuteInServer("Delete from BI_Tikets")
            cn.AppendTableToSqlServer("BI_Tikets", Data)

            MsgBox("Done!")
        End If
    End Sub
End Class