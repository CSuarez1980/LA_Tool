Public Class frm091
    Private WithEvents _N6P As New WH
    Private WithEvents _L7P As New WH
    Private _EKBE As New MultiThreading.WorkerHandler


    Private _StartDate As String
    Private _EndDate As String
    Public _N6PData As Object
    Public _L7PData As Object

    Public _Finished As Integer = 0


    Public Property StartDate() As String
        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return _EndDate
        End Get
        Set(ByVal value As String)
            _EndDate = value
        End Set
    End Property

    Private Sub Finished()
        _Finished += 1

        If _Finished = 2 Then

            Me.Close()
        End If

    End Sub
    Private Sub Report(ByVal Message As String)
        txtMessage.Text = Message
    End Sub
    Private Sub ReportChange(ByVal Message As String)
        txtMessage.Text = Message
    End Sub

    Private Sub Check_EKBE()
        Dim i As Integer = 0
        Dim EKKO As New EKKO_W
        Dim EKET As New EKET_W
        Dim EKBETable As New DataTable
        _N6P.Clear_Workers()


        For Each W As EKBE In _EKBE.Workers
            If W.Region = "LA" Then
                _L7PData = W.Purch_Docs
            Else
                _N6PData = W.Purch_Docs
            End If

            EKBETable.Merge(W.Data)
        Next

        If EKBETable.Rows.Count > 0 Then
            Dim cn As New OAConnection.Connection
            cn.AppendTableToSqlServer("SC_EKBE", EKBETable)
        End If

        If Not _N6PData Is Nothing Then
            For Each PO As String In _N6PData
                i += 1
                EKKO.Include_Document(PO)
                EKET.Include_Document(PO)

                If i = 1000 Then
                    EKKO.SAPBox = "N6P"
                    _N6P.Include_Worker(EKKO)
                    EKKO = New EKKO_W
                    i = 0

                    EKET.SAPBox = "N6P"
                    _N6P.Include_Worker(EKET)
                    EKET = New EKET_W
                End If
            Next

            If i > 0 Then
                EKKO.SAPBox = "N6P"
                _N6P.Include_Worker(EKKO)

                EKET.SAPBox = "N6P"
                _N6P.Include_Worker(EKET)
            End If

            _N6P.Run_Process()
        End If


        If Not _L7PData Is Nothing Then
            i = 0
            Dim EKKO2 As New EKKO_W
            Dim EKET2 As New EKET_W
            _L7P.Clear_Workers()

            For Each PO As String In _L7PData
                i += 1
                EKKO2.Include_Document(PO)
                EKET2.Include_Document(PO)

                If i = 1000 Then
                    EKKO2.SAPBox = "L7P"
                    _L7P.Include_Worker(EKKO2)
                    EKKO2 = New EKKO_W
                    i = 0

                    EKET2.SAPBox = "L7P"
                    _L7P.Include_Worker(EKET2)
                    EKET2 = New EKET_W
                End If
            Next

            If i > 0 Then
                EKKO2.SAPBox = "L7P"
                _L7P.Include_Worker(EKKO2)

                EKET2.SAPBox = "L7P"
                _L7P.Include_Worker(EKET2)
            End If

            _L7P.Run_Process()
        End If


    End Sub

    Private Sub frm091_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        AddHandler _N6P.Report_Downloaded, AddressOf Me.Finished
        AddHandler _L7P.Report_Downloaded, AddressOf Me.Finished

        AddHandler _N6P.ReportChange, AddressOf Me.Report
        AddHandler _L7P.ReportChange, AddressOf Me.Report

        AddHandler _EKBE.ProcessFinished, AddressOf Check_EKBE
        AddHandler _EKBE.ReportChange, AddressOf Me.Report

        Dim _EKBELA As New EKBE With {.Region = "LA", .SAPBox = "L7P", .StartDate = _StartDate, .EndDate = _EndDate}
        Dim _EKBENA As New EKBE With {.Region = "NA", .SAPBox = "N6P", .StartDate = _StartDate, .EndDate = _EndDate}

        _EKBE.Include_Worker(_EKBELA)
        _EKBE.Include_Worker(_EKBENA)

        _EKBE.Run_Process()

        'Dim r As New SAPCOM.EKBE_Report("N6P", gsUsuarioPC, AppId)
        'Dim TEKBE As New DataTable
        'Dim cn As New OAConnection.Connection

        'Report("Getting EKBE: N6P")
        'If r.IsReady Then
        '    r.PostingDateFrom = Date.Parse(_StartDate)
        '    r.PostingDateTo = _EndDate
        '    r.MaterialFrom = "30000000"
        '    r.MaterialTo = "39999999"
        '    r.Include_Movement("101")
        '    r.Include_Movement("105")
        '    r.Execute()

        '    If r.Success Then
        '        TEKBE = r.Data

        '        _N6PData = (From C In TEKBE.AsEnumerable _
        '                   Select C.Item("Purch Doc")).Distinct

        '        Dim UEKBE As New DataColumn
        '        UEKBE.ColumnName = "TNumber"
        '        UEKBE.Caption = "TNumber"
        '        UEKBE.DefaultValue = gsUsuarioPC

        '        Dim Reg As New DataColumn
        '        Reg.ColumnName = "Region"
        '        Reg.Caption = "Region"
        '        Reg.DefaultValue = "NA"

        '        TEKBE.Columns.Add(UEKBE)
        '        TEKBE.Columns.Add(Reg)

        '        cn.AppendTableToSqlServer("SC_EKBE", TEKBE)

        '        Dim i As Integer = 0
        '        Dim EKKO As New EKKO_W
        '        Dim EKET As New EKET_W
        '        _N6P.Clear_Workers()

        '        For Each PO As String In _N6PData
        '            i += 1
        '            EKKO.Include_Document(PO)
        '            EKET.Include_Document(PO)

        '            If i = 100 Then
        '                EKKO.SAPBox = "N6P"
        '                _N6P.Include_Worker(EKKO)
        '                EKKO = New EKKO_W
        '                i = 0

        '                EKET.SAPBox = "N6P"
        '                _N6P.Include_Worker(EKET)
        '                EKET = New EKET_W
        '            End If
        '        Next

        '        If i > 0 Then
        '            EKKO.SAPBox = "N6P"
        '            _N6P.Include_Worker(EKKO)

        '            EKET.SAPBox = "N6P"
        '            _N6P.Include_Worker(EKET)
        '        End If

        '        _N6P.Run_Process()


        '    End If
        'End If


        'Report("Getting EKBE: L7P")
        'Dim r2 As New SAPCOM.EKBE_Report("L7P", gsUsuarioPC, AppId)
        'Dim TEKBE2 As New DataTable
        'If r2.IsReady Then
        '    r2.PostingDateFrom = Date.Parse(_StartDate)
        '    r2.PostingDateTo = _EndDate
        '    r2.MaterialFrom = "30000000"
        '    r2.MaterialTo = "39999999"
        '    r2.Include_Movement("101")
        '    r2.Include_Movement("105")
        '    r2.Execute()

        '    If r2.Success Then
        '        TEKBE2 = r2.Data

        '        _L7PData = (From C In TEKBE2.AsEnumerable _
        '                   Select C.Item("Purch Doc")).Distinct

        '        Dim UEKBE2 As New DataColumn
        '        UEKBE2.ColumnName = "TNumber"
        '        UEKBE2.Caption = "TNumber"
        '        UEKBE2.DefaultValue = gsUsuarioPC

        '        Dim Reg2 As New DataColumn
        '        Reg2.ColumnName = "Region"
        '        Reg2.Caption = "Region"
        '        Reg2.DefaultValue = "LA"

        '        TEKBE2.Columns.Add(UEKBE2)
        '        TEKBE2.Columns.Add(Reg2)

        '        cn.AppendTableToSqlServer("SC_EKBE", TEKBE2)

        '        Dim i As Integer = 0
        '        Dim EKKO As New EKKO_W
        '        Dim EKET As New EKET_W
        '        _L7P.Clear_Workers()

        '        For Each PO As String In _L7PData
        '            i += 1
        '            EKKO.Include_Document(PO)
        '            EKET.Include_Document(PO)

        '            If i = 100 Then
        '                EKKO.SAPBox = "L7P"
        '                _L7P.Include_Worker(EKKO)
        '                EKKO = New EKKO_W
        '                i = 0

        '                EKET.SAPBox = "L7P"
        '                _L7P.Include_Worker(EKET)
        '                EKET = New EKET_W
        '            End If
        '        Next

        '        If i > 0 Then
        '            EKKO.SAPBox = "L7P"
        '            _L7P.Include_Worker(EKKO)

        '            EKET.SAPBox = "L7P"
        '            _L7P.Include_Worker(EKET)
        '        End If

        '        _L7P.Run_Process()
        '    End If
        'End If
    End Sub
