Imports System.Configuration
Imports System.Threading
Imports iTextSharp.text.pdf
Imports iTextSharp


Public Enum PrintingLanguage As Integer
    English = 0
    Spanish = 1
    Portuguese = 2
End Enum

Public Enum Printingtype As Integer
    NEU = 0
    ALE = 1
    ZSU2 = 3
    ZSU4 = 4
    NNXX = 5
    NEXX = 6
    ZGBL = 7
End Enum

Public Class POPrinting
#Region "Variables"
    Private _Documents As New List(Of PurchaseOrder)

    Private _DirectoryPath As String = ""
    Private _AllowPrintAle As Boolean = False
    Private _SAPGUI As Object
    Private _SAPBox As String = ""
    Private _FilePrinted As Integer = 0
    Private _WaitingTime As Integer = 3
    Private _ErrorMessage As New List(Of String)
    Private _TimeOut As Double
    Private _UseFreePDF4 As Boolean
#End Region
#Region "Properties"
    ''' <summary>
    ''' Directory where file will be printed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DirectoryPath() As String
        Get
            Return _DirectoryPath
        End Get
        Set(ByVal value As String)
            _DirectoryPath = value
        End Set
    End Property

    ''' <summary>
    ''' Force to print the document
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowPrintAle() As Boolean
        Get
            Return _AllowPrintAle
        End Get
        Set(ByVal value As Boolean)
            _AllowPrintAle = value
        End Set
    End Property

    ''' <summary>
    ''' Time to abort printing process. Default 3 minutes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WaitingTime() As Integer
        Get
            Return _WaitingTime
        End Get
        Set(ByVal value As Integer)
            _WaitingTime = value
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

    Public ReadOnly Property ErrorFound() As Integer
        Get
            Return _ErrorMessage.Count
        End Get
    End Property

    Public Property TimeOut() As Double
        Get
            Return _TimeOut
        End Get
        Set(ByVal value As Double)
            _TimeOut = value
        End Set
    End Property

    Public Property UseFreePDF4() As Boolean
        Get
            Return _UseFreePDF4
        End Get
        Set(ByVal value As Boolean)
            _UseFreePDF4 = value
        End Set
    End Property

