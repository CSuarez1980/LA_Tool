Public Class frm012
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    '10 Nov 2009
    '    '*****************************************************************************
    '    'Implementadas las rutinas para actualizar la validez del contrato.
    '    'Implementadas las rutinas para actualizar los source list individuales del contrato.
    '    '*****************************************************************************
    '    If Me.txtContrato.Text.Length > 0 Then
    '        Dim SAP As New SAPConection.c_SAP("BM4691", "tacares03", "L7P LA TS Prod")
    '        Dim cn As New OAConnection.Connection
    '        Dim Tabla As New DataTable
    '        Dim i As Double

    '        Tabla = cn.RunSentence("Select Material, Planta, Vendor, PO, Item From [Detalle de Contrato] Where OA = " & Me.txtContrato.Text).Tables(0)
    '        If Tabla.Rows.Count > 0 Then
    '            SAP.OpenConnection(SAPConfig)

    '            'Rutina para actualizar la validez del contrato; 
    '            'si la modificación es exitosa se procede a modificar el source list
    '            If SAP.FixValidityOfOA(Me.txtContrato.Text, Now.Date, Now.Date.AddDays(730)) Then ' -> Actualizo la fecha de validez del contrato
    '                For i = 0 To Tabla.Rows.Count - 1
    '                    'Rutina para actualizar el source list del contrato
    '                    SAP.FixSourceListOA(Tabla.Rows(i).Item("Material"), _
    '                                        Tabla.Rows(i).Item("Planta"), _
    '                                        Now.Date, Now.Date.AddDays(730), _
    '                                        Tabla.Rows(i).Item("Vendor"), _
    '                                        Tabla.Rows(i).Item("PO"), _
    '                                        Me.txtContrato.Text, _
    '                                        Tabla.Rows(i).Item("Item"))

    '                Next
    '                cn.SetOAUpdate(Trim(Me.txtContrato.Text))
    '            Else
    '                MsgBox("Error al actualizar la validez del contrato", MsgBoxStyle.Critical)
    '            End If

    '            SAP.CloseConnection()
    '            MsgBox("Done.", MsgBoxStyle.Information)
    '        End If
    '    Else
    '        MsgBox("No se ha ingresado ningún código de contrato.", MsgBoxStyle.Information)
    '    End If
    'End Sub

 
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        '10 Nov 2009
        '*****************************************************************************
        'Implementadas las rutinas para actualizar la validez del contrato.
        'Implementadas las rutinas para actualizar los source list individuales del contrato.
        '*****************************************************************************
        If Me.txtContrato.Text.Length > 0 Then

            'Dim SAP As New SAPConection.c_SAP("L7A TS Acceptance")
            'Dim c As New SAPCOM.SAPConnector
            'Dim u As Object = c.GetConnectionData("L7A", gsUsuarioPC, "LAT")
            Dim cn As New OAConnection.Connection
            Dim Tabla As New DataTable
            Dim i As Double

            Tabla = cn.RunSentence("Select Material, Planta, Vendor, PO, Item From [Detalle de Contrato] Where OA = " & Me.txtContrato.Text).Tables(0)
            If Tabla.Rows.Count > 0 Then
                Dim SAP As New SAPConection.c_SAP("L7P LA TS Prod")
                Dim c As New SAPCOM.SAPConnector
                Dim u As Object = c.GetConnectionData("L7P", gsUsuarioPC, "LAT")

                SAP.UserName = gsUsuarioPC
                SAP.Password = u.password
                SAP.OpenConnection(SAPConfig)

                'SAP.OpenConnection(SAPConfig)
                'Rutina para actualizar la validez del contrato; 
                'si la modificación es exitosa se procede a modificar el source list
                'If SAP.FixValidityOfOA(Me.txtContrato.Text, Now.Date, Now.Date.AddDays(730)) Then ' -> Actualizo la fecha de validez del contrato
                If SAP.FixValidityOfOA(Me.txtContrato.Text, Now.Date, txtYear.Text) Then ' -> Actualizo la fecha de validez del contrato
                    For i = 0 To Tabla.Rows.Count - 1
                        'Rutina para actualizar el source list del contrato
                        SAP.FixSourceListOA(Tabla.Rows(i).Item("Material"), _
                                            Tabla.Rows(i).Item("Planta"), _
                                            Now.Date, txtYear.Text, _
                                            Tabla.Rows(i).Item("Vendor"), _
                                            Tabla.Rows(i).Item("PO"), _
                                            Me.txtContrato.Text, _
                                            Tabla.Rows(i).Item("Item"))

                    Next
                    cn.SetOAUpdate(Trim(Me.txtContrato.Text))
                Else
                    MsgBox("Error al actualizar la validez del contrato", MsgBoxStyle.Critical)
                End If

                SAP.CloseConnection()
                MsgBox("Done.", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("No se ha ingresado ningún código de contrato.", MsgBoxStyle.Information)
        End If

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub frm012_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class