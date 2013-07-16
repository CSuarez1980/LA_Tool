Public Class frm007
    Public FileName$

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim FileName$
        FileName = "*.xls"
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Archivos de Excel (*.xls)|*.xls"

        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog.FileName
        End If

        If FileName.Length < 5 Then
            MsgBox("Por favor seleccione el formato a enviar", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            Me.txtFormato.Text = FileName
        End If
    End Sub

    Private Sub txtVendorID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVendorID.KeyPress
        
    End Sub

    Private Sub frm007_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lblVendor.Text = ""
        Me.cboIdioma.Text = "Español"
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        If Me.txtVendorID.Text = "" Then
            MsgBox("Debe ingresar un código de proveedor para continuar", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If Me.txtFormato.Text = "" Then
            MsgBox("Debe anexar un archivo para continuar", MsgBoxStyle.Information)
            Exit Sub
        End If

        If Me.txtVendorMail.Text = "" Then
            MsgBox("Debe ingresar el correo del vendor para continuar", MsgBoxStyle.Information)
            Exit Sub
        End If


        Dim cn As New OAConnection.Connection
        'Dim Vendor As DataTable
        Dim BT$
        Dim FPath$
        Dim Contratos As DataTable
        Dim returnValue As IO.StreamReader

        Dim Subject As String

        Dim Attachment(0) As String
        Dim Recipient = ""
        Dim CopyTo = ""

        FPath = My.Application.Info.DirectoryPath.ToString() & "\stationaries"

        Select Case Me.cboIdioma.Text
            Case "Español"
                FPath = FPath + "\Esp\"

            Case "Portugués"
                FPath = FPath + "\Por\"
        End Select

        'Select Case Me.cboFormatMail.text
        '    Case "TXT"
        '        FPath = FPath + "\TXT\"

        '    Case "HTML"
        '        FPath = FPath + "\HTML\"
        'End Select

        'returnValue = My.Computer.FileSystem.OpenTextFileReader(FPath & "Send_1.txt") '& Me.txtVendorID.Text & "; "
        BT = My.Computer.FileSystem.ReadAllText(FPath & "Send_1.txt", System.Text.Encoding.Default) & Me.txtVendorID.Text & "; "


        BT = BT & My.Computer.FileSystem.ReadAllText(FPath & "Send_2.txt", System.Text.Encoding.Default)

        Contratos = cn.RunSentence("Select oa, Planta, Plant from vstOwners Where Vendor = " & Me.txtVendorID.Text).Tables(0)

        If Contratos.Rows.Count > 0 Then
            Dim i%

            For i = 0 To Contratos.Rows.Count - 1
                BT = BT & Contratos.Rows(i).Item("OA") & " - " & Contratos.Rows(i).Item("Planta") & " " & Contratos.Rows(i).Item("Plant") & "<br>" & Chr(13)
            Next
        End If

        BT = BT & My.Computer.FileSystem.ReadAllText(FPath & "Send_3.txt", System.Text.Encoding.Default)
        BT = BT & CStr(Now.AddDays(15).ToShortDateString)
        BT = BT & My.Computer.FileSystem.ReadAllText(FPath & "Send_4.txt", System.Text.Encoding.Default)

        Attachment(0) = Me.txtFormato.Text
        cn.SendOutlookMail(Me.txtAsunto.Text, Attachment, Me.txtVendorMail.Text, CopyTo, BT, "", False, "HTML")

        Contratos = cn.RunSentence("Select OA From vstOwners Where vendor = " & Me.txtVendorID.Text).Tables(0)

        If Contratos.Rows.Count > 0 Then
            Dim i%
            For i = 0 To Contratos.Rows.Count - 1
                cn.AddStepToTrackingOA(Contratos.Rows(i).Item("OA"), "4", True)
            Next
        End If


        MsgBox("Correo exportado al Outlook.", MsgBoxStyle.Information)
    End Sub


    Private Sub txtVendorID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVendorID.Validated
        Dim cn As New OAConnection.Connection
        Dim Vendor As DataTable
        Dim BT$
        Dim FPath$
        Dim Contratos As DataTable

        Dim Subject As String

        Dim Attachment(0) As String
        Dim Recipient = ""
        Dim CopyTo = ""


        If Me.txtVendorID.Text <> "" Then
            'Vendor = cn.GetAccessTable("Select vndName,vndMail1 From dbo_Vendor1 Where vndID = " & Me.txtVendorID.Text).Tables(0)
            Vendor = cn.RunSentence("Select Name,VndMail1 From VendorsG11 Where Vendor = '" & Me.txtVendorID.Text & "'").Tables(0)

            If Vendor.Rows.Count > 0 Then
                Me.lblVendor.Text = Vendor.Rows(0).Item("Name")
                Me.txtVendorMail.Text = Vendor.Rows(0).Item("vndMail1")
                Me.txtAsunto.Text = "*** Formato Azul para Cotizar:" & Me.txtVendorID.Text & " - " & Me.lblVendor.Text
            Else
                MsgBox("El código de proveedor ingresado no se encuentra en la base de datos.", MsgBoxStyle.Exclamation)
            End If
        Else
            Me.lblVendor.Text = ""
            Me.txtVendorMail.Text = ""
        End If
    End Sub

  
End Class