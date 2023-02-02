Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmStems
    Dim sqlCommand As String
    Dim myTable As New DataTable("Products")
    Dim danaosReportDataVar = New dbReportData()
    Dim updateStem As New frmUpdate()
    Dim pricingDate As Date
    Dim minutes As Integer = 1   ' change 5 into the count of minutes
    Dim t As New System.Timers.Timer(6000 * minutes)



    Private Sub frmStems_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSearch.Enabled = True
        pricingDateTimePicker.Format = DateTimePickerFormat.Custom
        pricingDateTimePicker.CustomFormat = "dd-MM-yyyy"
        DeliverDateTimePicker.Format = DateTimePickerFormat.Custom
        DeliverDateTimePicker.CustomFormat = "dd-MM-yyyy"
        'pricingDateTimePicker.CustomFormat = "  "

        'DeliverDateTimePicker.Format = DateTimePickerFormat.Custom
        'DeliverDateTimePicker.CustomFormat = " "
        cmbPricingType.Items.Add("")
        cmbPricingType.Items.Add("Spot")
        cmbPricingType.Items.Add("Contract")


        search()

        AddHandler t.Elapsed, AddressOf t_Elapsed
        ''t.Start()




    End Sub



    Private Sub t_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        search()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            ' Create Temp InputBox and Transfer value to txtSearch
            ''Dim tmpStemNumber As String
            ''tmpStemNumber = InputBox("Please enter stem key (ex. 003/CP/21-1)", "Search By Stem Key")
            ''txtSearch.Text = tmpStemNumber

            'Search stem
            search()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub search()
        'Create Sql Command for Danaos DTBase

        '        sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER ,
        'stems_main.ORDER_DATE as ENQUIRY_DATE
        ',VESSEL_DATA.VESSEL_NAME,
        'STEMS_MAIN.CONFIRMATION_STATUS,
        'STEMS_MAIN.PORT, STEMS_MAIN.ETA,
        'STEM_PRODUCTS_MAIN.DELIVERY_DATE,    
        'stems_main.PRICING_DATE, stems_main.PRICING_TYPE
        'FROM STEMS_MAIN
        'INNER JOIN VESSEL_DATA ON stems_main.customer_vessel=VESSEL_DATA.VESSEL_CODE
        'INNER JOIN STEM_PRODUCTS_MAIN ON stems_main.stem_company || stems_main.stem_series || stems_main.stem_number=stem_PRODUCTs_main.stem_company || stem_PRODUCTs_main.stem_series || stem_PRODUCTs_main.stem_number
        'WHERE
        '(STEMS_MAIN.CONFIRMATION_STATUS = 'CO' OR STEMS_MAIN.CONFIRMATION_STATUS = 'IN') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009')
        'AND (STEMS_MAIN.STEM_SERIES <> 'GB' AND STEMS_MAIN.STEM_SERIES <> 'KB' AND STEMS_MAIN.STEM_SERIES <> 'EB' AND STEMS_MAIN.STEM_SERIES <> 'SB' AND STEMS_MAIN.STEM_SERIES <> 'LC') AND ORDER_DATE >= '01-JAN-22' "
        sqlCommand = "WITH CTE AS (select (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER ,
