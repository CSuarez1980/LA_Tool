Imports SAPCOM.RepairsLevels
Imports System.Configuration
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports CrystalDecisions.CrystalReports.ViewerObjectModel

Public Class frm045
    Dim cn As New OAConnection.Connection
    Dim Comentarios As New DataTable

    Dim dtPOs As New DataTable
    Dim dtComments As New DataTable
    Dim IndexComments As Integer = 0
    Dim IndexPO As Integer = 0 ' Indice para el datatable de las PO's
    Dim IndexCM As Integer = 0 ' Indice para el datatable de los comentarios
    Dim CurPO As Double = 0
    Dim CurIt As Double = 0
    Dim Comments As String = ""

    Private Sub cmdDowload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDowload.Click
        Me.ToolBar.Enabled = False
        'Dim c As New SAPCOM.SAPConnector
        Dim i As Integer
        Dim Rep As New SAPCOM.OpenOrders_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC, AppId)
        'Dim Rep As New SAPCOM.POs_Report(Me.cboSAPBox.SelectedValue.ToString, gsUsuarioPC)
        Dim Plantas As New DataTable
        Dim Vendors As New DataTable
        Dim PGrp As New DataTable
        Dim POrg As New DataTable
        Dim POs As New DataTable
        Dim MatGrp As New DataTable

        Plantas = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Planta' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        Vendors = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'Vendor' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        PGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        POrg = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'PurchOrg' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)
        MatGrp = cn.RunSentence("Select Variantes.Valor,Variantes.Prefijo From Variantes,HeaderVariante Where (Variantes.IDVariante = HeaderVariante.IDVariante and Campo = 'MatGrp' and Variantes.Usuario = '" & gsUsuario & "' And HeaderVariante.IDVariante = " & Me.cboVariantes.SelectedValue.ToString & ")").Tables(0)

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

        '****************************************************************
        'Rep.IncludeDocsDatedFromTo(Me.dtpStart.Text, Me.dtpEnd.Text)


        Rep.RepairsLevel = IncludeRepairs
        Rep.IncludeDelivDates = True
        Rep.Include_GR_IR = True

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


                POs = Rep.Data
                POs.Columns.Add("Usuario")
                POs.Columns.Add("SAPBox")
                POs.Columns.Add("Status")

                For i = 0 To POs.Rows.Count - 1
                    POs.Rows(i).Item("Usuario") = gsUsuarioPC
                    POs.Rows(i).Item("SAPBox") = Me.cboSAPBox.SelectedValue.ToString
                    POs.Rows(i).Item("Status") = ""
                Next

                cn.ExecuteInServer("Delete From tmpRepCommentsPO Where Usuario = '" & gsUsuarioPC & "'")
                cn.AppendTableToSqlServer("tmpRepCommentsPO", POs)

                dtComments = cn.RunSentence("Select * From vstDetalleRepoPOComment Where Usuario = '" & gsUsuarioPC & "'").Tables(0)
                dtPOs = cn.RunSentence("Select * From vstHeaderRepoPOComment Where Usuario = '" & gsUsuarioPC & "'").Tables(0)


                Do While IndexCM < dtComments.Rows.Count - 1
                    Comments = ""

                    CurPO = dtComments.Rows(IndexCM).Item("Doc Number")
                    CurIt = dtComments.Rows(IndexCM).Item("Item Number")

                    Comments = GetComments(CurPO, CurIt)
                    SetComments(CurPO, CurIt, Comments)
                Loop


                Me.dtgPOReport.DataSource = dtPOs
                Me.txtTotalPO.Text = dtPOs.Rows.Count

            Else
                MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
                Me.txtEstado.Text = Rep.ErrMessage
            End If
        Else
            MsgBox(Rep.ErrMessage, MsgBoxStyle.Information)
            Me.txtEstado.Text = Rep.ErrMessage
        End If

        Me.ToolBar.Enabled = True
    End Sub

    Private Sub frm045_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cn.LoadCombo(Me.cboSAPBox, "Select BoxShortName,BoxLongName From SAPBox", "BoxShortName", "BoxLongName")
    End Sub

    Private Sub cboSAPBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSAPBox.SelectedIndexChanged
        If Me.cboSAPBox.SelectedValue.ToString <> "System.Data.DataRowView" Then
            cn.LoadCombo(Me.cboVariantes, "Select IDVariante, Nombre From HeaderVariante Where TNumber = '" & gsUsuario & "' and SAPBox = '" & Me.cboSAPBox.SelectedValue.ToString & "'", "IDVariante", "Nombre")
        End If
    End Sub

    Private Sub frm045_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Me.dtgPOReport.Width = Me.Width - 20
        Me.dtgPOReport.Height = Me.Height - 220
    End Sub

    ''' <summary>
    ''' Obtiene los comentarios de las POs
    ''' </summary>
    ''' <param name="PO">Numero de PO</param>
    ''' <param name="Item">Numero de Item</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetComments(ByVal PO As Double, ByRef Item As Double) As String
        Dim j As Integer = 0
        Dim Comment As String = ""

        For j = IndexCM To dtComments.Rows.Count - 1
            If (dtComments.Rows(j).Item("Doc Number") = PO) And (dtComments.Rows(j).Item("Item Number") = Item) Then
                Comment = Comment & "[" & dtComments.Rows(j).Item("Fecha").date & "] - " & dtComments.Rows(j).Item("Comentario") & " // "
                IndexCM = j
            Else
                IndexCM = j
                Return Comment
            End If
        Next

        Return Comment.Trim
    End Function

    ''' <summary>
    ''' Setea el comentario en la celda del datatable de las POs
    ''' </summary>
    ''' <param name="PO">Numero de PO a la que se agregara el comentario</param>
    ''' <param name="Item">Item de la PO a la que se agregara el comentario</param>
    ''' <param name="Comment">Comentario unificado</param>
    ''' <remarks></remarks>
    Private Sub SetComments(ByVal PO As Double, ByVal Item As Double, ByVal Comment As String)
        Dim i As Integer = 0
        For i = IndexPO To dtPOs.Rows.Count - 1
            If (dtPOs.Rows(i).Item("Doc Number") = PO) And (dtPOs.Rows(i).Item("Item Number") = Item) Then
                dtPOs.Rows(i).Item("Comments") = Comment
                Exit Sub
            Else
                IndexPO = i
            End If
        Next
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        cn.ExportDataTableToXL(dtPOs)
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        'dtPOs.WriteXml("")

        ' Dim SaveFile As New SaveFileDialog
        Dim FileName As String = ""

        Try
            SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            SaveFileDialog.Filter = "Archivos de XML (*.xml)|*.xml"

            If (SaveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                FileName = SaveFileDialog.FileName
            Else
                MsgBox("Acción cancelada por el ususario.")
                Exit Sub
            End If

            dtPOs.TableName = "POs"
            dtPOs.WriteXml(FileName)
        Catch ex As Exception

        End Try

    End Sub
End Class