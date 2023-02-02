Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class frmUpdate

    Dim sqlCommand As String
    Dim myTable As New DataTable("Products")
    Dim danaosReportDataVar = New dbReportData()
    Dim stemNumber As String = ""
    Private dtp As DateTimePicker
    Dim productHistory = New frmProductHistory()

    Public Sub setStemNumber(ByVal currentStemParam As String)
        stemNumber = currentStemParam
    End Sub

    Private Sub frmUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Pricing Type First option selected
        cmbPricingType.SelectedIndex = -1
        txtSearch.Text = stemNumber

        'Order Date Time Format


        'Pricing Date Format
        pricingDatePicker.Format = DateTimePickerFormat.Custom
        pricingDatePicker.CustomFormat = " "

        Dim i As Integer

        With cmbPricingBasedOn
            .Items.Add("Delivery Date")
            .Items.Add("Pricing Date")
        End With
        For i = -30 To 30
            cmbNumOfDaysETA.Items.Add(i)
        Next

        search()
    End Sub

    ''' <summary>
    ''' Retreive Data from Danaos In a DataTable
    ''' </summary>
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

    ''' <summary>
    ''' Fill Grid and Text Boxes
    ''' </summary>
    Public Sub fillData()
        ' Send table to grid
        DataGridView1.DataSource = myTable

        ' make all columns Read Only except MIn Quantity
        DataGridView1.Columns("PCODE").ReadOnly = True
        DataGridView1.Columns("MAX_QTY").ReadOnly = True
        DataGridView1.Columns("SELL_PRICE").ReadOnly = True
        DataGridView1.Columns("DELIVERY_DATE").ReadOnly = True

        ' Make Visible only product columns
        DataGridView1.Columns("ORDER_DATE").Visible = False
        DataGridView1.Columns("VESSEL_NAME").Visible = False
        DataGridView1.Columns("PORT").Visible = False
        DataGridView1.Columns("ETA").Visible = False
        DataGridView1.Columns("DELIVERY_DATE").Visible = True
        DataGridView1.Columns("PRICING_DATE").Visible = False
        DataGridView1.Columns("PRICING_TYPE").Visible = False
        DataGridView1.Columns("CONFIRMATION_STATUS").Visible = False
        DataGridView1.Columns("HEDGE_PRICE").Visible = True
        DataGridView1.Columns("DELIVERED_QTY").Visible = True
        DataGridView1.Columns("ETA_NUM_OF_DAYS").Visible = False
        DataGridView1.Columns("PRICING_BASED_ON").Visible = False
        DataGridView1.Columns("stem_product_line").Visible = False

        Dim dtp As New DateTimePicker

        With DataGridView1

            Dim gridCMB As New DataGridViewComboBoxColumn

            With gridCMB
                .HeaderText = "HEDGING_INDEX"
                .Items.Add("")
                .Items.Add("CE GASOIL")
                .Items.Add("0.5 FOB Rdam Barge")
                .Items.Add("GO 0.1 FOB MED")
                .Items.Add("0.5 FOB MED")
                .Items.Add("0.5 CIF MED")
                .Items.Add("GO 0.1 CIF MED")
                .Items.Add("ICE Brent")
            End With

            .Columns.Remove(DataGridView1.Columns(2))
            .Columns.Insert(2, gridCMB)

        End With

        DataGridView1.AutoResizeColumns()

        For i = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        For i = 0 To myTable.Rows.Count - 1
            DataGridView1.Rows(i).Cells(2).Value = myTable.Rows(i).Item(2).ToString
        Next

        ' Load Rest data to text boxes
        txtEnqDate.Text = myTable.Rows(0).Item("ORDER_DATE").ToString
        txtVessel.Text = myTable.Rows(0).Item("VESSEL_NAME").ToString
        txtPort.Text = myTable.Rows(0).Item("PORT").ToString
        txtETA.Text = myTable.Rows(0).Item("ETA").ToString

        If myTable.Rows(0).Item("ETA_NUM_OF_DAYS").ToString <> "" Then
            cmbNumOfDaysETA.Text = myTable.Rows(0).Item("ETA_NUM_OF_DAYS").ToString
        Else
            cmbNumOfDaysETA.SelectedIndex = -1
        End If

        If myTable.Rows(0).Item("PRICING_BASED_ON").ToString <> "" Then
            cmbPricingBasedOn.Text = myTable.Rows(0).Item("PRICING_BASED_ON").ToString
        Else
            cmbPricingBasedOn.SelectedIndex = -1
        End If


        If myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString = "CO" Then
            txtStatus.Text = "Confirmed"
        ElseIf myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString = "BI" Then
            txtStatus.Text = "Billable"
        ElseIf myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString = "LO" Then
            txtStatus.Text = "Lost"
        ElseIf myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString = "IN" Then
            txtStatus.Text = "Invoiced"
        ElseIf myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString = "HO" Then
            txtStatus.Text = "Hold"
        Else
            txtStatus.Text = myTable.Rows(0).Item("CONFIRMATION_STATUS").ToString
        End If


        'If myTable.Rows(0).Item("DELIVERY_DATE").ToString <> "" Then
        '    deliveryDateTimePicker.Value = myTable.Rows(0).Item("DELIVERY_DATE").ToString
        'Else
        '    'Pricing Date Format
        '    deliveryDateTimePicker.Format = DateTimePickerFormat.Custom
        '    deliveryDateTimePicker.CustomFormat = " "
        'End If

        If myTable.Rows(0).Item("PRICING_DATE").ToString <> "" Then
            pricingDatePicker.Value = myTable.Rows(0).Item("PRICING_DATE").ToString
        Else
            'Pricing Date Format
            pricingDatePicker.Format = DateTimePickerFormat.Custom
            pricingDatePicker.CustomFormat = " "
        End If

        If myTable.Rows(0).Item("PRICING_TYPE").ToString <> "" Then
            cmbPricingType.Text = myTable.Rows(0).Item("PRICING_TYPE").ToString
        Else
            cmbPricingType.SelectedIndex = -1
        End If

        'If myTable.Rows(0).Item("DELIVERY_DATE").ToString <> "" Then
        '    cmbPricingType.Text = myTable.Rows(0).Item("DELIVERY_DATE").ToString
        'Else
        '    cmbPricingType.SelectedIndex = -1
        'End If

    End Sub

    Public Sub DataGridPopup()
        Dim da
    End Sub

    Public Sub search()
        txtEnqDate.Text = ""
        txtVessel.Text = ""
        txtPort.Text = ""
        txtETA.Text = ""
        txtStatus.Text = ""
        cmbPricingType.SelectedIndex = 0
        'pricingDatePicker.Value = " "
        'deliveryDateTimePicker.Value = " "
        myTable.Clear()
        DataGridView1.DataSource = myTable

        'Create Sql Command for Danaos DTBase
        If (txtSearch.Text <> "") Then
            'sqlCommandGrid = "select DISTINCT stem_products_main.PCODE, stem_products_main.QTY as MIN_QTY, stem_products_main.QTY as MAX_QTY, stem_products_main.SELL_PRICE FROM STEMS_MAIN INNER JOIN stem_products_main ON stems_main.stem_company=stem_products_main.stem_company AND stems_main.stem_series=stem_products_main.stem_series AND stems_main.stem_number=stem_products_main.stem_number where stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number='" & txtSearch.Text & "'"
            sqlCommand = "select DISTINCT stem_products_main.PCODE, stem_products_main.INTERNAL_HEDGING_PRICING, stem_products_main.HEDGING_INDEX,STEM_PRODUCTS_MAIN.DELIVER_QTY AS DELIVERED_QTY, STEM_PRODUCTS_MAIN.HEDGE_PRICE, stem_products_main.MIN_QTY, stem_products_main.QTY as MAX_QTY, stem_products_main.SELL_PRICE, stem_products_main.DELIVERY_DATE,(SELECT VESSEL_NAME FROM VESSEL_DATA WHERE stems_main.customer_vessel = VESSEL_DATA.VESSEL_CODE) AS VESSEL_NAME, stems_main.PRICING_DATE, stems_main.PRICING_TYPE, STEMS_MAIN.CONFIRMATION_STATUS, STEMS_MAIN.PORT, STEMS_MAIN.ETA, STEMS_MAIN.ORDER_DATE, STEMS_MAIN.ETA_NUM_OF_DAYS, STEMS_MAIN.PRICING_BASED_ON, stem_products_main.stem_product_line FROM STEMS_MAIN INNER JOIN stem_products_main ON stems_main.stem_company=stem_products_main.stem_company AND stems_main.stem_series=stem_products_main.stem_series AND stems_main.stem_number=stem_products_main.stem_number where (stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number='" & txtSearch.Text & "' OR (stems_main.stem_ref_company ||'/'|| stems_main.stem_ref_series || '/' || stems_main.stem_ref_number='" & txtSearch.Text & "' AND stems_main.stem_series = 'AJ')) AND (STEMS_MAIN.STEM_COMPANY = '003' OR STEMS_MAIN.STEM_COMPANY = '009')"

            'Retreive Data function
            retreiveData()
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim value = pricingDatePicker.Value
        If DataGridView1.Rows.Count > 0 Then
            If cmbPricingBasedOn.SelectedIndex = 1 And pricingDatePicker.CustomFormat = " " Then
                MessageBox.Show("Pricing Date cannot be Empty when Pricing is based on Pricing Date.", "Important Message", MessageBoxButtons.OK)
                Exit Sub
            Else
                If txtStatus.Text = "Confirmed" Or txtStatus.Text = "Invoiced" Then
                    updateDanaosProductData()
                    updateDanaosStemData()

                    MessageBox.Show("Data Updated Succesfully!", "Important Message", MessageBoxButtons.OK)
                Else
                    MessageBox.Show("Data cannot be updated because Stem is NOT in Confirmed Status.", "Important Message", MessageBoxButtons.OK)
                End If
            End If
        Else
            MessageBox.Show("Please search a stem first.", "Important Message", MessageBoxButtons.OK)
        End If

        Me.Close()
        frmStems.search()
    End Sub

    Public Sub updateDanaosProductData()
        Try
            orclConn.Close()
        Catch ex As Exception

        End Try
        orclConn.Open()

        Using orclCmd As New OracleCommand
            For i = 0 To DataGridView1.Rows.Count - 1
                Try
                    Dim hedgingPrice = 0.0
                    Dim hedgingIndex = DataGridView1.Rows(i).Cells(2).Value
                    Dim minQty = 0.0
                    Dim productLine = DataGridView1.Rows(i).Cells("stem_product_line").Value
                    Dim hedgePrice = 0.0
                    Dim deliverQTY = 0.0
                    Dim pCode = DataGridView1.Rows(i).Cells("PCODE").Value
                    Dim deliveryDate As String = ""
                    If IsDBNull(DataGridView1.Rows(i).Cells("INTERNAL_HEDGING_PRICING").Value) Then
                        hedgingPrice = 0.0
                    Else
                        hedgingPrice = DataGridView1.Rows(i).Cells("INTERNAL_HEDGING_PRICING").Value
                    End If
                    If IsDBNull(DataGridView1.Rows(i).Cells("MIN_QTY").Value) Then
                        minQty = 0.0
                    Else
                        minQty = DataGridView1.Rows(i).Cells("MIN_QTY").Value
                    End If

                    If IsDBNull(DataGridView1.Rows(i).Cells("DELIVERED_QTY").Value) Then
                        deliverQTY = 0.0
                    Else
                        deliverQTY = DataGridView1.Rows(i).Cells("DELIVERED_QTY").Value
                    End If

                    If IsDBNull(DataGridView1.Rows(i).Cells("HEDGE_PRICE").Value) Then
                        hedgePrice = 0.0
                    Else
                        hedgePrice = DataGridView1.Rows(i).Cells("HEDGE_PRICE").Value
                    End If

                    If IsDBNull(DataGridView1.Rows(i).Cells(8).Value) Then
                        deliveryDate = ""
                    Else
                        If DataGridView1.Rows(i).Cells(8).Value = "00:00:00" Then
                            deliveryDate = ""
                        Else
                            deliveryDate = DataGridView1.Rows(i).Cells(8).Value
                        End If
                    End If

                    Dim orclSQL = "UPDATE stem_products_main SET   stem_products_main.DELIVERY_DATE=TO_DATE('" & deliveryDate & "','dd/mm/yyyy hh24:mi:ss'),stem_products_main.HEDGE_PRICE = " & hedgePrice & " ,stem_products_main.DELIVER_QTY = " & deliverQTY & " ,stem_products_main.internal_hedging_pricing = " & hedgingPrice & " , stem_products_main.min_qty = " & minQty & ", stem_products_main.hedging_index = '" & hedgingIndex & "' where stem_products_main.pcode='" & pCode & "' AND stem_products_main.stem_company ||'/'|| stem_products_main.stem_series || '/' || stem_products_main.stem_number='" & txtSearch.Text & "' and stem_products_main.stem_product_line = '" & productLine & "'"

                    orclCmd.Connection = orclConn

                    orclCmd.CommandText = orclSQL


                    orclCmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
                End Try
            Next
        End Using

        orclConn.Close()
    End Sub

    Public Sub updateDanaosStemData()
        Try
            orclConn.Close()
        Catch ex As Exception

        End Try
        orclConn.Open()

        Using orclCmd As New OracleCommand
            Try

                Dim pricingDate = pricingDatePicker.Value
                Dim pricingType = cmbPricingType.Text
                Dim pricingBasedOn = cmbPricingBasedOn.Text
                Dim numOfDaysETA = cmbNumOfDaysETA.Text
                'Dim orderDateIDK = Format(CDate(orderDateTimePicker.Value.ToString), "dd-MM-yy")

                Dim onlyDatePricing

                If pricingDatePicker.CustomFormat <> " " Then
                    onlyDatePricing = Format(CDate(pricingDatePicker.Value.ToString), "dd/MM/yyyy HH:MM:ss")
                    onlyDatePricing = onlyDatePricing.ToUpper()
                Else
                    onlyDatePricing = ""
                End If


                'Dim orderDateIDK2 = Format(CDate(pricingDatePicker.Value.ToString), "dd-MM-yy")

                'Dim orclSQL = "UPDATE stems_main SET STEMS_MAIN.PRICING_BASED_ON='" & pricingBasedOn & "',STEMS_MAIN.ETA_NUM_OF_DAYS = TO_DATE('" & numOfDaysETA & "',  'dd/mm/yyyy hh24:mi:ss'), stems_main.PRICING_TYPE = '" & pricingType & "', stems_main.PRICING_DATE = TO_DATE('" & onlyDatePricing & "', 'dd/mm/yyyy hh24:mi:ss') where stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "'"
                Dim orclSQL = "UPDATE stems_main SET STEMS_MAIN.PRICING_BASED_ON='" & pricingBasedOn & "',STEMS_MAIN.ETA_NUM_OF_DAYS = '" & numOfDaysETA & "', stems_main.PRICING_TYPE = '" & pricingType & "', stems_main.PRICING_DATE = TO_DATE('" & onlyDatePricing & "', 'dd/mm/yyyy hh24:mi:ss') where stems_main.stem_company ||'/'|| stems_main.stem_series || '/' || stems_main.stem_number='" & txtSearch.Text & "'"

                orclCmd.Connection = orclConn

                orclCmd.CommandText = orclSQL

                orclCmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show("Something went wrong. Please contact your IT Administrator. Error Message:" + ex.Message.ToString, "Important Message", MessageBoxButtons.OK)
            End Try
        End Using

        orclConn.Close()
    End Sub

    Private Sub DataGridView1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridView1.EditingControlShowing

        If DataGridView1.CurrentCell.ColumnIndex = 1 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress1

        ElseIf DataGridView1.CurrentCell.ColumnIndex = 3 Then

            AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress1

        End If

    End Sub

    Private Sub TextBox_keyPress1(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = Convert.ToChar(Keys.Back) Or e.KeyChar = Convert.ToChar(Keys.Enter)) Then e.Handled = True

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub



    Private Sub pricingDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles pricingDatePicker.ValueChanged
        'Pricing Date Format
        pricingDatePicker.Format = DateTimePickerFormat.Custom
        pricingDatePicker.CustomFormat = "dd/MM/yyyy"
    End Sub

    Private Sub cmbNumOfDaysETA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNumOfDaysETA.SelectedIndexChanged


    End Sub

    Private Sub cmbPricieDateNumofDays_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPricingBasedOn.SelectedIndexChanged
        If cmbPricingBasedOn.SelectedIndex = 0 Then
            pricingDatePicker.Format = DateTimePickerFormat.Custom
            pricingDatePicker.CustomFormat = " "
            cmbNumOfDaysETA.Visible = True
        ElseIf cmbPricingBasedOn.SelectedIndex = 1 Then
            cmbNumOfDaysETA.Visible = False
        End If
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.ColumnIndex = 8 Then

            dtp = New DateTimePicker
            dtp.Visible = False
            dtp.Format = DateTimePickerFormat.Short
            dtp.Visible = True

            Try
                If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing Then
                    DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = ""
                End If
            Catch ex As Exception
                Exit Sub
            End Try


            Try
                dtp.Value = DateTime.Parse(DataGridView1.CurrentCell.Value.ToString())
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtp.Value.ToString
            Catch ex As Exception

            End Try



            Dim rect = DataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
            dtp.Size = New Size(rect.Width, rect.Height)
            dtp.Location = New Point(rect.X, rect.Y)

            dtp.Visible = True


            DataGridView1.Controls.Add(dtp)

            AddHandler dtp.TextChanged, AddressOf DateTimePickerChange
            ' An event attached to dateTimePicker1 which is fired when DateTimeControl is closed.
            AddHandler dtp.CloseUp, AddressOf DateTimePickerClose

        Else
            'Dim rowIndex = DataGridView1.CurrentCell.RowIndex()
            'Dim tmpPCode = DataGridView1.Rows(rowIndex).Cells("PCODE").Value.ToString()
            'Dim tmpStemProductLine = DataGridView1.Rows(rowIndex).Cells("STEM_PRODUCT_LINE").Value.ToString()

            'frmProductHistory.setStemNumber(stemNumber)
            'frmProductHistory.setProductLine(tmpStemProductLine)
            'frmProductHistory.setPCode(tmpPCode)
            'frmProductHistory.Show()
        End If
    End Sub

    Private Sub DateTimePickerChange(ByVal sender As Object, ByVal e As EventArgs)

        If DataGridView1.CurrentCell.ColumnIndex = 8 Then
            DataGridView1.CurrentCell.Value = dtp.Text.ToString()
        End If

        ''MessageBox.Show(String.Format("Date changed to {0}", dtp.Text.ToString()))
    End Sub

    Private Sub DateTimePickerClose(ByVal sender As Object, ByVal e As EventArgs)
        dtp.Visible = False
    End Sub

    Private Sub btnEmptyDateFields_Click(sender As Object, e As EventArgs) Handles btnEmptyDateFields.Click
        For i = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Cells(8).Value = ""
        Next
    End Sub
End Class
