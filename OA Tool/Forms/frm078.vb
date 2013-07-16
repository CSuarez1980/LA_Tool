Public Class frm078

    Private Sub frm078_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim V
        Dim cn As New OAConnection.Connection
        Dim Data_Local As New DataTable
        Dim POs As New DataTable
        Dim Row As DataRow
        Dim R2PO As DataTable
        Dim Print As DataTable
        Dim OTD As DataTable
        Dim AutoMMR As DataTable
        Dim AutoPO As DataTable
        Dim Automation As DataTable
        Dim BI As DataTable

        Data_Local = cn.RunSentence("Select * From vst_SC_Sumary").Tables(0)
        POs = cn.RunSentence("SELECT Plant, [Total POs], [Total Items] FROM Get_SC_Sumary_Qty_POs('" & gsUsuarioPC & "') Where Spend = 'Local'").Tables(0)
        R2PO = cn.RunSentence("Select * From get_SC_Sumary_Rec_To_PO('" & gsUsuarioPC & "') Where Spend = 'Local' ").Tables(0)
        Print = cn.RunSentence("Select * From get_SC_Printed('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        OTD = cn.RunSentence("Select * From fn_Get_SC_OTD_Summary('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        AutoMMR = cn.RunSentence("Select * From fn_Get_SC_AUTO_MMR_Sumary('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        AutoPO = cn.RunSentence("Select * From fn_Get_SC_Sumary_AUTO_PO('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        Automation = cn.RunSentence("Select * From fn_Get_SC_Total_Automation('" & gsUsuario & "') Where Spend = 'Local'").Tables(0)
        BI = cn.RunSentence("Select * From fn_Get_SC_BI_Unblock_Sumary('','') Where Spend = 'National'").Tables(0)


        For Each Row In Data_Local.Rows
            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("Total Items")).SingleOrDefault)
            Row("Total PO Items") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In POs.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("Total POs")).SingleOrDefault)
            Row("Total POs") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In R2PO.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("% Req To PO")).SingleOrDefault)
            Row("% Req To PO") = IIf(Not V Is Nothing, V, 0)

            V = ((From C In Print.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("Total")).SingleOrDefault)
            Row("% Transmition") = IIf(Not V Is Nothing, ((V / Row("Total POs")) * 100), DBNull.Value)

            V = ((From C In OTD.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("% OTD")).SingleOrDefault)
            Row("% OTD") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoMMR.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("AutoMMR")).SingleOrDefault)
            Row("MMR") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In AutoPO.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("% Auto")).SingleOrDefault)
            Row("Cat / Auto PO") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In Automation.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("Total")).SingleOrDefault)
            Row("Total Automation") = IIf(Not V Is Nothing, ((V / Row("Total PO Items")) * 100), DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("Total BI")).SingleOrDefault)
            Row("Total BI") = IIf(Not V Is Nothing, V, DBNull.Value)

            V = ((From C In BI.AsEnumerable() Where C.Item("Plant") = Row("Plant") Select C.Item("% BI")).SingleOrDefault)
            Row("% BI On Time") = IIf(Not V Is Nothing, V, DBNull.Value)

        Next

        cn.ExportDataTableToXL(Data_Local)


    End Sub
End Class