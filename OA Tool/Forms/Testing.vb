Imports SAPCOM.RepairsLevels
Imports CrystalDecisions.CrystalReports.Engine

Public Class Testing
    Dim gsUsuarioPC As String = "BM4691"
    Dim dtPOs As New DataTable
    Dim dtComments As New DataTable
    Dim cn As New OAConnection.Connection
    Dim IndexPO As Integer = 0 ' Indice para el datatable de las PO's
    Dim IndexCM As Integer = 0 ' Indice para el datatable de los comentarios
    Dim CurPO As Double = 0
    Dim CurIt As Double = 0
    Dim Comments As String = ""


    Private Function GetComments(ByVal PO As Double, ByRef Item As Double) As String
        Dim j As Integer = 0
        Dim Comment As String = ""

        For j = IndexCM To dtComments.Rows.Count - 1
            If (dtComments.Rows(j).Item("Doc Number") = PO) And (dtComments.Rows(j).Item("Item Number") = Item) Then
                Comment = Comment & " // " & dtComments.Rows(j).Item("Comentario")
            Else
                IndexCM = j
                Return Comment
            End If
        Next

        Return Comment
    End Function

    Private Function SearchPOComment() As Boolean
        Dim i As Integer = 0

        For i = IndexPO To dtPOs.Rows.Count - 1
            If (dtPOs.Rows(i).Item("Doc Number") = CurPO) And (dtPOs.Rows(i).Item("Item Number") = CurIt) Then
                Return True
            End If
            IndexPO = i
        Next
    End Function

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


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim SAP As SAPConection.c_SAP
        Dim res As Boolean
        Dim Variantes As DataTable

        Dim row As DataRow

        Variantes = cn.RunSentence("Select * From vstBI_Variantes").Tables(0)
        cn.ExecuteInServer("Delete From BI_TMP_ZMR0_Report")
        For Each row In Variantes.Rows
            SAP = New SAPConection.c_SAP(row.Item("BoxLongName").ToString)
            SAP.UserName = "BM4691"
            SAP.OpenConnection(True)

            res = SAP.DownloadZMR0(row.Item("Variant").trim, row.Item("User"), "")
            If res Then
                Dim Rep As DataTable
                Rep = cn.Read_ZMR0_File("C:\ZMR0.xls", row.Item("SAPBox").trim)
                cn.UploadZMR0(Rep)
            End If

            SAP.CloseConnection()
        Next

        Dim dtPOs As New DataTable

        dtPOs = cn.RunSentence("Select Distinct SAP From BI_TMP_ZMR0_Report Where Plant is null").Tables(0)
        If dtPOs.Rows.Count > 0 Then
            For Each row In dtPOs.Rows
                Dim dt As New DataTable
                ' Dim Rep As New SAPCOM.POs_Report(row.Item("SAP"), "BM4691")
                Dim rep As New SAPCOM.POs_Report(row.Item("SAP"), "BM4691", AppId)



                dt = cn.RunSentence("Select [Purchase Doc] From BI_TMP_ZMR0_Report Where ((Plant is null) And (SAP = '" & row.Item("SAP") & "'))").Tables(0)
                If dt.Rows.Count > 0 Then
                    Dim xrow As DataRow
                    For Each xrow In dt.Rows
                        Rep.IncludeDocument(xrow.Item("Purchase Doc"))
                    Next

                    Rep.Execute()
                    If Rep.Success Then
                        If Rep.ErrMessage = Nothing Then
                            Dim x As DataRow
                            For Each x In Rep.Data.Rows
                                cn.ExecuteInServer("Update BI_TMP_ZMR0_Report Set Plant = '" & x.Item("Plant") & "' Where [Purchase Doc] = '" & x.Item("Doc Number") & "' And SAP = '" & row.Item("SAP") & "'")
                            Next
                        End If
                    End If
                End If
            Next
        End If

        'Agrego los nuevos elementos al reporte ZMR0
        cn.ExecuteInServer("Insert Into BI_ZMR0_Report SELECT * FROM dbo.vst_BI_NewBlocked")

        Dim dtUnblocked As DataTable
        Dim drUnblocked As DataRow
        dtUnblocked = cn.RunSentence("SELECT * FROM dbo.vst_BI_UnBlocked").Tables(0)

        dtUnblocked.Columns.Add("Unblocked")

        For Each drUnblocked In dtUnblocked.Rows
            'Elimino los registros del reporte que ya fueron desbloqueados:
            cn.ExecuteInServer("Delete From BI_ZMR0_Report Where SAP = '" & drUnblocked.Item("SAP") & "' And [Company Code] = '" & drUnblocked.Item("Company Code") & "' And [Invoice Number] = '" & drUnblocked.Item("Invoice Number") & "' And [Purchase Doc] = '" & drUnblocked.Item("Purchase Doc") & "' And [Line Item1] = '" & drUnblocked.Item("Line Item1") & "'")
            drUnblocked.Item("Unblocked") = Now.Date
        Next


        '************************************************************************
        '************************************************************************
        '             
        '************************************************************************
        '************************************************************************

        'Agrego las facturas que ya no aparecen en el reporte ZMR0 al histórico
        cn.AppendTableToSqlServer("BI_ZMR0_History", dtUnblocked)

        getData()
        'SAP.Dispose()
    End Sub

    Private Sub Testing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgvZMR0.DataSource = BS
        getData()

    End Sub

    Private Sub getData()
        Dim Table As DataTable
        Table = cn.RunSentence("Select * From BI_ZMR0_Report").Tables(0)
        Me.lblContador.Text = "Rows: " & Table.Rows.Count
        BS.DataSource = Table
    End Sub
End Class