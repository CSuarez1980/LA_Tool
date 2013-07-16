Imports System.Data
Imports System.Xml

Public Enum eSQLInstruction
    Insert = 1
    Update = 2
    Delete = 3
    [Select] = 4
End Enum

Public Enum SQLTipoDato
    [Integer] = 1
    [String] = 2
    [Date] = 3
End Enum

''' <summary>
''' Ejecuta instucciones directas a la base datos
''' </summary>
''' <remarks></remarks>
Public Class Table
    Inherits BaseConexion

#Region "variables"
    Private _Adapter As New SqlClient.SqlDataAdapter
#End Region

#Region "Methods"
    ''' <summary>
    ''' Retorna una datable con la información solitada
    ''' </summary>
    ''' <param name="SQlInstruction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTable(ByVal SQLInstruction As String) As DataTable
        Dim TmpTabla As New DataTable
        Dim DB As New Server

        Try
            If OpenConection() Then
                _Adapter = New SqlClient.SqlDataAdapter(SQLInstruction, _ConnectionDB)
                _Adapter.Fill(TmpTabla)
            End If

            Return TmpTabla
        Catch ex As Exception
            Return Nothing

        Finally
            CloseConnection()
            DB = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Hace el volcado de un DataTable en una tabla del servidor
    ''' </summary>
    ''' <param name="TableInServer">Nombre de la tabla en la que se realizará el volcado del DataTable</param>
    ''' <param name="DataTable">DataTable con la información que se incluirá en el servidor</param>
    Public Function AppendTableToSqlServer(ByVal TableInServer$, ByVal DataTable As DataTable) As Boolean
        Dim SqlBulkCopy As System.Data.SqlClient.SqlBulkCopy
        Dim DB As New WEBServer

        Try
            SqlBulkCopy = New SqlClient.SqlBulkCopy(DB.GetConnectionString)
            SqlBulkCopy.DestinationTableName = TableInServer
            SqlBulkCopy.WriteToServer(DataTable)
            Return True

        Catch ex As Exception
            Status = ex.Message
            Return False
        End Try

    End Function
#End Region

End Class

''' <summary>
''' Realiza un batch de instrucciones
''' </summary>
''' <remarks></remarks>
Public Class Transaccion
    Inherits BaseConexion

#Region "Variables"
    Private _Transacciones As New List(Of String)
    Private _Transaccion As SqlClient.SqlTransaction = Nothing

#End Region

#Region "Properties"
    ''' <summary>
    ''' Transacciones almacenadas para ser ejecutadas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Transacciones() As List(Of String)
        Get
            Return _Transacciones
        End Get
    End Property

#End Region

#Region "Methods"
    Public Sub New()
        _Transacciones.Clear()
    End Sub

    ''' <summary>
    ''' Agrega un comando SQL para realizar varias transacciones
    ''' </summary>
    ''' <param name="Transacción">SQL que se ejecutará en el servidor</param>
    ''' <remarks></remarks>
    Public Sub AgregarTransaccion(ByVal Transacción As String)
        _Transacciones.Add(Transacción)
    End Sub

    ''' <summary>
    ''' Elimina las todas transacciones almacenadas 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub EliminarTransacciones()
        _Transacciones.Clear()
    End Sub

    ''' <summary>
    ''' Ejecuta el batch de instrucciones en el servidor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Excecute() As Boolean
        Dim Transaccion As String = ""
        Dim res As Boolean = False
        Dim Comando As SqlClient.SqlCommand

        Try
            If _Transacciones.Count > 0 Then
                If OpenConection() Then
                    _Transaccion = _ConnectionDB.BeginTransaction

                    For Each Transaccion In _Transacciones
                        Comando = New SqlClient.SqlCommand(Transaccion, _ConnectionDB)
                        Comando.Transaction = _Transaccion
                        Comando.ExecuteNonQuery()
                    Next

                    _Transaccion.Commit()

                    res = True
                Else
                    res = False
                    Status = "Error al iniciar la conexion."
                End If
            Else
                res = False
                Status = "No se encontraron transacciones a ejecutar."
            End If


        Catch ex As Exception
            _Transaccion.Rollback()
            res = False
            Status = ex.Message

        Finally
            CloseConnection()
        End Try

        Return res

    End Function

