Imports System.Windows.Forms
Module Module1
    Dim Cn As New OAConnection.Connection
    Public Requi As DataRow
    Public gUser$ = ""
    Public gPwr$ = ""
    Public PGUser As Boolean = False

    'Public SAPConfig As Boolean = Cn.RunSentence("Select SAPConfig From [Users] Where TNumber = '" & Cn.GetUserId & "'").Tables(0).Rows(0).Item("SAPConfig")
    'Public PDFPath As String = Cn.RunSentence("Select PDFPath From [Users] Where TNumber = '" & Cn.GetUserId & "'").Tables(0).Rows(0).Item("PDFPath")

    Public SAPConfig As Boolean
    Public PDFPath As String
    Public PDFTimeOut As Double
    Public FreePDF4 As Boolean
    Public QDays As Integer


    ''' <summary>
    ''' Variable para determinar la distribucion entre nacionales o importados
    '''  *****************************************
    ''' Selección de filtro para Import/National
    ''' 0 = Ambos
    ''' 1 = Nacional
    ''' 2 = Importados
    '''*****************************************
    ''' </summary>
    ''' <remarks></remarks>
    Public giDistribution As Integer = 0

    '***********************************************
    '               gUsuario:
    ' -> Variable usada por si el usuario le esta 
    'haciendo backup a otra persona se setea global 
    'para los procesos.
    'El usuario que esta haciendo backup solo podrá
    'utilizar la configuración de la persona, no podrá
    'modificar nada de sus variantes.
    '***********************************************

    '***********************************************
    Public gsUsuario As String = ""

    '               gUsuarioPC:
    ' -> Variable usada para saber quien es el usario
    'que esta usando el sistema.
    '
    'La diferencia con la variable anterior es que no
    'podrá modificar los objetos creados por otros 
    'usuarios solo por él.
    '
    'Reemplaza a la funcion cn.GetUserId
    '***********************************************
    Public gsUsuarioPC As String = ""
    Public gsUserMail As String = ""
    Public AppId As String = "LAT"

    ''' <summary>
    ''' Pega el contenido del portapapeles a un datagrid
    ''' </summary>
    ''' <param name="dgv">Objeto datagrid</param>
    ''' <remarks></remarks>
    Public Sub PasteClipboardToDataGridView(ByVal dgv As DataGridView)
        '**************************************************
        '**************************************************
        '      colocar esta instrucción en el evento _KeyDown del Grid
        'If (e.KeyCode = Keys.V) Then
        ' PasteClipboardToDataGridView(Me.dtGridWiew)
        ' End If
        '**************************************************

        Dim rowSplitter As Char() = {vbCr, vbLf}
        Dim columnSplitter As Char() = {vbTab}

        'get the text from clipboard
        Dim dataInClipboard As IDataObject = Clipboard.GetDataObject()
        Dim stringInClipboard As String = CStr(dataInClipboard.GetData(DataFormats.Text))

        'split it into lines
        Dim rowsInClipboard As String() = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries)

        'get the row and column of selected cell in grid
        Dim r As Integer = dgv.SelectedCells(0).RowIndex
        Dim c As Integer = dgv.SelectedCells(0).ColumnIndex

        'add rows into grid to fit clipboard lines
        If (dgv.Rows.Count < (r + rowsInClipboard.Length)) Then
            dgv.Rows.Add(r + rowsInClipboard.Length - dgv.Rows.Count + 1)
        End If

        ' loop through the lines, split them into cells and place the values in the corresponding cell.

        Dim iRow As Integer = 0
        While iRow < rowsInClipboard.Length
            'split row into cell values
            Dim valuesInRow As String() = rowsInClipboard(iRow).Split(columnSplitter)
            'cycle through cell values
            Dim iCol As Integer = 0

            While iCol < valuesInRow.Length
                'assign cell value, only if it within columns of the grid
                If (dgv.ColumnCount - 1 >= c + iCol) Then
                    dgv.Rows(r + iRow).Cells(c + iCol).Value = valuesInRow(iCol)
                End If
                iCol += 1
            End While

            iRow += 1
        End While
    End Sub
    Public Sub PasteClipboardToDataGridView(ByVal dgv As DataGridView, ByRef Table As DataTable)
        '**************************************************
        '**************************************************
        '      colocar esta instrucción en el evento _KeyDown del Grid
        'If (e.KeyCode = Keys.V) Then
        ' PasteClipboardToDataGridView(Me.dtGridWiew)
        ' End If
        '**************************************************

        Dim data As IDataObject = Clipboard.GetDataObject

        If Not data.GetDataPresent("CSV", False) Then Return

        Try
            ' Obtenemos el texto del Portapapeles, y con él construimos
            ' un array de valores alfanuméricos como producto de obtener
            ' los valores separados por tabulaciones.
            '

            Dim rowSplitter As Char() = {vbCr, vbLf}
            Dim columnSplitter As Char() = {vbTab}

            Dim dataInClipboard As IDataObject = Clipboard.GetDataObject()
            Dim stringInClipboard As String = CStr(dataInClipboard.GetData(DataFormats.Text))

            Dim rowsInClipboard As String() = stringInClipboard.Split(rowSplitter, StringSplitOptions.RemoveEmptyEntries)
            Dim valuesInRow As String()

            Dim iRow As Integer = 0
            Dim Info As String

            For Each Info In rowsInClipboard

                valuesInRow = rowsInClipboard(iRow).Split(columnSplitter)
                If valuesInRow.Length = 0 Then
                    Exit For
                End If

                ' Referenciamos el objeto DataTable enlazado con
                ' el control DataGridView.
                Dim dt As DataTable = _
                DirectCast(dgv.DataSource, DataTable)

                Dim dr As DataRow = dt.NewRow
                ' Conforme recorremos los elementos del array...
                For x As Int32 = 0 To dt.Columns.Count - 1
                    'For x As Int32 = 0 To dgv.ColumnCount - 1
                    ' ... añadimos los valores a las columnas
                    dr.Item(x) = valuesInRow(x)
                Next

                ' Añadimos el objeto DataRow a la colección de
                ' filas del objeto DataTable.
                '
                dt.Rows.Add(dr)
                iRow += 1
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

End Module
