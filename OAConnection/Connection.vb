Imports System.Security.Permissions
Imports System.Windows.Forms
Imports System.Xml
Imports System.Data
Imports System.Reflection

'Imports Microsoft.Office.Interop.Excel.XlApplicationInternational
'Imports Microsoft.Office.Interop.Excel.XlRangeValueDataType
'Imports Microsoft.Office.Interop.Excel.XlCmdType
'Imports Microsoft.Office.Interop.Outlook

Public Class Connection
    Private ConecctionDB As SqlClient.SqlConnection
    Private Command As SqlClient.SqlCommand
    Private Adapter As SqlClient.SqlDataAdapter
    Private DataReader As SqlClient.SqlDataReader
    Private Tansaccion As SqlClient.SqlTransaction
    Private CxnAccess As OleDb.OleDbConnection

    Sub test()

        'Dim AP As New Microsoft.Office.Interop.Excel.Application
        'Dim WB As Microsoft.Office.Interop.Excel.Workbook = AP.Workbooks.Add
        'Dim WS As Microsoft.Office.Interop.Excel.Worksheet = WB.Sheets(1)
        'Dim array As New Object

        'WS.Range("A1").Select()
        'With WS.ListObjects.Add(SourceType:=0, Source:=array( _
        '    "OLEDB;Provider=SQLOLEDB.1;Password=gecosystem;Persist Security Info=True;User ID=sa;Data Source=20c4tc1;Use Procedure for Prepare=1;Au" _
        '    , _
        '    "to Translate=True;Packet Size=4096;Workstation ID=CNU8374800-W7;Use Encryption for Data=False;Tag with column collation when pos" _
        '    , "sible=False;Initial Catalog=OutlineAgreement"), Destination:=WS.Range("$A$1")) _
        '    .QueryTable
        '    .CommandType = xlCmdTable
        '    .CommandText = array("""OutlineAgreement"".""dbo"".""Detalle de Contrato""" _
        '    )
        '    .RowNumbers = False
        '    .FillAdjacentFormulas = False
        '    .PreserveFormatting = True
        '    .RefreshOnFileOpen = False
        '    .BackgroundQuery = True
        '    '.RefreshStyle =  xlInsertDeleteCells
        '    .SavePassword = False
        '    .SaveData = True
        '    .AdjustColumnWidth = True
        '    .RefreshPeriod = 0
        '    .PreserveColumnInfo = True
        '    .SourceConnectionFile = _
        '    "C:\Users\suarez.c.7\Documents\My Data Sources\20c4tc1 OutlineAgreement Detalle de Contrato.odc"
        '    .ListObject.DisplayName = _
        '    "Table__20c4tc1_OutlineAgreement_Detalle_de_Contrato"
        '    .Refresh(BackgroundQuery:=False)
        'End With

    End Sub

    Public Overridable Function ExecuteStoredProcedure(ByVal Store_Procedure_Name As String, Optional ByVal Parameters As List(Of Data.SqlClient.SqlParameter) = Nothing) As Boolean
        Dim conn As New SqlClient.SqlConnection(GetConnectionString)
        Try
            conn.Open()

            If conn.State = ConnectionState.Open Then
                Dim cmd As New SqlClient.SqlCommand
                Dim reader As SqlClient.SqlDataReader

                If Not Parameters Is Nothing Then
                    For Each P As Data.SqlClient.SqlParameter In Parameters
                        cmd.Parameters.Add(P)
                    Next
                End If

                cmd.CommandText = Store_Procedure_Name
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Connection = conn
                reader = cmd.ExecuteReader()
            End If
            Return True
        Catch ex As Exception
            Return False
        Finally
            conn.Close()
        End Try
    End Function

    Public Function GetConnectionString() As String
        'Dim reader As New System.Xml.Serialization.XmlSerializer(GetType(Server))
        'Dim file As New System.IO.StreamReader(My.Application.Info.DirectoryPath & "\ServerConfig.xml")
        'Dim MyConfig As New Server

        'MyConfig = CType(reader.Deserialize(file), Server)
        'file.Close()

        Return "Data Source=MXL1380Q1V; Initial Catalog=LA Tool;User ID=developer; Password=hmetal"

        ' Return "Data Source=172.28.23.2,1528; Initial Catalog=LATool;User ID=oemsadmin; Password=@bhiram1"
        'Return "Data Source=IBLCRPGSQLLA\SQLExpress; Initial Catalog=LA Tool; User ID=Developer; Password=hmetal; Connect Timeout=0;"
        'Return "Data Source=cnu8374800; Initial Catalog=OATool;User ID=guest; Password=guest"
        'GetConnectionString
    End Function
    Private Function _GetConnectionString_2() As String
        'Return "Data Source=gz1c7c1; Initial Catalog=DMTool;User ID=developer; Password=hmetal"
        Return "Data Source=20C4TC1; Initial Catalog=DMTool;User ID=DMTool; Password=DMTool"
        'Return "Data Source=cnu8374800; Initial Catalog=OATool;User ID=guest; Password=guest"
        'GetConnectionString
    End Function
    Public Sub KillProcess(ByVal ProcessName$)
        Dim Process() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcessesByName(ProcessName)
        On Error Resume Next

        If Process.Length <> 0 Then
            Dim i As Integer = 0

            For i = 0 To Process.Length - 1
                If Process(i).MainWindowTitle = "" Then
                    Process(i).Kill()
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' Ejecuta una instrucción SQL en el servidor
    ''' </summary>
    ''' <param name="SentenciaSql">Instrucción SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ExecuteInServer(ByVal SentenciaSql As String, Optional ByVal Cont As Integer = 0) As Integer
        Try
            'Establece la conexón con la base de datos
            ConecctionDB = New SqlClient.SqlConnection(GetConnectionString().ToString)
            ConecctionDB.Open()

            'Configura el commando de sql
            Command = New SqlClient.SqlCommand(SentenciaSql.ToString, ConecctionDB)
            Command.CommandTimeout = 360
            ExecuteInServer = Command.ExecuteNonQuery

            ' Retorna los registros afectados
            Return ExecuteInServer
        Catch ex As Exception
            'If Cont <= 3 Then
            'ConecctionDB.Close()
            'Cont += 1
            'ExecuteInServer(SentenciaSql, Cont)
            'End If
            Throw New Exception(ex.Message, ex)
        Finally
            ConecctionDB.Close()
        End Try

    End Function
    Public Function ExecuteInAccess(ByVal SentenciaSql As String) As Integer
        Dim comm As OleDb.OleDbCommand

        Try
            'Establece la conexón con la base de datos
            CxnAccess = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & My.Application.Info.DirectoryPath & "\OAData\New.mdb")
            CxnAccess.Open()

            'Configura el commando de sql

            comm = New OleDb.OleDbCommand(SentenciaSql.ToString, CxnAccess)
            ExecuteInAccess = Command.ExecuteNonQuery

            ' Retorna los registros afectados
            Return ExecuteInAccess
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        Finally
            ConecctionDB.Close()
        End Try

    End Function
    Public Function ExcelADO()
        'Establecer una conexión con el origen de datos. 
        Dim sConnectionString As String
        Dim sSampleFolder As String = ""


        sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & sSampleFolder & _
        "Book7.xls;Extended Properties=Excel 8.0;"


        Dim objConn As New System.Data.OleDb.OleDbConnection(sConnectionString)
        objConn.Open()

        'Agregar dos registros a la tabla. 
        Dim objCmd As New System.Data.OleDb.OleDbCommand()
        objCmd.Connection = objConn
        objCmd.CommandText = "Insert into [Sheet1$] (FirstName, LastName)" & _
        " values ('David', 'Ruiz')"
        objCmd.ExecuteNonQuery()
        objCmd.CommandText = "Insert into [Sheet1$] (FirstName, LastName)" & _
        " values ('Elena', 'Asensio')"
        objCmd.ExecuteNonQuery()

        'Cerrar la conexión. objConn.Close() 
    End Function
    Public Function DataTableToExcel(ByVal DT As DataTable, ByVal Path As String) As Boolean

        DataTableToExcel = True
        If My.Computer.FileSystem.FileExists(Path) Then
            Try
                My.Computer.FileSystem.DeleteFile(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.ThrowException)
            Catch ex As Exception
                DataTableToExcel = False
                Exit Function
            End Try
        End If

        Dim AP As New Microsoft.Office.Interop.Excel.Application
        Dim WB As Microsoft.Office.Interop.Excel.Workbook = AP.Workbooks.Add
        Dim WS As Microsoft.Office.Interop.Excel.Worksheet = WB.Sheets(1)
        Dim dc As DataColumn
        Dim iCols As Int32 = 0
        For Each dc In DT.Columns
            WS.Range("A1").Offset(0, iCols).Value = dc.ColumnName
            iCols += 1
        Next
        With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols)).Interior
            .Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
            .PatternColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
            '.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorLight2
            .TintAndShade = 0.399975585192419
            .PatternTintAndShade = 0
        End With
        With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols)).Font
            '.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1
            .TintAndShade = 0
        End With
        Dim iRows As Int32
        For iRows = 0 To DT.Rows.Count - 1
            WS.Range("A2").Offset(iRows).Resize(1, iCols).Value = DT.Rows(iRows).ItemArray()
        Next

        WS.Columns.AutoFit()
        WS.Range("A1").Select()
        Try
            WB.SaveAs(Path)
        Catch ex As Exception
            DataTableToExcel = False
        End Try
        WB.Close(False)
        AP.Quit()

    End Function
    ''' <summary>
    ''' Retorna un DataSet del servidor a partir de una instruccion SQL Ej:
    ''' <code>SELECT * FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code>
    ''' </summary>
    ''' <param name="sentenciaSQL">La sentencia SQL con el formato: SELECT * FROM Tabla</param>
    Public Function RunSentence(ByVal SentenciaSql As String, Optional ByVal pCount As Integer = 0) As System.Data.DataSet
        Dim datasettemp As New DataSet

        Try
            ConecctionDB = New SqlClient.SqlConnection(GetConnectionString())
            ConecctionDB.Open()

            Adapter = New SqlClient.SqlDataAdapter(SentenciaSql, ConecctionDB)
            Adapter.Fill(datasettemp)
            ConecctionDB.Close()

        Catch ex As Exception
            If pCount < 3 Then
                pCount += 1
                ConecctionDB.Close()
                Return RunSentence(SentenciaSql, pCount)
            Else
                If ConecctionDB.State = ConnectionState.Open Then
                    ConecctionDB.Close()
                End If
            End If

        Finally
            datasettemp.Dispose()

            If Not Adapter Is Nothing Then
                Adapter.Dispose()
                Adapter = Nothing
            End If
            ConecctionDB = Nothing
        End Try

        Return datasettemp
    End Function
    Public Function UpdateOrigenes() As DataSet
        Dim datasettemp As DataSet

        Try
            datasettemp = New DataSet()

            ConecctionDB = New SqlClient.SqlConnection(GetConnectionString)
            ConecctionDB.Open()

            Adapter = New SqlClient.SqlDataAdapter("Select * From Origen", ConecctionDB)
            Adapter.UpdateCommand = New SqlClient.SqlCommand("Update Origen Set Descripcion = @Descripcion", ConecctionDB)

            Dim workParm As SqlClient.SqlParameter = Adapter.UpdateCommand.Parameters.Add("@Descripcion", SqlDbType.Int)
            workParm.SourceColumn = "Descripcion"
            workParm.SourceVersion = DataRowVersion.Original

            Adapter.Fill(datasettemp, "Origen")

            Return datasettemp

        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)

        Finally

            ConecctionDB.Close()
            datasettemp.Dispose()
            Adapter.Dispose()
            Adapter = Nothing
            ConecctionDB = Nothing
        End Try

    End Function
    Public Sub ExecuteChangesOrigenes(ByVal DS As DataSet)
        Adapter = New SqlClient.SqlDataAdapter()
        Adapter.Fill(DS)
        Adapter.Update(DS)
    End Sub
    Public Function ReturnGigaOnOA(ByVal Gica$, ByVal Plant$) As System.Data.DataSet
        Dim Table As New DataSet
        Dim GetInfo As Boolean

        If Len(Gica) <> 8 Then
            MsgBox("Please check the material code: Parameter[GICA]", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        If Len(Plant) <> 4 Then
            MsgBox("Please check the plant code: Parameter[PLANT]", MsgBoxStyle.Exclamation)
            Exit Function
        End If

        GetInfo = False

        Try
            Table = Me.RunSentence("Select * From DetelleContratos Where gica = '" & Gica & "' And Plant = '" & Plant & "'")
            GetInfo = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        If GetInfo Then
            Return Table
        End If
    End Function
    Public Function GetActivesPlants() As DataSet
        Dim Table As DataSet
        Dim GetInfo As Boolean

        GetInfo = False

        Try
            Table = Me.RunSentence("Select * From Plant Where UseInDownload = 1")
            GetInfo = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        If GetInfo Then
            Return Table
        End If
    End Function
    'Public Function RunSQLAccess(ByVal SentenciaSQL As String) As Integer Implements DTMLibrary.IServers.RunSQLAccess
    '    Dim cmd As OleDb.OleDbCommand
    '    Try
    '        CxnAccess = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & My.Application.Info.DirectoryPath & "\CapturaDatosZZ.mdb")

    '        cmd = New OleDb.OleDbCommand(SentenciaSQL, CxnAccess)

    '        cmd.Connection.Open()
    '        cmd.ExecuteNonQuery()
    '        cmd.Connection.Close()

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message, ex.InnerException)
    '        cmd.Connection.Close()
    '        CxnAccess.Close()

    '    End Try

    'End Function
    ''' <summary>
    ''' Retorna un dataset de access a partir de una instruccion SQL Ej:
    ''' <code>SELECT * FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code>
    ''' </summary>
    ''' <param name="lsSql">La sentencia SQL con el formato: SELECT * FROM Tabla</param>
    ''' <param name="File">Archivo del que se extraeran los datos</param>
    ''' <param name="Sheet">Nombre de la hoja de la que se extraeran los datos</param>
    Public Function GetXLTable(ByVal File$, ByVal Sheet$, ByVal lsSQl$) As DataSet

        Dim tmpDS As New DataSet
        Dim tmpAdapter As OleDb.OleDbDataAdapter
        Dim tmpCon As OleDb.OleDbConnection

        tmpCon = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & File & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1""")
        tmpAdapter = New OleDb.OleDbDataAdapter(lsSQl, tmpCon)

        Try
            tmpAdapter.Fill(tmpDS)

        Catch ex As Exception
            MsgBox("Error intentado acceder al archivo de Excel", MsgBoxStyle.Critical)
        End Try

        tmpCon.Close()

        Return tmpDS

    End Function
    Public Function GetXl(ByVal File As String, ByVal Sheet As String) As DataTable
        Dim Table As DataTable

        Return Table
    End Function
    ''' <summary>
    ''' Retorna un dataset de access a partir de una instruccion SQL Ej:
    ''' <code>SELECT * FROM Tabla WHERE campo1=@campo1, campo2=@campo2</code>
    ''' </summary>
    ''' <param name="sentenciaSQL">La sentencia SQL con el formato: SELECT * FROM Tabla</param>
    Public Function GetAccessTable(ByVal SentenciaSQL As String) As DataSet
        Dim Table As DataSet
        Dim AD As OleDb.OleDbDataAdapter
        'Dim cmd As OleDb.OleDbCommand
        Try
            CxnAccess = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & My.Application.Info.DirectoryPath & "\OAData\New.mdb")
            CxnAccess.Open()

            Table = New DataSet

            AD = New OleDb.OleDbDataAdapter(SentenciaSQL, CxnAccess)

            AD.Fill(Table)

            CxnAccess.Close()
        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

        Return Table
    End Function    'Public Function ImportOpenOrders(ByVal txtFile$, ByVal SAPBox$) As Boolean
    '    Dim currentRow As String()
    '    Dim Table As New DataSet

    '    Me.RunSQLAccess("Delete From tmpOpenOrders")


    '    Using Reader As New Microsoft.VisualBasic.FileIO.TextFieldParser(txtFile)
    '        Reader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.FixedWidth
    '        Reader.SetFieldWidths(1, 10, 1, 5, 1, 10, 1, 8, 1, 20, 1, 4, 1, 4, 1, 3, 1, 4, 1, 10, 1, 40, 1, 18, 1, 3, 1, 10, 1, 11, 1, 5, 1, 5, 1, 3, 1, 18, 1, 3, 1, 18, 1, 3, 1, 12, 1, 12, 1, 14, 1, 5, 1, 11, 1, 11)

    '        Reader.ReadLine()

    '        Do While True
    '            Try
    '                currentRow = Reader.ReadFields()
    '                Exit Do
    '            Catch ex As Exception
    '                Reader.ReadLine()
    '            End Try
    '        Loop

    '        'currentRow = Reader.ReadFields()

    '        Do While Not IsNumeric(currentRow(1))
    '            currentRow = Reader.ReadFields()
    '        Loop

    '        While Not Reader.EndOfData
    '            Try
    '                If (currentRow(0) <> "-") Then
    '                    Me.RunSQLAccess("Insert Into tmpOpenOrders Values(" & _
    '                                    IIf(Len(currentRow(1)) = 0, "0", currentRow(1)) & "," & _
    '                                    IIf(Len(currentRow(3)) = 0, "0", currentRow(3)) & "," & _
    '                                    "'" & Replace(Replace(currentRow(5), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(7), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(9), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(11), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(13), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(15), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(17), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(19), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(21), "'", ""), ",", "") & "'," & _
    '                                    IIf(Len(currentRow(23)) = 0, "0", Format(Val(currentRow(23)), "#########0.00")) & "," & _
    '                                    "'" & Replace(Replace(currentRow(25), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(27), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(29), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(31), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(33), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(35), "'", ""), ",", "") & "'," & _
    '                                    IIf(Len(currentRow(37)) = 0, "0", Format(Val(currentRow(37)), "#########0.00")) & "," & _
    '                                    "'" & Replace(Replace(currentRow(39), "'", ""), ",", "") & "'," & _
    '                                    IIf(Len(currentRow(41)) = 0, "0", Format(Val(currentRow(41)), "#########0.00")) & "," & _
    '                                    "'" & Replace(Replace(currentRow(43), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(45), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(47), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(49), "'", ""), ",", "") & "'," & _
    '                                    IIf(Right(currentRow(51), 1) = "-", "-" & Left(currentRow(51), Len(currentRow(51)) - 1), currentRow(51)) & "," & _
    '                                    "'" & Replace(Replace(currentRow(53), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(55), "'", ""), ",", "") & "'," & _
    '                                    "'" & Replace(Replace(currentRow(1) + currentRow(3) + Left(SAPBox, 3), "'", ""), ",", "") & "'," & _
    '                                    "'" & Left(SAPBox, 3) & "')")
    '                    currentRow = Reader.ReadFields()
    '                End If

    '            Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
    '                MsgBox("Line " & ex.Message & _
    '                "is not valid and will be skipped.")
    '            End Try
    '        End While
    '    End Using


    '    'Update Server with new Open Orders
    '    'Table = Me.GetAccessTable("Select Purch_Doc, Item From tmpOpenOrders Where (Purch_Doc,Item) not in Select Purch_Doc,Item From dbo_Open_Orders")

    'End Function

    'Public Function GetUserPassword(ByRef SAPBox$) As String
    '    Try
    '        Return DeCode(Me.GetAccessTable("Select user" & Trim(Left(SAPBox, 3)) & " From dbo_Users Where UserId = '" & Me.GetUserId & "'").Tables(0).Rows(0).Item(0))
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        MsgBox("Error getting user password: [Funtion GetUserPassord]", MsgBoxStyle.Critical)
    '    End Try
    'End Function

    'Public Function Encode(ByVal lsString) As String
    '    Dim i%
    '    Dim Codigo$
    '    Dim Largo%

    '    Codigo = ""
    '    Largo = Len(lsString)

    '    For i = 0 To Largo - 1
    '        Codigo = Codigo & Chr(Asc(Left(lsString, 1)) + Largo)
    '        lsString = Right(lsString, (Len(lsString) - 1))
    '    Next i

    '    Encode = Codigo
    'End Function

    'Public Function DeCode(ByVal lsString) As String
    '    Dim i%
    '    Dim Codigo$
    '    Dim Largo%
    '    Codigo = ""
    '    Largo = Len(lsString)

    '    For i = 0 To Largo - 1
    '        Codigo = Codigo & Chr(Asc(Left(lsString, 1)) - Largo)
    '        lsString = Right(lsString, (Len(lsString) - 1))
    '    Next i

    '    Return Codigo

    'End Function
    ''' <summary>
    ''' Exporta un datatable a un archivo de Excel
    ''' </summary>
    ''' <param name="Table">DataTable que será exportada a Excel</param>
    ''' <param name="SaveDataTableAs">Nombre del archivo en que se guardarán los datos</param>
    ''' 
    Public Function ExportDataTableToXL(ByVal Table As DataTable, ByVal SaveDataTableAs As String) As Boolean

        Dim xl As New MyOffice.MSExcel

        Return xl.ExportToExcel(Table, SaveDataTableAs)

        ' ''Dim xl As New Microsoft.Office.Interop.Excel.Application
        ' ''Dim i, j As Integer

        ' ''Try
        ' ''    'xl = CreateObject("Excel.Application")
        ' ''    'xl.Visible = True
        ' ''    xl.Workbooks.Add()

        ' ''    If Len(Dir(SaveDataTableAs)) > 0 Then
        ' ''        Kill(SaveDataTableAs)
        ' ''    End If

        ' ''    For i = 0 To Table.Columns.Count - 1
        ' ''        xl.Cells(1, i + 1).value = Table.Columns(i).ColumnName
        ' ''        xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
        ' ''    Next i

        ' ''    For i = 0 To Table.Rows.Count - 1
        ' ''        For j = 0 To Table.Columns.Count - 1
        ' ''            If Table.Rows(i).Item(j).GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
        ' ''                xl.Cells(i + 2, j + 1).value = Microsoft.VisualBasic.Format(Table.Rows(i).Item(j), "dd/MM/yyyy")
        ' ''            Else
        ' ''                xl.Cells(i + 2, j + 1).value = Table.Rows(i).Item(j).ToString
        ' ''            End If
        ' ''        Next j
        ' ''    Next i

        ' ''    xl.ActiveWorkbook.SaveAs(SaveDataTableAs)
        ' ''    xl.ActiveWorkbook.Close()
        ' ''    xl.Quit()

        ' ''    ExportDataTableToXL = True

        ' ''Catch ex As Exception
        ' ''    MsgBox("Fail to export datatable to MS Excel" & Chr(13) & Chr(13) & ex.Message, MsgBoxStyle.Critical)
        ' ''    ExportDataTableToXL = False

        ' ''Finally
        ' ''    xl = Nothing
        ' ''End Try
    End Function
    Public Function GetScorecardSumary(ByVal Table As DataTable) As Boolean
        Dim xl As New MyOffice.MSExcel

        Return xl.GetScorecardSummary(Table)
    End Function
    ''' <summary>
    ''' Exporta un datatable a un archivo de Excel
    ''' </summary>
    ''' <param name="Table">DataTable que será exportada a Excel</param>
    ''' Esta función es para controlar el cuadro de dialogo
    Public Function ExportDataTableToXL(ByVal Table As DataTable) As Boolean
        Dim xl As New MyOffice.MSExcel

        Return xl.ExportToExcel(Table)
        'MsgBox("Done")


        ' '' ''Dim xl As New Microsoft.Office.Interop.Excel.Application
        ' '' ''Dim i, j As Integer
        ' '' ''Dim SaveFile As New SaveFileDialog
        ' '' ''Dim FileName As String = ""

        ' '' ''Try
        ' '' ''    SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        ' '' ''    SaveFile.Filter = "Archivos de Excel (*.xls)|*.xls"

        ' '' ''    If (SaveFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
        ' '' ''        FileName = SaveFile.FileName
        ' '' ''    Else
        ' '' ''        MsgBox("Acción cancelada por el ususario.")
        ' '' ''        Return False
        ' '' ''        Exit Function
        ' '' ''    End If

        ' '' ''    'xl = CreateObject("Excel.Application")
        ' '' ''    'xl.Visible = True
        ' '' ''    xl.Workbooks.Add()

        ' '' ''    If Len(Dir(FileName)) > 0 Then
        ' '' ''        Kill(FileName)
        ' '' ''    End If


        ' '' ''    'Table.WriteXml(FileName)
        ' '' ''    xl.Visible = True

        ' '' ''    For i = 0 To Table.Columns.Count - 1
        ' '' ''        xl.Cells(1, i + 1).value = Table.Columns(i).ColumnName
        ' '' ''        xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
        ' '' ''    Next i



        ' '' ''    For i = 0 To Table.Rows.Count - 1
        ' '' ''        For j = 0 To Table.Columns.Count - 1

        ' '' ''            If Table.Rows(i).Item(j).GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
        ' '' ''                xl.Cells(i + 2, j + 1).value = Format(Table.Rows(i).Item(j), "dd/MM/yyyy")
        ' '' ''            Else
        ' '' ''                xl.Cells(i + 2, j + 1).value = Table.Rows(i).Item(j).ToString
        ' '' ''            End If

        ' '' ''        Next j
        ' '' ''    Next i

        ' '' ''    xl.ActiveWorkbook.SaveAs(FileName)
        ' '' ''    xl.ActiveWorkbook.Close()
        ' '' ''    xl.Quit()

        ' '' ''    ExportDataTableToXL = True

        ' '' ''Catch ex As Exception
        ' '' ''    MsgBox("Fail to export datatable to MS Excel" & Chr(13) & Chr(13) & ex.Message, MsgBoxStyle.Critical)
        ' '' ''    ExportDataTableToXL = False

        ' '' ''Finally
        ' '' ''    xl = Nothing

        ' '' ''End Try

        'Dim SaveFile As New SaveFileDialog
        'Dim Path As String = ""

        'SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        'SaveFile.Filter = "Archivos de Excel (*.xls)|*.xls"

        'If (SaveFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
        '    Path = SaveFile.FileName
        'Else
        '    MsgBox("Acción cancelada por el ususario.")
        '    Return False
        '    Exit Function
        'End If

        'If My.Computer.FileSystem.FileExists(Path) Then
        '    Try
        '        My.Computer.FileSystem.DeleteFile(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin, FileIO.UICancelOption.ThrowException)
        '    Catch ex As Exception
        '        Return False
        '        Exit Function
        '    End Try
        'End If

        'Dim AP As New Microsoft.Office.Interop.Excel.Application
        'Dim WB As Microsoft.Office.Interop.Excel.Workbook = AP.Workbooks.Add
        'Dim WS As Microsoft.Office.Interop.Excel.Worksheet = WB.Sheets(1)
        'Dim dc As DataColumn
        'Dim iCols As Int32 = 0

        'For Each dc In Table.Columns
        '    WS.Range("A1").Offset(0, iCols).Value = dc.ColumnName
        '    iCols += 1
        'Next
        'With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols)).Interior
        '    .Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid
        '    .PatternColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic
        '    .ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorLight2
        '    .TintAndShade = 0.399975585192419
        '    .PatternTintAndShade = 0
        'End With

        'With WS.Range(WS.Cells(1, 1), WS.Cells(1, iCols)).Font
        '    .ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1
        '    .TintAndShade = 0
        'End With
        'Dim iRows As Int32
        'For iRows = 0 To Table.Rows.Count - 1
        '    WS.Range("A2").Offset(iRows).Resize(1, iCols).Value = Table.Rows(iRows).ItemArray()
        'Next

        'WS.Columns.AutoFit()
        'WS.Range("A1").Select()

        'Try
        '    WB.SaveAs(Path)
        'Catch ex As Exception
        '    Return False
        'End Try

        'WB.Close(False)
        'AP.Quit()
    End Function
    ''' <summary>
    ''' Exporta un datatable a un archivo de Excel
    ''' </summary>
    ''' <param name="Table">DataTable que será exportada a Excel</param>
    ''' Esta función es para controlar el cuadro de dialogo
    Public Function ExportDataTableToXLFromClipBoard(ByVal Table As DataTable) As Boolean

        Dim xl As New Microsoft.Office.Interop.Excel.Application
        Dim i, j As Integer
        Dim SaveFile As New SaveFileDialog
        Dim FileName As String = ""

        PutTable(Table)

        Try
            SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            SaveFile.Filter = "Archivos de Excel (*.xls)|*.xls"

            If (SaveFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                FileName = SaveFile.FileName
            Else
                MsgBox("Acción cancelada por el ususario.")
                Return False
                Exit Function
            End If

            'xl = CreateObject("Excel.Application")
            'xl.Visible = True
            xl.Workbooks.Add()

            If Len(Dir(FileName)) > 0 Then
                Kill(FileName)
            End If

            For i = 0 To Table.Columns.Count - 1
                xl.Cells(1, i + 1).value = Table.Columns(i).ColumnName
                xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
            Next i

            xl.Range("A2").Select()
            xl.ActiveSheet.Paste()

            'For i = 0 To Table.Rows.Count - 1
            '    For j = 0 To Table.Columns.Count - 1

            '        If Table.Rows(i).Item(j).GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
            '            xl.Cells(i + 2, j + 1).value = Format(Table.Rows(i).Item(j), "dd/MM/yyyy")
            '        Else
            '            xl.Cells(i + 2, j + 1).value = Table.Rows(i).Item(j).ToString
            '        End If

            '    Next j
            'Next i

            xl.ActiveWorkbook.SaveAs(FileName)
            xl.ActiveWorkbook.Close()
            xl.Quit()

            ExportDataTableToXLFromClipBoard = True

        Catch ex As Exception
            MsgBox("Fail to export datatable to MS Excel" & Chr(13) & Chr(13) & ex.Message, MsgBoxStyle.Critical)
            ExportDataTableToXLFromClipBoard = False

        Finally
            xl = Nothing

        End Try
    End Function
    ''' <summary>
    ''' Exporta un datagrid a un archivo de Excel
    ''' </summary>
    ''' <param name="Grid">DataTable que será exportada a Excel</param>
    ''' Esta función es para controlar el cuadro de dialogo
    Public Function ExportDataGridToXL(ByVal Grid As Windows.Forms.DataGridView) As Boolean
        Dim xl As Object 'New Microsoft.Office.Interop.Excel.Application
        Dim i, j As Integer
        Dim SaveFile As New SaveFileDialog
        Dim FileName As String = ""
        Dim ActiveColumns As Integer = 0


        Try
            SaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            SaveFile.Filter = "Archivos de Excel (*.xls)|*.xls"

            If (SaveFile.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                FileName = SaveFile.FileName
            Else
                MsgBox("Acción cancelada por el ususario.")
                Return False
                Exit Function
            End If

            xl = CreateObject("Excel.Application")
            'xl.Visible = True
            xl.Workbooks.Add()

            If Len(Dir(FileName)) > 0 Then
                Kill(FileName)
            End If

            'For i = 0 To Grid.Columns.Count - 1
            '    If Grid.Columns(i).Visible Then
            '        ActiveColumns += 1
            '    End If
            'Next


            Dim Column As DataGridViewColumn
            i = 0
            For Each Column In Grid.Columns
                If Column.Visible Then
                    xl.Cells(1, i + 1).value = Column.Name
                    xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
                    i += 1
                End If
            Next

            'For i = 0 To ActiveColumns
            '    If Grid.Columns(i).Visible Then
            '        xl.Cells(1, i + 1).value = Grid.Columns(i).Name
            '        xl.Cells(1, i + 1).interior.color = RGB(192, 192, 192)
            '    End If
            'Next i

            'xl.visible = True


            Dim Row As DataGridViewRow
            Dim Cell As DataGridViewCell

            i = 0

            For Each Row In Grid.Rows
                j = 0
                For Each Cell In Row.Cells
                    If Cell.Visible Then
                        If Cell.GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
                            xl.Cells(i + 2, j + 1).value = Microsoft.VisualBasic.Format(Cell.Value, "dd/MM/yyyy")
                        Else
                            xl.Cells(i + 2, j + 1).value = Cell.Value
                        End If
                        j += 1
                    End If
                Next
                i += 1
            Next


            'For i = 0 To Grid.Rows.Count - 1
            '    For j = 0 To ActiveColumns
            '        If Grid.Columns(j).Visible Then
            '            If Grid.Rows(i).Cells(j).GetType.ToString.ToUpper = "SYSTEM.DATETIME" Then
            '                xl.Cells(i + 2, j + 1).value = Format(Grid.Rows(i).Cells(j).Value, "dd/MM/yyyy")
            '            Else
            '                xl.Cells(i + 2, j + 1).value = Grid.Rows(i).Cells(j).Value
            '            End If
            '        End If
            '    Next j
            'Next i

            xl.ActiveWorkbook.SaveAs(FileName)
            xl.ActiveWorkbook.Close()
            xl.Quit()

            ExportDataGridToXL = True

        Catch ex As Exception
            MsgBox("Fail to export datatable to MS Excel" & Chr(13) & Chr(13) & ex.Message, MsgBoxStyle.Critical)
            ExportDataGridToXL = False

        Finally
            xl = Nothing
        End Try
    End Function
    'Public Function ImportInforecords(ByVal txtFile$) As Boolean
    '    Dim currentRow As String()
    '    Dim Table As New DataSet

    '    'Me.RunSQLAccess("Delete From tmpOpenOrders")

    '    Using Reader As New Microsoft.VisualBasic.FileIO.TextFieldParser(txtFile)
    '        Reader.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.FixedWidth
    '        Reader.SetFieldWidths(10, 18, 4, 4, 10, 1, 63, 5, 5, 23, 30, 38, 3, 3, 23, 12, 4, 4, 4, 19, 2, 7, 6, 6, 6, 3, 427, 5, 15, 8, 8, 3, 8, 3)

    '        'Reader.ReadLine()

    '        Do While True
    '            Try
    '                currentRow = Reader.ReadFields()
    '                Exit Do
    '            Catch ex As Exception
    '                Reader.ReadLine()
    '            End Try
    '        Loop

    '        'currentRow = Reader.ReadFields()

    '        Do While Not IsNumeric(currentRow(1))
    '            currentRow = Reader.ReadFields()
    '        Loop

    '        While Not Reader.EndOfData

    '            currentRow = Reader.ReadFields()
    '        End While
    '    End Using


    '    'Update Server with new Open Orders
    '    'Table = Me.GetAccessTable("Select Purch_Doc, Item From tmpOpenOrders Where (Purch_Doc,Item) not in Select Purch_Doc,Item From dbo_Open_Orders")

    'End Function

    'Public Function GetUserId() As String
    '    Dim UserId$
    '    Dim cn As New DTMConection.coneccion

    '    UserId = ""

    '    If Len(UCase(System.Environment.UserName.ToString)) = 6 Then
    '        UserId = UCase(System.Environment.UserName.ToString)
    '    Else
    '        UserId = UCase(cn.GetAccessTable("Select UserID From dbo_users Where ucase(UserShortName) = '" & UCase(System.Environment.UserName.ToString) & "'").Tables(0).Rows(0).Item(0))
    '    End If

    '    Return UserId
    'End Function
    Public Function SendOutlookMail(ByVal Subject As String, ByVal Attachment() As String, ByVal recipient As Object, ByVal copyto As Object, ByVal BodyText As String, ByVal SentOnBehalfOf As Object, ByVal ReturnReceipt As Boolean, ByVal FormatMail$, Optional ByVal BCC As String = "", Optional ByVal SendNow As Boolean = False, Optional ByVal IsSTR As Boolean = False) As Boolean
        Dim objOutlook As Microsoft.Office.Interop.Outlook.Application
        Dim objOutlookMsg As Microsoft.Office.Interop.Outlook.MailItem
        Dim objOutlookRecip As Microsoft.Office.Interop.Outlook.Recipient
        Dim objOutlookAttach As Microsoft.Office.Interop.Outlook.Attachment
        Dim myDestFolder As Microsoft.Office.Interop.Outlook.MAPIFolder
        Dim myTempFolder As Microsoft.Office.Interop.Outlook.MAPIFolder
        Dim TheAddress As String
        Dim myNameSpace As Microsoft.Office.Interop.Outlook.NameSpace
        Dim I%
        Dim J%
        Dim Sent As Boolean = False

        ' Create the Outlook session.
        objOutlook = New Microsoft.Office.Interop.Outlook.Application
        myNameSpace = objOutlook.GetNamespace("MAPI")

        'On Error GoTo ErrHandler
        myTempFolder = myNameSpace.GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderDrafts)
        myDestFolder = myTempFolder.Folders("DBMail")

        objOutlookMsg = objOutlook.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)

        If Not DBNull.Value.Equals(recipient) Then
            TheAddress = CStr(recipient)
        Else
            MsgBox("El destinatario se encuentra en blanco.", MsgBoxStyle.Exclamation)
            SendOutlookMail = False
            Exit Function
        End If

        'Dim Inspector As Microsoft.Office.Interop.Outlook.Inspector
        Dim Inspector = objOutlookMsg.GetInspector

        objOutlookMsg.To = TheAddress

        'Add the attachment to the e-mail message.
        If Not IsNothing(Attachment) And Len(Attachment(0)) > 0 Then 'If Not IsNullOrEmpty(Attachment) And Not IsNothing(Attachment) Then
            For J = 0 To UBound(Attachment)
                If IO.File.Exists(Attachment(J)) Then
                    objOutlookAttach = objOutlookMsg.Attachments.Add(Attachment(J))
                End If
            Next
        End If

        ' Add the Cc recipients to the e-mail message.
        If (Not IsNothing(copyto) Or copyto <> "") Then
            objOutlookMsg.CC = copyto
        End If

        If IsSTR Then 'include OEMS STR ION
            objOutlookMsg.CC = "strmamericas.im@pg.com;" & copyto
            'Else
            '    objOutlookMsg.CC = "selfservamericas.im@pg.com;" & copyto
        End If

        'Add the CC
        If BCC.Length > 0 Then
            objOutlookMsg.BCC = BCC
        End If

        If Not IsNothing(SentOnBehalfOf) And SentOnBehalfOf <> "" Then
            objOutlookMsg.SentOnBehalfOfName = SentOnBehalfOf
        End If

        objOutlookMsg.ReadReceiptRequested = ReturnReceipt

        ' Set the Subject, the Body, and the Importance of the e-mail message.
        objOutlookMsg.Subject = Subject
        If FormatMail = "TXT" Then
            objOutlookMsg.Body = BodyText & objOutlookMsg.Body
            objOutlookMsg.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatRichText
        Else
            objOutlookMsg.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML
            '.HTMLBody = Replace(BodyText, Chr(13), "<br>") & .Body
            objOutlookMsg.HTMLBody = Replace(BodyText, Chr(13), "<br>") & objOutlookMsg.HTMLBody
        End If

        objOutlookMsg.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh 'High importance

        ''Add the attachment to the e-mail message.
        'If Not IsNothing(Attachment) And Len(Attachment(0)) > 0 Then 'If Not IsNullOrEmpty(Attachment) And Not IsNothing(Attachment) Then
        '    For J = 0 To UBound(Attachment)
        '        If IO.File.Exists(Attachment(J)) Then
        '            objOutlookAttach = .Attachments.Add(Attachment(J))
        '        End If
        '    Next
        'End If
        Inspector = objOutlookMsg.GetInspector
        objOutlookMsg.Save()

        If SendNow Then
            objOutlookMsg.Send()
        Else
            objOutlookMsg.Move(myDestFolder)
        End If

        objOutlookMsg = Nothing
        objOutlook = Nothing
        Sent = True

        Return Sent
ErrHandler:
        If Err.Number = -2147221233 Then
            myDestFolder = myTempFolder.Folders.Add("DBMail")
            Resume Next
        Else
            MsgBox(Err.Number & " " & Err.Description)
        End If

        Return Sent

    End Function
    Public Function VerificarContratosPorVencer() As Boolean
        Dim Contratos As New DataTable
        Dim PorVencer As New DataTable
        Dim File(0) As String
        Dim S_File(0) As String
        Dim FPath$
        Dim BT$
        Dim lsSQL$
        'Dim Subject$


        FPath = My.Application.Info.DirectoryPath.ToString()
        BT = My.Computer.FileSystem.ReadAllText(FPath & "\Stationaries\PorVencer.txt", System.Text.Encoding.Default)

        File(0) = My.Application.Info.DirectoryPath & "\ContratosPorVencer.xls"
        S_File(0) = My.Application.Info.DirectoryPath & "\ContratosMOAPorVencer.xls"
        Contratos = Me.RunSentence("Select distinct(OwnerMail) as OwnerMail From vstContratosPorVencer").Tables(0)

        'Envio de contratos a compradores
        If Contratos.Rows.Count > 0 Then
            Dim i%
            Dim J%
            For i = 0 To Contratos.Rows.Count - 1
                PorVencer = Me.RunSentence("Select OA, Planta,Vendor, Name, Owner, LastUpdate From vstContratosPorVencer Where OwnerMail = '" & Contratos.Rows(i).Item("OwnerMail") & "'").Tables(0)
                If PorVencer.Rows.Count > 0 Then
                    If Me.ExportDataTableToXL(PorVencer, File(0)) Then
                        If Me.SendOutlookMail("***Urgente: Contratos por Vencer ***", File, Contratos.Rows(i).Item("OwnerMail"), "", BT, "", False, "TXT") Then
                            For J = 0 To PorVencer.Rows.Count - 1
                                lsSQL = "Insert into MailsSended(Send,OA,Planta,Vendor,Name,Owner,LastUpdate,Subject) Values('" & CStr(Today) & "'," & PorVencer.Rows(J).Item("OA") & ",'" & PorVencer.Rows(J).Item("Planta") & "'," & PorVencer.Rows(J).Item("Vendor") & ",'" & PorVencer.Rows(J).Item("Name") & "','" & PorVencer.Rows(J).Item("Owner") & "','" & CStr(PorVencer.Rows(J).Item("LastUpdate")) & "','Contrato por Vencer')"
                                Me.ExecuteInServer(lsSQL)
                            Next
                        End If
                    End If
                End If
            Next
        End If

        'Envio de los contrato con MOA a Sourcign
        Contratos = Me.RunSentence("Select distinct(SourcingMail) From vstContratosPorVencer Where sourcingMail is not null").Tables(0)
        If Contratos.Rows.Count > 0 Then
            Dim i%
            Dim J%
            For i = 0 To Contratos.Rows.Count - 1
                PorVencer = Me.RunSentence("Select OA, Planta,Vendor, Name, Sourcing, LastUpdate,SourcingMail From vstContratosPorVencer Where SourcingMail = '" & Contratos.Rows(i).Item("sourcingMail") & "'").Tables(0)
                If PorVencer.Rows.Count > 0 Then
                    If Me.ExportDataTableToXL(PorVencer, S_File(0)) Then
                        If Me.SendOutlookMail("***Urgente: Contratos por Vencer ***", S_File, Contratos.Rows(i).Item("SourcingMail"), "", BT, "", False, "TXT") Then
                            For J = 0 To PorVencer.Rows.Count - 1
                                lsSQL = "Insert into MailsSended(Send,OA,Planta,Vendor,Name,Owner,LastUpdate,Subject) Values('" & CStr(Today) & "'," & PorVencer.Rows(J).Item("OA") & ",'" & PorVencer.Rows(J).Item("Planta") & "'," & PorVencer.Rows(J).Item("Vendor") & ",'" & PorVencer.Rows(J).Item("Name") & "','" & PorVencer.Rows(J).Item("Sourcing") & "','" & CStr(PorVencer.Rows(J).Item("LastUpdate")) & "','Contrato por Vencer')"
                                Me.ExecuteInServer(lsSQL)
                            Next
                        End If
                    End If
                End If
            Next
        End If

        'Seteo del paso del contrato
        Contratos = Me.RunSentence("Select oa From vstContratosPorVencer Where InProcess = 0 or InProcess is null").Tables(0)
        If Contratos.Rows.Count > 0 Then
            Dim i%
            For i = 0 To Contratos.Rows.Count - 1
                Me.AddStepToTrackingOA(Contratos.Rows(i).Item("OA"), "1", False)
            Next
        End If
    End Function
    Public Function VerificarContratosVencidos() As Boolean
        Dim Contratos As New DataTable
        Dim PorVencer As New DataTable
        Dim File(0) As String
        Dim FPath$
        Dim BT$
        Dim lsSQL$

        FPath = My.Application.Info.DirectoryPath.ToString()
        BT = My.Computer.FileSystem.ReadAllText(FPath & "\Stationaries\Vencidos.txt")

        File(0) = My.Application.Info.DirectoryPath & "\ContratosVencidos.xls"
        Contratos = Me.RunSentence("Select distinct(OwnerMail) as OwnerMail From vstContratosVencidos").Tables(0)

        If Contratos.Rows.Count > 0 Then
            Dim i%
            Dim J%
            For i = 0 To Contratos.Rows.Count - 1
                PorVencer = Me.RunSentence("Select OA, Planta,Vendor, Name, Owner, LastUpdate  From vstContratosVencidos Where OwnerMail = '" & Contratos.Rows(i).Item("OwnerMail") & "'").Tables(0)
                If PorVencer.Rows.Count > 0 Then
                    If Me.ExportDataTableToXL(PorVencer, File(0)) Then
                        If Me.SendOutlookMail("***Urgente: Contratos Vencidos ***", File, Contratos.Rows(i).Item("OwnerMail"), "", BT, "", False, "TXT") Then
                            For J = 0 To PorVencer.Rows.Count - 1
                                lsSQL = "Insert into MailsSended(Send,OA,Planta,Vendor,Name,Owner,LastUpdate,Subject) Values('" & CStr(Today) & "'," & PorVencer.Rows(J).Item("OA") & ",'" & PorVencer.Rows(J).Item("Planta") & "'," & PorVencer.Rows(J).Item("Vendor") & ",'" & PorVencer.Rows(J).Item("Name") & "','" & PorVencer.Rows(J).Item("Owner") & "','" & CStr(PorVencer.Rows(J).Item("LastUpdate")) & "','Contrato Vencido')"
                                Me.ExecuteInServer(lsSQL)
                            Next
                        End If
                    End If
                End If
            Next
        End If


        Contratos = Me.RunSentence("Select oa From vstContratosVencidos Where InProcess = 0 or InProcess is null").Tables(0)
        If Contratos.Rows.Count > 0 Then
            Dim i%
            For i = 0 To Contratos.Rows.Count - 1
                Me.AddStepToTrackingOA(Contratos.Rows(i).Item("OA"), "2", False)
            Next
        End If

    End Function
    Public Function VerificarContratosSinOwner() As Boolean
        Dim ContratosSinOwner As New DataTable
        Dim Owner As New DataTable
        Dim File(0) As String
        Dim FPath$
        Dim BT$
        Dim lsSQL$

        FPath = My.Application.Info.DirectoryPath.ToString()
        BT = My.Computer.FileSystem.ReadAllText(FPath & "\Stationaries\SinOwner.txt")

        File(0) = My.Application.Info.DirectoryPath & "\ContratosSinOwner.xlsx"
        Owner = Me.RunSentence("Select distinct(spock) as spock, Mail From vstContratosSinOwner").Tables(0)

        If Owner.Rows.Count > 0 Then
            Dim i%
            Dim J%
            For i = 0 To Owner.Rows.Count - 1
                ContratosSinOwner = Me.RunSentence("Select OA, Plant,PlantName,Vendor, VendorName, Region, Country  From vstContratosSinOwner Where spock = '" & Owner.Rows(i).Item("spock") & "'").Tables(0)
                If ContratosSinOwner.Rows.Count > 0 Then
                    If Me.ExportDataTableToXL(ContratosSinOwner, File(0)) Then
                        If Me.SendOutlookMail("***Urgente: Contratos Sin Owner ***", File, Owner.Rows(i).Item("Mail"), "", BT, "", False, "TXT") Then
                            For J = 0 To ContratosSinOwner.Rows.Count - 1
                                lsSQL = "Insert into MailsSended(Send,OA,Planta,Vendor,Name,Owner,Subject) Values('" & CStr(Today) & "'," & ContratosSinOwner.Rows(J).Item("OA") & ",'" & ContratosSinOwner.Rows(J).Item("Plant") & "'," & ContratosSinOwner.Rows(J).Item("Vendor") & ",'" & ContratosSinOwner.Rows(J).Item("VendorName") & "','" & Owner.Rows(i).Item("spock") & "','Contratos sin owner')"
                                Me.ExecuteInServer(lsSQL)
                            Next
                        End If
                    End If
                End If
            Next
        End If

        Me.KillProcess("Excel")
    End Function
    Public Function VerificarSinOwner() As Boolean
        Dim Contratos As New DataTable
        Dim PorVencer As New DataTable
        Dim File(0) As String
        Dim FPath$
        Dim BT$
        Dim lsSQL$

        FPath = My.Application.Info.DirectoryPath.ToString()
        BT = My.Computer.FileSystem.ReadAllText(FPath & "\Stationaries\SinOwner.txt")

        File(0) = My.Application.Info.DirectoryPath & "\ContratosVencidos.xlsx"
        Contratos = Me.RunSentence("Select distinct(Owner) as Owner, OwnerMail From vstContratosSinOwner").Tables(0)

        If Contratos.Rows.Count > 0 Then
            Dim i%
            Dim J%
            For i = 0 To Contratos.Rows.Count - 1
                PorVencer = Me.RunSentence("Select OA, Planta,Vendor, Name, Owner, LastUpdate  From vstContratosVencidos Where Owner = '" & Contratos.Rows(i).Item("Owner") & "'").Tables(0)
                If PorVencer.Rows.Count > 0 Then
                    If Me.ExportDataTableToXL(PorVencer, File(0)) Then
                        If Me.SendOutlookMail("***Urgente: Contratos Vencidos ***", File, Contratos.Rows(i).Item("OwnerMail"), "", BT, "", False, "TXT") Then
                            For J = 0 To PorVencer.Rows.Count - 1
                                lsSQL = "Insert into MailsSended(Send,OA,Planta,Vendor,Name,Owner,LastUpdate,Subject) Values('" & CStr(Today) & "'," & PorVencer.Rows(J).Item("OA") & ",'" & PorVencer.Rows(J).Item("Planta") & "'," & PorVencer.Rows(J).Item("Vendor") & ",'" & PorVencer.Rows(J).Item("Name") & "','" & PorVencer.Rows(J).Item("Owner") & "','" & CStr(PorVencer.Rows(J).Item("LastUpdate")) & "','Contrato Vencido')"
                                Me.ExecuteInServer(lsSQL)
                            Next
                        End If
                    End If
                End If
            Next
        End If
    End Function
    ''' <summary>
    ''' Realiza un volcado de un Data Table a una tabla de SQL Server
    ''' </summary>
    ''' <param name="TableInServer">Nombre de la tabla en la que se volcarán los datos</param>
    ''' <param name="DataTable">Data Table con la información a ser enviada al servidor</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AppendTableToSqlServer(ByVal TableInServer$, ByVal DataTable As DataTable) As Boolean
        Dim SqlBulkCopy As System.Data.SqlClient.SqlBulkCopy
        SqlBulkCopy = New SqlClient.SqlBulkCopy(GetConnectionString)

        Try
            SqlBulkCopy.BulkCopyTimeout = 10000
            SqlBulkCopy.DestinationTableName = TableInServer
            'ExportDataTableToXL(DataTable)
            SqlBulkCopy.WriteToServer(DataTable)
            Return True
        Catch ex As Exception
            'Throw New Exception(ex.Message)
            'MsgBox(ex.Message)
            Return False
        Finally
            SqlBulkCopy.Close()
        End Try

        SqlBulkCopy = Nothing
    End Function
    Public Function TextFile_Data(ByVal Path As String) As DataTable

        TextFile_Data = Nothing
        Try
            Dim F As New Microsoft.VisualBasic.FileIO.TextFieldParser(Path)
            F.TextFieldType = FileIO.FieldType.Delimited
            F.SetDelimiters(Chr(9))
            Dim R As String()
            Dim D As New DataTable
            Dim CI As Integer

            R = F.ReadFields
            If R.Length > 0 Then
                CI = 1
                For Each CN As String In R
                    Do While Not D.Columns(CN) Is Nothing
                        CN = CN & CI
                        CI += 1
                    Loop
                    D.Columns.Add(CN, Type.GetType("System.String"))
                Next
            End If

            While Not F.EndOfData
                R = F.ReadFields
                D.LoadDataRow(R, True)
            End While

            F.Close()

            TextFile_Data = D

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Function
    Public Function AppendTableToAccess(ByVal TableInServer$, ByVal DataTable As System.Data.DataTable) As Boolean
        Dim SqlBulkCopy As System.Data.SqlClient.SqlBulkCopy
        Try
            SqlBulkCopy = New SqlClient.SqlBulkCopy("Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " & My.Application.Info.DirectoryPath & "\OAData\New.mdb")
            SqlBulkCopy.DestinationTableName = TableInServer
            SqlBulkCopy.WriteToServer(DataTable)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
    Public Function GetUserId() As String
        Return Environ("USERID")

        'If Len(System.Environment.UserName.ToString) = 6 AndAlso (System.Environment.UserName.ToString.IndexOf(".") = -1) Then
        '    Return UCase(System.Environment.UserName.ToString)
        'Else
        '    TUser = Me.RunSentence("Select TNumber From users Where UserShortName = '" & UCase(System.Environment.UserName.ToString) & "'").Tables(0)

        '    If TUser.Rows.Count = 0 Then
        '        MsgBox("User: " & UCase(System.Environment.UserName.ToString) & ", do not have access to this tool." & Chr(13) & Chr(13) & "Please contact you spoc.", MsgBoxStyle.Critical)
        '    Else
        '        Return TUser.Rows(0).Item("Tnumber")
        '    End If
        'End If
    End Function
    Public Function GetUserMail() As String
        Dim TMail As DataTable

        TMail = Me.RunSentence("Select Mail from users where TNumber = '" & Trim(Me.GetUserId) & "'").Tables(0)

        If TMail.Rows.Count = 0 Then
            MsgBox("User: " & UCase(System.Environment.UserName.ToString) & ", not found in database.", MsgBoxStyle.Critical)
            Return ""
        Else
            Return TMail.Rows(0).Item("Mail")
        End If

    End Function
    Public Function GetUserName() As String
        Dim TMail As DataTable

        TMail = Me.RunSentence("Select Nombre from users where TNumber = '" & Trim(Me.GetUserId) & "'").Tables(0)

        If TMail.Rows.Count = 0 Then
            MsgBox("User: " & UCase(System.Environment.UserName.ToString) & ", not found in database.", MsgBoxStyle.Critical)
            Return ""
        Else
            Return TMail.Rows(0).Item("Nombre")
        End If

    End Function
    ''' <summary>
    ''' Verifica si el usuario tiene realcionado en la tabla de distribución algún contrato
    ''' </summary>
    ''' <param name="Vendor">Código del proveedor que se buscará</param>
    Public Function CanUpdateOA(ByVal Vendor%) As Boolean
        Dim MisContratos As New DataTable

        MisContratos = Me.RunSentence("Select * From vstOwners Where OwnerMail = '" & Me.GetUserMail() & "'").Tables(0)
        If MisContratos.Rows.Count > 0 Then
            CanUpdateOA = True
        Else
            CanUpdateOA = False
        End If
    End Function
    ''' <summary>
    ''' Setea un contrato a un sigiente paso y cierra el paso que se encuentra activo
    ''' <code>True= El usuario inicia el proceso; False= El proceso no inicia</code>
    ''' </summary>
    ''' <param name=" Contrato ">Código del contrato que se actualizará</param>
    ''' <param name="OAStep" >Numero del paso al que se pasará</param>
    ''' <param name="IniciaProceso">Determina si es el usuaio quien inicia el proceso de cotización</param>
    Public Function AddStepToTrackingOA(ByVal Contrato$, ByVal OAStep$, ByVal IniciaProceso As Boolean) As Boolean

        If IniciaProceso Then
            Me.ExecuteInServer("Update Contratos Set InProcess = 1,Estado = " & OAStep & " Where OA = " & Contrato)
        End If

        Me.ExecuteInServer("update TrackingSteps set FechaFin = { fn NOW() }, PasaAlStep = " & OAStep & " Where OA = " & Contrato & " and FechaFin is null")
        Me.ExecuteInServer("Insert into TrackingSteps(FechaInicio,OA,Step,Usuario) Values({ fn NOW() }," & Contrato & "," & OAStep & ",'" & Me.GetUserId & "')")
    End Function
    ''' <summary>
    ''' Agrega un registro en la base de datos de una requi que se actualizó
    ''' </summary>
    ''' <param name="Requisition">Numero de Requisición</param>
    ''' <param name=" Item ">Numero de ítem</param>
    ''' <param name="ErrorCode">Código de error de la requi</param>
    Public Function AddRequiTracking(ByVal Requisition$, ByVal Item$, ByVal ErrorCode$) As Boolean
        Me.ExecuteInServer("Insert into TrackingRequis(Requisition,Item,IdError,Fecha,Usuario) Values(" & Requisition & "," & Item & ",'" & ErrorCode.Trim & "', { fn NOW() },'" & Me.GetUserId & "')")
    End Function
    ''' <summary>
    ''' Setea el contrato como actualizado en la base de datos
    ''' </summary>
    ''' <param name="OA">Código del contrato</param>
    Public Function SetOAUpdate(ByVal OA$) As Boolean
        'Dim FPath$
        'Dim BT$
        Dim T_OA As DataTable
        Dim Attachment(0) As String

        T_OA = Me.RunSentence("Select * From vstOwners Where OA = " & OA).Tables(0)

        If T_OA.Rows.Count > 0 Then

            'FPath = My.Application.Info.DirectoryPath.ToString() & "\stationaries"
            'BT = My.Computer.FileSystem.ReadAllText(FPath & "\OAActualizado.txt", System.Text.Encoding.Default)
            'BT = BT.Replace("@OA", OA)
            'BT = BT.Replace("@VENDOR", T_OA.Rows(0).Item("Vendor") & "-" & T_OA.Rows(0).Item("Name"))

            Me.ExecuteInServer("Update Contratos set InProcess = -1, LastUpdate = {fn Now()},Updated = 1 Where OA = " & OA)
            Me.AddStepToTrackingOA(OA, 6, True)

            'Me.SendOutlookMail("Contrato Actualizado", Attachment, T_OA.Rows(0).Item("OwnerMail"), "", BT, "", False, "TXT")
        Else
            MsgBox("El código de contrato no se ha encontrado en la base de datos", MsgBoxStyle.Information)
        End If

    End Function
    ''' <summary>
    ''' Setea el contrato con problemas para actualizar
    ''' </summary>
    ''' <param name="OA">Código del contrato</param>
    Public Function SetOAWithBSSTicket(ByVal OA$) As Boolean
        Dim FPath$
        Dim BT$
        Dim T_OA As DataTable
        Dim Attachment(0) As String

        T_OA = Me.RunSentence("Select * From vstOwners Where OA = " & OA).Tables(0)

        If T_OA.Rows.Count > 0 Then
            FPath = My.Application.Info.DirectoryPath.ToString() & "\stationaries"
            BT = My.Computer.FileSystem.ReadAllText(FPath & "\OAProblemaBSS.txt", System.Text.Encoding.Default)
            BT = BT.Replace("@OA", OA)
            BT = BT.Replace("@VENDOR", T_OA.Rows(0).Item("Vendor") & "-" & T_OA.Rows(0).Item("Name"))

            Me.ExecuteInServer("Update Contratos set InProcess = -1, LastUpdate = {fn now()}Where OA = " & OA)
            Me.AddStepToTrackingOA(OA, 7, True)

            Me.SendOutlookMail("Contrato con problemas al ser actualizado", Attachment, T_OA.Rows(0).Item("OwnerMail"), "", BT, "", False, "TXT")
        Else
            MsgBox("El código de contrato no se ha encontrado en la base de datos", MsgBoxStyle.Information)
        End If

    End Function
    ''' <summary>
    ''' Coloca la información de un DataTable en el PortaPapeles. 
    ''' <code>El listado que se colocará en el portapapeles es el de la primera columna</code>
    ''' </summary>
    ''' <param name="Tabla">Tabla que tiene la información que será colocada en el PortaPapeles</param>
    Public Function Put_DataTable_In_ClipBoard(ByVal Tabla As DataTable) As Boolean
        Dim Text As String = ""
        Dim i As Integer

        My.Computer.Clipboard.Clear()
        For i = 0 To Tabla.Rows.Count - 1
            Text = Text & Tabla.Rows(i).Item(0) & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(Text)
    End Function
    ''' <summary>
    ''' Coloca la información de un DataTable en el PortaPapeles en formato HTML. 
    ''' </summary>
    ''' <param name="Tabla">Tabla que tiene la información que será colocada en el PortaPapeles</param>
    Public Function Put_HTML_Table_In_ClipBoard(ByVal Tabla As DataTable) As Boolean
        Dim Text As String = ""
        Dim i, j As Integer

        My.Computer.Clipboard.Clear()
        Text = "<Table border=1  BORDERCOLOR=" & Chr(34) & "888888" & Chr(34) & "><tr>"

        For i = 0 To Tabla.Columns.Count - 1
            Text = Text & "<th  bgcolor=" & Chr(34) & "000099" & Chr(34) & ">" & Tabla.Columns(i).ColumnName & "</th>"
        Next i

        For i = 0 To Tabla.Rows.Count - 1
            Text = Text & "<TR>"
            For j = 0 To Tabla.Columns.Count - 1
                Text = Text & "<TD>" & Tabla.Rows(i).Item(j)
            Next
            Text = Text & Chr(13) & Chr(10)
        Next
        Text = Text & "</Table>"
        My.Computer.Clipboard.SetText(Text)
    End Function
    Public Function PutTable(ByVal Tabla As DataTable) As Boolean
        Dim Text As String = ""
        Dim i, j As Integer

        My.Computer.Clipboard.Clear()

        For i = 0 To Tabla.Rows.Count - 1
            For j = 0 To Tabla.Columns.Count - 1
                Text = Text & Tabla.Rows(i).Item(j) & vbTab
            Next
            Text = Text & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(Text)
    End Function
    Public Function AddCheckBoxColumn(ByVal ColumnTitle$, ByVal ColumnName$) As Windows.Forms.DataGridViewCheckBoxColumn
        Dim CheckColumn As New Windows.Forms.DataGridViewCheckBoxColumn

        CheckColumn.HeaderText = ColumnTitle
        CheckColumn.FlatStyle = Windows.Forms.FlatStyle.Popup
        CheckColumn.Width = 45
        CheckColumn.Name = ColumnName

        Return CheckColumn

    End Function
    Public Function AddButtonColumn(ByVal ColumnTitle$, ByVal ColumnName$, ByVal Width As Integer) As Windows.Forms.DataGridViewButtonColumn
        Dim ButtonColumn As New Windows.Forms.DataGridViewButtonColumn
        ButtonColumn.HeaderText = ColumnTitle
        ButtonColumn.FlatStyle = Windows.Forms.FlatStyle.System
        ButtonColumn.Width = Width
        ButtonColumn.Name = ColumnName

        Return ButtonColumn

    End Function
    Public Function AddPictureColumn(ByVal ColumnTitle$, ByVal ColumnName$, ByVal Width As Integer) As Windows.Forms.DataGridViewImageColumn
        Dim Col As New Windows.Forms.DataGridViewImageColumn

        Col.HeaderText = ColumnTitle
        Col.Width = 45
        Col.Name = ColumnName
        Col.ImageLayout = DataGridViewImageCellLayout.NotSet
        Col.ValuesAreIcons = False





        Return Col
    End Function
    Public Function Encode(ByVal lsString) As String
        Dim i%
        Dim Codigo$
        Dim Largo%

        Codigo = ""
        Largo = Len(lsString)

        For i = 0 To Largo - 1
            Codigo = Codigo & Chr(Asc(Left(lsString, 1)) + Largo)
            lsString = Right(lsString, (Len(lsString) - 1))
        Next i

        Encode = Codigo
    End Function
    Public Function DeCode(ByVal lsString) As String
        Dim i%
        Dim Codigo$
        Dim Largo%
        Codigo = ""
        Largo = Len(lsString)

        For i = 0 To Largo - 1
            Codigo = Codigo & Chr(Asc(Left(lsString, 1)) - Largo)
            lsString = Right(lsString, (Len(lsString) - 1))
        Next i

        Return Codigo

    End Function
    ''' <summary>
    ''' Carga un combobox a partir de una istrucción SQL
    ''' </summary>
    ''' <param name="oComboBox">Objeto combobox que será llenado con el SQL</param>
    ''' <param name="SQL">Instrucción SQL con la que se llenará el combo</param>
    ''' <param name="ColumnID">Nombre de la columna que contiene el Valor ID</param>
    ''' <param name="ColumnDisplay">Nombre de la columna con la información que se mostrará al usuario</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadCombo(ByRef oComboBox As ComboBox, ByVal SQL As String, ByVal ColumnID As String, ByVal ColumnDisplay As String) As Boolean
        ' '' ''Try
        ' '' ''    Dim Table As New DataTable

        ' '' ''    Dim ds As DataSet


        ' '' ''    ds = Me.RunSentence(SQL)
        ' '' ''    Table = Me.RunSentence(SQL).Tables(0)


        ' '' ''    oComboBox.DataSource = ds.Tables(0)
        ' '' ''    oComboBox.DisplayMember = ds.Tables(0).Columns(ColumnDisplay).Caption.ToString
        ' '' ''    oComboBox.ValueMember() = ds.Tables(0).Columns(ColumnID).Caption.ToString
        ' '' ''    '    oComboBox.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        ' '' ''    '    oComboBox.ValueMember() = ds.Tables(0).Columns(0).Caption.ToString

        ' '' ''    LoadCombo = True
        ' '' ''Catch ex As Exception
        ' '' ''    MsgBox("Error al intentar cargar el combo: [ " & oComboBox.Name & " ] " & Chr(13) & Chr(13) & "Descripción: " & ex.Message, MsgBoxStyle.Critical)

        ' '' ''End Try

        Try
            Dim Table As New DataTable
            Table = Me.RunSentence(SQL).Tables(0)

            oComboBox.DataSource = Table
            oComboBox.DisplayMember = Table.Columns(ColumnDisplay).Caption.ToString
            oComboBox.ValueMember() = Table.Columns(ColumnID).Caption.ToString

            LoadCombo = True
        Catch ex As Exception
            MsgBox("Error al intentar cargar el combo: [ " & oComboBox.Name & " ] " & Chr(13) & Chr(13) & "Descripción: " & ex.Message, MsgBoxStyle.Critical)

        End Try
    End Function
    ''' <summary>
    '''     ''' Crea un objeto ComboBox para un DataGrid con los root causes
    ''' </summary>
    ''' <param name="ColumnTitle">Titulo de la columna</param>
    ''' <param name="ColumName">Nombre de la columna</param>
    ''' <param name="Sql">Instrucción SQL que se utilizará para llenar el combo</param>
    ''' <param name="ColID">Columna que se utilizará como ID</param>
    ''' <param name="ColDisplay">Columna que se utilizará como display</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AddComboRootCauses(ByVal ColumnTitle$, ByVal ColumName$, ByVal Sql$, ByVal ColID As String, ByVal ColDisplay As String) As Windows.Forms.DataGridViewComboBoxColumn
        Dim ComboColumn As New Windows.Forms.DataGridViewComboBoxColumn
        Dim Table As New DataSet
        Dim i%

        Table = Me.RunSentence(Sql)

        If Table.Tables(0).Rows.Count > 0 Then
            ComboColumn.DataSource = Table.Tables(0)
            ComboColumn.DisplayMember = Table.Tables(0).Columns(ColDisplay).Caption.ToString
            ComboColumn.ValueMember = Table.Tables(0).Columns(ColID).Caption.ToString
        End If

        'ComboColumn.Width = 150         
        ComboColumn.HeaderText = ColumnTitle
        ComboColumn.FlatStyle = Windows.Forms.FlatStyle.Popup
        ComboColumn.DisplayStyle = Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton
        ComboColumn.AutoComplete = True
        ComboColumn.Name = ColumName
        Return ComboColumn
    End Function
    Public Function Read_ZMR0_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        W = Split(S, vbTab)
        For Each S In W
            If S = "Line Item" Then
                S = S & I
                I += 1
            End If
            If S = "Type" Then
                S = S & I
            End If
            D.Columns.Add(New DataColumn(S, System.Type.GetType("System.String")))
        Next

        Do
            S = FileReader.ReadLine
            If S Is Nothing Or S = "" Then
                ExitLoop = True
            Else
                W = Split(S, vbTab)
                If W.Length <= D.Columns.Count - 1 Then
                    DR = D.NewRow
                    I = 1
                    DR("SAP") = SAP
                    For Each S In W
                        DR(I) = S
                        I += 1
                    Next
                    D.Rows.Add(DR)
                End If
            End If
        Loop Until ExitLoop

        If Not S Is Nothing Then
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            ExitLoop = False
            Do
                S = FileReader.ReadLine
                If S Is Nothing Or S = "" Then
                    ExitLoop = True
                Else
                    W = Split(S, vbTab)
                    DR = D.NewRow
                    DR("SAP") = SAP
                    DR("Company Code") = W(0)
                    DR("Invoice Number") = W(1)
                    DR("Type1") = W(9)
                    DR("Currency") = W(8)
                    DR("Document Date") = W(2)
                    DR("Posting Date") = W(3)
                    If IsNumeric(W(4)) Then
                        DR("Vendor") = CDbl(W(4)).ToString
                    Else
                        DR("Vendor") = W(4)
                    End If
                    DR("Vendor Name") = W(5)
                    DR("Days") = W(7)
                    DR("Purchase Doc") = W(10)
                    DR("Type2") = W(11)
                    DR("Purch Org") = W(12)
                    DR("Purch Group") = W(13)
                    DR("Baseline Date") = W(16)
                    DR("Manual") = "X"
                    DR("Line Item0") = "0"
                    DR("Line Item1") = "0"
                    D.Rows.Add(DR)
                End If
            Loop Until ExitLoop

        End If

        For Each DR In D.Rows
            If IsNumeric(DR("Material")) Then
                DR("Material") = CDbl(DR("Material")).ToString
            End If
            If IsNumeric(DR("Vendor")) Then
                DR("Vendor") = CDbl(DR("Vendor")).ToString
            End If
        Next
        D.AcceptChanges()

        FileReader.Close()
        Read_ZMR0_File = D

    End Function
    Public Function Read_SAP_PO_Without_Ref_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        S = FileReader.ReadLine
        'S = FileReader.ReadLine
        'S = FileReader.ReadLine
        'S = FileReader.ReadLine
        'S = FileReader.ReadLine


        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        W = Split(S, vbTab)
        For Each S In W
            If S = "Vendor #" Then
                S = "Vendor"
            End If
            If S = "PO #" Then
                S = "Purch Document"
            End If


            If S = "Short Desc." Then
                S = "Short Desc"
            End If
            If S = "Mat #" Then
                S = "Material"
            End If
            If S = "Item #" Then
                S = "Line Item"
            End If
            'If S = "Line Item" Then
            '    S = S & I
            '    I += 1
            'End If
            'If S = "Type" Then
            '    S = S & I
            'End If
            'If S = "Currency" Then
            '    S = S & I
            '    I += 1
            'End If
            If S = "P.Org" Then
                S = "Purch Org"
            End If

            D.Columns.Add(New DataColumn(S, System.Type.GetType("System.String")))
        Next

        Do
            S = FileReader.ReadLine
            If S Is Nothing Or S = "" Then
                ExitLoop = True
            Else
                W = Split(S, vbTab)
                If W.Length <= D.Columns.Count - 1 Then
                    DR = D.NewRow
                    I = 1
                    DR("SAP") = SAP
                    For Each S In W
                        DR(I) = S
                        I += 1
                    Next
                    D.Rows.Add(DR)
                End If
            End If
        Loop Until ExitLoop

        If Not S Is Nothing Then
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            ExitLoop = False
            Do
                S = FileReader.ReadLine
                If S Is Nothing Or S = "" Then
                    ExitLoop = True
                Else
                    W = Split(S, vbTab)
                    DR = D.NewRow
                    DR("SAP") = SAP
                    DR("Company Code") = W(0)
                    DR("Invoice Number") = W(1)
                    DR("Type1") = W(9)
                    DR("Currency") = W(8)
                    DR("Document Date") = W(2)
                    DR("Posting Date") = W(3)
                    If IsNumeric(W(4)) Then
                        DR("Vendor") = CDbl(W(4)).ToString
                    Else
                        DR("Vendor") = W(4)
                    End If
                    DR("Vendor Name") = W(5)
                    DR("Days") = W(7)
                    DR("Purchase Doc") = W(10)
                    DR("Type2") = W(11)
                    DR("Purch Org") = W(12)
                    DR("Purch Group") = W(13)
                    DR("Baseline Date") = W(16)
                    DR("Manual") = "X"
                    DR("Line Item0") = "0"
                    DR("Line Item1") = "0"
                    D.Rows.Add(DR)
                End If
            Loop Until ExitLoop

        End If

        For Each DR In D.Rows
            If IsNumeric(DR("Vendor")) Then
                DR("Vendor") = CDbl(DR("Vendor")).ToString
            End If
        Next
        D.AcceptChanges()

        FileReader.Close()
        Read_SAP_PO_Without_Ref_File = D

    End Function
    Public Function Read_SAP_Park_Credit_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Org", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Grp", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Credit Amount", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Currency", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Credit Doc", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Comp Code", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Posting Date", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Entered By", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Reference", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Doc Header Text", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Delivery Note", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Bill of Lading", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purchasing Doc", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Line Item", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Rsn Code", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Rsn Description", System.Type.GetType("System.String")))

        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine


        Do
            Try
                'S = FileReader.ReadLine
                Do
                    S = FileReader.ReadLine
                    If S = "" Then
                        Exit Do
                    End If

                    W = Split(S, vbTab)
                    DR = D.NewRow

                    DR("SAP") = SAP
                    DR("Plant") = W(1)
                    DR("Purch Org") = W(2)
                    DR("Purch Grp") = W(4)
                    DR("Vendor") = W(5)
                    DR("Description") = W(6)
                    DR("Credit Amount") = W(7)
                    DR("Currency") = W(9)
                    DR("Credit Doc") = W(10)
                    DR("Comp Code") = W(11)
                    DR("Posting Date") = W(12)
                    DR("Entered By") = W(13)
                    DR("Reference") = W(14)
                    DR("Doc Header Text") = W(15)
                    DR("Delivery Note") = W(16)
                    DR("Bill of Lading") = W(17)
                    DR("Purchasing Doc") = W(19)
                    DR("Line Item") = W(20)
                    DR("Rsn Code") = W(21)
                    DR("Rsn Description") = W(22)

                    D.Rows.Add(DR)

                Loop Until S Is Nothing Or S = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Loop Until S Is Nothing

        D.AcceptChanges()
        FileReader.Close()

        Read_SAP_Park_Credit_File = D
    End Function
    Public Function Read_SAP_PO_Change_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Change Type", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("User", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Change Date", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Comp Code", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Org", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Doc Number", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Item Number", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Doc Type", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Material", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Grp", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Grp Description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor Name", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Currency", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Order Price Unit", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Change Time", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Old Quantity", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Old Unit", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("New Quantity", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("New Unit", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Old Price", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("New Currency", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("% Quantity / Price Change", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Comment", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Short Text", System.Type.GetType("System.String")))


        Do
            Try
                S = FileReader.ReadLine
                Do
                    S = FileReader.ReadLine

                    If S = "" Then
                        Exit Do
                    End If
                    W = Split(S, vbTab)

                    If W.Length > 10 Then
                        DR = D.NewRow
                        DR("SAP") = SAP
                        DR("Change Type") = W(0)
                        DR("User") = W(1)
                        DR("Change Date") = W(2)
                        DR("Comp Code") = W(3)
                        DR("Purch Org") = W(4)
                        DR("Plant") = W(5)
                        DR("Doc Number") = W(6)
                        DR("Item Number") = W(7)
                        DR("Doc Type") = W(8)
                        DR("Material") = W(9)
                        DR("Purch Grp") = W(10)
                        DR("Purch Grp Description") = W(11)
                        DR("Vendor") = W(12)
                        DR("Vendor Name") = W(13)
                        DR("Currency") = W(14)
                        DR("Order Price Unit") = W(15)
                        DR("Change Time") = W(16)
                        DR("Old Quantity") = W(17)
                        DR("Old Unit") = W(18)
                        DR("New Quantity") = W(19)
                        DR("New Unit") = W(20)
                        DR("Old Price") = W(21)
                        DR("New Currency") = W(22)
                        DR("% Quantity / Price Change") = W(23)
                        DR("Comment") = W(24)
                        DR("Short Text") = W(25)
                        D.Rows.Add(DR)
                    End If


                Loop Until S Is Nothing Or S = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Loop Until S Is Nothing

        D.AcceptChanges()

        FileReader.Close()

        Read_SAP_PO_Change_File = D
    End Function
    Public Function Read_SAP_PO_AI_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Document", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Item", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Doc Date", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Invoice Num", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Invoice Date", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Days", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor Name", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("PO Created By", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("PO created name", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Req Created by", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Req Create Name", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("P Group", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Pgr description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("P Org", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Porg description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Line Item Amount", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Req Tracking", System.Type.GetType("System.String")))

        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine


        Do
            S = FileReader.ReadLine
        Loop Until (S = "")


        Do
            Try

                Dim Quit As Boolean = False
                Do
                    S = FileReader.ReadLine
                    If S <> "" Then
                        W = Split(S, vbTab)

                        If W.Length > 1 AndAlso (W(1).ToString.IndexOf("Purch.doc") <> -1) Then
                            Quit = True
                        End If

                        If W.Length = 1 AndAlso Left(W(0).ToString.ToUpper, 13) = "END OF REPORT" Then
                            Quit = True
                        End If
                    End If

                Loop Until Quit

                S = FileReader.ReadLine

                Do
                    S = FileReader.ReadLine
                    If S = "" Then
                        Exit Do
                    End If

                    W = Split(S, vbTab)

                    If W(0) = "ZMXXR046" Then
                        S = FileReader.ReadLine
                        W = Split(S, vbTab)

                        Quit = False

                        Do
                            If W.Length > 2 Then
                                If W(1).ToString.ToUpper = "PURCH.DOC" Then
                                    Quit = True
                                End If
                            Else
                                If Left(W(0).ToString.ToUpper, 13) = "END OF REPORT" Then
                                    Quit = True
                                End If
                            End If

                            S = FileReader.ReadLine
                            W = Split(S, vbTab)

                            If Quit Then
                                S = FileReader.ReadLine
                                W = Split(S, vbTab)
                            End If

                        Loop Until Quit = True

                    End If

                    DR = D.NewRow

                    'If W(1) = "4500857373" Then
                    '    MsgBox("")
                    'End If



                    DR("SAP") = SAP
                    DR("Purch Document") = W(1)
                    DR("Item") = W(4)
                    DR("Doc Date") = W(5)
                    DR("Invoice Num") = W(7)
                    DR("Invoice Date") = W(9)
                    DR("Days") = W(11)
                    DR("Vendor") = W(13)
                    DR("Vendor Name") = W(16)
                    DR("PO Created By") = W(17)
                    DR("PO created name") = W(18)
                    DR("Req Created by") = W(19)
                    DR("Req Create Name") = W(20)
                    DR("P Group") = W(21)
                    DR("Pgr description") = W(22)
                    DR("Plant") = W(23)
                    DR("Plant description") = W(24)
                    DR("P Org") = W(25)
                    DR("Porg description") = W(26)
                    DR("Line Item Amount") = W(27)

                    If W.Length = 29 Then
                        DR("Req Tracking") = W(28)
                    End If

                    D.Rows.Add(DR)

                Loop Until S Is Nothing Or S = ""
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Loop Until S Is Nothing

        D.AcceptChanges()

        FileReader.Close()

        Read_SAP_PO_AI_File = D
    End Function
    Public Function Read_SAP_IROnly_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Document", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Item", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Date", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Total PO Value", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("P Grp", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("PG Description", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Vendor Name", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Requisition", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Purch Requisition Item", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Originator", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Release Required", System.Type.GetType("System.String")))
        D.Columns.Add(New DataColumn("Plant", System.Type.GetType("System.String")))

        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine

        Do
            S = FileReader.ReadLine
        Loop Until (S = "")


        Do
            Try
                Dim PlantCode As String = ""

                Do
                    S = FileReader.ReadLine
                    W = Split(S, vbTab)

                Loop Until (W(0).ToString.IndexOf("Plant :") <> -1)
                PlantCode = W(2)
                Dim Quit As Boolean = False
                Do
                    S = FileReader.ReadLine
                    If S <> "" Then
                        W = Split(S, vbTab)
                        If (W(1).ToString.IndexOf("Order #") <> -1) Then
                            Quit = True
                        End If
                    End If

                Loop Until Quit

                S = FileReader.ReadLine

                Do
                    S = FileReader.ReadLine
                    If S = "" Then
                        Exit Do
                    End If

                    W = Split(S, vbTab)

                    If W(0) = "ZMXXR091" Then
                        S = FileReader.ReadLine
                        S = FileReader.ReadLine
                        W = Split(S, vbTab)
                    End If

                    DR = D.NewRow

                    DR("SAP") = SAP

                    If W(1) = "" Then
                        Exit Do
                    End If

                    DR("Purch Document") = W(1)
                    DR("Item") = W(4)
                    DR("Description") = W(5)
                    DR("Date") = W(8)
                    DR("Total PO Value") = W(11)
                    DR("P Grp") = W(13)
                    DR("PG Description") = W(15)
                    DR("Vendor") = W(16)
                    DR("Vendor Name") = W(17)
                    DR("Purch Requisition") = W(18)
                    DR("Purch Requisition Item") = W(19)
                    DR("Originator") = W(20)
                    DR("Release Required") = W(22)
                    DR("Plant") = PlantCode

                    D.Rows.Add(DR)

                Loop Until S Is Nothing Or S = ""
            Catch ex As Exception
                ' MsgBox(ex.Message)
            End Try
        Loop Until S Is Nothing

        D.AcceptChanges()

        FileReader.Close()

        Read_SAP_IROnly_File = D
    End Function
    Public Function Read_SAP_Unblocked_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine
        S = FileReader.ReadLine

        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        W = Split(S, vbTab)
        For Each S In W
            If S = "Line Item" Then
                S = S & I
                I += 1
            End If
            If S = "Type" Then
                S = S & I
            End If
            If S = "Currency" Then
                S = S & I
                I += 1
            End If
            D.Columns.Add(New DataColumn(S, System.Type.GetType("System.String")))
        Next
        Do While Not S Is Nothing
            Do
                S = FileReader.ReadLine
                If S Is Nothing Or S = "" Then
                    ExitLoop = True
                Else
                    W = Split(S, vbTab)
                    If (W.Length > 10) AndAlso (W.Length <= D.Columns.Count - 1) Then
                        DR = D.NewRow
                        I = 1
                        DR("SAP") = SAP
                        For Each S In W
                            DR(I) = S
                            I += 1
                        Next
                        D.Rows.Add(DR)
                    End If
                End If
            Loop Until ExitLoop
        Loop


        For Each DR In D.Rows
            If IsNumeric(DR("Vendor")) Then
                DR("Vendor") = CDbl(DR("Vendor")).ToString
            End If
        Next
        D.AcceptChanges()

        FileReader.Close()
        Read_SAP_Unblocked_File = D

    End Function
    Public Function UploadZMR0(ByVal Table As DataTable) As Boolean

        Dim cn As New OAConnection.Connection
        Try
            Return cn.AppendTableToSqlServer("BI_TMP_ZMR0_Report", Table)
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function Encrypt(ByVal Txt As String) As String
        Dim I As Integer
        Dim L As Integer
        Dim ES As String
        Dim Msk As String

        Msk = ")@*$&^!\[]{/:';<~`+=zo2916qw-"
        L = Len(Txt)
        If L > 30 Then L = 30
        ES = ""
        For I = 1 To L
            ES = ES + Chr(Asc(Mid(Msk, I, 1)) Xor Asc(Mid(Txt, I, 1)))
        Next

        Encrypt = ES
    End Function
    Public Sub New()

    End Sub


    Public Function GetBRTable(ByVal Vendor As String, ByVal Description As String) As System.Data.DataTable
        Dim DataTableTmp As New DataTable
        Dim _Adapter As New SqlClient.SqlDataAdapter
        Dim Pr As New SqlClient.SqlParameter
        Dim _ConnectionDB As New SqlClient.SqlConnection

        Try
            DataTableTmp = New DataTable()

            _ConnectionDB = New SqlClient.SqlConnection(GetConnectionString)
            _ConnectionDB.Open()
            _Adapter = New SqlClient.SqlDataAdapter("Select Top 1 * From [Catalog BRF Rules] Where Vendor = @Vendor And RTrim(LTrim([Material Description])) = @Description", _ConnectionDB)

            Pr = New SqlClient.SqlParameter
            Pr.ParameterName = "Vendor"
            Pr.Value = Vendor
            _Adapter.SelectCommand.Parameters.Add(Pr)

            Pr = New SqlClient.SqlParameter
            Pr.ParameterName = "Description"
            Pr.Value = Description.Trim
            _Adapter.SelectCommand.Parameters.Add(Pr)

            _Adapter.Fill(DataTableTmp)

            If DataTableTmp.Rows.Count > 0 Then
                Return DataTableTmp
            Else
                Return Nothing
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            _ConnectionDB.Close()
            DataTableTmp.Dispose()
            _Adapter.Dispose()
            _Adapter = Nothing
            _ConnectionDB = Nothing
        End Try

    End Function
    Public Function getExceptions(ByVal pDistribution As Integer, Optional ByVal opTable As String = "") As String
        Dim lsFilter As String = ""

        If pDistribution <> 0 Then
            'Dim dtException As DataTable
            'dtException = RunSentence("Select VendorId From VendorException Where ExceptionFor = " & pDistribution).Tables(0)

            'If dtException.Rows.Count > 0 Then
            '    For Each r As DataRow In dtException.Rows
            '        If lsFilter.Length > 0 Then
            '            lsFilter = lsFilter & " Or "
            '        End If

            '        lsFilter = lsFilter & "(" & opTable & " [Vendor] = " & r("VendorId") & ")"
            '    Next

            '    lsFilter = " Or (" & lsFilter & ")"
            'End If

            lsFilter = " Or ([Vendor] In (Select VendorId From VendorException Where ExceptionFor = " & pDistribution & "))"
        End If

        Return lsFilter
    End Function

    Public Function Read_Trigger_File(ByVal Path As String, ByVal SAP As String) As DataTable
        Dim FileReader As New System.IO.StreamReader(Path)
        Dim S As String
        Dim W As Array
        Dim D As New DataTable
        Dim ExitLoop As Boolean = False
        Dim DR As DataRow
        Dim I As Integer = 0

        S = FileReader.ReadLine
        D.Columns.Add(New DataColumn("SAP", System.Type.GetType("System.String")))
        W = Split(S, vbTab)
        For Each S In W
            If S = "Line Item" Then
                S = S & I
                I += 1
            End If
            If S = "Type" Then
                S = S & I
            End If
            If S = "Currency" Then
                S = S & I
                I += 1
            End If

            If S = "CONTACT T#" Then
                S = S & I
                I += 1
            End If
            If S = "BASEUOM" Then
                S = S & I
                I += 1
            End If
            If S = "ALTUOM" Then
                S = S & I
                I += 1
            End If
            If S = "COMMENTS" Then
                S = S & I
                I += 1
            End If

            D.Columns.Add(New DataColumn(S, System.Type.GetType("System.String")))
        Next

        Do While Not S Is Nothing
            Do
                S = FileReader.ReadLine
                If S Is Nothing Or S = "" Then
                    ExitLoop = True
                Else
                    W = Split(S, vbTab)
                    'If W.Length <= D.Columns.Count - 1 Then
                    If IsNumeric(W(0)) Then

                        DR = D.NewRow
                        I = 1
                        DR("SAP") = SAP
                        For Each S In W
                            DR(I) = S
                            I += 1
                        Next

                        If Not DBNull.Value.Equals(DR("PLNT")) Then
                            D.Rows.Add(DR)
                        End If


                    End If



                End If
            Loop Until ExitLoop
        Loop


        For Each DR In D.Rows
            If IsNumeric(DR("Mat#")) Then
                DR("Mat#") = CDbl(DR("Mat#")).ToString
            End If

            If IsNumeric(DR("Vendor")) Then
                DR("Vendor") = CDbl(DR("Vendor")).ToString
            Else
                DR("Vendor") = 0
            End If
        Next


        If D.Columns.IndexOf("STEP1") <> -1 Then
            D.Columns.Remove("STEP1")
        End If

        If D.Columns.IndexOf("STEP2") <> -1 Then
            D.Columns.Remove("STEP2")
        End If

        If D.Columns.IndexOf("STEP3") <> -1 Then
            D.Columns.Remove("STEP3")
        End If
        If D.Columns.IndexOf("NOT IN TRGDB") <> -1 Then
            D.Columns.Remove("NOT IN TRGDB")
        End If
        If D.Columns.IndexOf("TYPE") <> -1 Then
            D.Columns.Remove("TYPE")
        End If
        If D.Columns.IndexOf("ON BOM") <> -1 Then
            D.Columns.Remove("ON BOM")
        End If
        If D.Columns.IndexOf("SERIAL #") <> -1 Then
            D.Columns.Remove("SERIAL #")
        End If
        If D.Columns.IndexOf("SLOC") <> -1 Then
            D.Columns.Remove("SLOC")
        End If
        If D.Columns.IndexOf("2ND CONTACT T#") <> -1 Then
            D.Columns.Remove("2ND CONTACT T#")
        End If
        If D.Columns.IndexOf("EST.A.CONS") <> -1 Then
            D.Columns.Remove("EST.A.CONS")
        End If
        If D.Columns.IndexOf("COMM/FAB") <> -1 Then
            D.Columns.Remove("COMM/FAB")
        End If
        If D.Columns.IndexOf("PRIORITY") <> -1 Then
            D.Columns.Remove("PRIORITY")
        End If
        If D.Columns.IndexOf("NOACTION") <> -1 Then
            D.Columns.Remove("NOACTION")
        End If
        If D.Columns.IndexOf("NOCONTR") <> -1 Then
            D.Columns.Remove("NOCONTR")
        End If
        If D.Columns.IndexOf("PROJREL") <> -1 Then
            D.Columns.Remove("PROJREL")
        End If
        If D.Columns.IndexOf("RESPCODE") <> -1 Then
            D.Columns.Remove("RESPCODE")
        End If
        If D.Columns.IndexOf("LANG") <> -1 Then
            D.Columns.Remove("LANG")
        End If
        If D.Columns.IndexOf("LOC.LANG.DESC") <> -1 Then
            D.Columns.Remove("LOC.LANG.DESC")
        End If
        If D.Columns.IndexOf("DAYSINSTEP1") <> -1 Then
            D.Columns.Remove("DAYSINSTEP1")
        End If
        If D.Columns.IndexOf("SENDTOSTEP2") <> -1 Then
            D.Columns.Remove("SENDTOSTEP2")
        End If
        If D.Columns.IndexOf("CONTACT T#2") <> -1 Then
            D.Columns.Remove("CONTACT T#2")
        End If
        If D.Columns.IndexOf("TAXIN") <> -1 Then
            D.Columns.Remove("TAXIN")
        End If
        If D.Columns.IndexOf("ACKNOWL. REQ") <> -1 Then
            D.Columns.Remove("ACKNOWL. REQ")
        End If
        If D.Columns.IndexOf("NEWCONTR") <> -1 Then
            D.Columns.Remove("NEWCONTR")
        End If
        If D.Columns.IndexOf("INFO UPDATE") <> -1 Then
            D.Columns.Remove("INFO UPDATE")
        End If
        If D.Columns.IndexOf("BASEUOM") <> -1 Then
            D.Columns.Remove("BASEUOM")
        End If
        If D.Columns.IndexOf("ALTUOM") <> -1 Then
            D.Columns.Remove("ALTUOM")
        End If
        If D.Columns.IndexOf("BASEUOM3") <> -1 Then
            D.Columns.Remove("BASEUOM3")
        End If
        If D.Columns.IndexOf("ALTUOM4") <> -1 Then
            D.Columns.Remove("ALTUOM4")
        End If
        If D.Columns.IndexOf("MAT. ORIGIN") <> -1 Then
            D.Columns.Remove("MAT. ORIGIN")
        End If
        If D.Columns.IndexOf("PROD. IN-HOUSE") <> -1 Then
            D.Columns.Remove("PROD. IN-HOUSE")
        End If
        If D.Columns.IndexOf("CONTROL CODE") <> -1 Then
            D.Columns.Remove("CONTROL CODE")
        End If
        If D.Columns.IndexOf("COMM/ IMPORT CODE") <> -1 Then
            D.Columns.Remove("COMM/ IMPORT CODE")
        End If

        If D.Columns.IndexOf("RETRNDTOSTEP1") <> -1 Then
            D.Columns.Remove("RETRNDTOSTEP1")
        End If

        If D.Columns.IndexOf("DAYSINSOURCNG") <> -1 Then
            D.Columns.Remove("DAYSINSOURCNG")
        End If

        If D.Columns.IndexOf("MRPGRP") <> -1 Then
            D.Columns.Remove("MRPGRP")
        End If

        If D.Columns.IndexOf("ABC") <> -1 Then
            D.Columns.Remove("ABC")
        End If

        If D.Columns.IndexOf("MRPCTL") <> -1 Then
            D.Columns.Remove("MRPCTL")
        End If

        If D.Columns.IndexOf("MAT. USAGE") <> -1 Then
            D.Columns.Remove("MAT. USAGE")
        End If

        If D.Columns.IndexOf("CFOP CAT") <> -1 Then
            D.Columns.Remove("CFOP CAT")
        End If

        If D.Columns.IndexOf("CALC SSTK") <> -1 Then
            D.Columns.Remove("CALC SSTK")
        End If

        If D.Columns.IndexOf("SAFETY STK") <> -1 Then
            D.Columns.Remove("SAFETY STK")
        End If

        If D.Columns.IndexOf("PROCTYPE") <> -1 Then
            D.Columns.Remove("PROCTYPE")
        End If

        If D.Columns.IndexOf("IN HOUSE PROD") <> -1 Then
            D.Columns.Remove("IN HOUSE PROD")
        End If

        If D.Columns.IndexOf("GRT") <> -1 Then
            D.Columns.Remove("GRT")
        End If

        If D.Columns.IndexOf("ISSUE UOM") <> -1 Then
            D.Columns.Remove("ISSUE UOM")
        End If

        If D.Columns.IndexOf("BASEUOM5") <> -1 Then
            D.Columns.Remove("BASEUOM5")
        End If

        If D.Columns.IndexOf("ALTUOM6") <> -1 Then
            D.Columns.Remove("ALTUOM6")
        End If

        If D.Columns.IndexOf("BIN") <> -1 Then
            D.Columns.Remove("BIN")
        End If

        If D.Columns.IndexOf("RNDING.VAL") <> -1 Then
            D.Columns.Remove("RNDING.VAL")
        End If

        If D.Columns.IndexOf("CALC EOQ") <> -1 Then
            D.Columns.Remove("CALC EOQ")
        End If
        If D.Columns.IndexOf("MIN.LOT SIZE") <> -1 Then
            D.Columns.Remove("MIN.LOT SIZE")
        End If
        If D.Columns.IndexOf("PROFIT CTR") <> -1 Then
            D.Columns.Remove("PROFIT CTR")
        End If
        If D.Columns.IndexOf("VAL CLASS") <> -1 Then
            D.Columns.Remove("VAL CLASS")
        End If
        If D.Columns.IndexOf("REP") <> -1 Then
            D.Columns.Remove("REP")
        End If
        If D.Columns.IndexOf("CAP") <> -1 Then
            D.Columns.Remove("CAP")
        End If
        If D.Columns.IndexOf("ERROR") <> -1 Then
            D.Columns.Remove("ERROR")
        End If
        If D.Columns.IndexOf("DAYSINSTEP3") <> -1 Then
            D.Columns.Remove("DAYSINSTEP3")
        End If
        If D.Columns.IndexOf("HISTORY") <> -1 Then
            D.Columns.Remove("HISTORY")
        End If


        If D.Columns.IndexOf("BASEUOM8") <> -1 Then
            D.Columns.Remove("BASEUOM8")
        End If

        If D.Columns.IndexOf("ALTUOM9") <> -1 Then
            D.Columns.Remove("ALTUOM9")
        End If


        D.AcceptChanges()

        FileReader.Close()
        Read_Trigger_File = D

    End Function

    Public Function LinQToDataTable(Of T)(ByVal source As IEnumerable(Of T)) As DataTable
        Return New ObjectShredder(Of T)().Shred(source, Nothing, Nothing)
    End Function

    Public Function LinQToDataTable(Of T)(ByVal source As IEnumerable(Of T), ByVal table As DataTable, ByVal options As LoadOption?) As DataTable
        Return New ObjectShredder(Of T)().Shred(source, table, options)
    End Function
End Class




Friend Class ObjectShredder(Of T)
    ' Fields
    Private _fi As FieldInfo()
    Private _ordinalMap As Dictionary(Of String, Integer)
    Private _pi As PropertyInfo()
    Private _type As Type

    ' Constructor 
    Public Sub New()
        Me._type = GetType(T)
        Me._fi = Me._type.GetFields
        Me._pi = Me._type.GetProperties
        Me._ordinalMap = New Dictionary(Of String, Integer)
    End Sub

    Public Function ShredObject(ByVal table As DataTable, ByVal instance As T) As Object()
        Dim fi As FieldInfo() = Me._fi
        Dim pi As PropertyInfo() = Me._pi
        If (Not instance.GetType Is GetType(T)) Then
            ' If the instance is derived from T, extend the table schema
            ' and get the properties and fields.
            Me.ExtendTable(table, instance.GetType)
            fi = instance.GetType.GetFields
            pi = instance.GetType.GetProperties
        End If

        ' Add the property and field values of the instance to an array.
        Dim values As Object() = New Object(table.Columns.Count - 1) {}
        Dim f As FieldInfo
        For Each f In fi
            values(Me._ordinalMap.Item(f.Name)) = f.GetValue(instance)
        Next
        Dim p As PropertyInfo
        For Each p In pi
            values(Me._ordinalMap.Item(p.Name)) = p.GetValue(instance, Nothing)
        Next

        ' Return the property and field values of the instance.
        Return values
    End Function


    ' Summary:           Loads a DataTable from a sequence of objects.
    ' source parameter:  The sequence of objects to load into the DataTable.</param>
    ' table parameter:   The input table. The schema of the table must match that 
    '                    the type T.  If the table is null, a new table is created  
    '                    with a schema created from the public properties and fields 
    '                    of the type T.
    ' options parameter: Specifies how values from the source sequence will be applied to 
    '                    existing rows in the table.
    ' Returns:           A DataTable created from the source sequence.

    Public Function Shred(ByVal source As IEnumerable(Of T), ByVal table As DataTable, ByVal options As LoadOption?) As DataTable

        ' Load the table from the scalar sequence if T is a primitive type.
        If GetType(T).IsPrimitive Then
            Return Me.ShredPrimitive(source, table, options)
        End If

        ' Create a new table if the input table is null.
        If (table Is Nothing) Then
            table = New DataTable(GetType(T).Name)
        End If

        ' Initialize the ordinal map and extend the table schema based on type T.
        table = Me.ExtendTable(table, GetType(T))

        ' Enumerate the source sequence and load the object values into rows.
        table.BeginLoadData()
        Using e As IEnumerator(Of T) = source.GetEnumerator
            Do While e.MoveNext
                If options.HasValue Then
                    table.LoadDataRow(Me.ShredObject(table, e.Current), options.Value)
                Else
                    table.LoadDataRow(Me.ShredObject(table, e.Current), True)
                End If
            Loop
        End Using
        table.EndLoadData()

        ' Return the table.
        Return table
    End Function


    Public Function ShredPrimitive(ByVal source As IEnumerable(Of T), ByVal table As DataTable, ByVal options As LoadOption?) As DataTable
        ' Create a new table if the input table is null.
        If (table Is Nothing) Then
            table = New DataTable(GetType(T).Name)
        End If
        If Not table.Columns.Contains("Value") Then
            table.Columns.Add("Value", GetType(T))
        End If

        ' Enumerate the source sequence and load the scalar values into rows.
        table.BeginLoadData()
        Using e As IEnumerator(Of T) = source.GetEnumerator
            Dim values As Object() = New Object(table.Columns.Count - 1) {}
            Do While e.MoveNext
                values(table.Columns.Item("Value").Ordinal) = e.Current
                If options.HasValue Then
                    table.LoadDataRow(values, options.Value)
                Else
                    table.LoadDataRow(values, True)
                End If
            Loop
        End Using
        table.EndLoadData()

        ' Return the table.
        Return table
    End Function

    Public Function ExtendTable(ByVal table As DataTable, ByVal type As Type) As DataTable
        ' Extend the table schema if the input table was null or if the value 
        ' in the sequence is derived from type T.
        Dim f As FieldInfo
        Dim p As PropertyInfo

        For Each f In type.GetFields
            If Not Me._ordinalMap.ContainsKey(f.Name) Then
                Dim dc As DataColumn

                ' Add the field as a column in the table if it doesn't exist
                ' already.
                dc = IIf(table.Columns.Contains(f.Name), table.Columns.Item(f.Name), table.Columns.Add(f.Name, f.FieldType))

                ' Add the field to the ordinal map.
                Me._ordinalMap.Add(f.Name, dc.Ordinal)
            End If

        Next

        For Each p In type.GetProperties
            If Not Me._ordinalMap.ContainsKey(p.Name) Then
                ' Add the property as a column in the table if it doesn't exist
                ' already.
                Dim dc As DataColumn
                If table.Columns.Contains(p.Name) Then
                    dc = table.Columns.Item(p.Name)
                Else
                    dc = table.Columns.Add(p.Name, p.PropertyType)
                End If
                ' Add the property to the ordinal map.
                Me._ordinalMap.Add(p.Name, dc.Ordinal)
            End If
        Next

        ' Return the table.
        Return table
    End Function

End Class

Namespace BaseDatos
    Public Class Tansacciones

    End Class
End Namespace






