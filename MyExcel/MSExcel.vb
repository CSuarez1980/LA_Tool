Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Data
Imports System.Text
Imports iXL = Microsoft.Office.Interop.Excel


Public Class MSExcel

    Public Function ExportToExcel(ByVal pTable As System.Data.DataTable) As Boolean
        Dim xlApp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim oQueryTable As Excel.QueryTable
        Dim rs As ADODB.Recordset

        Try
            xlApp = CreateObject("Excel.Application")

            xlApp.UserControl = True
            xlBook = xlApp.Workbooks.Add
            xlSheet = xlBook.Worksheets(1)

            rs = ConvertToRecordset(pTable)

            oQueryTable = xlSheet.QueryTables.Add(rs, xlSheet.Cells(1, 1))
            oQueryTable.Refresh()

            xlApp.Visible = True
            rs = Nothing

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ExportToExcel(ByVal pTable As System.Data.DataTable, ByVal pFilePath As String) As Boolean
        Dim xlApp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim oQueryTable As Excel.QueryTable
        Dim rs As ADODB.Recordset


        Try
            If Len(Dir(pFilePath)) > 0 Then
                Kill(pFilePath)
            End If

            xlApp = CreateObject("Excel.Application")

            xlApp.UserControl = True
            xlBook = xlApp.Workbooks.Add
            xlSheet = xlBook.Worksheets(1)

            rs = ConvertToRecordset(pTable)

            oQueryTable = xlSheet.QueryTables.Add(rs, xlSheet.Cells(1, 1))
            oQueryTable.Refresh()

            xlApp.ActiveWorkbook.SaveAs(pFilePath)

            xlApp.ActiveWorkbook.Close()
            xlApp.Quit()
            
            Return True
        Catch ex As Exception
            Return False
        Finally
            rs = Nothing
        End Try
    End Function

    Public Shared Sub DataTableToRange(ByVal anchorCell As Excel.Range, _
    ByVal tableToCopy As System.Data.DataTable, _
    Optional ByVal tableHeader As String = "")

        If tableHeader <> "" Then
            Try
                anchorCell.Value = tableHeader
                anchorCell = anchorCell.Offset(1, 0)
            Catch ex As Exception
            End Try
        End If

        Dim tableHeaderOffset As Integer = 0

        For Each loopHeaders As DataColumn In tableToCopy.Columns
            Try
                anchorCell.Offset(0, tableHeaderOffset).Value = loopHeaders.ColumnName
            Catch ex As Exception
            End Try

            tableHeaderOffset += 1

        Next

        anchorCell.Offset(1, 0).CopyFromRecordset(ConvertToRecordset(tableToCopy))

    End Sub

    Public Shared Function ConvertToRecordset(ByVal inTable As System.Data.DataTable) As ADODB.Recordset
        Dim result As ADODB.Recordset = New ADODB.Recordset()
        Dim Value As String = ""

        result.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        Dim resultFields As ADODB.Fields = result.Fields
        Dim inColumns As System.Data.DataColumnCollection = inTable.Columns
        Try

            For Each inColumn As DataColumn In inColumns
                resultFields.Append(inColumn.ColumnName, _
                    TranslateType(inColumn.DataType), _
                    inColumn.MaxLength, _
                    ADODB.FieldAttributeEnum.adFldIsNullable, _
                    Nothing)
            Next

            result.Open(System.Reflection.Missing.Value _
                    , System.Reflection.Missing.Value _
                    , ADODB.CursorTypeEnum.adOpenStatic _
                    , ADODB.LockTypeEnum.adLockOptimistic)

            For Each dr As DataRow In inTable.Rows
                result.AddNew(System.Reflection.Missing.Value, _
                          System.Reflection.Missing.Value)

                For columnIndex As Integer = 0 To inColumns.Count - 1
                    If Not DBNull.Value.Equals(dr(columnIndex)) Then
                        If dr(columnIndex).ToString <> "" Then

                            Value = dr(columnIndex).ToString
                            'Estos son caracteres que no pueden ser exportados a excel,
                            'el proceso se cae cuando los encuentra.

                            If Value.IndexOf("�") >= 0 Then
                                Value = Replace(Value, "�", "")
                            End If
                            If Value.IndexOf("″") >= 0 Then
                                Value = Replace(Value, "″", Chr(34))
                            End If
                            If Value.IndexOf("⅝") >= 0 Then
                                Value = Replace(Value, "⅝", "5/8")
                            End If
                            If Value.IndexOf("⅜") >= 0 Then
                                Value = Replace(Value, "⅜", "3/8")
                            End If
                            If Value.IndexOf("") >= 0 Then
                                Value = Replace(Value, "", "3/8")
                            End If
                            If Value.IndexOf("ǿ") >= 0 Then
                                Value = Replace(Value, "ǿ", "ó")
                            End If
                            resultFields(columnIndex).Value = Value
                        Else
                            resultFields(columnIndex).Value = ""
                        End If
                    Else
                        resultFields(columnIndex).Value = dr(columnIndex)
                    End If
                Next
            Next
            Return result
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Shared Function TranslateType(ByVal columnType As Type) As ADODB.DataTypeEnum
        Select Case columnType.UnderlyingSystemType.ToString()

            Case "System.Boolean"
                Return ADODB.DataTypeEnum.adBoolean

            Case "System.Byte"
                Return ADODB.DataTypeEnum.adUnsignedTinyInt

            Case "System.Char"
                Return ADODB.DataTypeEnum.adChar

            Case "System.DateTime"
                Return ADODB.DataTypeEnum.adDate

            Case "System.Decimal"
                Return ADODB.DataTypeEnum.adCurrency

            Case "System.Double"
                Return ADODB.DataTypeEnum.adDouble

            Case "System.Int16"
                Return ADODB.DataTypeEnum.adSmallInt

            Case "System.Int32"
                Return ADODB.DataTypeEnum.adInteger

            Case "System.Int64"
                Return ADODB.DataTypeEnum.adBigInt

            Case "System.SByte"
                Return ADODB.DataTypeEnum.adTinyInt

            Case "System.Single"
                Return ADODB.DataTypeEnum.adSingle

            Case "System.UInt16"
                Return ADODB.DataTypeEnum.adUnsignedSmallInt

            Case "System.UInt32"
                Return ADODB.DataTypeEnum.adUnsignedInt

            Case "System.UInt64"
                Return ADODB.DataTypeEnum.adUnsignedBigInt

        End Select

        'Note Strings are not cased and will return here:
        Return ADODB.DataTypeEnum.adVarChar

    End Function

    Public Function GetScorecardSummary(ByVal pTable As System.Data.DataTable) As Boolean
        Dim xlApp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim oQueryTable As Excel.QueryTable
        Dim rs As ADODB.Recordset


        Try
            xlApp = CreateObject("Excel.Application")

            xlApp.UserControl = True
            xlBook = xlApp.Workbooks.Add
            xlSheet = xlBook.Worksheets(1)

            rs = ConvertToRecordset(pTable)

            oQueryTable = xlSheet.QueryTables.Add(rs, xlSheet.Cells(1, 1))
            oQueryTable.Refresh()

            xlApp.Visible = True

            xlSheet.Range("1:1").Select()
            xlApp.Selection.insert(iXL.XlDirection.xlDown)

            '*********************************************
            xlSheet.Range("A1:C1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "Location"
            xlApp.Range("D1:E1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "Order Count"
            xlApp.Range("F1:H1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "Automation"
            xlApp.Range("I1:J1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "Order Creation And Transmition"
            xlApp.Range("K1:M1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "Orders Followup"
            xlApp.Range("N1:O1").Select()
            With xlApp.Selection
                .HorizontalAlignment = iXL.XlHAlign.xlHAlignCenter
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = False
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.Merge()
            xlApp.ActiveCell.FormulaR1C1 = "P2P Items"
            xlApp.Range("A1:C2").Select()
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorDark1
                '.ThemeColor = xlApp.xlThemeColorDark1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Interior
                .Pattern = iXL.XlPattern.xlPatternSolid
                .PatternColorIndex = iXL.XlPattern.xlPatternAutomatic
                .ThemeColor = iXL.XlThemeColor.xlThemeColorLight2
                .color = 12611584
                '.Pattern = xlApp.xlSolid
                ' .PatternColorIndex = xlApp.xlAutomatic




                '.ThemeColor = xlApp.xlThemeColorLight2
                .TintAndShade = 0
                .PatternTintAndShade = 0
            End With
            xlApp.Range("D1:E2").Select()
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorDark1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Interior
                .Pattern = iXL.XlPattern.xlPatternSolid
                .PatternColorIndex = iXL.XlPattern.xlPatternAutomatic
                .Color = 6806414
                .TintAndShade = 0
                .PatternTintAndShade = 0
            End With
            xlApp.Range("F1:H2").Select()
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorDark1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorDark1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Interior
                .Pattern = 1
                .PatternColorIndex = -4105
                .ThemeColor = 10
                .TintAndShade = -0.249977111117893
                .PatternTintAndShade = 0
            End With
            xlApp.Range("I1:J2").Select()
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorLight1
                '.ThemeColor = xlApp.xlThemeColorLight1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Interior
                .Pattern = iXL.XlPattern.xlPatternSolid
                .PatternColorIndex = iXL.XlPattern.xlPatternAutomatic
                .Color = 15773696
                .TintAndShade = 0.399975585192419
                .PatternTintAndShade = 0
            End With
            
            xlApp.Range("K1:M2").Select()
            With xlApp.Selection.Interior
                .Pattern = iXL.XlPattern.xlPatternSolid
                .PatternColorIndex = iXL.XlPattern.xlPatternAutomatic
                .Color = 65535
                .TintAndShade = 0
                .PatternTintAndShade = 0
            End With
            xlApp.Range("N1:O2").Select()
            With xlApp.Selection.Font
                .ThemeColor = iXL.XlThemeColor.xlThemeColorDark1
                .TintAndShade = 0
            End With
            With xlApp.Selection.Interior
                .Pattern = iXL.XlPattern.xlPatternSolid
                .PatternColorIndex = iXL.XlPattern.xlPatternAutomatic
                .ThemeColor = iXL.XlThemeColor.xlThemeColorAccent2
                .TintAndShade = -0.249977111117893
                .PatternTintAndShade = 0
            End With
            xlApp.Selection.Font.Bold = True
            xlApp.Range("A1:O2").Select()
            xlApp.Range("N1").Activate()
            xlApp.Selection.Font.Bold = False
            xlApp.Selection.Font.Bold = True
            xlApp.Selection.Borders(5).LineStyle = -4142
            'xlApp.Selection.Borders(xlApp.xlDiagonalDown).LineStyle = xlApp.xlNone
            xlApp.Selection.Borders(6).LineStyle = -4142
            With xlApp.Selection.Borders(7)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(8)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(9)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(10)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            xlApp.Selection.Borders(11).LineStyle = -4142
            xlApp.Selection.Borders(12).LineStyle = -4142
            xlApp.Selection.Borders(5).LineStyle = -4142
            xlApp.Selection.Borders(6).LineStyle = -4142
            With xlApp.Selection.Borders(7)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(8)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(9)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(10)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            xlApp.Selection.Borders(11).LineStyle = -4142
            xlApp.Selection.Borders(12).LineStyle = -4142
            xlApp.Selection.Borders(5).LineStyle = -4142
            xlApp.Selection.Borders(6).LineStyle = -4142
            With xlApp.Selection.Borders(7)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(8)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(9)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = -4138
            End With
            With xlApp.Selection.Borders(10)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            xlApp.Selection.Borders(11).LineStyle = -4142
            xlApp.Selection.Borders(12).LineStyle = -4142
            xlApp.Selection.Borders(5).LineStyle = -4142
            xlApp.Selection.Borders(6).LineStyle = -4142
            With xlApp.Selection.Borders(7)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(8)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(9)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(10)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(11)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            With xlApp.Selection.Borders(12)
                .LineStyle = 1
                .ColorIndex = 0
                .TintAndShade = 0
                .Weight = 2
            End With
            xlApp.Rows("1:1").RowHeight = 12
            xlApp.Rows("1:1").Select()
            With xlApp.Selection
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = True
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
            End With
            xlApp.Rows("2:2").Select()
            xlApp.Selection.RowHeight = 40.5
            xlApp.Selection.RowHeight = 48.75
            With xlApp.Selection
                .HorizontalAlignment = 1
                .VerticalAlignment = iXL.XlVAlign.xlVAlignBottom
                .WrapText = True
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Selection.RowHeight = 70
            With xlApp.Selection
                .HorizontalAlignment = 1
                .VerticalAlignment = -4160
                .WrapText = True
                .Orientation = 0
                .AddIndent = False
                .IndentLevel = 0
                .ShrinkToFit = False
                .ReadingOrder = -5002
                .MergeCells = False
            End With
            xlApp.Columns("F:K").Select()
            xlApp.Range("F2").Activate()
            xlApp.Selection.Style = "Percent"
            xlApp.Columns("O:O").Select()
            xlApp.Range("O2").Activate()
            xlApp.Selection.Style = "Percent"
            xlApp.Range("F:K,O:O").Select()
            xlApp.Range("O2").Activate()
            xlApp.Selection.NumberFormat = "0.00%"

            xlApp.Columns("L:M").Select()
            xlApp.Selection.NumberFormat = "0"

            xlApp.Range("K1:M1").Select()
            xlApp.Selection.UnMerge()
            xlApp.Columns("K:K").Select()
            xlApp.Selection.NumberFormat = "0.00%"
            xlApp.Range("K1:M1").Select()
            xlApp.Selection.Merge()

            xlApp.Range("N1:O1").Select()
            xlApp.Selection.UnMerge()
            xlApp.Columns("N:N").Select()
            xlApp.Selection.NumberFormat = "0"
            xlApp.Range("N1:O1").Select()
            xlApp.Selection.Merge()

            xlApp.Columns("F:F").ColumnWidth = 13.43
            xlApp.Columns("G:G").ColumnWidth = 15.86
            xlApp.Columns("H:H").ColumnWidth = 14.29
            xlApp.Columns("I:I").ColumnWidth = 14.71
            xlApp.Columns("I:I").ColumnWidth = 12.29
            xlApp.Columns("K:K").ColumnWidth = 9.71
            xlApp.Columns("L:L").ColumnWidth = 14.29
            xlApp.Columns("M:M").ColumnWidth = 15
            xlApp.Columns("N:N").ColumnWidth = 14.71
            xlApp.Columns("O:O").ColumnWidth = 16.14
            xlApp.Columns("K:K").ColumnWidth = 9.57

            '*********************************************










            ' xlApp.ActiveWorkbook.SaveAs(pPath)
            'xlApp.ActiveWorkbook.Close()
            'xlApp.Quit()

            'rs.Close()
            Return True
        Catch ex As Exception
            Return Nothing
        Finally
            rs = Nothing
        End Try
    End Function


    Public Function GetStatus161ReleaseFile(ByVal pTable As System.Data.DataTable, ByVal pPath As String) As Boolean
        Dim xlApp As Excel.Application
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim oQueryTable As Excel.QueryTable
        Dim rs As ADODB.Recordset


        Try

            If Len(Dir(pPath)) > 0 Then
                Kill(pPath)
            End If

            xlApp = CreateObject("Excel.Application")

            xlApp.UserControl = True
            xlBook = xlApp.Workbooks.Add
            xlSheet = xlBook.Worksheets(1)

            rs = ConvertToRecordset(pTable)

            oQueryTable = xlSheet.QueryTables.Add(rs, xlSheet.Cells(1, 1))
            oQueryTable.Refresh()

            'xlApp.Visible = True



            xlSheet.Range("A1:F1").Select()

            With xlApp.Selection.Interior
                .colorindex = 11
                
            End With

            With xlApp.Selection
                .HorizontalAlignment = -4108
                .VerticalAlignment = -4108
                .WrapText = True
                .Orientation = 0
                .AddIndent = False
                .ShrinkToFit = True
                .ReadingOrder = -5002

            End With

            xlApp.Selection.Font.colorindex = 2
            xlApp.Selection.Font.Bold = True

            xlSheet.Columns("A:A").ColumnWidth = 4.43
            xlSheet.Columns("B:B").ColumnWidth = 8.57
            xlSheet.Columns("C:C").ColumnWidth = 11.29
            xlSheet.Columns("D:D").ColumnWidth = 9.29
            xlSheet.Columns("E:E").ColumnWidth = 37.14
            xlSheet.Columns("F:F").ColumnWidth = 16

            xlApp.ActiveWorkbook.SaveAs(pPath)
            xlApp.ActiveWorkbook.Close()
            xlApp.Quit()

            'rs.Close()
            Return True
        Catch ex As Exception
            Return Nothing
        Finally
            rs = Nothing
        End Try
    End Function

End Class
