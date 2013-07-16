Public Class frm079

    Private Data As New DataTable
    Private cn As New OAConnection.Connection

    Private Sub frm079_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Data = cn.RunSentence("Select * From BR_Tax_Rules").Tables(0)
        BS.DataSource = Data
        grdRules.DataSource = BS

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If MsgBox("Do you really want to delete rule " & grdRules.CurrentRow.Cells("IDRule").Value & "?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Delete rule") = MsgBoxResult.Yes Then
            cn.ExecuteInServer("Delete from BR_Tax_Rules Where IDRULE = " & grdRules.CurrentRow.Cells("IDRule").Value)
            grdRules.Rows.Remove(grdRules.CurrentRow)
        End If
    End Sub

    Private Sub cmdAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        If txtLegEnt.Text <> "" AndAlso txtSAP.Text <> "" Then
            Try
                cn.ExecuteInServer("Insert Into BR_Tax_Rules(LE, SAPBox, [Vendor code], Plant, [Mat Group], Tax, [Mat Usage], [Mat Origin], [NCM Code], [Mat Category]) Values ('" & txtLegEnt.Text & "', '" & txtSAP.Text & "', '" & txtVCode.Text & "', '" & txtPlant.Text & "', '" & txtMatGrp.Text & "', '" & txtTCode.Text & "', '" & txtMUsage.Text & "', '" & txtMOrigin.Text & "', '" & txtNCMCode.Text & "','" & txtMatCategory.Text & "')")
                MsgBox("Saved.", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox("Exception while saving rule.", MsgBoxStyle.Exclamation)
            End Try
        Else
            MsgBox("SAPBox or legal entity can't be blank.", MsgBoxStyle.Exclamation)
        End If
    End Sub
End Class