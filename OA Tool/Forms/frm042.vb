Public Class frm042
    Dim cn As New OAConnection.Connection
    Dim Tabla As New DataTable
    Private Sub frm042_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboUsers, "Select TNumber, TNumber + ' - ' + Nombre as Nombre From [Users] Where TNumber <> 'BM4691' Order by Nombre", "TNumber", "Nombre")

    End Sub

    Private Sub cboUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUsers.SelectedIndexChanged
        Dim Table As New DataTable
        Dim Perfiles As New DataTable


        If Me.cboUsers.SelectedValue.ToString <> "System.Data.DataRowView" Then
            Me.dtgPerfiles.Columns.Clear()

            Table = cn.RunSentence("Select User_Perfil.ID_Perfil From User_Perfil Where User_Perfil.ID_User = '" & Me.cboUsers.SelectedValue.ToString & "'").Tables(0)
            Perfiles = cn.RunSentence("Select ID,Descripcion From Perfil").Tables(0)

            Me.dtgPerfiles.DataSource = Perfiles
            Me.dtgPerfiles.Columns.Insert(0, cn.AddCheckBoxColumn("Link", "Link"))

            If Table.Rows.Count > 0 Then
                Dim i As Integer
                Dim j As Integer

                For i = 0 To Table.Rows.Count - 1
                    For j = 0 To Me.dtgPerfiles.Rows.Count - 1
                        If Not Me.dtgPerfiles.Rows(j).Cells("ID").Value <> Table.Rows(i).Item("ID_Perfil") Then
                            Me.dtgPerfiles.Rows(j).Cells("Link").Value = True
                        End If
                    Next
                Next
            End If

        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        cn.ExecuteInServer("Delete From User_Perfil Where ID_User = '" & Me.cboUsers.SelectedValue.ToString & "'")
        Me.dtgPerfiles.EndEdit()

        Dim i As Integer

        For i = 0 To Me.dtgPerfiles.Rows.Count - 1
            If Me.dtgPerfiles.Rows(i).Cells("Link").Value Then
                cn.ExecuteInServer("Insert into User_Perfil values ('" & Me.cboUsers.SelectedValue.ToString & "'," & Me.dtgPerfiles.Rows(i).Cells("ID").Value & ")")
            End If
        Next
    End Sub
End Class