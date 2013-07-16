Imports System.Data.Common
Imports System.Data
Imports System.Configuration

Public Class SAPTools
    Private Application
    Private Connection
    Private Session
    Private lsDirectorio = My.Application.Info.DirectoryPath & "\OADownLoad\"

    'Session.findById("wnd[0]").sendVKey 26 'Expand Header
    'Session.findById("wnd[0]").sendVKey 29 'Collapse Header
    'Session.findById("wnd[0]").sendVKey 27 'Expand Overview
    'Session.findById("wnd[0]").sendVKey 30 'Collapse Overview
    'Session.findById("wnd[0]").sendVKey 28 'Expand Details
    'Session.findById("wnd[0]").sendVKey 31 'Collapse Details
    ''' <summary>
    ''' Esta funcion realiza el push para que a la requi se le asigne el contrato.
    ''' </summary>
    ''' <param name="Requisicion">Número de la requi a la que hay que hacer el Push</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PushRequi(ByVal Requisicion As String) As Boolean
        Session.findById("wnd[0]").maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme57"
        Session.findById("wnd[0]").sendVKey(0)

        '**************************************************
        '  Clear field for selection with SAP Variant
        '**************************************************
        Session.findById("wnd[0]/tbar[1]/btn[17]").press()
        Session.findById("wnd[1]/usr/txtV-LOW").text = "LA_Tool"
        Session.findById("wnd[1]/usr/ctxtENVIR-LOW").text = ""
        Session.findById("wnd[1]/usr/txtENAME-LOW").text = "BM4691"
        Session.findById("wnd[1]/usr/txtAENAME-LOW").text = ""
        Session.findById("wnd[1]/usr/txtMLANGU-LOW").text = ""
        Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus()
        Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 6
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        '**************************************************

        Session.findById("wnd[0]/usr/ctxtBA_BANFN-LOW").text = Requisicion
        Session.findById("wnd[0]/usr/ctxtBA_BANFN-LOW").caretPosition = 8
        Session.findById("wnd[0]/tbar[1]/btn[8]").press()

        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
            PushRequi = False
            Exit Function
        End If


        Session.findById("wnd[0]/tbar[1]/btn[18]").press()
        Session.findById("wnd[0]/tbar[0]/btn[11]").press()

        PushRequi = True
    End Function

    Public Function GetConnectionToSAP(ByVal Box As String, ByVal TNumber As String, ByVal Pwrd As String, ByVal SAPConfig As Boolean) As Object
        Try
            Application = CreateObject("Sapgui.ScriptingCtrl.1")

            If Not SAPConfig Then
                Connection = Application.OpenConnection(Box)
                Session = Connection.Children(0)
                Session.findbyId("wnd[0]").Maximize()

                Session.findbyId("wnd[0]/usr/txtRSYST-BNAME").Text = TNumber
                Session.findbyId("wnd[0]/usr/pwdRSYST-BCODE").Text = Pwrd

                Session.findbyId("wnd[0]").sendVKey(0)

                If Session.findById("wnd[0]/sbar").Text <> "" Then
                    MsgBox("No se puede realizar la conexion con SAP." & vbCr & vbCr & "Por favor verifique el nombre de usuario y la contraseña y vuelva a intentarlo.", MsgBoxStyle.Critical)
                    Me.CloseSession(Session)
                    Return False
                    Exit Function
                End If
            Else
                Connection = Application.OpenConnection(Box & " - SSO")
                Session = Connection.Children(0)
            End If


            If Not Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").SetFocus()
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").Selected = True
                Session.findbyId("wnd[1]/tbar[0]/btn[0]").press()
            End If

            Return Session

        Catch ex As Exception
            MsgBox("Can't get SAP Connection.", MsgBoxStyle.Critical)
            Return False

        Finally

        End Try

    End Function

    Public Function DownloadHeaderContrato(ByRef MySession) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(135, 29, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekko"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,10]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,16]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,16]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "EkORG"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").setFocus()
        MySession.findById("wnd[1]/usr/chk[1,8]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,10]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = "4600000000"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = "4699999999"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 10
        MySession.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        'Modificaron el nombre de este objeto el 15 de mayo del 2010
        'MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        'MySession.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Ekko_Contratos.txt"

        If Len(Dir(lsDirectorio & "EKKO_Contratos.txt")) > 0 Then
            Kill(lsDirectorio & "EKKO_Contratos.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 18
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        Return True

    End Function

    Public Function DownloadDetalleContratos(ByVal MySession As Object, ByVal Plantas As DataSet) As Boolean
        Dim i As Integer

        MySession.findById("wnd[0]").resizeWorkingPane(135, 29, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekpo"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,14]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,7]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,7]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,13]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,17]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "netpr"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]").sendVKey(0)
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "plifz"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]").sendVKey(0)
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 3
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,3]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = "4600000000"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = "4699999999"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 10
        MySession.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtI3-LOW").text = "30000000"
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").text = "39999999"
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").caretPosition = 8
        MySession.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()


        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = Plantas.Tables(0).Rows(0).Item("Code")

        For i = 1 To Plantas.Tables(0).Rows.Count - 1
            MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = Plantas.Tables(0).Rows(i).Item("Code")
            MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = i
        Next

        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "0051"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").setFocus()
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 1
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "0300"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 2
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "0301"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 3
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "4004"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "4841"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 5
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "4950"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 6
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9476"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 7
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9475"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 8
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "0278"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 9
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "4563"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 10
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "2930"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 11
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "2921"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 12
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9245"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 13
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9266"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 14
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9265"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 15
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9367"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 16
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "8727"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 17
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "7761"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = 18
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "9653"
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 4

        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)


        '*************************************************************************************
        'MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Ekpo_Contratos.txt"

        If Len(Dir(lsDirectorio & "EKPO_Contratos.txt")) > 0 Then
            Kill(lsDirectorio & "EKPO_Contratos.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 17
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function

    Public Function DownloadHeaderCompras(ByVal MySession As Object, ByVal FechaInicio$, ByVal FechaFinal$) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(74, 17, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekko"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,12]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,16]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,16]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "ekorg"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()

        'Comentado para el reporte de David Ferreira
        '****************************************************************
        MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = "4500000000"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = "4599999999"
        '****************************************************************

        MySession.findById("wnd[0]/usr/txtI2-LOW").setFocus()
        MySession.findById("wnd[0]/usr/txtI2-LOW").caretPosition = 0
        MySession.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtI3-LOW").text = FechaInicio
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").text = FechaFinal
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"

        'MySession.findById("wnd[0]/usr/ctxtLIST_BRE").setFocus()
        'MySession.findById("wnd[0]/usr/ctxtLIST_BRE").caretPosition = 4

        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Ekko_Compras.txt"

        If Len(Dir(lsDirectorio & "EKKO_Compras.txt")) > 0 Then
            Kill(lsDirectorio & "EKKO_Compras.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 12
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function

    Public Function DownloadDetalleCompras(ByVal MySession As Object, ByVal Planta$, ByVal MinPO$, ByVal MaxPO$) As Boolean
        MySession.findById("wnd[0]").maximize()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekpo"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,7]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,14]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,14]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        '****************************************************************
        'Comentado para el reporte de David Fereira
        MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = "4500000000"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = "4599999999"

        'MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = "3000000000"
        'MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = "4599999999"
        ''****************************************************************

        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 10
        MySession.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        '****************************************************************
        'Comentado para el reporte de David Fereira
        MySession.findById("wnd[0]/usr/ctxtI3-LOW").text = "30000000"
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").text = "39999999"
        '****************************************************************

        MySession.findById("wnd[0]/usr/ctxtI4-LOW").text = Planta
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,13]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,20]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "netpr"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()


        '***************************************************
        ' Para evitar un error al momento de bajar la info
        ' Feb.2 2010
        If Session.findById("wnd[0]/sbar").Text <> "" Then
            Return False
        End If
        '***************************************************

        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************


        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "EKPO_Compras_" & Planta & ".txt"

        If Len(Dir(lsDirectorio & "EKPO_Compras_" & Planta & ".txt")) > 0 Then
            Kill(lsDirectorio & "EKPO_Compras_" & Planta & ".txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 15
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        Return True

    End Function

    Public Function DownloadVendors(ByRef MySession As Object) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(121, 24, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "lfa1"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,6]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,6]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/usr/lbl[25,3]").setFocus()
        'MySession.findById("wnd[0]/usr/lbl[25,3]").caretPosition = 0
        'MySession.findById("wnd[0]").sendVKey(2)
        'MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Vendors.txt"


        If Len(Dir(lsDirectorio & "Vendors.txt")) > 0 Then
            Kill(lsDirectorio & "Vendors.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 11
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function
    ''' <summary>
    ''' Baja los vendors para el reporte de Catalogos
    ''' </summary>
    ''' <param name="MySession">Conexion con SAP</param>
    ''' <param name="Variante">Variante</param>
    ''' <param name="Caja">Nombre de la caja, esto porque GBP el nombre del control de impresion cambió</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadVendorsFix(ByRef MySession As Object, ByVal Variante As DataTable, ByVal Caja$) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(121, 24, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "lfa1"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]").sendVKey(8)

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,6]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,6]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        MySession.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()


        MySession.findById("wnd[1]/tbar[0]/btn[24]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        '******************************************************************
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = Variante.Rows(0).Item("Vendor")

        'Dim i As Integer
        'For i = 1 To Variante.Rows.Count - 1
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = Variante.Rows(i).Item("Vendor")
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = i
        'Next

        'MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/usr/lbl[25,3]").setFocus()
        'MySession.findById("wnd[0]/usr/lbl[25,3]").caretPosition = 0
        'MySession.findById("wnd[0]").sendVKey(2)
        'MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'Select Case Caja
        '    Case "GBP", "G4P"
        '        Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()
        '    Case Else
        '        MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        'End Select

        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Vendors.txt"


        If Len(Dir(lsDirectorio & "Vendors.txt")) > 0 Then
            Kill(lsDirectorio & "Vendors.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 11
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function

    Public Function DownloadMasterData(ByRef MySession As Object, ByRef Planta$) As Boolean

        MySession.findbyId("wnd[0]").resizeWorkingPane(97, 13, False)
        MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "/nzse16"
        MySession.findbyId("wnd[0]").sendVKey(0)
        MySession.findbyId("wnd[0]/usr/ctxtI_TABLE").Text = "marc"
        MySession.findbyId("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findbyId("wnd[0]").sendVKey(0)
        MySession.findbyId("wnd[0]").sendVKey(8)

        If Not MySession.findbyId("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findbyId("wnd[1]").Close()
            MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findbyId("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").Select()
        MySession.findbyId("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findbyId("wnd[1]/usr/chk[1,4]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,5]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,14]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,20]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,20]").SetFocus()

        MySession.findbyId("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findbyId("wnd[2]/usr/chkSCAN_STRING-START").Selected = False
        MySession.findbyId("wnd[2]/usr/txtRSYSF-STRING").Text = "MRP"
        MySession.findbyId("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
        MySession.findbyId("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[3]/usr/lbl[37,8]").SetFocus()
        MySession.findbyId("wnd[3]/usr/lbl[37,8]").caretPosition = 1
        MySession.findbyId("wnd[3]").sendVKey(2)
        MySession.findbyId("wnd[1]/usr/chk[1,3]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,3]").SetFocus()

        MySession.findbyId("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findbyId("wnd[2]/usr/chkSCAN_STRING-START").Selected = False

        MySession.findbyId("wnd[2]/usr/txtRSYSF-STRING").Text = "PO"
        MySession.findbyId("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 2
        MySession.findbyId("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[3]/usr/lbl[3,5]").SetFocus()
        MySession.findbyId("wnd[3]/usr/lbl[3,5]").caretPosition = 1
        MySession.findbyId("wnd[3]").sendVKey(2)
        MySession.findbyId("wnd[1]/usr/chk[1,3]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,4]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[1,4]").SetFocus()
        MySession.findbyId("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findbyId("wnd[0]/mbar/menu[3]/menu[2]").Select()
        MySession.findbyId("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findbyId("wnd[1]/usr/chk[2,5]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[2,6]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[2,15]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[2,21]").Selected = True
        MySession.findbyId("wnd[1]/usr/chk[2,21]").SetFocus()
        MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[0]/usr/ctxtI1-LOW").Text = "30000000"
        MySession.findbyId("wnd[0]/usr/ctxtI1-HIGH").Text = "39999999"
        MySession.findbyId("wnd[0]/usr/ctxtI2-LOW").Text = Planta
        MySession.findbyId("wnd[0]/usr/ctxtLIST_BRE").Text = "9999"
        MySession.findbyId("wnd[0]/usr/txtMAX_SEL").Text = "999999999"
        MySession.findbyId("wnd[0]/usr/txtMAX_SEL").SetFocus()
        MySession.findbyId("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findbyId("wnd[0]").sendVKey(0)
        MySession.findbyId("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "%pc"
        MySession.findbyId("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").Select()
        'MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[1,0]").SetFocus()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[1]/usr/ctxtDY_PATH").Text = lsDirectorio
        MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").Text = "MARC_" & Planta & ".txt"

        If Len(Dir(lsDirectorio & "MARC_" & Planta & ".txt")) > 0 Then
            Kill(lsDirectorio & "MARC_" & Planta & ".txt")
        End If

        MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 6
        MySession.findbyId("wnd[1]").sendVKey(0)

    End Function

    Public Function DownloadDetalleCatalogo(ByVal MySession As Object, ByVal Planta$, ByVal Variante As DataTable, ByVal Caja$) As Boolean
        MySession.findById("wnd[0]").maximize()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekpo"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,7]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,14]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,14]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        'Seteo de las ordenes de compra descargadas de la EKKO

        MySession.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()


        MySession.findById("wnd[1]/tbar[0]/btn[24]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        'Cambiado por el metodo de pegado del clipboard
        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = Variante.Rows(0).Item("PurchDoc")
        'Dim i As Integer
        'For i = 1 To Variante.Rows.Count - 1
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = Variante.Rows(i).Item("PurchDoc")
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = i
        'Next


        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 10
        MySession.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtI3-LOW").text = ""
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").text = ""
        MySession.findById("wnd[0]/usr/ctxtI4-LOW").text = Planta


        '*************************************************************************************
        'Para que solo baje lo no gicado
        'MySession.findById("wnd[0]").sendVKey(8)
        MySession.findById("wnd[0]/usr/btn%_I3_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        '*************************************************************************************

        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,13]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,20]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,21]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "netpr"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,9]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)


        '*************************************************************************************
        ' '' ''Select Case Caja
        ' '' ''    Case "GBP", "G4P"
        ' '' ''        Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()
        ' '' ''    Case Else
        ' '' ''        MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        ' '' ''End Select
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************



        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "DetalleCatalogos.txt"

        If Len(Dir(lsDirectorio & "DetalleCatalogos.txt")) > 0 Then
            Kill(lsDirectorio & "DetalleCatalogos.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 15
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function

    Public Function DownloadCurrentOTD(ByVal MySession As Object, ByVal Planta$) As Boolean
        MySession.findbyId("wnd[0]").resizeWorkingPane(97, 24, False)
        MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "/nmc$k"
        MySession.findbyId("wnd[0]").sendVKey(0)
        MySession.findbyId("wnd[0]/usr/ctxtSL_MATNR-LOW").Text = "30000000"
        MySession.findbyId("wnd[0]/usr/ctxtSL_MATNR-HIGH").Text = "39999999"
        MySession.findbyId("wnd[0]/usr/ctxtSL_WERKS-LOW").Text = Planta

        MySession.findbyId("wnd[0]/usr/ctxtSL_SPMON-LOW").Text = Month(Today) & "/" & Year(Today) - 1
        MySession.findbyId("wnd[0]/usr/ctxtSL_SPMON-HIGH").Text = Month(Today) & "/" & Year(Today)
        MySession.findbyId("wnd[0]/usr/ctxtSL_SPMON-HIGH").SetFocus()
        MySession.findbyId("wnd[0]/usr/ctxtSL_SPMON-HIGH").caretPosition = 7
        MySession.findbyId("wnd[0]/tbar[1]/btn[8]").press()
        'MySession.findbyId("wnd[0]/mbar/menu[5]/menu[2]/menu[2]").Select()
        MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "%pc"
        MySession.findbyId("wnd[0]").sendVKey(0)

        '*************************************************************************************
        'MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").Select()
        'MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").SetFocus()

        ' Estas dos líneas se comentaron para usar el FindByName

        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[1]/usr/ctxtDY_PATH").Text = lsDirectorio
        MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").Text = "CurrentOTD_" & Planta & ".txt" 'Plant Code

        If Len(Dir(lsDirectorio & "CurrentOTD_" & Planta & ".txt")) > 0 Then
            Kill(lsDirectorio & "CurrentOTD_" & Planta & ".txt")
        End If

        MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findbyId("wnd[0]/tbar[0]/btn[12]").press()
        MySession.findbyId("wnd[0]/tbar[0]/btn[12]").press()
    End Function

    Public Function DownloadManufacter(ByVal MySession As Object, ByVal Plantas As DataTable) As Boolean
        ''MySession.findbyId("wnd[0]").resizeWorkingPane(97, 21, False)
        ''MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "/nzma1"
        ''MySession.findbyId("wnd[0]").sendVKey(0)
        ''MySession.findbyId("wnd[0]/usr/ctxtS_WERKS-LOW").Text = Planta
        ''MySession.findbyId("wnd[0]/usr/ctxtS_MATNR-LOW").Text = "30000000"
        ''MySession.findbyId("wnd[0]/usr/ctxtS_MATNR-HIGH").Text = "39999999"
        ''MySession.findbyId("wnd[0]/usr/ctxtS_MATNR-HIGH").SetFocus()
        ''MySession.findbyId("wnd[0]/usr/ctxtS_MATNR-HIGH").caretPosition = 8
        ''MySession.findbyId("wnd[0]/tbar[1]/btn[8]").press()
        ''MySession.findbyId("wnd[0]/tbar[0]/okcd").Text = "%pc"
        ''MySession.findbyId("wnd[0]").sendVKey(0)

        ' ''*************************************************************************************
        ' ''MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[1,0]").Select()
        ' ''MySession.findbyId("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[1,0]").SetFocus()
        ''MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        ' ''*************************************************************************************


        ''MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()
        ''MySession.findbyId("wnd[1]/usr/ctxtDY_PATH").Text = lsDirectorio
        ''MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").Text = "Manufacter_" & Planta & ".txt"

        ''If Len(Dir(lsDirectorio & "Manufacter_" & Planta & ".txt")) > 0 Then
        ''    Kill(lsDirectorio & "Manufacter_" & Planta & ".txt")
        ''End If

        ''MySession.findbyId("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        ''MySession.findbyId("wnd[1]/tbar[0]/btn[0]").press()


        '**************************************************************************************
        '    New Script
        '**************************************************************************************
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzma1"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/btn%_S_WERKS_%_APP_%-VALU_PUSH").press()

        Put_DataTable_In_ClipBoard(Plantas)
        MySession.findById("wnd[1]/tbar[0]/btn[24]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        MySession.findById("wnd[0]/usr/ctxtS_MATNR-LOW").text = "30000000"
        MySession.findById("wnd[0]/usr/ctxtS_MATNR-HIGH").text = "39999999"

        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        'MySession.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()

        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "ZMA1.txt"


        If Len(Dir(lsDirectorio & "ZMA1.txt")) > 0 Then
            Kill(lsDirectorio & "ZMA1.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

    End Function

    Public Function DownloadEKBE(ByRef Mysession As Object, ByVal Planta$, ByVal Inicio$, ByVal Final$, ByVal Path As String) As Boolean
        Mysession.findById("wnd[0]").resizeWorkingPane(96, 27, False)
        Mysession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        Mysession.findById("wnd[0]").sendVKey(0)
        Mysession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekbe"
        Mysession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        Mysession.findById("wnd[0]").sendVKey(8)


        If Not Mysession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Mysession.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Mysession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If


        Mysession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        Mysession.findById("wnd[1]/tbar[0]/btn[14]").press()
        Mysession.findById("wnd[1]/usr/chk[2,13]").selected = True
        Mysession.findById("wnd[1]/usr/chk[2,14]").selected = True
        Mysession.findById("wnd[1]/usr/chk[2,14]").setFocus()
        Mysession.findById("wnd[1]/tbar[0]/btn[71]").press()
        Mysession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        Mysession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "mat"
        Mysession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
        Mysession.findById("wnd[2]").sendVKey(0)
        Mysession.findById("wnd[3]/usr/lbl[4,4]").setFocus()
        Mysession.findById("wnd[3]/usr/lbl[4,4]").caretPosition = 1
        Mysession.findById("wnd[3]").sendVKey(2)
        Mysession.findById("wnd[1]/usr/chk[2,0]").selected = True
        Mysession.findById("wnd[1]/usr/chk[2,1]").selected = True
        Mysession.findById("wnd[1]/usr/chk[2,1]").setFocus()
        Mysession.findById("wnd[1]/tbar[0]/btn[0]").press()
        Mysession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        Mysession.findById("wnd[1]/tbar[0]/btn[14]").press()
        Mysession.findById("wnd[1]/usr/chk[1,3]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,4]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,5]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,13]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,12]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,12]").setFocus()
        Mysession.findById("wnd[1]/tbar[0]/btn[71]").press()
        Mysession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        Mysession.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
        Mysession.findById("wnd[2]/tbar[0]/btn[0]").press()
        Mysession.findById("wnd[3]/usr/lbl[3,4]").setFocus()
        Mysession.findById("wnd[3]/usr/lbl[3,4]").caretPosition = 1
        Mysession.findById("wnd[3]").sendVKey(2)
        Mysession.findById("wnd[1]/usr/chk[1,3]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,4]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,4]").setFocus()
        Mysession.findById("wnd[1]/tbar[0]/btn[71]").press()
        Mysession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "crea"
        Mysession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 4
        Mysession.findById("wnd[2]/tbar[0]/btn[0]").press()
        Mysession.findById("wnd[3]/usr/lbl[37,2]").setFocus()
        Mysession.findById("wnd[3]/usr/lbl[37,2]").caretPosition = 1
        Mysession.findById("wnd[3]").sendVKey(2)
        Mysession.findById("wnd[1]/usr/chk[1,19]").selected = True
        Mysession.findById("wnd[1]/usr/chk[1,19]").setFocus()
        Mysession.findById("wnd[1]/tbar[0]/btn[6]").press()
        Mysession.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()
        Mysession.findById("wnd[1]/tbar[0]/btn[8]").press()
        Mysession.findById("wnd[0]/usr/ctxtI2-LOW").text = Inicio
        Mysession.findById("wnd[0]/usr/ctxtI2-HIGH").text = Final
        Mysession.findById("wnd[0]/usr/ctxtI3-LOW").text = "30000000"
        Mysession.findById("wnd[0]/usr/ctxtI3-HIGH").text = "39999999"
        Mysession.findById("wnd[0]/usr/ctxtI4-LOW").text = Planta
        Mysession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        Mysession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        Mysession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        Mysession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        Mysession.findById("wnd[0]/tbar[1]/btn[8]").press()

        If Mysession.findById("wnd[0]/sbar").Text <> "" Then
            Return False
        End If

        Mysession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        Mysession.findById("wnd[0]").sendVKey(0)
        Mysession.findById("wnd[1]/tbar[0]/btn[0]").press()

        If Len(Dir(lsDirectorio & "EKBE.txt")) > 0 Then
            Kill(lsDirectorio & "EKBE.txt")
        End If

        Mysession.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
        Mysession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Ekbe.txt"
        Mysession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        Mysession.findById("wnd[1]/tbar[0]/btn[0]").press()

        DownloadEKBE = True
    End Function

    Public Function DownloadEKET(ByRef MySession As Object, ByVal MinPO$, ByVal MaxPO$, ByVal Path As String)

        MySession.findById("wnd[0]").resizeWorkingPane(96, 39, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "eket"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,5]").selected = True
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,5]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,8]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,8]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/usr/ctxtI1-LOW").text = MinPO
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").text = MaxPO
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 10
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        ' MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        If Len(Dir(lsDirectorio & "EKET.txt")) > 0 Then
            Kill(lsDirectorio & "EKET.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "eket.txt"
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        MySession.findById("wnd[1]").sendVKey(0)
    End Function

    Public Function DownloadEban(ByRef Session As Object)
        If Len(Dir(lsDirectorio & "EBAN.txt")) > 0 Then
            Kill(lsDirectorio & "EBAN.txt")
        End If

        Session.findById("wnd[0]").resizeWorkingPane(96, 39, False)
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/usr/ctxtI_TABLE").text = "eban"
        Session.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        Session.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        Session.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        Session.findById("wnd[1]/usr/chk[2,10]").selected = True
        Session.findById("wnd[1]/usr/chk[2,21]").selected = True
        Session.findById("wnd[1]/usr/chk[2,21]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "purch"
        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[3]/usr/lbl[36,8]").setFocus()
        Session.findById("wnd[3]/usr/lbl[36,8]").caretPosition = 1
        Session.findById("wnd[3]").sendVKey(2)
        Session.findById("wnd[1]/usr/chk[2,0]").selected = True
        Session.findById("wnd[1]/usr/chk[2,7]").selected = True
        Session.findById("wnd[1]/usr/chk[2,7]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        Session.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        Session.findById("wnd[0]/usr/ctxtI2-LOW").text = "30000000"
        Session.findById("wnd[0]/usr/ctxtI2-HIGH").text = "39999999"
        Session.findById("wnd[0]/usr/ctxtI2-HIGH").setFocus()
        Session.findById("wnd[0]/usr/ctxtI2-HIGH").caretPosition = 8
        Session.findById("wnd[0]/usr/btn%_I3_%_APP_%-VALU_PUSH").press()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        Session.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        Session.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        Session.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        Session.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        Session.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        Session.findById("wnd[1]/usr/chk[1,5]").selected = True
        Session.findById("wnd[1]/usr/chk[1,15]").selected = True
        Session.findById("wnd[1]/usr/chk[1,16]").selected = True
        Session.findById("wnd[1]/usr/chk[1,17]").selected = True
        Session.findById("wnd[1]/usr/chk[1,18]").selected = True
        Session.findById("wnd[1]/usr/chk[1,19]").selected = True
        Session.findById("wnd[1]/usr/chk[1,20]").selected = True
        Session.findById("wnd[1]/usr/chk[1,22]").selected = True
        Session.findById("wnd[1]/usr/chk[1,22]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "menge"
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        Session.findById("wnd[3]").sendVKey(2)
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        Session.findById("wnd[1]/usr/chk[1,6]").selected = True
        Session.findById("wnd[1]/usr/chk[1,8]").selected = True
        Session.findById("wnd[1]/usr/chk[1,9]").selected = True
        Session.findById("wnd[1]/usr/chk[1,10]").selected = True

        Session.findById("wnd[1]/usr/chk[1,22]").selected = True

        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "flief"
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        Session.findById("wnd[2]").sendVKey(0)
        Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 1
        Session.findById("wnd[3]").sendVKey(2)
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        Session.findById("wnd[1]/usr/chk[1,6]").selected = True
        Session.findById("wnd[1]/usr/chk[1,7]").selected = True
        Session.findById("wnd[1]/usr/chk[1,8]").selected = True

        Session.findById("wnd[1]/usr/chk[1,7]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "plifz"
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        Session.findById("wnd[3]").sendVKey(2)
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,3]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[6]").press()
        Session.findById("wnd[0]/tbar[1]/btn[8]").press()

        'Session.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        'Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        'Session.findById("wnd[1]/usr/chk[2,10]").selected = True
        'Session.findById("wnd[1]/usr/chk[2,21]").selected = True
        'Session.findById("wnd[1]/usr/chk[2,21]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        'Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "purch"
        'Session.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[3]/usr/lbl[36,8]").setFocus()
        'Session.findById("wnd[3]/usr/lbl[36,8]").caretPosition = 1
        'Session.findById("wnd[3]").sendVKey(2)
        'Session.findById("wnd[1]/usr/chk[2,0]").selected = True
        'Session.findById("wnd[1]/usr/chk[2,7]").selected = True
        'Session.findById("wnd[1]/usr/chk[2,7]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        'Session.findById("wnd[0]/usr/ctxtI2-LOW").text = "30000000"
        'Session.findById("wnd[0]/usr/ctxtI2-HIGH").text = "39999999"
        'Session.findById("wnd[0]/usr/ctxtI2-HIGH").setFocus()
        'Session.findById("wnd[0]/usr/ctxtI2-HIGH").caretPosition = 8
        'Session.findById("wnd[0]/usr/btn%_I3_%_APP_%-VALU_PUSH").press()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        'Session.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        'Session.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        'Session.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        'Session.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        'Session.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        'Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        'Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        'Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,5]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,15]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,16]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,17]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,18]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,19]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,20]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,20]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        'Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "werks"
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        'Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        'Session.findById("wnd[3]").sendVKey(2)
        'Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,8]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,9]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,11]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,13]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,14]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,15]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,15]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "lifnr"
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        'Session.findById("wnd[2]").sendVKey(0)
        'Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        'Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 1
        'Session.findById("wnd[3]").sendVKey(2)
        'Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,5]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,7]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,8]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,9]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,7]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "plifz"
        'Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 5
        'Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        'Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
        'Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
        'Session.findById("wnd[3]").sendVKey(2)
        'Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        'Session.findById("wnd[1]/usr/chk[1,3]").setFocus()
        'Session.findById("wnd[1]/tbar[0]/btn[6]").press()
        'Session.findById("wnd[0]/tbar[1]/btn[8]").press()

        Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()

        'Session.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()

        Session.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "EBAN.txt"
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        Session.findById("wnd[1]").sendVKey(0)
    End Function

    Public Function Find_Item(ByRef Session As Object, ByVal lsValor$) As Boolean
        Dim Index%
        Dim Found As Boolean
        Dim Item As Integer
        Dim Valor$

        Index = 0
        Found = False
        Valor = Trim(Session.findbyid("wnd[0]/usr").findallbynameex("MEPO1211-EBELP", 31).elementAt(0).Text)
        Do While Found <> True
            If Trim(Session.findbyid("wnd[0]/usr").findallbynameex("MEPO1211-EBELP", 31).elementAt(0).Text) = lsValor Then
                Session.findbyid("wnd[0]/usr").findallbynameex("MEPO1211-EBELP", 31).elementAt(0).setfocus()
                Found = True
                Find_Item = True
            Else
                Index = Index + 1
                Session.findbyid("wnd[0]/usr").findbynameex("SAPLMEGUITC_1211", 80).verticalScrollbar.position = Index

                If Trim(Session.findbyid("wnd[0]/usr").findallbynameex("MEPO1211-EBELP", 31).elementAt(0).Text) = Valor Then
                    Exit Do
                End If
            End If
        Loop
    End Function

    Public Function UpdateCatalogos(ByRef MySession As Object, ByVal PO As Object, ByVal Item As Object, ByVal Tax As Object, ByVal Jurisd_Code As Object, ByVal Mat_Usage As Object, ByVal Mat_Origen As Object, ByVal NCM_Code As Object, ByVal Mat_Category As Object) As String

        MySession.findById("wnd[0]").maximize()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nme22n"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/tbar[1]/btn[17]").press()
        MySession.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = PO
        MySession.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").caretPosition = 10
        MySession.findById("wnd[1]").sendVKey(0)

        If Trim(Left(MySession.findById("wnd[0]/sbar").Text, 7)) = "E: User" Or Trim(Left(MySession.findById("wnd[0]/sbar").Text, 8)) = "E: Docum" Then
            Return MySession.findById("wnd[0]/sbar").Text
        End If


        MySession.findById("wnd[0]").sendVKey(28)

        MySession.findById("wnd[0]").sendVKey(26) 'Expand Header
        MySession.findById("wnd[0]").sendVKey(28) 'Expand Details
        MySession.findById("wnd[0]").sendVKey(27) 'Expand Overview

        If Not Find_Item(MySession, Item) Then
            MsgBox("El ítem " & Item & " no se ha encontrado.", MsgBoxStyle.Information)
            Return "Linea no encontrada"
        End If

        'Toda esta validación se eliminó ya que en la computadora de Leonardo Valverde presentaba errores
        'Estas validaciones son por si la línea está eliminada o bloqueada

        '' MySession.findbyid("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/btnDETAIL").press()
        ''Verifica si la línea se encuentra eliminada.
        ''############################################

        ''If Not MySession.findbyid("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/btnMEPO1211-STATUSICON[0,0]", False) = Nothing Then
        'If MySession.findbyid("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/btnMEPO1211-STATUSICON[0,0]").tooltip = "Deleted" Then
        '    Return "Línea eliminada"
        'End If
        ''############################################
        '' End If

        ''If Not MySession.findbyid("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/btnMEPO1211-STATUSICON[0,0]", False) = Nothing Then
        'If MySession.findbyid("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/btnMEPO1211-STATUSICON[0,0]").tooltip = "Blocked" Then
        '    Return "Línea bloqueada"
        'End If
        ''End If

        MySession.findbyid("wnd[0]/usr").findbynameex("DETAIL", 40).press()
        MySession.findbyid("wnd[0]/usr").findbynameex("TABIDT7", 91).select()

        If Not DBNull.Value.Equals(Tax) Then 'Verifica si el hay información en el tax code
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32).Text = Tax.ToString.Trim          'Tax Code
        End If
        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        If Not DBNull.Value.Equals(Jurisd_Code) Then 'Verifica si hay informacion en la variable
            'If Jurisd_Code.Trim.Length > 0 Then
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1317-TXJCD", 32).Text = Jurisd_Code.ToString.Trim 'Jurisd. Code
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        MySession.findbyid("wnd[0]/usr").findbynameex("TABIDT11", 91).select()

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Usage) Then
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BMATUSE", 32).Text = Mat_Usage.ToString.Trim  'Material Usage
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Origen) Then
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BMATORG", 32).Text = Mat_Origen.ToString.Trim  'Material Origin
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(NCM_Code) Then
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BNBM", 32).Text = NCM_Code.ToString.Trim     'NCM Code
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Category) Then
            MySession.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BINDUST", 32).Text = Mat_Category.ToString.Trim  'Mat Category
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        MySession.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
            'Return MySession.findById("wnd[0]/sbar").Text
        End If

        If Not MySession.findById("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            MySession.findById("wnd[1]/usr/btnSPOP-VAROPTION1").press()
            'Return MySession.findById("wnd[0]/sbar").Text
        End If


        If Trim(MySession.findById("wnd[0]/sbar").Text) = "" Then
            Return "Please check the changes."
        Else
            Return MySession.findById("wnd[0]/sbar").Text
        End If

    End Function

    Sub BuscarNombreControles(ByRef objGrid$, ByRef txtFecha$, ByRef ScrollBar$, ByRef txtQty$, ByRef txtPrice$, ByVal MySession As Object)
        'Buscar el nombre del grid
        '*************************************
        '   grid para L7A
        '*************************************
        If Not MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]", False) Is Nothing Then
            objGrid = "wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]"
            txtFecha = "wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/ctxtMEPO1211-EEIND[9,0]"
            ScrollBar = "wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211"
            txtQty = "wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-MENGE[6,0]"
            txtPrice = "wnd[0]/usr/subSUB0:SAPLMEGUI:0020/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-NETPR[10,0]"
            Exit Sub
        End If

        '*************************************
        '   grid para G4P
        '*************************************
        If Not MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]", False) Is Nothing Then
            objGrid = "wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]"
            txtFecha = "wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/ctxtMEPO1211-EEIND[9,0]"
            ScrollBar = "wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211"
            txtQty = "wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-MENGE[6,0]"
            txtPrice = "wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-NETPR[10,0]"
            Exit Sub
        End If

        '*************************************
        '   grid para G4P
        '*************************************
        If Not MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]", False) Is Nothing Then
            objGrid = "wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]"
            txtFecha = "wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/ctxtMEPO1211-EEIND[9,0]"
            ScrollBar = "wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211"
            txtQty = "wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-MENGE[6,0]"
            txtPrice = "wnd[0]/usr/subSUB0:SAPLMEGUI:0015/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-NETPR[10,0]"
            Exit Sub
        End If

        If Not MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]", False) Is Nothing Then
            objGrid = "wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-EBELP[1,0]"
            txtFecha = "wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/ctxtMEPO1211-EEIND[9,0]"
            ScrollBar = "wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211"
            txtQty = "wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-MENGE[6,0]"
            txtPrice = "wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB2:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1211/tblSAPLMEGUITC_1211/txtMEPO1211-NETPR[10,0]"
            Exit Sub
        End If
    End Sub

    Public Sub CloseSession(ByVal SAP)
        SAP.findbyId("wnd[0]").Close()
        SAP.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
    End Sub

    'Sub ty()
    '    Dim cnt As New ADODB.Connection
    '    Dim rst As New ADODB.Recordset
    '    Dim stCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\test.xls;Extended Properties=""Excel 8.0;HDR=YES"";"
    '    Dim stSQL As String = "Select * from [sheet1$]"
    '    Try
    '        cnt.Open(stCon)
    '        rst.Open(stSQL, cnt)
    '        'Forme.TextBox1.Text = rst.Fields(0).Value

    '        MsgBox(rst.Fields(0).Value)


    '    Catch Err As Exception
    '        'MessageBox.Show(Err.Message)
    '    Finally
    '        rst.Close()
    '        cnt.Close()
    '        rst = Nothing
    '        cnt = Nothing
    '    End Try
    'End Sub

    Public Function DownloadSourceList(ByRef MySession As Object) As Boolean
        Session.findById("wnd[0]").maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/usr/ctxtI_TABLE").text = "eord"
        Session.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        Session.findById("wnd[0]").sendVKey(8)

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        Session.findById("wnd[1]/usr/chk[1,5]").selected = True
        Session.findById("wnd[1]/usr/chk[1,9]").selected = True
        Session.findById("wnd[1]/usr/chk[1,10]").selected = True
        Session.findById("wnd[1]/usr/chk[1,10]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[6]").press()
        Session.findById("wnd[0]/usr/ctxtI1-LOW").text = "30000000"
        Session.findById("wnd[0]/usr/ctxtI1-HIGH").text = "39999999"
        Session.findById("wnd[0]/usr/ctxtI1-HIGH").setFocus()
        Session.findById("wnd[0]/usr/ctxtI1-HIGH").caretPosition = 8
        Session.findById("wnd[0]/tbar[1]/btn[8]").press()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        Session.findById("wnd[0]").sendVKey(0)


        '*************************************************************************************
        'Session.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        Session.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************

        Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "EORD.txt"

        If Len(Dir(lsDirectorio & "EORD.txt")) > 0 Then
            Kill(lsDirectorio & "EORD.txt")
        End If

        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()
    End Function
    ''' <summary>
    ''' Decarga la tabla EKKO de los catálogos
    ''' </summary>
    ''' <param name="MySession">Coneccion con SAP</param>
    ''' <param name="FechaInicio">Fecha Inicio del reporte</param>
    ''' <param name="FechaFinal">Fecha Final para el reporte</param>
    ''' <param name="Caja">Nombre de la caja de la que se descarga la info. Esto porque GBP el nombre del control se llama diferente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadHeaderCatalogos(ByVal MySession As Object, ByVal FechaInicio$, ByVal FechaFinal$, ByVal Caja$) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(128, 27, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekko"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,8]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,12]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[6,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[6,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[2,0]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,0]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        Session.findById("wnd[0]/usr/ctxtI1-LOW").text = "EC"
        Session.findById("wnd[0]/usr/ctxtI1-LOW").caretPosition = 2
        Session.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()

        Session.findById("wnd[0]/usr/ctxtI3-LOW").text = FechaInicio
        Session.findById("wnd[0]/usr/ctxtI3-HIGH").text = FechaFinal

        MySession.findById("wnd[0]/usr/ctxtI4-LOW").setFocus()
        MySession.findById("wnd[0]/usr/ctxtI4-LOW").caretPosition = 0
        MySession.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()

        If Trim(MySession.findById("wnd[0]/sbar").Text) <> "" Then
            MsgBox(MySession.findById("wnd[0]/sbar").Text, MsgBoxStyle.Critical)
            Return False
        End If

        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = "1377"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "1378"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,2]").text = "1376"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,3]").text = "1379"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").text = "1369"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").caretPosition = 4
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[5,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[5,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,3]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        ' ''Select Case Caja
        ' ''    Case "GBP", "G4P"
        ' ''        MySession.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()

        ' ''    Case Else
        ' ''        MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        ' ''End Select

        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************


        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "HeaderCatalogos.txt"

        If Len(Dir(lsDirectorio & "HeaderCatalogos.txt")) > 0 Then
            Kill(lsDirectorio & "HeaderCatalogos.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 19
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        Return True
    End Function

    Public Function DownloadHeaderCatalogos_Utilities(ByVal MySession As Object, ByVal FechaInicio$, ByVal FechaFinal$, ByVal Variante As DataTable, ByVal Caja$) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(128, 27, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekko"
        MySession.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        MySession.findById("wnd[0]").sendVKey(8)

        If Not MySession.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            MySession.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
            MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        End If

        MySession.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[2,8]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,10]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,16]").selected = True

        MySession.findById("wnd[1]/usr/chk[2,12]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[6,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[6,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[2,0]").selected = True
        MySession.findById("wnd[1]/usr/chk[2,0]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        '*********************************************************************
        'Codigo para diferenciar las ordenes de compra que son de utilities
        MySession.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = "EC"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "NB"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").caretPosition = 2
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        '*********************************************************************

        Session.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()

        MySession.findById("wnd[0]/usr/ctxtI3-LOW").text = FechaInicio
        MySession.findById("wnd[0]/usr/ctxtI3-HIGH").text = FechaFinal


        MySession.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()


        Session.findById("wnd[1]/tbar[0]/btn[24]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()

        'MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = Variante.Rows(0).Item("Valor")

        'Dim i As Integer
        'For i = 1 To Variante.Rows.Count - 1
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = Variante.Rows(i).Item("Valor")
        '    MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = i
        'Next

        'MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        If Trim(MySession.findById("wnd[0]/sbar").Text) <> "" Then
            MsgBox(MySession.findById("wnd[0]/sbar").Text, MsgBoxStyle.Critical)
            Return False
        End If

        MySession.findById("wnd[0]/usr/btn%_I5_%_APP_%-VALU_PUSH").press()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = "1377"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "1378"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,2]").text = "1376"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,3]").text = "1379"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").text = "1369"
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").setFocus()
        MySession.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").caretPosition = 4

        MySession.findById("wnd[1]/tbar[0]/btn[8]").press()
        MySession.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        MySession.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        MySession.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        MySession.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        MySession.findById("wnd[1]/tbar[0]/btn[14]").press()
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,4]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,11]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,12]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,15]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[71]").press()
        MySession.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
        MySession.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
        MySession.findById("wnd[2]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[3]/usr/lbl[5,2]").setFocus()
        MySession.findById("wnd[3]/usr/lbl[5,2]").caretPosition = 1
        MySession.findById("wnd[3]").sendVKey(2)
        MySession.findById("wnd[1]/usr/chk[1,3]").selected = True
        MySession.findById("wnd[1]/usr/chk[1,3]").setFocus()
        MySession.findById("wnd[1]/tbar[0]/btn[6]").press()
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()

        'Verifica que se encontraran ordenes de compra
        If Trim(MySession.findById("wnd[0]/sbar").Text) <> "" Then
            MsgBox(MySession.findById("wnd[0]/sbar").Text, MsgBoxStyle.Critical)
            Return False
        End If

        MySession.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        MySession.findById("wnd[0]").sendVKey(0)

        '*************************************************************************************
        ' ''Select Case Caja
        ' ''    Case "GBP", "G4P"
        ' ''        MySession.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[0,0]").select()
        ' ''    Case Else
        ' ''        MySession.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        ' ''End Select

        MySession.findbyid("wnd[1]/usr").findbynameex("SPOPLI-SELFLAG", 41).select()
        '*************************************************************************************


        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtDY_PATH").text = lsDirectorio
        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "HeaderCatalogos.txt"

        If Len(Dir(lsDirectorio & "HeaderCatalogos.txt")) > 0 Then
            Kill(lsDirectorio & "HeaderCatalogos.txt")
        End If

        MySession.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 19
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()

        Return True
    End Function

    Public Function GetCostCenter(ByRef MySession As Object, ByVal PO$, ByVal Item$) As String
        MySession.findById("wnd[0]").maximize()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nme23n"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/tbar[1]/btn[17]").press()
        MySession.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = PO
        MySession.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").caretPosition = 10
        MySession.findById("wnd[1]").sendVKey(0)

        MySession.findById("wnd[0]").sendVKey(28)

        MySession.findById("wnd[0]").sendVKey(26) 'Expand Header
        MySession.findById("wnd[0]").sendVKey(28) 'Expand Details
        MySession.findById("wnd[0]").sendVKey(27) 'Expand Overview

        If Not Find_Item(MySession, Item) Then
            MsgBox("El ítem " & Item & " no se ha encontrado.", MsgBoxStyle.Information)
            'Return "Linea no encontrada"
        End If

        MySession.findbyid("wnd[0]/usr").findbynameex("DETAIL", 40).press()


        'Seleccionar el tab de Account Assignment
        MySession.findbyid("wnd[0]/usr").findbynameex("TABIDT12", 91).select()
        'MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12").select()

        If Not MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12/ssubTABSTRIPCONTROL1SUB:SAPLMEVIEWS:1101/subSUB2:SAPLMEACCTVI:0100/subSUB1:SAPLMEACCTVI:1100/subKONTBLOCK:SAPLKACB:1101/ctxtCOBL-KOSTL", False) Is Nothing Then
            Return MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12/ssubTABSTRIPCONTROL1SUB:SAPLMEVIEWS:1101/subSUB2:SAPLMEACCTVI:0100/subSUB1:SAPLMEACCTVI:1100/subKONTBLOCK:SAPLKACB:1101/ctxtCOBL-KOSTL").text()
        Else
            Return "Verificar"
        End If

    End Function

    Public Function FixSourceListOA(ByRef MySession As Object, ByVal Gica$, ByVal Planta$, ByVal Inicio$, ByVal Fin$, ByVal Vendor$, ByVal PG$, ByVal OA$, ByVal Item$) As Boolean

        MySession.findById("wnd[0]").resizeWorkingPane(119, 28, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nme01"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtEORD-MATNR").text = Gica
        MySession.findById("wnd[0]/usr/ctxtEORD-WERKS").text = Planta
        MySession.findById("wnd[0]/usr/ctxtEORD-WERKS").setFocus()
        MySession.findById("wnd[0]/usr/ctxtEORD-WERKS").caretPosition = 0
        MySession.findById("wnd[0]").sendVKey(0)

        If Trim(MySession.findById("wnd[0]/sbar").Text) <> "" Then
            Return False
        End If

        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-VDATU[0,0]").text = Inicio
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-BDATU[1,0]").text = Fin
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-LIFNR[2,0]").text = Vendor
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EKORG[3,0]").text = PG
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EBELN[6,0]").text = OA
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EBELP[7,0]").text = Item
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").text = "1"
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").setFocus()
        MySession.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").caretPosition = 1
        MySession.findById("wnd[0]/tbar[0]/btn[11]").press()
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]").sendVKey(0)

        FixSourceListOA = True
    End Function

    Public Function SyncOA_MD(ByRef MySession As Object, ByVal pFile As String) As Boolean
        Dim JobName As String = "DB_Job"
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000459"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/chkSELCHGPR").selected = True
        MySession.findById("wnd[0]/usr/chkSELCHGTX").selected = False
        MySession.findById("wnd[0]/usr/chkCHGZMANP").selected = False
        MySession.findById("wnd[0]/usr/txtOA_FILE").text = pFile
        MySession.findById("wnd[0]/usr/txtP_BNAME").text = JobName & "_" & TimeOfDay.Hour & "_" & TimeOfDay.Minute & "_" & TimeOfDay.Second
        MySession.findById("wnd[0]/usr/txtP_BNAME").setFocus()
        MySession.findById("wnd[0]/usr/txtP_BNAME").caretPosition = 4
        MySession.findById("wnd[0]/tbar[1]/btn[8]").press()
        MySession.findById("wnd[1]/usr/ctxtPRI_PARAMS-PDEST").text = "locl"
        MySession.findById("wnd[1]/usr/ctxtPRI_PARAMS-PDEST").caretPosition = 4
        MySession.findById("wnd[1]").sendVKey(0)
        MySession.findById("wnd[1]/tbar[0]/btn[13]").press()
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
    End Function

    Public Function FixMasterDataChecks(ByRef MySession As Object, ByVal Gica$, ByVal Planta$) As Boolean
        MySession.findById("wnd[0]").resizeWorkingPane(118, 27, False)
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nmm02"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtRMMG1-MATNR").text = Gica
        MySession.findById("wnd[0]/usr/ctxtRMMG1-MATNR").caretPosition = 0
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[1]/usr/tblSAPLMGMMTC_VIEW").getAbsoluteRow(0).selected = True
        MySession.findById("wnd[1]/tbar[0]/btn[0]").press()
        MySession.findById("wnd[1]/usr/ctxtRMMG1-WERKS").text = Planta
        MySession.findById("wnd[1]/usr/ctxtRMMG1-WERKS").caretPosition = 4
        MySession.findById("wnd[1]").sendVKey(0)
        MySession.findById("wnd[0]/usr/tabsTABSPR1/tabpSP09/ssubTABFRA1:SAPLMGMM:2000/subSUB2:SAPLMGD1:2301/chkMARC-KAUTB").selected = True
        MySession.findById("wnd[0]/usr/tabsTABSPR1/tabpSP09/ssubTABFRA1:SAPLMGMM:2000/subSUB4:SAPLMGD1:2313/chkMARC-KORDB").selected = True
        MySession.findById("wnd[0]/usr/tabsTABSPR1/tabpSP09/ssubTABFRA1:SAPLMGMM:2000/subSUB4:SAPLMGD1:2313/chkMARC-KORDB").setFocus()
        MySession.findById("wnd[0]/tbar[0]/btn[11]").press()
    End Function

    Public Function FixValidityOfOA(ByRef MySession As Object, ByVal Contrato$, ByVal Inicio$, ByVal Fin$) As Boolean
        MySession.findById("wnd[0]").maximize()
        MySession.findById("wnd[0]/tbar[0]/okcd").text = "/nme32k"
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/usr/ctxtRM06E-EVRTN").text = Contrato
        MySession.findById("wnd[0]/usr/ctxtRM06E-EVRTN").caretPosition = 10
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/tbar[1]/btn[6]").press()
        MySession.findById("wnd[0]/usr/ctxtEKKO-KDATB").text = Inicio
        MySession.findById("wnd[0]/usr/ctxtEKKO-KDATE").text = Fin
        MySession.findById("wnd[0]/usr/ctxtEKKO-KDATE").setFocus()
        MySession.findById("wnd[0]/usr/ctxtEKKO-KDATE").caretPosition = 10
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]").sendVKey(0)
        MySession.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not Session.findById("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
            Session.findById("wnd[1]/usr/btnSPOP-OPTION1").SetFocus()
            Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press()
        End If

        'MySession.findById("wnd[1]/usr/btnSPOP-OPTION1").press()
    End Function

    ''' <summary>
    ''' Coloca la información de un DataTable en el PortaPapeles. 
    ''' <code>El listado que se colocará en el portapapeles es el de la primera columna</code>
    ''' </summary>
    ''' <param name="Tabla">Tabla que tiene la información que será colocada en el PortaPapeles</param>

    Public Function Put_DataTable_In_ClipBoard(ByVal Tabla As DataTable) As Boolean
        Dim Text As String = ""
        Dim i As Integer

        My.Computer.Clipboard.Clear()
        For i = 0 To Tabla.Rows.Count - 1
            Text = Text & Tabla.Rows(i).Item(0) & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(Text)
    End Function

    Public Sub New()

    End Sub
End Class