stems_main.ORDER_DATE as ENQUIRY_DATE
,VESSEL_DATA.VESSEL_NAME,
STEMS_MAIN.CONFIRMATION_STATUS,
STEMS_MAIN.PORT, STEMS_MAIN.ETA,
stem_products_main.DELIVERY_DATE,
stems_main.PRICING_DATE, stems_main.PRICING_TYPE
FROM STEMS_MAIN
INNER JOIN VESSEL_DATA ON stems_main.customer_vessel=VESSEL_DATA.VESSEL_CODE
INNER JOIN STEM_PRODUCTS_MAIN ON stems_main.stem_company || stems_main.stem_series || stems_main.stem_number=stem_PRODUCTs_main.stem_company || stem_PRODUCTs_main.stem_series || stem_PRODUCTs_main.stem_number
WHERE
(STEMS_MAIN.CONFIRMATION_STATUS = 'CO' OR STEMS_MAIN.CONFIRMATION_STATUS = 'IN' OR STEMS_MAIN.CONFIRMATION_STATUS = 'BI') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009')
AND (STEMS_MAIN.STEM_SERIES <> 'GB' AND STEMS_MAIN.STEM_SERIES <> 'KB' AND STEMS_MAIN.STEM_SERIES <> 'EB' AND STEMS_MAIN.STEM_SERIES <> 'SB' AND STEMS_MAIN.STEM_SERIES <> 'LC') AND ORDER_DATE >= '01-JAN-22' "

        If (String.IsNullOrEmpty(txtSearch.Text)) Then
            sqlCommand = sqlCommand & String.Empty
        Else
            sqlCommand = sqlCommand & " AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number LIKE '%" & txtSearch.Text & "%'"
        End If

        If (String.IsNullOrEmpty(txtVessel.Text)) Then
            sqlCommand = sqlCommand & String.Empty
        Else
            sqlCommand = sqlCommand & "  AND VESSEL_NAME LIKE '%" & txtVessel.Text & "%'"
        End If


        If (String.IsNullOrEmpty(txtPort.Text)) Then
            sqlCommand = sqlCommand & String.Empty
        Else
            sqlCommand = sqlCommand & "  AND STEMS_MAIN.PORT LIKE '" & txtPort.Text & "%'"
        End If

        Try
            If (Nothing Or String.IsNullOrEmpty(cmbPricingType.Text)) Then
                sqlCommand = sqlCommand & String.Empty
            Else
                sqlCommand = sqlCommand & "  AND   stems_main.PRICING_TYPE= '" & cmbPricingType.SelectedItem.ToString & "'"
            End If
        Catch ex As Exception
            sqlCommand = sqlCommand & String.Empty

        End Try

        Try
            If (Nothing Or String.IsNullOrEmpty(cmbConfirmationStatus.Text)) Then
                sqlCommand = sqlCommand & String.Empty
            Else
                sqlCommand = sqlCommand & "  AND  STEMS_MAIN.CONFIRMATION_STATUS= '" & cmbConfirmationStatus.SelectedItem.ToString & "'"
            End If
        Catch ex As Exception
            sqlCommand = sqlCommand & String.Empty

        End Try


        Try
            If (pricingDateTimePicker.Checked = False) Then
                sqlCommand = sqlCommand & String.Empty
            Else
                sqlCommand = sqlCommand & "  AND  stems_main.PRICING_DATE LIKE TO_DATE('" & pricingDateTimePicker.Text & "%','DD-MM-YYYY  HH24:MI:SS')"
            End If
        Catch ex As Exception
            sqlCommand = sqlCommand & String.Empty

        End Try

        Try
            If (DeliverDateTimePicker.Checked = False) Then
                sqlCommand = sqlCommand & String.Empty
            Else
                sqlCommand = sqlCommand & "  AND  STEMS_MAIN.DELIVERY_DATE LIKE TO_DATE('" & DeliverDateTimePicker.Text & "%','DD-MM-YYYY  HH24:MI:SS')"
            End If
        Catch ex As Exception
            sqlCommand = sqlCommand & String.Empty

        End Try

        Try
            sqlCommand = sqlCommand & " ORDER BY enquiry_date DESC FETCH FIRST 200 ROWS ONLY"
        Catch ex As Exception
            sqlCommand = sqlCommand & String.Empty
        End Try

        sqlCommand = sqlCommand & " ) SELECT MAX(STEM_NUMBER) AS STEM_NUMBER, MAX(ENQUIRY_DATE) AS ENQUIRY_DATE, MAX(VESSEL_NAME) AS VESSEL_NAME, MAX(CONFIRMATION_STATUS) AS CONFIRMATION_STATUS, MAX(PORT) AS PORT, MAX(ETA) AS ETA, MAX(DELIVERY_DATE) AS DELIVERY_DATE, MAX(PRICING_DATE) AS PRICING_DATE, MAX(PRICING_TYPE) AS PRICING_TYPE FROM CTE GROUP BY STEM_NUMBER"

        'Try
        '    If (String.IsNullOrEmpty(deliveryDate.ToString)) Then
        '        sqlCommand = sqlCommand & String.Empty
        '    Else
        '        'sqlCommand = sqlCommand & "  AND  STEMS_MAIN.DELIVERY_DATE= '" & DeliverDateTimePicker.Value.ToString & "'"
        '        sqlCommand = sqlCommand & String.Empty
        '    End If
        'Catch ex As Exception
        '    sqlCommand = sqlCommand & String.Empty
        'End Try

        'If (txtSearch.Text <> "") Then
        '    'sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN where stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO' OR STEMS_MAIN.CONFIRMATION_STATUS = 'HO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        '    sqlCommand = sqlCommand & " AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "'"
        'Else
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE, stems_main.PRICING_DATE, stems_main.PRICING_TYPE FROM STEMS_MAIN where (STEMS_MAIN.CONFIRMATION_STATUS = 'CO' OR STEMS_MAIN.CONFIRMATION_STATUS = 'HO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC')"
        'End If
        System.Console.WriteLine("sql=" & sqlCommand)

        myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)
        fillData()
        'Retreive Data function
        ''0retreiveData()
    End Sub

    Public Sub retreiveData()
        If myTable.Rows.Count <> 0 Then
            'Call Function to fill from class danaosReportData
            myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

            'Fill data in form
            fillData()
        Else
            MessageBox.Show("No Data Found.", "Important Message", MessageBoxButtons.OK)
        End If
    End Sub

    Public Sub fillData()
        DataGridView1.DataSource = myTable
        DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        frmUpdate.setStemNumber(DataGridView1.CurrentRow.Cells("STEM_NUMBER").Value.ToString)
        frmUpdate.Show()
    End Sub

    Private Sub txtVessel_TextChanged(sender As Object, e As EventArgs) Handles txtVessel.TextChanged
        'If txtVessel.Text = "" And txtSearch.Text = "" And txtPort.Text = "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE, stems_main.PRICING_DATE, stems_main.PRICING_TYPE FROM STEMS_MAIN where (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtVessel.Text = "" Or txtVessel.Text <> "") And txtSearch.Text <> "" And txtPort.Text = "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtVessel.Text = "" Or txtVessel.Text <> "") And txtSearch.Text <> "" And txtPort.Text <> "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where stems_main.PORT like '%" & txtPort.Text & "%' AND VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtVessel.Text = "" Or txtVessel.Text <> "") And txtSearch.Text = "" And txtPort.Text <> "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where stems_main.PORT like '%" & txtPort.Text & "%' AND VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'Else
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'End If

        'myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)
        search()
        'Retreive Data function
        ''retreiveData()
    End Sub

    Private Sub txtPort_TextChanged(sender As Object, e As EventArgs) Handles txtPort.TextChanged
        'If txtPort.Text = "" And txtSearch.Text = "" And txtVessel.Text = "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE, stems_main.PRICING_DATE, stems_main.PRICING_TYPE FROM STEMS_MAIN where (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtPort.Text = "" Or txtPort.Text <> "") And txtSearch.Text <> "" And txtVessel.Text = "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN where stems_main.PORT like '%" & txtPort.Text & "%' AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtPort.Text = "" Or txtPort.Text <> "") And txtSearch.Text <> "" And txtVessel.Text <> "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where stems_main.PORT like '%" & txtPort.Text & "%' AND VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'ElseIf (txtPort.Text = "" Or txtPort.Text <> "") And txtSearch.Text = "" And txtVessel.Text <> "" Then
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN INNER JOIN VESSELS_LIST ON stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE where stems_main.PORT like '%" & txtPort.Text & "%' AND VESSELS_LIST.VESSEL_NAME like '%" & txtVessel.Text & "%' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'Else
        '    sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN where STEMS_MAIN.PORT like '%" & txtPort.Text & "%' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"
        'End If

        'myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)
        search()
        'Retreive Data function
        ''retreiveData()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
        txtVessel.Text = ""
        txtPort.Text = ""
        cmbConfirmationStatus.Text = ""
        cmbPricingType.SelectedIndex = -1
        pricingDateTimePicker.Checked = False
        DeliverDateTimePicker.Checked = False
        sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE, stems_main.PRICING_DATE, stems_main.PRICING_TYPE FROM STEMS_MAIN where (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"

        myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        'Retreive Data function
        'retreiveData()
        fillData()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub pricingDateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles pricingDateTimePicker.ValueChanged
        'pricingDateTimePicker.Format = DateTimePickerFormat.Custom
        ''pricingDateTimePicker.CustomFormat = "dd/MM/yy"

        'Dim onlyDateOrder = Format(CDate(pricingDateTimePicker.Value.ToString), "dd-MMM-yy HH:MM:ss")
        'onlyDateOrder = onlyDateOrder.ToUpper()

        'Dim splitArray() = onlyDateOrder.Split(" ")

        'sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN where STEMS_MAIN.PRICING_DATE like '%" & splitArray(0) & "%' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"

        'myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        'Retreive Data function
        pricingDateTimePicker.Checked = True
        search()
        ''retreiveData()
    End Sub

    Private Sub cmbConfirmationStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbConfirmationStatus.SelectedIndexChanged
        'sqlCommand = "select DISTINCT (stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number) As STEM_NUMBER , stems_main.ORDER_DATE as ENQUIRY_DATE,(SELECT VESSEL_NAME FROM VESSELS_LIST WHERE stems_main.customer_vessel = VESSELS_LIST.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.DELIVERY_DATE AS DELIVERY_DATE FROM STEMS_MAIN where STEMS_MAIN.CONFIRMATION_STATUS = '" & cmbConfirmationStatus.Text & "' AND (STEMS_MAIN.CONFIRMATION_STATUS = 'CO' OR STEMS_MAIN.CONFIRMATION_STATUS = 'HO') AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') AND (STEM_SERIES <> 'GB' AND STEM_SERIES <> 'KB' AND STEM_SERIES <> 'EB' AND STEM_SERIES <> 'SB' AND STEM_SERIES <> 'LC') ORDER BY ORDER_DATE"

        'myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        'Retreive Data function
        search()
        '' retreiveData()
    End Sub

    Private Sub cmbDelivered_SelectedIndexChanged(sender As Object, e As EventArgs)
        myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        'Retreive Data function
        retreiveData()
    End Sub

    Private Sub cmbPricingType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPricingType.SelectedIndexChanged


        ''myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        'Retreive Data function
        search()
        '' retreiveData()
    End Sub

    Private Sub DeliverDateTimePicker_ValueChanged(sender As Object, e As EventArgs) Handles DeliverDateTimePicker.ValueChanged
        ''deliveryDate = DeliverDateTimePicker.Value.ToString
        DeliverDateTimePicker.Checked = True
        search()
        '' retreiveData()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        search()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class