Namespace Reports
    Public Enum Actions
        Include = 1
        Exclude = 2
    End Enum

    Public Class BaseReport
#Region "Variables"
        Private _Data As DataTable
        Private _Parameters As New List(Of Parameter)
        Private _SQL_Table As String
        Private _Report_Fields As String
#End Region
#Region "Properties"
        Public ReadOnly Property Data()
            Get
                Return _Data
            End Get
        End Property
        Public ReadOnly Property Parameters() As List(Of Parameter)
            Get
                Return _Parameters
            End Get
        End Property
        Public Property SQL_Table() As String
            Get
                Return _SQL_Table
            End Get
            Set(ByVal value As String)
                _SQL_Table = value
            End Set
        End Property
        Public Property Report_Field() As String
            Get
                Return _Report_Fields
            End Get
            Set(ByVal value As String)
                _Report_Fields = value
            End Set
        End Property

#End Region
#Region "Methods"
        Public Sub New()
            Clear()
        End Sub
        Public Function Clear()
            _Data = New DataTable
            _Parameters.Clear()
        End Function
        Public Sub Add_Parameter(ByVal pField As String, ByVal pValue As String, Optional ByVal pAction As Actions = Actions.Include)
            _Parameters.Add(New Parameter(pField, pValue, pAction))
        End Sub
        Public Function Excecute() As Boolean
            Dim cn As New OAConnection.Connection
            Dim SQL As String = ""

            SQL = "Select " & _Report_Fields & " From " & _SQL_Table

            If _Parameters.Count > 0 Then
                Dim Filter As String = ""

                SQL = SQL & " Where "

                For Each P As Parameter In _Parameters
                    Filter = "([" & P.Field & "]" & IIf(P.Action = Actions.Include, " = ", " <> ") & "@" & P.Value & ")"
                Next
            End If

        End Function

#End Region
    End Class

    Public Class Parameter
#Region "Variables"
        Private _Field As String
        Private _Value As String
        Private _Action As Actions
#End Region
#Region "Properties"
        Public Property Field() As String
            Get
                Return _Field
            End Get
            Set(ByVal value As String)
                _Field = value
            End Set
        End Property
        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
            End Set
        End Property
        Public Property Action() As Actions
            Get
                Return _Action
            End Get
            Set(ByVal value As Actions)
                _Action = value
            End Set
        End Property
#End Region
#Region "Methods"
        Public Sub New()
            _Field = ""
            _Value = ""
            _Action = Actions.Include
        End Sub
        Public Sub New(ByVal pField As String, ByVal pValue As String, Optional ByVal pAction As Actions = Actions.Include)
            _Field = pField
            _Value = pValue
            _Action = pAction
        End Sub
#End Region
    End Class

End Namespace




