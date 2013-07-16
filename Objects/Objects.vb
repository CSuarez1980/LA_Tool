Public Class Material
#Region "Variables"
    Private _Material As String
    Private _PDT As Integer
    Private _AutoPO As Boolean
    Private _SourceList As Boolean
    Private _Price As Double
    Private _OA_Line_Item As Integer
#End Region

#Region "Properties"

    ''' <summary>
    ''' Material number (Gica)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Material() As String
        Get
            Return _Material
        End Get
        Set(ByVal value As String)
            _Material = value
        End Set
    End Property

    ''' <summary>
    ''' Planned delivery date
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PDT() As Integer
        Get
            Return _PDT
        End Get
        Set(ByVal value As Integer)
            _PDT = value
        End Set
    End Property

    ''' <summary>
    ''' Check for AutoPO
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutoPO() As Boolean
        Get
            Return _AutoPO
        End Get
        Set(ByVal value As Boolean)
            _AutoPO = value
        End Set
    End Property

    ''' <summary>
    ''' Check for Source List
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SourceList() As Boolean
        Get
            Return _SourceList
        End Get
        Set(ByVal value As Boolean)
            _SourceList = value
        End Set
    End Property

    ''' <summary>
    ''' Material price
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Price() As Double
        Get
            Return _Price
        End Get
        Set(ByVal value As Double)
            _Price = value
        End Set
    End Property

    ''' <summary>
    ''' Outline agreement line item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AO_Line_Item() As Integer
        Get
            Return _OA_Line_Item
        End Get
        Set(ByVal value As Integer)
            _OA_Line_Item = value
        End Set
    End Property

#End Region



End Class
