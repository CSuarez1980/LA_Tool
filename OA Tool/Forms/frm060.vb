Public Class frm060
    Dim dtVendor As New DataTable
    Dim cn As New OAConnection.Connection
    Dim _Case As String = ""

    Public Property OEMS_Case_ID() As String
        Get
            Return _Case
        End Get
        Set(ByVal value As String)
            _Case = value
        End Set
    End Property


    Private Sub frm060_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadInfo()

        cn.LoadCombo(Me.cboIdioma, "Select IDIdioma,Descripcion From Idioma", "IDIdioma", "Descripcion")
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cboIdioma_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboIdioma.SelectionChangeCommitted
        cn.LoadCombo(Me.cboEstacionarios, "SELECT IDEstacionario, Descripcion FROM Estacionarios Where IDUsuario = '" & gsUsuario & "' And IDIdioma = '" & Me.cboIdioma.SelectedValue.ToString & "'", "IDEstacionario", "Descripcion")
    End Sub

    Private Sub cmdOutlook_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutlook.Click
        Dim cn As New OAConnection.Connection
        Dim Mail As New DataTable
        Dim i As Integer = 0
        Dim Orden As String = ""
        Dim Vendors As New List(Of String)
        Dim _IsSTR As Boolean = False

        dtgVendors.EndEdit()

        '*******************************************
        'Verifico que se seleccione un estacionario:
        '*******************************************
        If Me.cboEstacionarios.SelectedValue <> Nothing Then
            If Me.cboEstacionarios.SelectedValue.ToString = "System.Data.DataRowView" Then
                MsgBox("You should select a stationary...", MsgBoxStyle.Exclamation, "Stationary selection")
                Exit Sub
            End If
        Else
            MsgBox("You should select a stationary...", MsgBoxStyle.Exclamation, "Stationary selection")
            Exit Sub
        End If

        Me.dtgVendors.EndEdit()


        '***************************************
        'Obtengo la estructura del estacionario:
        '***************************************
        Mail = cn.RunSentence("Select * From Estacionarios Where IdEstacionario = " & Me.cboEstacionarios.SelectedValue.ToString).Tables(0)

        If Mail.Rows.Count > 0 Then
            Dim row As System.Windows.Forms.DataGridViewRow
            Dim Body As String = ""
            Dim Requi As String = ""
            Dim BCC As String = ""
            Dim DT As New DataTable
            Dim MyTag As String = ""

            Dim Attach As String()
            ReDim Attach(1)

            DT.Columns.Add("Requisition")
            DT.Columns.Add("Item")
            DT.Columns.Add("Material")
            DT.Columns.Add("Quantity")
            DT.Columns.Add("Manufacter")
            DT.Columns.Add("Part Number")

            DT.Rows.Add()
            DT.Rows(0).Item("Requisition") = Me.txtRequisition.Text
            DT.Rows(0).Item("Item") = Me.txtItem.Text
            DT.Rows(0).Item("Material") = Me.txtGica.Text & " - " & Me.txtDescription.Text
            DT.Rows(0).Item("Quantity") = Me.txtQuantity.Text
            DT.Rows(0).Item("Manufacter") = Me.txtManufacter.Text
            DT.Rows(0).Item("Part Number") = Me.txtPartNumber.Text


            Body = Mail.Rows(0).Item("Mensage")

            cn.Put_HTML_Table_In_ClipBoard(DT)
            MyTag = My.Computer.Clipboard.GetText(Windows.Forms.TextDataFormat.Text) & Chr(13) & Me.txtComment.Text
            Body = Replace(Mail.Rows(0).Item("Mensage"), "<@QUOTE>", MyTag)
            Attach(0) = Me.txtAttach.Text

            For Each row In dtgVendors.Rows
                If row.Cells("Ck").Value Then
                    If BCC.Length > 0 Then
                        BCC = BCC & ";"

                    End If

                    BCC = BCC & row.Cells("Mail").Value
                    Vendors.Add(row.Cells("Vendor").Value)
                End If
            Next

            If Microsoft.VisualBasic.Left(txtGica.Text, 1) = "3" Then
                _IsSTR = True
            End If

            Dim Subject As String = ""
            Subject = "Case ID:" & _Case & " PR Number  " & Me.txtRequisition.Text & "-" & Mail.Rows(0).Item("Asunto")

            cn.SendOutlookMail(Subject, Attach, "", "", Body, "", False, "HTML", BCC, False, _IsSTR)

            Dim V As String

            For Each V In Vendors
                cn.ExecuteInServer("Insert Into HistoricoCotizacion(Fecha, Requi, Item, SAPBox, Vendor, TNumber) Values({fn now()}, " & txtRequisition.Text & "," & txtItem.Text & ",'" & txtSAPBox.Text & "'," & V & ",'" & gsUsuarioPC & "')")
            Next



            MsgBox("Mails was created successfully.", MsgBoxStyle.Information, "Mail created")
        Else
            MsgBox("No se encontró el estacionario.")
        End If
    End Sub
  
    Private Sub dtgVendors_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dtgVendors.CellEndEdit
        If Not DBNull.Value.Equals(dtgVendors.CurrentCell.Value) AndAlso dtgVendors.CurrentRow.Cells("Vendor").Value <> "" Then
            Dim dtVn As New DataTable

            dtVn = cn.RunSentence("Select Name, VndMail1 as Mail, Country  From VendorsG11 Where Vendor = '" & dtgVendors.CurrentCell.Value & "'").Tables(0)

            If dtVn.Rows.Count > 0 Then
                dtgVendors.CurrentRow.Cells("Name").Value = dtVn.Rows(0).Item("Name")
                dtgVendors.CurrentRow.Cells("Mail").Value = dtVn.Rows(0).Item("Mail")
                dtgVendors.CurrentRow.Cells("Country").Value = dtVn.Rows(0).Item("Country")
                dtgVendors.CurrentRow.Cells("CK").Value = True
            End If
        End If
    End Sub

    Private Sub cmdSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSelectAll.Click
        Dim row As Windows.Forms.DataGridViewRow

        For Each row In dtgVendors.Rows
            If Not DBNull.Value.Equals(row.Cells("Vendor").Value) AndAlso row.Cells("Vendor").Value <> "" Then
                row.Cells("CK").Value = True
            End If
        Next
    End Sub

    Private Sub cmdClearSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClearSelection.Click
        Dim row As Windows.Forms.DataGridViewRow

        For Each row In dtgVendors.Rows
            row.Cells("CK").Value = False
        Next
    End Sub

    Private Sub cmdHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHistory.Click
        Dim form As New frm061

        form.Requisition = Me.txtRequisition.Text
        form.Item = Me.txtItem.Text
        form.Show()

    End Sub

    Private Sub cmdAttach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAttach.Click
        Dim FileName$
        FileName = "*.*"
        OpenFileDialog1.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog1.Filter = "All Files (*.*)|*.*"

        If (OpenFileDialog1.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            FileName = OpenFileDialog1.FileName
        End If

        If FileName.Length < 5 Then
            MsgBox("Please select the file", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            Me.txtAttach.Text = FileName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        LoadInfo()
    End Sub

    Private Sub LoadInfo()
        dtVendor = cn.RunSentence("Select Ck, CF, Vendor, Name, Mail,Country From vstVendor_Manufacter Where Manufacter = '" & Me.txtManufacter.Text & "'").Tables(0)

        If dtVendor.Rows.Count > 0 Then
            Dim Cf As New DataTable 'confirmation
            Dim CE As New DataTable 'Sent

            Cf = cn.RunSentence("Select Requi, Item, SAPBox, Vendor, Confirmation From vstCoti_Confirm Where Requi = " & Me.txtRequisition.Text & " And Item = " & Me.txtItem.Text).Tables(0)
            CE = cn.RunSentence("Select Requi, Item, SAPBox, Vendor From vstCoti_Envios Where Requi = " & Me.txtRequisition.Text & " And Item = " & Me.txtItem.Text).Tables(0)

            If Cf.Rows.Count > 0 Or CE.Rows.Count > 0 Then
                Dim r As DataRow
                Dim cr As DataRow 'confirmation row

                For Each r In dtVendor.Rows
                    For Each cr In Cf.Rows
                        If r("Vendor") = cr("Vendor") Then
                            r("CF") = True
                        End If
                    Next

                    For Each cr In CE.Rows
                        If r("Vendor") = cr("Vendor") Then
                            r("CK") = True
                        End If
                    Next
                Next

            End If
        End If

        dtgVendors.DataSource = dtVendor
    End Sub

    Private Sub cmdConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdConfirm.Click
        Dim r As Windows.Forms.DataGridViewRow

        dtgVendors.EndEdit()
        For Each r In dtgVendors.Rows
            If Not DBNull.Value.Equals(r.Cells("Vendor").Value) AndAlso r.Cells("Vendor").Value <> "" Then
                cn.ExecuteInServer("Update HistoricoCotizacion Set Confirmation = " & IIf(r.Cells("CF").Value, 1, 0) & " Where Requi = " & Me.txtRequisition.Text & " And Item = " & Me.txtItem.Text & " And Vendor = " & r.Cells("Vendor").Value)
            End If
        Next

    End Sub

 
End Class