#End Region
#Region "Methods"
    Public Sub New(ByVal pSAPGUI As Object)
        _SAPGUI = pSAPGUI
    End Sub

    Public Sub CheckErrors()
        Dim PO As PurchaseOrder

        For Each PO In _Documents
            If Not PO.Printed Then
                For Each e As String In PO.ErrorList
                    _ErrorMessage.Add(PO.DocNumber & ": " & e)
                Next
            End If
        Next

    End Sub

    ''' <summary>
    ''' Start the printing process
    ''' </summary>
    ''' <param name="pSAPGUI">SAP GUI Interface, login have to be done before exceute</param>
    ''' <remarks></remarks>
    Public Sub Excecute(Optional ByVal ChangeOA As Boolean = Nothing)

        Dim PO As PurchaseOrder
        Dim StartTime As Date = Now()
        Dim TimeOut As Boolean = False

        For Each PO In _Documents
            Try
                If Not _UseFreePDF4 Then
                    If PO.PDFFile Is Nothing Then
                        If System.IO.Directory.GetFiles(_DirectoryPath, "*-1.pdf").Length > 0 Then
                            Dim PDFList As String() = System.IO.Directory.GetFiles(_DirectoryPath, "*-1.pdf")
                            For Each f As String In PDFList
                                My.Computer.FileSystem.MoveFile(f, Replace(f, _DirectoryPath, _DirectoryPath & "\BackUp"), True)
                                System.IO.File.Delete(f)
                            Next
                        End If
                        Print_PO(PO)
                        If PO.Printed Then
                            GetSpoolID(PO)
                            _FilePrinted += 1
                            StartTime = Now
                            Do While True
                                If (Now - StartTime).TotalMinutes < _TimeOut Then
                                    If PO.Printed AndAlso PO.PDFFile Is Nothing Then
                                        ChangeFileName(PO)
                                        If Not PO.PDFFile Is Nothing Then
                                            Exit Do
                                        End If
                                    End If
                                Else
                                    TimeOut = True
                                    SetPDFTimeout(PO.DocNumber)
                                    Exit Do
                                End If
                            Loop
                        End If
                    End If
                Else
                    If PO.PDFFile Is Nothing Then
                        Print_PO(PO)
                        If PO.Printed Then
                            _FilePrinted += 1
                        End If
                    End If

                End If
            Catch ex As Exception
                'Do nothing
            End Try
        Next

        If _UseFreePDF4 Then

            StartTime = Now
            Do
                'Wait until files are printed
                Thread.Sleep(4000)
                _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/n"
                _SAPGUI.findById("wnd[0]").sendVKey(0)

            Loop Until ((System.IO.Directory.GetFiles(_DirectoryPath, "*.pdf").Length = _FilePrinted) Or ((Now - StartTime).TotalMinutes > _TimeOut))

            Dim PDFFiles = System.IO.Directory.GetFiles(_DirectoryPath, "*.pdf")
            Dim PDFPO As String = ""

            For Each F As String In PDFFiles
                Dim mstrRutaPdf As String = ""
                Dim PDFText As String = ""

                mstrRutaPdf = F
                PDFText = Replace(ParsePdfText(mstrRutaPdf), " ", "")

                Dim c = Split(PDFText, _SAPBox & "-")
                For Each e As String In c
                    Dim r As String = Left(e, 10)
                    Dim dr As Double
                    If Double.TryParse(r, dr) Then
                        PDFPO = dr.ToString
                        Exit For
                    End If
                Next

                'Dim P As Integer = InStr(contenido, SAP, CompareMethod.Text
                If Not System.IO.File.Exists(_DirectoryPath & "\" & _SAPBox & "-" & PDFPO & ".pdf") Then
                    My.Computer.FileSystem.RenameFile(F, _SAPBox & "-" & PDFPO & ".pdf")
                End If

                'newname = _DirectoryPath & "\" & _SAPBox & "-" & Left(c(1), 10) & ".pdf"
            Next


            For Each PO In _Documents
                If System.IO.File.Exists(_DirectoryPath & "\" & _SAPBox & "-" & PO.DocNumber & ".pdf") Then
                    If PO.Printed Then
                        PO.PDFFile = _DirectoryPath & "\" & _SAPBox & "-" & PO.DocNumber & ".pdf"
                    End If
                Else
                    PO.IncludeErrorMessage("Error: File not found..")
                    PO.PDFFile = "File not found."
                    PO.Printed = False
                End If
            Next
        End If

        If TimeOut Then
            MsgBox("Printing process have taken more time than expected." & Chr(13) & Chr(13) & "Some document could not be printed, send printed files before start printing again.", MsgBoxStyle.Information, "Purchases order not printed")
        End If


        CheckErrors()
    End Sub

    Public Sub IncludeDocument(ByVal pDocument As PurchaseOrder)
        _Documents.Add(pDocument)
    End Sub
    Public Sub IncludeDocument(ByVal pDocNumber As String, Optional ByVal pPrintingType As Printingtype = SAPConection.Printingtype.NEU, Optional ByVal pLanguage As PrintingLanguage = PrintingLanguage.English)
        _Documents.Add(New PurchaseOrder(pDocNumber, pPrintingType, pLanguage))
    End Sub
    ''' <summary>
    ''' Gets the spool id for PDF
    ''' </summary>
    ''' <param name="PO">Purchase Document</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Sub GetSpoolID(ByVal PO As PurchaseOrder)
        Dim UA
        Dim TT
        Dim I As Integer
        Dim Sended As Boolean

        Try
            Sended = True

            Change_PO(PO.DocNumber)
            'Display_PO(PO.DocNumber)

            _SAPGUI.findById("wnd[0]/tbar[1]/btn[21]").press()
            UA = _SAPGUI.findById("wnd[0]/usr")
            TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            I = 0

            Do While True
                If ((TT.Rows.ElementAt(I).Item(1).text = PO.GetPrintingType) And (I <= TT.Rows.COUNT - 1) And (TT.Rows.ElementAt(I).Item(3).text = "Print output") And (TT.Rows.ElementAt(I).Item(6).text = PO.GetPrintingLanguage)) Then
                    Exit Do
                Else
                    I = I + 1
                End If
            Loop

            UA.FindByNameEx("SAPDV70ATC_NAST3", 80).Rows.ElementAt(I).Selected = True
            _SAPGUI.findById("wnd[0]/tbar[1]/btn[26]").press()



            PO.SpoolID = Val(Right(_SAPGUI.findById("wnd[1]/usr/lbl[6,6]").text, 9))

            _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").press()
            _SAPGUI.findById("wnd[0]/tbar[0]/btn[3]").press()
            _SAPGUI.findById("wnd[0]/tbar[0]/btn[3]").press()

        Catch ex As Exception
            PO.IncludeErrorMessage("Error getting spool id.")
        End Try

    End Sub
    ''' <summary>
    ''' Imprime una Orden de compra
    ''' </summary>
    ''' <param name="PO">Número de PO a ser impresa</param>
    ''' <param name="FormatoImpresion">Tipo del formato que se impimirá: "NNXX" ó "NEU"</param>
    ''' <param name="Idioma">Idioma para la impresión</param>
    ''' <param name="PrintAle">Establese si se imprime las ordenes con impresion ALE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Print_PO(ByVal PO As PurchaseOrder) As Boolean
        Dim UA
        Dim TT
        Dim R As String
        'Dim Sended As Boolean
        Dim I As Integer
        Dim S As String

        R = ""
        If Not Change_PO(PO.DocNumber) Then
            PO.IncludeErrorMessage("Error: " & _SAPGUI.findById("wnd[0]/sbar").text)
            PO.Printed = False
            'Return 
        End If

        'Change_PO(PO)
        On Error GoTo ErrHandle
        'Sended = True
        _SAPGUI.findById("wnd[0]/tbar[1]/btn[21]").press()

        UA = _SAPGUI.findById("wnd[0]/usr")
        TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
        I = 0

        If TT.Rows.ElementAt(I).Item(0).Tooltip = "Not processed" Then
            _SAPGUI.findById("wnd[0]/usr/tblSAPDV70ATC_NAST3").getAbsoluteRow(I).Selected = True
            _SAPGUI.findById("wnd[0]/tbar[1]/btn[18]").press()

            'Cerrar una ventana de información
            If Not _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
                _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
                _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").press()
            End If

            _SAPGUI.findById("wnd[0]/tbar[0]/btn[83]").press()
            UA = _SAPGUI.findById("wnd[0]/usr")
            TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            I = 0
        End If


        'Dim PrintedAsAle As Boolean = False

        'Do While ((TT.Rows.ElementAt(I).Item(1).text <> "") And TT.Rows.ElementAt(I).Item(0).Tooltip <> "Not processed")
        Do While (TT.Rows.ElementAt(I).Item(0).Tooltip <> "")

            'Verifico si tiene impresion ALE
            If TT.Rows.ElementAt(I).Item(3).text.ToString.IndexOf("ALE") <> -1 Or TT.Rows.ElementAt(I).Item(3).text.ToString.IndexOf("EDI") <> -1 Then
                PO.ALEPrinted = True
                'Exit Do
                'PrintedAsAle = True
            End If

            I = I + 1
            If I > 16 Then
                _SAPGUI.findById("wnd[0]/tbar[0]/btn[83]").press()
                I = 0
                UA = _SAPGUI.findById("wnd[0]/usr")
                TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            End If
        Loop

        If Not _AllowPrintAle And PO.ALEPrinted Then
            PO.Printed = False
            PO.IncludeErrorMessage("Purchase order electronically printed")
            Exit Function
        End If

        TT.Rows.ElementAt(I).Item(1).text = PO.GetPrintingType

        TT.Rows.ElementAt(I).Item(3).key = "1"
        TT.Rows.ElementAt(I).Item(4).text = "VN"

        '*******************************************
        '   Para la parte del idioma de la impresion
        TT.Rows.ElementAt(I).Item(6).Text = PO.GetPrintingLanguage
        '*******************************************

        _SAPGUI.findById("wnd[0]").sendVKey(3)

        If Trim(_SAPGUI.findById("wnd[0]/sbar").Text) <> "" Then
            R = Trim(_SAPGUI.findById("wnd[0]/sbar").Text)
        End If

        UA.FindByNameEx("NAST-VSZTP", 34).key = "4"
        _SAPGUI.findById("wnd[0]").sendVKey(3)
        _SAPGUI.findById("wnd[0]").sendVKey(3)

        'If (_SAPBox = "G4P") Or (_SAPBox = "GBP") Or _SAPBox = "L7A" Then
        '    UA.FindByNameEx("NAST-LDEST", 32).text = "ZPDF"
        'Else
        UA.FindByNameEx("NAST-LDEST", 32).text = "LOCL"
        'End If

        UA.FindByNameEx("NAST-DIMME", 42).Selected = True
        UA.FindByNameEx("NAST-DELET", 42).Selected = True
        _SAPGUI.findById("wnd[0]").sendVKey(3)
        _SAPGUI.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").SetFocus()
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        R = _SAPGUI.findById("wnd[0]/sbar").text

        If R.IndexOf("changed") <> -1 Then
            PO.Printed = True
            R = Nothing
        Else
            PO.IncludeErrorMessage("Error: Purchase order could not be printed.")
            PO.Printed = False
        End If

        _SAPGUI.findById("wnd[0]/tbar[0]/btn[3]").press()

