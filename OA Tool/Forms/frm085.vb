Public Class frm085
    Private _POType As Boolean = True 'True = goods / False = Services
    ''' <summary>
    ''' PO Type: Goods = True / Services = False
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property POType()
        Get
            Return _POType
        End Get
        Set(ByVal value)
            _POType = value
        End Set
    End Property

    Private Sub cmdGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGoods.Click
        _POType = True
        Me.Hide()
    End Sub
    Private Sub cmdServices_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdServices.Click
        _POType = False
        Me.Hide()
    End Sub
End Class