

Public Class Frm001
    Dim cn As New OAConnection.Connection
    Dim Access As New Access.Application

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim OACxn As New OAConnection.Connection
        Dim Table As DataSet
        Dim FileName$
        Dim OpenFileDialog As New Windows.Forms.OpenFileDialog

        FileName = ""
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        OpenFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
        End If

        If FileName <> "" Then
            Table = OACxn.GetXLTable(FileName, "Detalle_de_Catalogos", "select * from [Detalle_de_Catalogos$] Where update = 'X'")
            Me.dtgCatalogos.DataSource = Table.Tables(0)
            FixCells()
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Access.Run("ExportCatalogsToXL")
        MsgBox("Done!")
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        Dim SAPCn As New SAPConection.SAPTools
        Dim SAP As Object
        Dim i%

        If Me.cboSAPBox.Text = "" Then
            MsgBox("Debe seleccionar una caja para continuar...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.txtUser.Text = "" Then
            MsgBox("Debe ingresar su T-Number para continuar...", MsgBoxStyle.Information)
            Exit Sub
        End If

        If Me.txtPwr.Text = "" Then
            MsgBox("Debe ingresar su contraseña para continuar...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        SAP = SAPCn.GetConnectionToSAP(cboSAPBox.Text, Me.txtUser.Text, Me.txtPwr.Text, SAPConfig)
        For i = 0 To Me.dtgCatalogos.RowCount - 2
            With Me.dtgCatalogos.Rows(i)
                If UCase(Me.dtgCatalogos.Rows(i).Cells("update").Value) = "X" Then
                    Me.dtgCatalogos.Rows(i).Cells("System Message").Value = SAPCn.UpdateCatalogos(SAP, Me.dtgCatalogos.Rows(i).Cells("PurchDoc").Value, Me.dtgCatalogos.Rows(i).Cells("Item").Value, Me.dtgCatalogos.Rows(i).Cells("TaxCode").Value, Me.dtgCatalogos.Rows(i).Cells("Jur_Code").Value, Me.dtgCatalogos.Rows(i).Cells("Mat_Usage").Value, Me.dtgCatalogos.Rows(i).Cells("Mat_Origen").Value, Me.dtgCatalogos.Rows(i).Cells("NCM").Value, Me.dtgCatalogos.Rows(i).Cells("Category").Value)
                End If
            End With
        Next i

        SAP.findbyId("wnd[0]").Close()
        SAP.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()

        MsgBox("Catálogos Actualizados", MsgBoxStyle.Information)
    End Sub

    Private Sub btnSapProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSapProcess.Click
        Dim SAP As Object
        Dim SAPCn As New SAPConection.SAPTools
        Dim MinPOCatalogos$
        Dim MaxPOCatalogos$
        Dim Tabla As New DataTable
        Dim i%

        If Me.cboTipoDescarga.Text = "" Then
            MsgBox("Debe seleccionar el tipo de descarga para continuar...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.cboSAPBox.Text = "" Then
            MsgBox("Debe seleccionar una caja para continuar...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.txtUser.Text = "" Then
            MsgBox("Debe ingresar su T-Number para continuar...", MsgBoxStyle.Information)
            Exit Sub
        End If

        If Me.txtPwr.Text = "" Then
            MsgBox("Debe ingresar su contraseña para continuar...", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Date.Parse(Me.txtInicioCompras.Text) > Date.Parse(Me.txtFinCompras.Text) Then
            MsgBox("Error en el formato de las fechas." & vbCr & vbCr & "Favor verifique el rango de fechas y vuelva a intentarlo.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Access.DoCmd.RunSQL("Delete from HeaderCatalogos")
        Access.DoCmd.RunSQL("Delete from DetalleCatalogo")

        SAP = SAPCn.GetConnectionToSAP(cboSAPBox.Text, Me.txtUser.Text, Me.txtPwr.Text, SAPConfig)

        If Me.cboTipoDescarga.Text.ToLower = "catálogos" Then
            'Descarga de los catalogos
            '*********************************************************************
            '*********************************************************************
            If Not SAPCn.DownloadHeaderCatalogos(SAP, Me.txtInicioCompras.Text, Me.txtFinCompras.Text, Microsoft.VisualBasic.Left(Me.cboSAPBox.Text, 3)) Then
                SAPCn.CloseSession(SAP)
                Exit Sub
            End If
        Else
            'Descarga de los utilities
            Tabla = cn.RunSentence("Select Valor From Variantes Where Campo = 'Vendor' and Variante = 'UTILITIES' and usuario = '" & Me.cn.GetUserId & "'").Tables(0)

            If Tabla.Rows.Count > 0 Then
                cn.Put_DataTable_In_ClipBoard(Tabla)
                If Not SAPCn.DownloadHeaderCatalogos_Utilities(SAP, Me.txtInicioCompras.Text, Me.txtFinCompras.Text, Tabla, Microsoft.VisualBasic.Left(Me.cboSAPBox.Text, 3)) Then
                    SAPCn.CloseSession(SAP)
                    Exit Sub
                End If
            Else
                MsgBox("No hay proveedores en la variante!")
            End If
        End If

        Access.Run("ImportHeaderCatalogo", My.Application.Info.DirectoryPath & "\OADownLoad\HeaderCatalogos.txt")

        If Access.DCount("PurchDoc", "HeaderCatalogos") > 0 Then
            Tabla = cn.GetAccessTable("Select Distinct PurchDoc From HeaderCatalogos").Tables(0)
        Else
            MsgBox("Error al bajar el encabezado de los catalogos", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        cn.Put_DataTable_In_ClipBoard(Tabla)
        SAPCn.DownloadDetalleCatalogo(SAP, "", Tabla, Microsoft.VisualBasic.Left(Me.cboSAPBox.Text, 3))
        Access.Run("ImportDetalleCatalogo", My.Application.Info.DirectoryPath & "\OADownLoad\DetalleCatalogos.txt")

        Tabla = cn.GetAccessTable("Select distinct vendor From HeaderCatalogos").Tables(0)

        cn.Put_DataTable_In_ClipBoard(Tabla)

        SAPCn.DownloadVendorsFix(SAP, Tabla, Microsoft.VisualBasic.Left(Me.cboSAPBox.Text, 3))
        Access.Run("ImportVendors", My.Application.Info.DirectoryPath & "\OADownLoad\Vendors.txt")

        If Me.cboTipoDescarga.Text.ToLower = "utilities" Then
            Tabla = cn.GetAccessTable("Select PurchDoc, Item From [Detalle de Catalogos]").Tables(0)
            For i = 0 To Tabla.Rows.Count - 1
                Dim CostCenter$
                CostCenter = SAPCn.GetCostCenter(SAP, Tabla.Rows(i).Item("PurchDoc"), Tabla.Rows(i).Item("Item"))
                Access.DoCmd.RunSQL("Update DetalleCatalogo set CostCenter = '" & CostCenter & "' Where PO = " & Tabla.Rows(i).Item("PurchDoc") & " and Item = " & Tabla.Rows(i).Item("Item"))
                'cn.ExecuteInAccess("Update DetalleCatalogo set CostCenter = '" & CostCenter & "' Where PO = " & Tabla.Rows(i).Item("PurchDoc") & " and Item = " & Tabla.Rows(i).Item("Item"))
            Next
        End If

        On Error Resume Next
        Access.DoCmd.RunSQL("DetalleCatalogos_ImportErrors")
        SAPCn.CloseSession(SAP)

        MsgBox("Done!")

    End Sub

    Private Sub Frm001_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
    End Sub

    Private Sub Frm001_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Table As New DataSet
        Dim i%

        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Try
            Table = cn.RunSentence("Select BoxLongName From SAPBox")

            If Table.Tables(0).Rows.Count > 0 Then
                i = 0
                Do While i < Table.Tables(0).Rows.Count
                    cboSAPBox.Items.Add(Table.Tables(0).Rows(i).Item(0))
                    i = i + 1
                Loop
            Else
                MsgBox("No SAP Box Found. [Frm001::Load{Add SAP Boxes to combobox}]", MsgBoxStyle.Critical)
            End If


            Me.cboTipoDescarga.Items.Add("Catálogos")
            Me.cboTipoDescarga.Items.Add("Utilities")


        Catch ex As Exception
            MsgBox("Unable to load SAP Boxes {TRY-CATCH sentence}", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub FixCells()
        With Me.dtgCatalogos
            .Columns(0).Width = 25
            .Columns(1).Width = 70
            .Columns(2).Width = 30
            .Columns(3).Width = 300
            .Columns(4).Width = 40
            .Columns(5).Width = 60
            .Columns(6).Width = 250
            .Columns(7).Width = 50
            .Columns(8).Width = 50
            .Columns(9).Width = 50
            .Columns(9).Width = 50
            .Columns(10).Width = 50
            .Columns(11).Width = 50
            .Columns(12).Width = 50
            .Columns(13).Width = 250
        End With
    End Sub

End Class