TheExit:
        If Not R Is Nothing Then
            PO.IncludeErrorMessage(R)
        End If

        PO.Printed = True
        Exit Function

ErrHandle:
        PO.IncludeErrorMessage(Err.Description)
        PO.Printed = False
        Exit Function
    End Function
    Private Function Print_OA(ByVal PO As PurchaseOrder) As Boolean
        Dim UA
        Dim TT
        Dim R As String
        'Dim Sended As Boolean
        Dim I As Integer
        Dim S As String

        R = ""
        If Not Change_OA(PO.DocNumber) Then
            PO.IncludeErrorMessage("Error: " & _SAPGUI.findById("wnd[0]/sbar").text)
            PO.Printed = False
            'Return 
        End If

        'Change_PO(PO)
        On Error GoTo ErrHandle
        'Sended = True
        '_SAPGUI.findById("wnd[0]/tbar[1]/btn[21]").press()
        _SAPGUI.findById("wnd[0]/tbar[1]/btn[13]").press()

        UA = _SAPGUI.findById("wnd[0]/usr")
        TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
        I = 0

        If TT.Rows.ElementAt(I).Item(0).Tooltip = "Not processed" Then
            _SAPGUI.findById("wnd[0]/usr/tblSAPDV70ATC_NAST3").getAbsoluteRow(I).Selected = True
            _SAPGUI.findById("wnd[0]/tbar[1]/btn[18]").press()

            'Cerrar una ventana de información
            If Not _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
                _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
                _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").press()
            End If

            _SAPGUI.findById("wnd[0]/tbar[0]/btn[83]").press()
            UA = _SAPGUI.findById("wnd[0]/usr")
            TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            I = 0
        End If


        'Dim PrintedAsAle As Boolean = False

        'Do While ((TT.Rows.ElementAt(I).Item(1).text <> "") And TT.Rows.ElementAt(I).Item(0).Tooltip <> "Not processed")
        Do While (TT.Rows.ElementAt(I).Item(0).Tooltip <> "")

            'Verifico si tiene impresion ALE
            If TT.Rows.ElementAt(I).Item(3).text.ToString.IndexOf("ALE") <> -1 Or TT.Rows.ElementAt(I).Item(3).text.ToString.IndexOf("EDI") <> -1 Then
                PO.ALEPrinted = True
                'Exit Do
                'PrintedAsAle = True
            End If

            I = I + 1
            If I > 16 Then
                _SAPGUI.findById("wnd[0]/tbar[0]/btn[83]").press()
                I = 0
                UA = _SAPGUI.findById("wnd[0]/usr")
                TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            End If
        Loop

        'If Not _AllowPrintAle And PO.ALEPrinted Then
        '    PO.Printed = False
        '    PO.IncludeErrorMessage("Purchase order electronically printed")
        '    Exit Function
        'End If

        'If Not PrintAle And PrintedAsAle Then
        '    PO.Printed = False
        '    Return "Error: Impresión ALE"
        'End If

        TT.Rows.ElementAt(I).Item(1).text = PO.GetPrintingType
        TT.Rows.ElementAt(I).Item(3).key = "6"
        TT.Rows.ElementAt(I).Item(4).text = "LS"
        TT.Rows.ElementAt(I).Item(5).text = "G7P400"


        '*******************************************
        '   Para la parte del idioma de la impresion
        TT.Rows.ElementAt(I).Item(6).Text = PO.GetPrintingLanguage
        '*******************************************

        _SAPGUI.findById("wnd[0]").sendVKey(3)

        If Trim(_SAPGUI.findById("wnd[0]/sbar").Text) <> "" Then
            '_SAPGUI.findById("wnd[0]").sendVKey(3)
            _SAPGUI.findById("wnd[0]").sendVKey(0)
        End If

        UA.FindByNameEx("NAST-VSZTP", 34).key = "4"
        _SAPGUI.findById("wnd[0]").sendVKey(3)
        _SAPGUI.findById("wnd[0]").sendVKey(3)


        'UA.FindByNameEx("NAST-LDEST", 32).text = "LOCL"
        'UA.FindByNameEx("NAST-DIMME", 42).Selected = True
        'UA.FindByNameEx("NAST-DELET", 42).Selected = True
        '_SAPGUI.findById("wnd[0]").sendVKey(3)
        _SAPGUI.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").SetFocus()
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        R = _SAPGUI.findById("wnd[0]/sbar").text

        If R.IndexOf("changed") <> -1 Then
            PO.Printed = True
            PO.PDFFile = R
            R = Nothing

        Else
            'If Intentos = 2 Then
            PO.IncludeErrorMessage("Error: Purchase order could not be printed.")
            PO.Printed = False
            'End If
        End If

        'Next

        _SAPGUI.findById("wnd[0]/tbar[0]/btn[3]").press()

