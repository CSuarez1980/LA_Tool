Module Variables

    Public Login As String = Nothing
    Public Login_Backup As String = Nothing
    Public InvoiceNumber_Global As String
    Public InvoiceRow_Global As New DataGridViewRow

    Public N6P_CS As String = "N6P NA Prod- SSO"
    Public L6P_CS As String = "L6P LA SC  Prod - SSO"
    Public N6A_CS As String = "N6A NA SC Acc - SSO"

    Public DB_CS As String = "Data Source=MXL0221QY0\SQLEXPRESS;Initial Catalog=PSSD_BI;Persist Security Info=True;User ID=developer;Password=procter"
    'Public DB_CS As String = "Data Source=131.190.71.90\SQLEXPRESS;Initial Catalog=PSSD_BI;Persist Security Info=True;User ID=developer;Password=procter"
    Public PathPlastipakFile As String = "C:\P&G\PSSD_BI\Excel\Plastipak_Notification(" & Date.Now.Month & "-" & Date.Now.Day & "-" & Date.Now.Year & ").xls"

    Public FlagView As Boolean = False

    Public Count_A As Double = 0
    Public Count_F As Double = 0
    Public Count_R As Double = 0

End Module
