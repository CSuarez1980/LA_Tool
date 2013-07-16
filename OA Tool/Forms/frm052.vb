Public Class frm052

    Dim Access As New Access.Application
    Dim cn As New OAConnection.Connection


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtVendorCode.Text.Length = 0 Then
            MsgBox("Por favor ingrese un código de proveedor.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'Esto valida que solo el owner del contrato pueda bajarlo
        'If Not cn.CanUpdateOA(Me.txtVendorCode.Text) And (cn.GetUserId() <> "BM4691") Then
        '    MsgBox("Usted no tiene los privilegios para bajar este formato", MsgBoxStyle.Critical)
        '    Exit Sub
        'End If

        Dim T As New DataTable

        T = cn.RunSentence("Select * From VendorsG11 Where Vendor = '" & Me.txtVendorCode.Text & "'").Tables(0)

        If T.Rows.Count > 0 Then
            SaveFileDialog1.Filter = "Excel Files|*.xls"
            SaveFileDialog1.Title = "Save Blue Form to:"
            SaveFileDialog1.FileName = Me.txtVendorCode.Text & " - " & T.Rows(0)("Name")
            SaveFileDialog1.ShowDialog()

            If SaveFileDialog1.FileName <> "" Then
                If Len(Dir(SaveFileDialog1.FileName)) > 0 Then
                    If MsgBox("El archivo especificado ya existe." & Chr(13) & Chr(13) & "  Desea reemplazarlo?", vbQuestion + vbYesNo, "Reemplazar archivo") = vbYes Then
                        Kill(SaveFileDialog1.FileName)
                    Else
                        MsgBox("Acción cancelada por el usuario.", vbExclamation, "Acción Cancelada")
                        Exit Sub
                    End If
                End If
                Access.Run("GetNewBlueForm", Me.txtVendorCode.Text, SaveFileDialog1.FileName)
                MsgBox("Done!")

            Else
                MsgBox("Debe ingresar un nombre de archivo.", MsgBoxStyle.Exclamation)
            End If
        Else
            MsgBox("El proveedor ingresado no se encuentra en la base de datos.", MsgBoxStyle.Information, "Proveedor no encontrado")

        End If

        'If Access.DCount("Vendor", "dbo_VendorsG11", "Vendor='" & Me.txtVendorCode.Text & "'") > 0 Then
        '    SaveFileDialog1.Filter = "Excel Files|*.xls"
        '    SaveFileDialog1.Title = "Save Blue Form to:"
        '    SaveFileDialog1.FileName = Me.txtVendorCode.Text & " - " & Access.DFirst("Name", "Vendors", "Code = " & Me.txtVendorCode.Text)
        '    SaveFileDialog1.ShowDialog()

        '    If SaveFileDialog1.FileName <> "" Then
        '        If Len(Dir(SaveFileDialog1.FileName)) > 0 Then
        '            If MsgBox("El archivo especificado ya existe." & Chr(13) & Chr(13) & "  Desea reemplazarlo?", vbQuestion + vbYesNo, "Reemplazar archivo") = vbYes Then
        '                Kill(SaveFileDialog1.FileName)
        '            Else
        '                MsgBox("Acción cancelada por el usuario.", vbExclamation, "Acción Cancelada")
        '                Exit Sub
        '            End If
        '        End If
        '        Access.Run("GetNewBlueForm", Me.txtVendorCode.Text, SaveFileDialog1.FileName)
        '    Else
        '        MsgBox("Debe ingresar un nombre de archivo.", MsgBoxStyle.Exclamation)
        '    End If
        'Else
        '    MsgBox("El proveedor ingresado no se encuentra en la base de datos.", MsgBoxStyle.Information, "Proveedor no encontrado")
        'End If
        'MsgBox("Done!")
    End Sub

    Private Sub frm052_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        cn.KillProcess("MSAccess")
        cn.KillProcess("Excel")
    End Sub

    Private Sub frm052_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
    End Sub
End Class