Imports System.Data.Common
Imports System.Data
Imports System.Configuration
Imports SAPCOM.RepairsLevels
Imports System.ComponentModel
Imports System.Threading
Imports Microsoft.VisualBasic

Public Class c_SAP
    Implements IDisposable

    Dim _Connected As Boolean
    Dim _TNumber As String = ""
    Dim _Password As String = ""
    Dim _SAPBox As String = ""
    Dim _lsDirectory As String = My.Application.Info.DirectoryPath & "\OADownLoad\"

    Private Application
    Private Connection
    Private Session
    Private disposed As Boolean = False
    Private AppId As String = "LAT"
    Private _Configuration As Boolean

    ''' <summary>
    ''' Indica si la configuración de Windows & SAP estan OK
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Configuration() As Boolean
        Get
            Return _Configuration
        End Get

    End Property
    ''' <summary>
    ''' Directorio donde se descargaran los archivos de SAP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DownloadDirectory() As String
        Get
            Return _lsDirectory
        End Get
        Set(ByVal value As String)
            _lsDirectory = value
        End Set
    End Property
    Public ReadOnly Property GUI() As Object
        Get
            Return Session
        End Get
    End Property

    ''' <summary>
    ''' Determina si existe una conexión con SAP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Conected() As Boolean
        Get
            Return _Connected
        End Get
    End Property
    ''' <summary>
    ''' Setea un valor verdadero si existe conección con SAP
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property SetConected() As Boolean
        Set(ByVal value As Boolean)
            _Connected = value
        End Set
    End Property
    ''' <summary>
    ''' Crea una instacia de SAP
    ''' </summary>
    ''' <param name="TNumber">Nombre de usuario. Ej. BM4691</param>
    ''' <param name="Password">Contraseña para la caja a la que se hará login</param>
    ''' <param name="SAPBox">Nombre de la caja a la que tendrá acceso Ej: L7P LA TS Prod</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TNumber$, ByVal Password$, ByVal SAPBox$)
        _TNumber = TNumber
        _Password = Password
        _SAPBox = SAPBox
    End Sub
    ''' <summary>
    ''' Crea una instancia de SAP
    ''' </summary>
    ''' <param name="SAPBox">Caja de SAP</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal SAPBox$)
        If SAPBox.Length > 3 Then
            _SAPBox = SAPBox
        Else
            Select Case SAPBox
                Case "L7P"
                    _SAPBox = "L7P LA TS Prod"

                Case "L7A"
                    _SAPBox = "L7A TS Acceptance"

                Case "L6P"
                    _SAPBox = "L6P LA SC  Prod"

                Case "N6P"
                    _SAPBox = "N6P NA Prod"

                Case "GBP"
                    _SAPBox = "GBP GCM Production"

                Case "G4P"
                    _SAPBox = "G4P GCF/Cons Prod"
            End Select
        End If

    End Sub

    ''' <summary>
    ''' T-Number de usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserName() As String
        Get
            Return _TNumber
        End Get
        Set(ByVal value As String)
            _TNumber = value
        End Set
    End Property

    ''' <summary>
    ''' Abre una sesión de SAP
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenConnection(ByVal SAPConfig As Boolean)
        Try
            If Me._Connected Then
                Exit Sub
            End If

            If _SAPBox.Length = 0 Then 'por si no se ha ingresado la caja en el objeto
                Me.SetConected = False
                MsgBox("No se ha registrado la caja de SAP para realizar la conexion.", MsgBoxStyle.Exclamation, "Falta la contraseña de Usuario")
                Exit Sub
            End If

            Application = CreateObject("Sapgui.ScriptingCtrl.1")

            '**************************************************
            If Not SAPConfig Then
                Connection = Application.OpenConnection(_SAPBox)
                Session = Connection.Children(0)

                Session.findbyId("wnd[0]/usr/txtRSYST-BNAME").Text = _TNumber
                Session.findbyId("wnd[0]/usr/pwdRSYST-BCODE").Text = _Password

                Session.findbyId("wnd[0]").sendVKey(0)

                If Session.findById("wnd[0]/sbar").Text <> "" Then
                    MsgBox("No se puede realizar la conexion con SAP." & vbCr & vbCr & "Por favor verifique el nombre de usuario y la contraseña y vuelva a intentarlo.", MsgBoxStyle.Critical)
                    Me.SetConected = False
                    Session.findbyId("wnd[0]/tbar[0]/btn[15]").press()
                    Exit Sub
                End If
            Else
                Connection = Application.OpenConnection(_SAPBox & " - SSO")
                Session = Connection.Children(0)
            End If

            If Not Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").SetFocus()
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").Selected = True
                Session.findbyId("wnd[1]/tbar[0]/btn[0]").press()
            End If

            Me.SetConected = True

        Catch ex As Exception
            ' If (_SAPBox.IndexOf("G4P") >= 0) Or (_SAPBox.IndexOf("GBP") >= 0) Or (_SAPBox.IndexOf("N6P") >= 0) Then
            GoTo TrySSO
            'End If

            MsgBox("Can't get SAP Connection.", MsgBoxStyle.Critical)
            Me.SetConected = False
        Finally
        End Try

        Exit Sub

TrySSO:
        Try
            Connection = Application.OpenConnection(_SAPBox & "- SSO")
            Session = Connection.Children(0)

            If Not Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").SetFocus()
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").Selected = True
                Session.findbyId("wnd[1]/tbar[0]/btn[0]").press()
            End If

            Me.SetConected = True

        Catch ex As Exception
            MsgBox("Can't get SAP Connection.", MsgBoxStyle.Critical)
            Me.SetConected = False
        End Try

    End Sub

    ''' <summary>
    ''' Password para ingresar a la caja de SAP
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Password() As String
        Get
            Return _Password
        End Get

        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    ''' <summary>
    ''' Cierra la sesión de SAP
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CloseConnection()
        If _Connected Then
            Try
                Session.findbyId("wnd[0]").Close()
                Session.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
                Me.SetConected = False
            Catch ex As Exception
                Me.SetConected = False
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Envia las instrucciones para guardar un archivo a ser descargado de SAP 
    ''' </summary>
    ''' <param name="FileName">Nombre del archivo</param>
    ''' <param name="FilePath">Ruta donde se va a descargar el archivo</param>
    ''' <remarks></remarks>
    Private Function SaveSAPFile(ByVal FileName$, Optional ByVal FilePath$ = "") As Boolean

        If FileName.Length = 0 Then
            MsgBox("Debe ingresar el nombre de archivo para guardarlo.", MsgBoxStyle.Information)
            Return False
        End If

        If FilePath.Length = 0 Then
            FilePath = _lsDirectory
        End If

        If Session.findById("wnd[0]/sbar").Text <> "" Then
            Return False
        End If

        Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()

        Session.findById("wnd[1]/usr/ctxtDY_PATH").text = FilePath
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = FileName

        If My.Computer.FileSystem.FileExists(_lsDirectory & FileName) Then
            Kill(_lsDirectory & FileName)
        End If

        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 19
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()

        Return True
    End Function
    '''' <summary>
    '''' Descarga las ordenes de compra para catalogos y free form text
    '''' </summary>
    '''' <remarks></remarks>
    '''' <parameters></parameters>
    'Public Function DownloadUtilities(ByVal FechaInicio$, ByVal FechaFinal$) As Boolean
    '    If Me._Connected Then
    '        Session.findById("wnd[0]").resizeWorkingPane(128, 27, False)
    '        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
    '        Session.findById("wnd[0]").sendVKey(0)
    '        Session.findById("wnd[0]/usr/ctxtI_TABLE").text = "ekko"
    '        Session.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
    '        Session.findById("wnd[0]").sendVKey(8)

    '        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
    '            Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
    '            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
    '        End If

    '        Session.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
    '        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
    '        Session.findById("wnd[1]/usr/chk[2,8]").selected = True
    '        Session.findById("wnd[1]/usr/chk[2,10]").selected = True
    '        Session.findById("wnd[1]/usr/chk[2,12]").selected = True
    '        Session.findById("wnd[1]/usr/chk[2,12]").setFocus()
    '        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
    '        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
    '        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
    '        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
    '        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
    '        Session.findById("wnd[3]/usr/lbl[6,2]").setFocus()
    '        Session.findById("wnd[3]/usr/lbl[6,2]").caretPosition = 1
    '        Session.findById("wnd[3]").sendVKey(2)
    '        Session.findById("wnd[1]/usr/chk[2,0]").selected = True
    '        Session.findById("wnd[1]/usr/chk[2,0]").setFocus()
    '        Session.findById("wnd[1]/tbar[0]/btn[0]").press()

    '        Session.findById("wnd[0]/usr/ctxtI1-LOW").text = "EC"
    '        Session.findById("wnd[0]/usr/ctxtI1-LOW").caretPosition = 2
    '        Session.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").setFocus()
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/btnRSCSEL-SOP_I[0,0]").press()
    '        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
    '        Session.findById("wnd[1]/tbar[0]/btn[8]").press()

    '        Session.findById("wnd[0]/usr/ctxtI3-LOW").text = FechaInicio
    '        Session.findById("wnd[0]/usr/ctxtI3-HIGH").text = FechaFinal

    '        Session.findById("wnd[0]/usr/ctxtI4-LOW").setFocus()
    '        Session.findById("wnd[0]/usr/ctxtI4-LOW").caretPosition = 0
    '        Session.findById("wnd[0]/usr/btn%_I4_%_APP_%-VALU_PUSH").press()

    '        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
    '            MsgBox(Session.findById("wnd[0]/sbar").Text, MsgBoxStyle.Critical)
    '        End If

    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = "1377"
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = "1378"
    '        'Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,2]").text = "1376"
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,3]").text = "1379"
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").text = "1369"
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").setFocus()
    '        Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,4]").caretPosition = 4

    '        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
    '        Session.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
    '        Session.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
    '        Session.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
    '        Session.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
    '        Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
    '        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
    '        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,11]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,12]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,15]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,15]").setFocus()
    '        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
    '        Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
    '        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "org"
    '        Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 3
    '        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
    '        Session.findById("wnd[3]/usr/lbl[5,2]").setFocus()
    '        Session.findById("wnd[3]/usr/lbl[5,2]").caretPosition = 1
    '        Session.findById("wnd[3]").sendVKey(2)
    '        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
    '        Session.findById("wnd[1]/usr/chk[1,3]").setFocus()
    '        Session.findById("wnd[1]/tbar[0]/btn[6]").press()
    '        Session.findById("wnd[0]/tbar[1]/btn[8]").press()

    '        'Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
    '        'Session.findById("wnd[0]").sendVKey(0)
    '        'Session.findById("wnd[1]/usr/sub:SAPLSPO5:0201/radSPOPLI-SELFLAG[0,0]").select()
    '        'Session.findById("wnd[1]/tbar[0]/btn[0]").press()
    '        'Session.findById("wnd[1]/usr/ctxtDY_PATH").text = _lsDirectory
    '        'Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "HeaderUtilities.txt"

    '        'If Len(Dir(_lsDirectory & "HeaderUtilities.txt")) > 0 Then
    '        '    Kill(_lsDirectory & "HeaderUtilities.txt")
    '        'End If

    '        'Session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 19
    '        'Session.findById("wnd[1]/tbar[0]/btn[0]").press()

    '        Return Me.SaveSAPFile("HeaderUtilities.txt")

    '    End If
    'End Function

    ''' <summary>
    ''' Esta funcion realiza el push para que a la requi se le asigne el contrato.
    ''' </summary>
    ''' <param name="Requisicion">Número de la requi a la que hay que hacer el Push</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PushRequi(ByVal Requisicion As String) As Boolean
        If Not _Connected Then
            PushRequi = False
            Exit Function
        End If

        Session.findById("wnd[0]").maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme57"
        Session.findById("wnd[0]").sendVKey(0)
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
    ''' <summary>
    ''' Descarga la informacion de los source list de los contratos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadSourceList() As Boolean

        If Not _Connected Then
            Return False
        End If

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

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If Not Me.SaveSAPFile("SourceList.txt") Then
            MsgBox("Error al guardar el archivo.", MsgBoxStyle.Critical)
            Return False
        Else
            Return True
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Function
    ''' <summary>
    ''' Prepara la impresion de la PO
    ''' </summary>
    ''' <param name="NUM">Número de PO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Change_PO(ByVal NUM As String) As Boolean
        Session.findById("wnd[0]").Iconify()
        Session.findById("wnd[0]").Maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme22n"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/tbar[1]/btn[17]").press()
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = NUM
        Session.findById("wnd[1]").sendVKey(0)

        If Not Session.findbyId("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
            Session.findbyId("wnd[1]/usr/btnSPOP-OPTION1").press()
        End If

        If Not Session.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            Session.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
            If ((Trim(Session.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("RELEASES ALREADY") <> -1) And (Trim(Session.findById("wnd[0]/sbar").Text).ToUpper.IndexOf("SAPSCRIPT") <> -1)) Then
                Return False
            End If
        End If

        'Verfico que la pantalla no se encuentre bloqueada
        If Not Session.findbyid("wnd[0]/usr").findbynameex("LOCK", 40).changeable Then
            Session.findbyid("wnd[0]/tbar[1]").findbynameex("btn[7]", 40).press()
        End If

        Return True
        'Session.findById("wnd[1]").sendVKey(0)
    End Function
    ''' <summary>
    ''' Imprime una Orden de compra
    ''' </summary>
    ''' <param name="PO">Número de PO a ser impresa</param>
    ''' <param name="FormatoImpresion">Tipo del formato que se impimirá: "NNXX" ó "NEU"</param>
    ''' <param name="Idioma">Idioma para la impresión</param>
    ''' <param name="PrintAle">Establese si se imprime las ordenes con impresion ALE</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Print_PO(ByVal PO As String, ByVal FormatoImpresion As String, ByVal Idioma As String, ByVal PrintAle As Boolean) As String
        Dim UA
        Dim TT
        Dim R As String
        Dim Sended As Boolean
        Dim I As Integer
        Dim S As String
        'Dim Intentos As Integer = 0


        If Not Me._Connected Then
            MsgBox("Couldn't get SAP connection")
            Print_PO = "Couldn't get SAP connection"
            Exit Function
        End If

        'For Intentos = 0 To 2
        R = ""
        If Not Change_PO(PO) Then
            Return "Error: " & Session.findById("wnd[0]/sbar").text
        End If

        'Change_PO(PO)
        On Error GoTo ErrHandle
        Sended = True
        Session.findById("wnd[0]/tbar[1]/btn[21]").press()

        UA = Session.findById("wnd[0]/usr")
        TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
        I = 0

        If TT.Rows.ElementAt(I).Item(0).Tooltip = "Not processed" Then
            Session.findById("wnd[0]/usr/tblSAPDV70ATC_NAST3").getAbsoluteRow(I).Selected = True
            Session.findById("wnd[0]/tbar[1]/btn[18]").press()

            'Cerrar una ventana de información
            If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
                Session.findById("wnd[1]/tbar[0]/btn[0]").SetFocus()
                Session.findById("wnd[1]/tbar[0]/btn[0]").press()
            End If

            Session.findById("wnd[0]/tbar[0]/btn[83]").press()
            UA = Session.findById("wnd[0]/usr")
            TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            I = 0
        End If


        Dim PrintedAsAle As Boolean = False

        Do While ((TT.Rows.ElementAt(I).Item(1).text <> "") And TT.Rows.ElementAt(I).Item(0).Tooltip <> "Not processed")

            'Verifico si tiene impresion ALE
            If TT.Rows.ElementAt(I).Item(3).text.ToString.IndexOf("ALE") <> -1 Then
                PrintedAsAle = True
            End If

            I = I + 1
            If I > 16 Then
                Session.findById("wnd[0]/tbar[0]/btn[83]").press()
                I = 0
                UA = Session.findById("wnd[0]/usr")
                TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            End If
        Loop


        If Not PrintAle And PrintedAsAle Then
            Return "Error: Impresión ALE"
        End If


        TT.Rows.ElementAt(I).Item(1).text = FormatoImpresion
        TT.Rows.ElementAt(I).Item(3).key = "1"
        TT.Rows.ElementAt(I).Item(4).text = "VN"

        '*******************************************
        '   Para la parte del idioma de la impresion
        TT.Rows.ElementAt(I).Item(6).Text = Idioma
        '*******************************************

        Session.findById("wnd[0]").sendVKey(3)

        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
            R = Trim(Session.findById("wnd[0]/sbar").Text)
        End If

        UA.FindByNameEx("NAST-VSZTP", 34).key = "4"
        Session.findById("wnd[0]").sendVKey(3)
        Session.findById("wnd[0]").sendVKey(3)
        UA.FindByNameEx("NAST-LDEST", 32).text = "LOCL"
        UA.FindByNameEx("NAST-DIMME", 42).Selected = True
        UA.FindByNameEx("NAST-DELET", 42).Selected = True
        Session.findById("wnd[0]").sendVKey(3)
        Session.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not Session.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            Session.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").SetFocus()
            Session.findbyId("wnd[1]/usr/btnSPOP-VAROPTION1").press()
        End If

        R = Session.findById("wnd[0]/sbar").text

        If R.IndexOf("changed") > 0 Then
            'Exit For
        Else
            'If Intentos = 2 Then
            Return "Error: PO:" & PO & " couldn't be printed."
            'End If
        End If

        'Next

        Session.findById("wnd[0]/tbar[0]/btn[3]").press()

