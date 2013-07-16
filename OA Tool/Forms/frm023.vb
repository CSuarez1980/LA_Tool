Public Class frm023
    Public cn As New OAConnection.Connection

    Private Sub frm023_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboPerfiles, "Select ID, Descripcion From Perfil", "ID", "Descripcion")
    End Sub

    Private Sub cboPerfiles_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPerfiles.SelectedIndexChanged
        Dim Table As New DataTable
        Dim Formularios As New DataTable


        If Me.cboPerfiles.SelectedValue.ToString <> "System.Data.DataRowView" Then
            Me.dtgPerfiles.Columns.Clear()

            Table = cn.RunSentence("Select Perfil_Form.ID_Form From Perfil_Form Where Perfil_Form.ID_Perfil = " & Me.cboPerfiles.SelectedValue.ToString).Tables(0)
            Formularios = cn.RunSentence("Select [ID], [Description], NombreFormulario From Form").Tables(0)

            Me.dtgPerfiles.DataSource = Formularios
            Me.dtgPerfiles.Columns.Insert(0, cn.AddCheckBoxColumn("Link", "Link"))

            If Table.Rows.Count > 0 Then
                Dim i As Integer
                Dim j As Integer

                For i = 0 To Table.Rows.Count - 1
                    For j = 0 To Me.dtgPerfiles.Rows.Count - 1
                        If Not Me.dtgPerfiles.Rows(j).Cells("ID").Value <> Table.Rows(i).Item("ID_Form") Then
                            Me.dtgPerfiles.Rows(j).Cells("Link").Value = True
                        End If
                    Next
                Next
            End If

        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        cn.ExecuteInServer("Delete From Perfil_Form Where ID_Perfil = " & Me.cboPerfiles.SelectedValue.ToString)
        Me.dtgPerfiles.EndEdit()

        Dim i As Integer

        For i = 0 To Me.dtgPerfiles.Rows.Count - 1
            If Me.dtgPerfiles.Rows(i).Cells("Link").Value Then
                cn.ExecuteInServer("Insert into Perfil_Form values (" & Me.cboPerfiles.SelectedValue.ToString & "," & Me.dtgPerfiles.Rows(i).Cells("ID").Value & ")")
            End If
        Next
    End Sub
End Class