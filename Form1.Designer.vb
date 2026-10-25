<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtCustomerSearch = New System.Windows.Forms.TextBox()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.lblMobile = New System.Windows.Forms.Label()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.grpAddCustomer = New System.Windows.Forms.GroupBox()
        Me.btnAddCustomer = New System.Windows.Forms.Button()
        Me.lblNewAddress = New System.Windows.Forms.Label()
        Me.lblNewMobile = New System.Windows.Forms.Label()
        Me.lblNewName = New System.Windows.Forms.Label()
        Me.txtNewAddress = New System.Windows.Forms.TextBox()
        Me.txtNewMobile = New System.Windows.Forms.TextBox()
        Me.txtNewCustomerName = New System.Windows.Forms.TextBox()
        Me.txtWeight = New System.Windows.Forms.TextBox()
        Me.txtCurrentRate = New System.Windows.Forms.TextBox()
        Me.lblCurrentRate = New System.Windows.Forms.Label()
        Me.txtCut = New System.Windows.Forms.TextBox()
        Me.lblCut = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.lblPerchiNo = New System.Windows.Forms.Label()
        Me.txtPerchiNo = New System.Windows.Forms.TextBox()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.txtTime = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnHistory = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnDailyReport = New System.Windows.Forms.Button()
        Me.Panelheader = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btnCustomers = New System.Windows.Forms.Button()
        Me.btnKhata = New System.Windows.Forms.Button()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.btnWhatsApp = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboPortName = New System.Windows.Forms.ComboBox()
        Me.cboBaudRate = New System.Windows.Forms.ComboBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnGetWeight = New System.Windows.Forms.Button()
        Me.chkAutoRead = New System.Windows.Forms.CheckBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.txtCashOut = New System.Windows.Forms.TextBox()
        Me.txtCashIn = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lable1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAdvance = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUdhar = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOpenWorth = New System.Windows.Forms.Button()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtGold = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.txtName1 = New System.Windows.Forms.TextBox()
        Me.txtGinni = New System.Windows.Forms.TextBox()
        Me.txtPure = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRattiIn = New System.Windows.Forms.TextBox()
        Me.btnSubmitGinni = New System.Windows.Forms.Button()
        Me.btnGinni = New System.Windows.Forms.Button()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.btnOpenperchi = New System.Windows.Forms.Button()
        Me.btnOpenDashboard = New System.Windows.Forms.Button()
        Me.grpAddCustomer.SuspendLayout()
        Me.Panelheader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.DarkRed
        Me.btnSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Location = New System.Drawing.Point(43, 158)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(92, 43)
        Me.btnSearch.TabIndex = 0
        Me.btnSearch.Text = "Customer Search "
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'txtCustomerSearch
        '
        Me.txtCustomerSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerSearch.Location = New System.Drawing.Point(183, 163)
        Me.txtCustomerSearch.Multiline = True
        Me.txtCustomerSearch.Name = "txtCustomerSearch"
        Me.txtCustomerSearch.Size = New System.Drawing.Size(215, 31)
        Me.txtCustomerSearch.TabIndex = 10
        Me.txtCustomerSearch.TabStop = False
        Me.txtCustomerSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCustomerName
        '
        Me.lblCustomerName.AutoSize = True
        Me.lblCustomerName.ForeColor = System.Drawing.Color.Navy
        Me.lblCustomerName.Location = New System.Drawing.Point(428, 151)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(85, 13)
        Me.lblCustomerName.TabIndex = 3
        Me.lblCustomerName.Text = "Customer Name:"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.cmbCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(511, 140)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(145, 28)
        Me.cmbCustomer.TabIndex = 0
        '
        'lblMobile
        '
        Me.lblMobile.AutoSize = True
        Me.lblMobile.ForeColor = System.Drawing.Color.Navy
        Me.lblMobile.Location = New System.Drawing.Point(429, 184)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.Size = New System.Drawing.Size(58, 13)
        Me.lblMobile.TabIndex = 5
        Me.lblMobile.Text = "Mobile No:"
        '
        'txtMobile
        '
        Me.txtMobile.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.Location = New System.Drawing.Point(511, 171)
        Me.txtMobile.Multiline = True
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(145, 30)
        Me.txtMobile.TabIndex = 0
        Me.txtMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblAddress
        '
        Me.lblAddress.AutoSize = True
        Me.lblAddress.ForeColor = System.Drawing.Color.Navy
        Me.lblAddress.Location = New System.Drawing.Point(429, 213)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.Size = New System.Drawing.Size(48, 13)
        Me.lblAddress.TabIndex = 7
        Me.lblAddress.Text = "Address:"
        '
        'txtAddress
        '
        Me.txtAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(511, 204)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(145, 30)
        Me.txtAddress.TabIndex = 0
        Me.txtAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'grpAddCustomer
        '
        Me.grpAddCustomer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.grpAddCustomer.BackColor = System.Drawing.Color.AliceBlue
        Me.grpAddCustomer.Controls.Add(Me.btnAddCustomer)
        Me.grpAddCustomer.Controls.Add(Me.lblNewAddress)
        Me.grpAddCustomer.Controls.Add(Me.lblNewMobile)
        Me.grpAddCustomer.Controls.Add(Me.lblNewName)
        Me.grpAddCustomer.Controls.Add(Me.txtNewAddress)
        Me.grpAddCustomer.Controls.Add(Me.txtNewMobile)
        Me.grpAddCustomer.Controls.Add(Me.txtNewCustomerName)
        Me.grpAddCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.grpAddCustomer.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAddCustomer.ForeColor = System.Drawing.Color.Navy
        Me.grpAddCustomer.Location = New System.Drawing.Point(34, 204)
        Me.grpAddCustomer.Name = "grpAddCustomer"
        Me.grpAddCustomer.Size = New System.Drawing.Size(382, 132)
        Me.grpAddCustomer.TabIndex = 10
        Me.grpAddCustomer.TabStop = False
        Me.grpAddCustomer.Text = "Add New Customer"
        '
        'btnAddCustomer
        '
        Me.btnAddCustomer.BackColor = System.Drawing.Color.DarkRed
        Me.btnAddCustomer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCustomer.ForeColor = System.Drawing.Color.White
        Me.btnAddCustomer.Location = New System.Drawing.Point(251, 31)
        Me.btnAddCustomer.Name = "btnAddCustomer"
        Me.btnAddCustomer.Size = New System.Drawing.Size(100, 46)
        Me.btnAddCustomer.TabIndex = 11
        Me.btnAddCustomer.Text = "Add Customer"
        Me.btnAddCustomer.UseVisualStyleBackColor = False
        '
        'lblNewAddress
        '
        Me.lblNewAddress.AutoSize = True
        Me.lblNewAddress.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewAddress.Location = New System.Drawing.Point(9, 95)
        Me.lblNewAddress.Name = "lblNewAddress"
        Me.lblNewAddress.Size = New System.Drawing.Size(65, 20)
        Me.lblNewAddress.TabIndex = 10
        Me.lblNewAddress.Text = "Address:"
        '
        'lblNewMobile
        '
        Me.lblNewMobile.AutoSize = True
        Me.lblNewMobile.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewMobile.Location = New System.Drawing.Point(7, 57)
        Me.lblNewMobile.Name = "lblNewMobile"
        Me.lblNewMobile.Size = New System.Drawing.Size(59, 20)
        Me.lblNewMobile.TabIndex = 9
        Me.lblNewMobile.Text = "Mobile:"
        '
        'lblNewName
        '
        Me.lblNewName.AutoSize = True
        Me.lblNewName.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewName.Location = New System.Drawing.Point(6, 27)
        Me.lblNewName.Name = "lblNewName"
        Me.lblNewName.Size = New System.Drawing.Size(104, 20)
        Me.lblNewName.TabIndex = 8
        Me.lblNewName.Text = "Add Customer"
        '
        'txtNewAddress
        '
        Me.txtNewAddress.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewAddress.Location = New System.Drawing.Point(116, 89)
        Me.txtNewAddress.Multiline = True
        Me.txtNewAddress.Name = "txtNewAddress"
        Me.txtNewAddress.Size = New System.Drawing.Size(257, 33)
        Me.txtNewAddress.TabIndex = 0
        Me.txtNewAddress.TabStop = False
        Me.txtNewAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNewMobile
        '
        Me.txtNewMobile.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewMobile.Location = New System.Drawing.Point(117, 57)
        Me.txtNewMobile.Name = "txtNewMobile"
        Me.txtNewMobile.Size = New System.Drawing.Size(114, 25)
        Me.txtNewMobile.TabIndex = 0
        Me.txtNewMobile.TabStop = False
        Me.txtNewMobile.Text = "03"
        Me.txtNewMobile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtNewCustomerName
        '
        Me.txtNewCustomerName.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNewCustomerName.Location = New System.Drawing.Point(116, 25)
        Me.txtNewCustomerName.Name = "txtNewCustomerName"
        Me.txtNewCustomerName.Size = New System.Drawing.Size(115, 25)
        Me.txtNewCustomerName.TabIndex = 0
        Me.txtNewCustomerName.TabStop = False
        Me.txtNewCustomerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWeight
        '
        Me.txtWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWeight.Location = New System.Drawing.Point(511, 237)
        Me.txtWeight.Multiline = True
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(145, 30)
        Me.txtWeight.TabIndex = 1
        Me.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCurrentRate
        '
        Me.txtCurrentRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrentRate.Location = New System.Drawing.Point(511, 270)
        Me.txtCurrentRate.Multiline = True
        Me.txtCurrentRate.Name = "txtCurrentRate"
        Me.txtCurrentRate.Size = New System.Drawing.Size(145, 30)
        Me.txtCurrentRate.TabIndex = 2
        Me.txtCurrentRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCurrentRate
        '
        Me.lblCurrentRate.AutoSize = True
        Me.lblCurrentRate.ForeColor = System.Drawing.Color.Navy
        Me.lblCurrentRate.Location = New System.Drawing.Point(429, 280)
        Me.lblCurrentRate.Name = "lblCurrentRate"
        Me.lblCurrentRate.Size = New System.Drawing.Size(67, 13)
        Me.lblCurrentRate.TabIndex = 13
        Me.lblCurrentRate.Text = "CurrentRate:"
        '
        'txtCut
        '
        Me.txtCut.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCut.Location = New System.Drawing.Point(511, 303)
        Me.txtCut.Multiline = True
        Me.txtCut.Name = "txtCut"
        Me.txtCut.Size = New System.Drawing.Size(145, 30)
        Me.txtCut.TabIndex = 3
        Me.txtCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCut
        '
        Me.lblCut.AutoSize = True
        Me.lblCut.ForeColor = System.Drawing.Color.Navy
        Me.lblCut.Location = New System.Drawing.Point(433, 313)
        Me.lblCut.Name = "lblCut"
        Me.lblCut.Size = New System.Drawing.Size(26, 13)
        Me.lblCut.TabIndex = 15
        Me.lblCut.Text = "Cut:"
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.LightYellow
        Me.txtAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.Location = New System.Drawing.Point(511, 502)
        Me.txtAmount.Multiline = True
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(145, 30)
        Me.txtAmount.TabIndex = 0
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.ForeColor = System.Drawing.Color.Navy
        Me.lblAmount.Location = New System.Drawing.Point(433, 514)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(46, 13)
        Me.lblAmount.TabIndex = 19
        Me.lblAmount.Text = "Amount:"
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.LightYellow
        Me.txtRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(511, 336)
        Me.txtRate.Multiline = True
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReadOnly = True
        Me.txtRate.Size = New System.Drawing.Size(145, 30)
        Me.txtRate.TabIndex = 4
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.ForeColor = System.Drawing.Color.Navy
        Me.lblRate.Location = New System.Drawing.Point(433, 346)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(33, 13)
        Me.lblRate.TabIndex = 17
        Me.lblRate.Text = "Rate:"
        '
        'lblPerchiNo
        '
        Me.lblPerchiNo.AutoSize = True
        Me.lblPerchiNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPerchiNo.ForeColor = System.Drawing.Color.Navy
        Me.lblPerchiNo.Location = New System.Drawing.Point(43, 109)
        Me.lblPerchiNo.Name = "lblPerchiNo"
        Me.lblPerchiNo.Size = New System.Drawing.Size(70, 16)
        Me.lblPerchiNo.TabIndex = 21
        Me.lblPerchiNo.Text = "Perchi No:"
        '
        'txtPerchiNo
        '
        Me.txtPerchiNo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtPerchiNo.BackColor = System.Drawing.Color.AliceBlue
        Me.txtPerchiNo.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPerchiNo.ForeColor = System.Drawing.Color.Black
        Me.txtPerchiNo.Location = New System.Drawing.Point(124, 101)
        Me.txtPerchiNo.Multiline = True
        Me.txtPerchiNo.Name = "txtPerchiNo"
        Me.txtPerchiNo.ReadOnly = True
        Me.txtPerchiNo.Size = New System.Drawing.Size(150, 30)
        Me.txtPerchiNo.TabIndex = 22
        Me.txtPerchiNo.TabStop = False
        Me.txtPerchiNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Navy
        Me.lblDate.Location = New System.Drawing.Point(323, 112)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(40, 16)
        Me.lblDate.TabIndex = 23
        Me.lblDate.Text = "Date:"
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.ForeColor = System.Drawing.Color.Navy
        Me.lblTime.Location = New System.Drawing.Point(562, 109)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(42, 16)
        Me.lblTime.TabIndex = 24
        Me.lblTime.Text = "Time:"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1001
        '
        'txtTime
        '
        Me.txtTime.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtTime.BackColor = System.Drawing.Color.AliceBlue
        Me.txtTime.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime.ForeColor = System.Drawing.Color.Black
        Me.txtTime.Location = New System.Drawing.Point(613, 101)
        Me.txtTime.Multiline = True
        Me.txtTime.Name = "txtTime"
        Me.txtTime.ReadOnly = True
        Me.txtTime.Size = New System.Drawing.Size(150, 30)
        Me.txtTime.TabIndex = 26
        Me.txtTime.TabStop = False
        Me.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDate
        '
        Me.txtDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtDate.BackColor = System.Drawing.Color.AliceBlue
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.Black
        Me.txtDate.Location = New System.Drawing.Point(375, 101)
        Me.txtDate.Multiline = True
        Me.txtDate.Name = "txtDate"
        Me.txtDate.ReadOnly = True
        Me.txtDate.Size = New System.Drawing.Size(150, 30)
        Me.txtDate.TabIndex = 27
        Me.txtDate.TabStop = False
        Me.txtDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.DarkRed
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(662, 139)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 30)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save Perchi"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'PrintDocument1
        '
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.PrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'btnPreview
        '
        Me.btnPreview.BackColor = System.Drawing.Color.DarkRed
        Me.btnPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPreview.ForeColor = System.Drawing.Color.White
        Me.btnPreview.Location = New System.Drawing.Point(662, 168)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(100, 30)
        Me.btnPreview.TabIndex = 9
        Me.btnPreview.Text = "Preview"
        Me.btnPreview.UseVisualStyleBackColor = False
        '
        'btnHistory
        '
        Me.btnHistory.BackColor = System.Drawing.Color.DarkRed
        Me.btnHistory.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnHistory.ForeColor = System.Drawing.Color.White
        Me.btnHistory.Location = New System.Drawing.Point(662, 223)
        Me.btnHistory.Name = "btnHistory"
        Me.btnHistory.Size = New System.Drawing.Size(100, 30)
        Me.btnHistory.TabIndex = 30
        Me.btnHistory.Text = "History"
        Me.btnHistory.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.DarkRed
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(662, 196)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(100, 30)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnDailyReport
        '
        Me.btnDailyReport.BackColor = System.Drawing.Color.DarkRed
        Me.btnDailyReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDailyReport.ForeColor = System.Drawing.Color.White
        Me.btnDailyReport.Location = New System.Drawing.Point(662, 251)
        Me.btnDailyReport.Name = "btnDailyReport"
        Me.btnDailyReport.Size = New System.Drawing.Size(100, 30)
        Me.btnDailyReport.TabIndex = 32
        Me.btnDailyReport.Text = "DailyReport"
        Me.btnDailyReport.UseVisualStyleBackColor = False
        '
        'Panelheader
        '
        Me.Panelheader.BackColor = System.Drawing.Color.Navy
        Me.Panelheader.Controls.Add(Me.PictureBox1)
        Me.Panelheader.Controls.Add(Me.TextBox1)
        Me.Panelheader.Controls.Add(Me.PictureBox2)
        Me.Panelheader.ForeColor = System.Drawing.Color.White
        Me.Panelheader.Location = New System.Drawing.Point(-6, 13)
        Me.Panelheader.Name = "Panelheader"
        Me.Panelheader.Size = New System.Drawing.Size(782, 59)
        Me.Panelheader.TabIndex = 33
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(691, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(60, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.Navy
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.White
        Me.TextBox1.Location = New System.Drawing.Point(192, 7)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(356, 48)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.Text = "Sultan Gold Testing Lab"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(35, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(59, 50)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'btnCustomers
        '
        Me.btnCustomers.BackColor = System.Drawing.Color.DarkRed
        Me.btnCustomers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCustomers.ForeColor = System.Drawing.Color.White
        Me.btnCustomers.Location = New System.Drawing.Point(662, 277)
        Me.btnCustomers.Name = "btnCustomers"
        Me.btnCustomers.Size = New System.Drawing.Size(100, 30)
        Me.btnCustomers.TabIndex = 34
        Me.btnCustomers.Text = "Customer"
        Me.btnCustomers.UseVisualStyleBackColor = False
        '
        'btnKhata
        '
        Me.btnKhata.BackColor = System.Drawing.Color.DarkRed
        Me.btnKhata.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnKhata.ForeColor = System.Drawing.Color.White
        Me.btnKhata.Location = New System.Drawing.Point(662, 305)
        Me.btnKhata.Name = "btnKhata"
        Me.btnKhata.Size = New System.Drawing.Size(100, 30)
        Me.btnKhata.TabIndex = 35
        Me.btnKhata.Text = "Accounts"
        Me.btnKhata.UseVisualStyleBackColor = False
        '
        'cmbType
        '
        Me.cmbType.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"BUY", "SALE"})
        Me.cmbType.Location = New System.Drawing.Point(431, 241)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(63, 24)
        Me.cmbType.TabIndex = 36
        '
        'btnWhatsApp
        '
        Me.btnWhatsApp.BackColor = System.Drawing.Color.Transparent
        Me.btnWhatsApp.BackgroundImage = CType(resources.GetObject("btnWhatsApp.BackgroundImage"), System.Drawing.Image)
        Me.btnWhatsApp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnWhatsApp.FlatAppearance.BorderSize = 0
        Me.btnWhatsApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnWhatsApp.Location = New System.Drawing.Point(338, 346)
        Me.btnWhatsApp.Name = "btnWhatsApp"
        Me.btnWhatsApp.Size = New System.Drawing.Size(60, 60)
        Me.btnWhatsApp.TabIndex = 37
        Me.btnWhatsApp.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Navy
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.ForeColor = System.Drawing.Color.White
        Me.Panel1.Location = New System.Drawing.Point(4, 576)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(769, 53)
        Me.Panel1.TabIndex = 38
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(200, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(395, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Created By SultanSoft 03172503282"
        '
        'cboPortName
        '
        Me.cboPortName.FormattingEnabled = True
        Me.cboPortName.Location = New System.Drawing.Point(35, 343)
        Me.cboPortName.Name = "cboPortName"
        Me.cboPortName.Size = New System.Drawing.Size(84, 21)
        Me.cboPortName.TabIndex = 39
        Me.cboPortName.TabStop = False
        '
        'cboBaudRate
        '
        Me.cboBaudRate.FormattingEnabled = True
        Me.cboBaudRate.Location = New System.Drawing.Point(125, 343)
        Me.cboBaudRate.Name = "cboBaudRate"
        Me.cboBaudRate.Size = New System.Drawing.Size(84, 21)
        Me.cboBaudRate.TabIndex = 40
        Me.cboBaudRate.TabStop = False
        '
        'btnConnect
        '
        Me.btnConnect.BackColor = System.Drawing.Color.DarkRed
        Me.btnConnect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConnect.ForeColor = System.Drawing.Color.White
        Me.btnConnect.Location = New System.Drawing.Point(35, 370)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(84, 42)
        Me.btnConnect.TabIndex = 41
        Me.btnConnect.Text = "Connect Scale"
        Me.btnConnect.UseVisualStyleBackColor = False
        '
        'btnGetWeight
        '
        Me.btnGetWeight.BackColor = System.Drawing.Color.DarkRed
        Me.btnGetWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGetWeight.ForeColor = System.Drawing.Color.White
        Me.btnGetWeight.Location = New System.Drawing.Point(125, 370)
        Me.btnGetWeight.Name = "btnGetWeight"
        Me.btnGetWeight.Size = New System.Drawing.Size(84, 42)
        Me.btnGetWeight.TabIndex = 42
        Me.btnGetWeight.Text = "Get Weight"
        Me.btnGetWeight.UseVisualStyleBackColor = False
        '
        'chkAutoRead
        '
        Me.chkAutoRead.AutoSize = True
        Me.chkAutoRead.Location = New System.Drawing.Point(236, 346)
        Me.chkAutoRead.Name = "chkAutoRead"
        Me.chkAutoRead.Size = New System.Drawing.Size(81, 17)
        Me.chkAutoRead.TabIndex = 43
        Me.chkAutoRead.Text = "CheckBox1"
        Me.chkAutoRead.UseVisualStyleBackColor = True
        '
        'Timer2
        '
        '
        'txtCashOut
        '
        Me.txtCashOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashOut.Location = New System.Drawing.Point(511, 402)
        Me.txtCashOut.Multiline = True
        Me.txtCashOut.Name = "txtCashOut"
        Me.txtCashOut.Size = New System.Drawing.Size(145, 30)
        Me.txtCashOut.TabIndex = 6
        Me.txtCashOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCashIn
        '
        Me.txtCashIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCashIn.Location = New System.Drawing.Point(511, 369)
        Me.txtCashIn.Multiline = True
        Me.txtCashIn.Name = "txtCashIn"
        Me.txtCashIn.Size = New System.Drawing.Size(145, 30)
        Me.txtCashIn.TabIndex = 5
        Me.txtCashIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(433, 379)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Cash\In"
        '
        'lable1
        '
        Me.lable1.AutoSize = True
        Me.lable1.ForeColor = System.Drawing.Color.Navy
        Me.lable1.Location = New System.Drawing.Point(433, 412)
        Me.lable1.Name = "lable1"
        Me.lable1.Size = New System.Drawing.Size(53, 13)
        Me.lable1.TabIndex = 47
        Me.lable1.Text = "Cash\Out"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(433, 445)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Advance"
        '
        'txtAdvance
        '
        Me.txtAdvance.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvance.Location = New System.Drawing.Point(511, 435)
        Me.txtAdvance.Multiline = True
        Me.txtAdvance.Name = "txtAdvance"
        Me.txtAdvance.Size = New System.Drawing.Size(145, 30)
        Me.txtAdvance.TabIndex = 7
        Me.txtAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(433, 478)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(36, 13)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Udhar"
        '
        'txtUdhar
        '
        Me.txtUdhar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUdhar.Location = New System.Drawing.Point(511, 468)
        Me.txtUdhar.Multiline = True
        Me.txtUdhar.Name = "txtUdhar"
        Me.txtUdhar.Size = New System.Drawing.Size(145, 30)
        Me.txtUdhar.TabIndex = 8
        Me.txtUdhar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.DarkRed
        Me.btnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(662, 333)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 30)
        Me.btnCancel.TabIndex = 52
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnOpenWorth
        '
        Me.btnOpenWorth.BackColor = System.Drawing.Color.DarkRed
        Me.btnOpenWorth.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenWorth.ForeColor = System.Drawing.Color.White
        Me.btnOpenWorth.Location = New System.Drawing.Point(662, 362)
        Me.btnOpenWorth.Name = "btnOpenWorth"
        Me.btnOpenWorth.Size = New System.Drawing.Size(100, 30)
        Me.btnOpenWorth.TabIndex = 53
        Me.btnOpenWorth.Text = "Worth"
        Me.btnOpenWorth.UseVisualStyleBackColor = False
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.Color.LightYellow
        Me.txtBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalance.Location = New System.Drawing.Point(511, 538)
        Me.txtBalance.Multiline = True
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(145, 30)
        Me.txtBalance.TabIndex = 54
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(433, 550)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Balance:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(109, 511)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 16)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Gold"
        '
        'txtGold
        '
        Me.txtGold.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtGold.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGold.Location = New System.Drawing.Point(37, 530)
        Me.txtGold.Multiline = True
        Me.txtGold.Name = "txtGold"
        Me.txtGold.Size = New System.Drawing.Size(176, 40)
        Me.txtGold.TabIndex = 56
        Me.txtGold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(310, 511)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 16)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Cash"
        '
        'txtCash
        '
        Me.txtCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCash.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCash.Location = New System.Drawing.Point(240, 530)
        Me.txtCash.Multiline = True
        Me.txtCash.Name = "txtCash"
        Me.txtCash.Size = New System.Drawing.Size(176, 40)
        Me.txtCash.TabIndex = 58
        Me.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtName1
        '
        Me.txtName1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName1.Location = New System.Drawing.Point(38, 435)
        Me.txtName1.Multiline = True
        Me.txtName1.Name = "txtName1"
        Me.txtName1.Size = New System.Drawing.Size(115, 30)
        Me.txtName1.TabIndex = 10
        Me.txtName1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGinni
        '
        Me.txtGinni.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGinni.Location = New System.Drawing.Point(159, 435)
        Me.txtGinni.Multiline = True
        Me.txtGinni.Name = "txtGinni"
        Me.txtGinni.Size = New System.Drawing.Size(84, 30)
        Me.txtGinni.TabIndex = 11
        Me.txtGinni.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPure
        '
        Me.txtPure.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPure.Location = New System.Drawing.Point(246, 435)
        Me.txtPure.Multiline = True
        Me.txtPure.Name = "txtPure"
        Me.txtPure.Size = New System.Drawing.Size(84, 30)
        Me.txtPure.TabIndex = 12
        Me.txtPure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Navy
        Me.Label11.Location = New System.Drawing.Point(63, 417)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 16)
        Me.Label11.TabIndex = 63
        Me.Label11.Text = "Name"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(182, 417)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(43, 16)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "Ginni"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Navy
        Me.Label10.Location = New System.Drawing.Point(267, 417)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 16)
        Me.Label10.TabIndex = 65
        Me.Label10.Text = "Pure"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(349, 417)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 16)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "Ratti/In"
        '
        'txtRattiIn
        '
        Me.txtRattiIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRattiIn.Location = New System.Drawing.Point(333, 435)
        Me.txtRattiIn.Multiline = True
        Me.txtRattiIn.Name = "txtRattiIn"
        Me.txtRattiIn.Size = New System.Drawing.Size(84, 30)
        Me.txtRattiIn.TabIndex = 13
        Me.txtRattiIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnSubmitGinni
        '
        Me.btnSubmitGinni.BackColor = System.Drawing.Color.DarkRed
        Me.btnSubmitGinni.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmitGinni.ForeColor = System.Drawing.Color.White
        Me.btnSubmitGinni.Location = New System.Drawing.Point(317, 471)
        Me.btnSubmitGinni.Name = "btnSubmitGinni"
        Me.btnSubmitGinni.Size = New System.Drawing.Size(100, 30)
        Me.btnSubmitGinni.TabIndex = 14
        Me.btnSubmitGinni.Text = "Submit"
        Me.btnSubmitGinni.UseVisualStyleBackColor = False
        '
        'btnGinni
        '
        Me.btnGinni.BackColor = System.Drawing.Color.DarkRed
        Me.btnGinni.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGinni.ForeColor = System.Drawing.Color.White
        Me.btnGinni.Location = New System.Drawing.Point(662, 391)
        Me.btnGinni.Name = "btnGinni"
        Me.btnGinni.Size = New System.Drawing.Size(100, 30)
        Me.btnGinni.TabIndex = 69
        Me.btnGinni.Text = "Ginni"
        Me.btnGinni.UseVisualStyleBackColor = False
        '
        'btnBackup
        '
        Me.btnBackup.BackColor = System.Drawing.Color.DarkRed
        Me.btnBackup.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackup.ForeColor = System.Drawing.Color.White
        Me.btnBackup.Location = New System.Drawing.Point(663, 420)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(100, 30)
        Me.btnBackup.TabIndex = 70
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.UseVisualStyleBackColor = False
        '
        'btnRestore
        '
        Me.btnRestore.BackColor = System.Drawing.Color.DarkRed
        Me.btnRestore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestore.ForeColor = System.Drawing.Color.White
        Me.btnRestore.Location = New System.Drawing.Point(663, 448)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(100, 30)
        Me.btnRestore.TabIndex = 71
        Me.btnRestore.Text = "Restore"
        Me.btnRestore.UseVisualStyleBackColor = False
        '
        'btnOpenperchi
        '
        Me.btnOpenperchi.BackColor = System.Drawing.Color.DarkRed
        Me.btnOpenperchi.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenperchi.ForeColor = System.Drawing.Color.White
        Me.btnOpenperchi.Location = New System.Drawing.Point(663, 476)
        Me.btnOpenperchi.Name = "btnOpenperchi"
        Me.btnOpenperchi.Size = New System.Drawing.Size(100, 30)
        Me.btnOpenperchi.TabIndex = 72
        Me.btnOpenperchi.Text = "Perchi"
        Me.btnOpenperchi.UseVisualStyleBackColor = False
        '
        'btnOpenDashboard
        '
        Me.btnOpenDashboard.BackColor = System.Drawing.Color.DarkRed
        Me.btnOpenDashboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOpenDashboard.ForeColor = System.Drawing.Color.White
        Me.btnOpenDashboard.Location = New System.Drawing.Point(663, 505)
        Me.btnOpenDashboard.Name = "btnOpenDashboard"
        Me.btnOpenDashboard.Size = New System.Drawing.Size(100, 30)
        Me.btnOpenDashboard.TabIndex = 73
        Me.btnOpenDashboard.Text = "Dashboard"
        Me.btnOpenDashboard.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Lavender
        Me.ClientSize = New System.Drawing.Size(775, 633)
        Me.Controls.Add(Me.btnOpenDashboard)
        Me.Controls.Add(Me.btnOpenperchi)
        Me.Controls.Add(Me.btnWhatsApp)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.btnBackup)
        Me.Controls.Add(Me.btnGinni)
        Me.Controls.Add(Me.btnSubmitGinni)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtRattiIn)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtPure)
        Me.Controls.Add(Me.txtGinni)
        Me.Controls.Add(Me.txtName1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCash)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtGold)
        Me.Controls.Add(Me.txtBalance)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnOpenWorth)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtUdhar)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtAdvance)
        Me.Controls.Add(Me.lable1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtCashOut)
        Me.Controls.Add(Me.txtCashIn)
        Me.Controls.Add(Me.chkAutoRead)
        Me.Controls.Add(Me.btnGetWeight)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.cboBaudRate)
        Me.Controls.Add(Me.cboPortName)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cmbType)
        Me.Controls.Add(Me.btnKhata)
        Me.Controls.Add(Me.btnCustomers)
        Me.Controls.Add(Me.Panelheader)
        Me.Controls.Add(Me.btnDailyReport)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnHistory)
        Me.Controls.Add(Me.txtCustomerSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtTime)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.txtPerchiNo)
        Me.Controls.Add(Me.lblPerchiNo)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lblAmount)
        Me.Controls.Add(Me.txtRate)
        Me.Controls.Add(Me.lblRate)
        Me.Controls.Add(Me.txtCut)
        Me.Controls.Add(Me.lblCut)
        Me.Controls.Add(Me.txtCurrentRate)
        Me.Controls.Add(Me.lblCurrentRate)
        Me.Controls.Add(Me.txtWeight)
        Me.Controls.Add(Me.grpAddCustomer)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.txtMobile)
        Me.Controls.Add(Me.lblMobile)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Controls.Add(Me.lblCustomerName)
        Me.ForeColor = System.Drawing.Color.Navy
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sultan Perchi"
        Me.grpAddCustomer.ResumeLayout(False)
        Me.grpAddCustomer.PerformLayout()
        Me.Panelheader.ResumeLayout(False)
        Me.Panelheader.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnSearch As Button
    Friend WithEvents txtCustomerSearch As TextBox
    Friend WithEvents lblCustomerName As Label
    Friend WithEvents cmbCustomer As ComboBox
    Friend WithEvents lblMobile As Label
    Friend WithEvents txtMobile As TextBox
    Friend WithEvents lblAddress As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents grpAddCustomer As GroupBox
    Friend WithEvents btnAddCustomer As Button
    Friend WithEvents lblNewAddress As Label
    Friend WithEvents lblNewMobile As Label
    Friend WithEvents lblNewName As Label
    Friend WithEvents txtNewAddress As TextBox
    Friend WithEvents txtNewMobile As TextBox
    Friend WithEvents txtNewCustomerName As TextBox
    Friend WithEvents txtWeight As TextBox
    Friend WithEvents txtCurrentRate As TextBox
    Friend WithEvents lblCurrentRate As Label
    Friend WithEvents txtCut As TextBox
    Friend WithEvents lblCut As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents lblAmount As Label
    Friend WithEvents txtRate As TextBox
    Friend WithEvents lblRate As Label
    Friend WithEvents lblPerchiNo As Label
    Friend WithEvents txtPerchiNo As TextBox
    Friend WithEvents lblDate As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents Timer1 As Timer
    Friend WithEvents txtTime As TextBox
    Friend WithEvents txtDate As TextBox
    Friend WithEvents btnSave As Button
    Friend WithEvents PrintDocument1 As Printing.PrintDocument
    Friend WithEvents PrintPreviewDialog1 As PrintPreviewDialog
    Friend WithEvents btnPreview As Button
    Friend WithEvents btnHistory As Button
    Friend WithEvents btnPrint As Button
    Friend WithEvents btnDailyReport As Button
    Friend WithEvents Panelheader As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents btnCustomers As Button
    Friend WithEvents btnKhata As Button
    Friend WithEvents cmbType As ComboBox
    Friend WithEvents btnWhatsApp As Button
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents cboPortName As ComboBox
    Friend WithEvents cboBaudRate As ComboBox
    Friend WithEvents btnConnect As Button
    Friend WithEvents btnGetWeight As Button
    Friend WithEvents chkAutoRead As CheckBox
    Friend WithEvents Timer2 As Timer
    Friend WithEvents txtCashOut As TextBox
    Friend WithEvents txtCashIn As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents lable1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtAdvance As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUdhar As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOpenWorth As Button
    Friend WithEvents txtBalance As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtGold As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtCash As TextBox
    Friend WithEvents txtName1 As TextBox
    Friend WithEvents txtGinni As TextBox
    Friend WithEvents txtPure As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtRattiIn As TextBox
    Friend WithEvents btnSubmitGinni As Button
    Friend WithEvents btnGinni As Button
    Friend WithEvents btnBackup As Button
    Friend WithEvents btnRestore As Button
    Friend WithEvents btnOpenperchi As Button
    Friend WithEvents btnOpenDashboard As Button
End Class