#End Region
End Class

''' <summary>
''' Objeto para agregar parametros SQL
''' </summary>
''' <remarks></remarks>
''' 

Public Class SQLInstrucParam
#Region "variables"
    Private _Campo As String
    Private _Valor As Object
    Private _IsPrimaryKey As Boolean
    Private _TipoDato As SQLTipoDato
#End Region

#Region "Properties"
    ''' <summary>
    ''' Campo en la base de datos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Campo() As String
        Get
            Return _Campo
        End Get
        Set(ByVal value As String)
            _Campo = value
        End Set
    End Property

    ''' <summary>
    ''' Valor que se asignara al campo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Valor() As Object
        Get
            Return _Valor
        End Get
        Set(ByVal value As Object)
            _Valor = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el campo es una llave primaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsPrimaryKey() As Boolean
        Get
            Return _IsPrimaryKey
        End Get
        Set(ByVal value As Boolean)
            _IsPrimaryKey = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica el tipo de dato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TipoDato() As SQLTipoDato
        Get
            Return _TipoDato
        End Get
        Set(ByVal value As SQLTipoDato)
            _TipoDato = value

        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New(ByVal pCampo As String, ByVal pValor As Object, ByVal pIsPrimaryKey As Boolean, Optional ByVal pTipoDato As SQLTipoDato = SQLTipoDato.String)
        _Campo = pCampo
        _Valor = pValor
        _IsPrimaryKey = pIsPrimaryKey
        _TipoDato = pTipoDato
    End Sub

#End Region
End Class

Public Class SQLInstruction
    Inherits BaseConexion

#Region "Variables"
    Private _Tabla As String
    Private _Parametros As New List(Of SQLInstrucParam)
    Private _TipoInstruccion As eSQLInstruction
    Private _Data As New DataTable
#End Region

#Region "Properties"

    ''' <summary>
    ''' Tabla donde se ejecutara la instruccion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Tabla() As String
        Get
            Return _Tabla
        End Get
        Set(ByVal value As String)
            _Tabla = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de parametros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Parametros() As List(Of SQLInstrucParam)
        Get
            Return _Parametros
        End Get
    End Property

    ''' <summary>
    ''' Determina el tipo de instruccion que se ejecutara
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TipoInstruccion()
        Get
            Return _TipoInstruccion
        End Get
    End Property

    ''' <summary>
    ''' Datatable con resultado de consulta select
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Data() As DataTable
        Get
            Return _Data
        End Get
    End Property

#End Region

#Region "Methods"
    Public Sub New(ByVal pInstruction As eSQLInstruction)
        _TipoInstruccion = pInstruction
    End Sub

    Public Sub AgregarParametro(ByVal pParametro As SQLInstrucParam)
        _Parametros.Add(pParametro)
    End Sub

    Private Function GetSQLIntruction() As String
        Dim lCampo As SQLInstrucParam
        Dim SQLIntruction As String = ""
        Dim lsValues As String = ""
        Dim lsCampos As String = ""
        Dim lsWhere As String = ""

        For Each lCampo In _Parametros

            If lsCampos.Length > 0 Or lsValues.Length > 0 Then
                lsCampos = lsCampos & ","
                lsValues = lsValues & ","
            End If

            Select Case _TipoInstruccion

                Case eSQLInstruction.Select
                    lsCampos = lsCampos & lCampo.Campo

                    If lsWhere.Length > 0 AndAlso lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & " And "
                    End If

                    If lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & "(" & lCampo.Campo & " = @" & lCampo.Campo & ")"
                    End If

                Case eSQLInstruction.Insert
                    lsCampos = lsCampos & lCampo.Campo
                    lsValues = lsValues & "@" & lCampo.Campo

                Case eSQLInstruction.Update
                    If lsWhere.Length > 0 AndAlso lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & " And "
                    End If

                    If Not lCampo.IsPrimaryKey Then
                        lsValues = lsValues & lCampo.Campo & " = " & "@" & lCampo.Campo
                    End If

                    If lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & "([" & lCampo.Campo & "] = @" & lCampo.Campo & ")"
                    End If

                Case eSQLInstruction.Delete
                    If lsWhere.Length > 0 AndAlso lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & " And "
                    End If

                    If lCampo.IsPrimaryKey Then
                        lsWhere = lsWhere & "([" & lCampo.Campo & "] = @" & lCampo.Campo & ")"
                    End If

            End Select
        Next


        Select Case _TipoInstruccion
            Case eSQLInstruction.Select
                SQLIntruction = "Select " & lsCampos & " From " & _Tabla & IIf(lsWhere.Length > 0, " Where (" & lsWhere & ")", "")

            Case eSQLInstruction.Insert
                SQLIntruction = "Insert Into " & _Tabla & "(" & lsCampos & ") Values(" & lsValues & ")"

            Case eSQLInstruction.Update
                SQLIntruction = "Update " & _Tabla & " Set " & lsValues & IIf(lsWhere.Length > 0, " Where (" & lsWhere & ")", "")


            Case eSQLInstruction.Delete
                SQLIntruction = "Delete From " & _Tabla & IIf(lsWhere.Length > 0, " Where (" & lsWhere & ")", "")


        End Select
        Return SQLIntruction
    End Function

    Public Function Execute() As Boolean
        Status = ""
        Try
            OpenConection()

            If _TipoInstruccion <> eSQLInstruction.Select Then
                Dim cmd As New SqlClient.SqlCommand(GetSQLIntruction, _ConnectionDB)
                Dim lParametro As SQLInstrucParam



                For Each lParametro In _Parametros
                    cmd.Parameters.AddWithValue("@" & lParametro.Campo, lParametro.Valor)
                Next

                cmd.ExecuteNonQuery()
            Else

                Dim _Adapter As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(GetSQLIntruction, _ConnectionDB)
                'Dim lPar As SqlClient.SqlParameter

                For Each lParametro As SQLInstrucParam In _Parametros
                    If lParametro.IsPrimaryKey Then
                        'Dim p As SqlClient.SqlParameter = _Adapter.SelectCommand.Parameters.Add("@" & lParametro.Campo, lParametro.Valor)
                        _Adapter.SelectCommand.Parameters.Add(New SqlClient.SqlParameter("@" & lParametro.Campo, lParametro.Valor))

                        'lPar = New SqlClient.SqlParameter
                        'lPar.ParameterName = "@" & lParametro.Campo
                        'lPar.Value = lParametro.Valor
                        '_Adapter.SelectCommand.Parameters.Add(lPar)

                    End If
                Next

                _Adapter.Fill(_Data)

            End If



            Return True
        Catch ex As Exception
            Status = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try

    End Function
#End Region
End Class

Public Class BaseConexion

#Region "Variables"
    Private _Status As String
    Public _ConnectionDB As New SqlClient.SqlConnection
#End Region

#Region "Properties"
    ''' <summary>
    ''' Indica el resultado de la ultima operación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status() As String
        Get
            Return _Status
        End Get
        Set(ByVal value As String)
            _Status = value
        End Set
    End Property

    ''' <summary>
    ''' Induca el estado de la conexión
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ConnectionStatus() As Boolean
        Get
            Return _ConnectionDB.State
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Abre la conexion con la base de datos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function OpenConection() As Boolean
        Try
            If _ConnectionDB.State.Equals(ConnectionState.Closed) Then
                Dim DB As New WEBServer
                _ConnectionDB = New SqlClient.SqlConnection(DB.GetConnectionString)
                _ConnectionDB.Open()
            End If

            Return True
        Catch ex As Exception
            _Status = ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Cierra la conexión con la base de datos
    ''' </summary>
    Public Function CloseConnection() As Boolean
        Try
            If _ConnectionDB.State.Equals(ConnectionState.Open) Then
                _ConnectionDB.Close()
            End If

            Return True
        Catch ex As Exception
            _Status = ex.Message
            Return True
        End Try

    End Function
