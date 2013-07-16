Imports System.Windows.Forms

Namespace ConfigurationGrid
    Public Class DataGrid
#Region "Variables"
        Private _Columns As New List(Of Column)
        Private _GridConfiguration As DataTable
        Private _cn As New OAConnection.Connection
        Public _MyDataGrid As System.Windows.Forms.DataGridView
        Private _TNumber As String
        Private _FormId As String
#End Region

#Region "Properties"
        Public Property Columns() As List(Of Column)
            Get
                Return _Columns
            End Get
            Set(ByVal value As List(Of Column))
                _Columns = value
            End Set
        End Property

        'Public Property DataGrid() As System.Windows.Forms.DataGridView
        '    Get
        '        Return _MyDataGrid
        '    End Get
        '    Set(ByVal value As System.Windows.Forms.DataGridView)
        '        _MyDataGrid = value
        '    End Set
        'End Property

        Public ReadOnly Property TNumber() As String
            Get
                Return _TNumber
            End Get
        End Property

        Public ReadOnly Property FormID() As String
            Get
                Return _FormId
            End Get

        End Property
#End Region

#Region "Methods"

        Public Sub New(ByVal TNumber As String, ByVal FormId As String)
            _TNumber = TNumber
            _FormId = FormId
        End Sub

        Public Sub GetConfiguration()
            If Not _MyDataGrid Is Nothing Then

                _GridConfiguration = _cn.RunSentence("Select * From Config_Forms Where Usuario = '" & _TNumber & "' And NombreFormulario = '" & _FormId & "' Order by Orden").Tables(0)
                If _GridConfiguration.Rows.Count > 0 Then

                    Dim Row As DataRow
                    For Each Row In _GridConfiguration.Rows
                        Dim NewColumn As New Column
                        NewColumn.ColumnName = Row("Columna")
                        NewColumn.Position = Row("Orden")
                        NewColumn.Visible = Row("Mostrar")

                        _Columns.Add(NewColumn)
                    Next
                Else
                    Dim Col As System.Windows.Forms.DataGridViewColumn
                    Dim NewColumn As New Column

                    For Each Col In _MyDataGrid.Columns
                        _cn.ExecuteInServer("Insert Into Config_Forms(Usuario,NombreFormulario,Columna,Mostrar,Orden) " & _
                                          "Values('" & _TNumber & "','" & FormID & "','" & Col.HeaderText & "'," & IIf(Col.Visible, 1, 0) & "," & Col.DisplayIndex & ")")

                        NewColumn.ColumnName = Col.HeaderText
                        NewColumn.Position = Col.DisplayIndex
                        NewColumn.Visible = Col.Visible

                        _Columns.Add(NewColumn)
                    Next
                End If
            Else
                MsgBox("Please set the datagrid to the class object", MsgBoxStyle.Critical)
            End If
        End Sub

        Public Sub SetConfiguration()
            If _Columns.Count > 0 Then
                Dim Col As OAConnection.ConfigurationGrid.Column
                For Each Col In _Columns
                    _MyDataGrid.Columns(Col.ColumnName).Visible = Col.Visible
                    _MyDataGrid.Columns(Col.ColumnName).DisplayIndex = Col.Position
                    Select Case _MyDataGrid.Columns(Col.ColumnName).Name
                        Case "CK", "RC Text", "Comments", "R_Code", "Status", "RootCause"
                            _MyDataGrid.Columns(Col.ColumnName).ReadOnly = False
                        Case Else
                            _MyDataGrid.Columns(Col.ColumnName).ReadOnly = True

                    End Select
                Next

            Else
                MsgBox("No column configuration was found.")
            End If

        End Sub

        Public Function SaveConfiguration() As Boolean
            Try
                Dim Col As System.Windows.Forms.DataGridViewColumn

                For Each Col In _MyDataGrid.Columns
                    _cn.ExecuteInServer("Update Config_Forms Set Orden = " & Col.DisplayIndex & " Where Usuario = '" & _TNumber & "' And NombreFormulario = '" & _FormId & "' And Columna = '" & Col.HeaderText & "'")
                Next

                Return True

            Catch ex As Exception
                Return False
            End Try
        End Function
#End Region

    End Class

    Public Class Column
#Region "Variables"
        Private _Position As Integer
        Private _Visible As Boolean
        Private _ColumnName As String
#End Region

#Region "Propiedades"
        Public Property Position() As Integer
            Get
                Return _Position
            End Get
            Set(ByVal value As Integer)
                _Position = value
            End Set
        End Property

        Public Property Visible() As Boolean
            Get
                Return _Visible
            End Get
            Set(ByVal value As Boolean)
                _Visible = value
            End Set
        End Property

        Public Property ColumnName() As String
            Get
                Return _ColumnName
            End Get
            Set(ByVal value As String)
                _ColumnName = value
            End Set
        End Property
#End Region
    End Class
End Namespace