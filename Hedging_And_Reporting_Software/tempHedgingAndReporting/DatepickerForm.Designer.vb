<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DatepickerForm
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
        Me.dtp = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'dtp
        '
        Me.dtp.Location = New System.Drawing.Point(12, 1)
        Me.dtp.Name = "dtp"
        Me.dtp.Size = New System.Drawing.Size(200, 20)
        Me.dtp.TabIndex = 0
        '
        'DatepickerForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(229, 23)
        Me.Controls.Add(Me.dtp)
        Me.Name = "DatepickerForm"
        Me.Text = "DatepickerForm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dtp As DateTimePicker
End Class
