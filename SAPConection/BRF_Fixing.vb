Imports System
Imports System.IO

Public Class BRF_Fixing
#Region "Variables"
    Private _Documents As New List(Of BRF_PO)
    Private _SAPGUI As Object
#End Region

#Region "Properties"
    Public ReadOnly Property Documents() As List(Of BRF_PO)
        Get
            Return _Documents
        End Get
    End Property
#End Region

#Region "Methods"
    Public Sub New(ByVal pSAPGUI As Object)
        _SAPGUI = pSAPGUI
    End Sub
    Public Sub Execute()
        For Each D As BRF_PO In _Documents
            If Change_PO(D.DocNumber) Then
                FixTax(D.DocNumber, D.ItemNumber)
            End If
        Next
    End Sub

    Public Sub IncludePO(ByVal Item As BRF_PO)
        _Documents.Add(Item)
    End Sub
    Private Function Change_PO(ByVal NUM As String) As Boolean
        '************************************************************************************************************************************
        '  Una vez estable el proceso se pueden eliminar las líneas writeline, son para tener un trackeo de las instrucciones que se realizan.
        '************************************************************************************************************************************

        Dim File As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & NUM & ".txt"

        WriteLine(File, "**********     Open change PO transaction   ************")
        WriteLine(File, "")

        _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/nme22n"
        WriteLine(File, "_SAPGUI.findById(wnd[0]/tbar[0]/okcd).text = /nme22n")
        _SAPGUI.findById("wnd[0]").sendVKey(0)
        WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(0)")
        _SAPGUI.findById("wnd[0]/tbar[1]/btn[17]").press()
        WriteLine(File, "_SAPGUI.findById(wnd[0]/tbar[1]/btn[17]).press()")
        _SAPGUI.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = NUM
        WriteLine(File, "_SAPGUI.findById(wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN).text = NUM")
        _SAPGUI.findById("wnd[1]").sendVKey(0)
        WriteLine(File, " _SAPGUI.findById(wnd[1]).sendVKey(0)")

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
            WriteLine(File, "_SAPGUI.findbyId(wnd[1]/usr/btnSPOP-OPTION1).press()")
        End If

        If Not _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            _SAPGUI.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
            WriteLine(File, "_SAPGUI.findbyId(wnd[1]/usr/btnSPOP-VAROPTION1).press()")
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

        _SAPGUI.findById("wnd[0]").sendVKey(27)
        WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(27)")
        _SAPGUI.findById("wnd[0]").sendVKey(28)
        WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(28)")
        Return True
    End Function
    Private Function FixTax(ByVal Num As String, ByVal Item As String) As Boolean
        Dim File As String = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\" & Num & ".txt"
        Try
            Dim Fixed As Boolean = False
            WriteLine(File, "")
            WriteLine(File, "******************** PO: " & Num & " - " & Item & "  ********************")
            WriteLine(File, "")


            If _SAPGUI.findbyid("wnd[0]/usr").findbynameex("EDITFILTER", 40) Is Nothing Then
                MsgBox("Filter botton not found.")
                WriteLine(File, "Filter botton not found")
            End If

            '_SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/btnEDITFILTER").press()
            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("EDITFILTER", 40).press()
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(EDITFILTER, 40).press()")
            '_SAPGUI.findById("wnd[1]/usr/subSUB_DYN0500:SAPLSKBH:0600/btnAPP_WL_SING").press()
            _SAPGUI.findbyid("wnd[1]/usr").findbynameex("APP_WL_SING", 40).press()
            WriteLine(File, "_SAPGUI.findById(wnd[1]/usr/subSUB_DYN0500:SAPLSKBH:0600/btnAPP_WL_SING).press()")
            '_SAPGUI.findById("wnd[1]/usr/subSUB_DYN0500:SAPLSKBH:0600/btn600_BUTTON").press()
            _SAPGUI.findbyid("wnd[1]/usr").findbynameex("600_BUTTON", 40).press()
            WriteLine(File, "_SAPGUI.findbyid(wnd[1]/usr).findbynameex(600_BUTTON, 40).press()")
            _SAPGUI.findById("wnd[2]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW").text = Item
            WriteLine(File, "_SAPGUI.findById(wnd[2]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW).text = " & Item)
            '_SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB1:SAPLMEGUI:6000/cmbDYN_6000-LIST").key = "   1"
            '_SAPGUI.findById("wnd[2]/usr/ssub%_SUBSCREEN_FREESEL:SAPLSSEL:1105/ctxt%%DYN001-LOW").caretPosition = 2
            _SAPGUI.findById("wnd[2]").sendVKey(0)
            WriteLine(File, "_SAPGUI.findById(wnd[2]).sendVKey(0)")

            If Not _SAPGUI.findbyid("wnd[0]/usr").findbynameex("DYN_6000-LIST", 34) Is Nothing Then
                _SAPGUI.findbyid("wnd[0]/usr").findbynameex("DYN_6000-LIST", 34).setfocus()
            Else
                WriteLine(File, "**-> Item picklist not found.")
            End If

            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("DYN_6000-LIST", 34).key = "   1"
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(DYN_6000-LIST, 34).key =    1")

            '_SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB1:SAPLMEGUI:6000/cmbDYN_6000-LIST").key = "   1"
            'WriteLine(File, "_SAPGUI.findById(wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB1:SAPLMEGUI:6000/cmbDYN_6000-LIST).key = 1")

            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("TABIDT7", 91).setfocus()
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(TABIDT7, 91).setfocus()")

            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("TABIDT7", 91).select()
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(TABIDT7, 91).select()")

            '_SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT7").select()
            'WriteLine(File, "_SAPGUI.findById(wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT7).select()")

            _SAPGUI.findById("wnd[0]").sendVKey(0)
            WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(0)")
            WriteLine(File, "*** Changing tax and running BRF+ ***")

            'Cambiar el text item porque a veces no se llama correctamente el BRF+ hay direfentes is porque el HP!!!! control se llama diferente dependiendo la caja
            If Not _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]", False) Is Nothing Then
                'L7P - G4P & L6P:
                Dim TxtItem As String = _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").text

                'Modifico el texto para que acepte el cambio y pueda llamar al BRF+
                _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = "." & Left(Replace(TxtItem, " ", ""), (TxtItem.Length - 1))
                _SAPGUI.findById("wnd[0]").sendVKey(0)

                _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = TxtItem
                _SAPGUI.findById("wnd[0]").sendVKey(0)

            Else
                'L7A:
                If Not _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]", False) Is Nothing Then
                    Dim TxtItem As String = _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").text

                    'Modifico el texto para que acepte el cambio y pueda llamar al BRF+
                    _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = "." & Left(Replace(TxtItem, " ", ""), (TxtItem.Length - 1))
                    _SAPGUI.findById("wnd[0]").sendVKey(0)

                    _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = TxtItem
                    _SAPGUI.findById("wnd[0]").sendVKey(0)
                Else
                    'GBP:
                    If Not _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]", False) Is Nothing Then
                        Dim TxtItem As String = _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").text

                        'Modifico el texto para que acepte el cambio y pueda llamar al BRF+
                        _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = "." & Left(Replace(TxtItem, " ", ""), (TxtItem.Length - 1))
                        _SAPGUI.findById("wnd[0]").sendVKey(0)

                        _SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-TXZ01[5,0]").Text = TxtItem
                        _SAPGUI.findById("wnd[0]").sendVKey(0)
                    End If
                End If
            End If

            Dim Tax As String = _SAPGUI.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32).text
            WriteLine(File, "Dim Tax As String = _SAPGUI.findbyid(wnd[0]/usr).findbynameex(MEPO1317-MWSKZ, 32).text")
            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32).text = "XX"
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(MEPO1317-MWSKZ, 32).text = XX")
            _SAPGUI.findById("wnd[0]").sendVKey(0)
            WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(0)")


            If _SAPGUI.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32) Is Nothing Then
                _SAPGUI.findbyid("wnd[0]/usr").findbynameex("TABIDT7", 91).select()
                WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(TABIDT7, 91).select()")
            End If

            _SAPGUI.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32).text = Tax
            WriteLine(File, "_SAPGUI.findbyid(wnd[0]/usr).findbynameex(MEPO1317-MWSKZ, 32).text = " & Tax)

            _SAPGUI.findById("wnd[0]").sendVKey(0)
            WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(0)")

            '_SAPGUI.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT7/ssubTABSTRIPCONTROL1SUB:SAPLMEGUI:1317/ctxtMEPO1317-MWSKZ").text = "5B"
            'WriteLine(File, "_SAPGUI.findById(wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT7/ssubTABSTRIPCONTROL1SUB:SAPLMEGUI:1317/ctxtMEPO1317-MWSKZ).text = 5B")

            _SAPGUI.findById("wnd[0]").sendVKey(0)
            WriteLine(File, "_SAPGUI.findById(wnd[0]).sendVKey(0)")
            _SAPGUI.findById("wnd[0]/tbar[0]/btn[11]").press()
            WriteLine(File, "_SAPGUI.findById(wnd[0]/tbar[0]/btn[11]).press()")

            If Not _SAPGUI.findById("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
                _SAPGUI.findById("wnd[1]/usr/btnSPOP-VAROPTION1").press()
                WriteLine(File, "_SAPGUI.findById(wnd[1]/usr/btnSPOP-VAROPTION1).press()")
            End If

            If Not _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
                _SAPGUI.findById("wnd[1]/tbar[0]/btn[0]").press()
                WriteLine(File, "_SAPGUI.findById(wnd[1]/tbar[0]/btn[0]).press()")
            End If

            WriteLine(File, "")
            WriteLine(File, _SAPGUI.findById("wnd[0]/sbar").Text)
            WriteLine(File, "******************** Item: " & Item & " Changed  ********************")
            WriteLine(File, "")
            Return Fixed
        Catch ex As Exception
            MsgBox(ex.Message)
            WriteLine(File, ">>>>> Error found: " & ex.Message)
            WriteLine(File, "")
            Return False
        End Try

        _SAPGUI.findById("wnd[0]/tbar[0]/okcd").text = "/n"
        WriteLine(File, "_SAPGUI.findById(wnd[0]/tbar[0]/okcd).text = /n")
        _SAPGUI.findById("wnd[0]").sendVKey(0)
    End Function
    Private Function CreateFile(ByVal Path As String) As Boolean

        Try
            Dim File As System.IO.FileStream
            If Not System.IO.File.Exists(Path) Then
                File = System.IO.File.Create(Path)
                File.Close()
            End If

            Return True

        Catch oe As Exception
            MsgBox(oe.Message, MsgBoxStyle.Critical)
            Return False
        End Try

    End Function
    Private Sub WriteLine(ByVal Path As String, ByVal Text As String)
        Try

            If Not System.IO.File.Exists(Path) Then
                If Not CreateFile(Path) Then
                    MsgBox("File couldn't be created: " & Path, MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If

            Dim osw As System.IO.StreamWriter

            osw = New System.IO.StreamWriter(Path, True)
            osw.WriteLine(Text)
            osw.Close()

        Catch ex As Exception

        End Try
    End Sub

#End Region


End Class

Public Class BRF_PO
#Region "Variables"
    Private _DocNumber As String = Nothing
    Private _ItemNumber As String = Nothing
    Private _Fixed As Boolean = False
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
    Public Property ItemNumber() As String
        Get
            Return _ItemNumber
        End Get
        Set(ByVal value As String)
            _ItemNumber = value
        End Set
    End Property
    ''' <summary>
    ''' Purchase document was sucessfully fixed
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Fixed() As Boolean
        Get
            Return _Fixed
        End Get
        Set(ByVal value As Boolean)
            _Fixed = value
        End Set
    End Property
#End Region
#Region "Methods"
    Public Sub New()
    End Sub
    Public Sub New(ByVal pDocNumber As String, ByVal ItemNumber As String)
        If pDocNumber.Trim.Length > 0 Then
            _DocNumber = pDocNumber
            _ItemNumber = ItemNumber
            _Fixed = False
        Else
            MsgBox("No document number found.", MsgBoxStyle.Exclamation, "Class PO::SUB New")
        End If
    End Sub
#End Region
End Class