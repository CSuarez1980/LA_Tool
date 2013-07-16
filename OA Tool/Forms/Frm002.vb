Public Class frm002
    Dim SAPCn As New SAPConection.SAPTools
    Dim SAP As Object
    Dim cn As New OAConnection.Connection
    Dim Access As New Access.Application
    Dim Plantas As DataSet
    Dim Tabla As DataSet

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSapProcess.Click
        Plantas = cn.GetActivesPlants()

        'Dim oSAP As New SAPConection.c_SAP
        'Dim dtEban As New DataTable

        ' oSAP.DownloadDetailPO("L7P", "BM4691", "0045")

        'oSAP.DownloadOTD("L7P", "BM4691", "0045")

        'oSAP.DownloadPO("L7P", "BM4691", "05/23/2010", "05/23/2010")
        ' '' ''Dim dtOA As New DataTable
        ' '' ''Dim dtOADetail As New DataTable

        ' '' ''dtOA = oSAP.DownloadHeaderOA("L7P", gsUsuarioPC)
        ' '' ''dtOADetail = oSAP.DownloadDetailOA("L7P", gsUsuarioPC)
        'dtEban = oSAP.DownloadEBAN("L7P", gsUsuarioPC)

        '' '' ''Remove columns from HeaderOAReport
        ' '' ''If dtOA.Columns.IndexOf("Company Code") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Company Code")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Doc Type") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Doc Type")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Company Code") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Company Code")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Your Ref") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Your Ref")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Salesperson") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Salesperson")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Telephone") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Telephone")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("OA Number") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("OA Number")
        ' '' ''End If

        ' '' ''If dtOA.Columns.IndexOf("Our Ref") <> -1 Then
        ' '' ''    dtOA.Columns.Remove("Our Ref")
        ' '' ''End If

        ' '' ''If dtOA.Rows.Count > 0 Then
        ' '' ''    cn.ExecuteInServer("Delete From HeaderContrato")
        ' '' ''    cn.AppendTableToSqlServer("HeaderContrato", dtOA)
        ' '' ''End If

        '' '' ''*************************************************************
        '' '' ''   Remove Columns of OA Detail
        '' '' ''*************************************************************

        ' '' ''If dtOADetail.Columns.IndexOf("Quantity") <> -1 Then
        ' '' ''    dtOADetail.Columns.Remove("Quantity")
        ' '' ''End If

        ' '' ''If dtOADetail.Columns.IndexOf("Mat Group") <> -1 Then
        ' '' ''    dtOADetail.Columns.Remove("Mat Group")
        ' '' ''End If

        ' '' ''If dtOADetail.Columns.IndexOf("Tracking Fld") <> -1 Then
        ' '' ''    dtOADetail.Columns.Remove("Tracking Fld")
        ' '' ''End If

        ' '' ''If dtOADetail.Columns.IndexOf("Price Unit") <> -1 Then
        ' '' ''    dtOADetail.Columns.Remove("Price Unit")
        ' '' ''End If

        ' '' ''If dtOADetail.Rows.Count > 0 Then
        ' '' ''    cn.ExecuteInServer("Delete From DetalleContrato")
        ' '' ''    cn.AppendTableToSqlServer("DetalleContrato", dtOADetail)
        ' '' ''End If

        'SAP = SAPCn.GetConnectionToSAP("L7A TS Acceptance", Me.txtUser.Text, Me.txtPwr.Text)
        SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", Me.txtUser.Text, Me.txtPwr.Text, SAPConfig)

        Dim i%
        Dim MinPO$
        Dim MaxPO$
        Dim MinPOCatalogos$
        Dim MaxPOCatalogos$

        Plantas = cn.GetActivesPlants()

        If Me.chkHeaderContrato.Checked Then
            SAPCn.DownloadHeaderContrato(SAP)
        End If

        If Me.chkActualizarHeaderContratos.Checked Then
            Access.Run("ImportHeaderContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKKO_Contratos.txt")
        End If

        If Me.chkDetalleContrato.Checked Then
            SAPCn.DownloadDetalleContratos(SAP, Plantas)
        End If

        If Me.chkActualizarDetalleContrato.Checked Then
            Access.Run("ImportDetalleContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKPO_Contratos.txt")
        End If

        If Me.chkHeaderCompras.Checked Then
            SAPCn.DownloadHeaderCompras(SAP, Me.txtInicioCompras.Text, Me.txtFinCompras.Text)
            Access.Run("ImportHeaderCompras", My.Application.Info.DirectoryPath & "\OADownLoad\EKKO_Compras.txt")
            MinPO = Access.Run("GetMinPO")
            MaxPO = Access.Run("GetMaxPO")
        End If

        If Me.chkDetalleCompras.Checked Then
            Access.DoCmd.RunSQL("Delete from DetalleCompras")

            For i = 0 To Plantas.Tables(0).Rows.Count - 1
                If SAPCn.DownloadDetalleCompras(SAP, Plantas.Tables(0).Rows(i).Item(0), MinPO, MaxPO) Then
                    Access.Run("ImportDetalleCompras", My.Application.Info.DirectoryPath & "\OADownLoad\EKPO_Compras_" & Plantas.Tables(0).Rows(i).Item(0) & ".txt")
                End If
            Next i
        End If

        If Me.chkProveedores.Checked Then
            SAPCn.DownloadVendors(SAP)
        End If

        If Me.chkActualizarProveedores.Checked Then
            Access.Run("ImportVendors", My.Application.Info.DirectoryPath & "\OADownLoad\Vendors.txt")
        End If

        'If Me.chkHeaderCatalogos.Checked Then
        '    Access.DoCmd.RunSQL("Delete from HeaderCatalogos")
        '    Access.DoCmd.RunSQL("Delete from DetalleCatalogo")

        '    SAPCn.DownloadHeaderCatalogos(SAP, Me.txtInicioCompras.Text, Me.txtFinCompras.Text)
        '    Access.Run("ImportHeaderCatalogo", My.Application.Info.DirectoryPath & "\OADownLoad\HeaderCatalogos.txt")

        '    MinPOCatalogos = Access.Run("GetMinPOCatalogo")
        '    MaxPOCatalogos = Access.Run("GetMaxPOCatalogo")

        '    SAPCn.DownloadDetalleCatalogo(SAP, "", MinPOCatalogos, MaxPOCatalogos)

        '    Access.Run("ImportDetalleCatalogo", My.Application.Info.DirectoryPath & "\OADownLoad\DetalleCatalogos.txt")
        'End If


        If Me.chkActualOTD.Checked Then
            Access.DoCmd.RunSQL("Delete From CurrentOTD")

            For i = 0 To Plantas.Tables(0).Rows.Count - 1
                SAPCn.DownloadCurrentOTD(SAP, Plantas.Tables(0).Rows(i).Item(0))
                Access.Run("ImportCurrentOTD", My.Application.Info.DirectoryPath & "\OADownLoad\CurrentOTD_" & Plantas.Tables(0).Rows(i).Item(0) & ".txt")
                Access.DoCmd.RunSQL("Update CurrentOTD Set Planta = '" & Plantas.Tables(0).Rows(i).Item(0) & "' Where isNull(Planta)")
                Access.DoCmd.RunSQL("Delete From CurrentOTD Where isNull(Gica)")
            Next

            Access.DoCmd.RunSQL("Update CurrentOTD Set OTD = round(((Del1 + Del2)/(Del1 + Del2 + Del3 + Del4 + Del5) * 100),2) Where MeanPDT <> 0")
        End If
        '*********************************************************************

        If Me.chkMasterData.Checked Then
            Access.DoCmd.RunSQL("Delete From MasterData")
            For i = 0 To Plantas.Tables(0).Rows.Count - 1
                SAPCn.DownloadMasterData(SAP, Plantas.Tables(0).Rows(i).Item(0))
                Access.Run("ImportMasterData", My.Application.Info.DirectoryPath & "\OADownLoad\Marc_" & Plantas.Tables(0).Rows(i).Item(0) & ".txt")
            Next
        End If

        If Me.chkManufacter.Checked Then
            Access.DoCmd.RunSQL("Delete From Manufacter")
            'For i = 0 To Plantas.Tables(0).Rows.Count - 1
            SAPCn.DownloadManufacter(SAP, Plantas.Tables(0))
            Access.Run("ImportManufacter", My.Application.Info.DirectoryPath & "\OADownLoad\ZMA1.txt")
            'Next
        End If


        If Me.chkKathia.Checked Then
            'cn.ExecuteInAccess("Delete From EKBE")
            'cn.ExecuteInAccess("Delete From EKET")
            Access.DoCmd.RunSQL("Delete From EKBE")
            Access.DoCmd.RunSQL("Delete From EKET")

            For i = 0 To Plantas.Tables(0).Rows.Count - 1
                If SAPCn.DownloadEKBE(SAP, Plantas.Tables(0).Rows(i).Item(0), Me.txt1.Text, Me.txt2.Text, PDFPath) Then
                    'Access.Run("ImportEKBE", My.Application.Info.DirectoryPath & "\OADownLoad\EKBE.txt")
                    Access.Run("ImportEKBE", PDFPath & "\EKBE.txt")
                End If
            Next

            MinPO = Access.DMin("PO", "EKBE")
            MaxPO = Access.DMax("PO", "EKBE")

            SAPCn.DownloadEKET(SAP, MinPO, MaxPO, PDFPath)
            'Access.Run("ImportEKET", My.Application.Info.DirectoryPath & "\OADownLoad\EKET.txt")
            Access.Run("ImportEKET", PDFPath & "\EKET.txt")


            'Access.Run("ImportEKET", My.Application.Info.DirectoryPath & "\OADownLoad\EKET.txt", False)
        End If

        If Me.chkSourceList.Checked Then
            Access.DoCmd.RunSQL("Delete from EORD")
            SAPCn.DownloadSourceList(SAP)

            ' Access.Run("ImportEORD", My.Application.Info.DirectoryPath & "\OADownLoad\EORD.txt")
            Access.Run("ImportEORD", PDFPath & "\EORD.txt")
        End If

        On Error Resume Next
        Access.DoCmd.RunSQL("Drop table EKPO_Contratos_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKKO_Contratos_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Vendors_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKKO_Compras_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Ekbe_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EORD_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKET_ImportErrors")

        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0045_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0051_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0278_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0300_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0301_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_2921_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_2930_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4004_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4563_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4841_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4950_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_7761_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_8727_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9245_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9265_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9266_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9367_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9475_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9476_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9653_ImportErrors")

        Access.DoCmd.RunSQL("Drop table Manufacter_0045_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0051_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0278_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0300_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0301_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_2921_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_2930_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4004_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4563_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4841_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4950_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_7761_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_8727_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9245_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9265_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9266_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9367_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9475_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9476_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9653_ImportErrors")

        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors1")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors2")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors3")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors4")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors5")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors6")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors7")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors8")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors9")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors10")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors11")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors12")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors13")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors14")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors15")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors16")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors17")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors18")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors19")
        Access.DoCmd.RunSQL("Drop table EKBE_ImportErrors20")

        SAPCn.CloseSession(SAP)

        If Me.chkSincServer.Checked Then
            Tabla = cn.GetAccessTable("Select * From [Detalle de Contrato]")
            If Tabla.Tables(0).Rows.Count > 0 Then
                cn.ExecuteInServer("Delete From ContratosActivos")
                cn.AppendTableToSqlServer("ContratosActivos", Tabla.Tables(0))
                cn.ExecuteInServer("Insert Into Contratos Select * from vstContratosNuevos")
                cn.ExecuteInServer("Delete From Contratos Where (OA Not In(Select Distinct OA From vstContratos))")
                cn.ExecuteInServer("Delete From Distribucion Where (OA Not In(Select Distinct OA From vstContratos))")
            End If
        End If

        MsgBox("Done!", MsgBoxStyle.Information)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Sap As New SAPConection.SAPTools
    End Sub

    Private Sub ToolStripButton2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        'Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        Access.Run("ExportToXLDetalleDeContrato")
        MsgBox("Done!.")
    End Sub

    Private Sub frm002_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        cn.KillProcess("MSAccess")
        SAP = Nothing
        SAPCn = Nothing
    End Sub

    Private Sub frm002_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        If cn.GetUserId <> "BM4691" Then
            Me.chkSincServer.Enabled = False
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Access.Run("ExportOTDReport")
        MsgBox("Done!.")
    End Sub

End Class