Public Class PopUp
    Dim SAPCn As New SAPConection.SAPTools
    Dim cn As New OAConnection.Connection
    Dim SAP As Object
    Dim Tabla As New DataTable
    Public SAPConfig As Boolean = cn.RunSentence("Select SAPConfig From [Users] Where TNumber = '" & cn.GetUserId & "'").Tables(0).Rows(0).Item("SAPConfig")


    Private Sub PopUp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Requisition.Text = Requi.Item("Requisition").ToString
        Item.Text = Requi.Item("Item").ToString
        Gica.Text = Requi.Item("Gica").ToString
        Material.Text = Requi.Item("Material").ToString
        Planta.Text = Requi.Item("Planta").ToString

        If (Not DBNull.Value.Equals(Requi.Item("ChangedOn"))) Then
            ChangedOn.Text = Date.Parse(Requi.Item("ChangedOn").ToString)
        Else
            ChangedOn.Text = ""
        End If

        If (Not DBNull.Value.Equals(Requi.Item("ReqDate"))) Then
            ReqDate.Text = Date.Parse(Requi.Item("ReqDate"))
        Else
            ReqDate.Text = ""
        End If

        If Not DBNull.Value.Equals(Requi.Item("DeliveryDate")) Then
            DeliveryDate.Text = Date.Parse(Requi.Item("DeliveryDate"))
        Else
            DeliveryDate.Text = ""
        End If

        If Not DBNull.Value.Equals(Requi.Item("ReleaseDate")) Then
            ReleaseDate.Text = Requi.Item("ReleaseDate")
        Else
            ReleaseDate.Text = ""
        End If

        PG.Text = Requi.Item("PG").ToString
        CreatedBy.Text = Requi.Item("CreatedBy").ToString
        Requisitioner.Text = Requi.Item("Requisitioner").ToString
        Qty.Text = Requi.Item("Qty").ToString
        UOM.Text = Requi.Item("UOM").ToString
        GRProcTime.Text = Requi.Item("GRProcTime").ToString
        DesiredVendor.Text = Requi.Item("DesiredVendor").ToString
        FixedVendor.Text = Requi.Item("FixedVendor").ToString
        POrg.Text = Requi.Item("POrg").ToString
        OA.Text = Requi.Item("OA").ToString
        OAItem.Text = Requi.Item("OAItem").ToString
        Inforecord.Text = Requi.Item("Inforecord").ToString
        PDT.Text = Requi.Item("PDT").ToString
        OA_Contrato.Text = Requi.Item("OA_Contrato").ToString
        Item_Contrato.Text = Requi.Item("Item_Contrato").ToString

        If (Not DBNull.Value.Equals(Requi.Item("OAInicio"))) Then
            OAInicio.Text = Date.Parse(Requi.Item("OAInicio").ToString)
        Else
            OAInicio.Text = ""
        End If

        If (Not DBNull.Value.Equals(Requi.Item("OAFin"))) Then
            OAFin.Text = Date.Parse(Requi.Item("OAFin").ToString)
        Else
            OAFin.Text = ""
        End If

        PG_Contrato.Text = Requi.Item("PG_Contrato").ToString
        PO_Contrato.Text = Requi.Item("PO_contrato").ToString
        PDT_Contrato.Text = Requi.Item("PDT_Contrato").ToString
        PDT_MasterData.Text = Requi.Item("PDT_MasterData").ToString

        If (Not DBNull.Value.Equals(Requi.Item("AutoPO_MasterData"))) And (Requi.Item("AutoPO_MasterData").ToString <> "") Then
            AutoPO_MasterData.Checked = True
        Else
            AutoPO_MasterData.Checked = False
        End If

        If (Not DBNull.Value.Equals(Requi.Item("SourceList_MasterData"))) And (Requi.Item("SourceList_MasterData").ToString <> "") Then
            SourceList_MasterData.Checked = True
        Else
            SourceList_MasterData.Checked = False
        End If

        If Not DBNull.Value.Equals(Requi.Item("SourceListOA_Start")) Then
            SourceListOA_Start.Text = Date.Parse(Requi.Item("SourceListOA_Start").ToString)
        Else
            SourceListOA_Start.Text = ""
        End If

        If Not DBNull.Value.Equals(Requi.Item("SourceListOA_End")) Then
            SourceListOA_End.Text = Date.Parse(Requi.Item("SourceListOA_End").ToString)
        Else
            SourceListOA_End.Text = ""
        End If


        EvaluarRequis()

    End Sub


    Private Sub EvaluarRequis()
        Dim Fail$
        Dim InOA As Boolean
        Dim OASourceList As Boolean
        Dim Where$

        Fail = ""
        Where = ""
        InOA = False
        OASourceList = False

        If DBNull.Value.Equals(Requi.Item("OA_Contrato")) Or (Requi.Item("OA_Contrato").ToString = "") Then
            Me.OA_Contrato.BackColor = Drawing.Color.Red
            Me.Item_Contrato.BackColor = Drawing.Color.Red
            Fail = "1" 'Material no está en contrato
            Where = "Where ID = '1'"
        Else
            InOA = True
        End If

        'Si el material tiene contrato
        If InOA And ((DBNull.Value.Equals(Requi.Item("SourceListOA_Start"))) Or Requi.Item("SourceListOA_Start").ToString = "") Then
            Fail = Fail & "2" 'Material sin el source list del contrato
            Me.SourceListOA_Start.BackColor = Drawing.Color.Red
            Me.SourceListOA_End.BackColor = Drawing.Color.Red

            If Where.Length > 0 Then
                Where = Where & " or ID = '2'"
            Else
                Where = "Where ID = '2'"
            End If
        Else
            OASourceList = True
        End If

        If InOA Then
            If (Requi.Item("ReqDate") < Requi.Item("OAInicio")) Or (Requi.Item("ReqDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "0" 'La requisición esta fuera de la validez del contrato
                Me.ReqDate.BackColor = Drawing.Color.Red
                Me.OAInicio.BackColor = Drawing.Color.Red
                Me.OAFin.BackColor = Drawing.Color.Red

                If Where.Length > 0 Then
                    Where = Where & " or ID = '0'"
                Else
                    Where = "Where ID = '0'"
                End If

            End If
        End If

        If InOA And OASourceList Then
            If (Requi.Item("ReqDate") < Requi.Item("SourceListOA_Start")) Or (Requi.Item("ReqDate") > Requi.Item("SourceListOA_End")) Then
                Fail = Fail & "3" 'La requisición esta fuera de la validez del source list
                Me.ReqDate.BackColor = Drawing.Color.Red
                Me.SourceListOA_End.BackColor = Drawing.Color.Red
                Me.SourceListOA_Start.BackColor = Drawing.Color.Red


                If Where.Length > 0 Then
                    Where = Where & " or ID = '3'"
                Else
                    Where = "Where ID = '3'"
                End If
            End If
        End If


        If InOA Then
            If (Requi.Item("DeliveryDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "4" 'La fecha de entrega de la requi es posterior a la fecha de vencimiento del contrato
                Me.OAFin.BackColor = Drawing.Color.Red
                Me.DeliveryDate.BackColor = Drawing.Color.Red

                If Where.Length > 0 Then
                    Where = Where & " or ID = '4'"
                Else
                    Where = "Where ID = '4'"
                End If
            End If
        End If


        If InOA Then
            If DBNull.Value.Equals(Requi.Item("AutoPO_MasterData")) Or (Requi.Item("AutoPO_MasterData").ToString = "") Then
                Fail = Fail & "5" 'Material sin el check de Auto PO de master data
                Me.AutoPO_MasterData.BackColor = Drawing.Color.Red

                If Where.Length > 0 Then
                    Where = Where & " or ID = '5'"
                Else
                    Where = "Where ID = '5'"
                End If
            End If
        End If

        If InOA Then
            If DBNull.Value.Equals(Requi.Item("SourceList_MasterData")) Or (Requi.Item("SourceList_MasterData").ToString = "") Then
                Fail = Fail & "6" 'Material sin el check de source list de master data
                Me.SourceList_MasterData.BackColor = Drawing.Color.Red

                If Where.Length > 0 Then
                    Where = Where & " or ID = '6'"
                Else
                    Where = "Where ID = '6'"
                End If
            End If
        End If


        If (DBNull.Value.Equals(Requi.Item("OA")) Or (Requi.Item("OA") = "")) And Not DBNull.Value.Equals(Requi.Item("OA_Contrato")) Then
            Fail = Fail & "8" 'Material no tiene Assing & Process y está en contrato

            If Where.Length > 0 Then
                Where = Where & " or ID = '8'"
            Else
                Where = "Where ID = '8'"
            End If
        End If


        If InOA Then
            If Trim(Requi.Item("PG_Contrato")) = "081" Then
                Fail = Fail & "9" 'Materiel en contrato de Importados

                If Where.Length > 0 Then
                    Where = Where & " or ID = '9'"
                Else
                    Where = "Where ID = '9'"
                End If
            End If
        End If

        If InOA And OASourceList Then
            If (Requi.Item("DeliveryDate") > Requi.Item("SourceListOA_End")) Then
                Fail = Fail & "B" 'La fecha de entrega de la requi es posterior a la fecha de vencimiento del source list
                Me.SourceListOA_End.BackColor = Drawing.Color.Red
                Me.DeliveryDate.BackColor = Drawing.Color.Red

                If Where.Length > 0 Then
                    Where = Where & " or ID = 'B'"
                Else
                    Where = "Where ID = 'B'"
                End If
            End If
        End If

        If Fail.Length > 0 Then
            Me.lblResultado.Text = "Esta Requisición no corre en automático."
            Me.lblResultado.ForeColor = Drawing.Color.Red
        Else
            Me.lblResultado.Text = "Esta Requisición debería correr en automático."
            Me.lblResultado.ForeColor = Drawing.Color.Green
        End If

        If Where.Length > 0 Then
            Dim cn As New OAConnection.Connection

            Tabla = cn.RunSentence("Select * From CrashRequis " & Where).Tables(0)

            Me.dtgContratos.DataSource = Tabla
        Else
            Me.dtgContratos.DataSource = ""
        End If

    End Sub

    Private Sub cmdFix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdFix.Click
        ' Dim log As New Login
        Dim Fix As Boolean = False

        'log.ShowDialog()

        Dim c As New SAPCOM.SAPConnector
        Dim u As Object = c.GetConnectionData("L7P", cn.GetUserId, "LAT")

        gUser = cn.GetUserId
        gPwr = u.password

        If gUser.Length > 0 And gPwr.Length > 0 Then
            SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", cn.GetUserId, u.password, SAPConfig)
            'SAP = SAPCn.GetConnectionToSAP("L7A TS Acceptance", Trim(gUser), Trim(gPwr))
            Dim row As DataRow

            For Each row In Tabla.Rows
                If UCase(Trim(row.Item("Reparable"))) = "S" Then
                    Select Case Trim(row.Item("ID"))
                        Case "0", "4"
                            'Si la requi esta fuera de la validez del contrato o la fecha de entrega de la requi es porterior a la fecha
                            'de vencimiento del contrato

                            Dim FechaInicio As Date
                            Dim FechaFinal As Date

                            FechaInicio = Date.Parse(Requi.Item("ReqDate")) 'Fecha de creacion de la requi
                            FechaInicio = FechaInicio.AddDays(-1) 'Seteo de la fecha de inicio del source list un día antes de la fecha de creación de la requi

                            FechaFinal = Date.Parse(Requi.Item("DeliveryDate")) 'Fecha de entraga de la requi
                            FechaFinal = FechaFinal.AddDays(1) 'Seteo de la fecha final a un día despues de la fecha de entrega de la requi

                            'Verifico el día menor: OA.Inicio Vs Requi.ReqDate
                            If FechaInicio > Date.Parse(Requi.Item("OAInicio")) Then
                                FechaInicio = Date.Parse(Requi.Item("OAInicio"))
                            End If

                            'Verifico el día mayor: OA.Inicio Vs Requi.ReqDate
                            If FechaFinal < Date.Parse(Requi.Item("OAFin")) Then
                                FechaFinal = Date.Parse(Requi.Item("OAFin"))
                            End If

                            SAPCn.FixValidityOfOA(SAP, Requi.Item("OA_Contrato"), FechaInicio, FechaFinal)

                        Case "2"
                            SAPCn.FixSourceListOA(SAP, Requi.Item("Gica"), Requi.Item("Planta"), Requi.Item("OAInicio"), Requi.Item("OAFin"), Requi.Item("OA_Vendor"), Requi.Item("PO_Contrato"), Requi.Item("OA_Contrato"), Requi.Item("Item_Contrato"))

                        Case "3", "B"
                            'La requi se encuentra fuera de la validez del source list del contrato
                            Dim FechaInicio As Date
                            Dim FechaFinal As Date

                            FechaInicio = Date.Parse(Requi.Item("ReqDate")) 'Fecha de creacion de la requi
                            FechaInicio = FechaInicio.AddDays(-1) 'Seteo de la fecha de inicio del source list un día antes de la fecha de creación de la requi

                            FechaFinal = Date.Parse(Requi.Item("DeliveryDate")) 'Fecha de entraga de la requi
                            FechaFinal = FechaFinal.AddDays(1) 'Seteo de la fecha final a un día despues de la fecha de entrega de la requi

                            'Verifico el día menor: OA.Inicio Vs Requi.ReqDate
                            If FechaInicio > Date.Parse(Requi.Item("OAInicio")) Then
                                FechaInicio = Date.Parse(Requi.Item("OAInicio"))
                            End If

                            'Verifico el día mayor: OA.Inicio Vs Requi.ReqDate
                            If FechaFinal < Date.Parse(Requi.Item("OAFin")) Then
                                FechaFinal = Date.Parse(Requi.Item("OAFin"))
                            End If

                            SAPCn.FixSourceListOA(SAP, Requi.Item("Gica"), Requi.Item("Planta"), FechaInicio, FechaFinal, Requi.Item("OA_Vendor"), Requi.Item("PO_Contrato"), Requi.Item("OA_Contrato"), Requi.Item("Item_Contrato"))

                        Case "5", "6"
                            'Sin los check's de Auto PO y Source List de la master data y en contrato que no sea de importados.
                            If (row.Item("ID") = 5 Or row.Item("ID") = 6) And Requi.Item("PG_Contrato") <> "081" Then
                                SAPCn.FixMasterDataChecks(SAP, Requi.Item("Gica"), Requi.Item("Planta"))
                            End If


                        Case "8"
                            'Si no se ha asignado el contrato realisa el Push 
                            If Requi.Item("PG_Contrato") <> "081" Then
                                SAPCn.PushRequi(Requi.Item("Requisition"))
                            End If

                    End Select



                    cn.AddRequiTracking(Requi.Item("Requisition"), Requi.Item("Item"), row.Item("ID"))

                End If
            Next row

            'MsgBox("Fixed!")
            SAPCn.CloseSession(SAP)
        End If
    End Sub

    Private Sub SourceListOA_Start_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SourceListOA_Start.TextChanged

    End Sub
End Class