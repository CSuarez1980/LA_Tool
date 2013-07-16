Public Class frmFletes
    Dim cn As New OAConnection.Connection
    Dim Tabla As DataTable

    Private Sub frmFletes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboMedio, "Select * From Medio", "IDMedio", "Descripcion")
        cn.LoadCombo(Me.cboDestino, "Select * From Destino", "IDDestino", "Descripcion")
    End Sub

    Private Sub cboDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDestino.SelectedIndexChanged
        If Me.cboDestino.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboOrigen, "SELECT Origen_Destino.IDOrigen, Origen.Descripcion FROM Origen_Destino, Origen Where Origen_Destino.IDOrigen = Origen.IDOrigen And Origen_Destino.IDDestino = " & Me.cboDestino.SelectedValue.ToString, "IDOrigen", "Descripcion")
        End If
    End Sub


    Private Sub cboOrigen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboOrigen.SelectedIndexChanged
        If Me.cboOrigen.SelectedValue.ToString <> "System.Data.DataRowView" Then
            Tabla = cn.RunSentence("Select * From vstFlete Where IDOrigen = " & Me.cboOrigen.SelectedValue.ToString & " And IDDestino = " & Me.cboDestino.SelectedValue.ToString & " And IDMedio = " & Me.cboMedio.SelectedValue.ToString).Tables(0)
            If Tabla.Rows.Count <= 0 Then
                MsgBox("No se encontraron valores para los parámetros especificados")
            End If

            Me.dtgFletes.DataSource = Tabla
        End If
    End Sub

    Private Sub cmdExportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExportar.Click
        SaveFileDialog1.Filter = "Excel Files|*.xls"
        SaveFileDialog1.Title = "Exportar Import Analisis:"
        SaveFileDialog1.FileName = "Import Analisis"
        SaveFileDialog1.ShowDialog()

        If SaveFileDialog1.FileName <> "" Then
            If Len(Dir(SaveFileDialog1.FileName)) > 0 Then
                If MsgBox("El archivo especificado ya existe." & Chr(13) & Chr(13) & "  Desea reemplazarlo?", vbQuestion + vbYesNo, "Reemplazar archivo") = vbYes Then
                    Kill(SaveFileDialog1.FileName)
                Else
                    MsgBox("Acción cancelada por el usuario.", vbExclamation, "Acción Cancelada")
                    Exit Sub
                End If
            End If
            cn.ExportDataTableToXL(Tabla, SaveFileDialog1.FileName)

        Else
            MsgBox("Debe ingresar un nombre de archivo.", MsgBoxStyle.Exclamation)
        End If
      
        MsgBox("Done!")
    End Sub

    Private Sub cmdImportar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdImportar.Click
        Dim Table As New DataSet
        Dim FileName$
        Dim OpenFileDialog As New Windows.Forms.OpenFileDialog

        FileName = ""
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
        End If

        If FileName <> "" Then
            Table = cn.GetXLTable(FileName, "sheet1$", "Select * From [Import Analisis$]")

            Me.dtgFletes.DataSource = Table.Tables(0)

            Me.cboMedio.Enabled = False
            Me.cboOrigen.Enabled = False
            Me.cboDestino.Enabled = False
        End If
    End Sub

    Private Sub cboMedio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMedio.SelectedIndexChanged

    End Sub
End Class