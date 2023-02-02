Imports Oracle.ManagedDataAccess.Client
Imports System
Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop


Public Class Home

    Dim createAJ As New Adjustment()
    Dim myTableUsers As New DataTable("Users")
    Dim danaosReportDataVar = New dbReportData()
    Dim danaosReportDataStem = New dbReportData()
    Dim danaosReportDataProd = New dbReportData()
    Dim danaosReportDataRef = New dbReportData()
    Dim danaosReportDataAdj = New dbReportData()
    Dim danaosReportDataRand = New dbReportData()


    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        frmStems.Show()
    End Sub

    Private Sub btnReport2_Click(sender As Object, e As EventArgs) Handles btnReport2.Click
        frmReport2.Show()
    End Sub

    Private Sub btnReport1_Click(sender As Object, e As EventArgs) Handles btnReport1.Click
        frmReport1.Show()
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        myTableUsers.Clear()
        myTableUsers.Reset()


        Dim username = Environment.UserName
        Dim sqlCommandStem = "SELECT DISTINCT * FROM EMPLOYEES inner join TRD_EMPLOYEES ON employees.user_login=trd_employees.user_login"
        'Dim sqlCommandStem = "SELECT DISTINCT * FROM EMPLOYEES inner join TRD_EMPLOYEES ON employees.user_login=trd_employees.user_login WHERE employees.user_domain_name='aristos.a'"
        'Dim sqlCommandStem = "SELECT DISTINCT * FROM EMPLOYEES inner join TRD_EMPLOYEES ON employees.user_login=trd_employees.user_login WHERE employees.user_domain_name='" & username & "'"
        myTableUsers = danaosReportDataVar.retreiveDataDanaos(sqlCommandStem)
        Dim Status = ""
        Try
            Status = myTableUsers.Rows(0).Item("ACCESS_TO_VB_APP").ToString
            'If Status = "N" Or String.IsNullOrEmpty(Status) Then

            If username = "theodoros.h" Or username = "aristos.a" Or username = "gregoris.g" Then
                btnUpdate.Visible = True
                btnAdjustment.Visible = True
                btnReport1.Visible = True
                btnReport2.Visible = True
            ElseIf username = "george.p" And (Status = "Y") Then
                btnUpdate.Visible = False
                btnAdjustment.Visible = False
                btnReport1.Visible = True
                btnReport2.Visible = True

            ElseIf username = "andreas.h" Or username = "kyproula.k" Or username = "marios.a" And (Status = "Y") Then
                btnAdjustment.Visible = False
                btnReport1.Visible = True
                btnReport2.Visible = True
                btnUpdate.Visible = True
            ElseIf username = "panayiotis.e" And (Status = "Y") Then
                btnAdjustment.Visible = True
                btnReport1.Visible = False
                btnReport2.Visible = False
                btnUpdate.Visible = False
            Else
                MessageBox.Show("You have no access to the application.", "Important Message", MessageBoxButtons.OK)
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("You have no access to the application.", "Important Message", MessageBoxButtons.OK)
            Me.Close()
        End Try

    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdjustment_Click(sender As Object, e As EventArgs) Handles btnAdjustment.Click

        createAJ.createAdjustment()
    End Sub
End Class