Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions

Public Class frmProductHistory

    Dim myTable As New DataTable("ProductsHistory")
    Dim danaosReportDataVar = New dbReportData()
    Dim sqlCommand As String
    Dim stemNumber As String = ""
    Dim productLine As String
    Dim PCode As String = ""

    Public Sub setStemNumber(ByVal currentStemParam As String)
        stemNumber = currentStemParam
    End Sub

    Public Sub setProductLine(ByVal currentProductLine As String)
        productLine = currentProductLine
    End Sub

    Public Sub setPCode(ByVal currentPCodeParam As String)
        PCode = currentPCodeParam
    End Sub

    Private Sub frmProductHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadProductHistoryData()
    End Sub

    Public Sub loadProductHistoryData()
        search()
    End Sub

    Public Sub search()
        myTable.Clear()
        DataGridView2.DataSource = myTable
        'sqlCommandGrid = "select DISTINCT stem_products_main.PCODE, stem_products_main.QTY as MIN_QTY, stem_products_main.QTY as MAX_QTY, stem_products_main.SELL_PRICE FROM STEMS_MAIN INNER JOIN stem_products_main ON stems_main.stem_company=stem_products_main.stem_company AND stems_main.stem_series=stem_products_main.stem_series AND stems_main.stem_number=stem_products_main.stem_number where stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number='" & txtSearch.Text & "'"
        sqlCommand = "select DISTINCT stem_products_main_history.PCODE, stem_products_main_history.INTERNAL_HEDGING_PRICING, stem_products_main_history.HEDGING_INDEX, stem_products_main_history.DELIVER_QTY AS DELIVERED_QTY, stem_products_main_history.HEDGE_PRICE, stem_products_main_history.MIN_QTY, stem_products_main_history.QTY as MAX_QTY, stem_products_main_history.SELL_PRICE, stem_products_main_history.DELIVERY_DATE FROM stem_products_main_history where (stem_products_main_history.stem_company ||'/'|| stem_products_main_history.stem_series || '/' || stem_products_main_history.stem_number='" & stemNumber & "') AND stem_products_main_history.PCODE = '" & PCode & "' AND stem_products_main_history.STEM_PRODUCT_LINE = '" & productLine & "'"

        'Retreive Data function
        retreiveData()
    End Sub

    Public Sub retreiveData()
        'Call Function to fill from class danaosReportData
        myTable = danaosReportDataVar.retreiveDataDanaos(sqlCommand)

        If myTable.Rows.Count <> 0 Then
            'Fill data in form
            fillData()
        Else
            MessageBox.Show("No Data Found.", "Important Message", MessageBoxButtons.OK)
        End If
    End Sub

    Public Sub fillData()
        ' Send table to grid
        DataGridView2.DataSource = myTable

        Dim dtp As New DateTimePicker

        DataGridView2.AutoResizeColumns()

        For i = 0 To DataGridView2.Columns.Count - 1
            DataGridView2.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next


    End Sub


End Class