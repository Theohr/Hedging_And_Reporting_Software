<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Home
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
        Me.btnReport1 = New System.Windows.Forms.Button()
        Me.btnReport2 = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAdjustment = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnReport1
        '
        Me.btnReport1.Location = New System.Drawing.Point(62, 12)
        Me.btnReport1.Name = "btnReport1"
        Me.btnReport1.Size = New System.Drawing.Size(75, 23)
        Me.btnReport1.TabIndex = 0
        Me.btnReport1.Text = "Daily Orders"
        Me.btnReport1.UseVisualStyleBackColor = True
        '
        'btnReport2
        '
        Me.btnReport2.Location = New System.Drawing.Point(62, 59)
        Me.btnReport2.Name = "btnReport2"
        Me.btnReport2.Size = New System.Drawing.Size(75, 23)
        Me.btnReport2.TabIndex = 1
        Me.btnReport2.Text = "Enquiries"
        Me.btnReport2.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(62, 105)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(62, 193)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Log Out"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnAdjustment
        '
        Me.btnAdjustment.Location = New System.Drawing.Point(62, 148)
        Me.btnAdjustment.Name = "btnAdjustment"
        Me.btnAdjustment.Size = New System.Drawing.Size(75, 23)
        Me.btnAdjustment.TabIndex = 4
        Me.btnAdjustment.Text = "Adjustment"
        Me.btnAdjustment.UseVisualStyleBackColor = True
        '
        'Home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(204, 241)
        Me.Controls.Add(Me.btnAdjustment)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnReport2)
        Me.Controls.Add(Me.btnReport1)
        Me.Name = "Home"
        Me.Text = "Home"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnReport1 As Button
    Friend WithEvents btnReport2 As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents btnAdjustment As Button
End Class