End Class

Public Class EKKO_W
    Inherits MultiThreading.Worker

#Region "Variables"
    Private _SAPBox As String
    Private _POs As New List(Of String)
#End Region
#Region "Properties"
    Public Property SAPBox() As String
        Get
            Return _SAPBox
        End Get
        Set(ByVal value As String)
            _SAPBox = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub Include_Document(ByVal Document As String)
        _POs.Add(Document)
    End Sub
    Public Overrides Sub MyWork()
        Dim EKKO As New SAPCOM.EKKO_Report(_SAPBox, gsUsuarioPC, AppId)

        If EKKO.IsReady Then
            For Each PO As String In _POs
                EKKO.IncludeDocument(PO)
            Next

            EKKO.Execute()
            If EKKO.Success Then
                Dim UEKET As New DataColumn
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC

                Dim Reg As New DataColumn
                Reg.ColumnName = "Region"
                Reg.Caption = "Region"
                Reg.DefaultValue = IIf(_SAPBox = "L7P", "LA", "NA")

                EKKO.Data.Columns.Add(UEKET)
                EKKO.Data.Columns.Add(Reg)
                'Dim cn As New OAConnection.Connection
                'cn.AppendTableToSqlServer("SC_OTD_EKKO", EKKO.Data)
                Data = EKKO.Data
            End If
        Else
            MsgBox("Fail to connect with RFC Class: BGEKKO Box: " & _SAPBox & "Error message: " & EKKO.ErrMessage)
        End If
    End Sub
