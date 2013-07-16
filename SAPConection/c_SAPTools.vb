Public Class c_SAPTools
    ''' <summary>
    ''' Crear una tabla con la estructura necesaria para el source list de los contratos
    ''' </summary>
    ''' <returns>DataTable con estructura del Source List</returns>
    ''' <remarks></remarks>
    Public Function GetSourcceListTableStructure() As DataTable
        Dim colunmText As DataColumn

        GetSourcceListTableStructure = New DataTable("SourceList")
        colunmText = New DataColumn("Client")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.Integer")
        colunmText.Caption = "Client"
        colunmText.Unique = True
        GetSourcceListTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = 0

        colunmText = New DataColumn("Material")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Material"
        colunmText.Unique = False
        GetSourcceListTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Plant")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.String")
        colunmText.Caption = "Plant"
        colunmText.Unique = False
        GetSourcceListTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""

        colunmText = New DataColumn("Start")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.DateTime")
        colunmText.Caption = "Start"
        colunmText.Unique = False
        GetSourcceListTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""


        colunmText = New DataColumn("End")
        colunmText.AllowDBNull = True
        colunmText.DataType.GetType("System.DateTime")
        colunmText.Caption = "End"
        colunmText.Unique = False
        GetSourcceListTableStructure.Columns.Add(colunmText)
        colunmText.DefaultValue = ""



    End Function

    Public Function ImportSourceListData(ByVal File$) As DataTable
        Dim MyReader As Microsoft.VisualBasic.FileIO.TextFieldParser
        Dim Tabla As New DataTable
        Dim ldgica As Double

        Tabla = Me.GetSourcceListTableStructure

        MyReader = My.Computer.FileSystem.OpenTextFieldParser(File)
        MyReader.SetDelimiters("|")

        Dim textFields As String()
        While Not MyReader.EndOfData
            Try
                textFields = MyReader.ReadFields()

                If textFields(0).Length = 0 Then
                    If textFields(2).ToUpper.Trim <> "CLIENT" Then
                        Tabla.Rows.Add()
                        If Double.TryParse(textFields(3).ToString, ldgica) Then
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Client") = textFields(2).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Material") = ldgica
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Plant") = textFields(4).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("Planta") = ""
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("Start") = textFields(5).ToUpper.Trim
                            Tabla.Rows(Tabla.Rows.Count - 1).Item("End") = textFields(6).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("AutoPO") = textFields(6).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("S_List") = textFields(7).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("TaxCode") = textFields(8).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("Manufacter") = textFields(9).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("Man_Name") = textFields(10).ToUpper.Trim
                            'Tabla.Rows(Tabla.Rows.Count - 1).Item("Part_Number") = textFields(11).ToUpper.Trim
                        End If
                    End If
                End If

            Catch ex As Exception
            End Try
        End While
        Return Tabla

    End Function



End Class

'Public Class objCrearSourceList
'    Private _Material As String = ""
'    Private _Planta As String = ""
'    Private _FechaInicio As Date = Now.Today
'    Private _FechaFinal As Date = Now.Today
'    Private _Vendor As String = ""
'    Private _PurchGrp As String = ""
'    Private _Contrato As String = ""
'    Private _OAItem As String = ""

'    ''' <summary>
'    ''' Número de material
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property Material() As String
'        Get
'            Return _Material
'        End Get

'        Set(ByVal value As String)
'            _Material = value
'        End Set
'    End Property

'    ''' <summary>
'    ''' Código de la planta
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property Planta() As String
'        Get
'            Return _Planta
'        End Get
'        Set(ByVal value As String)
'            _Planta = value
'        End Set
'    End Property

'    ''' <summary>
'    ''' Fecha de inicio del source list [Debería ser la misma del contrato]
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property FechaInicio() As Date
'        Get
'            Return _FechaInicio
'        End Get
'        Set(ByVal value As Date)
'            _FechaInicio = value
'        End Set
'    End Property

'    ''' <summary>
'    ''' Fecha final del source list [Debería ser la misma fecha final del contrato]
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property FechaFinal() As Date
'        Get
'            Return _FechaFinal
'        End Get
'        Set(ByVal value As Date)
'            _FechaFinal = value
'        End Set
'    End Property

'    ''' <summary>
'    ''' Código del proveedor
'    ''' </summary>
'    ''' <value></value>
'    ''' <returns></returns>
'    ''' <remarks></remarks>
'    Public Property Vendor() As String
'        Get
'            Return _Vendor
'        End Get
'        Set(ByVal value As String)
'            _Vendor = value
'        End Set
'    End Property

'End Class