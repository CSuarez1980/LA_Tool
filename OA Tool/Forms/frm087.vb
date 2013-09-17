Imports System.Data.OleDb

Public Class frm087
    Dim Data As New DataTable
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Data.Rows.Count > 0 Then
            If MsgBox("You are going to change the NCM Table selection." & Chr(13) & Chr(13) & "Do you really want to apply this new table?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Changing NCM Table") = MsgBoxResult.Yes Then
                If Not bgwNCM.IsBusy Then
                    bgwNCM.RunWorkerAsync()
                Else
                    MsgBox("Background worker is busy, please wait.", MsgBoxStyle.Information)
                End If
            End If
        Else
            MsgBox("No data found.", MsgBoxStyle.Exclamation)
        End If

    End Sub

    Private Sub cmdExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExcel.Click
        Dim cn As New OAConnection.Connection
        Dim D As DataTable

        D = cn.RunSentence("Select * From [Catalog BRF Rules]").Tables(0)
        cn.ExportDataTableToXL(D)

    End Sub

    Private Sub cmdSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSearch.Click
        txtStatus.Text = "Status: Reading NCM Code file..."
        ofdFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ofdFile.Filter = "NCM Code Files (*.xlsx)|*.xlsx"
        If ofdFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Data = GetXL(ofdFile.FileName)

            If Data.Rows.Count > 0 Then
                Data.Columns(0).ColumnName = "Vendor Code"
                Data.Columns(1).ColumnName = "Vendor Name"
                Data.Columns(2).ColumnName = "Material Description"
                Data.Columns(3).ColumnName = "Long Description"
                Data.Columns(4).ColumnName = "NCM Code"
                Data.Columns(5).ColumnName = "Material Usage"
                Data.Columns(6).ColumnName = "Material Category"
                Data.Columns(7).ColumnName = "Material Origen"
                Data.Rows(0).Delete()
            Else
                MsgBox("Data couldn't be selected from [DATA] sheet" & Chr(13) & Chr(13) & "Raw data MUST be in 'DATA' sheet.", MsgBoxStyle.Exclamation)
            End If

            If Not Data Is Nothing Then
                dtgData.DataSource = Data
            End If
        End If
        txtStatus.Text = "Status: Done."
    End Sub

    Private Function Read_NCM_File(ByVal Path As String) As DataTable

        Xl(Path)

        'Dim cn As New OAConnection.Connection
        'Dim x As New DataTable

        'x = cn.GetXLTable(Path, "Data", "Select * From $Data").Tables(0)

        'Dim FileReader As New System.IO.StreamReader(Path, System.Text.Encoding.Default)
        'Dim S As String
        'Dim W As Array
        'Dim D As New DataTable
        'Dim ExitLoop As Boolean = False
        'Dim DR As DataRow
        'Dim I As Integer = 0


        'S = FileReader.ReadLine
        'D.Columns.Add(New DataColumn("Vendor", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Name", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Material Description", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Long Desc", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("NCMCode", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Spend", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Material Usage", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Material Category", System.Type.GetType("System.String")))
        'D.Columns.Add(New DataColumn("Material Origen", System.Type.GetType("System.String")))

        ''Para mostrar a Alejandra Baltodano como owner de los que no se les asigna

        'Do While Not S Is Nothing
        '    S = FileReader.ReadLine
        '    If S Is Nothing Or S = "" Then
        '        ExitLoop = True
        '    Else
        '        W = Split(S, vbTab)

        '        DR = D.NewRow
        '        I = 0

        '        For Each S In W
        '            DR(I) = S
        '            I += 1
        '        Next

        '        D.Rows.Add(DR)
        '    End If
        'Loop

        'D.AcceptChanges()
        'FileReader.Close()

        ' Return D
    End Function


    Private Function Xl(ByVal Path As String) As Boolean
        ' Establecemos la conexión con el libro de Excel, utilizando
        ' el ISAM de Excel del motor Microsoft Jet.
        '
        Using cnn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\myFolder\myExcel2007file.xlsx; Extended Properties=Excel 12.0 Xml;HDR=YES;")

            ' Creamos la consulta SQL de creación de tabla
            '
            Dim sql As String = _
                "SELECT * INTO [Hoja_Excel] " & _
                "IN ''[ODBC;Driver={SQL Server};" & _
                      "Server=MXL1380Q1V;Database=LA Tool;UID=SA;PWD=heavymetal]" & _
                "FROM [Data$]"

            ' Creamos el comando
            '
            Dim cmd As New OleDbCommand(sql, cnn)

            Try
                ' Abrimos la conexión
                cnn.Open()

                ' Ejecutamos el comando
                Dim n As Integer = cmd.ExecuteNonQuery

                ' Obtenemos los registros afectados
                MsgBox("Nº de registros exportados: " & CStr(n))

            Catch ex As Exception
                ' Se ha producido un error
                MsgBox(ex.Message)

            End Try

        End Using
    End Function

    Private Function GetXL(ByVal Path As String) As DataTable
        Dim ConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=""" & Path & """;Extended Properties=""Excel 12.0 Xml;HDR=NO;IMEX=1;ReadOnly=true;"""
        Dim conn As New OleDb.OleDbConnection(ConnectionString)
        Dim selString As String = "SELECT * FROM [Data$]"
        Dim CmdSelect As New OleDb.OleDbCommand(selString, conn)
        Dim Adapter As New OleDb.OleDbDataAdapter(CmdSelect)
        Dim ds As DataSet = New DataSet
        Dim rawDataTbl As New DataTable

        Try
            Adapter.Fill(ds)
            rawDataTbl = ds.Tables(0)
            conn.Close()
            conn = Nothing
        Catch e As Exception
            MsgBox("Error while querying database as follows: " & vbCrLf & e.Message, MsgBoxStyle.Exclamation)
        End Try

        Return rawDataTbl
    End Function

    Private Sub bgwNCM_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgwNCM.DoWork
        Dim cn As New OAConnection.Connection
        cn.ExecuteInServer("Insert BR_NCM_Upload_Table(Upload_Date, Upload_By) Values({fn now()},'" & gsUsuarioPC & "')")
        cn.ExecuteInServer("Delete From [Catalog BRF Rules]")
        If Not cn.AppendTableToSqlServer("[Catalog BRF Rules]", Data) Then
            MsgBox("Unable to save Catalog BRF Rules. Please try it again.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub frm087_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cmdTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTemplate.Click

    End Sub
End Class