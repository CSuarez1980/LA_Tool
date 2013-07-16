Public Class frmImport
    Dim cn As New OAConnection.Connection

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboMedio, "Select * From Medio", "IDMedio", "Descripcion")
        cn.LoadCombo(Me.cboDestino, "Select * From Destino", "IDDestino", "Descripcion")

    End Sub
  
    'Private Sub cboDestino_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDestino.Click
    '    cn.LoadCombo(Me.cboOrigen, "SELECT Flete.IDOrigen, Origen.Descripcion FROM Flete, Origen Where Flete.IDOrigen = Origen.IDOrigen And Flete.IDDestino = " & Me.cboMedio.SelectedValue.ToString)
    'End Sub

    'Private Sub cboOrigen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboOrigen.SelectedIndexChanged
    '    cn.LoadCombo(Me.cboOrigen, "SELECT Flete.IDOrigen, Origen.Descripcion FROM Flete, Origen Where Flete.IDOrigen = Origen.IDOrigen And Flete.IDDestino = " & Me.cboMedio.SelectedValue.ToString)
    'End Sub

    Private Sub cboDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDestino.SelectedIndexChanged
        If Me.cboDestino.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboOrigen, "SELECT Origen_Destino.IDOrigen, Origen.Descripcion FROM Origen_Destino, Origen Where Origen_Destino.IDOrigen = Origen.IDOrigen And Origen_Destino.IDDestino = " & Me.cboDestino.SelectedValue.ToString, "IDOrigen", "Descripcion")
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        'SELECT TOP 1 IDOrigen, Origen, IDDestino, Destino, IDMedio, Medio, Peso, Costo FROM vstFlete WHERE (Peso >= 0.4) AND (IDOrigen = 1) AND (IDDestino = 1) AND (IDMedio = 1) 

    End Sub
End Class