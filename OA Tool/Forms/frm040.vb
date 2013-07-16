Imports System.Windows.Forms

Public Class frm040
    Dim cn As New OAConnection.Connection
    Dim Vendors As New DataTable

    Private Sub frm040_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadInfo()
    End Sub
    Private Sub dtgVendors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgVendors.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgVendors, Vendors)
        End If
    End Sub

    Private Sub cmdGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        Dim i As Integer = 0
        Dim IDVendor As String = ""
        Dim Vendor As String = ""
        Dim Mail As String = ""
        Dim SP As Boolean = False
        Dim CT As Boolean = False
        Dim Country As String = ""


        Me.pbEstado.Maximum = dtgVendors.Rows.Count
        Me.dtgVendors.EndEdit()
        For i = 0 To Me.dtgVendors.Rows.Count - 1

            IDVendor = Replace(IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("vendor").Value), "", Me.dtgVendors.Rows(i).Cells("vendor").Value), "'", "")
            Vendor = Replace(IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("Name").Value), "", Me.dtgVendors.Rows(i).Cells("Name").Value), "'", "")
            Mail = IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("vndMail1").Value), "", Me.dtgVendors.Rows(i).Cells("vndMail1").Value)
            SP = IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("vndSP").Value), False, Me.dtgVendors.Rows(i).Cells("vndSP").Value)
            CT = IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("CT").Value), False, Me.dtgVendors.Rows(i).Cells("CT").Value)
            Country = IIf(DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("Country").Value), "", Me.dtgVendors.Rows(i).Cells("Country").Value)

            Try
                If IDVendor <> Nothing Then
                    If Not DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("vendor")) Then
                        cn.ExecuteInServer("Insert Into VendorsG11 Values('" & IDVendor & "','" & Vendor & "','" & Mail & "','" & IIf(SP, 1, 0) & "','" & IIf(CT, 1, 0) & "','" & Country & "')")
                    End If
                End If
            Catch ex As Exception
                If Not DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("Vendor").Value) Then
                    cn.ExecuteInServer("Update VendorsG11 set Name = '" & Vendor & "', vndMail1 = '" & Mail & "', vndSP = '" & IIf(SP, 1, 0) & "', CT = '" & IIf(CT, 1, 0) & "', Country = '" & Country & "' Where Vendor = '" & IDVendor & "'")
                End If
            End Try
            Me.pbEstado.Value = i

        Next
        Me.pbEstado.Value = 0
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadInfo(Me.txtFiltro.Text)
    End Sub
    ''' <summary>
    ''' Carga la info de los vendors
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInfo()
        Vendors = cn.RunSentence("Select * From VendorsG11").Tables(0)
        If Vendors.Rows.Count = 0 Then
            MsgBox("No se han encontrado registros de vendors en la tabla G11", MsgBoxStyle.Information)
        End If

        Me.dtgVendors.DataSource = Vendors
    End Sub
    ''' <summary>
    ''' Carga la info de los vendors con un parámetro de busqueda
    ''' </summary>
    ''' <param name="Filtro">Palabra clave a ser buscada</param>
    ''' <remarks></remarks>
    Private Sub LoadInfo(ByVal Filtro As String)
        If Filtro.Length = 0 Then
            LoadInfo()
        Else
            Vendors = cn.RunSentence("Select distinct Vendor, Name, VndMail1, vndSP,CT,Country From VendorsG11 Where Vendor Like '%" & Filtro & "%' or Name Like'%" & Filtro & "%' Or VndMail1 like'%" & Filtro & "%'").Tables(0)
            If Vendors.Rows.Count = 0 Then
                MsgBox("No se han encontrado registros de vendors en la tabla G11", MsgBoxStyle.Information)
            End If
            Me.dtgVendors.DataSource = Vendors

        End If
    End Sub
End Class