TheExit:
        If Not R Is Nothing Then
            PO.IncludeErrorMessage(R)
        End If

        PO.Printed = True
        Exit Function

ErrHandle:
        PO.IncludeErrorMessage(Err.Description)
        PO.Printed = False
        Exit Function
    End Function
    ''' <summary>
    ''' Prepara la impresion de la PO
    ''' </summary>
    ''' <param name="NUM">Número de PO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Change_PO(ByVal NUM As String) As Boolean
        '_SAPGUI.findById("wnd[0]").Iconify()
        _SAPGUI.findById("wnd[0]").Maximize()
        _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/nme22n"
        _SAPGUI.findById("wnd[0]").sendVKey(0)
        _SAPGUI.findById("wnd[0]/tbar[1]/btn[17]").press()
        _SAPGUI.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = NUM
        _SAPGUI.findById("wnd[1]").sendVKey(0)

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
        End If

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        If Trim(_SAPGUI.findById("wnd[0]/sbar").Text) <> "" Then
            If ((Trim(_SAPGUI.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("RELEASES ALREADY") <> -1) And (Trim(_SAPGUI.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("SAPSCRIPT") <> -1)) Then
                Return False
            End If
        End If

        'Verfico que la pantalla no se encuentre bloqueada
        If Not _SAPGUI.findbyid("wnd[0]/usr").findbynameex("LOCK", 40).changeable Then
            _SAPGUI.findbyid("wnd[0]/tbar[1]").findbynameex("btn[7]", 40).press()
        End If

        Return True
        'Session.findById("wnd[1]").sendVKey(0)
    End Function
    Private Function Change_OA(ByVal NUM As String) As Boolean
        '_SAPGUI.findById("wnd[0]").Iconify()
        _SAPGUI.findById("wnd[0]").Maximize()
        _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/nme32K"
        _SAPGUI.findById("wnd[0]").sendVKey(0)

        _SAPGUI.findById("wnd[0]/usr/ctxtRM06E-EVRTN").text = NUM
        '_SAPGUI.findById("wnd[0]/tbar[1]/btn[17]").press()
        '_SAPGUI.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = NUM

        _SAPGUI.findById("wnd[0]").sendVKey(0)

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
        End If

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        If Trim(_SAPGUI.findById("wnd[0]/sbar").Text) <> "" Then
            If ((Trim(_SAPGUI.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("RELEASES ALREADY") <> -1) And (Trim(_SAPGUI.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("SAPSCRIPT") <> -1)) Then
                Return False
            End If
        End If

        'Verfico que la pantalla no se encuentre bloqueada
        'If Not _SAPGUI.findbyid("wnd[0]/usr").findbynameex("LOCK", 40).changeable Then
        ' _SAPGUI.findbyid("wnd[0]/tbar[1]").findbynameex("btn[7]", 40).press()
        ' End If

        Return True
        'Session.findById("wnd[1]").sendVKey(0)
    End Function
    Private Sub ChangeFileName(ByVal PO As PurchaseOrder, Optional ByVal UsesFreePDF4 As Boolean = False)
        Dim MyPath As String
        Dim OldName As String
        Dim NewName As String = Nothing

        Try
            If Not UsesFreePDF4 Then
                Dim PDFList As String() = System.IO.Directory.GetFiles(_DirectoryPath, "*-1.pdf")
                For Each f As String In PDFList
                    If Right(f, PO.SpoolID.Length + 6).ToUpper = (PO.SpoolID + "-1.pdf").ToUpper Then 'Busco el que tenga el spool ID igual
                        My.Computer.FileSystem.RenameFile(f, SAPBox & "-" & PO.DocNumber & ".pdf")
                        NewName = _DirectoryPath & "\" & SAPBox & "-" & PO.DocNumber & ".pdf"
                    End If
                Next

                PO.PDFFile = NewName
            Else
                Dim PDFList As String() = System.IO.Directory.GetFiles(_DirectoryPath, "DBPrinted*.pdf")

                If PDFList.Length >= 0 Then
                    If PDFList.Length = 1 Then
                        For Each f As String In PDFList

                            My.Computer.FileSystem.RenameFile(f, SAPBox & "-" & PO.DocNumber & ".pdf")
                            NewName = _DirectoryPath & "\" & SAPBox & "-" & PO.DocNumber & ".pdf"
                        Next
                    Else

                    End If
                Else
                    PO.PDFFile = "Multiple PDF printed files found."
                End If



                PO.PDFFile = NewName
            End If

        Catch ex As Exception
            'Match_PO = "Error al modificar el nombre del PDF. Spool ID:" & Spool
        End Try

    End Sub
    Function Display_PO(ByVal pDocNumber As String) As Boolean
        _SAPGUI.findById("wnd[0]").Iconify()
        _SAPGUI.findById("wnd[0]").Maximize()
        _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/nme23n"
        _SAPGUI.findById("wnd[0]").sendVKey(0)
        _SAPGUI.findById("wnd[0]/tbar[1]/btn[17]").press()
        _SAPGUI.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = pDocNumber
        _SAPGUI.findById("wnd[1]").sendVKey(0)
    End Function
    Public Function GetLanguage(ByVal pLanguage As String) As PrintingLanguage
        Select Case pLanguage.ToUpper
            Case "EN"
                Return PrintingLanguage.English

            Case "PT"
                Return PrintingLanguage.Portuguese

            Case "ES"
                Return PrintingLanguage.Spanish

            Case Else
                _ErrorMessage.Add("Language: " & pLanguage & " not found.")
                Return PrintingLanguage.English
        End Select

    End Function
    Public Function GetFilePath(ByVal pDocNumber As String) As String
        Dim Path As String = ""

        For Each Document As PurchaseOrder In _Documents
            If Document.DocNumber = pDocNumber Then
                If Document.Printed Then
                    Return Document.PDFFile
                Else
                    Return Document.ErrorList(0)
                End If
            End If
        Next

        Return "Document not found."

    End Function
    Public Sub SetPDFTimeout(ByVal pDocNumber As String)
        For Each Document As PurchaseOrder In _Documents
            If Document.DocNumber = pDocNumber Then
                Document.IncludeErrorMessage("Error: PDF Time out")
                Document.Printed = False
                Exit For
            End If
        Next
    End Sub
    Public Function DocumentPrinted(ByVal pDocNumber As String) As Boolean
        For Each Document As PurchaseOrder In _Documents
            If Document.DocNumber = pDocNumber Then
                If Document.Printed Then
                    Return Document.Printed
                End If
            End If
        Next
    End Function
    Public Function ParsePdfText(ByVal sourcePDF As String, _
   Optional ByVal fromPageNum As Integer = 0, _
   Optional ByVal toPageNum As Integer = 0) As String


        Dim sb As New System.Text.StringBuilder()

        Try

            Dim reader As New iTextSharp.text.pdf.PdfReader(sourcePDF)

            Dim pageBytes() As Byte = Nothing

            Dim token As PRTokeniser = Nothing

            Dim tknType As Integer = -1

            Dim tknValue As String = String.Empty


            If fromPageNum = 0 Then

                fromPageNum = 1

            End If

            If toPageNum = 0 Then

                toPageNum = reader.NumberOfPages

            End If


            If fromPageNum > toPageNum Then

                Throw New ApplicationException("Parameter error: The value of fromPageNum can " & _
                "not be larger than the value of toPageNum")

            End If


            For i As Integer = fromPageNum To toPageNum Step 1

                pageBytes = reader.GetPageContent(i)

                If Not IsNothing(pageBytes) Then

                    token = New PRTokeniser(pageBytes)

                    While token.NextToken()

                        tknType = token.TokenType()

                        tknValue = token.StringValue

                        If tknType = PRTokeniser.TokType.STRING Then

                            sb.Append(token.StringValue)

                            'I need to add these additional tests to properly add whitespace to the output string

                        ElseIf tknType = 1 AndAlso tknValue = "-600" Then

                            sb.Append(" ")

                        ElseIf tknType = 10 AndAlso tknValue = "TJ" Then

                            sb.Append(" ")

                        End If

                    End While

                End If

            Next i

        Catch ex As Exception

            'MsgBox.Show("Exception occured. " & ex.Message)

            Return String.Empty

        End Try

        Return sb.ToString()

    End Function
#End Region
End Class

Public Class PurchaseOrder
#Region "Variables"
    Private _DocNumber As String = Nothing
    Private _PrintType As Printingtype = PrintingType.NEU
    Private _Language As PrintingLanguage = PrintingLanguage.English
    Private _SpoolId As String = Nothing
    Private _PDFFile As String = Nothing
    Private _Printed As Boolean = False
    Private _ErrorMessage As New List(Of String)
    Private _AlePrinted As Boolean = False
    
#End Region
#Region "Properties"

    ''' <summary>
    ''' Document Number
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocNumber() As String
        Get
            Return _DocNumber
        End Get
        Set(ByVal value As String)
            _DocNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Printing Type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PrintingType() As Printingtype
        Get
            Return _PrintType
        End Get
        Set(ByVal value As Printingtype)
            _PrintType = value
        End Set
    End Property

    ''' <summary>
    ''' Language for printing
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Language() As PrintingLanguage
        Get
            Return _Language
        End Get
        Set(ByVal value As PrintingLanguage)
            _Language = value
        End Set
    End Property

    ''' <summary>
    ''' SAP Spool ID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SpoolID() As String
        Get
            Return _SpoolId
        End Get
        Set(ByVal value As String)
            _SpoolId = value
        End Set
    End Property

    ''' <summary>
    ''' PDF File Printed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PDFFile() As String
        Get
            Return _PDFFile
        End Get

        Set(ByVal value As String)
            _PDFFile = value
        End Set
    End Property

    ''' <summary>
    ''' Purchase document was sucessfully printed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Printed() As Boolean
        Get
            Return _Printed
        End Get

        Set(ByVal value As Boolean)
            _Printed = value
        End Set
    End Property

    Public Property ALEPrinted() As Boolean
        Get
            Return _AlePrinted
        End Get
        Set(ByVal value As Boolean)
            _AlePrinted = value
        End Set
    End Property

    Public ReadOnly Property GetPrintingType() As String
        Get
            Select Case _PrintType
                Case SAPConection.Printingtype.ALE
                    Return "ALE"

                Case SAPConection.Printingtype.NEU
                    Return "NEU"

                Case SAPConection.Printingtype.ZSU2
                    Return "ZSU2"

                Case SAPConection.Printingtype.ZSU4
                    Return "ZSU4"

                Case SAPConection.Printingtype.NEXX
                    Return "NEXX"

                Case SAPConection.Printingtype.NNXX
                    Return "NNXX"

                Case SAPConection.Printingtype.ZGBL
                    Return "ZGBL"

                Case Else
                    Return ""
            End Select
        End Get
    End Property

    Public ReadOnly Property GetPrintingLanguage() As String
        Get
            Select Case _Language
                Case PrintingLanguage.English
                    Return "EN"

                Case PrintingLanguage.Portuguese
                    Return "PT"

                Case PrintingLanguage.Spanish
                    Return "ES"

                Case Else
                    Return ""

            End Select
        End Get
    End Property
    Public ReadOnly Property ErrorList() As List(Of String)
        Get
            Return _ErrorMessage
        End Get
    End Property

#End Region
#Region "Methods"
    Public Sub New()

    End Sub

    Public Sub New(ByVal pDocNumber As String, Optional ByVal pPrintingType As Printingtype = SAPConection.Printingtype.NEU, Optional ByVal pLanguage As PrintingLanguage = PrintingLanguage.English)
        If pDocNumber.Trim.Length > 0 Then
            _DocNumber = pDocNumber
            _PrintType = pPrintingType
            _Language = pLanguage
        Else
            MsgBox("No document number found.", MsgBoxStyle.Exclamation, "Class PO::SUB New")
        End If

    End Sub

    Public Sub IncludeErrorMessage(ByVal pErrorMessage As String)
        _ErrorMessage.Add(pErrorMessage)
    End Sub
#End Region
End Class
