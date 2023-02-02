Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class Adjustment

    Dim myTableStem As New DataTable("Stem")
    Dim myTableProduct As New DataTable("Products")
    Dim myTableStemRef As New DataTable("StemRef")
    Dim myTableRandom As New DataTable("Ran")
    Dim myTableAdjustment As New DataTable("Adj")
    Dim danaosReportDataVar = New dbReportData()
    Dim danaosReportDataStem = New dbReportData()
    Dim danaosReportDataProd = New dbReportData()
    Dim danaosReportDataRef = New dbReportData()
    Dim danaosReportDataAdj = New dbReportData()
    Dim danaosReportDataRand = New dbReportData()
    Public Sub createAdjustment()
        Dim excel As Excel.Application = New Excel.Application()
        Dim wBook As Excel.Workbook
        Dim wSheet As New Excel.Worksheet
        Dim dtc As DataColumn
        Dim dtr As DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0
        Dim myStream As Stream
        Dim saveFileDialog1 As New SaveFileDialog()
        Dim savepath As String = ""

        Dim openDialog As New OpenFileDialog()
        openDialog.Title = "Please select an Excel File:"
        'openDialog.InitialDirectory = "C:\Temp\"
        openDialog.Filter = "xlsx files (*.xlsx)|*.xlsx"
        openDialog.FilterIndex = 2
        openDialog.RestoreDirectory = True

        Try
            If openDialog.ShowDialog() = DialogResult.OK Then
                If openDialog.FileName <> "" Then
                    Dim fileName = openDialog.FileName
                    wBook = excel.Workbooks.Open(fileName)
                    wSheet = wBook.ActiveSheet
                    Dim colsCount As Integer = wSheet.UsedRange.Columns.Count
                    Dim rowsCount As Integer = wSheet.UsedRange.Rows.Count

                    For j = 2 To rowsCount
                        myTableStem.Clear()
                        myTableStem.Reset()
                        myTableProduct.Clear()
                        myTableProduct.Reset()
                        myTableStemRef.Clear()
                        myTableStemRef.Reset()
                        myTableRandom.Clear()
                        myTableRandom.Reset()
                        myTableAdjustment.Clear()
                        myTableAdjustment.Reset()

                        If wSheet.Cells(j, "A").Value <> "" Then
                            Dim aValue = wSheet.Cells(j, "A").Value
                            'GET ROW WITH STEM NUM VALUES
                            Dim sqlCommandStem = "SELECT * FROM STEMS_MAIN WHERE STEMS_MAIN.stem_company ||'/'|| STEMS_MAIN.stem_series || '/' || STEMS_MAIN.stem_number='" & aValue & "'"

                            myTableStem = danaosReportDataStem.retreiveDataDanaos(sqlCommandStem)

                            'if stem exists True
                            'if items exist True
                            'if remarks <> automatically created True 

                            If myTableStem.Rows.Count > 0 Then
                                Dim sqlCommandStemProducts = "SELECT * FROM STEM_PRODUCTS_MAIN WHERE STEM_PRODUCTS_MAIN.stem_company ||'/'|| STEM_PRODUCTS_MAIN.stem_series || '/' || STEM_PRODUCTS_MAIN.stem_number='" & aValue & "' AND PCODE='" & wSheet.Cells(j, "B").Value & "'"
                                myTableProduct = danaosReportDataProd.retreiveDataDanaos(sqlCommandStemProducts)

                                If myTableProduct.Rows.Count > 0 Then

                                    Dim str = myTableStem.Rows(0).Item("STEM_NUMBER").ToString
                                    Dim strArr() = str.Split("-")
                                    Dim fiscalYear = "20" + strArr(0)

                                    Dim sqlCommandLastAP = "select * FROM trd_journal_numbers WHERE company='" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "' AND fiscal_year='" & fiscalYear & "' AND journal_series='AP'"

                                    myTableRandom = danaosReportDataRand.retreiveDataDanaos(sqlCommandLastAP)

                                    If myTableRandom.Rows.Count > 0 Then
                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        Using orclCmd As New OracleCommand
                                            Try
                                                Dim orclSQL = "UPDATE trd_journal_numbers set last_number='" & Convert.ToInt32(myTableRandom.Rows(0).Item("LAST_NUMBER").ToString) + 1 & "' where company='" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "' AND fiscal_year='" & fiscalYear & "' AND journal_series='AP'"

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using

                                        orclConn.Close()
                                    Else
                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        Using orclCmd As New OracleCommand
                                            Try
                                                Dim orclSQL = "INSERT INTO trd_journal_numbers (company , fiscal_year , journal_series,last_number, record_version) VALUES ('" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "' ,'" & fiscalYear & "' , 'AP','1', '1')"

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using

                                        orclConn.Close()
                                    End If

                                    Dim sqlCommandAdjustment = "Select company as STEM_COMPANY, journal_series as STEM_SERIES, (SUBSTR(fiscal_year, 3, 2)||'-'||last_number) as STEM_NUMBER,company ||'/'|| journal_series ||'/'||(SUBSTR(fiscal_year, 3, 2)||'-'||last_number) as FULL_STEM_NUMBER FROM  trd_journal_numbers WHERE company='" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "' AND journal_series='AP'"
                                    myTableAdjustment = danaosReportDataAdj.retreiveDataDanaos(sqlCommandAdjustment)

                                    ' Gets the row with STEM REF NUM Values
                                    Dim sqlCommandStemRef = "SELECT * FROM STEMS_MAIN WHERE STEMS_MAIN.STEM_REF_COMPANY ||'/'|| STEMS_MAIN.STEM_REF_SERIES || '/' || STEMS_MAIN.STEM_REF_NUMBER='" & aValue & "'"
                                    myTableStemRef = danaosReportDataRef.retreiveDataDanaos(sqlCommandStemRef)

                                    ' Insert from Stem Ref Else from Stem
                                    If myTableStemRef.Rows.Count <> 0 Then
                                        'Insert Stems Main with stem ref
                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        Using orclCmd As New OracleCommand
                                            Try
                                                'Insert Stems Main Query
                                                Dim orclSQL = "INSERT INTO STEMS_MAIN 
                                                                     (STEM_COMPANY,STEM_SERIES,STEM_NUMBER,ORDER_DATE,
                                                                      TRADER,ACCOUNT,PORT,ETA,LOCAL_AGENT,SUPPLIER_TERMS,
                                                                      SUPPLIER,REMARKS,SUPPLIER_CODE_ACCOUNT,SUPPLIER_CODE_CONTACT,
                                                                      SUPPLIER_CODE_SUPPLIER, CUSTOMER_PO,ORDER_DATE_2,PRICING_TYPE,
                                                                      PRICING_DATE,DELIVERED,PRICING_BASED_ON,ETA_NUM_OF_DAYS,DELIVERY_DATE,
                                                                      STEM_REF_COMPANY,STEM_REF_SERIES,STEM_REF_NUMBER,SUPPLIER_TYPE,RECORD_VERSION)
                                                               VALUES
                                                                    ('" & myTableAdjustment.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ORDER_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("TRADER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("ACCOUNT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("PORT").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ETA").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                     '" & myTableStem.Rows(0).Item("LOCAL_AGENT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_TERMS").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER").ToString & "',
                                                                    'Automatically Created',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_ACCOUNT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_CONTACT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_SUPPLIER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("CUSTOMER_PO").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ORDER_DATE_2").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("PRICING_TYPE").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("PRICING_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("DELIVERED").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("PRICING_BASED_ON").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("ETA_NUM_OF_DAYS").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("DELIVERY_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_TYPE").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("RECORD_VERSION").ToString & "')
                                                                   "

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using



                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        'Insert PCode in Stems Product Main For Stem Ref
                                        Using orclCmd As New OracleCommand
                                            Try
                                                Dim orclSQL = "INSERT INTO STEM_PRODUCTS_MAIN 
                                                                     (STEM_COMPANY,STEM_SERIES,STEM_NUMBER,PCODE,
                                                                      UNIT_GROUP,UNIT_ID,CONVERSION_FACTOR,CURRENCY,QTY,BUY_PRICE,
                                                                      SELL_PRICE,COMMISSION,STEM_PRODUCT_LINE,SUPPLIER_TERMS,
                                                                      SUPPLIER, SERIAL_NO_SUPP,SUPPLIER_INVOICE_DATE,UNIT_GROUP_SEC,
                                                                      UNIT_ID_SEC,CONVERSION_FACTOR_SEC,QTY_SEC,RECORD_VERSION,SUPPLIER_CODE_SUPPLIER,
                                                                      MIN_QTY,INTERNAL_HEDGING_PRICING,HEDGING_INDEX,DELIVER_QTY,HEDGE_PRICE)
                                                               VALUES
                                                                    ('" & myTableAdjustment.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("PCODE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_GROUP").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_ID").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CONVERSION_FACTOR").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CURRENCY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("BUY_PRICE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SELL_PRICE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("COMMISSION").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("STEM_PRODUCT_LINE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER_TERMS").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SERIAL_NO_SUPP").ToString & "',
                                                                    TO_DATE('" & myTableProduct.Rows(0).Item("SUPPLIER_INVOICE_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_GROUP_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_ID_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CONVERSION_FACTOR_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("QTY_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("RECORD_VERSION").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER_CODE_SUPPLIER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("MIN_QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("INTERNAL_HEDGING_PRICING").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("HEDGING_INDEX").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("DELIVER_QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("HEDGE_PRICE").ToString & "')
                                                                   "

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using

                                        orclConn.Close()

                                        'Insert AdjNumber and Status in excel
                                        wSheet.Cells(j, colsCount - 1).Value = myTableAdjustment.Rows(0).Item("FULL_STEM_NUMBER").ToString
                                        wSheet.Cells(j, colsCount).Value = "Success"
                                    ElseIf myTableStem.Rows.Count <> 0 Or myTableStem.Rows(0).Item("REMARKS").ToString <> "Automatically Created" Then
                                        'Insert Stems Main with stem
                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        Using orclCmd As New OracleCommand
                                            Try
                                                'Insert Stems Main Query
                                                Dim orclSQL = "INSERT INTO STEMS_MAIN 
                                                                     (STEM_COMPANY,STEM_SERIES,STEM_NUMBER,ORDER_DATE,
                                                                      TRADER,ACCOUNT,PORT,ETA,LOCAL_AGENT,SUPPLIER_TERMS,
                                                                      SUPPLIER,REMARKS,SUPPLIER_CODE_ACCOUNT,SUPPLIER_CODE_CONTACT,
                                                                      SUPPLIER_CODE_SUPPLIER, CUSTOMER_PO,ORDER_DATE_2,PRICING_TYPE,
                                                                      PRICING_DATE,DELIVERED,PRICING_BASED_ON,ETA_NUM_OF_DAYS,DELIVERY_DATE,
                                                                      STEM_REF_COMPANY,STEM_REF_SERIES,STEM_REF_NUMBER,SUPPLIER_TYPE,RECORD_VERSION)
                                                               VALUES
                                                                    ('" & myTableAdjustment.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ORDER_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("TRADER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("ACCOUNT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("PORT").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ETA").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                     '" & myTableStem.Rows(0).Item("LOCAL_AGENT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_TERMS").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER").ToString & "',
                                                                    'Automatically Created',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_ACCOUNT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_CONTACT").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_CODE_SUPPLIER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("CUSTOMER_PO").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("ORDER_DATE_2").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("PRICING_TYPE").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("PRICING_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("DELIVERED").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("PRICING_BASED_ON").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("ETA_NUM_OF_DAYS").ToString & "',
                                                                    TO_DATE('" & myTableStem.Rows(0).Item("DELIVERY_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableStem.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("SUPPLIER_TYPE").ToString & "',
                                                                    '" & myTableStem.Rows(0).Item("RECORD_VERSION").ToString & "')
                                                                   "

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using

                                        Try
                                            orclConn.Close()
                                        Catch ex As Exception

                                        End Try
                                        orclConn.Open()

                                        'Insert PCode in Stems Product Main For Stem
                                        Using orclCmd As New OracleCommand
                                            Try
                                                Dim orclSQL = "INSERT INTO STEM_PRODUCTS_MAIN 
                                                                     (STEM_COMPANY,STEM_SERIES,STEM_NUMBER,PCODE,
                                                                      UNIT_GROUP,UNIT_ID,CONVERSION_FACTOR,CURRENCY,QTY,BUY_PRICE,
                                                                      SELL_PRICE,COMMISSION,STEM_PRODUCT_LINE,SUPPLIER_TERMS,
                                                                      SUPPLIER, SERIAL_NO_SUPP,SUPPLIER_INVOICE_DATE,UNIT_GROUP_SEC,
                                                                      UNIT_ID_SEC,CONVERSION_FACTOR_SEC,QTY_SEC,RECORD_VERSION,SUPPLIER_CODE_SUPPLIER,
                                                                      MIN_QTY,INTERNAL_HEDGING_PRICING,HEDGING_INDEX,DELIVER_QTY,HEDGE_PRICE)
                                                               VALUES
                                                                    ('" & myTableAdjustment.Rows(0).Item("STEM_COMPANY").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_SERIES").ToString & "',
                                                                    '" & myTableAdjustment.Rows(0).Item("STEM_NUMBER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("PCODE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_GROUP").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_ID").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CONVERSION_FACTOR").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CURRENCY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("BUY_PRICE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SELL_PRICE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("COMMISSION").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("STEM_PRODUCT_LINE").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER_TERMS").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SERIAL_NO_SUPP").ToString & "',
                                                                    TO_DATE('" & myTableProduct.Rows(0).Item("SUPPLIER_INVOICE_DATE").ToString & "','dd/mm/yyyy hh24:mi:ss'),
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_GROUP_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("UNIT_ID_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("CONVERSION_FACTOR_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("QTY_SEC").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("RECORD_VERSION").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("SUPPLIER_CODE_SUPPLIER").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("MIN_QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("INTERNAL_HEDGING_PRICING").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("HEDGING_INDEX").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("DELIVER_QTY").ToString & "',
                                                                    '" & myTableProduct.Rows(0).Item("HEDGE_PRICE").ToString & "')
                                                                   "

                                                orclCmd.Connection = orclConn

                                                orclCmd.CommandText = orclSQL

                                                orclCmd.ExecuteNonQuery()

                                            Catch ex As Exception
                                                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                                            End Try
                                        End Using

                                        orclConn.Close()

                                        'Insert AdjNumber and Status in excel
                                        wSheet.Cells(j, colsCount - 1).Value = myTableAdjustment.Rows(0).Item("FULL_STEM_NUMBER").ToString
                                        wSheet.Cells(j, colsCount).Value = "Success"
                                    Else
                                        wSheet.Cells(j, colsCount - 1).Value = "N/A"
                                        wSheet.Cells(j, colsCount).Value = "Fail"
                                    End If
                                End If
                            End If
                        End If
                    Next

                    wBook.Save()
                    wBook.Close()
                    excel.Quit()
                Else
                    MessageBox.Show("Please choose a valid Excel File.", "Important Information", MessageBoxButtons.OK)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Please choose a valid Excel File.", "Important Information", MessageBoxButtons.OK)
        End Try
    End Sub

End Class
