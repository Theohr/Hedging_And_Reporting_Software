<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dateTimePickerOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnExportReport1 = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dateTimePickerOrderDate
        '
        Me.dateTimePickerOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dateTimePickerOrderDate.Location = New System.Drawing.Point(108, 23)
        Me.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate"
        Me.dateTimePickerOrderDate.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePickerOrderDate.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Order Date/Time:"
        '
        'btnExportReport1
        '
        Me.btnExportReport1.Location = New System.Drawing.Point(57, 68)
        Me.btnExportReport1.Name = "btnExportReport1"
        Me.btnExportReport1.Size = New System.Drawing.Size(75, 23)
        Me.btnExportReport1.TabIndex = 2
        Me.btnExportReport1.Text = "Export"
        Me.btnExportReport1.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(183, 68)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmReport1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(327, 120)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExportReport1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTimePickerOrderDate)
        Me.Name = "frmReport1"
        Me.Text = "Daily Orders"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dateTimePickerOrderDate As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents btnExportReport1 As Button
    Friend WithEvents btnClose As Button
End Class
