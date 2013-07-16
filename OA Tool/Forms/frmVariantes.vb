Imports System.Windows.Forms

Public Class frmVariantes
    Dim Plantas As New DataTable
    Dim Vendors As New DataTable
    Dim PGrp As New DataTable
    Dim POrg As New DataTable
    Dim MatGrp As New DataTable
    Dim Variantes As New DataTable
    Dim Country As New DataTable
    Dim Saved As Boolean
    Dim cn As New OAConnection.Connection

    Private Sub frmVariantesTrigger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cboVariantes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVariantes.SelectedIndexChanged
        GetVariants()
    End Sub

    Public Sub SaveVariant()
        Dim i%
        Dim Aux As String = ""
        Dim row As System.Windows.Forms.DataGridViewRow
        Dim POFilter As Integer = 0

        Me.dtgPGrp.EndEdit()
        Me.dtgPlantas.EndEdit()
        Me.dtgPOrg.EndEdit()
        Me.dtgVendors.EndEdit()
        Me.dtgMatGrp.EndEdit()
        Me.dtgCountry.EndEdit()

        Try
            '*****************************************
            ' Selección de filtro para Import/National
            ' 0 = Ambos
            ' 1 = Nacional
            ' 2 = Importados
            '*****************************************
            If Me.optBoth.Checked Then
                POFilter = 0
            End If

            If Me.optNational.Checked Then
                POFilter = 1
            End If

            If Me.optImport.Checked Then
                POFilter = 2
            End If

            cn.ExecuteInServer("Update HeaderVariante set POFilter = " & POFilter & " Where IDVariante = " & Me.cboVariantes.SelectedValue.ToString)

            cn.RunSentence("Delete from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "'")
            For i = 0 To Me.dtgPlantas.Rows.Count - 1
                If Not DBNull.Value.Equals(Me.dtgPlantas.Rows(i).Cells("Valor").Value) Then '------------> Verifico que no sea nulo
                    If Me.dtgPlantas.Rows(i).Cells("Valor").Value <> "" Then '---------------------------> Verifico que tenga un valor
                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','Planta','" & Me.dtgPlantas.Rows(i).Cells("Valor").Value.Trim & "','" & Me.dtgPlantas.Rows(i).Cells("Pref").Value & "')")
                    End If
                End If
            Next


            For i = 0 To Me.dtgVendors.Rows.Count - 1
                If Not DBNull.Value.Equals(Me.dtgVendors.Rows(i).Cells("Valor").Value) Then
                    If Me.dtgVendors.Rows(i).Cells("Valor").Value <> "" Then
                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','Vendor','" & Me.dtgVendors.Rows(i).Cells("Valor").Value.Trim & "','" & Me.dtgVendors.Rows(i).Cells("Pref").Value & "')")
                    End If
                End If
            Next


            For i = 0 To Me.dtgPGrp.Rows.Count - 1
                If Not DBNull.Value.Equals(Me.dtgPGrp.Rows(i).Cells("Valor").Value) Then
                    If Me.dtgPGrp.Rows(i).Cells("Valor").Value <> "" Then
                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','PurchGrp','" & Me.dtgPGrp.Rows(i).Cells("Valor").Value.ToUpper.Trim & "','" & Me.dtgPGrp.Rows(i).Cells("Pref").Value & "')")
                    End If
                End If
            Next


            For i = 0 To Me.dtgPOrg.Rows.Count - 1
                If Not DBNull.Value.Equals(Me.dtgPOrg.Rows(i).Cells("Valor").Value) Then
                    If Me.dtgPOrg.Rows(i).Cells("Valor").Value <> "" Then

                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','PurchOrg','" & Me.dtgPOrg.Rows(i).Cells("Valor").Value.ToUpper.Trim & "','" & Me.dtgPOrg.Rows(i).Cells("Pref").Value & "')")
                    End If
                End If
            Next

            For i = 0 To Me.dtgMatGrp.Rows.Count - 1
                If Not DBNull.Value.Equals(Me.dtgMatGrp.Rows(i).Cells("Valor").Value) Then
                    If Me.dtgMatGrp.Rows(i).Cells("Valor").Value <> "" Then

                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','MatGrp','" & Me.dtgMatGrp.Rows(i).Cells("Valor").Value.ToUpper.Trim & "','" & Me.dtgMatGrp.Rows(i).Cells("Pref").Value & "')")
                    End If
                End If
            Next

            For Each row In Me.dtgCountry.Rows
                If Not DBNull.Value.Equals(row.Cells("Valor").Value) Then
                    If row.Cells("Valor").Value <> "" Then
                        cn.RunSentence("Insert Into Variantes(IDVariante,Usuario,Campo,Valor,Prefijo) Values('" & Me.cboVariantes.SelectedValue.ToString & "','" & gsUsuarioPC & "','COUNTRY','" & row.Cells("Valor").Value.ToUpper.Trim & "','" & row.Cells("Pref").Value & "')")
                    End If
                End If
            Next

            MsgBox("Variante guardada.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox("Error el guardar las variantes.", MsgBoxStyle.Critical)
        End Try

    End Sub

    Private Sub GetVariants()
        Saved = False
        Dim I As Integer = 0
        Dim POFilter As Integer
        Me.lblBIDefault.Visible = False

        If Me.cboVariantes.SelectedValue.ToString <> "System.Data.DataRowView" Then
            Dim dtPOFilter As DataTable
            dtPOFilter = cn.RunSentence("Select POFilter,BIDefault From HeaderVariante Where IDVariante = " & Me.cboVariantes.SelectedValue.ToString).Tables(0)

            If Not DBNull.Value.Equals(dtPOFilter.Rows(0).Item("POFilter")) Then
                POFilter = dtPOFilter.Rows(0).Item("POFilter")
            Else
                POFilter = 0
            End If

            Select Case POFilter
                Case 0
                    Me.optBoth.Checked = True

                Case 1
                    Me.optNational.Checked = True

                Case 2
                    Me.optImport.Checked = True
            End Select

            If dtPOFilter.Rows(0).Item("BIDefault") Then
                Me.lblBIDefault.Visible = True
            End If

            Me.dtgPGrp.DataSource = ""
            Me.dtgPGrp.Columns.Clear()

            Me.dtgPlantas.DataSource = ""
            Me.dtgPlantas.Columns.Clear()

            Me.dtgPOrg.DataSource = ""
            Me.dtgPOrg.Columns.Clear()

            Me.dtgVendors.DataSource = ""
            Me.dtgVendors.Columns.Clear()

            Me.dtgMatGrp.DataSource = ""
            Me.dtgMatGrp.Columns.Clear()

            Me.dtgCountry.DataSource = ""
            Me.dtgCountry.Columns.Clear()

            '*********************************
            '*********************************
            '   Variante para las plantas
            '*********************************
            '*********************************
            Plantas = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'PLANTA'").Tables(0)
            Me.dtgPlantas.DataSource = Plantas
            Me.dtgPlantas.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgPlantas.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgPlantas.Columns(0).Width = 30

            For I = 0 To Me.dtgPlantas.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicional
                Me.dtgPlantas.Rows(I).Cells("Pref").Value = Plantas.Rows(I).Item("Prefijo")
            Next

            '*********************************
            '*********************************
            '   Variante para los vendors
            '*********************************
            '*********************************
            Vendors = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'VENDOR'").Tables(0)
            Me.dtgVendors.DataSource = Vendors
            Me.dtgVendors.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgVendors.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgVendors.Columns(0).Width = 30

            For I = 0 To Me.dtgVendors.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicionar
                Me.dtgVendors.Rows(I).Cells("Pref").Value = Vendors.Rows(I).Item("Prefijo")
            Next


            '*********************************
            '*********************************
            '   Variante para los P. Grps
            '*********************************
            '*********************************
            PGrp = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'PURCHGRP'").Tables(0)
            Me.dtgPGrp.DataSource = PGrp
            Me.dtgPGrp.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgPGrp.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgPGrp.Columns(0).Width = 30

            For I = 0 To Me.dtgPGrp.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicionar
                Me.dtgPGrp.Rows(I).Cells("Pref").Value = PGrp.Rows(I).Item("Prefijo")
            Next

            '*********************************
            '*********************************
            '   Variante para los P.Orgs
            '*********************************
            '*********************************
            POrg = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'PURCHORG'").Tables(0)
            Me.dtgPOrg.DataSource = POrg
            Me.dtgPOrg.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgPOrg.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgPOrg.Columns(0).Width = 30

            For I = 0 To Me.dtgPOrg.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicionar
                Me.dtgPOrg.Rows(I).Cells("Pref").Value = POrg.Rows(I).Item("Prefijo")
            Next


            '*********************************
            '*********************************
            '   Variante para los Mat. Grps
            '*********************************
            '*********************************
            MatGrp = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'MATGRP'").Tables(0)
            Me.dtgMatGrp.DataSource = MatGrp
            Me.dtgMatGrp.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgMatGrp.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgMatGrp.Columns(0).Width = 30

            For I = 0 To Me.dtgMatGrp.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicionar
                Me.dtgMatGrp.Rows(I).Cells("Pref").Value = MatGrp.Rows(I).Item("Prefijo")
            Next

            '*********************************
            '*********************************
            '   Variante para los Vendor / Country
            '*********************************
            '*********************************
            Country = cn.RunSentence("Select Prefijo,Valor from Variantes Where Usuario = '" & gsUsuarioPC & "' and IDVariante = '" & Me.cboVariantes.SelectedValue.ToString & "' And Campo = 'COUNTRY'").Tables(0)
            Me.dtgCountry.DataSource = Country
            Me.dtgCountry.Columns("Prefijo").Visible = False 'Escondo esta columna para utilizar el boton del grid
            Me.dtgCountry.Columns.Insert(0, cn.AddButtonColumn("Pref", "Pref", 30))
            Me.dtgCountry.Columns(0).Width = 30

            For I = 0 To Me.dtgCountry.Rows.Count - 2 ' count -2; ya que en el grid aparece una fila adicionar
                Me.dtgCountry.Rows(I).Cells("Pref").Value = Country.Rows(I).Item("Prefijo")
            Next

        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGuardar.Click
        SaveVariant()
    End Sub

    Private Sub cmdVariantes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdVariantes.Click
        Dim Form As New frm025
        Form.ShowDialog()
        cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuarioPC & "'", "IDVariante", "Nombre")
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuarioPC & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub ChangePrefix(ByRef DtGrid As Windows.Forms.DataGridView, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Dim curRow As Integer = 0
        Dim curCol As Integer = 0

        curRow = DtGrid.CurrentCell.RowIndex
        curCol = DtGrid.CurrentCell.ColumnIndex

        Select Case DtGrid.Columns(e.ColumnIndex).HeaderText.ToUpper
            Case "PREF"
                If DtGrid.Rows(curRow).Cells("Pref").Value = "<>" Then
                    DtGrid.Rows(curRow).Cells("Pref").Value = ""
                Else
                    DtGrid.Rows(curRow).Cells("Pref").Value = "<>"
                End If
        End Select
    End Sub

#Region "Selección de la celda"
    Private Sub dtgMatGrp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgMatGrp.CellContentClick
        Me.ChangePrefix(Me.dtgMatGrp, e)
    End Sub

    Private Sub dtgPlantas_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgPlantas.CellContentClick
        Me.ChangePrefix(Me.dtgPlantas, e)
    End Sub

    Private Sub dtgVendors_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgVendors.CellContentClick
        Me.ChangePrefix(Me.dtgVendors, e)

    End Sub

    Private Sub dtgPOrg_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgPOrg.CellContentClick
        Me.ChangePrefix(Me.dtgPOrg, e)
    End Sub

    Private Sub dtgPGrp_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgPGrp.CellContentClick
        Me.ChangePrefix(Me.dtgPGrp, e)
    End Sub

    Private Sub dtgMateriales_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgCountry.CellContentClick
        Me.ChangePrefix(Me.dtgCountry, e)
    End Sub
#End Region

#Region "DataGrid KeyDown"
    Private Sub dtgPlantas_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgPlantas.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgPlantas, Plantas)
        End If
    End Sub

    Private Sub dtgVendors_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgVendors.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgVendors, Vendors)
        End If
    End Sub

    Private Sub dtgPOrg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgPOrg.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgPOrg, POrg)
        End If
    End Sub

    Private Sub dtgPGrp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgPGrp.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgPGrp, PGrp)
        End If
    End Sub

    Private Sub dtgMatGrp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgMatGrp.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgMatGrp, MatGrp)
        End If
    End Sub

    Private Sub dtgCountry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dtgCountry.KeyDown
        If (e.KeyCode = Keys.V) Then
            PasteClipboardToDataGridView(Me.dtgCountry, Country)
        End If
    End Sub
#End Region

#Region "Selección del tipo de Filtro: Nacional/Import"
    Private Sub optNational_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNational.CheckedChanged
        If optNational.Checked Then
            lblText.Text = "Only materials with national vendor, this option shows purchases order where the Vendor country and PO plant country are the same."
        End If
    End Sub

    Private Sub optImport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optImport.CheckedChanged
        If optImport.Checked Then
            lblText.Text = "Only materials with import vendor, this option shows purchases order where both Vendor country and PO plant country are differents."
        End If
    End Sub

    Private Sub optBoth_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optBoth.CheckedChanged
        If optBoth.Checked Then
            lblText.Text = "Show all the report."
        End If
    End Sub
#End Region

    Private Sub cmdBIDefautl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBIDefautl.Click
        If Me.cboVariantes.Text <> "" Then
            cn.ExecuteInServer("Update HeaderVariante set BIDefault = 0 Where SAPBox = '" & cboSAPBox.SelectedValue.ToString & "' And TNumber = '" & gsUsuarioPC & "'")
            cn.ExecuteInServer("Update HeaderVariante set BIDefault = 1 Where IDVariante = " & cboVariantes.SelectedValue.ToString)
            Me.lblBIDefault.Visible = True
        End If
    End Sub
End Class