#End Region


End Class
Public Class EKET_W
    Inherits MultiThreading.Worker

#Region "Variables"
    Private _SAPBox As String
    Private _POs As New List(Of String)
#End Region
#Region "Properties"
    Public Property SAPBox() As String
        Get
            Return _SAPBox
        End Get
        Set(ByVal value As String)
            _SAPBox = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub Include_Document(ByVal Document As String)
        _POs.Add(Document)
    End Sub
    Public Overrides Sub MyWork()
        Dim R2 As New SAPCOM.EKET_Report(_SAPBox, gsUsuarioPC, AppId)
        Dim TEKET As New DataTable
        Dim cn As New OAConnection.Connection

        If R2.IsReady Then
            For Each PO As String In _POs
                R2.IncludeDocument(PO)
            Next

            R2.Execute()

            If R2.Success Then
                TEKET = R2.Data
                Dim UEKET As New DataColumn
                'Columna del Usuario que descarga el reporte
                UEKET.ColumnName = "TNumber"
                UEKET.Caption = "TNumber"
                UEKET.DefaultValue = gsUsuarioPC

                Dim Reg As New DataColumn
                Reg.ColumnName = "Region"
                Reg.Caption = "Region"
                Reg.DefaultValue = IIf(_SAPBox = "L7P", "LA", "NA")

                TEKET.Columns.Add(UEKET)
                TEKET.Columns.Add(Reg)

                'cn.AppendTableToSqlServer("SC_EKET", TEKET)
                Data = TEKET
                'cn.RunSentence("Delete From SC_EKET Where ([Delivery date] = '00000000') or ([Stat Del Date] = '00000000')")
            End If
        Else
            MsgBox("Unable to get SAP connection to EKET: " & _SAPBox & ". Message: " & R2.ErrMessage)
        End If
    End Sub
