Public Class Tools
    ''' <summary>
    ''' Crear una tabla con la estructura necesaria para Trigger
    ''' </summary>
    ''' <returns>DataTable con estructura de Trigger</returns>
    ''' <remarks></remarks>
    Public Function GetTriggerTableStructure() As DataTable
        Dim colunmText As DataColumn

        GetTriggerTableStructure = New DataTable("Trigger")

        colunmText = New DataColumn("Gica")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.Integer")
        colunmText.Caption = "Gica"
        colunmText.Unique = True
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = 0

        colunmText = New DataColumn("Material")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Material"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Planta")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Planta"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("P_Org")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "P_Org"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("P_Grp")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "P_Grp"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        'colunmText = New DataColumn("AutoPO")
        'colunmText.AllowDBNull = True
        'colunmText.DataType.GetType("System.Windows.Forms.DataGridViewCheckBoxColumn")
        'colunmText.Caption = "AutoPO"
        'colunmText.Unique = False
        'GetTriggerTableStructure.Columns.Add(colunmText)
        'colunmText.DefaultValue = ""

        'colunmText = New DataColumn("S_List")
        'colunmText.AllowDBNull = True
        'colunmText.DataType.GetType("System.String")
        'colunmText.Caption = "S_List"
        'colunmText.Unique = False
        'GetTriggerTableStructure.Columns.Add(colunmText)
        'colunmText.DefaultValue = ""

        colunmText = New DataColumn("TaxCode")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "TaxCode"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        'colunmText = New DataColumn("Acq_Req")
        'colunmText.AllowDBNull = True
        'colunmText.DataType.GetType("System.String")
        'colunmText.Caption = "Acq_Req"
        'colunmText.Unique = False
        'GetTriggerTableStructure.Columns.Add(colunmText)
        'colunmText.DefaultValue = ""

        colunmText = New DataColumn("Manu_Name")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Manu_Name"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Part_Number")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Part_Number"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Vendor")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Vendor"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Contrato")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Contrato"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        'colunmText = New DataColumn("New_Contrato")
        'colunmText.AllowDBNull = True
        'colunmText.DataType.GetType("System.String")
        'colunmText.Caption = "New_Contrato"
        'colunmText.Unique = False
        'GetTriggerTableStructure.Columns.Add(colunmText)
        'colunmText.DefaultValue = ""

        colunmText = New DataColumn("Precio")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Precio"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("PDT")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "PDT"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Currency")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Currency"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Dias")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Días"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Mat_Origin")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Mat_Origin"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Control_Code")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Control_Code"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Info_Update")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Info_Update"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Mat_Group")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Mat_Group"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("ErrorMessage")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "ErrorMessage"
        colunmText.Unique = False
        GetTriggerTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""
    End Function

    ''' <summary>
    ''' Importa un archivo con la estructura especificada en la función GetTriggerTableStructure
    ''' </summary>
    ''' <param name="File">Nombre del archivo con la imformación de Trigger</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function ImportTriggerData(ByVal File$) As DataTable
        Dim MyReader As Microsoft.VisualBasic.FileIO.TextFieldParser
        Dim Tabla As New DataTable
        Dim ldgica As Double
        Dim lsgica$

        Tabla = Me.GetTriggerTableStructure

        MyReader = My.Computer.FileSystem.OpenTextFieldParser(File)
        'MyReader.SetDelimiters("|")
        MyReader.SetDelimiters(Chr(9))

        Dim textFields As String()
        While Not MyReader.EndOfData
            Try
                textFields = MyReader.ReadFields()

                If textFields(0).Trim.ToUpper <> "MAT#" Then
                    If textFields(2).ToUpper.Trim <> "MATERIAL" Then
                        If Double.TryParse(textFields(0).ToString, ldgica) Then
                            Tabla.Rows.Add()
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Gica") = ldgica
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Material") = textFields(6).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Planta") = textFields(1).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("P_Org") = textFields(25).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("P_Grp") = textFields(26).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("AutoPO") = textFields(29).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("S_List") = textFields(30).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("TaxCode") = textFields(28).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Manu_Name") = textFields(12).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Part_Number") = textFields(11).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Mat_Group") = textFields(8).ToUpper.Trim
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End While

        Dim cn As New OAConnection.Connection
        ImportTriggerData = Tabla

    End Function
End Class
