Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop

Public Class dbReportData

    Dim myTable As New DataTable("Products")

    Public Function retreiveDataDanaos(ByVal tmpSqlCommand As String) As DataTable
        Dim orclCmd As New OracleCommand

        Try
            orclConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex, MessageBoxButtons.OK)
        End Try

        Try
            orclConn.Open()
        Catch ex As Exception
            MessageBox.Show(ex, MessageBoxButtons.OK)
        End Try

        ' Create Adapter
        Dim dr As OracleDataAdapter
        dr = New OracleDataAdapter(tmpSqlCommand, orclConn)

        myTable.Clear()
        dr.Fill(myTable)

        Try
            orclConn.Close()
        Catch ex As Exception

        End Try

        Return myTable
    End Function

    Public Sub exportReport1(ByVal tmpMyTable As DataTable)
        Try
            Dim excel As Excel.Application = New Excel.Application()
            Dim wBook As Excel.Workbook
            Dim wSheet As New Excel.Worksheet
            Dim dtc As DataColumn
            Dim dtr As DataRow
            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0

            'Check if path exists if not creates it automatically
            If Not System.IO.Directory.Exists("C:\Temp\StemDetailsExported\") Then
                System.IO.Directory.CreateDirectory("C:\Temp\StemDetailsExported\")
            End If

            'Adds a workbook
            wBook = excel.Workbooks.Add()
            wSheet = excel.ActiveSheet()

            'gets column index from datatable and adds them to excel cells
            For Each dtc In myTable.Columns
                colIndex = colIndex + 1
                excel.Cells(1, colIndex) = dtc.ColumnName
            Next

            'gets row index from datatable and adds values in each column
            For Each dtr In myTable.Rows
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dtc In myTable.Columns
                    colIndex = colIndex + 1
                    excel.Cells(rowIndex + 1, colIndex) = dtr(dtc.ColumnName).ToString
                Next
            Next

            wSheet.Columns.AutoFit()


            Dim stemArray() As String
            Dim stemDateDashes As String = ""

            '            stemArray = orderDate.Split("/")

            'Splits Stem Name and re adjusts with dashes for saving
            For i = 0 To stemArray.Length - 1
                If i = 0 Then
                    stemDateDashes = stemArray(i)
                Else
                    stemDateDashes = stemDateDashes + "-" + stemArray(i)
                End If
            Next

            Dim dateTimeNow = DateTime.Now.ToString

            Dim dtArray() As String
            dtArray = dateTimeNow.Split(" ")
            dateTimeNow = dtArray(1)
            Dim dtArray2() As String
            dtArray2 = dateTimeNow.Split(":")
            dateTimeNow = dtArray2(0) + "-" + dtArray2(1) + "-" + dtArray2(2) + dtArray(2)

            Dim strFileName As String = "C:\Temp\StemDetailsExported\STEMS-" & stemDateDashes & "-TIME-" & dateTimeNow & ".xlsx"

            '' If same file exists delete and replace
            'If System.IO.File.Exists(strFileName) Then
            '    System.IO.File.Delete(strFileName)
            'End If

            wBook.SaveAs(strFileName)
            wBook.Close()
            excel.Quit()

            MessageBox.Show("Data Exported Succesfully!", "Important Message", MessageBoxButtons.OK)
        Catch ex As Exception
            MessageBox.Show("There was an error exporting data. Please contact your IT Administrator.", "Important Message", MessageBoxButtons.OK)
        End Try
    End Sub

    Public Sub exportReport2(ByVal tmpMyTable As DataTable)
        Try
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

            'Check if path exists if not creates it automatically
            If Not System.IO.Directory.Exists("C:\Temp\StemDetailsExported\") Then
                System.IO.Directory.CreateDirectory("C:\Temp\StemDetailsExported\")
            End If

            saveFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx"
            saveFileDialog1.FilterIndex = 2
            saveFileDialog1.RestoreDirectory = True

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                savepath = saveFileDialog1.FileName.ToString() + saveFileDialog1.DefaultExt

            End If



            'Adds a workbook
            wBook = excel.Workbooks.Add()
            wSheet = excel.ActiveSheet()

            'gets column index from datatable and adds them to excel cells
            For Each dtc In myTable.Columns
                colIndex = colIndex + 1
                excel.Cells(1, colIndex) = dtc.ColumnName.ToString
            Next

            'gets row index from datatable and adds values in each column
            For Each dtr In myTable.Rows
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dtc In myTable.Columns
                    colIndex = colIndex + 1
                    excel.Cells(rowIndex + 1, colIndex) = dtr(dtc.ColumnName).ToString
                Next
            Next

            wSheet.Columns.AutoFit()


            'Dim stemArrayFrom() As String
            'Dim stemDateDashesFrom As String = ""


            ''stemArrayFrom = orderDateFrom.Split("/")

            ''Splits Stem Name and re adjusts with dashes for saving
            'For i = 0 To stemArrayFrom.Length - 1
            '    If i = 0 Then
            '        stemDateDashesFrom = stemArrayFrom(i)
            '    Else
            '        stemDateDashesFrom = stemDateDashesFrom + "-" + stemArrayFrom(i)
            '    End If
            'Next

            'Dim stemArrayTo() As String
            'Dim stemDateDashesTo As String = ""

            'stemArrayTo = orderDateTo.Split("/")

            'Splits Stem Name And re adjusts with dashes for saving
            'For i = 0 To stemArrayTo.Length - 1
            '    If i = 0 Then
            '        stemDateDashesTo = stemArrayTo(i)
            '    Else
            '        stemDateDashesTo = stemDateDashesTo + "-" + stemArrayTo(i)
            '    End If
            'Next

            'Dim dateTimeNow = DateTime.Now.ToString
            'Dim dtArray() As String
            'dtArray = dateTimeNow.Split(" ")
            'dateTimeNow = dtArray(1)
            'Dim dtArray2() As String
            'dtArray2 = dateTimeNow.Split(":")
            'dateTimeNow = dtArray2(0) + "-" + dtArray2(1) + "-" + dtArray2(2)  ''+"-" + dtArray(2)

            'Dim strFileName As String = "C:\Temp\StemDetailsExported\STEMS-" & stemDateDashesFrom & "-TO-" & stemDateDashesTo & "-TIME-" & dateTimeNow & ".xlsx"

            ' If same file exists delete and replace
            If System.IO.File.Exists(savepath) Then
                System.IO.File.Delete(savepath)
            End If


            wBook.SaveAs(savepath)
            wBook.Close()
            excel.Quit()



            MessageBox.Show("Data Exported Succesfully!", "Important Message", MessageBoxButtons.OK)
        Catch ex As Exception
            MessageBox.Show("There was an error exporting data. Please contact your IT Administrator.", "Important Message", MessageBoxButtons.OK)
        End Try
    End Sub

End Class
