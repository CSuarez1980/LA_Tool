Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Public Class frm000
    'Dim o_SAP As New SAPConection.c_SAP

    Dim SAPCn As New SAPConection.SAPTools
    Dim SAP As Object
    Dim cn As New OAConnection.Connection
    Dim Access As New Access.Application
    Dim Plantas As DataSet
    Dim Tabla As DataTable
    Dim Status As String = ""
    Dim Segundos As Integer = 0


    '****************************************************************************************
    '****************************************************************************************
    '                 DoWorks para los hilos
    '****************************************************************************************
    '****************************************************************************************
    Private Sub OADownload_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles OADownload.DoWork
        Dim Worker As BackgroundWorker = CType(sender, BackgroundWorker)
        e.Result = DownloadOA(Worker, e)
    End Sub

    Private Sub RequiDownload_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles RequiDownload.DoWork
        Dim Worker As BackgroundWorker = CType(sender, BackgroundWorker)
        e.Result = DownloadRequi(Worker, e)
    End Sub

    '****************************************************************************************
    '****************************************************************************************
    '                    Reporte de estados de los hilos
    '****************************************************************************************
    '****************************************************************************************
    Private Sub RequiDownload_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles RequiDownload.ProgressChanged
        Me.ProgressBar1.Value = e.ProgressPercentage
        Me.txtStatus.Items.Add(My.Computer.Clock.LocalTime.ToString & " - " & Status)
    End Sub

    Private Sub oadownload_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles OADownload.ProgressChanged
        Me.ProgressBar1.Value = e.ProgressPercentage
        Me.txtStatus.Items.Add(My.Computer.Clock.LocalTime.ToString & " - " & Status)
    End Sub

    '****************************************************************************************
    '****************************************************************************************
    '                    Funciones que descargan la información para los hilos
    '****************************************************************************************
    '****************************************************************************************

    Function DownloadOA(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As String
        Dim Plantas As New DataSet
        Dim Table As New DataTable
        Plantas = cn.GetActivesPlants()

        Status = "Iniciando: Conexión para descarga de Contratos...."
        worker.ReportProgress(1)


        Dim L7Pass As DataTable
        Dim L7PW As String = ""
        L7Pass = cn.RunSentence("Select L7P From [Users] Where TNumber = 'BM4691'").Tables(0)


        If L7Pass.Rows.Count > 0 Then
            L7PW = cn.Encrypt(L7Pass.Rows(0).Item("L7P"))
        Else
            MsgBox("No Pass found.", MsgBoxStyle.Critical)
            Return ""
            Exit Function
        End If

        SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", "BM4691", L7PW, Me.chkSSO.Checked)

        Status = "Iniciando: Limpiando HeaderContrato."
        worker.ReportProgress(5)
        Access.DoCmd.RunSQL("Delete From HeaderContrato")

        Status = "Iniciando: Descarga header de contrato..."
        worker.ReportProgress(10)
        SAPCn.DownloadHeaderContrato(SAP)

        Status = "Iniciando: Importando Header contratos..."
        worker.ReportProgress(20)
        Access.Run("ImportHeaderContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKKO_Contratos.txt")

        Status = "Iniciando: Limpiando DetalleContrato."
        worker.ReportProgress(25)
        Access.DoCmd.RunSQL("Delete From DetalleContrato")

        Status = "Iniciando: Descarga de detalle de contratos..."
        worker.ReportProgress(30)
        SAPCn.DownloadDetalleContratos(SAP, Plantas)

        Status = "Iniciando: Importando Detalle de contratos..."
        worker.ReportProgress(40)
        Access.Run("ImportDetalleContrato", My.Application.Info.DirectoryPath & "\OADownLoad\EKPO_Contratos.txt")

        Status = "Iniciando: Exportando Detalle de contratos al Servidor"
        worker.ReportProgress(50)


        Table = cn.GetAccessTable("Select * From DetalleContrato").Tables(0)
        If Table.Rows.Count > 0 Then
            cn.ExecuteInServer("Delete From DetalleContrato")
            cn.AppendTableToSqlServer("DetalleContrato", Table)

            worker.ReportProgress(55)
            Status = "Finalizado: Exportado Detalle de contratos al Servidor"
        Else
            worker.ReportProgress(55)
            Status = "****** Finalizado: Fail!![Exportado Detalle de contratos al Servidor]"
        End If

        Status = "Iniciando: Exportando Header de contratos al Servidor"
        worker.ReportProgress(60)
        Table = cn.GetAccessTable("Select * From HeaderContrato").Tables(0)
        If Table.Rows.Count > 0 Then
            cn.ExecuteInServer("Delete from HeaderContrato")
            cn.AppendTableToSqlServer("HeaderContrato", Table)
            Status = "Finalizado: Exportado Header de contratos al Servidor"
            worker.ReportProgress(60)

            Status = ""
            worker.ReportProgress(65)
            Status = "***********************************************"
            worker.ReportProgress(70)

            Status = ""
            worker.ReportProgress(80)

        Else
            worker.ReportProgress(65)
            Status = "****** Finalizado: Fail!![Exportado Header de contratos al Servidor]"
        End If


        '*******************************************************************************************************
        '*******************************************************************************************************
        '             Requis:
        Status = "Iniciando: Descarga de tabla EBAN...."
        worker.ReportProgress(10)
        SAPCn.DownloadEban(SAP)

        Status = "Finalizado: Descarga de tabla EBAN...."
        worker.ReportProgress(20)


        Status = "Iniciando: Importando archivo descargado...."
        worker.ReportProgress(25)
        Access.Run("ImportEBAN", My.Application.Info.DirectoryPath & "\OADownLoad\EBAN.txt")

        Status = "Iniciando: Cargando DataTable...."
        worker.ReportProgress(30)
        Tabla = cn.GetAccessTable("Select * From Eban").Tables(0)

        If Tabla.Rows.Count > 0 Then
            Status = "Iniciando: Limpiando tabla en Servidor...."
            worker.ReportProgress(35)
            cn.ExecuteInServer("Delete From Eban")

            Status = "Iniciando: Realizando volcado de información al Servidor...."
            worker.ReportProgress(40)
            cn.AppendTableToSqlServer("EBAN", Tabla)

            Status = "Finalizado: Actualización de la información de las requisiciones."
            worker.ReportProgress(50)

        Else
            Status = "Status: Error al importar las Requisiciones"
            worker.ReportProgress(100)
        End If


        '*******************************************************************************************************
        '*******************************************************************************************************
        '             Master Data:

        Dim i As Integer
        Access.DoCmd.RunSQL("Delete From MasterData")

        For i = 0 To Plantas.Tables(0).Rows.Count - 1
            Status = "Iniciando: Descarga MD Planta." & Plantas.Tables(0).Rows(i).Item(0)
            worker.ReportProgress(i)

            SAPCn.DownloadMasterData(SAP, Plantas.Tables(0).Rows(i).Item(0))
            Access.Run("ImportMasterData", My.Application.Info.DirectoryPath & "\OADownLoad\Marc_" & Plantas.Tables(0).Rows(i).Item(0) & ".txt")
        Next

        Table = cn.GetAccessTable("Select * From MasterData").Tables(0)
        If Table.Rows.Count > 0 Then
            cn.ExecuteInServer("Delete From MasterData")
            cn.AppendTableToSqlServer("MasterData", Table)
        Else
            Status = "Status: Error al importar Master Data"
            worker.ReportProgress(100)
        End If


        '*******************************************************************************************************
        '*******************************************************************************************************
        '             Source List:
        Status = "Iniciando: Descarga de SourceList"
        worker.ReportProgress(10)

        Status = "Iniciando: Limpieza de Tabla Local"
        worker.ReportProgress(20)
        Access.DoCmd.RunSQL("Delete from EORD")

        Status = "Iniciando: Descarga de Source List"
        worker.ReportProgress(30)
        SAPCn.DownloadSourceList(SAP)

        Status = "Iniciando: Importando registros"
        worker.ReportProgress(50)
        Access.Run("ImportEORD", My.Application.Info.DirectoryPath & "\OADownLoad\EORD.txt")

        Status = "Iniciando: Cargando registros"
        worker.ReportProgress(60)
        Table = cn.GetAccessTable("Select * From EORD").Tables(0)

        If Table.Rows.Count > 0 Then
            Status = "Iniciando: Limpiando Source List del Servidor"
            worker.ReportProgress(70)
            cn.ExecuteInServer("Delete from EORD")

            Status = "Iniciando: Enviando registros al Servidor"
            worker.ReportProgress(80)
            cn.AppendTableToSqlServer("EORD", Table)
        Else
            Status = "Status: Error al importar Source List"
            worker.ReportProgress(100)
        End If

        '*******************************************************************************************************
        '*******************************************************************************************************
        '             Manufacters:

        Dim z As New SAPCOM.Manufacturers_Report("L7P", "BM4691", AppId)
        Dim r As DataRow

        If Plantas.Tables(0).Rows.Count > 0 Then
            For Each r In Plantas.Tables(0).Rows
                z.IncludePlant(r("Code"))
            Next
        End If

        z.Execute()
        If z.Success Then
            If z.ErrMessage = Nothing Then
                cn.ExecuteInServer("Delete from Manufacter")
                If z.Data.Columns.IndexOf("Client") <> -1 Then
                    z.Data.Columns.Remove("Client")
                End If

                cn.AppendTableToSqlServer("Manufacter", z.Data)
            End If
        End If

        SAPCn.CloseSession(SAP)
        Return Status
    End Function

    Function DownloadRequi(ByVal worker As BackgroundWorker, ByVal e As DoWorkEventArgs) As String
       
        '            Status = "Excecutting Query:"
        '            worker.ReportProgress(60)
       
        Status = "Iniciando: Conexión para descarga de requisiciones...."
        worker.ReportProgress(1)

        '        o_SAP.OpenConnection(SAPConfig)

        SAP = SAPCn.GetConnectionToSAP("L7P LA TS Prod", "BM4691", "", Me.chkSSO.Checked)


        Status = "Iniciando: Descarga de tabla EBAN...."
        worker.ReportProgress(10)
        SAPCn.DownloadEban(SAP)

        Status = "Finalizado: Descarga de tabla EBAN...."
        worker.ReportProgress(20)
        SAPCn.CloseSession(SAP)

        Status = "Iniciando: Importando archivo descargado...."
        worker.ReportProgress(25)
        Access.Run("ImportEBAN", My.Application.Info.DirectoryPath & "\OADownLoad\EBAN.txt")

        Status = "Iniciando: Cargando DataTable...."
        worker.ReportProgress(30)
        Tabla = cn.GetAccessTable("Select * From Eban").Tables(0)

        If Tabla.Rows.Count > 0 Then
            Status = "Iniciando: Limpiando tabla en Servidor...."
            worker.ReportProgress(35)
            cn.ExecuteInServer("Delete From Eban")

            Status = "Iniciando: Realizando volcado de información al Servidor...."
            worker.ReportProgress(40)
            cn.AppendTableToSqlServer("EBAN", Tabla)

            Status = "Finalizado: Actualización de la información de las requisiciones."
            worker.ReportProgress(50)

        Else
            Status = "Fianlizado: No data importada"
            worker.ReportProgress(100)
            Status = "Status: Error al importar las Requisiciones"
        End If

        Return Status
    End Function
    
    Private Sub frm000_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Access.OpenCurrentDatabase(My.Application.Info.DirectoryPath & "\OAData\New.mdb")
        Me.lblNextUpload.Text = "Now downloading...."
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Segundos > 0 Then
            Segundos -= 1
        End If

        If Segundos <> 0 Then
            If Segundos > 60 Then
                Me.lblNextUpload.Text = Mid(Segundos / 60, 1, 2) & " Min."
            Else
                Me.lblNextUpload.Text = Segundos.ToString & " Seg."
            End If
        Else
            'Inicia la descarga de la información
            If Me.chkInicio.Checked Then
                Segundos = 3600
                OADownload.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub chkInicio_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInicio.CheckedChanged

    End Sub
End Class
