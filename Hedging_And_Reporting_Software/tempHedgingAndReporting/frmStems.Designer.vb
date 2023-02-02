<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmStems
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtVessel = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.cmbConfirmationStatus = New System.Windows.Forms.ComboBox()
        Me.cmbPricingType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pricingDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.DeliverDateTimePicker = New System.Windows.Forms.DateTimePicker()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 66)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1185, 609)
        Me.DataGridView1.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(866, 15)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(93, 23)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search Stem"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Location = New System.Drawing.Point(746, 17)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(114, 20)
        Me.txtSearch.TabIndex = 2
        '
        'txtPort
        '
        Me.txtPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPort.Location = New System.Drawing.Point(59, 41)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(98, 20)
        Me.txtPort.TabIndex = 3
        '
        'txtVessel
        '
        Me.txtVessel.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtVessel.Location = New System.Drawing.Point(59, 14)
        Me.txtVessel.Name = "txtVessel"
        Me.txtVessel.Size = New System.Drawing.Size(98, 20)
        Me.txtVessel.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Port:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Vessel:"
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(746, 42)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(213, 22)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "Clear Search Criteria"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(1102, 17)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 40)
        Me.btnClose.TabIndex = 8
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'cmbConfirmationStatus
        '
        Me.cmbConfirmationStatus.FormattingEnabled = True
        Me.cmbConfirmationStatus.Items.AddRange(New Object() {"", "CO", "HO"})
        Me.cmbConfirmationStatus.Location = New System.Drawing.Point(284, 14)
        Me.cmbConfirmationStatus.Name = "cmbConfirmationStatus"
        Me.cmbConfirmationStatus.Size = New System.Drawing.Size(65, 21)
        Me.cmbConfirmationStatus.TabIndex = 9
        '
        'cmbPricingType
        '
        Me.cmbPricingType.AutoCompleteCustomSource.AddRange(New String() {"", "Spot", "Contract"})
        Me.cmbPricingType.FormattingEnabled = True
        Me.cmbPricingType.Location = New System.Drawing.Point(518, 17)
        Me.cmbPricingType.Name = "cmbPricingType"
        Me.cmbPricingType.Size = New System.Drawing.Size(95, 21)
        Me.cmbPricingType.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(180, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Delivered:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(442, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Pricing Type:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(180, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Confirmation Status:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(442, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Pricing Date:"
        '
        'pricingDateTimePicker
        '
        Me.pricingDateTimePicker.Checked = False
        Me.pricingDateTimePicker.Location = New System.Drawing.Point(516, 42)
        Me.pricingDateTimePicker.Name = "pricingDateTimePicker"
        Me.pricingDateTimePicker.Size = New System.Drawing.Size(179, 20)
        Me.pricingDateTimePicker.TabIndex = 16
        '
        'DeliverDateTimePicker
        '
        Me.DeliverDateTimePicker.Checked = False
        Me.DeliverDateTimePicker.Location = New System.Drawing.Point(241, 40)
        Me.DeliverDateTimePicker.Name = "DeliverDateTimePicker"
        Me.DeliverDateTimePicker.Size = New System.Drawing.Size(179, 20)
        Me.DeliverDateTimePicker.TabIndex = 17
        '
        'frmStems
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1209, 688)
        Me.Controls.Add(Me.DeliverDateTimePicker)
        Me.Controls.Add(Me.pricingDateTimePicker)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbPricingType)
        Me.Controls.Add(Me.cmbConfirmationStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtVessel)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmStems"
        Me.Text = "frmStems"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtVessel As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnClear As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents cmbConfirmationStatus As ComboBox
    Friend WithEvents cmbPricingType As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents pricingDateTimePicker As DateTimePicker
    Friend WithEvents DeliverDateTimePicker As DateTimePicker
End Class