#End Region

End Class

''' <summary>
''' Establece los parámetros para conectarse a la base de datos
''' </summary>
''' <remarks></remarks>
Public Class WEBServer
#Region "Variables"
    Private _ServerName As String
    Private _ServerIP As String
    Private _LoginUser As String
    Private _LoginPassword As String
    Private _DataBase As String
#End Region

#Region "Properties"

    ''' <summary>
    ''' Server name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ServerName() As String
        Get
            Return _ServerName
        End Get
    End Property


    ''' <summary>
    ''' Server IP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ServerIP() As String
        Get
            Return _ServerIP
        End Get
    End Property

    ''' <summary>
    ''' User name for login
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LoginUser() As String
        Get
            Return _LoginUser
        End Get
    End Property

    ''' <summary>
    ''' User password for data base access
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LoginPassword() As String
        Get
            Return _LoginPassword
        End Get
    End Property

    ''' <summary>
    ''' Database name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DataBase() As String
        Get
            Return _DataBase
        End Get
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Returna el string de conexción
    ''' </summary>
    ''' <param name="UseServerIP">Indica si se debe usar la IP del servidor o en nombre</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetConnectionString(Optional ByVal UseServerIP As Boolean = False) As String
        If UseServerIP Then
            Return "Data Source=" & _ServerIP & "; Initial Catalog=" & _DataBase & ";User ID=" & _LoginUser & "; Password=" & _LoginPassword & "; Connection Timeout=3000;"

        Else
            Return "Data Source=" & _ServerName & "; Initial Catalog=" & _DataBase & ";User ID=" & _LoginUser & "; Password=" & _LoginPassword & "; Connection Timeout=3000;"

        End If
    End Function

    ''' <summary>
    ''' Carga la configuración de conexcion del archivo XML por default
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetXmlConnectionString() As Boolean
        Try
            Dim DS As New DataSet
            DS.ReadXml("ServerConfig.xml")

            Dim SC As DataTable = DS.Tables(0)

            _ServerName = SC.Rows(0)("ServerName")
            _DataBase = SC.Rows(0)("DataBaseName")
            _LoginUser = SC.Rows(0)("UserID")
            _LoginPassword = SC.Rows(0)("UserPwr")
            _ServerIP = SC.Rows(0)("ServerIP")

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Function GetDefaultConnectionString() As Boolean
        Try
            _ServerName = "MXL1380Q1V"
            _DataBase = "LA Tool"
            _LoginUser = "SA"
            _LoginPassword = "heavymetal"
            _ServerIP = "131.190.71.208"

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Carga la configuración de conexion desde un archivo XML
    ''' </summary>
    ''' <param name="FilePath">Ruta del archivo de configuración de la base de datos</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetXmlConnectionString(ByVal FilePath As String) As Boolean
        Try
            Dim DS As New DataSet
            DS.ReadXml(FilePath)

            Dim SC As DataTable = DS.Tables(0)

            _ServerName = SC.Rows(0)("ServerName")
            _DataBase = SC.Rows(0)("DataBaseName")
            _LoginUser = SC.Rows(0)("UserID")
            _LoginPassword = SC.Rows(0)("UserPwr")
            _ServerIP = SC.Rows(0)("ServerIP")

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Sub New()
        GetDefaultConnectionString()
    End Sub

    ''' <summary>
    ''' Crea una instancia con referencia a un archivo xml
    ''' </summary>
    ''' <param name="XMLFile">Ruta del archivo XML con la configuración del servidor</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal XMLFile As String)
        GetXmlConnectionString(XMLFile)
    End Sub
#End Region

End Class
