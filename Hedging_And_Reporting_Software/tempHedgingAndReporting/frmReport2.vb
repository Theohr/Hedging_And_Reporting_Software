Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmReport2

    Dim myTable As New DataTable("Products")
    Dim danaosReportDataVar = New dbReportData()
    Dim sqlCommand As String

    Private Sub frmReport2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dateTimePickerFrom.Format = DateTimePickerFormat.Custom
        dateTimePickerFrom.CustomFormat = "dd/MM/yy"

        dateTimePickerTo.Format = DateTimePickerFormat.Custom
        dateTimePickerTo.CustomFormat = "dd/MM/yy"
    End Sub

    Private Sub btnExportReport2_Click(sender As Object, e As EventArgs) Handles btnExportReport2.Click
        Dim onlyDateFrom = Format(CDate(dateTimePickerFrom.Value.ToString), "dd-MMM-yy")
        onlyDateFrom = onlyDateFrom.ToUpper()

        Dim onlyDateTo = Format(CDate(dateTimePickerTo.Value.ToString), "dd-MMM-yy")
        onlyDateTo = onlyDateTo.ToUpper()



        sqlCommand = "SELECT DISTINCT CONCAT(stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number, stem_products_main.PCODE) AS CONCATENATE,
                        stems_main.ORDER_DATE as ENQUIRY_DATE,  
                        STEM_PRODUCTS_MAIN.DELIVERY_DATE AS DELIVERY_DATE, stem_products_main.qty, STEMS_MAIN.PORT, STEMS_MAIN.TRADER, stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number as STEM_NUMBER,
                         stems_main.pricing_based_on AS PRICING_BASED_ON
                        ,
                        
                        (SELECT VESSEL_NAME FROM VESSEL_DATA WHERE stems_main.customer_vessel = VESSEL_DATA.VESSEL_CODE) AS VESSEL_NAME,
                        (CASE WHEN stems_main.pricing_type='Contract'
                        
                         THEN
                            (SELECT SUPPLIER_ADDRESSES.NAME FROM SUPPLIER_ADDRESSES WHERE STEMS_MAIN.ACCOUNT = SUPPLIER_ADDRESSES.SUPPLIER AND SUPPLIER_ADDRESSES.SUPPLIER_CATEGORY = 'X')
                         ELSE
                            ''
                         END) AS CUSTOMER,
                        (CASE WHEN stems_main.pricing_TYPE ='Contract' 
                       
                        THEN 
                            'Yes'
                        ELSE  
                            'No'
                        END) AS IS_CONTRACT,
                        (SELECT IMO_NO FROM VESSEL_DATA WHERE stems_main.customer_vessel = VESSEL_DATA.VESSEL_CODE) AS IMO_NUMBER,
                        stem_products_main.MIN_QTY, stem_products_main.QTY as MAX_QTY, stem_products_main.SELL_PRICE, stem_products_main.PCODE, 
                        STEM_PRODUCTS_MAIN.INTERNAL_HEDGING_PRICING, stem_products_main.HEDGING_INDEX, STEMS_MAIN.ETA, STEMS_MAIN.PRICING_TYPE, 
                        stems_main.pricing_based_on AS PRICING_BASED_ON
                        ,
                        
                        (CASE WHEN stems_main.pricing_based_on ='ETA' 
                       
                        THEN 
                            ADD_MONTHS (TO_DATE(stems_main.eta,'DD/MM/YYYY') + stems_main.eta_num_of_days,0)
                        WHEN stems_main.pricing_based_on ='Pricing Date' 
                        
                        THEN 
                            ADD_MONTHS (TO_DATE(stems_main.pricing_date,'DD/MM/YYYY') + stems_main.eta_num_of_days,0)
                        ELSE  
                            NULL
                        END) AS PRICING_DATE_AUTO_CALC,
                        stems_main.eta_num_of_days AS NUM_OF_DAYS,
                        STEMS_MAIN.PRICING_DATE, STEMS_MAIN.CONFIRMATION_STATUS,
                        (stem_products_main.QTY * stem_products_main.SELL_PRICE) AS GROSS_NOTIONAL

                        FROM STEMS_MAIN INNER JOIN stem_products_main ON stems_main.stem_company=stem_products_main.stem_company 
                        AND stems_main.stem_series=stem_products_main.stem_series 
                        AND stems_main.stem_number=stem_products_main.stem_number where stems_main.ORDER_DATE  BETWEEN '" & onlyDateFrom & "' AND '" & onlyDateTo & "' AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009') ORDER BY ORDER_DATE"

        myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)
        danaosReportDataVar.exportReport2(myTable)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class