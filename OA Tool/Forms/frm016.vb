Public Class frm016

    Private Sub frm016_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection
        Dim Tabla As DataTable
        Dim PorVencer As DataTable
        Dim Vencidos As DataTable

        Tabla = cn.RunSentence("Select * From vstOATrackingByRegion").Tables(0)
        If Tabla.Rows.Count > 0 Then
            dtgTracking.DataSource = Tabla
        Else
            MsgBox("No se han encontrado registros para mostrar.", MsgBoxStyle.Exclamation)
        End If

        Vencidos = cn.RunSentence("Select * From vstVencidosPorRegion").Tables(0)
        If Vencidos.Rows.Count > 0 Then
            Me.dtgVencidos.DataSource = Vencidos
        End If

        PorVencer = cn.RunSentence("Select * From vstPorVencerPorRegion").Tables(0)
        If PorVencer.Rows.Count > 0 Then
            Me.dtgPorVencer.DataSource = PorVencer
        End If



    End Sub
End Class