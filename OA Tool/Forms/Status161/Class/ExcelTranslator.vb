Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.OleDb

'References
'http://support.microsoft.com/kb/316934/es
'http://www.codeproject.com/KB/vb/Senthil_S__Software_Eng_.aspx

Public Class ExcelTranslator

    'La tabla debe empezar en A1
    Public Function ExcelToDataTable(ByVal ExcelFilePath As String) As DataTable
        Dim dt As New DataTable
        Dim dr As DataRow

        Dim Exc As Excel.Application = Nothing
        Dim WB As Excel.Workbook
        Dim WS As Excel.Worksheet = Nothing

        Dim RowIndex As Integer = 2
        Dim ColumnIndex As Integer = 1
        Dim ColumnNumber As Integer = 0
        Dim ColumnName As String = "Bullio"

        Try
            Exc = New Excel.ApplicationClass()
            WB = Exc.Workbooks.Open(ExcelFilePath)
            WS = WB.ActiveSheet

            'Agramos las Columnas al DataTable
            While ColumnName <> ""
                Try
                    ColumnName = (WS.Cells(1, ColumnIndex).Value2).ToString
                Catch ex As Exception
                    ColumnName = ""
                End Try

                If ColumnName <> "" Then
                    dt.Columns.Add(ColumnName)
                End If
                ColumnIndex = ColumnIndex + 1
            End While

            ColumnNumber = dt.Columns.Count
            ColumnIndex = 1
            RowIndex = 2

            'LLenamos las filas del DataTable
            While WS.Cells(RowIndex, ColumnIndex).Value2 <> Nothing
                dr = dt.NewRow()
                For c = 0 To ColumnNumber - 1
                    Try
                        dr(c) = (WS.Cells(RowIndex, c + 1).Value2).ToString
                    Catch
                        dr(c) = Nothing
                    End Try
                Next
                dt.Rows.Add(dr)
                RowIndex = RowIndex + 1
            End While

            WB.Close()
            Exc.Quit()
            releaseObject(Exc)
            releaseObject(WB)
            releaseObject(WS)
            ExcelToDataTable = dt
        Catch
            ExcelToDataTable = Nothing
        End Try

    End Function

    Public Function DataTableToExcel(ByVal dt As DataTable, ByVal ExcelFilePath As String) As Boolean

        Dim Exc As Excel.Application = Nothing
        Dim WB As Excel.Workbook
        Dim WS As Excel.Worksheet = Nothing
        Dim misValue As Object = System.Reflection.Missing.Value

        Try
            Exc = New Excel.ApplicationClass()
            WB = Exc.Workbooks.Add(misValue)
            WS = WB.Sheets("sheet1")
            WS.Name = "Result"

            Dim ColumnIndex As Integer = 1
            Dim Rowindex As Integer = 2
            Dim ColumnNumber As Integer = dt.Columns.Count

            'Escribimos las columnas
            For Each dc As DataColumn In dt.Columns
                Exc.Cells(1, ColumnIndex) = dc.ToString
                ColumnIndex = ColumnIndex + 1
            Next

            'Escribimos las Filas
            For Each dr As DataRow In dt.Rows
                For c = 1 To ColumnNumber
                    Try
                        Exc.Cells(Rowindex, c) = dr(c - 1).ToString
                    Catch
                        Exc.Cells(Rowindex, c) = Nothing
                    End Try
                Next
                Rowindex = Rowindex + 1
            Next

            'Guardamos el Archivo de Excel
            'WB.SaveAs(ExcelFilePath)
            Try
                'http://msdn.microsoft.com/en-us/library/microsoft.office.interop.excel.xlfileformat%28office.11%29.aspx
                WB.SaveAs(ExcelFilePath, 56)
            Catch ex As Exception
                WB.SaveAs(ExcelFilePath)
            End Try

            WB.Close()
            Exc.Quit()
            releaseObject(Exc)
            releaseObject(WB)
            releaseObject(WS)
            DataTableToExcel = True
        Catch
            DataTableToExcel = False
        End Try
    End Function

    Public Function DataTableToExcel(ByVal dt As DataTable, ByVal ExcelFilePath As String, ByVal SheetName As String) As Boolean

        Dim Exc As Excel.Application = Nothing
        Dim WB As Excel.Workbook
        Dim WS As Excel.Worksheet = Nothing
        Dim ExcConstans As Excel.Constants
        Dim misValue As Object = System.Reflection.Missing.Value
        Dim Dir As Excel.XlDirection

        Try
            Exc = New Excel.ApplicationClass()
            WB = Exc.Workbooks.Add(misValue)
            WS = WB.Sheets("sheet1")
            WS.Name = SheetName

            Dim ColumnIndex As Integer = 1
            Dim Rowindex As Integer = 2
            Dim ColumnNumber As Integer = dt.Columns.Count

            'Escribimos las columnas
            For Each dc As DataColumn In dt.Columns
                Exc.Cells(1, ColumnIndex) = dc.ToString

                'Cambiamos el Color de los encabezados
                Try
                    Exc.Cells(1, ColumnIndex).Select()
                    With Exc.Selection.Interior
                        .Pattern = ExcConstans.xlSolid
                        .PatternColorIndex = ExcConstans.xlAutomatic
                        '.ThemeColor = xlThemeColorDark1
                        .TintAndShade = -0.349986266670736
                        .PatternTintAndShade = 0
                    End With
                Catch
                End Try

                'Acomodamos el tamano de las Columnas a sus valores
                Try
                    Exc.Cells(1, ColumnIndex).EntireColumn.AutoFit()
                    ColumnIndex = ColumnIndex + 1
                Catch
                End Try
            Next

            'Escribimos las Filas
            For Each dr As DataRow In dt.Rows
                For c = 1 To ColumnNumber
                    Try
                        Exc.Cells(Rowindex, c) = dr(c - 1).ToString
                    Catch
                        Exc.Cells(Rowindex, c) = Nothing
                    End Try
                Next
                Rowindex = Rowindex + 1
            Next

            Exc.Columns("A:BF").Select()
            Exc.Columns("A:BF").EntireColumn.AutoFit()
            Exc.ActiveWindow.ScrollColumn = 1

            Exc.Rows("1:1").Select()
            Exc.Range(Exc.Selection, Exc.Selection.End(Dir.xlDown)).Select()
            Exc.Selection.RowHeight = 15

            'Guardamos el Archivo de Excel
            'WB.SaveAs(ExcelFilePath)
            Try
                'http://msdn.microsoft.com/en-us/library/microsoft.office.interop.excel.xlfileformat%28office.11%29.aspx
                WB.SaveAs(ExcelFilePath, 56)
            Catch ex As Exception
                WB.SaveAs(ExcelFilePath)
            End Try

            WB.Close()
            Exc.Quit()
            releaseObject(Exc)
            releaseObject(WB)
            releaseObject(WS)
            DataTableToExcel = True
        Catch
            DataTableToExcel = False
        End Try
    End Function

    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Function ExcelToDataTableOLEDB(ByVal ExcelFilePath As String, ByVal SheetName As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim dr As DataRow
            Dim ECS As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & ExcelFilePath & ";" & "Extended Properties=""Excel 8.0;HDR=YES"""

            Dim conn1 As New System.Data.OleDb.OleDbConnection(ECS)
            conn1.Open()
            Dim cmd1 As New System.Data.OleDb.OleDbCommand("Select * From [" & SheetName & "$]", conn1)
            Dim rdr As OleDbDataReader = cmd1.ExecuteReader

            'Agregamos las columnas al DataTable
            For i = 0 To rdr.FieldCount - 1
                dt.Columns.Add(rdr.GetName(i))
            Next

            'Agregamos las filas al DataTable
            Do While rdr.Read()
                dr = dt.NewRow()
                For c = 0 To rdr.FieldCount - 1
                    Try
                        dr(c) = rdr(c).ToString
                    Catch
                        dr(c) = Nothing
                    End Try
                Next
                dt.Rows.Add(dr)
            Loop
            rdr.Close()
            conn1.Close()
            ExcelToDataTableOLEDB = dt
        Catch ex As Exception
            ExcelToDataTableOLEDB = Nothing
        End Try
    End Function

    Public Function ExcelToDataTableOLEDB(ByVal ExcelFilePath As String, ByVal SheetName As String, ByVal ColumnIndexs() As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim dr As DataRow
            Dim ECS As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=" & ExcelFilePath & ";" & "Extended Properties=""Excel 8.0;HDR=YES"""

            Dim conn1 As New System.Data.OleDb.OleDbConnection(ECS)
            conn1.Open()
            Dim cmd1 As New System.Data.OleDb.OleDbCommand("Select * From [" & SheetName & "$]", conn1)
            Dim rdr As OleDbDataReader = cmd1.ExecuteReader

            'Agregamos la columna al DataTable
            For x = 0 To ColumnIndexs.Length - 1
                dt.Columns.Add(rdr.GetName(ColumnIndexs(x)))
            Next

            'Agregamos las filas al DataTable
            Do While rdr.Read()
                dr = dt.NewRow()
                'For c = 0 To rdr.FieldCount - 1
                For x = 0 To ColumnIndexs.Length - 1
                    Try
                        dr(x) = rdr(ColumnIndexs(x)).ToString
                    Catch
                        dr(x) = Nothing
                    End Try
                Next
                dt.Rows.Add(dr)
            Loop
            rdr.Close()
            conn1.Close()
            ExcelToDataTableOLEDB = dt
            Dim RowCount As Double = dt.Rows.Count
        Catch ex As Exception
            ExcelToDataTableOLEDB = Nothing
        End Try
    End Function

End Class