#End Region

End Class

Public Class EKBE
    Inherits MultiThreading.Worker

    Private _StartDate As String
    Private _EndDate As String
    Private _Region As String
    Private _SAPBox As String
    Private _Data As Object

    Public Property SAPBox() As String
        Get
            Return _SAPBox
        End Get
        Set(ByVal value As String)
            _SAPBox = value
        End Set
    End Property
    Public Property StartDate() As String
        Get
            Return _StartDate
        End Get
        Set(ByVal value As String)
            _StartDate = value
        End Set
    End Property
    Public Property EndDate() As String
        Get
            Return _EndDate
        End Get
        Set(ByVal value As String)
            _EndDate = value
        End Set
    End Property
    Public Property Region() As String
        Get
            Return _Region
        End Get
        Set(ByVal value As String)
            _Region = value
        End Set
    End Property
    Public ReadOnly Property Purch_Docs() As Object
        Get
            Return _Data
        End Get
    End Property
    Public Overrides Sub MyWork()
        Dim r As New SAPCOM.EKBE_Report(_SAPBox, gsUsuarioPC, AppId)
        Dim TEKBE As New DataTable
        Dim cn As New OAConnection.Connection

        If r.IsReady Then
            r.PostingDateFrom = Date.Parse(_StartDate)
            r.PostingDateTo = _EndDate
            r.MaterialFrom = "30000000"
            r.MaterialTo = "39999999"
            r.Include_Movement("101")
            r.Include_Movement("105")
            r.Execute()

            If r.Success Then
                TEKBE = r.Data
                Dim X = (From C In TEKBE.AsEnumerable _
                        Select C.Item("Purch Doc")).Distinct
                _Data = X

                Dim UEKBE As New DataColumn
                UEKBE.ColumnName = "TNumber"
                UEKBE.Caption = "TNumber"
                UEKBE.DefaultValue = gsUsuarioPC

                Dim Reg As New DataColumn
                Reg.ColumnName = "Region"
                Reg.Caption = "Region"
                Reg.DefaultValue = _Region

                TEKBE.Columns.Add(UEKBE)
                TEKBE.Columns.Add(Reg)

                Data = TEKBE
            End If
        End If
    End Sub
End Class
Public Class WH
    Inherits MultiThreading.WorkerHandler

#Region "Events"
    Public Event Report_Downloaded()
#End Region
#Region "Variables"
    Private _EKKOData As New DataTable
    Private _EKETData As New DataTable
    Private _Data As New DataTable
#End Region
#Region "Properties"
    Public Shadows ReadOnly Property Data() As DataTable
        Get
            Return _Data
        End Get
    End Property
#End Region
    Public Sub TheEnd() Handles MyBase.ProcessFinished
        For Each W As Object In Workers
            Select Case W.GetType.Name
                Case "EKKO_W"
                    _EKKOData.Merge(W.Data)
                Case "EKET_W"
                    _EKETData.Merge(W.Data)
            End Select
        Next

        Dim cn As New OAConnection.Connection

        cn.AppendTableToSqlServer("SC_EKET", _EKETData)
        cn.RunSentence("Delete From SC_EKET Where ([Delivery date] = '00000000') or ([Stat Del Date] = '00000000')")
        cn.AppendTableToSqlServer("SC_OTD_EKKO", _EKKOData)

        '_Data = cn.RunSentence("Select * From vst_SC_OTD Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        RaiseEvent Report_Downloaded()
    End Sub

End Class
