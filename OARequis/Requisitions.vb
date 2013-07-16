Public Class Requisition
    Private Function ReturnRequisitionStatus(ByVal Requisition$, ByVal Item$) As DataRow
        Dim Tabla As New DataTable
        Dim cn As New OAConnection.Connection

        Tabla = cn.RunSentence("Select * from vstRequis Where Requisition = " & Requisition & " and Item = " & Item).Tables(0)

        If Tabla.Rows.Count > 0 Then
            Return Tabla.Rows(0)
        Else
            Dim xRow As DataRow

            xRow = Tabla.NewRow()
            Return xRow
        End If


    End Function

    ''' <summary>
    ''' Abre una ventana con la información de la requisición y su status para que corra en automatico.
    ''' </summary>
    ''' <param name="Requisition">Número de la requisición en L7P </param>
    ''' <param name="Item">Número del ítem de la requisición</param>
    Public Sub TestRequisition(ByVal Requisition$, ByVal Item$)
        If DBNull.Value.Equals(Requisition) Then
            MsgBox("El no se ha ingresado el número de requisición", MsgBoxStyle.Information)
            Exit Sub
        End If

        If DBNull.Value.Equals(Item) Then
            MsgBox("El no se ha ingresado el número de ítem de la requisición", MsgBoxStyle.Information)
            Exit Sub
        End If

        Requi = Me.ReturnRequisitionStatus(Requisition, Item)

        If DBNull.Value.Equals(Requi.Item("Requisition")) Then
            MsgBox("No se ha encontrado la requisicion: " & Requisition & " Item: " & Item & vbCr & vbCr & "Por favor verifique que la requi sea de L7P y sea de un material gicado.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim Form As New PopUp
        Form.Show()

    End Sub

    ''' <summary>
    ''' Retorna un valor verdadero si la requisición corre en automatico y un valor falso si no
    ''' </summary>
    ''' <param name="Requisition">Número de la requisición en L7P </param>
    ''' <param name="Item">Número del ítem de la requisición</param>
    Public Function ReturnStatus(ByVal Requisition$, ByVal Item$) As Boolean
        Dim Fail$
        Dim InOA As Boolean
        Dim OASourceList As Boolean
        Dim Where$


        Fail = ""
        Where = ""
        InOA = False
        OASourceList = False

        Requi = Me.ReturnRequisitionStatus(Requisition, Item)

        'If DBNull.Value.Equals(Requi) Then
        '    Return False
        'End If
        'Verifica que el requi se encuentra en la base de datos

        If DBNull.Value.Equals(Requi.Item("Requisition")) Then
            Return False
        End If


        If DBNull.Value.Equals(Requi.Item("OA_Contrato")) Then
            Fail = "1" 'Material no está en contrato
            Where = "Where ID = 1"
        Else
            InOA = True
        End If

        If InOA Then
            If (Requi.Item("ReqDate") < Requi.Item("OAInicio")) Or (Requi.Item("ReqDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "0" 'La requisición esta fuera de la validez del contrato
            End If
        End If


        'Si el material tiene contrato
        If InOA And (DBNull.Value.Equals(Requi.Item("SourceListOA_Start"))) Then
            Fail = Fail & "2" 'Material sin el source list del contrato

            If Where.Length > 0 Then
                Where = Where & " or ID = 2"
            Else
                Where = "Where ID = 2"
            End If
        Else
            OASourceList = True
        End If


        If InOA And OASourceList Then
            If (Requi.Item("ReqDate") < Requi.Item("SourceListOA_Start")) Or (Requi.Item("ReqDate") > Requi.Item("SourceListOA_End")) Then
                Fail = Fail & "3" 'La requisición esta fuera de la validez del source list

                If Where.Length > 0 Then
                    Where = Where & " or ID = 3"
                Else
                    Where = "Where ID = 3"
                End If
            End If
        End If


        If InOA Then
            If (Requi.Item("DeliveryDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "4" 'La fecha de entrega de la requi es posterior a la fecha de vencimiento del contrato

                If Where.Length > 0 Then
                    Where = Where & " or ID = 4"
                Else
                    Where = "Where ID = 4"
                End If
            End If
        End If


        If InOA Then
            If DBNull.Value.Equals(Requi.Item("AutoPO_MasterData")) Or Requi.Item("AutoPO_MasterData") = "" Then
                Fail = Fail & "5" 'Material sin el check de Auto PO de master data

                If Where.Length > 0 Then
                    Where = Where & " or ID = 5"
                Else
                    Where = "Where ID = 5"
                End If
            End If
        End If

        If InOA Then
            If DBNull.Value.Equals(Requi.Item("SourceList_MasterData")) Or (Requi.Item("SourceList_MasterData") = "") Then
                Fail = Fail & "6" 'Material sin el check de source list de master data

                If Where.Length > 0 Then
                    Where = Where & " or ID = 6"
                Else
                    Where = "Where ID = 6"
                End If
            End If
        End If


        If DBNull.Value.Equals(Requi.Item("OA")) And Not DBNull.Value.Equals(Requi.Item("OA_Contrato")) Then
            Fail = Fail & "8" 'Material no tiene Assing & Process y está en contrato

            If Where.Length > 0 Then
                Where = Where & " or ID = 8"
            Else
                Where = "Where ID = 8"
            End If
        End If


        If InOA Then
            If Trim(Requi.Item("PG_Contrato")) = "081" Then
                Fail = Fail & "9" 'Materiel en contrato de Importados

                If Where.Length > 0 Then
                    Where = Where & " or ID = 9"
                Else
                    Where = "Where ID = 9"
                End If
            End If
        End If


        If Fail.Length > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Retorna un valor string con los códigos de fallo
    ''' </summary>
    ''' <param name="Requisition">Número de la requisición en L7P </param>
    ''' <param name="Item">Número del ítem de la requisición</param>
    Public Function ReturnStatus(ByVal Requisition$, ByVal Item$, ByVal ReturnDetail As Boolean) As String
        Dim Fail$
        Dim InOA As Boolean
        Dim OASourceList As Boolean
        Dim Where$


        Fail = ""
        Where = ""
        InOA = False
        OASourceList = False

        Requi = Me.ReturnRequisitionStatus(Requisition, Item)

        'If DBNull.Value.Equals(Requi) Then
        '    Return False
        'End If
        'Verifica que el requi se encuentra en la base de datos

        If DBNull.Value.Equals(Requi.Item("Requisition")) Then
            Return False
        End If


        If DBNull.Value.Equals(Requi.Item("OA_Contrato")) Then
            Fail = "1" 'Material no está en contrato
            Where = "Where ID = 1"
        Else
            InOA = True
        End If

        If InOA Then
            If (Requi.Item("ReqDate") < Requi.Item("OAInicio")) Or (Requi.Item("ReqDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "0" 'La requisición esta fuera de la validez del contrato
            End If
        End If


        'Si el material tiene contrato
        If InOA And (DBNull.Value.Equals(Requi.Item("SourceListOA_Start"))) Then
            Fail = Fail & "2" 'Material sin el source list del contrato

            If Where.Length > 0 Then
                Where = Where & " or ID = 2"
            Else
                Where = "Where ID = 2"
            End If
        Else
            OASourceList = True
        End If


        If InOA And OASourceList Then
            If (Requi.Item("ReqDate") < Requi.Item("SourceListOA_Start")) Or (Requi.Item("ReqDate") > Requi.Item("SourceListOA_End")) Then
                Fail = Fail & "3" 'La requisición esta fuera de la validez del source list

                If Where.Length > 0 Then
                    Where = Where & " or ID = 3"
                Else
                    Where = "Where ID = 3"
                End If
            End If
        End If


        If InOA Then
            If (Requi.Item("DeliveryDate") > Requi.Item("OAFin")) Then
                Fail = Fail & "4" 'La fecha de entrega de la requi es posterior a la fecha de vencimiento del contrato

                If Where.Length > 0 Then
                    Where = Where & " or ID = 4"
                Else
                    Where = "Where ID = 4"
                End If
            End If
        End If


        If InOA Then
            If DBNull.Value.Equals(Requi.Item("AutoPO_MasterData")) Or (Requi.Item("AutoPO_MasterData") = "") Then
                Fail = Fail & "5" 'Material sin el check de Auto PO de master data

                If Where.Length > 0 Then
                    Where = Where & " or ID = 5"
                Else
                    Where = "Where ID = 5"
                End If
            End If
        End If

        If InOA Then
            If DBNull.Value.Equals(Requi.Item("SourceList_MasterData")) Or (Requi.Item("SourceList_MasterData") = "") Then
                Fail = Fail & "6" 'Material sin el check de source list de master data

                If Where.Length > 0 Then
                    Where = Where & " or ID = 6"
                Else
                    Where = "Where ID = 6"
                End If
            End If
        End If


        If (DBNull.Value.Equals(Requi.Item("OA")) Or Requi.Item("OA") = "") And Not DBNull.Value.Equals(Requi.Item("OA_Contrato")) Then
            Fail = Fail & "8" 'Material no tiene Assing & Process y está en contrato

            If Where.Length > 0 Then
                Where = Where & " or ID = 8"
            Else
                Where = "Where ID = 8"
            End If
        End If


        If InOA Then
            If Trim(Requi.Item("PG_Contrato")) = "081" Then
                Fail = Fail & "9" 'Materiel en contrato de Importados

                If Where.Length > 0 Then
                    Where = Where & " or ID = 9"
                Else
                    Where = "Where ID = 9"
                End If
            End If
        End If


        If Fail.Length > 0 Then
            Return Fail
        Else
            Return -1
        End If
    End Function
End Class
