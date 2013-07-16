Public Class frm038
    Public cn As New OAConnection.Connection

    Private Sub frm038_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Tabla As New DataTable
        Tabla = cn.RunSentence("Select * From [Users] Where TNumber = '" & gsUsuarioPC & "'").Tables(0)

        If Tabla.Rows.Count > 0 Then
            Me.txtPDF.Text = Tabla.Rows(0).Item("PDFPath")
            Me.chkSSO.Checked = Tabla.Rows(0).Item("SAPConfig")
            Me.txtDays.Text = Tabla.Rows(0).Item("QDays")
            Me.txtTimeOut.Text = Tabla.Rows(0).Item("PDFTimeOut")
            chkFreePDF4.Checked = Tabla.Rows(0).Item("FreePDF4")
        Else
            MsgBox("No se encontró información de configuración.", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        cn.ExecuteInServer("Update [Users] Set PDFPath = '" & Me.txtPDF.Text.ToLower & "', SAPConfig = " & IIf(Me.chkSSO.Checked, "1", "0") & ", QDays = " & Me.txtDays.Text & ", PDFTimeOut = " & Me.txtTimeOut.Text & ", FreePDF4 = " & IIf(Me.chkFreePDF4.Checked, "1", "0") & " Where TNumber = '" & gsUsuarioPC & "'")

        SAPConfig = chkSSO.Checked
        PDFPath = Me.txtPDF.Text.Trim
        QDays = Me.txtDays.Text
        PDFTimeOut = Me.txtTimeOut.Text
        FreePDF4 = chkFreePDF4.Checked
    End Sub

   
End Class