TheExit:
        Print_PO = R
        'On Error GoTo 0
        Exit Function

ErrHandle:
        'If Err() = 613 Or 619 Then
        '    Session.findById("wnd[0]/tbar[0]/btn[3]").press()
        'End If
        ''MsgBox (Err)
        'MsgBox (Err.Description)
        Sended = False
        Resume TheExit
    End Function
    Function Display_PO(ByVal NUM As String) As Boolean
        Session.findById("wnd[0]").Iconify()
        Session.findById("wnd[0]").Maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme23n"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/tbar[1]/btn[17]").press()
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = NUM
        Session.findById("wnd[1]").sendVKey(0)
    End Function
    ''' <summary>
    ''' Modifica el spool del archivo impreso
    ''' </summary>
    ''' <param name="PO">Número de PO</param>
    ''' <param name="FormatoImpresion">Formato en el que se imprimió la PO</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function Spool_PO(ByVal PO As String, ByVal FormatoImpresion As String, ByVal Idioma As String) As String
        Dim UA
        Dim TT
        Dim R As String
        Dim I As Integer
        Dim Sended As Boolean

        R = ""
        Try
            Sended = True

            Display_PO(PO)
            Session.findById("wnd[0]/tbar[1]/btn[21]").press()
            UA = Session.findById("wnd[0]/usr")
            TT = UA.FindByNameEx("SAPDV70ATC_NAST3", 80)
            I = 0

            Do While True
                If ((TT.Rows.ElementAt(I).Item(1).text = FormatoImpresion) And (I <= TT.Rows.COUNT - 1) And (TT.Rows.ElementAt(I).Item(3).text = "Print output") And (TT.Rows.ElementAt(I).Item(6).text = Idioma)) Then
                    Exit Do
                Else
                    I = I + 1
                End If

            Loop

            UA.FindByNameEx("SAPDV70ATC_NAST3", 80).Rows.ElementAt(I).Selected = True
            Session.findById("wnd[0]/tbar[1]/btn[26]").press()
            R = Val(Right(Session.findById("wnd[1]/usr/lbl[6,6]").text, 9))
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
            Session.findById("wnd[0]/tbar[0]/btn[3]").press()
            Session.findById("wnd[0]/tbar[0]/btn[3]").press()

            Spool_PO = R
        Catch ex As Exception
            Spool_PO = "Error al imprimir. Posible vendor en Sup. Portal."
        End Try

    End Function
    Public Function DownloadTrigger(ByVal Plantas As DataTable) As Boolean
        If Not _Connected Then
            Return False
        End If

        If Plantas.Rows.Count > 0 Then
            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/usr/ctxtI_TABLE").text = "ZMXXMRC1"
            Session.findById("wnd[0]").sendVKey(8)
            Session.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
            Session.findById("wnd[1]/tbar[0]/btn[14]").press()
            Session.findById("wnd[1]/usr/chk[2,5]").selected = True
            Session.findById("wnd[1]/usr/chk[2,6]").selected = True
            Session.findById("wnd[1]/usr/chk[2,6]").setFocus()
            Session.findById("wnd[1]/tbar[0]/btn[71]").press()
            Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
            Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "step"
            Session.findById("wnd[2]/usr/chkSCAN_STRING-START").setFocus()
            Session.findById("wnd[2]/tbar[0]/btn[0]").press()
            Session.findById("wnd[3]/usr/lbl[5,2]").setFocus()
            Session.findById("wnd[3]").sendVKey(2)
            Session.findById("wnd[1]/usr/chk[2,0]").selected = True
            Session.findById("wnd[1]/usr/chk[2,0]").setFocus()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
            Session.findById("wnd[0]/usr/ctxtI1-LOW").text = "30000000"
            Session.findById("wnd[0]/usr/ctxtI1-HIGH").text = "39999999"
            Session.findById("wnd[0]/usr/ctxtI2-LOW").setFocus()
            Session.findById("wnd[0]/usr/ctxtI2-LOW").caretPosition = 0
            Session.findById("wnd[0]/usr/btn%_I2_%_APP_%-VALU_PUSH").press()

            Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = Plantas.Rows(0).Item("code")

            Dim I As Integer
            For I = 1 To Plantas.Rows.Count - 1
                Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,1]").text = Plantas.Rows(I).Item("code")
                Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE").verticalScrollbar.position = I
            Next
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            'Choose fields
            Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
            Session.findById("wnd[1]/tbar[0]/btn[14]").press()
            Session.findById("wnd[1]/usr/chk[1,4]").selected = True
            Session.findById("wnd[1]/usr/chk[1,5]").selected = True
            Session.findById("wnd[1]/usr/chk[1,10]").selected = True
            Session.findById("wnd[1]/usr/chk[1,11]").selected = True
            Session.findById("wnd[1]/usr/chk[1,12]").selected = True
            Session.findById("wnd[1]/usr/chk[1,13]").selected = True
            Session.findById("wnd[1]/usr/chk[1,14]").selected = True
            Session.findById("wnd[1]/usr/chk[1,14]").setFocus()
            Session.findById("wnd[1]/tbar[0]/btn[71]").press()
            Session.findById("wnd[2]/usr/chkSCAN_STRING-START").selected = False
            Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "manu"
            Session.findById("wnd[2]/usr/txtRSYSF-STRING").caretPosition = 4
            Session.findById("wnd[2]/tbar[0]/btn[0]").press()
            Session.findById("wnd[3]/usr/lbl[3,2]").setFocus()
            Session.findById("wnd[3]/usr/lbl[3,2]").caretPosition = 2
            Session.findById("wnd[3]").sendVKey(2)
            Session.findById("wnd[1]/usr/chk[1,3]").selected = True
            Session.findById("wnd[1]/usr/chk[1,4]").selected = True
            Session.findById("wnd[1]/usr/chk[1,5]").selected = True
            Session.findById("wnd[1]/usr/chk[1,5]").setFocus()
            Session.findById("wnd[1]/tbar[0]/btn[6]").press()

            Session.findById("wnd[0]/usr/txtI3-LOW").text = "2"
            Session.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
            Session.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Not Me.SaveSAPFile("Trigger.txt") Then
                MsgBox("Error al guardar el archivo.", MsgBoxStyle.Critical)
                Return False
            Else
                Return True
            End If

        Else
            MsgBox("No se pasaron por parámetro las plantas.", MsgBoxStyle.Critical)
            Return False
        End If


    End Function
    Public Function PrepararTriggerForm() As Boolean
        If _Connected Then
            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nztdb"
            Session.findById("wnd[0]").sendVKey(0)

            Session.findById("wnd[0]/usr/chkP_STEP1").selected = False
            Session.findById("wnd[0]/usr/chkP_STEP3").selected = False
            Session.findById("wnd[0]/usr/ctxtS_WERKS-LOW").text = "2930"
            Session.findById("wnd[0]/usr/chkP_STEP3").setFocus()
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()
            If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
                Return False
            Else
                Return True
            End If
        End If
    End Function
    ''' <summary>
    ''' Actualiza los valores del material en la master data
    ''' </summary>
    ''' <param name="Material">Objeto Material</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UploadValuesOnTrigger(ByRef Material As Object, ByVal lsUser As String) As Boolean
        'Sube los valores de los materiales a Trigger
        Dim i As Integer = 0
        Dim Gica As Double
        Dim Planta$

        'Busca el material en la pantalla de Trigger
        Do While True

            If Not Double.TryParse(Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-MATNR[5," & i & "]").text, Gica) Then
                Return False
            End If

            Planta = Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-WERKS[4," & i & "]").text
            If ((Material.Gica = Gica) And (Material.Planta = Planta)) Then
                Exit Do
            End If

            'Verifica si no se encontró en matrial
            If Planta.Trim.Length = 0 Then
                MsgBox("No se encontrado el material en SAP. Por favor verifique si material: " & Material.Gica & " " & Material.Material, MsgBoxStyle.Critical)
                Return False
            End If
            i = i + 1
        Loop

        'Colocación de los nuevos datos en la pantalla de Trigger
        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-MWSKZ[32," & i & "]").text = Material.TaxCode
        Session.findById("wnd[0]").sendVKey(0)
        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
            Material.ErrorMessage = Trim(Session.findById("wnd[0]/sbar").Text)
            Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-MWSKZ[32," & i & "]").text = ""
            Session.findById("wnd[0]").sendVKey(0)
            Return False
        End If

        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-EVRTN[37," & i & "]").text = Material.Contrato
        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-LIFNR[35," & i & "]").text = Material.Vendor
        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/txtZMXXMRC1-NETPR[40," & i & "]").text = Material.Precio
        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/txtZMXXMRC1-PLIFZ[44," & i & "]").text = Material.PDT
        Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/ctxtZMXXMRC1-ZPCONTACT[28," & i & "]").text = lsUser

        If Material.New_Contrato Then
            Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/chkZMXXMRC1-ZNWCONTR[36," & i & "]").selected = True
        End If

        If Material.AutoPO Then
            Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/chkZMXXMRC1-KAUTB[33," & i & "]").selected = True
        End If

        If Material.S_List Then
            Session.findById("wnd[0]/usr/tblSAPMZMXXTDBTC_0100/chkZMXXMRC1-KORDB[34," & i & "]").selected = True
        End If

        Session.findById("wnd[0]").sendVKey(0)

        If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
            Return False
        Else
            Return True
        End If
    End Function
    ''' <summary>
    ''' Cambia la validez de un contrato
    ''' </summary>
    ''' <param name="Contrato">Número del contrato al que se le modificará la validez</param>
    ''' <param name="Inicio">Nueva fecha de inicio del contrato</param>
    ''' <param name="Fin">Nueva fecha final de la validez del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FixValidityOfOA(ByVal Contrato$, ByVal Inicio$, ByVal Fin$) As Boolean
        If _Connected Then
            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme32k"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/usr/ctxtRM06E-EVRTN").text = Contrato
            Session.findById("wnd[0]/usr/ctxtRM06E-EVRTN").caretPosition = 10
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/tbar[1]/btn[6]").press()
            Session.findById("wnd[0]/usr/ctxtEKKO-KDATB").text = Inicio
            Session.findById("wnd[0]/usr/ctxtEKKO-KDATE").text = Fin
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/tbar[0]/btn[11]").press()

            If Not Session.findById("wnd[1]/usr/btnSPOP-OPTION1", False) Is Nothing Then
                Session.findById("wnd[1]/usr/btnSPOP-OPTION1").SetFocus()
                Session.findById("wnd[1]/usr/btnSPOP-OPTION1").press()
            End If
            FixValidityOfOA = True
        Else
            FixValidityOfOA = False
        End If
    End Function
    ''' <summary>
    ''' Crea el Source List del contrato material por material
    ''' </summary>
    ''' <param name="Gica">Número de Gica</param>
    ''' <param name="Planta">Código de Planta</param>
    ''' <param name="Inicio">Fecha de Incio para el source list[Debe ser igual al del contrato</param>
    ''' <param name="Fin">Fecha de Final para el source list[Debe ser igual al del contrato</param>
    ''' <param name="Vendor">Código del proveedor</param>
    ''' <param name="PurchOrg">Purchasing group del contrato</param>
    ''' <param name="OA">Número del contrato</param>
    ''' <param name="Item">Line Item del contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FixSourceListOA(ByVal Gica As Double, ByVal Planta$, ByVal Inicio As Date, ByVal Fin As Date, ByVal Vendor As Double, ByVal PurchOrg As String, ByVal OA As Double, ByVal Item As Integer) As Boolean

        If _Connected Then
            Session.findById("wnd[0]").resizeWorkingPane(119, 28, False)
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme01"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/usr/ctxtEORD-MATNR").text = Gica
            Session.findById("wnd[0]/usr/ctxtEORD-WERKS").text = Planta
            Session.findById("wnd[0]/usr/ctxtEORD-WERKS").setFocus()
            Session.findById("wnd[0]/usr/ctxtEORD-WERKS").caretPosition = 0
            Session.findById("wnd[0]").sendVKey(0)

            If Trim(Session.findById("wnd[0]/sbar").Text) <> "" Then
                Return False
            End If

            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-VDATU[0,0]").text = Inicio
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-BDATU[1,0]").text = Fin
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-LIFNR[2,0]").text = Vendor
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EKORG[3,0]").text = PurchOrg
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EBELN[6,0]").text = OA
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-EBELP[7,0]").text = Item
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").text = "1"
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").setFocus()
            Session.findById("wnd[0]/usr/tblSAPLMEORTC_0205/ctxtEORD-AUTET[10,0]").caretPosition = 1
            Session.findById("wnd[0]/tbar[0]/btn[11]").press()
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]").sendVKey(0)

            FixSourceListOA = True
        Else
            FixSourceListOA = False
        End If
    End Function

    ''' <summary>
    ''' Salva la información de ingresada en trigger
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveTriggerData() As Boolean
        Session.findById("wnd[0]/tbar[0]/btn[11]").press()
    End Function

    ''' <summary>
    ''' Download the header of outlineagreement using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox">SAP Box</param>
    ''' <param name="UserID">User T Number</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadHeaderOA(ByVal SAPBox As String, ByVal UserID As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.EKKO_Report(SAPBox, UserID, AppId)

        Rep.DocumentFrom = "460000000"
        Rep.DocumentTo = "469999999"
        Rep.DeletionIndicator = False
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data

                'Remove columns from HeaderOAReport
                If Table.Columns.IndexOf("Company Code") <> -1 Then
                    Table.Columns.Remove("Company Code")
                End If

                If Table.Columns.IndexOf("Doc Type") <> -1 Then
                    Table.Columns.Remove("Doc Type")
                End If

                If Table.Columns.IndexOf("Company Code") <> -1 Then
                    Table.Columns.Remove("Company Code")
                End If

                If Table.Columns.IndexOf("Your Ref") <> -1 Then
                    Table.Columns.Remove("Your Ref")
                End If

                If Table.Columns.IndexOf("Salesperson") <> -1 Then
                    Table.Columns.Remove("Salesperson")
                End If

                If Table.Columns.IndexOf("Telephone") <> -1 Then
                    Table.Columns.Remove("Telephone")
                End If

                If Table.Columns.IndexOf("OA Number") <> -1 Then
                    Table.Columns.Remove("OA Number")
                End If

                If Table.Columns.IndexOf("Our Ref") <> -1 Then
                    Table.Columns.Remove("Our Ref")
                End If

            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the detail of outlineagreement using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox"> SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadDetailOA(ByVal SAPBox As String, ByVal UserID As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.EKPO_Report(SAPBox, UserID, AppId)

        Rep.DocumentFrom = "460000000"
        Rep.DocumentTo = "469999999"
        Rep.DeletionIndicator = False
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data

                If Table.Columns.IndexOf("Quantity") <> -1 Then
                    Table.Columns.Remove("Quantity")
                End If

                If Table.Columns.IndexOf("Mat Group") <> -1 Then
                    Table.Columns.Remove("Mat Group")
                End If

                If Table.Columns.IndexOf("Tracking Fld") <> -1 Then
                    Table.Columns.Remove("Tracking Fld")
                End If

                If Table.Columns.IndexOf("Price Unit") <> -1 Then
                    Table.Columns.Remove("Price Unit")
                End If
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Return the detail of Purchases Orders by reation date
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadPO(ByVal SAPBox As String, ByVal UserID As String, ByVal DateFrom As String, ByVal DateTo As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.POs_Report(SAPBox, UserID, AppId)

        Rep.IncludeDocsDatedFromTo(DateFrom, DateTo)
        Rep.ExcludeMaterial("")
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the master data information by Plant code using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox"></param>
    ''' <param name="UserID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadMD(ByVal SAPBox As String, ByVal UserID As String, ByVal Plants As DataTable) As DataTable
        Dim Table As New DataTable

        Dim Rep As New SAPCOM.MARC_Report(SAPBox, UserID, AppId)
        Dim Row As DataRow

        For Each Row In Plants.Rows
            Rep.IncludePlant(Row("Code"))
        Next

        Rep.Execute()
        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download Vendor information using PSL Connector
    ''' </summary>
    ''' <param name="SAPBox">SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadVendors(ByVal SAPBox As String, ByVal UserID As String)
        Dim Table As New DataTable

        Dim Rep As New SAPCOM.LFA1_Report(SAPBox, UserID, AppId)

        Rep.Execute()
        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data
            End If
        End If

        Return Table
    End Function

    Public Function DownloadEBAN(ByVal SAPBox As String, ByVal UserID As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.OpenReqs_Report(SAPBox, UserID, AppId)

        'Rep.IncludeMaterialFromTo("30000000", "39999999")
        Rep.RepairsLevel = IncludeRepairs
        Rep.Execute()
        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the header of Purchases Order from EKKO
    ''' </summary>
    ''' <param name="SAPBox">SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <param name="DateFrom">Created From</param>
    ''' <param name="DateTo">Created to</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadHeaderPO(ByVal SAPBox As String, ByVal UserID As String, ByVal DateFrom As String, ByVal DateTo As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.EKKO_Report(SAPBox, UserID, AppId)

        Rep.DocumentFrom = "450000000"
        Rep.DocumentTo = "459999999"
        Rep.DocDateFrom = DateFrom
        Rep.DocDateTo = DateTo

        Rep.DeletionIndicator = False
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data

                'Remove columns from HeaderOAReport
                If Table.Columns.IndexOf("Doc Type") <> -1 Then
                    Table.Columns.Remove("Doc Type")
                End If

                If Table.Columns.IndexOf("Validity Start") <> -1 Then
                    Table.Columns.Remove("Validity Start")
                End If

                If Table.Columns.IndexOf("Validity End") <> -1 Then
                    Table.Columns.Remove("Validity End")
                End If

                If Table.Columns.IndexOf("Your Ref") <> -1 Then
                    Table.Columns.Remove("Your Ref")
                End If

                If Table.Columns.IndexOf("Salesperson") <> -1 Then
                    Table.Columns.Remove("Salesperson")
                End If

                If Table.Columns.IndexOf("Telephone") <> -1 Then
                    Table.Columns.Remove("Telephone")
                End If

                If Table.Columns.IndexOf("OA Number") <> -1 Then
                    Table.Columns.Remove("OA Number")
                End If

                If Table.Columns.IndexOf("Our Ref") <> -1 Then
                    Table.Columns.Remove("Our Ref")
                End If

                If Table.Columns.IndexOf("Doc Date") <> -1 Then
                    Table.Columns.Remove("Doc Date")
                End If

            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the detail of purchase Order using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox"> SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <param name="Plant">Plant Code</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadDetailPO(ByVal SAPBox As String, ByVal UserID As String, ByVal Plant As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.EKPO_Report(SAPBox, UserID, AppId)

        Rep.DocumentFrom = "450000000"
        Rep.DocumentTo = "459999999"
        Rep.IncludePlant(Plant)
        Rep.MaterialTo = "39999999"
        Rep.MaterialFrom = "30000000"

        Rep.DeletionIndicator = False
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data


                If Table.Columns.IndexOf("Mat Group") <> -1 Then
                    Table.Columns.Remove("Mat Group")
                End If

                If Table.Columns.IndexOf("Tracking Fld") <> -1 Then
                    Table.Columns.Remove("Tracking Fld")
                End If

                If Table.Columns.IndexOf("Price Unit") <> -1 Then
                    Table.Columns.Remove("Price Unit")
                End If

                If Table.Columns.IndexOf("Inforecord") <> -1 Then
                    Table.Columns.Remove("Inforecord")
                End If

                If Table.Columns.IndexOf("PDT") <> -1 Then
                    Table.Columns.Remove("PDT")
                End If
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the detail of Manufacters using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox"> SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <param name="Plant">Plant Code</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadManufacter(ByVal SAPBox As String, ByVal UserID As String, ByVal Plant As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.Manufacturers_Report(SAPBox, UserID, AppId)

        Rep.IncludePlant(Plant)
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data


                If Table.Columns.IndexOf("Client") <> -1 Then
                    Table.Columns.Remove("Client")
                End If
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Download the detail of OTD using the PSL Connector
    ''' </summary>
    ''' <param name="SAPBox"> SAP Box</param>
    ''' <param name="UserID">User ID</param>
    ''' <param name="Plant">Plant Code</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DownloadOTD(ByVal SAPBox As String, ByVal UserID As String, ByVal Plant As String) As DataTable
        Dim Table As New DataTable
        Dim Rep As New SAPCOM.OTD_Report(SAPBox, UserID, AppId)

        Rep.IncludePlant(Plant)
        Rep.Execute()

        If Rep.Success Then
            If Rep.ErrMessage = Nothing Then
                Table = Rep.Data
                If Table.Columns.IndexOf("Client") <> -1 Then
                    Table.Columns.Remove("Client")
                End If
            End If
        End If

        Return Table
    End Function

    ''' <summary>
    ''' Get the costcenter of the item
    ''' </summary>
    ''' <param name="PO">PO Number</param>
    ''' <param name="Item">Item number</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostCenter(ByVal PO$, ByVal Item$) As String
        If Not _Connected Then
            Return "No SAP Connection"
        End If

        Session.findById("wnd[0]").maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme23n"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/tbar[1]/btn[17]").press()
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = PO
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").caretPosition = 10
        Session.findById("wnd[1]").sendVKey(0)

        Session.findById("wnd[0]").sendVKey(28)

        Session.findById("wnd[0]").sendVKey(26) 'Expand Header
        Session.findById("wnd[0]").sendVKey(28) 'Expand Details
        Session.findById("wnd[0]").sendVKey(27) 'Expand Overview

        If Not Find_Item(Session, Item) Then
            MsgBox("El ítem " & Item & " no se ha encontrado.", MsgBoxStyle.Information)
            'Return "Linea no encontrada"
        End If

        Session.findbyid("wnd[0]/usr").findbynameex("DETAIL", 40).press()

        'Seleccionar el tab de Account Assignment
        Session.findbyid("wnd[0]/usr").findbynameex("TABIDT12", 91).select()
        'MySession.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0010/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12").select()

        If Not Session.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12/ssubTABSTRIPCONTROL1SUB:SAPLMEVIEWS:1101/subSUB2:SAPLMEACCTVI:0100/subSUB1:SAPLMEACCTVI:1100/subKONTBLOCK:SAPLKACB:1101/ctxtCOBL-KOSTL", False) Is Nothing Then
            Return Session.findById("wnd[0]/usr/subSUB0:SAPLMEGUI:0019/subSUB3:SAPLMEVIEWS:1100/subSUB2:SAPLMEVIEWS:1200/subSUB1:SAPLMEGUI:1301/subSUB2:SAPLMEGUI:1303/tabsITEM_DETAIL/tabpTABIDT12/ssubTABSTRIPCONTROL1SUB:SAPLMEVIEWS:1101/subSUB2:SAPLMEACCTVI:0100/subSUB1:SAPLMEACCTVI:1100/subKONTBLOCK:SAPLKACB:1101/ctxtCOBL-KOSTL").text()
        Else
            Return "Verificar"
        End If

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

    Public Function UpdateCatalogos(ByVal PO As Object, ByVal Item As Object, ByVal Tax As Object, ByVal Jurisd_Code As Object, ByVal Mat_Usage As Object, ByVal Mat_Origen As Object, ByVal NCM_Code As Object, ByVal Mat_Category As Object) As String

        If Not _Connected Then
            Return "No SAP Connection"
        End If

        Session.findById("wnd[0]").maximize()
        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nme22n"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/tbar[1]/btn[17]").press()
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").text = PO
        Session.findById("wnd[1]/usr/subSUB0:SAPLMEGUI:0003/ctxtMEPO_SELECT-EBELN").caretPosition = 10
        Session.findById("wnd[1]").sendVKey(0)

        If Trim(Left(Session.findById("wnd[0]/sbar").Text, 7)) = "E: User" Or Trim(Left(Session.findById("wnd[0]/sbar").Text, 8)) = "E: Docum" Then
            Return Session.findById("wnd[0]/sbar").Text
        End If


        Session.findById("wnd[0]").sendVKey(28)

        Session.findById("wnd[0]").sendVKey(26) 'Expand Header
        Session.findById("wnd[0]").sendVKey(28) 'Expand Details
        Session.findById("wnd[0]").sendVKey(27) 'Expand Overview

        If Not Find_Item(Session, Item) Then
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

        'Verfico que la pantalla no se encuentre bloqueada
        If Not Session.findbyid("wnd[0]/usr").findbynameex("LOCK", 40).changeable Then
            Session.findbyid("wnd[0]/tbar[1]").findbynameex("btn[7]", 40).press()
        End If


        Session.findbyid("wnd[0]/usr").findbynameex("DETAIL", 40).press()
        Session.findbyid("wnd[0]/usr").findbynameex("TABIDT7", 91).select()

        If Not DBNull.Value.Equals(Tax) Then 'Verifica si el hay información en el tax code
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1317-MWSKZ", 32).Text = Tax.ToString.Trim          'Tax Code
        End If
        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        If Not DBNull.Value.Equals(Jurisd_Code) Then 'Verifica si hay informacion en la variable
            'If Jurisd_Code.Trim.Length > 0 Then
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1317-TXJCD", 32).Text = Jurisd_Code.ToString.Trim 'Jurisd. Code
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        Session.findbyid("wnd[0]/usr").findbynameex("TABIDT11", 91).select()

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Usage) Then
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BMATUSE", 32).Text = Mat_Usage.ToString.Trim  'Material Usage
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Origen) Then
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BMATORG", 32).Text = Mat_Origen.ToString.Trim  'Material Origin
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(NCM_Code) Then
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BNBM", 32).Text = NCM_Code.ToString.Trim     'NCM Code
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If

        If Not DBNull.Value.Equals(Mat_Category) Then
            Session.findbyid("wnd[0]/usr").findbynameex("MEPO1326-J_1BINDUST", 32).Text = Mat_Category.ToString.Trim  'Mat Category
        End If

        'MySession.findById("wnd[0]").sendVKey(0)
        'If MySession.findById("wnd[0]/sbar").Text <> "" Then
        '    MsgBox(MySession.findById("wnd[0]/sbar").Text)
        '    Return MySession.findById("wnd[0]/sbar").Text
        '    Exit Function
        'End If


        Session.findById("wnd[0]/tbar[0]/btn[11]").press()

        If Not Session.findById("wnd[1]/tbar[0]/btn[0]", False) Is Nothing Then
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()
            'Return MySession.findById("wnd[0]/sbar").Text
        End If

        If Not Session.findById("wnd[1]/usr/btnSPOP-VAROPTION1", False) Is Nothing Then
            Session.findById("wnd[1]/usr/btnSPOP-VAROPTION1").press()
            'Return MySession.findById("wnd[0]/sbar").Text
        End If


        If Trim(Session.findById("wnd[0]/sbar").Text) = "" Then
            Return "Please check the changes."
        Else
            Return Session.findById("wnd[0]/sbar").Text
        End If

    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Application = Nothing
                Connection = Nothing
                Session = Nothing
                GC.Collect()
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    Public Function Check_SAP_Config() As Boolean

        'Getting Windows Configuration
        Dim DateSep As String = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator
        Dim DateFormat = Split(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, DateSep)
        Dim DecimalSep As String = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        Dim lsMessage As String = ""

        'Getting SAP configuration:

        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nsu3"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/usr/tabsTABSTRIP1/tabpDEFA").select()

        Dim SAPNumeric As String = (Session.findbyid("wnd[0]/usr").findbynameex("USDEFAULTS-DCPFM", 34).text)
        Dim SAPDateFormat = Split(Session.findbyid("wnd[0]/usr").findbynameex("USDEFAULTS-DATFM", 34).text, "/")

        'Check Windows date format:
        If DateSep <> "/" Then
            lsMessage = " - Wrong Windows date separator." & Chr(13)
        End If
        If Left((DateFormat(0).ToString.ToUpper), 1) <> "M" Then
            lsMessage = lsMessage & " - Wrong Windows date format." & Chr(13)
        End If

        'Check SAP date format:
        If SAPDateFormat(0).ToString.Length > 2 Then
            'if delimeter <> "/" then have more then 2 characters
            lsMessage = lsMessage & " - Wrong SAP date separator." & Chr(13)
        Else
            If Left((SAPDateFormat(0).ToString.ToUpper), 1) <> "M" Then
                lsMessage = lsMessage & " - Wrong SAP date format." & Chr(13)
            End If
        End If

        'Check numeric format
        If DecimalSep <> "." Then
            lsMessage = lsMessage & " - Wrong Windows decimal separator." & Chr(13)
        End If
        If Left(Right(SAPNumeric.Trim, 3), 1) <> "." Then
            lsMessage = lsMessage & " - Wrong SAP decimal separator." & Chr(13)
        End If

        If lsMessage.Trim.Length > 0 Then
            MsgBox("Wrong configration was found:" & Chr(13) & Chr(13) & lsMessage & Chr(13) & Chr(13) & "Please set the correct configuration before working with this tool.", MsgBoxStyle.Exclamation, "Warning!!!")
        End If

    End Function

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "DownloadFZ12"
    Public Function DownloadZFI2(ByVal Variante As String, ByVal TNumber As String) As DataTable
        If Not _Connected Then
            Return Nothing
        End If

        Dim T As DataTable
        Dim SF As New OAConnection.Connection

        Try
            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzfi2"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante
            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber
            Session.findById("wnd[1]/usr/txtENAME-LOW").setFocus()
            Session.findById("wnd[1]/usr/txtENAME-LOW").caretPosition = 6
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "%PC"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

            Session.findById("wnd[1]/usr/ctxtDY_PATH").text = _lsDirectory
            Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "ZFI2.xls"

            If Len(Dir(_lsDirectory & "ZFI2.xls")) > 0 Then
                Kill(_lsDirectory & "ZFI2.xls")
            End If

            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

            T = SF.TextFile_Data(_lsDirectory & "\ZFI2.xls")

            Return T


        Catch ex As Exception
            Return Nothing
        End Try

    End Function
