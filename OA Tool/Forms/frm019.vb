Public Class frm019
    Dim SAPCn As New SAPConection.SAPTools
    Dim SAP As Object
    Dim cn As New OAConnection.Connection
    Dim Access As New Access.Application
    Dim Plantas As DataSet
    Dim Tabla As DataTable
    'Dim SAP_ERP As New SAPCOM.OpenReqs_Report("L7P", "bm4691")


    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim i%
        'Dim rec As DataTable

        If cn.GetUserId = "BM4691" Then
            If MsgBox("Descargar las requisiciones de SAP?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Descargar requisiciones?") = MsgBoxResult.Yes Then
                SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", Me.txtUser.Text, Me.txtPwr.Text, SAPConfig)

                '********************************************************************************
                'SAP_ERP.IncludeMaterialFromTo("30000000", "39999999")
                'SAP_ERP.RepairsLevel = 2
                'SAP_ERP.Execute()
                '********************************************************************************

                SAPCn.DownloadEban(SAP)
                SAPCn.CloseSession(SAP)
                'SAPCn = Nothing

                Access.Run("ImportEBAN", My.Application.Info.DirectoryPath & "\OADownLoad\EBAN.txt")

                Tabla = cn.GetAccessTable("Select * From Eban").Tables(0)
                cn.ExecuteInServer("Delete From Eban")
                cn.AppendTableToSqlServer("EBAN", Tabla)
            End If
        End If

        Tabla = cn.RunSentence("Select * From vstRequis").Tables(0)

        EvaluarRequis()
        Me.dtgContratos.DataSource = Tabla
        Me.txtContratos.Text = Tabla.Rows.Count

        If Tabla.Columns.Count > 0 Then
            i = 0
            Do While i < Tabla.Columns.Count
                cboFiltro.Items.Add(Tabla.Columns(i).ColumnName)
                i = i + 1
            Loop
        End If
    End Sub


    Private Sub frm019_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        cn.KillProcess("MSAccess")
        SAP = Nothing
        SAPCn = Nothing
    End Sub


    Private Sub frm019_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Dim i%
        Tabla = cn.RunSentence("Select * From CrashRequis").Tables(0)

        If Tabla.Rows.Count > 0 Then
            i = 0
            cboIssues.Items.Add("Listado de Problemas en Requisiciones")
            Do While i < Tabla.Rows.Count
                cboIssues.Items.Add(Trim(Tabla.Rows(i).Item("ID")) & " - " & Tabla.Rows(i).Item("Descripción"))
                i = i + 1
            Loop
        End If
    End Sub

    Private Sub EvaluarRequis()
        Dim i%
        Dim Status$
        Dim Fail$
        Dim InOA As Boolean
        Dim OASourceList As Boolean

        Dim x As New OARequis.Requisition

        For i = 0 To Tabla.Rows.Count - 1
            Fail = ""
            InOA = False
            OASourceList = False


            If DBNull.Value.Equals(Tabla.Rows(i).Item("OA_Contrato")) Then
                Fail = "1" 'Material no está en contrato
            Else
                InOA = True
            End If

            If InOA Then
                If (Tabla.Rows(i).Item("ReqDate") < Tabla.Rows(i).Item("OAInicio")) Or (Tabla.Rows(i).Item("ReqDate") > Tabla.Rows(i).Item("OAFin")) Then
                    Fail = Fail & "0" 'La requisición esta fuera de la validez del contrato
                End If
            End If

            'Si el material tiene contrato
            If InOA And ((DBNull.Value.Equals(Tabla.Rows(i).Item("SourceListOA_Start")))) Then
                Fail = Fail & "2" 'Material sin el source list del contrato
            Else
                OASourceList = True
            End If

            If InOA And OASourceList Then
                If (Tabla.Rows(i).Item("ReqDate") < Tabla.Rows(i).Item("SourceListOA_Start")) Or (Tabla.Rows(i).Item("ReqDate") > Tabla.Rows(i).Item("SourceListOA_End")) Then
                    Fail = Fail & "3" 'La requisición esta fuera de la validez del source list
                End If
            End If

            If InOA Then
                If (Tabla.Rows(i).Item("DeliveryDate") > Tabla.Rows(i).Item("OAFin")) Then
                    Fail = Fail & "4" 'La fecha de entrega de la requi es posterior a la fecha de vencimiento del contrato
                End If
            End If


            If InOA Then

                If DBNull.Value.Equals(Tabla.Rows(i).Item("AutoPO_MasterData")) Or Tabla.Rows(i).Item("AutoPO_MasterData") = "" Then
                    Fail = Fail & "5" 'Material sin el check de Auto PO de master data
                End If
            End If

            If InOA Then
                If DBNull.Value.Equals(Tabla.Rows(i).Item("SourceList_MasterData")) Or (Tabla.Rows(i).Item("SourceList_MasterData") = "") Then
                    Fail = Fail & "6" 'Material sin el check de source list de master data
                End If
            End If


            If DBNull.Value.Equals(Tabla.Rows(i).Item("OA")) And Not DBNull.Value.Equals(Tabla.Rows(i).Item("OA_Contrato")) Then
                Fail = Fail & "8" 'Material no tiene Assing & Process y está en contrato
            End If


            If InOA Then
                If Trim(Tabla.Rows(i).Item("PG_Contrato")) = "081" Then
                    Fail = Fail & "9" 'Materiel en contrato de Importados
                End If
            End If

            If Fail.Length > 0 Then
                Status = "Fail"
            Else
                Status = "Ok"
            End If

            Tabla.Rows(i).Item("Status") = Status
            Tabla.Rows(i).Item("Reason") = Fail
            'Tabla.Rows(i).Item("Test") = x.ReturnStatus(Tabla.Rows(i).Item("Requisition"), Tabla.Rows(i).Item("Item"))
        Next



    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Filtrar()
    End Sub

    Private Sub Filtrar()
        If txtBuscar.Text <> "" And Me.cboFiltro.Text <> "" Then
            Select Case Tabla.Columns(cboFiltro.Text).DataType.ToString
                Case "System.Double", "System.Int16", "System.Int32", "System.Int64"
                    Tabla.DefaultView.RowFilter = (cboFiltro.Text & " = " & txtBuscar.Text)

                Case "System.String"
                    Tabla.DefaultView.RowFilter = (cboFiltro.Text & " like '%" & txtBuscar.Text & "%'")
            End Select
        Else
            Tabla.DefaultView.RowFilter = ""
        End If
    End Sub

    Private Sub txtBuscar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBuscar.KeyPress
        If e.KeyChar = Chr(13) Then
            Filtrar()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Me.dtgContratos.SelectionMode = Windows.Forms.DataGridViewSelectionMode.CellSelect
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim req As New OARequis.Requisition
        If Me.dtgContratos.SelectionMode <> Windows.Forms.DataGridViewSelectionMode.FullRowSelect Then
            MsgBox("El modo de selección de celdas debe ser por fila.", MsgBoxStyle.Information)
        Else
            ' req.ReturnStatus("10681927", "10")
            req.TestRequisition(Me.dtgContratos.SelectedRows.Item(0).Cells("Requisition").Value, Me.dtgContratos.SelectedRows.Item(0).Cells("Item").Value)
        End If

    End Sub
End Class