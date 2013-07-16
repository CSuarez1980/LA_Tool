Public Class DMS_User
#Region "Variables"
    Private _SPS As String = Nothing
    Private _PTB As String = Nothing
    Private _Mat_Group As String = Nothing
    Private _Purch_Grp As String = Nothing
    Private _Purch_Org As String = Nothing
    Private _Plant As String = Nothing
    Private _SAPBox As String = Nothing
    Private _Success As Boolean = False
    Private _Message As String = Nothing
    Private _Type As String = Nothing
#End Region
#Region "Properties"
    Public Property SPS() As String
        Get
            Return _SPS
        End Get
        Set(ByVal value As String)
            _SPS = value
        End Set
    End Property
    Public Property PTB() As String
        Get
            Return _PTB
        End Get
        Set(ByVal value As String)
            _PTB = value
        End Set
    End Property
    Public Property Material_Group() As String
        Get
            Return _Mat_Group
        End Get
        Set(ByVal value As String)
            _Mat_Group = value
        End Set
    End Property
    Public Property Purch_Grp() As String
        Get
            Return _Purch_Grp
        End Get
        Set(ByVal value As String)
            _Purch_Grp = value
        End Set
    End Property
    Public Property Purch_Org() As String
        Get
            Return _Purch_Org
        End Get
        Set(ByVal value As String)
            _Purch_Org = value
        End Set
    End Property
    Public Property Plant() As String
        Get
            Return _Plant
        End Get
        Set(ByVal value As String)
            _Plant = value
        End Set
    End Property
    Public Property SAPBox() As String
        Get
            Return _SAPBox
        End Get
        Set(ByVal value As String)
            _SAPBox = value
        End Set
    End Property
    Public Property Success() As Boolean
        Get
            Return _Success
        End Get
        Set(ByVal value As Boolean)
            _Success = value
        End Set
    End Property
    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property
    Public Property Type() As String
        Get
            Return _Type
        End Get
        Set(ByVal value As String)
            _Type = value
        End Set
    End Property

#End Region
#Region "Methods"
    Public Sub New(ByVal SAPBox As String, Optional ByVal Mat_Group As String = Nothing, Optional ByVal Purch_Grp As String = Nothing, Optional ByVal Purch_Org As String = Nothing, Optional ByVal Plant As String = Nothing, Optional ByVal Type As String = "")
        _Mat_Group = Mat_Group
        _Purch_Grp = Purch_Grp
        _Purch_Org = Purch_Org
        _Plant = Plant
        _SAPBox = SAPBox
        _Type = Type

        Execute()
    End Sub
    Public Sub New()

    End Sub
    Public Sub Execute()

        If SAPBox Is Nothing Then
            _Success = False
            _Message = "No SAP Box found"
            Exit Sub
        End If

        If Plant Is Nothing Then
            _Success = False
            _Message = "No plant code found"
            Exit Sub
        End If

        Dim cnx As New OAConnection.Connection
        Dim Data As New DataTable
        Dim Param As String = ""

        If Not _Plant Is Nothing Then
            Param = " And (Plant = '" & _Plant & "')"
        End If

        If Not _Mat_Group Is Nothing Then
            Param = Param & " And (([Mat Grp] = '" & _Mat_Group & "') Or ([Mat Grp] = ''))"
        End If

        If Not _Purch_Grp Is Nothing Then
            Param = Param & " And (([P Grp] = '" & _Purch_Grp & "') or ([P Grp] = ''))"
        End If

        If Not _Purch_Org Is Nothing Then
            Param = Param & " And ([P Org] = '" & _Purch_Org & "')"
        End If

        If _Type <> "" Then
            Param = Param & " And ((Type = '" & _Type & "') or (Type = ''))"
        End If

        Data = cnx.RunSentence("Select Top 1 * From LA_Indirect_Distribution Where ((SAPBox = '" & _SAPBox & "')" & Param & ")").Tables(0)

        If Data.Rows.Count > 0 Then
            _SPS = Data.Rows(0)("SPS").ToString
            _PTB = Data.Rows(0)("PTB").ToString
            _Message = Nothing
        Else
            _SPS = "BI5226"
            _PTB = "BI5226"
            _Message = "No record found. Owner was set as default."
        End If

        _Success = True
    End Sub
#End Region
End Class