#End Region

#Region "DownLoad ZMRO"
    Public Function DownloadZMR0(ByVal Variante As String, ByVal TNumber As String, ByVal Path As String) As Boolean
        If Not _Connected Then
            Return False
        End If
        Try
            Session.findById("wnd[0]").maximize()
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzmr0"
            Session.findById("wnd[0]").sendVKey(0)
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim
            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            Session.findById("wnd[1]/usr/txtV-LOW").caretPosition = 14
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            If Dir(Path).Length > 0 Then
                Kill(Path)
            End If

            Session.findById("wnd[0]/usr/txtP_FILE").text = Path


            'If Dir(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\ZMR0.xls").Length > 0 Then
            '    Kill(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\ZMR0.xls")
            'End If
            'Session.findById("wnd[0]/usr/txtP_FILE").text = My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData & "\ZMR0.xls"

            Session.findById("wnd[0]/usr/txtP_FILE").setFocus()
            Session.findById("wnd[0]/usr/txtP_FILE").caretPosition = 7
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            'Session.findById("wnd[0]/sbar").text()

            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("ERROR") = -1 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

    Public Function Get_SAP_IROnly(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable
        'Dim Path As String = ""

        'Path = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\Unblock_" & Variante & ".xls"

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000448"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()


            If Box = "L6P" Then

                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").pressToolbarButton("&FIND")
                Session.findById("wnd[2]/usr/chkGS_SEARCH-EXACT_WORD").selected = True
                Session.findById("wnd[2]/usr/txtGS_SEARCH-VALUE").text = Variante
                Session.findById("wnd[2]/usr/cmbGS_SEARCH-SEARCH_ORDER").key = "0"
                Session.findById("wnd[2]/usr/chkGS_SEARCH-EXACT_WORD").setFocus()
                Session.findById("wnd[2]/tbar[0]/btn[0]").press()
                Session.findById("wnd[2]/tbar[0]/btn[12]").press()
                Session.findById("wnd[1]").sendVKey(2)
                Session.findById("wnd[2]/tbar[0]/btn[0]").press()
                Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").selectedRows = Session.findById("wnd[1]/usr/cntlALV_CONTAINER_1/shellcont/shell").CurrentCellRow
                Session.findById("wnd[1]").sendVKey(2)
            Else
                'No se brindaron nombres para las variantes
                Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

                Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
                Session.findById("wnd[1]/tbar[0]/btn[8]").press()
            End If

            Session.findbyid("wnd[0]/usr").findbynameex("S_AEDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_AEDAT-HIGH", 32).Text = DateTo.Date

            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Dir(Path & "IROnly_" & Variante & "_" & Box & ".xls").Length > 0 Then
                System.IO.File.Delete(Path & "IROnly_" & Variante & "_" & Box & ".xls")
            End If

            Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
            Session.findById("wnd[0]").sendVKey(0)

            Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

            Session.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
            Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "IROnly_" & Variante & "_" & Box & ".xls"
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()


            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSMITTED") > -1 Then
                'Verify if file was created:
                If Dir(Path & "IROnly_" & Variante & "_" & Box & ".xls").Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_IROnly_File(Path & "IROnly_" & Variante & "_" & Box & ".xls", Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Get_SAP_PO_Without_Reference(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable
        'Dim Path As String = ""

        'Path = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\Unblock_" & Variante & ".xls"

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        Path = Path & "PO_WO_Ref_" & Variante & "_" & Box & ".xls"

        If Dir(Path).Length > 0 Then
            System.IO.File.Delete(Path)
        End If
        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000449"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()

            'No se brindaron nombres para las variantes
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            Session.findbyid("wnd[0]/usr").findbynameex("S_BEDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_BEDAT-HIGH", 32).Text = DateTo.Date

            Session.findbyid("wnd[0]/usr").findbynameex("P_XLFILE", 31).Text = Path

            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            ' Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSMITTED") > -1 Then
                'Verify if file was created:
                If Dir(Path).Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_PO_Without_Ref_File(Path, Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function Get_SAP_PO_Changes_Report(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        Path = Path & "PO_Changes_" & Variante & "_" & Box & ".xls"

        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000445"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()

            'No se brindaron nombres para las variantes
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            'Session.findById("wnd[1]/usr/txtV-LOW").caretPosition = 14
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            Session.findbyid("wnd[0]/usr").findbynameex("S_CHGDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_CHGDAT-HIGH", 32).Text = DateTo.Date

            Session.findById("wnd[0]/tbar[1]/btn[8]").press()
            Session.findById("wnd[0]/tbar[1]/btn[5]").press()


            If Dir(Path).Length > 0 Then
                System.IO.File.Delete(Path)
            End If

            Session.findbyid("wnd[1]/usr").findbynameex("RLGRAP-FILENAME", 32).Text = Path
            Session.findbyid("wnd[1]/usr").findbynameex("RLGRAP-FILETYPE", 32).text = "DAT"
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()


            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSFERRED") > -1 Then
                'Verify if file was created:

                If Dir(Path).Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_PO_Change_File(Path, Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function Get_SAP_PO_After_Invoice_Report(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        'Path = Path & "PO_AI_" & Variante & "_" & Box & ".xls"

        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000450"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()

            'No se brindaron nombres para las variantes
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            'Session.findById("wnd[1]/usr/txtV-LOW").caretPosition = 14
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            Session.findbyid("wnd[0]/usr").findbynameex("S_AEDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_AEDAT-HIGH", 32).Text = DateTo.Date

            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Dir(Path & "PO_AI_" & Variante & "_" & Box & ".xls").Length > 0 Then
                System.IO.File.Delete(Path & "PO_AI_" & Variante & "_" & Box & ".xls")
            End If


            Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
            Session.findById("wnd[0]").sendVKey(0)


            Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

            Session.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
            Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "PO_AI_" & Variante & "_" & Box & ".xls"
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()


            'Session.findbyid("wnd[0]/usr").findbynameex("P_FILE", 32).Text = Path
            'Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSMITTED") > -1 Then
                'Verify if file was created:
                If Dir(Path).Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_PO_AI_File(Path & "PO_AI_" & Variante & "_" & Box & ".xls", Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function Get_SAP_Park_Credit_Report(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal FiscalFrom As String, Optional ByVal FiscalTo As String = "", Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If


        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000030"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()

            'No se brindaron nombres para las variantes
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            'Session.findById("wnd[1]/usr/txtV-LOW").caretPosition = 14
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            Session.findbyid("wnd[0]/usr").findbynameex("S_BLDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_BLDAT-HIGH", 32).Text = DateTo.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_GJAHR-LOW", 31).Text = FiscalFrom
            Session.findbyid("wnd[0]/usr").findbynameex("S_GJAHR-HIGH", 31).Text = FiscalTo

            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Dir(Path & "Park_Credit_" & Variante & "_" & Box & ".xls").Length > 0 Then
                System.IO.File.Delete(Path & "Park_Credit_" & Variante & "_" & Box & ".xls")
            End If

            Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
            Session.findById("wnd[0]").sendVKey(0)


            Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()

            Session.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
            Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "Park_Credit_" & Variante & "_" & Box & ".xls"
            Session.findById("wnd[1]/tbar[0]/btn[0]").press()


            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSMITTED") > -1 Then
                'Verify if file was created:
                If Dir(Path).Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_Park_Credit_File(Path & "Park_Credit_" & Variante & "_" & Box & ".xls", Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If


        Catch ex As Exception
            Return Nothing

        End Try
    End Function

    Public Function Get_GS_Value_From_EKPO(ByVal Box As String, ByVal Data As DataTable, ByVal Path As String) As DataTable
        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        If Dir(Path & "EKPO_" & Box & ".xls").Length > 0 Then
            System.IO.File.Delete(Path & "EKPO_" & Box & ".xls")
        End If

        Session.findById("wnd[0]/tbar[0]/okcd").text = "/nzse16"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[0]/usr/ctxtI_TABLE").text = "EKPO"
        Session.findById("wnd[0]/usr/ctxtI_TABLE").caretPosition = 4
        Session.findById("wnd[0]").sendVKey(8)

        'Dim cn As New OAConnection.Connection
        'cn.Put_DataTable_In_ClipBoard(Data)

        Session.findById("wnd[0]/mbar/menu[3]/menu[0]/menu[1]").select()
        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        Session.findById("wnd[1]/usr/chk[1,4]").selected = True
        Session.findById("wnd[1]/usr/chk[1,5]").selected = True
        Session.findById("wnd[1]/usr/chk[1,5]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[71]").press()
        Session.findById("wnd[2]/usr/txtRSYSF-STRING").text = "gross"
        Session.findById("wnd[2]/tbar[0]/btn[0]").press()
        Session.findById("wnd[3]/usr/lbl[37,2]").setFocus()
        Session.findById("wnd[3]/usr/lbl[37,2]").caretPosition = 3
        Session.findById("wnd[3]").sendVKey(2)
        Session.findById("wnd[1]/usr/chk[1,3]").selected = True
        Session.findById("wnd[1]/usr/chk[1,3]").setFocus()
        Session.findById("wnd[1]/tbar[0]/btn[6]").press()
        Session.findById("wnd[0]/mbar/menu[3]/menu[2]").select()
        Session.findById("wnd[1]/tbar[0]/btn[14]").press()
        Session.findById("wnd[1]/usr/chk[2,5]").selected = True
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        Session.findById("wnd[0]/usr/btn%_I1_%_APP_%-VALU_PUSH").press()

        'Ingresar las Ordenes de compra: en variable DATA:

        For Each r As DataRow In Data.Rows
            Session.findById("wnd[1]/tbar[0]/btn[13]").press()
            Session.findById("wnd[1]/usr/tabsTAB_STRIP/tabpSIVA/ssubSCREEN_HEADER:SAPLALDB:3010/tblSAPLALDBSINGLE/ctxtRSCSEL-SLOW_I[1,0]").text = r("Doc Number")
        Next

        Session.findById("wnd[1]/tbar[0]/btn[24]").press()
        Session.findById("wnd[1]/tbar[0]/btn[8]").press()
        Session.findById("wnd[0]/usr/ctxtLIST_BRE").text = "9999"
        Session.findById("wnd[0]/usr/txtMAX_SEL").text = "999999999"
        Session.findById("wnd[0]/usr/txtMAX_SEL").setFocus()
        Session.findById("wnd[0]/usr/txtMAX_SEL").caretPosition = 11
        Session.findById("wnd[0]").sendVKey(8)
        Session.findById("wnd[0]/tbar[0]/okcd").text = "%pc"
        Session.findById("wnd[0]").sendVKey(0)
        Session.findById("wnd[1]/usr/subSUBSCREEN_STEPLOOP:SAPLSPO5:0150/sub:SAPLSPO5:0150/radSPOPLI-SELFLAG[1,0]").select()
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()
        Session.findById("wnd[1]/usr/ctxtDY_PATH").text = Path
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").text = "EKPO_" & Box & ".xls"
        Session.findById("wnd[1]/usr/ctxtDY_FILENAME").caretPosition = 8
        Session.findById("wnd[1]/tbar[0]/btn[0]").press()


        If Dir(Path & "EKPO_" & Box & ".xls").Length > 0 Then
            Dim FileReader As New System.IO.StreamReader(Path & "EKPO_" & Box & ".xls")
            Dim S As String
            Dim W As Array
            Dim D As New DataTable
            Dim DR As DataRow
            Dim V As Double

            D.Columns.Add(New DataColumn("PO", System.Type.GetType("System.String")))
            D.Columns.Add(New DataColumn("Item", System.Type.GetType("System.String")))
            D.Columns.Add(New DataColumn("Value", System.Type.GetType("System.Double")))

            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine
            S = FileReader.ReadLine

            Do
                S = FileReader.ReadLine
                If Not S Is Nothing Then
                    W = Split(S, vbTab)

                    DR = D.NewRow

                    DR("PO") = W(1)
                    DR("Item") = W(3)

                    Double.TryParse(W(5), V)
                    DR("Value") = V

                    D.Rows.Add(DR)
                End If

            Loop Until S = Nothing

            D.AcceptChanges()
            FileReader.Close()
            Return D
        End If


    End Function

    Public Function Get_SAP_Unblocked_Report(ByVal Variante As String, ByVal TNumber As String, ByVal Box As String, ByVal DateFrom As Date, ByVal DateTo As Date, ByVal FiscalFrom As String, Optional ByVal FiscalTo As String = "", Optional ByVal Path As String = "") As DataTable
        Dim Data As New DataTable
        'Dim Path As String = ""

        'Path = My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData & "\Unblock_" & Variante & ".xls"

        If Right(Path, 1) <> "\" Then
            Path = Path & "\"
        End If

        Path = Path & "Unblock_" & Variante & "_" & Box & ".xls"

        Try
            'Start Transaction:
            Session.findById("wnd[0]/tbar[0]/okcd").text = "/nY_KLD_31000447"
            Session.findById("wnd[0]").sendVKey(0)

            'Select user variant:
            Session.findById("wnd[0]/tbar[1]/btn[17]").press()

            'No se brindaron nombres para las variantes
            Session.findById("wnd[1]/usr/txtV-LOW").text = Variante.Trim

            Session.findById("wnd[1]/usr/txtENAME-LOW").text = TNumber.Trim
            'Session.findById("wnd[1]/usr/txtV-LOW").caretPosition = 14
            Session.findById("wnd[1]/tbar[0]/btn[8]").press()

            Session.findbyid("wnd[0]/usr").findbynameex("S_BLDAT-LOW", 32).Text = DateFrom.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_BLDAT-HIGH", 32).Text = DateTo.Date
            Session.findbyid("wnd[0]/usr").findbynameex("S_GJAHR-LOW", 31).Text = FiscalFrom
            Session.findbyid("wnd[0]/usr").findbynameex("S_GJAHR-HIGH", 31).Text = FiscalTo

            If Dir(Path).Length > 0 Then
                System.IO.File.Delete(Path)
            End If

            Session.findbyid("wnd[0]/usr").findbynameex("P_FILE", 32).Text = Path
            Session.findById("wnd[0]/tbar[1]/btn[8]").press()

            If Session.findById("wnd[0]/sbar").text.ToString.ToUpper.IndexOf("TRANSMITTED") > -1 Then
                'Verify if file was created:
                If Dir(Path).Length > 0 Then
                    Dim cn As New OAConnection.Connection

                    Data = cn.Read_SAP_Unblocked_File(Path, Box)

                    If Not Data Is Nothing Then
                        Return Data
                    Else
                        MsgBox("Exception while reading SAP File. [" & Box & "-" & Variante & "]", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                Else
                    Return Nothing
                End If

            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing

        End Try

    End Function

End Class

Public Class SAPGUI
    Private SAPApp As Object = Nothing
    Private Connection As Object = Nothing
    Private Session As Object = Nothing

    Private LI As Boolean = False

    Public Property LoggedIn() As Boolean

        Get
            LoggedIn = LI
        End Get
        Set(ByVal value As Boolean)
            LI = value
        End Set

    End Property

    Public ReadOnly Property StatusBarText() As String
        Get
            StatusBarText = Session.findById("wnd[0]/sbar").text
        End Get
    End Property

    Sub New(ByVal Box As String, ByVal User As String, ByVal Password As String, Optional ByVal pLoginMethod As Boolean = False, Optional ByVal pSAPConfig As Boolean = False)
        Dim CS As String = GetConnString(Box)
        SAPApp = CreateObject("Sapgui.ScriptingCtrl.1")

        If Not pLoginMethod Then
            '*************************************
            ' Metodo de coneccion de Gomez
            '*************************************
            Try
                Connection = SAPApp.OpenConnection(CS)
                Session = Connection.Children(0)
                Session.findById("wnd[0]/usr/txtRSYST-BNAME").Text = User
                Session.findById("wnd[0]/usr/pwdRSYST-BCODE").Text = Password
                Session.findById("wnd[0]").sendVKey(0)

                Do While Not Session.findById("wnd[1]", False) Is Nothing
                    If Session.ActiveWindow.Text = "SAP" Then
                        Session.findById("wnd[1]/tbar[0]/btn[12]").Press()
                        Exit Sub
                    End If
                    If Not Session.findById("wnd[1]/usr/txtMULTI_LOGON_TEXT", False) Is Nothing Then
                        If Not Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                            Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT2").Select()
                        Else
                            Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT1").Select()
                        End If
                    End If
                    Session.findById("wnd[1]").sendVKey(0)
                Loop
                If Session.ActiveWindow.Text Like "SAP Easy Access*" Then
                    LI = True
                End If

            Catch ex As Exception

            End Try
        Else
            '*****************************************
            ' Metodo de coneccion para el  PSS LA Tool
            '*****************************************
            If Not pSAPConfig Then
                Connection = SAPApp.OpenConnection(CS)
                Session = Connection.Children(0)
                Session.findById("wnd[0]/usr/txtRSYST-BNAME").Text = User
                Session.findById("wnd[0]/usr/pwdRSYST-BCODE").Text = Password
                Session.findById("wnd[0]").sendVKey(0)

                Do While Not Session.findById("wnd[1]", False) Is Nothing
                    If Session.ActiveWindow.Text = "SAP" Then
                        Session.findById("wnd[1]/tbar[0]/btn[12]").Press()
                        Exit Sub
                    End If
                    If Not Session.findById("wnd[1]/usr/txtMULTI_LOGON_TEXT", False) Is Nothing Then
                        If Not Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                            Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT2").Select()
                        Else
                            Session.findById("wnd[1]/usr/radMULTI_LOGON_OPT1").Select()
                        End If
                    End If
                    Session.findById("wnd[1]").sendVKey(0)
                Loop
                If Session.ActiveWindow.Text Like "SAP Easy Access*" Then
                    LI = True
                End If

            Else
                '**********************************
                '        Si usa SSO
                '**********************************
                Try
                    Connection = SAPApp.OpenConnection(CS & " - SSO")
                    Session = Connection.Children(0)

                    If Not Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                        Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").SetFocus()
                        Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").Selected = True
                        Session.findbyId("wnd[1]/tbar[0]/btn[0]").press()
                    End If

                    If Session.ActiveWindow.Text Like "SAP Easy Access*" Then
                        LI = True
                    End If

                Catch ex As Exception
                    If (CS.IndexOf("G4P") >= 0) Or (CS.IndexOf("GBP") >= 0) Then
                        GoTo TryG4PSSO
                    End If

                    MsgBox("Can't get SAP Connection.", MsgBoxStyle.Critical)
                    LI = False
                End Try

            End If
        End If
        Exit Sub


TryG4PSSO:
        Try
            Connection = SAPApp.OpenConnection(CS & "- SSO")
            Session = Connection.Children(0)

            If Not Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2", False) Is Nothing Then
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").SetFocus()
                Session.findbyId("wnd[1]/usr/radMULTI_LOGON_OPT2").Selected = True
                Session.findbyId("wnd[1]/tbar[0]/btn[0]").press()
            End If

            LI = True

        Catch ex As Exception
            MsgBox("Can't get SAP Connection.", MsgBoxStyle.Critical)
            LI = False
        End Try
    End Sub

    Public Sub Close()

        If LI Then
            ActiveWindow.Close()
            If Not FindByNameEx("SPOP-OPTION1", 40) Is Nothing Then
                FindByNameEx("SPOP-OPTION1", 40).press()
            End If
        End If

    End Sub

    Public Function FindById(ByVal ID As String) As Object

        Try
            FindById = Session.findById(ID, False)
        Catch ex As Exception
            FindById = Nothing
        End Try

    End Function

    Public Function FindByNameEx(ByVal Name As String, ByVal Type As Long) As Object

        Try
            FindByNameEx = Session.ActiveWindow.FindByNameEx(Name, Type)
        Catch ex As Exception
            FindByNameEx = Nothing
        End Try

    End Function

    Public Function FindAllByNameEx(ByVal Name As String, ByVal Type As Long) As Object

        Try
            FindAllByNameEx = Session.ActiveWindow.FindAllByNameEx(Name, Type)
        Catch ex As Exception
            FindAllByNameEx = Nothing
        End Try

    End Function

    Public Function ActiveWindow() As Object
        ActiveWindow = Session.ActiveWindow
    End Function

    Public Function GuiFocus() As Object
        GuiFocus = ActiveWindow.GuiFocus
    End Function

    Public Sub SendVKey(ByVal Code As Integer)
        Session.ActiveWindow.SendVKey(Code)
    End Sub

    Public Sub StartTransaction(ByVal Code As String)
        Session.StartTransaction(Code)
    End Sub

    Public Sub SendCommand(ByVal Code As String)
        Session.SendCommand(Code)
    End Sub

    Public Function DisplayPO(ByVal PO As String) As Boolean
        If Not LI Then
            DisplayPO = False
            Exit Function
        End If

        DisplayPO = True
        StartTransaction("me23n")
        FindByNameEx("btn[17]", 40).Press()
        FindByNameEx("MEPO_SELECT-EBELN", 32).Text = PO
        FindByNameEx("btn[0]", 40).Press()
        If StatusBarText <> "" Then
            DisplayPO = False
        End If

    End Function

    Public Function ChangePO(ByVal PO As String) As Boolean

        ChangePO = True
        If Not DisplayPO(PO) Then
            ChangePO = False
        Else
            FindByNameEx("btn[7]", 40).Press()
            If StatusBarText <> "" AndAlso StatusBarText <> "Text contains formatting -> SAPscript editor" Then
                ChangePO = False
            End If
        End If

    End Function

    Public Function GetConnString(ByVal Box As String) As String

        Dim R As String = Nothing
        Select Case Box
            Case "GBP"
                R = "GBP GCM Production"
            Case "G4P"
                R = "G4P GCF/Cons Prod"
            Case "N6P"
                R = "N6P NA Prod"
            Case "N6A"
                R = "N6A NA SC Acc"
            Case "L7P"
                R = "L7P LA TS Prod"
            Case "L6P"
                R = "L6P LA SC  Prod"
            Case "L7A"
                R = "L7A TS Acceptance"

        End Select
        GetConnString = R

    End Function

    Public Sub ArrayToClipboard(ByVal A() As String)

        Dim S As String = Nothing
        Dim C As String = Nothing
        My.Computer.Clipboard.Clear()
        For Each S In A
            C = C & S & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(C)

    End Sub

    Public Sub TableToClipboard(ByVal DT As DataTable, ByVal ColumnIndex As Integer)

        Dim S As String = Nothing
        Dim DR As DataRow

        My.Computer.Clipboard.Clear()
        For Each DR In DT.Rows
            S = S & DR(ColumnIndex) & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(S)

    End Sub

    Public Sub DRArrayToClipboard(ByVal DRA() As DataRow, ByVal ColumnIndex As Object)
        Dim S As String = Nothing
        Dim DR As DataRow

        My.Computer.Clipboard.Clear()
        For Each DR In DRA
            S = S & DR(ColumnIndex) & Chr(13) & Chr(10)
        Next
        My.Computer.Clipboard.SetText(S)
    End Sub

End Class

Public Class SAP_Faxing_Report

    Public Data As DataTable = Nothing
    Private GUI As SAPGUI
    Private FN As String = "Faxing.txt"

    Sub New(ByVal Box As String, ByVal User As String, ByVal Password As String, Optional ByVal Area As String = Nothing)

        GUI = New SAPGUI(Box, User, Password)
        If GUI.LoggedIn Then
            GUI.StartTransaction("Y_KLD_31001497")
            GUI.FindByNameEx("S_TRANS-LOW", 32).Text = ""
            GUI.FindByNameEx("S_WERKS-LOW", 32).Text = "*"
            GUI.FindByNameEx("%_S_EBELN_%_APP_%-VALU_PUSH", 40).press()
            GUI.FindByNameEx("btn[24]", 40).press()
            GUI.FindByNameEx("btn[8]", 40).press()
            GUI.FindByNameEx("btn[8]", 40).press()
            GUI.SendCommand("%pc")
            GUI.FindAllByNameEx("SPOPLI-SELFLAG", 41).Item(1).Select()
            GUI.FindByNameEx("btn[0]", 40).press()
            GUI.FindByNameEx("DY_PATH", 32).Text = My.Computer.FileSystem.SpecialDirectories.Temp
            GUI.FindByNameEx("DY_FILENAME", 32).Text = FN
            GUI.FindByNameEx("btn[11]", 40).press()
        End If
        GUI.Close()

        Data = New DataTable
        Data.Columns.Add("PO", System.Type.GetType("System.String"))
        Data.Columns.Add("Recno", System.Type.GetType("System.String"))
        Data.Columns.Add("Transm_Date", System.Type.GetType("System.DateTime"))
        Data.Columns.Add("Transm_Time", System.Type.GetType("System.String"))
        Data.Columns.Add("Fax", System.Type.GetType("System.String"))
        Data.Columns.Add("Success", System.Type.GetType("System.Boolean"))
        Data.Columns.Add("Message", System.Type.GetType("System.String"))
        If Not Area Is Nothing Then
            Data.Columns.Add("Area", System.Type.GetType("System.String"))
        End If
        Data.Columns.Add("SAP", System.Type.GetType("System.String"))

        Dim FileReader As New System.IO.StreamReader(My.Computer.FileSystem.SpecialDirectories.Temp & "\" & FN)
        Dim S As String
        Dim ExitLoop As Boolean = False
        Dim W As Array
        Dim DR As DataRow

        Do
            S = FileReader.ReadLine
            W = Split(S, Chr(9))
            If W.Length > 0 AndAlso W(0) = " Message#" Then
                S = FileReader.ReadLine
                ExitLoop = True
            End If
        Loop Until ExitLoop

        Do
            S = FileReader.ReadLine
            If Not S Is Nothing Then
                W = Split(S, Chr(9))
                DR = Data.NewRow
                DR("PO") = W(14)
                DR("Recno") = CDbl(W(0)).ToString
                DR("Transm_Date") = CDate(W(9))
                DR("Transm_Time") = W(10)
                DR("Fax") = W(13)
                If W(2) = "Successful" Then
                    DR("Success") = True
                Else
                    DR("Success") = False
                End If
                DR("Message") = W(3)
                If Not Area Is Nothing Then
                    DR("Area") = Area
                End If
                DR("SAP") = Box
                Data.Rows.Add(DR)
            End If
        Loop Until S Is Nothing

    End Sub

End Class
Public Class Refaxer

    Private IDN() As String = Nothing
    Private Box As String
    Private User As String
    Private Password As String

    Private Structure BW_Args
        Public Box As String
        Public User As String
        Public Password As String
        Public IDN() As String
    End Structure

    Sub New(ByVal ABox As String, ByVal AUser As String, ByVal APassword As String)
        Box = ABox
        User = AUser
        Password = APassword

    End Sub

    Public Sub IncludePO(ByVal Number As String)
        If IDN Is Nothing Then
            ReDim IDN(0)
        Else
            ReDim Preserve IDN(UBound(IDN) + 1)
        End If

        IDN(UBound(IDN)) = Number
    End Sub

    Public Sub Execute()
        Dim A As New BW_Args
        A.Box = Box
        A.User = User
        A.Password = Password
        A.IDN = IDN
        FaxScript(A)

    End Sub

    Private Sub FaxScript(ByVal A As BW_Args)
        Dim PO As String
        Dim TT As Object
        Dim I As Integer
        Dim EM As String

        Dim Session As New SAPGUI(A.Box, A.User, A.Password)

        If Session.LoggedIn Then
            For Each PO In A.IDN
                If Session.ChangePO(PO) Then
                    Try
                        Session.FindById("wnd[0]/tbar[1]/btn[21]").Press()
                        TT = Session.FindAllByNameEx("DNAST-KSCHL", 32)
                        I = 0
                        Do While TT.ElementAt(I).Text <> "" And I <= TT.Count - 1
                            If Not Session.FindById("wnd[0]/usr/tblSAPDV70ATC_NAST3/lblDV70A-STATUSICON[0," & I & "]") Is Nothing AndAlso _
                            Session.FindById("wnd[0]/usr/tblSAPDV70ATC_NAST3/lblDV70A-STATUSICON[0," & I & "]").ToolTip = "Not processed" Then
                                Session.FindByNameEx("SAPDV70ATC_NAST3", 80).getAbsoluteRow(I).selected = True
                                Session.FindByNameEx("btn[18]", 40).press()
                                If Not Session.FindById("wnd[1]/tbar[0]/btn[0]") Is Nothing Then
                                    Session.FindById("wnd[1]/tbar[0]/btn[0]").Press()
                                End If
                                TT = Session.FindAllByNameEx("DNAST-KSCHL", 32)
                            Else
                                I += 1
                            End If
                        Loop
                        If I < TT.Count - 1 Then
                            TT.ElementAt(I).Text = "NNXX"
                            Session.FindAllByNameEx("NAST-NACHA", 34).ElementAt(I).Key = "2"
                            Session.FindById("wnd[0]/tbar[1]/btn[2]").Press()
                            Session.FindById("wnd[1]/tbar[0]/btn[2]").Press()
                            If Session.FindById("wnd[1]/usr/txtNAST-TELFX").Text <> "" AndAlso Session.FindById("wnd[1]/usr/ctxtNAST-TLAND").Text = "US" Then
                                Session.FindById("wnd[1]/tbar[0]/btn[0]").Press()
                                Session.FindById("wnd[0]/tbar[1]/btn[5]").Press()
                                Session.FindByNameEx("NAST-VSZTP", 34).Key = "4"
                                Session.FindById("wnd[0]/tbar[0]/btn[3]").Press()
                                Session.FindById("wnd[0]/tbar[0]/btn[11]").Press()  'SAVE BUTTON
                                If Not Session.FindById("wnd[1]/usr/btnSPOP-VAROPTION1") Is Nothing Then
                                    Session.FindById("wnd[1]/usr/btnSPOP-VAROPTION1").Press()
                                End If
                            Else
                                Session.FindById("wnd[1]/tbar[0]/btn[12]").Press()
                            End If

                        End If
                    Catch ex As Exception
                        EM = ex.Message
                    End Try
                End If
            Next
        End If
        Session.Close()

    End Sub

End Class
Public Class BI_Release

    Private BIDT As New DataTable
    Private Session As SAPGUI = Nothing
    Private Box As String
    Private User As String
    Private Password As String
    '***************************************
    Private LoginMethod As Boolean ' Para usar el método de coneccion de Gomez
    Private SAPConfig As Boolean ' Para usar SSO o metodo standard

    Public Event Notify(ByVal Box As String, ByVal IR As String, ByVal Result As String)

    Sub New(ByVal ABox As String, ByVal AUser As String, ByVal APassword As String, Optional ByVal pLoginMethod As Boolean = False, Optional ByVal pSAPConfig As Boolean = False)

        '***********************************************************************
        '  Clase de Gomez para conección
        Box = ABox
        User = AUser
        Password = APassword
        BIDT.Columns.Add("InvoiceNumber", System.Type.GetType("System.String"))
        BIDT.Columns.Add("Vendor", System.Type.GetType("System.String"))
        BIDT.Columns.Add("Manual", System.Type.GetType("System.String"))

        'Columnas Agregadas para LA:
        BIDT.Columns.Add("R Code", System.Type.GetType("System.String"))
        BIDT.Columns.Add("RC TEXT", System.Type.GetType("System.String"))
        '***********************************************************************
        LoginMethod = pLoginMethod
        SAPConfig = pSAPConfig

    End Sub

    Public Sub Include(ByVal IR As String, ByVal Vendor As String, ByVal Manual As String, ByVal RCode As String, ByVal RCText As String)

        Dim DR As DataRow = BIDT.NewRow
        DR("InvoiceNumber") = IR
        DR("Vendor") = Vendor
        DR("Manual") = Manual
        DR("R Code") = RCode
        DR("RC Text") = RCText
        BIDT.Rows.Add(DR)

    End Sub

    Public Sub LoadDataTable(ByVal DT As DataTable)
        Dim Reader As New DataTableReader(DT)
        BIDT.Load(Reader)
    End Sub

    Public Sub Execute()
        If BIDT.Rows.Count <= 0 Then Exit Sub
        Dim BIDR() As DataRow

        Session = New SAPGUI(Box, User, Password, True, SAPConfig)
        'Session = New SAPGUI(Box, User, Password, true, SAPConfig)

        BIDR = BIDT.Select("Manual = ''")
        If BIDR.Length > 0 Then
            Release_BI(BIDR)
        End If

        BIDR = BIDT.Select("Manual = 'X'")
        If BIDR.Length > 0 Then
            Release_D(BIDR)
        End If

        Session.Close()

    End Sub

    Private Sub Release_BI(ByVal BIDR() As DataRow)

        Dim DR As DataRow
        If Session.LoggedIn Then
            Try
                Session.StartTransaction("ZMR0")
                Session.FindByNameEx("S_BUKRS-LOW", 32).Text = "*"
                Session.FindByNameEx("S_GJAHR-LOW", 31).Text = Now.Year + 1
                Session.FindByNameEx("S_GJAHR-LOW", 31).SetFocus()
                Session.SendVKey(2)
                Session.FindByNameEx("shell", 122).SelectedRows = "2"
                Session.FindByNameEx("btn[0]", 40).Press()

                Session.FindByNameEx("P_DIFF", 42).Selected = True
                Session.FindByNameEx("P_INPT", 42).Selected = False
                Session.FindByNameEx("P_COUNT", 42).Selected = False
                Session.FindByNameEx("P_IMAGE", 42).Selected = False

                Session.FindByNameEx("P_SPGRP", 42).Selected = True
                Session.FindByNameEx("P_SPGRM", 42).Selected = False
                Session.FindByNameEx("P_HEAD", 42).Selected = True
                Session.FindByNameEx("P_MANL", 42).Selected = True

                Session.DRArrayToClipboard(BIDR, "Vendor")
                Session.FindByNameEx("%_S_LIFNR_%_APP_%-VALU_PUSH", 40).Press()
                Session.FindByNameEx("btn[24]", 40).Press()
                Session.FindByNameEx("btn[8]", 40).Press()
                Session.FindByNameEx("S_BLART-LOW", 32).Text = "*"
                Session.FindByNameEx("btn[8]", 40).Press()
                Dim Proc As Boolean
                Dim RS As String = Nothing
                For Each DR In BIDR
                    Proc = False
                    Do While FindIR(DR("InvoiceNumber")) Or Not Proc
                        If Not DBNull.Value.Equals(DR("R Code")) Then
                            RS = Release_Selected(DR("R Code"), DR("RC text"))
                        Else
                            RS = Release_Selected(DR("R Code"), DR("RC Text"))
                        End If
                        Proc = True
                    Loop

                    If Proc Then
                        RaiseEvent Notify(Box, DR("InvoiceNumber"), RS)
                    Else
                        RaiseEvent Notify(Box, DR("InvoiceNumber"), "IR Already Released!")
                    End If
                Next
            Catch ex As Exception
                Dim S As String
                S = ex.Message
            End Try
        Else
            For Each DR In BIDR
                RaiseEvent Notify(Box, DR("InvoiceNumber"), "SAP Login Failed!")
            Next
        End If

    End Sub

    Private Sub Release_D(ByVal BIDR() As DataRow)
        Dim DR As DataRow

        If Session.LoggedIn Then
            Session.StartTransaction("ZMR0")
            Session.FindByNameEx("S_BUKRS-LOW", 32).Text = "*"
            Session.FindByNameEx("S_GJAHR-LOW", 31).Text = Now.Year + 1
            Session.FindByNameEx("S_GJAHR-LOW", 31).SetFocus()
            Session.SendVKey(2)
            Session.FindByNameEx("shell", 122).SelectedRows = "2"
            Session.FindByNameEx("btn[0]", 40).Press()
            Session.FindByNameEx("P_COUNT", 42).Selected = False
            Session.FindByNameEx("P_INPT", 42).Selected = False
            Session.FindByNameEx("P_IMAGE", 42).Selected = False
            Session.FindByNameEx("P_SPGRP", 42).Selected = False
            Session.FindByNameEx("P_SPGRM", 42).Selected = False
            Session.FindByNameEx("P_MANL", 42).Selected = True
            Session.DRArrayToClipboard(BIDR, "Vendor")
            Session.FindByNameEx("%_S_LIFNR_%_APP_%-VALU_PUSH", 40).Press()
            Session.FindByNameEx("btn[24]", 40).Press()
            Session.FindByNameEx("btn[8]", 40).Press()
            Session.FindByNameEx("btn[8]", 40).Press()

            Dim Proc As Boolean
            Dim RS As String = Nothing
            For Each DR In BIDR
                Proc = False
                If FindIR_D(DR("InvoiceNumber")) Then
                    RS = Release_Selected(DR("R Code"), DR("RC Text"))
                    Proc = True
                End If
                If Proc Then
                    RaiseEvent Notify(Box, DR("InvoiceNumber"), RS)
                Else
                    RaiseEvent Notify(Box, DR("InvoiceNumber"), "IR Already Released!")
                End If
            Next
        Else
            For Each DR In BIDR
                RaiseEvent Notify(Box, DR("InvoiceNumber"), "SAP Login Failed!")
            Next
        End If

    End Sub

    Private Function FindIR(ByVal IR As String) As Boolean

        FindIR = False

        If Session.FindByNameEx("btn[13]", 40) Is Nothing Then Exit Function

        Session.SendVKey(71)


        Session.FindByNameEx("RSYSF-STRING", 31).Text = IR
        Session.FindByNameEx("SCAN_STRING-START", 42).Selected = False
        Session.FindByNameEx("btn[0]", 40).Press()

        If Not Session.ActiveWindow.Text = "Find" Then
            Session.FindByNameEx("btn[0]", 40).Press()
            Session.FindByNameEx("btn[12]", 40).Press()
            Return FindIR
            'Exit Function
        End If

        Dim I As Integer = 2
        Dim Found As Boolean = False
        Do While Not Session.FindById("wnd[2]/usr/lbl[48," & I & "]") Is Nothing And Not Found
            If Session.FindById("wnd[2]/usr/lbl[48," & I & "]").Text = "X" Or Session.FindById("wnd[2]/usr/lbl[48," & I & "]").Text = "R" Then
                Found = True
                Session.FindById("wnd[2]/usr/lbl[48," & I & "]").SetFocus()
            Else
                I += 1
            End If

        Loop

        If Found Then
            FindIR = True
            Session.FindByNameEx("btn[2]", 40).Press()
        Else
            'Modificación para el desbloqueo de Yordy:
            I = 2

            Do While Not Session.FindById("wnd[2]/usr/lbl[48," & I & "]") Is Nothing And Not Found
                If Session.FindById("wnd[2]/usr/lbl[5," & I & "]").Text = IR Then
                    Found = True
                    Session.FindById("wnd[2]/usr/lbl[5," & I & "]").SetFocus()
                    FindIR = True
                    Session.FindByNameEx("btn[2]", 40).Press()


                End If
                I += 1
            Loop

            If Not Found Then
                Session.FindByNameEx("btn[12]", 40).Press()
                Session.FindByNameEx("btn[12]", 40).Press()
            Else


                If Not Session.FindById("wnd[2]/usr/lbl[117,7]") Is Nothing Then
                    Session.FindById("wnd[2]/usr/lbl[117,7]").SetFocus()
                Else

                    If Not Session.FindById("wnd[0]/usr/lbl[117,8]") Is Nothing Then
                        If Session.FindById("wnd[0]/usr/lbl[117,8]").text = "X" Then
                            Session.FindById("wnd[0]/usr/lbl[117,8]").SetFocus()
                        End If
                    Else

                        Dim cPos As String = ""
                        cPos = Session.GuiFocus.id

                        cPos = Replace(cPos, "/app/con[0]/ses[0]/wnd[0]/usr/lbl[", "")
                        cPos = Replace(cPos, "]", "")

                        Dim res = cPos.Split(",")


                        If Not Session.FindById("wnd[0]/usr/lbl[117," & Integer.Parse(res(1) + 1) & "]") Is Nothing Then
                            If Session.FindById("wnd[0]/usr/lbl[117," & Integer.Parse(res(1) + 1) & "]").text = "X" Then
                                Session.FindById("wnd[0]/usr/lbl[117," & Integer.Parse(res(1) + 1) & "]").SetFocus()
                            End If

                        End If


                        FindIR = False
                    End If


                End If
            End If

        End If
    End Function
    Private Function FindIR_D(ByVal IR As String) As Boolean
        FindIR_D = False
        If Session.FindByNameEx("btn[13]", 40) Is Nothing Then Exit Function

        Session.SendVKey(71)
        Session.FindByNameEx("RSYSF-STRING", 31).Text = IR
        Session.FindByNameEx("SCAN_STRING-START", 42).Selected = False
        Session.FindByNameEx("btn[0]", 40).Press()

        If Not Session.ActiveWindow.Text = "Find" Then
            Session.FindByNameEx("btn[0]", 40).Press()
            Session.FindByNameEx("btn[12]", 40).Press()
            Exit Function
        End If

        Session.FindById("wnd[2]/usr/lbl[5,2]").SetFocus()
        Session.SendVKey(2)
        Dim I As String = Left(Right(Session.GuiFocus.ID, 3), 2)
        If Left(I, 1) = "," Then I = Right(I, 1)
        I = (Val(I) + 1).ToString
        If Session.FindById("wnd[0]/usr/lbl[117," & I & "]").Text = "X" Or Session.FindById("wnd[0]/usr/lbl[117," & I & "]").Text = "R" Then
            Session.FindById("wnd[0]/usr/lbl[117," & I & "]").Setfocus()
            FindIR_D = True
        End If

    End Function
    Private Function Release_Selected(ByVal RCode As String, ByVal RMessage As String) As String

        Release_Selected = Nothing
        Try
            Session.SendVKey(2)
            Session.FindByNameEx("ZMINVMR-MR_CODE", 32).text = RCode
            Session.FindByNameEx("ZMINVMR-MR_CUST_TEXT", 31).text = Microsoft.VisualBasic.Left(RMessage, 132)
            Session.FindByNameEx("btn[11]", 40).Press()
            Session.FindByNameEx("btn[3]", 40).Press()
            Session.FindByNameEx("btn[13]", 40).Press()
            Session.FindByNameEx("btn[0]", 40).Press()
            Release_Selected = Session.StatusBarText
        Catch ex As Exception
            Dim S As String
            S = ex.Message
        End Try

    End Function

End Class