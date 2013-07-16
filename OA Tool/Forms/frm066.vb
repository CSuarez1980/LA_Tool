Imports System.Windows.Forms

Public Class frm066
    Public cn As New OAConnection.Connection
    Public DT As New DataTable

    Private Sub frm066_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")

        'DT = cn.RunSentence("Select * From vst_ZFI2_Ready_For_Release").Tables(0)
        'DG.DataSource = DT
    End Sub

    Private Sub cmdRelease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRelease.Click
        Dim Table As New DataTable
        Dim NR As DataRow

        Table.Columns.Add("Caja")
        Table.Columns.Add("Record")
        Table.Columns.Add("Status como debe quedar")
        Table.Columns.Add("Urgente o Normal solicitud?")
        Table.Columns.Add("Razón de urgencia")
        Table.Columns.Add("Comentarios ingresados en la factura")

        DG.EndEdit()

        For Each row As Windows.Forms.DataGridViewRow In DG.Rows
            If row.Cells("CK").Value Then
                NR = Table.NewRow()

                NR("Caja") = row.Cells("SAP").Value
                NR("Record") = row.Cells("Record").Value
                NR("Status como debe quedar") = row.Cells("Requested Status").Value
                NR("Urgente o Normal solicitud?") = row.Cells("Priority").Value
                NR("Razón de urgencia") = row.Cells("Description").Value
                NR("Comentarios ingresados en la factura") = "SI"

                Table.Rows.Add(NR)
            End If
        Next

        Dim XL As New MyOffice.MSExcel


        If XL.GetStatus161ReleaseFile(Table, PDFPath & "\Records161.xls") Then
            Dim Attach() As String 'variable para anexar archivos al correo
            ReDim Attach(1)

            Attach(0) = PDFPath & "\Records161.xls"
            cn.SendOutlookMail("Solicitud cambio de Status Records", Attach, "", "", "Por favor su ayuda con el cambio de status de los siguientes records.", "", False, "HTML")
        End If


        For Each row As Windows.Forms.DataGridViewRow In DG.Rows
            If row.Cells("CK").Value Then
                cn.ExecuteInServer("Update BI_ZFI2_TMPRelease Set KU_Release_Pending = 0, KU_Release_Date = {fn now()}, KU_Release_TNumber = '" & gsUsuarioPC & "' Where Record = '" & row.Cells("Record").Value & "' And SAP = '" & row.Cells("SAP").Value & "'")
            End If
        Next

        MsgBox("Done.", MsgBoxStyle.Information)

    End Sub

    Private Sub cmdSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelect.Click

        DG.EndEdit()
        For Each Row As DataGridViewRow In DG.Rows
            Row.Cells("CK").Value = cmdSelect.CheckState
        Next

    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        GetInfo()
    End Sub


    Private Sub GetInfo(Optional ByVal FiltroAvanzado As String = "")
        DG.DataSource = ""
        DG.Columns.Clear()

        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim MatGrp As New DataTable

        Dim FP As String = "" 'Filtro para las plantas
        Dim FV As String = "" 'Filtro para los vendors
        Dim FPG As String = "" ' Filtro para purch grp
        Dim FPO As String = "" ' Filtro para POrg
        Dim FM As String = "" ' Filtro para material group
        Dim row As DataRow

        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)


        '***********************************************************************
        ' Configuro el filtro para los Plantas
        '***********************************************************************
        If Plantas.Rows.Count > 0 Then
            For Each row In Plantas.Rows
                Dim Eval As String = ""

                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                    Eval = "(Plant = '" & row.Item("Valor") & "')"
                Else
                    Eval = "(Plant <> '" & row.Item("Valor") & "')"
                End If


                If FP.Length > 0 Then
                    If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                        FP = FP & " or " & Eval
                    Else
                        FP = FP & " And " & Eval
                    End If
                Else
                    FP = Eval
                End If
            Next

            FP = "(" & FP & ")"
        End If

        '***********************************************************************
        '  Configuro el filtro para los vendors
        '***********************************************************************
        If Vendors.Rows.Count > 0 Then
            For Each row In Vendors.Rows
                Dim Eval As String = ""

                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                    Eval = "([Vendor] = '" & row.Item("Valor") & "')"
                Else
                    Eval = "([Vendor] <> '" & row.Item("Valor") & "')"
                End If


                If FV.Length > 0 Then
                    If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                        FV = FV & " or " & Eval
                    Else
                        FV = FV & " And " & Eval
                    End If
                Else
                    FV = Eval
                End If
            Next

            FV = "(" & FV & ")"
        End If

        '***********************************************************************
        ' Configuro el filtro para los P. Groups
        '***********************************************************************
        If PGrp.Rows.Count > 0 Then
            For Each row In PGrp.Rows
                Dim Eval As String = ""

                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                    Eval = "([Purch Group] = '" & row.Item("Valor") & "')"
                Else
                    Eval = "([Purch Group] <> '" & row.Item("Valor") & "')"
                End If

                If FPG.Length > 0 Then
                    If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                        FPG = FPG & " or " & Eval
                    Else
                        FPG = FPG & " And " & Eval
                    End If
                Else
                    FPG = Eval
                End If
            Next

            FPG = "(" & FPG & ")"
        End If

        '***********************************************************************
        ' Configuro el filtro para los P. Orgs
        '***********************************************************************
        If POrg.Rows.Count > 0 Then
            For Each row In POrg.Rows
                Dim Eval As String = ""

                If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                    Eval = "([Purch Org] = '" & row.Item("Valor") & "')"
                Else
                    Eval = "([Purch Org] <> '" & row.Item("Valor") & "')"
                End If

                If FPO.Length > 0 Then
                    If (DBNull.Value.Equals(row.Item("Prefijo")) Or (row.Item("Prefijo") = "")) Then
                        FPO = FPO & " or " & Eval
                    Else
                        FPO = FPO & " And " & Eval
                    End If
                Else
                    FPO = Eval
                End If
            Next

            FPO = "(" & FPO & ")"
        End If



        Dim SQL As String
        Dim lsFiltro As String = ""

        Select Case giDistribution
            Case 1
                lsFiltro = "(([Import/Nac] = 'National')" & cn.getExceptions(giDistribution) & ") And "
            Case 2
                lsFiltro = "(([Import/Nac] = 'Import')" & cn.getExceptions(giDistribution) & ") And "
        End Select

        SQL = "Select * From vst_ZFI2_Ready_For_Release Where (" & lsFiltro & " SAP = '" & Me.cboSAPBox.SelectedValue.ToString & "')"

        If FiltroAvanzado.Length > 0 Then
            SQL = SQL & " And (" & FiltroAvanzado & ")"
        End If

        If FP.Length > 0 Then
            SQL = SQL & " And " & FP
        End If

        If FV.Length > 0 Then
            SQL = SQL & " And " & FV
        End If

        If FPG.Length > 0 Then
            SQL = SQL & " And " & FPG
        End If

        If FPO.Length > 0 Then
            SQL = SQL & " And " & FPO
        End If

        DT = cn.RunSentence(SQL).Tables(0)

        DG.DataSource = DT
        'DG.Columns.Insert(0, cn.AddComboRootCauses("Status", "Status", "Select ID, Description From BI_ZFI2_Status_List Order by Description", "ID", "Description"))
        'DG.Columns.Insert(0, cn.AddComboRootCauses("Request Importance", "Request Importance", "Select ID, Description From BI_ZFI2_Importance", "ID", "Description"))
        'DG.Columns.Insert(0, cn.AddComboRootCauses("Importance Reason", "Importance Reason", "Select Id, Description From BI_ZFI2_Importance_Reason", "ID", "Description"))
        'DG.Columns.Insert(0, cn.AddComboRootCauses("Request New Status", "Request New Status", "Select Status From BI_ZFI2_Release_Status_New_Code", "Status", "Status"))
        ''txtTotal.Text = "Total records found: " & DT.Rows.Count


        'Dim DGR As DataGridViewRow

        'For Each DGR In DG.Rows
        '    If Not DBNull.Value.Equals(DGR.Cells("Status ID")) Then
        '        DGR.Cells("Status").Value = DGR.Cells("Status ID").Value
        '    End If
        'Next


        'DG.Columns.Insert(0, cn.AddPictureColumn("Comment", "Comment", 25))
        'CkeckComments()

    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub
End Class