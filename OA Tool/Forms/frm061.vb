Public Class frm061
    Private _Requisition As String
    Private _Item As String


    Public Property Requisition() As String
        Get
            Return _Requisition
        End Get
        Set(ByVal value As String)
            _Requisition = value
        End Set
    End Property

    Public Property Item() As String
        Get
            Return _Item
        End Get
        Set(ByVal value As String)
            _Item = value
        End Set
    End Property


    Private Sub frm061_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If _Requisition.Length > 0 AndAlso _Item.Length > 0 Then
            Dim Table As New DataTable
            Dim cn As New OAConnection.Connection

            Table = cn.RunSentence("Select * From vstHistoricoCotizacion Where Requisition = " & _Requisition & " And Item = " & _Item).Tables(0)

            If Table.Rows.Count > 0 Then
                Me.dtgHistory.DataSource = Table
            Else
                MsgBox("No data could be found.", MsgBoxStyle.Information)
            End If

        End If
    End Sub
End Class