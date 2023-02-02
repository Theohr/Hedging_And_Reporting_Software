<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport2
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
        Me.dateTimePickerFrom = New System.Windows.Forms.DateTimePicker()
        Me.dateTimePickerTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnExportReport2 = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dateTimePickerFrom
        '
        Me.dateTimePickerFrom.Location = New System.Drawing.Point(67, 22)
        Me.dateTimePickerFrom.Name = "dateTimePickerFrom"
        Me.dateTimePickerFrom.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePickerFrom.TabIndex = 0
        '
        'dateTimePickerTo
        '
        Me.dateTimePickerTo.Location = New System.Drawing.Point(67, 68)
        Me.dateTimePickerTo.Name = "dateTimePickerTo"
        Me.dateTimePickerTo.Size = New System.Drawing.Size(200, 20)
        Me.dateTimePickerTo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(28, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To:"
        '
        'btnExportReport2
        '
        Me.btnExportReport2.Location = New System.Drawing.Point(53, 112)
        Me.btnExportReport2.Name = "btnExportReport2"
        Me.btnExportReport2.Size = New System.Drawing.Size(75, 23)
        Me.btnExportReport2.TabIndex = 4
        Me.btnExportReport2.Text = "Export"
        Me.btnExportReport2.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(182, 112)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmReport2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 158)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExportReport2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dateTimePickerTo)
        Me.Controls.Add(Me.dateTimePickerFrom)
        Me.Name = "frmReport2"
        Me.Text = "Enquiries"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dateTimePickerFrom As DateTimePicker
    Friend WithEvents dateTimePickerTo As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnExportReport2 As Button
    Friend WithEvents btnClose As Button
End Class
