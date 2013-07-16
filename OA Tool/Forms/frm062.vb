Public Class frm062
    Private _Data As DataTable

    Public Property Data() As DataTable
        Get
            Return _Data
        End Get
        Set(ByVal value As DataTable)
            _Data = value

            Me.dtgRequisitions.DataSource = _Data
        End Set
    End Property
End Class