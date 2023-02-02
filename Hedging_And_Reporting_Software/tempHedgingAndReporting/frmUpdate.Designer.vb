<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmUpdate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pricingDatePicker = New System.Windows.Forms.DateTimePicker()
        Me.cmbPricingType = New System.Windows.Forms.ComboBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtEnqDate = New System.Windows.Forms.TextBox()
        Me.txtVessel = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtETA = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.cmbNumOfDaysETA = New System.Windows.Forms.ComboBox()
        Me.cmbPricingBasedOn = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnEmptyDateFields = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(688, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Pricing Date:"
        '
        'pricingDatePicker
        '
        Me.pricingDatePicker.Location = New System.Drawing.Point(762, 19)
        Me.pricingDatePicker.Name = "pricingDatePicker"
        Me.pricingDatePicker.Size = New System.Drawing.Size(191, 20)
        Me.pricingDatePicker.TabIndex = 5
        '
        'cmbPricingType
        '
        Me.cmbPricingType.FormattingEnabled = True
        Me.cmbPricingType.Items.AddRange(New Object() {"Spot", "Contract"})
        Me.cmbPricingType.Location = New System.Drawing.Point(426, 47)
        Me.cmbPricingType.Name = "cmbPricingType"
        Me.cmbPricingType.Size = New System.Drawing.Size(174, 21)
        Me.cmbPricingType.TabIndex = 6
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(9, 222)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1067, 211)
        Me.DataGridView1.TabIndex = 11
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(436, 497)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Enquiry Date:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Vessel:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Port:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "ETA:"
        '
        'txtEnqDate
        '
        Me.txtEnqDate.Location = New System.Drawing.Point(89, 14)
        Me.txtEnqDate.Name = "txtEnqDate"
        Me.txtEnqDate.ReadOnly = True
        Me.txtEnqDate.Size = New System.Drawing.Size(141, 20)
        Me.txtEnqDate.TabIndex = 18
        '
        'txtVessel
        '
        Me.txtVessel.Location = New System.Drawing.Point(89, 43)
        Me.txtVessel.Name = "txtVessel"
        Me.txtVessel.ReadOnly = True
        Me.txtVessel.Size = New System.Drawing.Size(141, 20)
        Me.txtVessel.TabIndex = 19
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(89, 73)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.ReadOnly = True
        Me.txtPort.Size = New System.Drawing.Size(141, 20)
        Me.txtPort.TabIndex = 20
        '
        'txtETA
        '
        Me.txtETA.Location = New System.Drawing.Point(89, 101)
        Me.txtETA.Name = "txtETA"
        Me.txtETA.ReadOnly = True
        Me.txtETA.Size = New System.Drawing.Size(141, 20)
        Me.txtETA.TabIndex = 21
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(330, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 22
        Me.Label9.Text = "Pricing Type:"
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(496, 166)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.ReadOnly = True
        Me.txtSearch.Size = New System.Drawing.Size(118, 20)
        Me.txtSearch.TabIndex = 23
        Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(89, 131)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(77, 20)
        Me.txtStatus.TabIndex = 25
        Me.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Status:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(445, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Stem"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(569, 497)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 30
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'cmbNumOfDaysETA
        '
        Me.cmbNumOfDaysETA.FormattingEnabled = True
        Me.cmbNumOfDaysETA.Location = New System.Drawing.Point(535, 18)
        Me.cmbNumOfDaysETA.Name = "cmbNumOfDaysETA"
        Me.cmbNumOfDaysETA.Size = New System.Drawing.Size(69, 21)
        Me.cmbNumOfDaysETA.TabIndex = 31
        '
        'cmbPricingBasedOn
        '
        Me.cmbPricingBasedOn.FormattingEnabled = True
        Me.cmbPricingBasedOn.Location = New System.Drawing.Point(426, 18)
        Me.cmbPricingBasedOn.Name = "cmbPricingBasedOn"
        Me.cmbPricingBasedOn.Size = New System.Drawing.Size(103, 21)
        Me.cmbPricingBasedOn.TabIndex = 32
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(330, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 13)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Pricing Based on:"
        '
        'btnEmptyDateFields
        '
        Me.btnEmptyDateFields.Location = New System.Drawing.Point(978, 19)
        Me.btnEmptyDateFields.Name = "btnEmptyDateFields"
        Me.btnEmptyDateFields.Size = New System.Drawing.Size(75, 23)
        Me.btnEmptyDateFields.TabIndex = 35
        Me.btnEmptyDateFields.Text = "Empty Dates"
        Me.btnEmptyDateFields.UseVisualStyleBackColor = True
        '
        'frmUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1091, 533)
        Me.Controls.Add(Me.btnEmptyDateFields)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cmbNumOfDaysETA)
        Me.Controls.Add(Me.cmbPricingBasedOn)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtETA)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtVessel)
        Me.Controls.Add(Me.txtEnqDate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.cmbPricingType)
        Me.Controls.Add(Me.pricingDatePicker)
        Me.Controls.Add(Me.Label2)
        Me.Name = "frmUpdate"
        Me.Text = "Danaos Stems"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents pricingDatePicker As DateTimePicker
    Friend WithEvents cmbPricingType As ComboBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnSave As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtEnqDate As TextBox
    Friend WithEvents txtVessel As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtETA As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnClose As Button
    Friend WithEvents cmbNumOfDaysETA As ComboBox
    Friend WithEvents cmbPricingBasedOn As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btnEmptyDateFields As Button
End Class
