Imports System.Windows.Forms
Imports System.Linq

Public Class frm047
    Dim cn As New OAConnection.Connection
    Public Status As String = "Tax Changes process"
    Public SAP As String
    Public Catalog As New DataTable
    Public Total As Integer 'Total of records to be updated

    Private Sub frm047_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub btnSapProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSapProcess.Click
        Dim Ekko As New SAPCOM.EKKO_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        Dim Ekpo As New SAPCOM.EKPO_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        Dim dtEkko As New DataTable
        Dim dtEkpo As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim Plantas As New DataTable

        Dim i As Integer

        Me.tlbHerramientas.Enabled = False

        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)

        If PGrp.Rows.Count > 0 Then
            For i = 0 To PGrp.Rows.Count - 1
                If DBNull.Value.Equals(PGrp.Rows(i).Item("Prefijo")) Then
                    Ekko.IncludePurchGroup("")
                    Ekko.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                Else
                    If PGrp.Rows(i).Item("Prefijo") = "" Then
                        Ekko.IncludePurchGroup("")
                        Ekko.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    Else
                        Ekko.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If POrg.Rows.Count > 0 Then
            For i = 0 To POrg.Rows.Count - 1
                If DBNull.Value.Equals(POrg.Rows(i).Item("Prefijo")) Then
                    Ekko.IncludePurchOrg("")
                    Ekko.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                Else
                    If POrg.Rows(i).Item("Prefijo") = "" Then
                        Ekko.IncludePurchOrg("")
                        Ekko.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                    Else
                        Ekko.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If Vendors.Rows.Count > 0 Then
            For i = 0 To Vendors.Rows.Count - 1
                If DBNull.Value.Equals(Vendors.Rows(i).Item("Prefijo")) Then
                    Ekko.IncludeVendor("")
                    Ekko.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                Else
                    If Vendors.Rows(i).Item("Prefijo") = "" Then
                        Ekko.IncludeVendor("")
                        Ekko.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    Else
                        Ekko.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    End If
                End If
            Next
        End If

        Ekko.IncludeDocType("EC")
        Ekko.DocDateFrom = Me.dtpStart.Text
        Ekko.DocDateTo = Me.dtpEnd.Text
        Ekpo.DeletionIndicator = False
        Ekko.Execute()

        If Ekko.Success Then
            If Ekko.ErrMessage = Nothing Then
                dtEkko = Ekko.Data
            End If
        Else
            MsgBox(Ekko.ErrMessage, MsgBoxStyle.Exclamation)
        End If

        Dim row As DataRow

        For Each row In dtEkko.Rows
            Ekpo.IncludeDocument(row.Item("Doc Number"))
        Next

        For Each row In Plantas.Rows
            If row.Item("Prefijo").ToString.Length <> 0 Then
                Ekpo.ExcludePlant(row.Item("Valor"))
            Else
                Ekpo.IncludePlant(row.Item("Valor"))
            End If
        Next

        Ekpo.DeletionIndicator = False

        Ekpo.Execute()
        If Ekpo.Success Then
            If Ekpo.ErrMessage = Nothing Then
                dtEkpo = Ekpo.Data
            End If
        Else
            MsgBox(Ekpo.ErrMessage, MsgBoxStyle.Exclamation)
        End If

        Dim UEKPO As New DataColumn
        Dim UEKKO As New DataColumn

        UEKKO.ColumnName = "TNumber"
        UEKKO.Caption = "TNumber"
        UEKKO.DefaultValue = gsUsuarioPC

        UEKPO.ColumnName = "TNumber"
        UEKPO.Caption = "TNumber"
        UEKPO.DefaultValue = gsUsuarioPC

        dtEkko.Columns.Add(UEKKO)
        dtEkpo.Columns.Add(UEKPO)

        'Comentado para evitar el ciclo, implementado el columns.add()
        'dtEkko.Columns.Add("TNumber")
        'dtEkpo.Columns.Add("TNumber")

        'For i = 0 To dtEkko.Rows.Count - 1
        '    dtEkko.Rows(i).Item("TNumber") = gsUsuarioPC
        'Next

        'For i = 0 To dtEkpo.Rows.Count - 1
        '    dtEkpo.Rows(i).Item("TNumber") = gsUsuarioPC
        'Next

        cn.ExecuteInServer("Delete From _EKKO Where TNumber = '" & gsUsuarioPC & "'")
        cn.ExecuteInServer("Delete From _EKPO Where TNumber = '" & gsUsuarioPC & "'")
        cn.AppendTableToSqlServer("_EKKO", dtEkko)
        cn.AppendTableToSqlServer("_EKPO", dtEkpo)

        'Catalog = cn.RunSentence("Select * from vstCatalogos Where TNumber = '" & gsUsuarioPC & "'").Tables(0)

        Catalog = cn.RunSentence("Select * From vstCatalogos Where TNumber = '" & gsUsuarioPC & "'" & IIf(cmdFilter.Checked, " And [Mat Group] = 'S23153000'", "")).Tables(0)

        ''******************************************************************************************************
        ''  Update the CostCenter
        ''******************************************************************************************************
        'Dim Row As DataRow
        'Dim SAPcn As New SAPConection.c_SAP(Me.cboSAPBox.Text)
        'SAPcn.OpenConnection(SAPConfig)

        'For Each Row In Catalog.Rows
        '    ' Dim CostCenter$
        '    Row.Item("Cost Center") = SAPcn.GetCostCenter(Row.Item("Doc number"), Row.Item("Item Number"))
        '    'Access.DoCmd.RunSQL("Update DetalleCatalogo set CostCenter = '" & CostCenter & "' Where PO = " & Tabla.Rows(i).Item("PurchDoc") & " and Item = " & Tabla.Rows(i).Item("Item"))
        '    'cn.ExecuteInAccess("Update DetalleCatalogo set CostCenter = '" & CostCenter & "' Where PO = " & Tabla.Rows(i).Item("PurchDoc") & " and Item = " & Tabla.Rows(i).Item("Item"))
        'Next
        'SAPcn.CloseConnection()
        ''******************************************************************************************************
        Me.dtgCatalogos.DataSource = Catalog
        Check_Almacenaje()
        MsgBox("Done!")
        Me.tlbHerramientas.Enabled = True
    End Sub

    'Private Sub frm047_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    '    Me.dtgCatalogos.Width = Me.Width - 25
    '    Me.dtgCatalogos.Height = Me.Height - 190
    'End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        SAP = cboSAPBox.SelectedValue

        If Me.cboSAPBox.Text = "" Then
            MsgBox("Please select a SAP Box to continue...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Me.dtgCatalogos.EndEdit()
        Catalog.AcceptChanges()

        Total = (From C In Catalog.AsEnumerable() _
                 Where C.Field(Of Boolean)("Update") = "True" _
                 Select C.Item("Update")).Count()

        lblRuning.DisplayStyle = ToolStripItemDisplayStyle.Image
        BG.RunWorkerAsync()
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim drRow As System.Windows.Forms.DataGridViewRow

        For Each drRow In Me.dtgCatalogos.Rows
            drRow.Cells("Update").Value = True
        Next

        Me.dtgCatalogos.EndEdit()
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim drRow As System.Windows.Forms.DataGridViewRow
        Me.dtgCatalogos.EndEdit()

        For Each drRow In Me.dtgCatalogos.Rows
            drRow.Cells("Update").Value = False
        Next

        Me.dtgCatalogos.EndEdit()
    End Sub
    Private Sub cmdCopyValues_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCopyValues.Click
        Dim row As System.Windows.Forms.DataGridViewRow
        Dim Column As Integer
        Me.dtgCatalogos.EndEdit()

        Column = Me.dtgCatalogos.CurrentCell.ColumnIndex

        For Each row In Me.dtgCatalogos.Rows
            row.Cells(Column).Value = Clipboard.GetText
        Next
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTax.Click
        Me.dtgCatalogos.EndEdit()

        '**************************************************************************************************************
        '  Comentado para la implementación del BRF en la modificacion de catalogos
        '**************************************************************************************************************
        'Dim row As System.Windows.Forms.DataGridViewRow
        'Dim TaxRow As DataRow
        'Dim Tax As DataTable = cn.RunSentence("Select * From TaxCodeAssigment_Catalogs").Tables(0)

        'If Tax.Rows.Count > 0 Then
        '    For Each row In Me.dtgCatalogos.Rows
        '        For Each TaxRow In Tax.Rows
        '            If (row.Cells("Update").Value) AndAlso (TaxRow("IdVendor").ToString = row.Cells("Vendor").Value) Then
        '                row.Cells("Tax Code").Value = TaxRow("Tax_Code")

        '                '***************************************************
        '                'Este proceso se colocó dos veces para evitar que
        '                'alguno se quedara sin el NCM: dtgCatalogos.CellValueChanged
        '                Select Case TaxRow("Tax_Code").ToString.Trim.ToUpper
        '                    Case "5A", "5B", "5D"
        '                        row.Cells("NCM").Value = "ISS_00000"

        '                    Case "1B"
        '                        row.Cells("NCM").Value = "0000000000"

        '                    Case "1C"
        '                        If row.Cells("Plant").Value = "9245" Then
        '                            row.Cells("NCM").Value = "0084799090"
        '                        End If
        '                End Select
        '                '****************************************************
        '                Exit For
        '            End If
        '        Next

        '        Dim T2 As TaxInfo
        '        T2 = GetTax(Me.cboSAPBox.SelectedValue.ToString, row.Cells("LE").Value, row.Cells("Vendor").Value, row.Cells("Plant").Value, row.Cells("Mat Group").Value)

        '        If Not T2 Is Nothing AndAlso (row.Cells("Tax Code").Value <> T2.Tax) Then
        '            row.Cells("Tax Code").Value = T2.Tax
        '            row.Cells("NCM").Value = T2.NCM
        '            row.Cells("Mat Usage").Value = T2.MatUsage
        '            row.Cells("Mat Origen").Value = T2.matOri
        '        End If

        '    Next

        '    MsgBox("Tax updated")
        'Else
        '    MsgBox("No data could be retreive from Tax Table")
        'End If

        For Each r As System.Windows.Forms.DataGridViewRow In Me.dtgCatalogos.Rows
            'Se aplica un filtro en la vista para evitar mostrar PO's con UOM ACT & LE
            If (r.Cells("Update").Value) Then
                Dim T As New DataTable
                T = cn.GetBRTable(r.Cells("Vendor").Value, r.Cells("Short Text").Value)
                If Not T Is Nothing AndAlso T.Rows.Count > 0 Then
                    r.Cells("NCM").Value = T.Rows(0)("NCM Code").ToString.PadLeft(10, "0")
                    r.Cells("Mat Usage").Value = T.Rows(0)("Material Usage")
                    r.Cells("Mat Origen").Value = T.Rows(0)("Material Origen")
                End If
            End If

            If ((r.Cells("Vendor").Value.ToString.Trim) = "30000448") And (r.Cells("Short Text").Value.ToString.ToUpper.Trim.IndexOf("ELECTROBRAS – RIO NEGRO") > 0) Then
                r.Cells("Tax Code").Value = "5A"
                r.Cells("NCM").Value = "ISS_00000"
                r.Cells("Mat Usage").Value = 2
                r.Cells("Mat Origen").Value = 0
            End If

        Next

        MsgBox("NCM search finished.", MsgBoxStyle.Information)
    End Sub

    'Public Function GetBRTable(ByVal Vendor As String, ByVal Description As String) As System.Data.DataTable
    '    Dim DataTableTmp As New DataTable
    '    Dim _Adapter As New SqlClient.SqlDataAdapter
    '    Dim Pr As New SqlClient.SqlParameter
    '    Dim _ConnectionDB As New SqlClient.SqlConnection

    '    Try
    '        DataTableTmp = New DataTable()

    '        _ConnectionDB = New SqlClient.SqlConnection(cn.GetConnectionString)
    '        _ConnectionDB.Open()
    '        _Adapter = New SqlClient.SqlDataAdapter("Select Top 1 * From [Catalog BRF Rules] Where Vendor = @Vendor And [Material Description] = @Description", _ConnectionDB)

    '        Pr = New SqlClient.SqlParameter
    '        Pr.ParameterName = "Vendor"
    '        Pr.Value = Vendor
    '        _Adapter.SelectCommand.Parameters.Add(Pr)

    '        Pr = New SqlClient.SqlParameter
    '        Pr.ParameterName = "Description"
    '        Pr.Value = Description
    '        _Adapter.SelectCommand.Parameters.Add(Pr)

    '        _Adapter.Fill(DataTableTmp)

    '        If DataTableTmp.Rows.Count > 0 Then
    '            Return DataTableTmp
    '        Else
    '            Return Nothing
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    Finally
    '        _ConnectionDB.Close()
    '        DataTableTmp.Dispose()
    '        _Adapter.Dispose()
    '        _Adapter = Nothing
    '        _ConnectionDB = Nothing
    '    End Try

    'End Function
    Private Sub Check_Almacenaje()
        Dim row As DataGridViewRow
        Dim SAPBox As String = Microsoft.VisualBasic.Left(Me.cboSAPBox.SelectedValue.ToString, 3)
        Dim RiseWarning As Boolean = False

        For Each row In dtgCatalogos.Rows

            If ((SAPBox = "L7P") AndAlso (row.Cells("Plant").Value = "2921")) Or ((SAPBox = "L6P") AndAlso (row.Cells("Plant").Value = "1867")) Then
                Select Case row.Cells("Vendor").Value
                    Case 15033348, 15219645, 15219715, 30001095, 30001687
                        Dim TextToEvaluate As String = LCase(row.Cells("Short Text").Value)

                        If (TextToEvaluate.ToLower.IndexOf("armazenagem") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If

                        If (TextToEvaluate.ToLower.IndexOf("armazenamento") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If

                        If (TextToEvaluate.ToLower.IndexOf("locação") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If

                        If (TextToEvaluate.ToLower.IndexOf("aluguel") >= 0) Then
                            row.DefaultCellStyle.BackColor = Drawing.Color.LightCoral
                            RiseWarning = True
                        End If
                End Select
            End If
        Next

        If RiseWarning Then
            MsgBox("Highlighted rows should use tax code 5B", MsgBoxStyle.Exclamation, "Plase check Tax Code")
        End If
    End Sub
    Private Sub dtgCatalogos_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgCatalogos.CellValueChanged


        ' Comentado para utilizar la funcion BRF


        ''**************************************
        '' Tambien se encuentra en: cmdTax.Click
        ''**************************************
        'If dtgCatalogos.Columns(e.ColumnIndex).Name.Equals("Tax Code", StringComparison.InvariantCultureIgnoreCase) Then
        '    If Not DBNull.Value.Equals(dtgCatalogos.Rows(e.RowIndex).Cells("Tax Code").Value) Then

        '        Select Case dtgCatalogos.Rows(e.RowIndex).Cells("Tax Code").Value.ToString.Trim.ToUpper
        '            Case "5A", "5B", "5D"
        '                dtgCatalogos.Rows(e.RowIndex).Cells("NCM").Value = "ISS_00000"

        '            Case "1B"
        '                dtgCatalogos.Rows(e.RowIndex).Cells("NCM").Value = "0000000000"

        '            Case "1C"
        '                If dtgCatalogos.Rows(e.RowIndex).Cells("Plant").Value = "9245" Then
        '                    dtgCatalogos.Rows(e.RowIndex).Cells("NCM").Value = "0084799090"
        '                End If
        '        End Select
        '    End If

        '    Dim T2 As TaxInfo
        '    T2 = GetTax(Me.cboSAPBox.SelectedValue.ToString, dtgCatalogos.Rows(e.RowIndex).Cells("LE").Value, dtgCatalogos.Rows(e.RowIndex).Cells("Vendor").Value, dtgCatalogos.Rows(e.RowIndex).Cells("Plant").Value, dtgCatalogos.Rows(e.RowIndex).Cells("Mat Group").Value)

        '    If Not T2 Is Nothing AndAlso (dtgCatalogos.Rows(e.RowIndex).Cells("Tax Code").Value <> T2.Tax) Then
        '        dtgCatalogos.Rows(e.RowIndex).Cells("Tax Code").Value = T2.Tax
        '        dtgCatalogos.Rows(e.RowIndex).Cells("NCM").Value = T2.NCM
        '        dtgCatalogos.Rows(e.RowIndex).Cells("Mat Usage").Value = T2.MatUsage
        '        dtgCatalogos.Rows(e.RowIndex).Cells("Mat Origen").Value = T2.MatOri
        '    End If
        'End If
    End Sub
    Private Sub dtgCatalogos_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dtgCatalogos.ColumnHeaderMouseClick
        Check_Almacenaje()
    End Sub
    Private Sub BG_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BG.DoWork
        'Dim Row As System.Windows.Forms.DataGridViewRow
        'Dim cont As Integer = 0

        'Dim SAPCn As New SAPCOM.SAPConnector
        'Dim Conn As Object = SAPCn.GetSAPConnection(SAP, gsUsuarioPC, "LAT")

        'For Each Row In dtgCatalogos.Rows
        '    If Row.Cells("Update").Value Then
        '        cont += 1
        '        Status = "Changing [" & cont & " Of " & Total & "]: PO:" & Row.Cells("Doc Number").Value & " Item: " & Row.Cells("Item Number").Value
        '        BG.ReportProgress((cont / Total) * 100)
        '        Dim POChange As New SAPCOM.POChanges(Conn, Row.Cells("Doc Number").Value)

        '        If POChange.IsReady Then
        '            POChange.TaxCode(Row.Cells("Item Number").Value) = Row.Cells("Tax Code").Value.ToString.ToUpper.Trim
        '            POChange.JurisdCode(Row.Cells("Item Number").Value) = Row.Cells("Jur Code").Value.ToString.ToUpper.Trim
        '            POChange.MaterialOrigin(Row.Cells("Item Number").Value) = Row.Cells("Mat Origen").Value.ToString.ToUpper.Trim
        '            POChange.MaterialUsage(Row.Cells("Item Number").Value) = Row.Cells("Mat Usage").Value.ToString.ToUpper.Trim
        '            POChange.BrasNCMCode(Row.Cells("Item Number").Value) = Row.Cells("NCM").Value.ToString.ToUpper.Trim
        '            POChange.MaterialCategory(Row.Cells("Item Number").Value) = Row.Cells("Mat Category").Value.ToString.ToUpper.Trim
        '            'POChange.ItemNetPrice(Row.Cells("Item Number").Value) = Row.Cells("Price").Value.ToString.ToUpper.Trim
        '            'POChange.PurchOrg = "1495"

        '            POChange.CommitChanges()
        '            If Not POChange.Success Then
        '                Dim er As String
        '                Dim EM As String = ""

        '                For Each er In POChange.Results
        '                    EM = EM & Chr(13) & er
        '                Next

        '                MsgBox("Error while changing PO: " & SAP & ":" & Row.Cells("Doc Number").Value & " Item " & Row.Cells("Item Number").Value & Chr(13) & Chr(13) & "Description:" & EM)
        '            End If

        '        Else
        '            MsgBox("Error getting SAP Connection.", MsgBoxStyle.Exclamation)
        '        End If
        '    End If
        'Next

        Dim Row As System.Windows.Forms.DataGridViewRow
        Dim cont As Integer = 0
        Dim SAPCn As New SAPCOM.SAPConnector
        Dim Conn As Object = SAPCn.GetSAPConnection(SAP, gsUsuarioPC, "LAT")
        Dim CD As SAPCOM.ConnectionData
        Dim SC As New SAPCOM.SAPConnector
        CD = SC.GetConnectionData(SAP, gsUsuarioPC, "LAT")
        Dim SCn = SC.GetSAPConnection(CD)
        Dim iSAP As New SAPConection.c_SAP(SAP)

        iSAP.UserName = gsUsuarioPC
        iSAP.Password = CD.Password
        iSAP.OpenConnection(SAPConfig)
        Dim BRF As New SAPConection.BRF_Fixing(iSAP.GUI)

        For Each Row In dtgCatalogos.Rows
            If Row.Cells("Update").Value Then
                cont += 1
                Status = "Changing [" & cont & " Of " & Total & "]: PO:" & Row.Cells("Doc Number").Value & " Item: " & Row.Cells("Item Number").Value
                BG.ReportProgress((cont / Total) * 100)
                Dim POChange As New SAPCOM.POChanges(Conn, Row.Cells("Doc Number").Value)

                If POChange.IsReady Then
                    POChange.TaxCode(Row.Cells("Item Number").Value) = Row.Cells("Tax Code").Value.ToString.ToUpper.Trim
                    POChange.JurisdCode(Row.Cells("Item Number").Value) = Row.Cells("Jur Code").Value.ToString.ToUpper.Trim
                    POChange.MaterialOrigin(Row.Cells("Item Number").Value) = Row.Cells("Mat Origen").Value.ToString.ToUpper.Trim
                    POChange.MaterialUsage(Row.Cells("Item Number").Value) = Row.Cells("Mat Usage").Value.ToString.ToUpper.Trim
                    POChange.BrasNCMCode(Row.Cells("Item Number").Value) = Row.Cells("NCM").Value.ToString.ToUpper.Trim
                    POChange.MaterialCategory(Row.Cells("Item Number").Value) = Row.Cells("Mat Category").Value.ToString.ToUpper.Trim

                    POChange.CommitChanges()
                    If Not POChange.Success Then
                        Dim er As String
                        Dim EM As String = ""

                        For Each er In POChange.Results
                            EM = EM & Chr(13) & er
                        Next

                        MsgBox("Error while changing PO: " & SAP & ":" & Row.Cells("Doc Number").Value & " Item " & Row.Cells("Item Number").Value & Chr(13) & Chr(13) & "Description:" & EM)
                    Else
                        If iSAP.Conected Then
                            BRF.IncludePO(New SAPConection.BRF_PO(Row.Cells("Doc Number").Value, Row.Cells("Item Number").Value))
                            BRF.Execute()
                        End If
                    End If
                Else
                    MsgBox("Error getting SAP Connection.", MsgBoxStyle.Exclamation)
                End If
            End If
        Next

        iSAP.CloseConnection()

        MsgBox("Done.", MsgBoxStyle.Information)
    End Sub
    Private Sub BG_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BG.ProgressChanged
        lblStatus.Text = Status
        PB.Value = e.ProgressPercentage
    End Sub
    Private Sub BG_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BG.RunWorkerCompleted
        lblRuning.DisplayStyle = ToolStripItemDisplayStyle.None
        PB.Value = 0
        lblStatus.Text = "Process finished."
    End Sub
    Public Function GetTax(ByVal pSAP As String, ByVal pLE As String, Optional ByVal pVendor As String = Nothing, Optional ByVal pPlant As String = Nothing, Optional ByVal pMatGrp As String = Nothing) As TaxInfo
        Dim cn As New OAConnection.Connection
        Dim Data As DataTable
        Dim Where As String = ""

        Try
            If Not pVendor Is Nothing Then
                Where = "(([Vendor code] = '') or ([Vendor code] = '" & pVendor & "'))"
            End If

            If Not pPlant Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                Where = Where & "((Plant = '') or (Plant = '" & pPlant & "'))"
            End If

            If Not pMatGrp Is Nothing Then
                If Where <> "" Then
                    Where = Where & " And "
                End If

                Where = Where & "(([Mat Group] = '') or ([Mat Group] = '" & pMatGrp & "'))"
            End If


            Data = cn.RunSentence("Select *,0 as Value From BR_Tax_Rules Where (LE = '" & pLE & "' and SAPBox = '" & pSAP & "')" & IIf(Where <> "", " And (" & Where & ")", "")).Tables(0)
            If Data.Rows.Count > 0 Then
                If Data.Rows.Count = 1 Then
                    Dim T As New TaxInfo

                    T.Tax = Data.Rows(0).Item("TAX")
                    T.matOri = Data.Rows(0).Item("Mat Origin")
                    T.MatUsage = Data.Rows(0).Item("Mat Usage")
                    T.NCM = Data.Rows(0).Item("NCM Code")
                    Return T
                Else

                    For Each r As DataRow In Data.Rows
                        Dim val As Integer = 0

                        If (r("LE") = pLE) Then
                            val += 2
                        Else
                            If r("LE") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("SAPBox") = pSAP) Then
                            val += 2
                        Else
                            If r("SAPBox") = "" Then
                                val += 1
                            End If
                        End If


                        If (r("Plant") = pPlant) Then
                            val += 2
                        Else
                            If r("Plant") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("Vendor code") = pVendor) Then
                            val += 2
                        Else
                            If r("Vendor code") = "" Then
                                val += 1
                            End If
                        End If

                        If (r("Mat Group") = pMatGrp) Then
                            val += 2
                        Else
                            If r("Mat Group") = "" Then
                                val += 1
                            End If
                        End If

                        r("Value") = val
                    Next


                    Dim T As New TaxInfo
                    Dim Tx = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Tax")).ToList()
                    Dim TMO = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Mat Origin")).ToList()
                    Dim TMU = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Mat Usage")).ToList()
                    Dim TNCM = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("NCM Code")).ToList()
                    Dim TMC = (From C In Data.AsEnumerable() Order By C.Item("Value") Descending Select C.Item("Mat Category")).ToList()

                    T.Tax = Tx(0)
                    T.matOri = TMO(0)
                    T.MatUsage = TMU(0)
                    T.NCM = TNCM(0)
                    T.MatCategory = TMC(0)

                    'MsgBox("Multiple choises for:" & Chr(13) & Chr(13) & "SAPBox: " & pSAP & Chr(13) & "LE: " & pLE & Chr(13) & "Plant:" & pPlant & Chr(13) & "Vendor: " & pVendor & Chr(13) & "Mat. Grp: " & pMatGrp)
                    Return T
                End If
            Else
                ' MsgBox("Rules can't be found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class

Public Class TaxInfo
    Public Tax
    Public NCM
    Public MatUsage
    Public MatOri
    Public MatCategory
End Class