Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing

Public Class FormPerchi
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    ' UI Panels & Headers
    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    ' Form Controls
    Private lblCustomer As New Label()
    Private cmbCustomer As New ComboBox()

    Private lblSerialNo As New Label()
    Private txtSerialNo As New TextBox()

    Private lblDate As New Label()
    Private txtDate As New TextBox()

    Private lblTime As New Label()
    Private txtTime As New TextBox()

    Private lblAddCustTitle As New Label()
    Private txtAddCustomer As New TextBox()
    Private btnAddName As New Button()

    Private lblWeight As New Label()
    Private txtWeight As New TextBox()

    Private lblCutStone As New Label()
    Private txtCutStone As New TextBox()

    Private lblNetWeight As New Label()
    Private txtNetWeight As New TextBox()

    Private lblRattiIn As New Label()
    Private txtRattiIn As New TextBox()

    Private lblPure As New Label()
    Private txtPure As New TextBox()

    Private lblRate As New Label()
    Private txtRate As New TextBox()

    Private lblAmount As New Label()
    Private txtAmount As New TextBox()

    ' Action Buttons
    Private btnPrintSave As New Button()
    Private btnSavePerchi As New Button()
    Private btnHistory As New Button()

    Public Sub New()
        Me.Text = "Sultan Soft Perchi System"
        Me.Size = New Size(750, 520)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
    End Sub

    Private Sub SetupUI()
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Sultan Soft Perchi System"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 16, FontStyle.Bold)
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

        Dim xLeft As Integer = 30
        Dim yStart As Integer = 80
        Dim gapY As Integer = 40

        lblCustomer.Text = "Customer Name:"
        lblCustomer.Location = New Point(xLeft, yStart)
        lblCustomer.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblCustomer.ForeColor = Color.DarkBlue
        lblCustomer.AutoSize = True

        cmbCustomer.Location = New Point(xLeft + 130, yStart - 2)
        cmbCustomer.Size = New Size(180, 25)
        cmbCustomer.DropDownStyle = ComboBoxStyle.DropDown
        cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems

        lblSerialNo.Text = "Perchi No:"
        lblSerialNo.Location = New Point(xLeft, yStart + gapY)
        lblSerialNo.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblSerialNo.ForeColor = Color.DarkBlue
        lblSerialNo.AutoSize = True

        txtSerialNo.Location = New Point(xLeft + 130, yStart + gapY - 2)
        txtSerialNo.Size = New Size(180, 25)
        txtSerialNo.TextAlign = HorizontalAlignment.Center
        txtSerialNo.ReadOnly = True
        txtSerialNo.TabStop = False

        lblAddCustTitle.Text = "Add Customer:"
        lblAddCustTitle.Location = New Point(xLeft, yStart + (gapY * 2))
        lblAddCustTitle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblAddCustTitle.ForeColor = Color.DarkBlue
        lblAddCustTitle.AutoSize = True

        txtAddCustomer.Location = New Point(xLeft + 130, yStart + (gapY * 2) - 2)
        txtAddCustomer.Size = New Size(180, 25)

        btnAddName = Create3DButton("Add Name", New Point(xLeft + 130, yStart + (gapY * 3)), New Size(180, 32))
        AddHandler btnAddName.Click, AddressOf btnAddName_Click

        btnPrintSave = Create3DButton("Print & Save", New Point(xLeft, yStart + (gapY * 4.5)), New Size(135, 35))
        btnSavePerchi = Create3DButton("Save Only", New Point(xLeft + 145, yStart + (gapY * 4.5)), New Size(135, 35))
        btnHistory = Create3DButton("History", New Point(xLeft, yStart + (gapY * 5.6)), New Size(280, 35))

        AddHandler btnPrintSave.Click, AddressOf btnPrintSave_Click
        AddHandler btnSavePerchi.Click, AddressOf btnSavePerchi_Click
        AddHandler btnHistory.Click, AddressOf btnHistory_Click

        Dim xRight As Integer = 400
        Dim rY As Integer = 80
        Dim rGap As Integer = 38

        lblDate.Text = "Date:"
        lblDate.Location = New Point(xRight, rY)
        lblDate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblDate.ForeColor = Color.DarkBlue
        lblDate.AutoSize = True

        txtDate.Location = New Point(xRight + 100, rY - 2)
        txtDate.Size = New Size(170, 25)
        txtDate.ReadOnly = True
        txtDate.TabStop = False

        lblTime.Text = "Time:"
        lblTime.Location = New Point(xRight, rY + rGap)
        lblTime.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblTime.ForeColor = Color.DarkBlue
        lblTime.AutoSize = True

        txtTime.Location = New Point(xRight + 100, rY + rGap - 2)
        txtTime.Size = New Size(170, 25)
        txtTime.ReadOnly = True
        txtTime.TabStop = False

        lblWeight.Text = "Weight:"
        lblWeight.Location = New Point(xRight, rY + (rGap * 2))
        lblWeight.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblWeight.ForeColor = Color.DarkBlue
        lblWeight.AutoSize = True

        txtWeight.Location = New Point(xRight + 100, rY + (rGap * 2) - 2)
        txtWeight.Size = New Size(170, 25)

        lblCutStone.Text = "Cut Stone:"
        lblCutStone.Location = New Point(xRight, rY + (rGap * 3))
        lblCutStone.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblCutStone.ForeColor = Color.DarkBlue
        lblCutStone.AutoSize = True

        txtCutStone.Location = New Point(xRight + 100, rY + (rGap * 3) - 2)
        txtCutStone.Size = New Size(170, 25)

        lblNetWeight.Text = "Net Weight:"
        lblNetWeight.Location = New Point(xRight, rY + (rGap * 4))
        lblNetWeight.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblNetWeight.ForeColor = Color.DarkBlue
        lblNetWeight.AutoSize = True

        txtNetWeight.Location = New Point(xRight + 100, rY + (rGap * 4) - 2)
        txtNetWeight.Size = New Size(170, 25)
        txtNetWeight.ReadOnly = True
        txtNetWeight.BackColor = Color.LightYellow

        lblRattiIn.Text = "Ratti / In:"
        lblRattiIn.Location = New Point(xRight, rY + (rGap * 5))
        lblRattiIn.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblRattiIn.ForeColor = Color.DarkBlue
        lblRattiIn.AutoSize = True

        txtRattiIn.Location = New Point(xRight + 100, rY + (rGap * 5) - 2)
        txtRattiIn.Size = New Size(170, 25)

        lblPure.Text = "Pure Gold:"
        lblPure.Location = New Point(xRight, rY + (rGap * 6))
        lblPure.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblPure.ForeColor = Color.DarkBlue
        lblPure.AutoSize = True

        txtPure.Location = New Point(xRight + 100, rY + (rGap * 6) - 2)
        txtPure.Size = New Size(170, 25)
        txtPure.ReadOnly = True
        txtPure.BackColor = Color.LightYellow

        lblRate.Text = "Rate:"
        lblRate.Location = New Point(xRight, rY + (rGap * 7))
        lblRate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblRate.ForeColor = Color.DarkBlue
        lblRate.AutoSize = True

        txtRate.Location = New Point(xRight + 100, rY + (rGap * 7) - 2)
        txtRate.Size = New Size(170, 25)

        lblAmount.Text = "Amount:"
        lblAmount.Location = New Point(xRight, rY + (rGap * 8))
        lblAmount.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblAmount.ForeColor = Color.DarkBlue
        lblAmount.AutoSize = True

        txtAmount.Location = New Point(xRight + 100, rY + (rGap * 8) - 2)
        txtAmount.Size = New Size(170, 25)
        txtAmount.ReadOnly = True
        txtAmount.BackColor = Color.LightYellow
        txtAmount.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)

        Me.Controls.Add(headerPanel)
        Me.Controls.Add(footerPanel)
        Me.Controls.Add(lblCustomer)
        Me.Controls.Add(cmbCustomer)
        Me.Controls.Add(lblSerialNo)
        Me.Controls.Add(txtSerialNo)
        Me.Controls.Add(lblDate)
        Me.Controls.Add(txtDate)
        Me.Controls.Add(lblTime)
        Me.Controls.Add(txtTime)
        Me.Controls.Add(lblAddCustTitle)
        Me.Controls.Add(txtAddCustomer)
        Me.Controls.Add(btnAddName)
        Me.Controls.Add(lblWeight)
        Me.Controls.Add(txtWeight)
        Me.Controls.Add(lblCutStone)
        Me.Controls.Add(txtCutStone)
        Me.Controls.Add(lblNetWeight)
        Me.Controls.Add(txtNetWeight)
        Me.Controls.Add(lblRattiIn)
        Me.Controls.Add(txtRattiIn)
        Me.Controls.Add(lblPure)
        Me.Controls.Add(txtPure)
        Me.Controls.Add(lblRate)
        Me.Controls.Add(txtRate)
        Me.Controls.Add(lblAmount)
        Me.Controls.Add(txtAmount)
        Me.Controls.Add(btnPrintSave)
        Me.Controls.Add(btnSavePerchi)
        Me.Controls.Add(btnHistory)

        AddHandler txtWeight.TextChanged, AddressOf CalculateValues
        AddHandler txtCutStone.TextChanged, AddressOf CalculateValues
        AddHandler txtRattiIn.TextChanged, AddressOf CalculateValues
        AddHandler txtRate.TextChanged, AddressOf CalculateValues

        AddHandler cmbCustomer.KeyDown, AddressOf MoveCursorOnEnter
        AddHandler txtWeight.KeyDown, AddressOf MoveCursorOnEnter
        AddHandler txtCutStone.KeyDown, AddressOf MoveCursorOnEnter
        AddHandler txtRattiIn.KeyDown, AddressOf MoveCursorOnEnter
        AddHandler txtRate.KeyDown, AddressOf MoveCursorOnEnter
    End Sub

    Private Function Create3DButton(text As String, loc As Point, sz As Size) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Location = loc
        btn.Size = sz
        btn.BackColor = Color.Maroon
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Standard
        Return btn
    End Function

    Private Sub FormPerchi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim cmd As New SQLiteCommand("CREATE TABLE IF NOT EXISTS Transactions (PerchiNo TEXT UNIQUE, Date TEXT, CustomerName TEXT, Type TEXT, Weight REAL, Rate REAL, Amount REAL, CashIn REAL, CashOut REAL, Balance REAL);", conn)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
        End Try

        txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        LoadCustomers()
        GenerateNextPerchiNo()

        cmbCustomer.TabStop = True
        txtWeight.TabStop = True
        txtCutStone.TabStop = True
        txtRattiIn.TabStop = True
        txtRate.TabStop = True

        cmbCustomer.TabIndex = 0
        txtWeight.TabIndex = 1
        txtCutStone.TabIndex = 2
        txtRattiIn.TabIndex = 3
        txtRate.TabIndex = 4

        Me.ActiveControl = cmbCustomer
    End Sub

    Private Sub MoveCursorOnEnter(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Select Case True
                Case sender Is cmbCustomer
                    txtWeight.Focus()
                Case sender Is txtWeight
                    txtCutStone.Focus()
                Case sender Is txtCutStone
                    txtRattiIn.Focus()
                Case sender Is txtRattiIn
                    txtRate.Focus()
                Case sender Is txtRate
                    btnPrintSave.Focus()
            End Select
        End If
    End Sub

    Public Sub GenerateNextPerchiNo()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT PerchiNo FROM Transactions ORDER BY ROWID DESC LIMIT 1", conn)
                Dim lastPerchi As Object = cmd.ExecuteScalar()

                If lastPerchi IsNot Nothing AndAlso Not DBNull.Value.Equals(lastPerchi) Then
                    Dim lastNoStr As String = lastPerchi.ToString()
                    Dim numericPart As Integer = 0
                    If Integer.TryParse(lastNoStr.Replace("SP-", "").Trim(), numericPart) Then
                        numericPart += 1
                        txtSerialNo.Text = "SP-" & numericPart.ToString("D6")
                    Else
                        txtSerialNo.Text = "SP-000101"
                    End If
                Else
                    txtSerialNo.Text = "SP-000101"
                End If
            End Using
        Catch ex As Exception
            txtSerialNo.Text = "SP-000101"
        End Try
    End Sub

    Public Sub LoadCustomerComboBox()
        LoadCustomers()
    End Sub

    Public Sub LoadCustomers()
        cmbCustomer.Items.Clear()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT Name FROM Customers ORDER BY Name ASC"
                Using cmd As New SQLiteCommand(query, conn)
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            If Not dr("Name") Is DBNull.Value Then
                                cmbCustomer.Items.Add(dr("Name").ToString().Trim())
                            End If
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAddName_Click(sender As Object, e As EventArgs)
        If txtAddCustomer.Text.Trim() <> "" Then
            Dim customerName As String = txtAddCustomer.Text.Trim()
            Try
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    Dim query As String = "INSERT OR IGNORE INTO Customers (Name) VALUES (@Name)"
                    Using cmd As New SQLiteCommand(query, conn)
                        cmd.Parameters.AddWithValue("@Name", customerName)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using

                MessageBox.Show("Customer successfully add ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomers()
                cmbCustomer.Text = customerName
                txtAddCustomer.Clear()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Baraye meherbani customer ka naam likhein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub CalculateValues(sender As Object, e As EventArgs)
        Try
            Dim weight As Double = Val(txtWeight.Text)
            Dim cutStone As Double = Val(txtCutStone.Text)
            Dim rattiIn As Double = Val(txtRattiIn.Text)
            Dim rate As Double = Val(txtRate.Text)

            Dim netWeight As Double = weight - cutStone
            txtNetWeight.Text = netWeight.ToString("0.00")

            Dim goldRatti As Double = 96 - rattiIn
            Dim rawPure As Double = (goldRatti * netWeight) / 96
            Dim pureGold As Double = Math.Round(rawPure, 3)
            txtPure.Text = pureGold.ToString("0.000")

            Dim amount As Double = (pureGold * rate) / 11.664
            txtAmount.Text = Math.Round(amount).ToString("0")

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SavePerchiRecord()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim amountVal As Double = 0
                Double.TryParse(txtAmount.Text, amountVal)

                Dim query As String = "INSERT OR REPLACE INTO Transactions (PerchiNo, Date, CustomerName, Type, Weight, Rate, Amount, CashIn, CashOut, Balance) " &
                                      "VALUES (@PerchiNo, @Date, @CustomerName, @Type, @Weight, @Rate, @Amount, @CashIn, @CashOut, @Balance)"
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@PerchiNo", txtSerialNo.Text.Trim())
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("yyyy-MM-dd"))
                    cmd.Parameters.AddWithValue("@CustomerName", cmbCustomer.Text.Trim())
                    cmd.Parameters.AddWithValue("@Type", "SALE")
                    cmd.Parameters.AddWithValue("@Weight", Val(txtNetWeight.Text))
                    cmd.Parameters.AddWithValue("@Rate", Val(txtRate.Text))
                    cmd.Parameters.AddWithValue("@Amount", amountVal)
                    cmd.Parameters.AddWithValue("@CashIn", amountVal)
                    cmd.Parameters.AddWithValue("@CashOut", 0)
                    cmd.Parameters.AddWithValue("@Balance", amountVal)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPrintSave_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(cmbCustomer.Text) Then
            MessageBox.Show("Customer Name select karna zaroori hai!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SavePerchiRecord()

        Dim pd As New PrintDocument()
        AddHandler pd.PrintPage, AddressOf PrintSlipFormat
        pd.Print()

        MessageBox.Show("Record save ho gaya aur print bhej diya gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ClearFormFields()
        GenerateNextPerchiNo()
    End Sub

    Private Sub btnSavePerchi_Click(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(cmbCustomer.Text) Then
            MessageBox.Show("Customer Name select karna zaroori hai!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        SavePerchiRecord()

        MessageBox.Show("Record ba-aasan save ho gaya (Print nahi nikla)!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ClearFormFields()
        GenerateNextPerchiNo()
        cmbCustomer.Focus()
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs)
        Dim frm As New FormHistory()
        frm.Show()
        frm.BringToFront()
    End Sub

    Private Sub PrintSlipFormat(sender As Object, e As PrintPageEventArgs)
        Dim fontHeader As New Font("Segoe Ui", 13, FontStyle.Bold)
        Dim fontNormal As New Font("Arial", 9, FontStyle.Regular)
        Dim fontBold As New Font("Arial", 9, FontStyle.Bold)
        Dim fontUrdu As New Font("Jameel Noori Nastaleeq", 11, FontStyle.Regular)
        Dim fontUrduBranding As New Font("Jameel Noori Nastaleeq", 10, FontStyle.Bold)
        Dim fontEngBranding As New Font("Arial", 9, FontStyle.Bold)

        Dim boxX As Single = 10
        Dim currentY As Single = 10
        Dim boxWidth As Single = 260
        Dim pen As New Pen(Color.Black, 2)

        Dim sfCenter As New StringFormat()
        sfCenter.Alignment = StringAlignment.Center

        e.Graphics.DrawString("SULTAN GOLD TESTING LAB", fontHeader, Brushes.Black, New RectangleF(boxX, currentY, boxWidth, 25), sfCenter)
        currentY += 28

        Dim headerBoxHeight As Single = 45
        e.Graphics.DrawRectangle(pen, boxX, currentY, boxWidth, headerBoxHeight)

        Dim innerY As Single = currentY + 6
        Dim left As Single = boxX + 8

        e.Graphics.DrawString("NAME: " & cmbCustomer.Text, fontBold, Brushes.Black, left, innerY)
        e.Graphics.DrawString("SERIAL: " & txtSerialNo.Text, fontBold, Brushes.Black, left + 135, innerY)
        innerY += 20
        e.Graphics.DrawString("DATE: " & txtDate.Text, fontNormal, Brushes.Black, left, innerY)
        e.Graphics.DrawString("TIME: " & txtTime.Text, fontNormal, Brushes.Black, left + 135, innerY)

        currentY += headerBoxHeight + 5

        Dim rowHeight As Single = 22
        Dim midX As Single = boxX + 130

        Dim labels() As String = {"WEIGHT", "CUT STONE ", "NET WEIGHT", "RATTI / IN", "PURE", "RATE", "AMOUNT"}
        Dim values() As String = {txtWeight.Text, txtCutStone.Text, txtNetWeight.Text, txtRattiIn.Text, txtPure.Text, txtRate.Text, txtAmount.Text}

        For i As Integer = 0 To labels.Length - 1
            e.Graphics.DrawRectangle(pen, boxX, currentY, boxWidth, rowHeight)
            e.Graphics.DrawLine(pen, midX, currentY, midX, currentY + rowHeight)

            Dim isBold As Boolean = (i = 2 Or i = 4 Or i = 6)
            Dim currentFont As Font = If(isBold, fontBold, fontNormal)

            e.Graphics.DrawString(labels(i), currentFont, Brushes.Black, boxX + 8, currentY + 3)
            e.Graphics.DrawString(values(i), currentFont, Brushes.Black, midX + 8, currentY + 3)

            currentY += rowHeight
        Next

        currentY += 5
        Dim footerBoxHeight As Single = 110
        e.Graphics.DrawRectangle(pen, boxX, currentY, boxWidth, footerBoxHeight)

        Dim sfUrdu As New StringFormat()
        sfUrdu.Alignment = StringAlignment.Far

        Dim line1 As String = "نوٹ :- کاؤنٹر چھوڑنے سے پہلے سونا اور رقم"
        e.Graphics.DrawString(line1, fontUrdu, Brushes.Black, New RectangleF(boxX + 5, currentY + 6, boxWidth - 10, 22), sfUrdu)

        Dim line2 As String = "چیک کر لیں بعد میں ہماری کوئی ذمہ داری نہیں ہو گی ۔"
        e.Graphics.DrawString(line2, fontUrdu, Brushes.Black, New RectangleF(boxX + 5, currentY + 30, boxWidth - 10, 22), sfUrdu)

        Dim line3 As String = "کسی بھی قسم کا سافٹ ویئر بنوانے کے لیے رابطہ کریں"
        e.Graphics.DrawString(line3, fontUrduBranding, Brushes.Black, New RectangleF(boxX + 2, currentY + 56, boxWidth - 4, 22), sfCenter)

        Dim line4 As String = "SultanSoft 03172503282"
        e.Graphics.DrawString(line4, fontEngBranding, Brushes.Black, New RectangleF(boxX + 2, currentY + 82, boxWidth - 4, 20), sfCenter)
    End Sub

    Private Sub ClearFormFields()
        txtWeight.Clear()
        txtCutStone.Clear()
        txtNetWeight.Clear()
        txtPure.Clear()
        txtRattiIn.Clear()
        txtRate.Clear()
        txtAmount.Clear()
        txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
    End Sub

End Class