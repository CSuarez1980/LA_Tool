Imports SAPCOM.RepairsLevels

Public Class frm046

    Dim POs As New DataTable
    Dim cn As New OAConnection.Connection
    Dim SAPBox As String = ""
    Dim FInicio As String
    Dim FFinal As String
    Dim VariantID As String


    Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
        Me.PB.Style = Windows.Forms.ProgressBarStyle.Marquee
        lblStatus.Text = "Downloading report... Please be patient"
        imgWorking.Visible = True
        SAPBox = cboSAPBox.SelectedValue.ToString

        VariantID = Me.cboVariantes.SelectedValue.ToString
        FInicio = Me.dtpStart.Text
        FFinal = Me.dtpEnd.Text
        Me.dtgPO.Columns.Clear()

        BG.RunWorkerAsync()


        'Dim Rep As New SAPCOM.POs_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        'Dim i As Integer = 0
        'Dim Plantas As New DataTable
        'Dim Vendors As New DataTable
        'Dim PGrp As New DataTable
        'Dim POrg As New DataTable
        'Dim MatGrp As New DataTable

        'Me.dtgPO.Columns.Clear()

        'Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        'Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        'PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        'POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        'MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)

        'If Plantas.Rows.Count > 0 Then
        '    For i = 0 To Plantas.Rows.Count - 1
        '        If DBNull.Value.Equals(Plantas.Rows(i).Item("Prefijo")) Then
        '            Rep.IncludePlant("")
        '            Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
        '        Else
        '            If Plantas.Rows(i).Item("Prefijo") = "" Then
        '                Rep.IncludePlant("")
        '                Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
        '            Else
        '                Rep.ExcludePlant(Plantas.Rows(i).Item("Valor"))
        '            End If
        '        End If
        '    Next
        'End If

        'If PGrp.Rows.Count > 0 Then
        '    For i = 0 To PGrp.Rows.Count - 1
        '        If DBNull.Value.Equals(PGrp.Rows(i).Item("Prefijo")) Then
        '            Rep.IncludePurchGroup("")
        '            Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
        '        Else
        '            If PGrp.Rows(i).Item("Prefijo") = "" Then
        '                Rep.IncludePurchGroup("")
        '                Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
        '            Else
        '                Rep.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))
        '            End If
        '        End If
        '    Next
        'End If

        'If POrg.Rows.Count > 0 Then
        '    For i = 0 To POrg.Rows.Count - 1
        '        If DBNull.Value.Equals(POrg.Rows(i).Item("Prefijo")) Then
        '            Rep.IncludePurchOrg("")
        '            Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
        '        Else
        '            If POrg.Rows(i).Item("Prefijo") = "" Then
        '                Rep.IncludePurchOrg("")
        '                Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
        '            Else
        '                Rep.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))
        '            End If
        '        End If
        '    Next
        'End If

        'If Vendors.Rows.Count > 0 Then
        '    For i = 0 To Vendors.Rows.Count - 1
        '        If DBNull.Value.Equals(Vendors.Rows(i).Item("Prefijo")) Then
        '            Rep.IncludeVendor("")
        '            Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
        '        Else
        '            If Vendors.Rows(i).Item("Prefijo") = "" Then
        '                Rep.IncludeVendor("")
        '                Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
        '            Else
        '                Rep.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
        '            End If
        '        End If
        '    Next
        'End If


        'If MatGrp.Rows.Count > 0 Then
        '    For i = 0 To MatGrp.Rows.Count - 1
        '        If DBNull.Value.Equals(MatGrp.Rows(i).Item("Prefijo")) Then
        '            Rep.IncludeMatGroup("")
        '            Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
        '        Else
        '            If MatGrp.Rows(i).Item("Prefijo") = "" Then
        '                Rep.IncludeMatGroup("")
        '                Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
        '            Else
        '                Rep.ExcludeMatGroup(MatGrp.Rows(i).Item("Valor"))
        '            End If
        '        End If
        '    Next
        'End If

        'Rep.IncludeDocsDatedFromTo(Me.dtpStart.Text, Me.dtpEnd.Text)


        'Rep.RepairsLevel = IncludeRepairs
        ''Rep.IncludeDocument("4500830537")
        ''Rep.IncludeDelivDates = True

        'Rep.Execute()

        'If Rep.Success Then
        '    If Rep.ErrMessage = Nothing Then
        '        POs = Rep.Data

        '        '*****************************************************
        '        '  13 de Enero 2010:
        '        '
        '        '  Este código fue agregado para evitar que en G4P 
        '        '  se presentaran problemas con columna adicionales
        '        '  exclusibas de en esta caja
        '        '*****************************************************

        '        If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
        '            POs.Columns.Remove("EKKO-WAERS-0219")
        '        End If

        '        If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
        '            POs.Columns.Remove("EKPO-ZWERT")
        '        End If

        '        If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
        '            POs.Columns.Remove("EKKO-WAERS-0218")
        '        End If

        '        If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
        '            POs.Columns.Remove("EKKO-WAERS-0220")
        '        End If

        '        If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
        '            POs.Columns.Remove("EKKO-MEMORYTYPE")
        '        End If

        '        '*****************************************************
        '        '*****************************************************

        '        'Me.dtgRequisiciones.DataSource = POs

        '        POs = Rep.Data
        '        'POs.Columns.Add("Usuario")
        '        'POs.Columns.Add("SAPBox")
        '        POs.Columns.Add("Status")
        '        POs.Columns.Add("Confirm")


        '        '*****************************************************************
        '        '*****************************************************************
        '        Dim ETN As New DataColumn
        '        Dim ESB As New DataColumn

        '        'Columna del Usuario que descarga el reporte
        '        ETN.ColumnName = "Usuario"
        '        ETN.Caption = "Usuario"
        '        ETN.DefaultValue = gsUsuarioPC


        '        'Columna de la caja
        '        ESB.DefaultValue = Me.cboSAPBox.SelectedValue.ToString
        '        ESB.ColumnName = "SAPBox"
        '        ESB.Caption = "SAPBox"

        '        POs.Columns.Add(ETN)
        '        POs.Columns.Add(ESB)
        '        '*****************************************************************
        '        '*****************************************************************

        '        cn.ExecuteInServer("Delete From tmpPOReport Where Usuario = '" & gsUsuarioPC & "'")

        '        cn.AppendTableToSqlServer("tmpPOReport", POs)

        '        Dim MinPO = (From C In POs.AsEnumerable() _
        '                Select C.Item("Doc Number")).Min

        '        Dim MaxPO = (From C In POs.AsEnumerable() _
        '                     Select C.Item("Doc Number")).Max


        '        Dim R2 As New SAPCOM.EKPO_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, "LAT")

        '        R2.DeletionIndicator = False

        '        R2.DocumentFrom = MinPO
        '        R2.DocumentTo = MaxPO

        '        R2.AddCustomField("BANFN", "Requisition")
        '        R2.AddCustomField("BNFPO", "Req Item")

        '        R2.Execute()


        '        If R2.Success Then

        '            'If R2.Data.Columns.IndexOf("Client") <> -1 Then
        '            '    R2.Data.Columns.Remove("Client")
        '            'End If

        '            If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
        '                R2.Data.Columns.Remove("Short Text")
        '            End If

        '            If R2.Data.Columns.IndexOf("Material") <> -1 Then
        '                R2.Data.Columns.Remove("Material")
        '            End If

        '            If R2.Data.Columns.IndexOf("Plant") <> -1 Then
        '                R2.Data.Columns.Remove("Plant")
        '            End If

        '            If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
        '                R2.Data.Columns.Remove("Inforecord")
        '            End If

        '            If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
        '                R2.Data.Columns.Remove("Quantity")
        '            End If

        '            If R2.Data.Columns.IndexOf("UOM") <> -1 Then
        '                R2.Data.Columns.Remove("UOM")
        '            End If

        '            If R2.Data.Columns.IndexOf("Price") <> -1 Then
        '                R2.Data.Columns.Remove("Price")
        '            End If

        '            If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
        '                R2.Data.Columns.Remove("Tax Code")
        '            End If

        '            If R2.Data.Columns.IndexOf("PDT") <> -1 Then
        '                R2.Data.Columns.Remove("PDT")
        '            End If

        '            If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
        '                R2.Data.Columns.Remove("Mat Group")
        '            End If

        '            If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
        '                R2.Data.Columns.Remove("Tracking Fld")
        '            End If


        '            If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
        '                R2.Data.Columns.Remove("Price Unit")
        '            End If



        '            cn.ExecuteInServer("Delete From tmpPOReport_Requis Where [User] = '" & gsUsuarioPC & "'")

        '            '*****************************************************************
        '            '*****************************************************************
        '            Dim ETN2 As New DataColumn
        '            Dim ESB2 As New DataColumn

        '            'Columna del Usuario que descarga el reporte
        '            ETN2.ColumnName = "Usuario"
        '            ETN2.Caption = "Usuario"
        '            ETN2.DefaultValue = gsUsuarioPC


        '            'Columna de la caja
        '            ESB2.DefaultValue = Me.cboSAPBox.SelectedValue.ToString
        '            ESB2.ColumnName = "SAPBox"
        '            ESB2.Caption = "SAPBox"

        '            R2.Data.Columns.Add(ETN2)
        '            R2.Data.Columns.Add(ESB2)

        '            '*****************************************************************
        '            '*****************************************************************


        '            cn.AppendTableToSqlServer("tmpPOReport_Requis", R2.Data)

        '        Else
        '            MsgBox("Could not retreave requisitions:" & Chr(13) & Chr(13) & R2.ErrMessage, MsgBoxStyle.Exclamation)
        '        End If

        '        POs = cn.RunSentence("Select * From vst_POReport").Tables(0)

        '        Me.dtgPO.DataSource = POs

        '    Else
        '        MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
        '    End If
        'Else
        '    MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)

        'End If

    End Sub

    Private Sub frm046_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim Row As Double = 0
        Dim Col As Double = 0
        Dim Tabla As New DataTable

        'Tabla.ReadXml("C:\x.xml")

        cn.ExportDataTableToXL(POs)

        'For Row = 0 To POs.Rows.Count
        '    For Col = 0 To POs.Columns.Count

        '    Next
        'Next

        'cn.ExportDataTableToXL(Me.dtgPO.DataSource)
        'cn.AppendTableToSqlServer("POReport", POs)
        'MsgBox("Done.")
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click




        'cn.AppendTableToSqlServer("tmpRepoMaria", POs)
        'SFD.Filter = "XML Files|*.xml"
        'SFD.Title = "Save to:"
        'SFD.FileName = "C:\PO.xml"
        'SFD.ShowDialog()

        'If SFD.FileName <> "" Then
        '    If Len(Dir(SFD.FileName)) > 0 Then
        '        If MsgBox("File already exist." & Chr(13) & Chr(13) & "  Do you want to replace it?", vbQuestion + vbYesNo, "Replace File") = vbYes Then
        '            Kill(SFD.FileName)
        '        Else
        '            MsgBox("File not exported.", vbExclamation, "User cancel")
        '            Exit Sub
        '        End If
        '    End If

        '    POs.TableName = "POs"
        '    POs.WriteXml(SFD.FileName)

        'Else
        '    MsgBox("Please type file name.", MsgBoxStyle.Exclamation)
        'End If
    End Sub

    Private Sub BG_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BG.DoWork
        Dim Rep As New SAPCOM.POs_Report(SAPBox, gsUsuarioPC, AppId)
        Dim i As Integer = 0
        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim MatGrp As New DataTable



        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & VariantID & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & VariantID & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & VariantID & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & VariantID & ")").Tables(0)
        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & VariantID & ")").Tables(0)

        If Plantas.Rows.Count > 0 Then
            For i = 0 To Plantas.Rows.Count - 1
                If DBNull.Value.Equals(Plantas.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePlant("")
                    Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
                Else
                    If Plantas.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePlant("")
                        Rep.IncludePlant(Plantas.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePlant(Plantas.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If PGrp.Rows.Count > 0 Then
            For i = 0 To PGrp.Rows.Count - 1
                If DBNull.Value.Equals(PGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchGroup("")
                    Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                Else
                    If PGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchGroup("")
                        Rep.IncludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchGroup(PGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If POrg.Rows.Count > 0 Then
            For i = 0 To POrg.Rows.Count - 1
                If DBNull.Value.Equals(POrg.Rows(i).Item("Prefijo")) Then
                    Rep.IncludePurchOrg("")
                    Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                Else
                    If POrg.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludePurchOrg("")
                        Rep.IncludePurchOrg(POrg.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludePurchOrg(POrg.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        If Vendors.Rows.Count > 0 Then
            For i = 0 To Vendors.Rows.Count - 1
                If DBNull.Value.Equals(Vendors.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeVendor("")
                    Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                Else
                    If Vendors.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeVendor("")
                        Rep.IncludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    Else
                        Rep.ExcludeVendor(Vendors.Rows(i).Item("Valor").ToString.PadLeft(10, "0"))
                    End If
                End If
            Next
        End If


        If MatGrp.Rows.Count > 0 Then
            For i = 0 To MatGrp.Rows.Count - 1
                If DBNull.Value.Equals(MatGrp.Rows(i).Item("Prefijo")) Then
                    Rep.IncludeMatGroup("")
                    Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                Else
                    If MatGrp.Rows(i).Item("Prefijo") = "" Then
                        Rep.IncludeMatGroup("")
                        Rep.IncludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    Else
                        Rep.ExcludeMatGroup(MatGrp.Rows(i).Item("Valor"))
                    End If
                End If
            Next
        End If

        Rep.IncludeDocsDatedFromTo(FInicio, FFinal)

        Rep.RepairsLevel = IncludeRepairs
       
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                POs = Rep.Data

                '*****************************************************
                '  13 de Enero 2010:
                '
                '  Este código fue agregado para evitar que en G4P 
                '  se presentaran problemas con columna adicionales
                '  exclusibas de en esta caja
                '*****************************************************

                If POs.Columns.IndexOf("EKKO-WAERS-0219") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0219")
                End If

                If POs.Columns.IndexOf("EKPO-ZWERT") <> -1 Then
                    POs.Columns.Remove("EKPO-ZWERT")
                End If

                If POs.Columns.IndexOf("EKKO-WAERS-0218") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0218")
                End If

                If POs.Columns.IndexOf("EKKO-WAERS-0220") <> -1 Then
                    POs.Columns.Remove("EKKO-WAERS-0220")
                End If

                If POs.Columns.IndexOf("EKKO-MEMORYTYPE") <> -1 Then
                    POs.Columns.Remove("EKKO-MEMORYTYPE")
                End If

                '*****************************************************
                '*****************************************************

                'Me.dtgRequisiciones.DataSource = POs

                'POs = Rep.Data
                'POs.Columns.Add("Usuario")
                'POs.Columns.Add("SAPBox")
                POs.Columns.Add("Status")
                POs.Columns.Add("Confirm")


                '*****************************************************************
                '*****************************************************************
                Dim ETN As New DataColumn
                Dim ESB As New DataColumn

                'Columna del Usuario que descarga el reporte
                ETN.ColumnName = "Usuario"
                ETN.Caption = "Usuario"
                ETN.DefaultValue = gsUsuarioPC


                'Columna de la caja
                ESB.DefaultValue = SAPBox
                ESB.ColumnName = "SAPBox"
                ESB.Caption = "SAPBox"

                POs.Columns.Add(ETN)
                POs.Columns.Add(ESB)
                '*****************************************************************
                '*****************************************************************

                cn.ExecuteInServer("Delete From tmpPOReport Where Usuario = '" & gsUsuarioPC & "'")
                cn.ExecuteInServer("Delete From tmpPOReport_Requis Where [User] = '" & gsUsuarioPC & "'")
                cn.AppendTableToSqlServer("tmpPOReport", POs)

                Dim MinPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Min

                Dim MaxPO = (From C In POs.AsEnumerable() _
                             Where C.Item("Doc Number") > "450000000" _
                             Select C.Item("Doc Number")).Max


                Dim R2 As New SAPCOM.EKPO_Report(SAPBox, gsUsuarioPC, "LAT")

                R2.DeletionIndicator = False

                R2.DocumentFrom = MinPO
                R2.DocumentTo = MaxPO

                R2.AddCustomField("BANFN", "Requisition")
                R2.AddCustomField("BNFPO", "Req Item")
                R2.AddCustomField("IDNLF", "IDNLF")

                R2.Execute()


                If R2.Success Then

                    'If R2.Data.Columns.IndexOf("Client") <> -1 Then
                    '    R2.Data.Columns.Remove("Client")
                    'End If

                    If R2.Data.Columns.IndexOf("Short Text") <> -1 Then
                        R2.Data.Columns.Remove("Short Text")
                    End If

                    If R2.Data.Columns.IndexOf("Material") <> -1 Then
                        R2.Data.Columns.Remove("Material")
                    End If

                    If R2.Data.Columns.IndexOf("Plant") <> -1 Then
                        R2.Data.Columns.Remove("Plant")
                    End If

                    If R2.Data.Columns.IndexOf("Inforecord") <> -1 Then
                        R2.Data.Columns.Remove("Inforecord")
                    End If

                    If R2.Data.Columns.IndexOf("Quantity") <> -1 Then
                        R2.Data.Columns.Remove("Quantity")
                    End If

                    If R2.Data.Columns.IndexOf("UOM") <> -1 Then
                        R2.Data.Columns.Remove("UOM")
                    End If

                    If R2.Data.Columns.IndexOf("Price") <> -1 Then
                        R2.Data.Columns.Remove("Price")
                    End If

                    If R2.Data.Columns.IndexOf("Tax Code") <> -1 Then
                        R2.Data.Columns.Remove("Tax Code")
                    End If

                    If R2.Data.Columns.IndexOf("PDT") <> -1 Then
                        R2.Data.Columns.Remove("PDT")
                    End If

                    If R2.Data.Columns.IndexOf("Mat Group") <> -1 Then
                        R2.Data.Columns.Remove("Mat Group")
                    End If

                    If R2.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                        R2.Data.Columns.Remove("Tracking Fld")
                    End If


                    If R2.Data.Columns.IndexOf("Price Unit") <> -1 Then
                        R2.Data.Columns.Remove("Price Unit")
                    End If


                    '*****************************************************************
                    '*****************************************************************
                    Dim ETN2 As New DataColumn
                    Dim ESB2 As New DataColumn

                    'Columna del Usuario que descarga el reporte
                    ETN2.ColumnName = "Usuario"
                    ETN2.Caption = "Usuario"
                    ETN2.DefaultValue = gsUsuarioPC


                    'Columna de la caja
                    ESB2.DefaultValue = SAPBox
                    ESB2.ColumnName = "SAPBox"
                    ESB2.Caption = "SAPBox"

                    R2.Data.Columns.Add(ETN2)
                    R2.Data.Columns.Add(ESB2)

                    '*****************************************************************
                    '*****************************************************************


                    cn.AppendTableToSqlServer("tmpPOReport_Requis", R2.Data)

                    '*****************************************************************
                    '*****************************************************************
                    Dim MinCT = (From C In POs.AsEnumerable() _
                            Where C.Item("Doc Number") < "450000000" _
                            Select C.Item("Doc Number")).Min

                    Dim MaxCT = (From C In POs.AsEnumerable() _
                                 Where C.Item("Doc Number") < "450000000" _
                                 Select C.Item("Doc Number")).Max



                    If Not MinCT Is Nothing AndAlso Not MaxCT Is Nothing Then
                        Dim R3 As New SAPCOM.EKPO_Report(SAPBox, gsUsuarioPC, "LAT")

                        R3.DeletionIndicator = False
                        R3.DocumentFrom = MinCT
                        R3.DocumentTo = MaxCT

                        R3.AddCustomField("BANFN", "Requisition")
                        R3.AddCustomField("BNFPO", "Req Item")
                        R3.AddCustomField("IDNLF", "IDNLF")

                        R3.Execute()

                        If R3.Success Then
                            If R3.Data.Columns.IndexOf("Short Text") <> -1 Then
                                R3.Data.Columns.Remove("Short Text")
                            End If
                            If R3.Data.Columns.IndexOf("Material") <> -1 Then
                                R3.Data.Columns.Remove("Material")
                            End If
                            If R3.Data.Columns.IndexOf("Plant") <> -1 Then
                                R3.Data.Columns.Remove("Plant")
                            End If
                            If R3.Data.Columns.IndexOf("Inforecord") <> -1 Then
                                R3.Data.Columns.Remove("Inforecord")
                            End If
                            If R3.Data.Columns.IndexOf("Quantity") <> -1 Then
                                R3.Data.Columns.Remove("Quantity")
                            End If
                            If R3.Data.Columns.IndexOf("UOM") <> -1 Then
                                R3.Data.Columns.Remove("UOM")
                            End If
                            If R3.Data.Columns.IndexOf("Price") <> -1 Then
                                R3.Data.Columns.Remove("Price")
                            End If
                            If R3.Data.Columns.IndexOf("Tax Code") <> -1 Then
                                R3.Data.Columns.Remove("Tax Code")
                            End If
                            If R3.Data.Columns.IndexOf("PDT") <> -1 Then
                                R3.Data.Columns.Remove("PDT")
                            End If
                            If R3.Data.Columns.IndexOf("Mat Group") <> -1 Then
                                R3.Data.Columns.Remove("Mat Group")
                            End If
                            If R3.Data.Columns.IndexOf("Tracking Fld") <> -1 Then
                                R3.Data.Columns.Remove("Tracking Fld")
                            End If
                            If R3.Data.Columns.IndexOf("Price Unit") <> -1 Then
                                R3.Data.Columns.Remove("Price Unit")
                            End If
                            Dim ETN3 As New DataColumn
                            Dim ESB3 As New DataColumn

                            ETN3.ColumnName = "Usuario"
                            ETN3.Caption = "Usuario"
                            ETN3.DefaultValue = gsUsuarioPC

                            ESB3.DefaultValue = SAPBox
                            ESB3.ColumnName = "SAPBox"
                            ESB3.Caption = "SAPBox"

                            R3.Data.Columns.Add(ETN3)
                            R3.Data.Columns.Add(ESB3)

                            cn.AppendTableToSqlServer("tmpPOReport_Requis", R3.Data)
                        Else
                            MsgBox("Could not retreave Catalog ID:" & Chr(13) & Chr(13) & R2.ErrMessage, MsgBoxStyle.Exclamation)
                        End If
                    End If
                End If
                POs = cn.RunSentence("Select * From vst_POReport Where Usuario = '" & gsUsuarioPC & "'").Tables(0)

            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            End If
        Else
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)

        End If
    End Sub

    Private Sub BG_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BG.RunWorkerCompleted
        PB.Style = Windows.Forms.ProgressBarStyle.Continuous
        imgWorking.Visible = False
        lblStatus.Text = "Purchase Order report: Finished."
        Me.dtgPO.DataSource = POs
    End Sub
End Class