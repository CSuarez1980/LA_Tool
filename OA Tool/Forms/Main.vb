Imports System.Windows.Forms

Public Class Main
    Public Aux As Boolean
    Public ServerUpdate As String = ""
    Friend Const Server As String = "MXL0221R17"
    Friend Const User As String = "publish"
    Friend Const Pass As String = "heavymetal"
    Public ShowDMS As Boolean = False


    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Agregar código aquí para abrir el archivo.
        End If
    End Sub
    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: agregar código aquí para guardar el contenido actual del formulario en un archivo.
        End If
    End Sub
    Private Sub ExitToolsStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Global.System.Windows.Forms.Application.Exit()
    End Sub
    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub
    Private Sub TileVerticleToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub
    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub
    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cierre todos los formularios secundarios del primario.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub
    Private Sub BajarinformacionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBajarinformacion.Click
        Dim Form As New frm002
        Form.Show()
    End Sub
    Private Sub ActualizarCatálogosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCatálogos.Click
        Dim form As New frm047
        form.Show()
    End Sub
    Private Sub BajarFormatoAzulParaCotizarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBajarFormato.Click
        Dim Form As New frm003
        Form.Show()
    End Sub
    Private Sub EstatoDeContratoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContratosPorVencer.Click
        Dim Form As New frm004
        Form.Show()
    End Sub
    Private Sub VerificarContratosPorVencerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVerificarContratosPorVencer.Click
        Dim cn As New OAConnection.Connection
        cn.VerificarContratosPorVencer()
        cn.VerificarContratosVencidos()
    End Sub
    Private Sub ListadoDeConrtatosPorFechaDeActualizaciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContratosVencidos.Click
        Dim form As New frm005
        form.Show()
    End Sub
    Private Sub BajarTodosLosContratosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BajarTodosLosContratosToolStripMenuItem.Click
        Dim cn As New OAConnection.Connection
        Dim DT As New DataTable

        DT = cn.RunSentence("Select * From [Detalle de Contrato]").Tables(0)

        If DT.Rows.Count > 0 Then
            cn.ExportDataTableToXL(DT)
            MsgBox("Done.")
        Else
            MsgBox("Data could not be loaded.")
        End If

        'Dim Access As New Access.Application
        'Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        'Access.Run("ExportToXLDetalleDeContrato")
        'Access.CloseCurrentDatabase()
        'Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        'cn.KillProcess("MSAccess")
    End Sub
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        'SincServer()
        ActualizarAccess()
    End Sub
    Private Sub SincServer()
        Dim Access As New Access.Application
        Dim cn As New OAConnection.Connection
        Dim i%
        Dim Plantas As DataTable
        Dim Path$

        Me.PBar.Value = 0

        Path = "L:\PSL\LA\LA Outline Agreements\OADownload\"

        If Len(Dir(Path & "Path.txt")) = 0 Then
            MsgBox("You should be logged in Novel Network" & Chr(13) & Chr(13) & "Please connect to Novel Network and try again.", MsgBoxStyle.Exclamation)
            Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
            cn.KillProcess("MSAccess")
            Exit Sub
        End If

        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        Plantas = cn.GetActivesPlants.Tables(0)

        Access.DoCmd.RunSQL("Delete From CurrentOTD")
        Me.PBar.Value = 5


        Access.DoCmd.RunSQL("Delete from DetalleCompras")
        Me.PBar.Value = 10


        Access.DoCmd.RunSQL("Delete from DetalleContrato")
        Me.PBar.Value = 15

        Access.DoCmd.RunSQL("Delete from HeaderContrato")
        Me.PBar.Value = 20

        Access.DoCmd.RunSQL("Delete from HeaderCompras")
        Me.PBar.Value = 25

        Access.DoCmd.RunSQL("Delete From Manufacter")
        Me.PBar.Value = 30

        Access.DoCmd.RunSQL("Delete From MasterData")
        Me.PBar.Value = 35

        Access.DoCmd.RunSQL("Delete From Vendors")
        Me.PBar.Value = 40

        Access.Run("ImportHeaderContrato", Path & "EKKO_Contratos.txt")
        Me.PBar.Value = 50

        Access.Run("ImportDetalleContrato", Path & "EKPO_Contratos.txt")
        Me.PBar.Value = 55

        Access.Run("ImportHeaderCompras", Path & "EKKO_Compras.txt")
        Me.PBar.Value = 60

        Access.Run("ImportVendors", Path & "Vendors.txt")
        Me.PBar.Value = 65

        For i = 0 To Plantas.Rows.Count - 1
            Access.Run("ImportDetalleCompras", Path & "EKPO_Compras_" & Plantas.Rows(i).Item(0) & ".txt")

            Access.Run("ImportCurrentOTD", Path & "CurrentOTD_" & Plantas.Rows(i).Item(0) & ".txt")

            Access.DoCmd.RunSQL("Update CurrentOTD Set Planta = '" & Plantas.Rows(i).Item(0) & "' Where isNull(Planta)")

            Access.DoCmd.RunSQL("Delete From CurrentOTD Where isNull(Gica)")

            Access.Run("ImportMasterData", Path & "Marc_" & Plantas.Rows(i).Item(0) & ".txt")

            Access.Run("ImportManufacter", Path & "Manufacter_" & Plantas.Rows(i).Item(0) & ".txt")

            Me.PBar.Value = Me.PBar.Value + 1
        Next i

        Access.DoCmd.RunSQL("Update CurrentOTD Set OTD = round(((Del1 + Del2)/(Del1 + Del2 + Del3 + Del4 + Del5) * 100),2) Where MeanPDT <> 0")

        Me.PBar.Value = 90

        On Error Resume Next
        Access.DoCmd.RunSQL("Drop table EKPO_Contratos_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKKO_Contratos_ImportErrors")
        Access.DoCmd.RunSQL("Vendors_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKKO_Compras_ImportErrors")

        Me.PBar.Value = 95

        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0045_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0051_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0278_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0300_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_0301_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_2921_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_2930_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4004_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4563_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4841_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_4950_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_7761_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_8727_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9245_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9265_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9266_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9367_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9475_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9476_ImportErrors")
        Access.DoCmd.RunSQL("Drop table EKPO_Compras_9653_ImportErrors")

        Me.PBar.Value = 98

        Access.DoCmd.RunSQL("Drop table Manufacter_0045_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0051_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0278_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0300_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_0301_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_2921_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_2930_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4004_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4563_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4841_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_4950_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_7761_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_8727_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9245_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9265_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9266_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9367_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9475_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9476_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Manufacter_9653_ImportErrors")
        Access.DoCmd.RunSQL("Drop table Ekbe_ImportErrors")

        Access.DoCmd.RunSQL("Update ThisVersion set LastUpdate = #" & Today.Date & "#")

        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        cn.KillProcess("MSAccess")

        MsgBox("System synchronized.", MsgBoxStyle.Information)


        Me.PBar.Value = 0
    End Sub
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        'Dim Access As New Access.Application
        'Dim cn As New OAConnection.Connection

        'Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        'Access.Run("ExportToXLDetalleDeContrato")
        'Access.CloseCurrentDatabase()
        'Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        'cn.KillProcess("MSAccess")

        Dim cn As New OAConnection.Connection
        Dim Table As DataTable

        Table = cn.RunSentence("Select * From [Detalle de Contrato]").Tables(0)

        If Table.Rows.Count > 0 Then
            cn.ExportDataTableToXL(Table)
        Else
            MsgBox("No data could be found.", MsgBoxStyle.Exclamation)
        End If

        'cn.AppendTableToExcel(Table)


        'MsgBox("Done!")
    End Sub
    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContratosPorVencer.Click
        Dim Form As New frm004

        Form.Show()
    End Sub
    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdContratosVencidos.Click
        Dim form As New frm005

        form.Show()
    End Sub
    Private Sub DistribuciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMiDistribucion.Click
        Dim form As New frm010
        form.Show()
    End Sub
    Private Sub Main_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Dim cn As New OAConnection.Connection

        cn.KillProcess("MSAccess")
        cn.KillProcess("Excel")

        cn = Nothing
    End Sub
    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cn As New OAConnection.Connection
        Dim Up As DataTable

        Up = cn.RunSentence("Select * From Updates").Tables(0)

        '************************************************************************************************
        '************************************************************************************************
        '   Metodo para verificar si hay alguna actualización de la base via FTP
        '************************************************************************************************
        '************************************************************************************************
        If Not Debugger.IsAttached Then
            'Si hay cambio de password en el servidor hay que modificar el ejecutable del updates con el nuevo password 
            'y subirlo al FTP server
            Dim U As New Updates.UpdateClass("LATool", My.Application.Info.Title, My.Application.Info.Version, Up.Rows(0).Item("Server"), Up.Rows(0).Item("Users"), Up.Rows(0).Item("Pass"))
            'Dim U As New Updates.UpdateClass("LATool", My.Application.Info.Title, My.Application.Info.Version)
            U.Update()
        End If

        'Dim ThisVersion$
        'Dim Access As New Access.Application
        Dim Tabla As DataTable
        Aux = False
        Me.ReqUpdate.Enabled = False
        Me.chkVerificarSourceList.Visible = False

        gsUsuarioPC = cn.GetUserId

        Dim SName = Split(cn.GetConnectionString(), ";")
        lblServer.Text = Replace(SName(0), "Data Source=", "Server Name: ").Trim & " :: " & Replace(SName(1), "Initial Catalog=", "Data Base: ").Trim

        '****************************************************
        'Configuro la aplicacion con los accesos del usuario:
        SeteoAccesos()
        '****************************************************
        '************************************************
        'Verifico que el usuario tenga acceso al sistema:
        Tabla = cn.RunSentence("Select * From Users Where TNumber = '" & gsUsuarioPC & "'").Tables(0)
        If Tabla.Rows.Count = 0 Then
            MsgBox("You do not have access to this application." & Chr(13) & Chr(13) & "Please request the access to your DB spoc.", MsgBoxStyle.Critical)
            Application.Exit()
        Else
            'Verifico si el usuario está haciendo BackUp
            SAPConfig = Tabla.Rows(0).Item("SAPConfig")
            PDFPath = Tabla.Rows(0).Item("PDFPath")
            QDays = Tabla.Rows(0).Item("QDays")
            gsUserMail = Tabla.Rows(0).Item("Mail")
            giDistribution = Tabla(0).Item("Distribution")
            PDFTimeOut = Tabla(0).Item("PDFTimeOut")
            FreePDF4 = Tabla(0).Item("FreePDF4")
            PGUser = Tabla(0).Item("PG Empl")

            If Tabla.Rows(0).Item("MakingBackUp") Then
                MsgBox("You are backuping to user: " & Tabla.Rows(0).Item("MakingBackUpTo"), MsgBoxStyle.Information, "BackUp Status")
                gsUsuario = Tabla.Rows(0).Item("MakingBackUpTo")
                Me.Text = Me.Text & " - Making BackUp To: " & gsUsuario
            Else
                gsUsuario = Tabla.Rows(0).Item("TNumber")
            End If

            If (Now.Date - Date.Parse(Tabla.Rows(0).Item("LastLogon"))).Days > 90 Then
                MsgBox("Your account has been locked due to inactivity for 90 days. Please contact your spoc for re-activation", MsgBoxStyle.Critical, "Account Locked due inactivity")
                End
            Else
                cn.ExecuteInServer("Update [Users] set LastLogon = {fn Now()} where TNumber = '" & gsUsuarioPC & "'")
            End If
            'Verifico si algún usuario esta haciendo backup a otro.
            'Propuesta realizada en la reunion del 8 de marzo 2010.
            'Que al usuario se le muestre un mensage si alguien le esta haciendo BackUp

            If Tabla.Rows(0).Item("BackUped") Then
                MsgBox("User: " & Tabla.Rows(0).Item("BackUpedBy") & " is set as your back up.", MsgBoxStyle.Information, "BackUp Status: <Active>")
            End If
        End If
        '************************************************
        'Verifico los contratos vencidos:
        Tabla = cn.RunSentence("Select OA From vstContratosVencidos Where OwnerMail = '" & gsUserMail & "'").Tables(0)
        If Tabla.Rows.Count > 0 Then
            Me.Timer1.Enabled = True
            Me.lblAlert.Visible = True
            Me.lblAlert.Text = "You have " & Tabla.Rows.Count & " OA's over due"
        Else
            Me.Timer1.Enabled = False
            Me.lblAlert.Visible = False
        End If

        'Verifico los contratos por vencer:
        Tabla = cn.RunSentence("Select OA From vstContratosPorVencer Where OwnerMail = '" & gsUserMail & "' and (InProcess <> 1 or InProcess is null)").Tables(0)
        If Tabla.Rows.Count > 0 Then
            Me.lblInformacion.Visible = True
            Me.lblInformacion.Text = "There are " & Tabla.Rows.Count & " OA's to be updated."
            Me.imgInformacion.Visible = True
        Else
            Me.lblInformacion.Visible = False
            Me.imgInformacion.Visible = False
        End If

        'Verifico los materiales con diferencias en los PDT de los contratos y la master data:
        Tabla = cn.RunSentence("Select * From vstContrato_MasterData where PDT_OA <> PDT_MD and OwnerMail = '" & gsUserMail & "'").Tables(0)
        If Tabla.Rows.Count > 0 Then
            Me.lblPDT.Visible = True
            Me.lblPDT.Text = "There are " & Tabla.Rows.Count & " materials with PDT differences."
            Me.ImgPDT.Visible = True
        Else
            Me.ImgPDT.Visible = False
            Me.lblPDT.Visible = False
        End If

        'Verificacion de que los contratos tengan source list
        Tabla = cn.RunSentence("Select * From vstMaterialesSinSourceList where OwnerMail = '" & gsUserMail & "'").Tables(0)
        If Tabla.Rows.Count > 0 Then
            Me.imgSourceList.Visible = True
            Me.txtSourceList.Visible = True
            Me.txtSourceList.Text = "There are " & Tabla.Rows.Count & " materials without OA source list."
        End If

        Me.txtActiveUser.Text = gsUsuarioPC & ": " & cn.GetUserName

        Me.lblVersion.Text = "Version: " & My.Application.Info.Version.ToString

        Select Case giDistribution
            Case 0
                Me.lblDistribution.Text = "Distribution: Local and Import"
            Case 1
                Me.lblDistribution.Text = "Distribution: Only Local"
            Case 2
                Me.lblDistribution.Text = "Distribution: Only Import"
        End Select

        If gsUsuarioPC <> "BP3741" Then '-> Este usuario tiene problemas con esta funcion de Windows
            lblOS.Text = "Runing on: " & My.Computer.Info.OSFullName
        End If

        bgCheckDMS.RunWorkerAsync()
    End Sub
    Private Sub ActualizarServidorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuActualizarServidor.Click
        Dim cn As New OAConnection.Connection
        Dim Access As New Access.Application
        Dim Table As DataTable

        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        cn.ExecuteInServer("Delete from CurrentOTD")
        Me.PBar.Value = 5
        cn.ExecuteInServer("Delete from DetalleCompras")
        Me.PBar.Value = 8
        cn.ExecuteInServer("Delete from DetalleContrato")
        Me.PBar.Value = 12
        cn.ExecuteInServer("Delete from HeaderCompras")
        Me.PBar.Value = 15
        cn.ExecuteInServer("Delete from HeaderContrato")
        Me.PBar.Value = 19
        cn.ExecuteInServer("Delete from Manufacter")
        Me.PBar.Value = 23
        cn.ExecuteInServer("Delete from MasterData")
        Me.PBar.Value = 24
        cn.ExecuteInServer("Delete from vendors")
        Me.PBar.Value = 29
        cn.ExecuteInServer("Delete from EORD")
        Me.PBar.Value = 32

        Table = cn.GetAccessTable("Select * From CurrentOTD").Tables(0)
        cn.AppendTableToSqlServer("CurrentOTD", Table)
        Me.PBar.Value = 35

        Table = cn.GetAccessTable("Select * From DetalleCompras").Tables(0)
        cn.AppendTableToSqlServer("DetalleCompras", Table)
        Me.PBar.Value = 45

        Table = cn.GetAccessTable("Select * From DetalleContrato").Tables(0)
        cn.AppendTableToSqlServer("DetalleContrato", Table)
        Me.PBar.Value = 59

        Table = cn.GetAccessTable("Select * From HeaderCompras").Tables(0)
        cn.AppendTableToSqlServer("HeaderCompras", Table)
        Me.PBar.Value = 65

        Table = cn.GetAccessTable("Select * From HeaderContrato").Tables(0)
        cn.AppendTableToSqlServer("HeaderContrato", Table)
        Me.PBar.Value = 70

        Table = cn.GetAccessTable("Select * From Manufacter").Tables(0)
        cn.AppendTableToSqlServer("Manufacter", Table)
        Me.PBar.Value = 80

        Table = cn.GetAccessTable("Select * From MasterData").Tables(0)
        cn.AppendTableToSqlServer("MasterData", Table)
        Me.PBar.Value = 90

        Table = cn.GetAccessTable("Select * From EORD").Tables(0)
        cn.AppendTableToSqlServer("EORD", Table)
        Me.PBar.Value = 95

        Table = cn.GetAccessTable("Select * From vendors").Tables(0)
        cn.AppendTableToSqlServer("Vendors", Table)
        Me.PBar.Value = 100

        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)

        MsgBox("Done!.")
        Me.PBar.Value = 0
    End Sub
    Private Sub ActualizarAccess()
        Dim Access As New Access.Application

        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        Me.PBar.Value = 5

        Access.DoCmd.RunSQL("Delete from CurrentOTD")
        Me.PBar.Value = 10

        Access.DoCmd.RunSQL("Delete from DetalleCompras")
        Me.PBar.Value = 15

        Access.DoCmd.RunSQL("Delete from DetalleContrato")
        Me.PBar.Value = 20

        Access.DoCmd.RunSQL("Delete from HeaderCompras")
        Me.PBar.Value = 25

        Access.DoCmd.RunSQL("Delete from HeaderContrato")
        Me.PBar.Value = 30

        Access.DoCmd.RunSQL("Delete from Manufacter")
        Me.PBar.Value = 35

        Access.DoCmd.RunSQL("Delete from MasterData")
        Me.PBar.Value = 40

        Access.DoCmd.RunSQL("Delete from vendors")
        Me.PBar.Value = 45

        Access.DoCmd.RunSQL("Insert into CurrentOTD select * from dbo_CurrentOTD")
        Me.PBar.Value = 50

        Access.DoCmd.RunSQL("Insert into DetalleCompras select * from dbo_DetalleCompras")
        Me.PBar.Value = 55

        Access.DoCmd.RunSQL("Insert into DetalleContrato select * from dbo_DetalleContrato")
        Me.PBar.Value = 60

        Access.DoCmd.RunSQL("Insert into HeaderCompras select * from dbo_HeaderCompras")
        Me.PBar.Value = 65

        Access.DoCmd.RunSQL("Insert into HeaderContrato select * from dbo_HeaderContrato")
        Me.PBar.Value = 70

        Access.DoCmd.RunSQL("Insert into Manufacter select * from dbo_Manufacter")
        Me.PBar.Value = 80

        Access.DoCmd.RunSQL("Insert into MasterData select * from dbo_MasterData")
        Me.PBar.Value = 90

        Access.DoCmd.RunSQL("Insert into vendors select '410' as client, Vendor as code, Name from dbo_vendorsG11")
        Me.PBar.Value = 95

        Access.DoCmd.RunSQL("Update ThisVersion set LastUpdate = #" & Today.Date & "#")
        Me.PBar.Value = 100

        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)

        MsgBox("Done!.")
        Me.PBar.Value = 0

    End Sub
    Private Sub VerificarContratosSinOwnerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVerificarContratosSinOwner.Click
        Dim cn As New OAConnection.Connection

        cn.VerificarContratosSinOwner()
        MsgBox("Done!.")
        cn = Nothing
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Aux Then
            Me.imgAlerta.Visible = True
            Aux = False
        Else
            Me.imgAlerta.Visible = False
            Aux = True
        End If

    End Sub
    Private Sub EnviarFormatoAzulAProveedorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnviarFormatoVendor.Click
        Dim x As New frm007

        x.Show(Me)
    End Sub
    Private Sub ContratosPorVencerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContratosPorVencerLA.Click
        Dim x As New frm008

        x.Show(Me)
    End Sub
    Private Sub ControsvencidosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuControsVencidosLA.Click
        Dim x As New frm009

        x.Show(Me)
    End Sub
    Private Sub DistribuciónDeContratosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDistribucionContratosLA.Click
        Dim x As New frm006

        x.Show(Me)
    End Sub
    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Me.Timer2.Enabled = False

        If Me.chkVerificarSourceList.Checked Then
            Dim SAP As Object
            Dim SAPCn As New SAPConection.SAPTools
            Dim Access As New Access.Application
            Dim cn As New OAConnection.Connection
            Dim Tabla As New DataTable

            Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

            Access.DoCmd.RunSQL("Delete from EORD")
            SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", "bm4691", "tacares02", SAPConfig)
            SAPCn.DownloadSourceList(SAP)
            SAPCn.CloseSession(SAP)

            Access.Run("ImportEORD", My.Application.Info.DirectoryPath & "\OADownLoad\EORD.txt")
            Access.DoCmd.RunSQL("Drop table EORD_ImportErrors")

            Tabla = cn.GetAccessTable("Select * From vstSourceListModificado").Tables(0)
            If Tabla.Rows.Count > 0 Then
                MsgBox("Se han detectado diferencias en el source list.", MsgBoxStyle.Critical)
                Me.chkVerificarSourceList.Checked = False
            End If
            
            Access.CloseCurrentDatabase()
            Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
            SAP = Nothing
            Access = Nothing
            cn = Nothing
            SAPCn = Nothing
        End If

        Me.Timer2.Enabled = True
    End Sub
    Private Sub chkVerificarSourceList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVerificarSourceList.CheckedChanged
        If Me.chkVerificarSourceList.Checked Then
            Me.Timer2.Enabled = True
        Else
            Me.Timer2.Enabled = False
        End If
    End Sub
    Private Sub EnviarFormatoAzulParaActualizarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEnviarFormatoActualizar.Click
        Dim x As New frm011

        x.Show(Me)
    End Sub
    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnviarFormatoVendor.Click
        Dim x As New frm007

        x.Show(Me)
    End Sub
    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdEnviarFormatoActualizar.Click
        Dim form As New frm011

        form.Show()
    End Sub
    Private Sub MarcarContratoComoActualizadoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarcarContratoComoActualizado.Click
        
        Dim Form As New frm012
        Form.Show(Me)
    End Sub
    Private Sub SeguimientoDeMisContratosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSeguimientoMisContratos.Click
        Dim form As New frm013

        form.Show(Me)
    End Sub
    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSeguimientoMisContratos.Click
        Dim form As New frm013

        form.Show(Me)
    End Sub
    Private Sub SeguimientoDeContratosLAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSeguimientoContratosLA.Click
        Dim cn As New OAConnection.Connection
        Dim form As New frm014

        form.Show(Me)
    End Sub
    Private Sub MarcarContratoConProblemasTktBSSToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMarcarContratoConProblemasTktBSS.Click
        Dim cn As New OAConnection.Connection

        Dim form As New frm015
        form.Show(Me)
    End Sub
    Private Sub SeguimentoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSeguimentoRegional.Click
        Dim cn As New OAConnection.Connection

        Dim form As New frm016

        form.Show(Me)

    End Sub
    Private Sub MaterialesConDiferenciasEnPDTToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMaterialesConDiferenciasEnPDT.Click
        Dim Form As New frm017

        Form.Show(Me)
    End Sub
    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialesConDiferenciasEnPDT.Click
        Dim Form As New frm018
        Form.Show(Me)
    End Sub
    Private Sub BajarRequisionesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRequisiones.Click
        Dim Form As New frm019
        Form.Show(Me)
    End Sub
    Private Sub OANotify_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OANotify.Tick
        Dim cn As New OAConnection.Connection
        Dim Update As String

        Update = cn.RunSentence("Select [Update] From Version").Tables(0).Rows(0).Item("Update")

        If Not DBNull.Value.Equals(Update) Then
            If Update <> ServerUpdate Then
                Me.Notify.Visible = True
                Me.Notify.ShowBalloonTip(2, "Información Actualizada", "El Servidor ha refrescado la información de requisiones", ToolTipIcon.Info)
                ServerUpdate = Update
            End If
        End If

        cn = Nothing
    End Sub
    Private Sub Notify_BalloonTipClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Notify.BalloonTipClosed
        Me.Notify.Visible = False
    End Sub
    Private Sub ReqUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReqUpdate.Tick
        Dim SAP As Object
        Dim SAPCn As New SAPConection.SAPTools
        Dim Access As New Access.Application
        Dim cn As New OAConnection.Connection
        Dim Tabla As New DataTable
        Dim Plantas As New DataSet
        Dim i%

        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")

        Plantas = cn.GetActivesPlants
        SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", "bm4691", "tacares02", SAPConfig)
        SAPCn.DownloadSourceList(SAP)
        SAPCn.DownloadDetalleContratos(SAP, Plantas)
        SAPCn.DownloadHeaderContrato(SAP)

        Access.DoCmd.RunSQL("Delete From MasterData")
        For i = 0 To Plantas.Tables(0).Rows.Count - 1
            SAPCn.DownloadMasterData(SAP, Plantas.Tables(0).Rows(i).Item("Code"))
            Access.Run("ImportMasterData", My.Application.Info.DirectoryPath & "\OADownLoad\Marc_" & Plantas.Tables(0).Rows(i).Item("Code") & ".txt")
        Next

        SAPCn.CloseSession(SAP)

        Access.Run("ImportEORD", My.Application.Info.DirectoryPath & "\OADownLoad\EORD.txt")
        Access.Run("ImportHeaderContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKKO_Contratos.txt")
        Access.Run("ImportDetalleContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKPO_Contratos.txt")

        Access.DoCmd.RunSQL("Drop table EORD_ImportErrors")

        Tabla = cn.GetAccessTable("Select * From vstSourceListModificado").Tables(0)
        If Tabla.Rows.Count > 0 Then
            MsgBox("Se han detectado diferencias en el source list.", MsgBoxStyle.Critical)
            Me.chkVerificarSourceList.Checked = False
        End If


        Tabla = cn.GetAccessTable("Select * From DetalleContrato").Tables(0)
        cn.AppendTableToSqlServer("DetalleContrato", Tabla)


        Tabla = cn.GetAccessTable("Select * From HeaderContrato").Tables(0)
        cn.AppendTableToSqlServer("HeaderContrato", Tabla)

        Tabla = cn.GetAccessTable("Select * From MasterData").Tables(0)
        cn.AppendTableToSqlServer("MasterData", Tabla)

        Tabla = cn.GetAccessTable("Select * From EORD").Tables(0)
        cn.AppendTableToSqlServer("EORD", Tabla)

        Access.CloseCurrentDatabase()
        Access.Quit(Global.Access.AcQuitOption.acQuitSaveAll)
        SAP = Nothing
        Access = Nothing
        cn = Nothing
        SAPCn = Nothing

    End Sub
    Private Sub ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMaterialesSinSourceList.Click
        Dim Form As New frm021

        Form.Text = "[021] Materiales sin Source list de contrato [Por Usuario]"
        Form.Show(Me)
    End Sub
    Private Sub MaterialesSinSourceListLAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMaterialesSinSourceListLA.Click
        Dim Form As New frm021

        Form.Text = "[021] Materiales sin Source List LA"
        Form.Show(Me)
    End Sub
    Private Sub TriggerDBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim form As New Trigger.frmTrigger

        form.Show(Me)
    End Sub
    Private Sub SeteoAccesos()
        Dim i%
        Dim Tabla As DataTable
        Dim cn As New OAConnection.Connection
        Tabla = cn.RunSentence("Select * from vstPerfiles where TNumber = '" & gsUsuarioPC & "'").Tables(0)

        If Tabla.Rows.Count > 0 Then
            For i = 0 To Tabla.Rows.Count - 1
                Select Case Tabla.Rows(i).Item("ID_Formulario").ToString
                    Case "2"
                        Me.mnuConfigurarVariante.Enabled = True

                    Case "3"
                        Me.mnuCatálogos.Enabled = True

                    Case "5"
                        Me.mnuBajarinformacion.Enabled = True

                    Case "6"
                        Me.mnuBajarFormato.Enabled = True

                    Case "8"
                        Me.mnuContratosPorVencer.Enabled = True
                        Me.cmdContratosPorVencer.Enabled = True

                    Case "9"
                        Me.mnuContratosVencidos.Enabled = True
                        Me.cmdContratosVencidos.Enabled = True

                    Case "10"
                        Me.mnuDistribucionContratosLA.Enabled = True

                    Case "11"
                        Me.mnuEnviarFormatoVendor.Enabled = True
                        Me.cmdEnviarFormatoVendor.Enabled = True

                    Case "12"
                        Me.mnuContratosPorVencerLA.Enabled = True

                    Case "13"
                        Me.mnuControsVencidosLA.Enabled = True

                    Case "14"
                        Me.mnuMiDistribucion.Enabled = True

                    Case "15"
                        Me.mnuEnviarFormatoActualizar.Enabled = True
                        Me.cmdEnviarFormatoActualizar.Enabled = True

                    Case "17"
                        Me.mnuMarcarContratoComoActualizado.Enabled = True

                    Case "18"
                        Me.mnuSeguimientoMisContratos.Enabled = True
                        Me.cmdSeguimientoMisContratos.Enabled = True

                    Case "19"
                        Me.mnuSeguimientoContratosLA.Enabled = True

                    Case "20"
                        Me.mnuMarcarContratoConProblemasTktBSS.Enabled = True

                    Case "22"
                        Me.mnuMaterialesConDiferenciasEnPDT.Enabled = True

                    Case "23"
                        Me.cmdMaterialesConDiferenciasEnPDT.Enabled = True

                    Case "24"
                        Me.mnuRequisiones.Enabled = True

                    Case "25"
                        Me.mnuMaterialesSinSourceListLA.Enabled = True
                        Me.cmdMaterialesSinSourceList.Enabled = True

                        'Case "26"
                        '    Me.mnuTrigger.Enabled = True

                    Case "27"
                        Me.mnuVerificarContratosPorVencer.Enabled = True

                    Case "28"
                        Me.mnuVerificarContratosSinOwner.Enabled = True

                    Case "29"
                        Me.mnuActualizarServidor.Enabled = True

                    Case "30"
                        Me.mnuAccesos.Enabled = True

                    Case "31"

                    Case "32"
                        Me.mnuDestinos.Enabled = True

                    Case "33"
                        Me.mnuConfiguraciónFletes.Enabled = True

                    Case "34"
                        Me.mnuAnalisisImportación.Enabled = True

                    Case "35"
                        Me.mnuOrigenes.Enabled = True

                    Case "36"
                        Me.mnuAssignAndProcess.Enabled = True

                    Case "37"
                        Me.mnuVarianteUsuario.Enabled = True

                    Case "38"
                        Me.mnuDownloader.Enabled = True

                    Case "39"
                        Me.mnuPerfilesUsuario.Enabled = True

                    Case "40"
                        Me.mnuPVL.Enabled = True

                    Case "41"
                        Me.mnuBlockedInvoice.Enabled = True


                    Case "43"
                        Me.mnuUploadAPTrade.Enabled = True

                    Case "44"
                        Me.mnuStatus161.Enabled = True

                    Case "45"
                        Me.mnuReleaseStatus161.Enabled = True

                    Case "46"
                        Me.mnu_Reports_Internal_Control.Enabled = True

                    Case "47"
                        mnuTaxRules.Enabled = True

                    Case "48"
                        mnuVerifyUpload.Enabled = True

                    Case "49"
                        mnuDMSUpload.Enabled = True

                    Case "50"
                        mnuNCMUpload.Enabled = True

                    Case "51"
                        mnuInternalControl.Enabled = True

                    Case "52"
                        mnuNCMOwner.Enabled = True

                    Case "53"
                        mnuDMSReport.Enabled = True

                End Select
            Next
        End If
    End Sub
    Private Sub mnuAccesos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAccesos.Click
        Dim Form As New frm023

        Form.Show(Me)
    End Sub
    Private Sub AnálisisDeIToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAnalisisImportación.Click
        Dim Form As New ImportAnalisis.frmImport
        Form.Show()

    End Sub
    Private Sub MantenimientoDeOrigenesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOrigenes.Click
        Dim form As New ImportAnalisis.frmOrigen
        form.Show()
    End Sub
    Private Sub MantenimientoDeDestinosToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDestinos.Click
        Dim form As New ImportAnalisis.frmDestinos
        form.Show()
    End Sub
    Private Sub ConfiguraciónDeFletesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguraciónFletes.Click
        Dim Form As New ImportAnalisis.frmFletes
        Form.Show()
    End Sub
    Private Sub AssignProcessToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAssignAndProcess.Click
        Dim Form As New frm024
        Form.Show()
    End Sub
    Private Sub ConfiguraciónDeVariantesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfigurarVariante.Click
        Dim form As New frmVariantes
        form.Show(Me)
    End Sub
    Private Sub mnuVarianteUsuario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVarianteUsuario.Click
        Dim Form As New frm025
        Form.Show()
    End Sub
    Private Sub SAPPasswordsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SAPPasswordsToolStripMenuItem.Click
        'Dim form As New frm029
        'form.Show()

        Dim F As New SAPCOM.Connection_Manager(gsUsuarioPC, AppId)
        F.ShowDialog()

    End Sub
    Private Sub SeguimentoToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeguimentoToolStripMenuItem.Click
        Dim Form As New frm030
        Form.Show()
    End Sub
    Private Sub PrintPOsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    Private Sub AgregarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarToolStripMenuItem.Click
        Dim Form As New frm033
        Form.Show()
    End Sub
    Private Sub EditarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarToolStripMenuItem.Click
        Dim Form As New frm034
        Form.Show()
    End Sub
    Private Sub ToolStripButton3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim form As New Working
        form.Show()
    End Sub
    Private Sub mnuPOAbiertas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPOAbiertas.Click
        Dim Form As New frm032
        Form.Show()
    End Sub
    Private Sub ConfiguraciónToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfiguraciónToolStripMenuItem.Click
        Dim form As New frm038
        form.Show()
    End Sub
    Private Sub AgregarToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AgregarToolStripMenuItem1.Click
        Dim form As New frm036
        form.Show()
    End Sub
    Private Sub EditarToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditarToolStripMenuItem1.Click
        Dim form As New frm037
        form.Show()
    End Sub
    Private Sub PVLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPVL.Click
        Dim Form As New frm040
        Form.Show()
    End Sub
    Private Sub BackUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BackUpToolStripMenuItem.Click
        Dim Form As New frm041

        Form.Show()
    End Sub
    Private Sub DownloaderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDownloader.Click
        Dim Form As New frm000

        Form.Show()
    End Sub
    Private Sub mnuPerfilesUsuario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPerfilesUsuario.Click
        Dim form As New frm042
        form.Show()
    End Sub
    Private Sub PorRangoDeFechaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PorRangoDeFechaToolStripMenuItem.Click
        Dim form As New frm043
        form.Show()
    End Sub
    Private Sub POCommentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POCommentsToolStripMenuItem.Click
        Dim form As New frm045
        form.Show()
    End Sub
    Private Sub POReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POReportToolStripMenuItem.Click
        Dim form As New frm046
        form.Show()
    End Sub
    Private Sub BlockedInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBlockedInvoice.Click
        Dim form As New frm048
        form.Show()
    End Sub
    Private Sub UploadAPTradeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuUploadAPTrade.Click
        Dim Form As New frm051
        Form.Show()
    End Sub
    Private Sub DownloadBlueFormByVendorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadBlueFormByVendorToolStripMenuItem.Click
        Dim Form As New frm052
        Form.Show()
    End Sub
    Private Sub CurrencyConverterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurrencyConverterToolStripMenuItem.Click
        Dim form As New frm053
        form.Show()
    End Sub
    Private Sub cmdExchange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExchange.Click
        Dim form As New frm053
        form.Show()
    End Sub
    Private Sub SpendReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpendReportToolStripMenuItem.Click
        Dim form As New frm054
        form.Show()
    End Sub
    Private Sub ByUnblockedDateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ByUnblockedDateToolStripMenuItem.Click
        Dim form As New frm055
        form.Show()
    End Sub
    Private Sub TaxMaintenanceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TaxMaintenanceToolStripMenuItem.Click
        Dim form As New frm057
        form.Show()
    End Sub
    Private Sub DownloadBrazilianBlueFToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadBrazilianBlueFToolStripMenuItem.Click
        Dim form As New frm058
        form.Show()
    End Sub
    Private Sub QuotationDirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuotationDirectoryToolStripMenuItem.Click
        Dim form As New frm059
        form.Show()
    End Sub
    Private Sub IncludeNewMaterialToOAToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncludeNewMaterialToOAToolStripMenuItem.Click
        Dim form As New frm063

        form.Show()
    End Sub
    Private Sub UploadTicketXMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadTicketXMLToolStripMenuItem.Click
        Dim form As New frm064
        form.Show()
    End Sub
    Private Sub Status161ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStatus161.Click
        Dim form As New frm065
        form.Show()

    End Sub
    Private Sub ReleaseRecordsStatus161ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReleaseStatus161.Click
        Dim form As New frm066
        form.Show()
    End Sub
    Private Sub ScorecardReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ScorecardReportToolStripMenuItem.Click
        Dim form As New frm067
        form.Show()
    End Sub
    Private Sub UnbloquedReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnbloquedReportToolStripMenuItem.Click
        Dim form As New frm069
        form.Show()
    End Sub
    Private Sub PurchOrdersWoReferenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PurchOrdersWoReferenceToolStripMenuItem.Click
        Dim form As New frm070
        form.Show()
    End Sub
    Private Sub OAAndMasterDataToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OAAndMasterDataToolStripMenuItem.Click
        Dim form As New frm071
        form.Show()
    End Sub
    Private Sub IROnlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IROnlyToolStripMenuItem.Click
        Dim form As New frm072
        form.Show()
    End Sub
    Private Sub POAfterInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles POAfterInvoiceToolStripMenuItem.Click
        Dim form As New frm073
        form.Show()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Dim form As New frm074
        form.show()
    End Sub
    Private Sub OutlineAgreementPurchaseOrderPriceQuantityChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutlineAgreementPurchaseOrderPriceQuantityChangesToolStripMenuItem.Click
        Dim form As New frm075
        form.Show()
    End Sub
    Private Sub OTDReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OTDReportToolStripMenuItem.Click
        Dim form As New frm076
        form.Show()
    End Sub
    Private Sub RecToPOToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim form As New frm077
        form.Show()
    End Sub
    Private Sub CatalogsTaxRulesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTaxRules.Click
        Dim form As New frm079
        form.Show()
    End Sub
    Private Sub Status161ReleaseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Status161ReleaseToolStripMenuItem.Click
        Dim Form As New frm080
        Form.Show()
    End Sub
    Private Sub LABloquedInvoiceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LABloquedInvoiceToolStripMenuItem.Click
        Dim Form As New frm081
        Form.Show()
    End Sub
    Private Sub OpenOrdersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenOrdersToolStripMenuItem.Click
        Dim Form As New frm082
        Form.Show()
    End Sub
    Private Sub mnuVerifyUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVerifyUpload.Click
        Dim form As New frm083
        form.Show()
    End Sub
    Private Sub wbDMS_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wbDMS.DocumentCompleted

    End Sub
    Private Sub bgCheckDMS_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgCheckDMS.DoWork
        Dim cn As New OAConnection.Connection
        Dim dt As DataTable

        dt = cn.RunSentence("Select * From LA_Indirect_Distribution Where SPS = '" & gsUsuarioPC & "'").Tables(0)
        If dt.Rows.Count > 0 Then
            ShowDMS = True
        End If
    End Sub
    Private Sub bgCheckDMS_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bgCheckDMS.RunWorkerCompleted
        If ShowDMS Then
            wbDMS.Navigate("http://mxl1380q1v/LA%20WEB/forms/DMS.aspx?TNumber=" & gsUsuarioPC)
            wbDMS.Visible = True
        End If
    End Sub
    Private Sub MyOpenOrdersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_LA_Open_Items Where SPS = '" & gsUsuarioPC & "'").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If
    End Sub
    Private Sub MyOpenRequisitionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_LA_Open_Req Where SPS = '" & gsUsuarioPC & "'").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If

    End Sub
    Private Sub MyTriggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_DMS_LA_Trigger Where SPS = '" & gsUsuarioPC & "'").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If
    End Sub
    Private Sub OpenOrdersToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_LA_Open_Items").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If
    End Sub
    Private Sub OpenRequisitionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_LA_Open_Req").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If
    End Sub
    Private Sub TriggerToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim cn As New OAConnection.Connection
        Dim dt As New DataTable

        dt = cn.RunSentence("Select * From vst_DMS_LA_Trigger").Tables(0)

        If dt.Rows.Count > 0 Then
            cn.ExportDataTableToXL(dt)
        Else
            MsgBox("No records could be selected.")
        End If
    End Sub
    Private Sub DMSReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDMSReport.Click
        Dim frm As New frmAmericasDMS
        frm.Show()
    End Sub
    Private Sub DistributionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDMSUpload.Click
        Dim frm As New frm084
        frm.Show()
    End Sub
    Private Sub OAQuarterlyReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OAQuarterlyReportToolStripMenuItem.Click
        Dim frm As New frm086
        frm.Show()
    End Sub
    Private Sub NCMMassiveUploadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNCMUpload.Click
        Dim frm As New frm087
        frm.Show()
    End Sub
    Private Sub CatalogPurchGrpChangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CatalogPurchGrpChangeToolStripMenuItem.Click
        Dim frm As New frm088
        frm.Show()
    End Sub
    Private Sub BRNCMChangesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNCMOwner.Click
        Dim form As New frm089
        form.Show()
    End Sub

    Private Sub TransmissionReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TransmissionReportToolStripMenuItem.Click
        Dim form As New frm090
        form.Show()
    End Sub
End Class
Public Class SC_PO_Report
    Private _Data As DataTable
    Private _Plants As New List(Of PO_Parameter)
    Private _Vendors As New List(Of PO_Parameter)
End Class
Public Enum Action
    Include = 1
    Exclude = 0
End Enum
Public Class PO_Parameter
    Private _Action As Action = Action.Include
    Private _Value As String = ""
    Private _Field As String = ""

    Public Sub New(ByVal pField As String, ByVal pValue As String, Optional ByVal pAction As Action = Action.Include)
        _Field = pField
        _Value = pValue
        _Action = pAction
    End Sub

End Class
