Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormAccounts
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private lblPerchiNo As New Label()
    Private txtPerchiNo As New TextBox()
    Private lblDate As New Label()
    Private dtpDate As New DateTimePicker()
    Private lblTime As New Label()
    Private txtTime As New TextBox()

    Private btnCustSearch As New Button()
    Private txtCustSearch As New TextBox()

    Private grpAddCustomer As New GroupBox()
    Private lblNewCust As New Label()
    Private txtNewCust As New TextBox()
    Private lblNewMobile As New Label()
    Private txtNewMobile As New TextBox()
    Private lblNewAddr As New Label()
    Private txtNewAddr As New TextBox()
    Private btnAddCustomer As New Button()

    Private cmbComPort As New ComboBox()
    Private cmbBaudRate As New ComboBox()
    Private chkScale As New CheckBox()
    Private btnConnectScale As New Button()
    Private btnGetWeight As New Button()

    ' Ginni Section Controls
    Private lblMetricName As New Label()
    Private cmbMetricName As New ComboBox()
    Private lblGinni As New Label()
    Private txtGinni As New TextBox()
    Private lblPure As New Label()
    Private txtPure As New TextBox()
    Private lblRattiIn As New Label()
    Private txtRattiIn As New TextBox()
    Private btnSubmitMetric As New Button()
    Private editingGinniRowId As Integer = 0

    Private pnlGoldDisplay As New Panel()
    Private lblGoldTitle As New Label()
    Private lblGoldVal As New Label()
    Private pnlCashDisplay As New Panel()
    Private lblCashTitle As New Label()
    Private lblCashVal As New Label()

    Private lblCustName As New Label()
    Private cmbCustName As New ComboBox()
    Private lblMobile As New Label()
    Private txtMobile As New TextBox()
    Private lblAddress As New Label()
    Private txtAddress As New TextBox()

    Private cmbBuySale As New ComboBox()
    Private txtBuySaleVal As New TextBox()

    Private lblCurrentRate As New Label()
    Private txtCurrentRate As New TextBox()
    Private lblCut As New Label()
    Private txtCut As New TextBox()
    Private lblRate As New Label()
    Private txtRate As New TextBox()

    Private lblCashIn As New Label()
    Private txtCashIn As New TextBox()
    Private lblCashOut As New Label()
    Private txtCashOut As New TextBox()

    Private lblAmount As New Label()
    Private txtAmount As New TextBox()
    Private lblBalance As New Label()
    Private txtBalance As New TextBox()

    Private rightNavPanel As New Panel()
    Private btnPrint As Button

    Private WithEvents printDoc As New PrintDocument()

    Private isCalculating As Boolean = False
    Private isSelectingCustomer As Boolean = False
    Private isLoadingEdit As Boolean = False
    Private previousCustomerBalance As Double = 0

    Public Sub New()
        Me.Text = "Sultan Perchi - Main Accounts"
        Me.Size = New Size(950, 650)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        LoadCustomerData()
        RegisterCalculationEvents()
        GenerateNextPerchiNo()
        LoadDailyClosingSummary()

        AddHandler Me.Shown, Sub() txtBuySaleVal.Focus()
        AddHandler Me.Activated, Sub() LoadDailyClosingSummary()
    End Sub

    Private Sub SetupUI()
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 55
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Sultan Gold Testing Lab"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 18, FontStyle.Bold)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        headerPanel.Controls.Add(lblTitle)

        footerPanel.Dock = DockStyle.Bottom
        footerPanel.Height = 35
        footerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblFooter.Text = "Created By SultanSoft 03172503282"
        lblFooter.ForeColor = Color.White
        lblFooter.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblFooter.Dock = DockStyle.Fill
        lblFooter.TextAlign = ContentAlignment.MiddleCenter
        footerPanel.Controls.Add(lblFooter)

        lblPerchiNo.Text = "Perchi No:"
        lblPerchiNo.Location = New Point(20, 70)
        lblPerchiNo.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblPerchiNo.ForeColor = Color.DarkBlue
        lblPerchiNo.AutoSize = True

        txtPerchiNo.Location = New Point(90, 67)
        txtPerchiNo.Size = New Size(110, 25)
        txtPerchiNo.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtPerchiNo.ReadOnly = True
        txtPerchiNo.TabStop = False

        lblDate.Text = "Date:"
        lblDate.Location = New Point(220, 70)
        lblDate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblDate.ForeColor = Color.DarkBlue
        lblDate.AutoSize = True

        dtpDate.Location = New Point(260, 67)
        dtpDate.Size = New Size(120, 25)
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.TabStop = False
        AddHandler dtpDate.ValueChanged, Sub() LoadDailyClosingSummary()

        lblTime.Text = "Time:"
        lblTime.Location = New Point(395, 70)
        lblTime.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblTime.ForeColor = Color.DarkBlue
        lblTime.AutoSize = True

        txtTime.Location = New Point(435, 67)
        txtTime.Size = New Size(100, 25)
        txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        txtTime.TabStop = False

        btnCustSearch.Text = "Customer Search"
        btnCustSearch.Location = New Point(20, 105)
        btnCustSearch.Size = New Size(120, 28)
        btnCustSearch.BackColor = Color.Maroon
        btnCustSearch.ForeColor = Color.White
        btnCustSearch.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        txtCustSearch.Location = New Point(145, 107)
        txtCustSearch.Size = New Size(150, 25)
        txtCustSearch.TabStop = False

        grpAddCustomer.Text = "Add New Customer"
        grpAddCustomer.Location = New Point(20, 140)
        grpAddCustomer.Size = New Size(275, 125)
        grpAddCustomer.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        grpAddCustomer.ForeColor = Color.DarkBlue

        lblNewCust.Text = "Add Customer"
        lblNewCust.Location = New Point(10, 22)
        lblNewCust.Font = New Font("Segoe UI", 8, FontStyle.Regular)
        lblNewCust.AutoSize = True

        txtNewCust.Location = New Point(85, 19)
        txtNewCust.Size = New Size(95, 22)

        lblNewMobile.Text = "Mobile:"
        lblNewMobile.Location = New Point(10, 48)
        lblNewMobile.Font = New Font("Segoe UI", 8, FontStyle.Regular)
        lblNewMobile.AutoSize = True

        txtNewMobile.Location = New Point(85, 45)
        txtNewMobile.Size = New Size(95, 22)

        lblNewAddr.Text = "Address:"
        lblNewAddr.Location = New Point(10, 74)
        lblNewAddr.Font = New Font("Segoe UI", 8, FontStyle.Regular)
        lblNewAddr.AutoSize = True

        txtNewAddr.Location = New Point(85, 71)
        txtNewAddr.Size = New Size(180, 22)

        btnAddCustomer.Text = "Add Customer"
        btnAddCustomer.Location = New Point(185, 19)
        btnAddCustomer.Size = New Size(80, 48)
        btnAddCustomer.BackColor = Color.Maroon
        btnAddCustomer.ForeColor = Color.White
        btnAddCustomer.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        AddHandler btnAddCustomer.Click, AddressOf BtnAddCustomer_Click

        grpAddCustomer.Controls.Add(lblNewCust)
        grpAddCustomer.Controls.Add(txtNewCust)
        grpAddCustomer.Controls.Add(lblNewMobile)
        grpAddCustomer.Controls.Add(txtNewMobile)
        grpAddCustomer.Controls.Add(lblNewAddr)
        grpAddCustomer.Controls.Add(txtNewAddr)
        grpAddCustomer.Controls.Add(btnAddCustomer)

        cmbComPort.Location = New Point(20, 275)
        cmbComPort.Size = New Size(65, 23)
        cmbComPort.Items.AddRange(New Object() {"COM1", "COM2", "COM3", "COM4"})
        cmbComPort.Text = "COM3"

        cmbBaudRate.Location = New Point(90, 275)
        cmbBaudRate.Size = New Size(65, 23)
        cmbBaudRate.Items.AddRange(New Object() {"2400", "4800", "9600"})
        cmbBaudRate.Text = "2400"

        chkScale.Text = "CheckBox1"
        chkScale.Location = New Point(165, 277)
        chkScale.AutoSize = True

        btnConnectScale.Text = "Connect"
        btnConnectScale.Location = New Point(20, 305)
        btnConnectScale.Size = New Size(85, 28)
        btnConnectScale.BackColor = Color.Maroon
        btnConnectScale.ForeColor = Color.White
        btnConnectScale.Font = New Font("Segoe UI", 8, FontStyle.Bold)

        btnGetWeight.Text = "Get Weight"
        btnGetWeight.Location = New Point(110, 305)
        btnGetWeight.Size = New Size(85, 28)
        btnGetWeight.BackColor = Color.Maroon
        btnGetWeight.ForeColor = Color.White
        btnGetWeight.Font = New Font("Segoe UI", 8, FontStyle.Bold)

        ' Ginni Section Setup
        lblMetricName.Text = "Name"
        lblMetricName.Location = New Point(20, 345)
        lblMetricName.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblMetricName.ForeColor = Color.DarkBlue
        lblMetricName.AutoSize = True

        cmbMetricName.Location = New Point(20, 362)
        cmbMetricName.Size = New Size(102, 23)
        cmbMetricName.DropDownStyle = ComboBoxStyle.DropDown
        cmbMetricName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbMetricName.AutoCompleteSource = AutoCompleteSource.ListItems

        lblGinni.Text = "Ginni"
        lblGinni.Location = New Point(127, 345)
        lblGinni.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblGinni.ForeColor = Color.DarkBlue
        lblGinni.AutoSize = True

        txtGinni.Location = New Point(127, 362)
        txtGinni.Size = New Size(47, 22)

        lblPure.Text = "Pure"
        lblPure.Location = New Point(179, 345)
        lblPure.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblPure.ForeColor = Color.DarkBlue
        lblPure.AutoSize = True

        txtPure.Location = New Point(179, 362)
        txtPure.Size = New Size(47, 22)

        lblRattiIn.Text = "Ratti/In"
        lblRattiIn.Location = New Point(231, 345)
        lblRattiIn.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblRattiIn.ForeColor = Color.DarkBlue
        lblRattiIn.AutoSize = True

        txtRattiIn.Location = New Point(231, 362)
        txtRattiIn.Size = New Size(47, 22)
        txtRattiIn.ReadOnly = True
        txtRattiIn.BackColor = Color.LightYellow

        btnSubmitMetric.Text = "Submit"
        btnSubmitMetric.Location = New Point(183, 390)
        btnSubmitMetric.Size = New Size(95, 25)
        btnSubmitMetric.BackColor = Color.Maroon
        btnSubmitMetric.ForeColor = Color.White
        btnSubmitMetric.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        AddHandler btnSubmitMetric.Click, AddressOf BtnSubmitMetric_Click

        AddHandler cmbMetricName.KeyDown, AddressOf Ginni_KeyDown
        AddHandler txtGinni.KeyDown, AddressOf Ginni_KeyDown
        AddHandler txtPure.KeyDown, AddressOf Ginni_KeyDown
        AddHandler txtGinni.TextChanged, AddressOf CalculateGinniRatti
        AddHandler txtPure.TextChanged, AddressOf CalculateGinniRatti

        pnlGoldDisplay.Location = New Point(20, 425)
        pnlGoldDisplay.Size = New Size(125, 60)
        pnlGoldDisplay.BackColor = Color.FromArgb(210, 220, 250)
        pnlGoldDisplay.BorderStyle = BorderStyle.FixedSingle

        lblGoldTitle.Text = "Gold"
        lblGoldTitle.Dock = DockStyle.Top
        lblGoldTitle.TextAlign = ContentAlignment.MiddleCenter
        lblGoldTitle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        lblGoldVal.Text = "0.000"
        lblGoldVal.Dock = DockStyle.Fill
        lblGoldVal.TextAlign = ContentAlignment.MiddleCenter
        lblGoldVal.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        pnlGoldDisplay.Controls.Add(lblGoldVal)
        pnlGoldDisplay.Controls.Add(lblGoldTitle)

        pnlCashDisplay.Location = New Point(155, 425)
        pnlCashDisplay.Size = New Size(140, 60)
        pnlCashDisplay.BackColor = Color.FromArgb(210, 220, 250)
        pnlCashDisplay.BorderStyle = BorderStyle.FixedSingle

        lblCashTitle.Text = "Cash"
        lblCashTitle.Dock = DockStyle.Top
        lblCashTitle.TextAlign = ContentAlignment.MiddleCenter
        lblCashTitle.Font = New Font("Segoe UI", 9, FontStyle.Bold)

        lblCashVal.Text = "0"
        lblCashVal.Dock = DockStyle.Fill
        lblCashVal.TextAlign = ContentAlignment.MiddleCenter
        lblCashVal.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblCashVal.ForeColor = Color.Green
        pnlCashDisplay.Controls.Add(lblCashVal)
        pnlCashDisplay.Controls.Add(lblCashTitle)

        Dim midX As Integer = 310
        Dim midY As Integer = 105
        Dim gapY As Integer = 35

        lblCustName.Text = "Customer Name:"
        lblCustName.Location = New Point(midX, midY)
        lblCustName.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblCustName.AutoSize = True

        cmbCustName.Location = New Point(midX + 95, midY - 3)
        cmbCustName.Size = New Size(175, 23)
        cmbCustName.DropDownStyle = ComboBoxStyle.DropDown
        cmbCustName.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbCustName.AutoCompleteSource = AutoCompleteSource.ListItems

        lblMobile.Text = "Mobile No:"
        lblMobile.Location = New Point(midX, midY + gapY)
        lblMobile.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblMobile.AutoSize = True

        txtMobile.Location = New Point(midX + 95, midY + gapY - 3)
        txtMobile.Size = New Size(175, 23)
        txtMobile.TabStop = False

        lblAddress.Text = "Address:"
        lblAddress.Location = New Point(midX, midY + (gapY * 2))
        lblAddress.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblAddress.AutoSize = True

        txtAddress.Location = New Point(midX + 95, midY + (gapY * 2) - 3)
        txtAddress.Size = New Size(175, 23)
        txtAddress.TabStop = False

        cmbBuySale.Location = New Point(midX, midY + (gapY * 3) - 3)
        cmbBuySale.Size = New Size(85, 23)
        cmbBuySale.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBuySale.Items.Clear()
        cmbBuySale.Items.AddRange(New Object() {"BUY", "SALE"})
        cmbBuySale.SelectedIndex = 0

        txtBuySaleVal.Location = New Point(midX + 95, midY + (gapY * 3) - 3)
        txtBuySaleVal.Size = New Size(175, 23)

        lblCurrentRate.Text = "CurrentRate:"
        lblCurrentRate.Location = New Point(midX, midY + (gapY * 4))
        lblCurrentRate.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblCurrentRate.AutoSize = True

        txtCurrentRate.Location = New Point(midX + 95, midY + (gapY * 4) - 3)
        txtCurrentRate.Size = New Size(175, 23)

        lblCut.Text = "Cut:"
        lblCut.Location = New Point(midX, midY + (gapY * 5))
        lblCut.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblCut.AutoSize = True

        txtCut.Location = New Point(midX + 95, midY + (gapY * 5) - 3)
        txtCut.Size = New Size(175, 23)

        lblRate.Text = "Rate:"
        lblRate.Location = New Point(midX, midY + (gapY * 6))
        lblRate.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblRate.AutoSize = True

        txtRate.Location = New Point(midX + 95, midY + (gapY * 6) - 3)
        txtRate.Size = New Size(175, 23)
        txtRate.BackColor = Color.LightYellow

        lblCashIn.Text = "Cash\In"
        lblCashIn.Location = New Point(midX, midY + (gapY * 7))
        lblCashIn.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblCashIn.AutoSize = True

        txtCashIn.Location = New Point(midX + 95, midY + (gapY * 7) - 3)
        txtCashIn.Size = New Size(175, 23)

        lblCashOut.Text = "Cash\Out"
        lblCashOut.Location = New Point(midX, midY + (gapY * 8))
        lblCashOut.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblCashOut.AutoSize = True

        txtCashOut.Location = New Point(midX + 95, midY + (gapY * 8) - 3)
        txtCashOut.Size = New Size(175, 23)

        lblAmount.Text = "Amount:"
        lblAmount.Location = New Point(midX, midY + (gapY * 9))
        lblAmount.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblAmount.AutoSize = True

        txtAmount.Location = New Point(midX + 95, midY + (gapY * 9) - 3)
        txtAmount.Size = New Size(175, 23)
        txtAmount.BackColor = Color.LightYellow

        lblBalance.Text = "Balance:"
        lblBalance.Location = New Point(midX, midY + (gapY * 10))
        lblBalance.Font = New Font("Segoe UI", 8.5F, FontStyle.Regular)
        lblBalance.AutoSize = True

        txtBalance.Location = New Point(midX + 95, midY + (gapY * 10) - 3)
        txtBalance.Size = New Size(175, 23)
        txtBalance.BackColor = Color.LightYellow
        txtBalance.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)

        txtBuySaleVal.TabIndex = 1
        txtCurrentRate.TabIndex = 2
        txtCut.TabIndex = 3
        txtRate.TabIndex = 4
        txtCashIn.TabIndex = 5
        txtCashOut.TabIndex = 6

        cmbCustName.TabIndex = 8
        cmbMetricName.TabIndex = 9
        txtGinni.TabIndex = 10
        txtPure.TabIndex = 11
        txtRattiIn.TabIndex = 12
        btnSubmitMetric.TabIndex = 13

        rightNavPanel.Location = New Point(600, 100)
        rightNavPanel.Size = New Size(120, 420)

        Dim btnNames As String() = {"Save Perchi", "Preview", "Print", "History", "DailyReport", "Customer", "Accounts", "Cancel", "Worth", "Ginni", "Backup", "Restore", "Perchi", "Dashboard"}
        Dim navY As Integer = 0
        For Each bName As String In btnNames
            Dim btn As New Button()
            btn.Text = bName
            btn.Location = New Point(0, navY)
            btn.Size = New Size(110, 26)
            btn.BackColor = Color.Maroon
            btn.ForeColor = Color.White
            btn.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
            btn.FlatStyle = FlatStyle.Standard

            If bName = "Print" Then
                btnPrint = btn
                btnPrint.TabIndex = 7
                btnPrint.TabStop = True
            End If

            Dim targetName As String = bName
            AddHandler btn.Click, Sub(s As Object, e As EventArgs) HandleNavClick(targetName)

            rightNavPanel.Controls.Add(btn)
            navY += 28
        Next

        Me.Controls.Add(headerPanel)
        Me.Controls.Add(footerPanel)
        Me.Controls.Add(lblPerchiNo)
        Me.Controls.Add(txtPerchiNo)
        Me.Controls.Add(lblDate)
        Me.Controls.Add(dtpDate)
        Me.Controls.Add(lblTime)
        Me.Controls.Add(txtTime)
        Me.Controls.Add(btnCustSearch)
        Me.Controls.Add(txtCustSearch)
        Me.Controls.Add(grpAddCustomer)
        Me.Controls.Add(cmbComPort)
        Me.Controls.Add(cmbBaudRate)
        Me.Controls.Add(chkScale)
        Me.Controls.Add(btnConnectScale)
        Me.Controls.Add(btnGetWeight)
        Me.Controls.Add(lblMetricName)
        Me.Controls.Add(cmbMetricName)
        Me.Controls.Add(lblGinni)
        Me.Controls.Add(txtGinni)
        Me.Controls.Add(lblPure)
        Me.Controls.Add(txtPure)
        Me.Controls.Add(lblRattiIn)
        Me.Controls.Add(txtRattiIn)
        Me.Controls.Add(btnSubmitMetric)
        Me.Controls.Add(pnlGoldDisplay)
        Me.Controls.Add(pnlCashDisplay)
        Me.Controls.Add(lblCustName)
        Me.Controls.Add(cmbCustName)
        Me.Controls.Add(lblMobile)
        Me.Controls.Add(txtMobile)
        Me.Controls.Add(lblAddress)
        Me.Controls.Add(txtAddress)
        Me.Controls.Add(cmbBuySale)
        Me.Controls.Add(txtBuySaleVal)
        Me.Controls.Add(lblCurrentRate)
        Me.Controls.Add(txtCurrentRate)
        Me.Controls.Add(lblCut)
        Me.Controls.Add(txtCut)
        Me.Controls.Add(lblRate)
        Me.Controls.Add(txtRate)
        Me.Controls.Add(lblCashIn)
        Me.Controls.Add(txtCashIn)
        Me.Controls.Add(lblCashOut)
        Me.Controls.Add(txtCashOut)
        Me.Controls.Add(lblAmount)
        Me.Controls.Add(txtAmount)
        Me.Controls.Add(lblBalance)
        Me.Controls.Add(txtBalance)
        Me.Controls.Add(rightNavPanel)
    End Sub

    Private Sub CalculateGinniRatti()
        Try
            Dim g As Double = 0
            Dim p As Double = 0
            Double.TryParse(txtGinni.Text, g)
            Double.TryParse(txtPure.Text, p)

            Dim rattiIn As Double = 0
            If g > 0 Then
                rattiIn = ((g - p) / g) * 96
            End If
            txtRattiIn.Text = rattiIn.ToString("0.00")
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Ginni_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If sender Is cmbMetricName Then
                txtGinni.Focus()
            ElseIf sender Is txtGinni Then
                txtPure.Focus()
            ElseIf sender Is txtPure Then
                BtnSubmitMetric_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    ' Load Ginni Row for Editing
    Public Sub LoadGinniForEdit(id As Integer, name As String, ginni As Double, pure As Double)
        editingGinniRowId = id
        cmbMetricName.Text = name
        txtGinni.Text = ginni.ToString()
        txtPure.Text = pure.ToString()
        CalculateGinniRatti()
        cmbMetricName.Focus()
    End Sub

    ' Save or Update Ginni Record
    Private Sub BtnSubmitMetric_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(cmbMetricName.Text) Then
            MessageBox.Show("Barah-e-karam Name select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cmbMetricName.Focus()
            Return
        End If

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim createTbl As New SQLiteCommand("CREATE TABLE IF NOT EXISTS Ginni (Name TEXT, Ginni REAL, Pure REAL, RattiIn REAL);", conn)
                createTbl.ExecuteNonQuery()

                If editingGinniRowId > 0 Then
                    Dim updateQuery As String = "UPDATE Ginni SET Name = @Name, Ginni = @Ginni, Pure = @Pure, RattiIn = @RattiIn WHERE ROWID = @id"
                    Using cmd As New SQLiteCommand(updateQuery, conn)
                        cmd.Parameters.AddWithValue("@Name", cmbMetricName.Text.Trim())
                        cmd.Parameters.AddWithValue("@Ginni", Convert.ToDouble(If(String.IsNullOrEmpty(txtGinni.Text), "0", txtGinni.Text)))
                        cmd.Parameters.AddWithValue("@Pure", Convert.ToDouble(If(String.IsNullOrEmpty(txtPure.Text), "0", txtPure.Text)))
                        cmd.Parameters.AddWithValue("@RattiIn", Convert.ToDouble(If(String.IsNullOrEmpty(txtRattiIn.Text), "0", txtRattiIn.Text)))
                        cmd.Parameters.AddWithValue("@id", editingGinniRowId)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Ginni record kamyabi se update ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    editingGinniRowId = 0
                Else
                    Dim query As String = "INSERT INTO Ginni (Name, Ginni, Pure, RattiIn) VALUES (@Name, @Ginni, @Pure, @RattiIn)"
                    Using cmd As New SQLiteCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Name", cmbMetricName.Text.Trim())
                        cmd.Parameters.AddWithValue("@Ginni", Convert.ToDouble(If(String.IsNullOrEmpty(txtGinni.Text), "0", txtGinni.Text)))
                        cmd.Parameters.AddWithValue("@Pure", Convert.ToDouble(If(String.IsNullOrEmpty(txtPure.Text), "0", txtPure.Text)))
                        cmd.Parameters.AddWithValue("@RattiIn", Convert.ToDouble(If(String.IsNullOrEmpty(txtRattiIn.Text), "0", txtRattiIn.Text)))
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Ginni record kamyabi se save ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            cmbMetricName.Text = ""
            txtGinni.Clear()
            txtPure.Clear()
            txtRattiIn.Clear()
            cmbMetricName.Focus()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadCustomerData()
        Try
            cmbCustName.Items.Clear()
            cmbMetricName.Items.Clear()
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT Name FROM Customers ORDER BY Name ASC", conn)
                Dim dr As SQLiteDataReader = cmd.ExecuteReader()
                While dr.Read()
                    Dim cName As String = dr("Name").ToString()
                    cmbCustName.Items.Add(cName)
                    cmbMetricName.Items.Add(cName)
                End While
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadDailyClosingSummary()
        Try
            Dim selDate As String = dtpDate.Value.ToString("yyyy-MM-dd")
            Dim closingWeight As Double = 0
            Dim closingCash As Double = 0

            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim createCmd As New SQLiteCommand("CREATE TABLE IF NOT EXISTS DailySummary (ReportDate TEXT PRIMARY KEY, OpningWeight REAL, OpningCash REAL, ClosingWeight REAL, ClosingCash REAL);", conn)
                createCmd.ExecuteNonQuery()

                Dim sql As String = "SELECT ClosingWeight, ClosingCash FROM DailySummary WHERE ReportDate = @rDate"
                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@rDate", selDate)
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            closingWeight = Convert.ToDouble(If(dr("ClosingWeight") IsNot DBNull.Value, dr("ClosingWeight"), 0))
                            closingCash = Convert.ToDouble(If(dr("ClosingCash") IsNot DBNull.Value, dr("ClosingCash"), 0))
                        End If
                    End Using
                End Using
            End Using

            lblGoldVal.Text = closingWeight.ToString("0.000")
            lblCashVal.Text = Math.Round(closingCash).ToString("0")

            If closingCash >= 0 Then
                lblCashVal.ForeColor = Color.Green
            Else
                lblCashVal.ForeColor = Color.Red
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RegisterCalculationEvents()
        AddHandler cmbCustName.SelectedIndexChanged, AddressOf CmbCustName_SelectedIndexChanged
        AddHandler cmbCustName.TextChanged, AddressOf CmbCustName_TextChanged
        AddHandler txtBuySaleVal.TextChanged, AddressOf CalculateValues
        AddHandler txtCurrentRate.TextChanged, AddressOf CalculateValues
        AddHandler txtCut.TextChanged, AddressOf CalculateValues
        AddHandler txtCashIn.TextChanged, AddressOf CashTextChanged
        AddHandler txtCashOut.TextChanged, AddressOf CashTextChanged
        AddHandler cmbBuySale.SelectedIndexChanged, AddressOf CmbBuySale_SelectedIndexChanged
    End Sub

    Private Sub CmbBuySale_SelectedIndexChanged(sender As Object, e As EventArgs)
        If isLoadingEdit Then Return
        CalculateValues()
    End Sub

    Private Sub CashTextChanged(sender As Object, e As EventArgs)
        If isLoadingEdit Then Return
        CalculateBalanceOnly()
    End Sub

    Private Sub CmbCustName_SelectedIndexChanged(sender As Object, e As EventArgs)
        If isLoadingEdit Then Return
        LoadCustomerDetails(cmbCustName.Text.Trim())
    End Sub

    Private Sub CmbCustName_TextChanged(sender As Object, e As EventArgs)
        If isLoadingEdit OrElse isSelectingCustomer Then Return
        LoadCustomerDetails(cmbCustName.Text.Trim())
    End Sub

    Private Sub LoadCustomerDetails(custName As String)
        If isLoadingEdit OrElse String.IsNullOrWhiteSpace(custName) Then Return
        isSelectingCustomer = True

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT Mobile, Address, IFNULL(CashBalance, 0) As CashBal FROM Customers WHERE Name = @Name"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Name", custName.Trim())
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            txtMobile.Text = If(dr("Mobile") IsNot DBNull.Value, dr("Mobile").ToString(), "")
                            txtAddress.Text = If(dr("Address") IsNot DBNull.Value, dr("Address").ToString(), "")
                            previousCustomerBalance = Convert.ToDouble(dr("CashBal"))
                        End If
                    End Using
                End Using
            End Using

            txtBalance.Text = "0"
            txtBalance.ForeColor = Color.Black
            txtCashIn.Clear()
            txtCashOut.Clear()
            txtBuySaleVal.Clear()
            txtAmount.Text = "0"

        Catch ex As Exception
        Finally
            isSelectingCustomer = False
        End Try
    End Sub

    Private Sub CalculateValues()
        If isLoadingEdit OrElse isCalculating Then Return
        isCalculating = True

        Try
            Dim weight As Double = 0
            Dim currentRate As Double = 0
            Dim cut As Double = 0

            Double.TryParse(txtBuySaleVal.Text, weight)
            Double.TryParse(txtCurrentRate.Text, currentRate)
            Double.TryParse(txtCut.Text, cut)

            Dim netRate As Double = currentRate - cut
            If netRate < 0 Then netRate = 0
            txtRate.Text = netRate.ToString("0")

            Dim totalAmount As Double = 0
            If weight > 0 AndAlso netRate > 0 Then
                totalAmount = (weight * netRate) / 11.664
            End If
            txtAmount.Text = Math.Round(totalAmount).ToString("0")

            Dim selectedType As String = cmbBuySale.Text.Trim().ToUpper()
            Dim amountVal As Double = Math.Round(totalAmount)

            If weight > 0 Then
                If selectedType = "BUY" Then
                    txtCashOut.Text = amountVal.ToString("0")
                    txtCashIn.Text = "0"
                ElseIf selectedType = "SALE" Then
                    txtCashIn.Text = amountVal.ToString("0")
                    txtCashOut.Text = "0"
                End If
            End If

            CalculateBalanceOnly()

        Catch ex As Exception
        Finally
            isCalculating = False
        End Try
    End Sub

    Private Sub CalculateBalanceOnly()
        If isLoadingEdit Then Return
        Try
            Dim amountVal As Double = 0
            Dim cashIn As Double = 0
            Dim cashOut As Double = 0
            Dim weight As Double = 0

            Double.TryParse(txtAmount.Text, amountVal)
            Double.TryParse(txtCashIn.Text, cashIn)
            Double.TryParse(txtCashOut.Text, cashOut)
            Double.TryParse(txtBuySaleVal.Text, weight)

            Dim selectedType As String = cmbBuySale.Text.Trim().ToUpper()
            Dim netEffect As Double = 0

            If weight > 0 Then
                If selectedType = "SALE" Then
                    netEffect += amountVal
                ElseIf selectedType = "BUY" Then
                    netEffect -= amountVal
                End If
            End If

            If cashIn > 0 Then
                netEffect -= cashIn
            End If

            If cashOut > 0 Then
                netEffect += cashOut
            End If

            txtBalance.Text = Math.Round(netEffect).ToString("0")

            If netEffect >= 0 Then
                txtBalance.ForeColor = Color.Green
                If netEffect > 0 Then txtBalance.Text = "+" & Math.Round(netEffect).ToString("0")
            Else
                txtBalance.ForeColor = Color.Red
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub GenerateNextPerchiNo()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim checkTbl As New SQLiteCommand("CREATE TABLE IF NOT EXISTS Transactions (PerchiNo TEXT UNIQUE, Date TEXT, CustomerName TEXT, Type TEXT, Weight REAL, Rate REAL, Amount REAL, CashIn REAL, CashOut REAL, Balance REAL);", conn)
                checkTbl.ExecuteNonQuery()

                Dim cmd As New SQLiteCommand("SELECT PerchiNo FROM Transactions ORDER BY ROWID DESC LIMIT 1", conn)
                Dim lastPerchi As Object = cmd.ExecuteScalar()

                If lastPerchi IsNot Nothing AndAlso Not DBNull.Value.Equals(lastPerchi) Then
                    Dim lastNoStr As String = lastPerchi.ToString()
                    Dim numericPart As Integer = 0
                    If Integer.TryParse(lastNoStr.Replace("SP-", "").Trim(), numericPart) Then
                        numericPart += 1
                        txtPerchiNo.Text = "SP-" & numericPart.ToString("D6")
                    Else
                        txtPerchiNo.Text = "SP-000101"
                    End If
                Else
                    txtPerchiNo.Text = "SP-000101"
                End If
            End Using
        Catch ex As Exception
            txtPerchiNo.Text = "SP-000101"
        End Try
    End Sub

    Private Sub BtnSavePerchi_Click(sender As Object, e As EventArgs)
        If cmbCustName.SelectedIndex = -1 OrElse String.IsNullOrWhiteSpace(cmbCustName.Text) Then
            MessageBox.Show("Barah-e-karam customer select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()

                Dim createTblQuery As String = "CREATE TABLE IF NOT EXISTS Transactions (" &
                                      "PerchiNo TEXT UNIQUE, Date TEXT, CustomerName TEXT, Type TEXT, " &
                                      "Weight REAL, Rate REAL, Amount REAL, CashIn REAL, CashOut REAL, Balance REAL);"
                Using cmdCreate As New SQLiteCommand(createTblQuery, conn)
                    cmdCreate.ExecuteNonQuery()
                End Using

                Dim cIn As Double = 0
                Dim cOut As Double = 0
                Double.TryParse(txtCashIn.Text, cIn)
                Double.TryParse(txtCashOut.Text, cOut)

                Dim query As String = "INSERT OR REPLACE INTO Transactions (PerchiNo, Date, CustomerName, Type, Weight, Rate, Amount, CashIn, CashOut, Balance) " &
                                      "VALUES (@PerchiNo, @Date, @CustomerName, @Type, @Weight, @Rate, @Amount, @CashIn, @CashOut, @Balance)"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PerchiNo", txtPerchiNo.Text.Trim())
                    cmd.Parameters.AddWithValue("@Date", dtpDate.Value.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@CustomerName", cmbCustName.Text.Trim())
                    cmd.Parameters.AddWithValue("@Type", cmbBuySale.Text.Trim())
                    cmd.Parameters.AddWithValue("@Weight", Convert.ToDouble(If(String.IsNullOrEmpty(txtBuySaleVal.Text), "0", txtBuySaleVal.Text)))
                    cmd.Parameters.AddWithValue("@Rate", Convert.ToDouble(If(String.IsNullOrEmpty(txtRate.Text), "0", txtRate.Text)))
                    cmd.Parameters.AddWithValue("@Amount", Convert.ToDouble(If(String.IsNullOrEmpty(txtAmount.Text), "0", txtAmount.Text)))
                    cmd.Parameters.AddWithValue("@CashIn", cIn)
                    cmd.Parameters.AddWithValue("@CashOut", cOut)
                    cmd.Parameters.AddWithValue("@Balance", Convert.ToDouble(If(String.IsNullOrEmpty(txtBalance.Text), "0", txtBalance.Text)))
                    cmd.ExecuteNonQuery()
                End Using

                Dim updateCust As String = "UPDATE Customers SET CashBalance = @CashBal WHERE Name = @Name"
                Using cmdUp As New SQLiteCommand(updateCust, conn)
                    cmdUp.Parameters.AddWithValue("@CashBal", Convert.ToDouble(If(String.IsNullOrEmpty(txtBalance.Text), "0", txtBalance.Text)))
                    cmdUp.Parameters.AddWithValue("@Name", cmbCustName.Text.Trim())
                    cmdUp.ExecuteNonQuery()
                End Using
            End Using

            LoadDailyClosingSummary()

        Catch ex As Exception
            MessageBox.Show("Error saving transaction: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnAddCustomer_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtNewCust.Text) Then
            MessageBox.Show("Barah-e-karam customer name darj karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "INSERT INTO Customers (Name, Mobile, Address) VALUES (@Name, @Mobile, @Address)"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Name", txtNewCust.Text.Trim())
                    cmd.Parameters.AddWithValue("@Mobile", txtNewMobile.Text.Trim())
                    cmd.Parameters.AddWithValue("@Address", txtNewAddr.Text.Trim())
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Naya Customer kamyabi se add ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadCustomerData()
            cmbCustName.Text = txtNewCust.Text.Trim()
            txtMobile.Text = txtNewMobile.Text.Trim()
            txtAddress.Text = txtNewAddr.Text.Trim()

            txtNewCust.Clear()
            txtNewMobile.Clear()
            txtNewAddr.Clear()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadPerchiForEdit(perchiNo As String)
        isLoadingEdit = True
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT * FROM Transactions WHERE PerchiNo = @PerchiNo"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PerchiNo", perchiNo)
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            txtPerchiNo.Text = If(dr("PerchiNo") IsNot DBNull.Value, dr("PerchiNo").ToString(), "")

                            Dim cName As String = If(dr("CustomerName") IsNot DBNull.Value, dr("CustomerName").ToString(), "")
                            If cmbCustName.Items.Contains(cName) Then
                                cmbCustName.SelectedItem = cName
                            Else
                                cmbCustName.Text = cName
                            End If

                            cmbBuySale.Text = If(dr("Type") IsNot DBNull.Value, dr("Type").ToString(), "BUY")
                            txtBuySaleVal.Text = If(dr("Weight") IsNot DBNull.Value, dr("Weight").ToString(), "")
                            txtRate.Text = If(dr("Rate") IsNot DBNull.Value, dr("Rate").ToString(), "")
                            txtAmount.Text = If(dr("Amount") IsNot DBNull.Value, dr("Amount").ToString(), "")

                            Dim cIn As Double = 0
                            Dim cOut As Double = 0

                            Try
                                If dr("CashIn") IsNot DBNull.Value Then Double.TryParse(dr("CashIn").ToString(), cIn)
                                If dr("CashOut") IsNot DBNull.Value Then Double.TryParse(dr("CashOut").ToString(), cOut)
                            Catch ex2 As Exception
                            End Try

                            If cIn = 0 AndAlso cOut = 0 Then
                                Dim weight As Double = 0
                                Double.TryParse(txtBuySaleVal.Text, weight)
                                Dim amount As Double = 0
                                Double.TryParse(txtAmount.Text, amount)
                                Dim tType As String = cmbBuySale.Text.Trim().ToUpper()
                                If weight > 0 Then
                                    If tType = "SALE" Then cIn = amount
                                    If tType = "BUY" Then cOut = amount
                                End If
                            End If

                            txtCashIn.Text = If(cIn > 0, cIn.ToString("0"), "")
                            txtCashOut.Text = If(cOut > 0, cOut.ToString("0"), "")
                            txtBalance.Text = If(dr("Balance") IsNot DBNull.Value, dr("Balance").ToString(), "")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading perchi for edit: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            isLoadingEdit = False
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim data As New PrintData()
        data.PerchiNo = txtPerchiNo.Text.Trim()
        data.PerchiDate = dtpDate.Value.ToString("dd/MM/yyyy")
        data.PerchiTime = txtTime.Text.Trim()
        data.CustomerName = cmbCustName.Text.Trim()
        data.Mobile = txtMobile.Text.Trim()
        data.TransactionType = cmbBuySale.Text.Trim()

        Dim val As Double = 0
        Double.TryParse(txtBuySaleVal.Text, val)
        If data.TransactionType.ToUpper() = "BUY" Then
            data.Buy = val
            data.Sale = 0
        Else
            data.Sale = val
            data.Buy = 0
        End If

        Double.TryParse(txtCurrentRate.Text, data.CurrentRate)
        Double.TryParse(txtCut.Text, data.Cut)
        Double.TryParse(txtRate.Text, data.Rate)
        Double.TryParse(txtAmount.Text, data.Amount)

        PrintManager.PrintSlip(e.Graphics, data)
    End Sub

    Private Sub BtnPreview_Click()
        Dim previewDlg As New PrintPreviewDialog()
        previewDlg.Document = printDoc
        previewDlg.Width = 600
        previewDlg.Height = 500
        previewDlg.ShowDialog()
    End Sub

    Private Sub BtnPrint_Click()
        BtnSavePerchi_Click(Nothing, Nothing)
        printDoc.Print()
    End Sub

    Private Sub ClearForm()
        isLoadingEdit = False
        previousCustomerBalance = 0
        cmbCustName.SelectedIndex = -1
        cmbCustName.Text = ""
        txtMobile.Clear()
        txtAddress.Clear()
        txtBuySaleVal.Clear()
        txtCurrentRate.Clear()
        txtCut.Clear()
        txtRate.Clear()
        txtCashIn.Clear()
        txtCashOut.Clear()
        txtAmount.Clear()
        txtBalance.Clear()

        GenerateNextPerchiNo()
        LoadDailyClosingSummary()
        txtBuySaleVal.Focus()
    End Sub

    Private Sub HandleNavClick(btnName As String)
        Select Case btnName.Trim()
            Case "Save Perchi"
                BtnSavePerchi_Click(Nothing, Nothing)
                MessageBox.Show("Perchi kamyabi se save ho gayi!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
            Case "Preview"
                BtnPreview_Click()
            Case "Print"
                BtnPrint_Click()
                MessageBox.Show("Perchi save aur print ho gayi!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
            Case "Perchi"
                Dim frm As New FormPerchi()
                frm.Show()
            Case "Dashboard"
                Me.Close()
            Case "Customer"
                Dim frm As New FormCustomerDirectory()
                frm.Show()
            Case "History"
                Dim frm As New FormHistory()
                frm.Show()
            Case "DailyReport"
                Dim frm As New FormDailyReport()
                frm.Show()
            Case "Worth"
                Dim frm As New FormWorth()
                frm.Show()
            Case "Ginni"
                Dim frm As FormGinni = CType(Application.OpenForms("FormGinni"), FormGinni)
                If frm IsNot Nothing Then
                    frm.BringToFront()
                Else
                    Dim newFrm As New FormGinni()
                    newFrm.Show()
                End If
            Case "Accounts"
                Dim frm As FormKhata = CType(Application.OpenForms("FormKhata"), FormKhata)
                If frm IsNot Nothing Then
                    frm.BringToFront()
                Else
                    Dim newRefFrm As New FormKhata()
                    newRefFrm.Show()
                End If
            Case "Cancel"
                ClearForm()
        End Select
    End Sub
End Class