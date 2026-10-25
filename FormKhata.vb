Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormKhata
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private pnlTopControls As New Panel()
    Private btnCustSearch As New Button()
    Private cmbCustomer As New ComboBox()
    Private btnPrint As New Button()
    Private btnClose As New Button()
    Private lblCurrentBalance As New Label()

    Private dgvKhata As New DataGridView()
    Private WithEvents printDoc As New PrintDocument()

    Public Sub New()
        Me.Text = "Customer Khata / Ledger"
        Me.Size = New Size(1020, 620)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        LoadCustomerList()
    End Sub

    Private Sub SetupUI()
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Customer Ledger Accounts"
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

        pnlTopControls.Dock = DockStyle.Top
        pnlTopControls.Height = 65
        pnlTopControls.BackColor = Color.FromArgb(235, 240, 250)

        btnCustSearch = Create3DButton("Customer Search", New Point(20, 18), New Size(130, 28))

        cmbCustomer.Location = New Point(160, 21)
        cmbCustomer.Size = New Size(180, 23)
        cmbCustomer.DropDownStyle = ComboBoxStyle.DropDown
        cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        cmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems
        AddHandler cmbCustomer.SelectedIndexChanged, AddressOf LoadCustomerKhata
        AddHandler cmbCustomer.TextChanged, AddressOf LoadCustomerKhata

        btnPrint = Create3DButton("Print", New Point(350, 18), New Size(85, 28))
        AddHandler btnPrint.Click, AddressOf BtnPrint_Click

        btnClose = Create3DButton("Close", New Point(445, 18), New Size(85, 28))
        AddHandler btnClose.Click, Sub() Me.Close()

        lblCurrentBalance.Text = "CURRENT BALANCE: 0"
        lblCurrentBalance.Location = New Point(580, 20)
        lblCurrentBalance.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblCurrentBalance.ForeColor = Color.DarkBlue
        lblCurrentBalance.AutoSize = True

        pnlTopControls.Controls.Add(btnCustSearch)
        pnlTopControls.Controls.Add(cmbCustomer)
        pnlTopControls.Controls.Add(btnPrint)
        pnlTopControls.Controls.Add(btnClose)
        pnlTopControls.Controls.Add(lblCurrentBalance)

        dgvKhata.Dock = DockStyle.Fill
        dgvKhata.BackgroundColor = Color.White
        dgvKhata.ReadOnly = True
        dgvKhata.AllowUserToAddRows = False
        dgvKhata.AllowUserToDeleteRows = False
        dgvKhata.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvKhata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvKhata.RowHeadersVisible = False
        dgvKhata.RowTemplate.Height = 25

        dgvKhata.EnableHeadersVisualStyles = False
        dgvKhata.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 102)
        dgvKhata.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvKhata.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvKhata.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvKhata.ColumnHeadersHeight = 32

        AddHandler dgvKhata.CellFormatting, AddressOf DgvKhata_CellFormatting

        Me.Controls.Add(dgvKhata)
        Me.Controls.Add(pnlTopControls)
        Me.Controls.Add(footerPanel)
        Me.Controls.Add(headerPanel)
    End Sub

    Private Function Create3DButton(text As String, loc As Point, sz As Size) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Location = loc
        btn.Size = sz
        btn.BackColor = Color.Maroon
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Standard
        Return btn
    End Function

    Private Sub LoadCustomerList()
        Try
            cmbCustomer.Items.Clear()
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim cmd As New SQLiteCommand("SELECT Name FROM Customers ORDER BY Name ASC", conn)
                Dim dr As SQLiteDataReader = cmd.ExecuteReader()
                While dr.Read()
                    cmbCustomer.Items.Add(dr("Name").ToString())
                End While
            End Using
        Catch ex As Exception
        End Try
    End Sub

    ' --- Corrected Running Ledger Calculation Logic ---
    Private Sub LoadCustomerKhata(sender As Object, e As EventArgs)
        Dim custName As String = cmbCustomer.Text.Trim()
        If String.IsNullOrEmpty(custName) Then Return

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()

                Dim query As String = "SELECT ROWID, Date, PerchiNo, Type, Weight, Rate, Amount, " &
                                      "IFNULL(CashIn, 0) AS CashIn, IFNULL(CashOut, 0) AS CashOut, Balance " &
                                      "FROM Transactions " &
                                      "WHERE CustomerName = @Name " &
                                      "ORDER BY ROWID ASC"

                Dim dt As New DataTable()
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Name", custName)
                    Using adapter As New SQLiteDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using

                Dim ledgerDt As New DataTable()
                ledgerDt.Columns.Add("Date", GetType(String))
                ledgerDt.Columns.Add("Perchi No", GetType(String))
                ledgerDt.Columns.Add("Buy", GetType(Double))
                ledgerDt.Columns.Add("Sale", GetType(Double))
                ledgerDt.Columns.Add("Rate", GetType(Double))
                ledgerDt.Columns.Add("Amount", GetType(Double))
                ledgerDt.Columns.Add("CashIn", GetType(Double))
                ledgerDt.Columns.Add("CashOut", GetType(Double))
                ledgerDt.Columns.Add("Balance", GetType(Double))

                Dim runningBalance As Double = 0

                For Each row As DataRow In dt.Rows
                    Dim tType As String = If(row("Type") IsNot DBNull.Value, row("Type").ToString().ToUpper(), "BUY")
                    Dim weight As Double = Convert.ToDouble(If(row("Weight") IsNot DBNull.Value, row("Weight"), 0))
                    Dim rate As Double = Convert.ToDouble(If(row("Rate") IsNot DBNull.Value, row("Rate"), 0))
                    Dim amount As Double = Convert.ToDouble(If(row("Amount") IsNot DBNull.Value, row("Amount"), 0))

                    Dim dbCashIn As Double = Convert.ToDouble(If(row("CashIn") IsNot DBNull.Value, row("CashIn"), 0))
                    Dim dbCashOut As Double = Convert.ToDouble(If(row("CashOut") IsNot DBNull.Value, row("CashOut"), 0))

                    Dim buyVal As Double = If(tType = "BUY", weight, 0)
                    Dim saleVal As Double = If(tType = "SALE", weight, 0)

                    Dim cashIn As Double = dbCashIn
                    Dim cashOut As Double = dbCashOut

                    ' Fallback calculation if CashIn/CashOut were saved as 0 in older entries
                    If weight > 0 AndAlso cashIn = 0 AndAlso cashOut = 0 Then
                        If tType = "SALE" Then cashIn = amount
                        If tType = "BUY" Then cashOut = amount
                    End If

                    ' Cumulative Ledger Balance:
                    ' Gold Trade: SALE (+Amount), BUY (-Amount)
                    ' Cash Payments: CashIn (-CashIn), CashOut (+CashOut)
                    If weight > 0 Then
                        If tType = "SALE" Then runningBalance += amount
                        If tType = "BUY" Then runningBalance -= amount
                    Else
                        runningBalance = runningBalance - cashIn + cashOut
                    End If

                    Dim newRow As DataRow = ledgerDt.NewRow()
                    newRow("Date") = Convert.ToDateTime(row("Date")).ToString("yyyy-MM-dd")
                    newRow("Perchi No") = row("PerchiNo").ToString()
                    newRow("Buy") = buyVal
                    newRow("Sale") = saleVal
                    newRow("Rate") = rate
                    newRow("Amount") = amount
                    newRow("CashIn") = cashIn
                    newRow("CashOut") = cashOut
                    newRow("Balance") = runningBalance
                    ledgerDt.Rows.Add(newRow)
                Next

                ' Reverse DataTable for newest entries at top (ROWID DESC)
                Dim reversedDt As DataTable = ledgerDt.Clone()
                For i As Integer = ledgerDt.Rows.Count - 1 To 0 Step -1
                    reversedDt.ImportRow(ledgerDt.Rows(i))
                Next

                dgvKhata.DataSource = reversedDt

                If runningBalance >= 0 Then
                    lblCurrentBalance.Text = "CURRENT BALANCE: +" & Math.Round(runningBalance).ToString("0")
                    lblCurrentBalance.ForeColor = Color.Green
                Else
                    lblCurrentBalance.Text = "CURRENT BALANCE: " & Math.Round(runningBalance).ToString("0")
                    lblCurrentBalance.ForeColor = Color.Red
                End If

            End Using

            For Each col As DataGridViewColumn In dgvKhata.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DgvKhata_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvKhata.Columns(e.ColumnIndex).Name = "Balance" AndAlso e.Value IsNot DBNull.Value Then
            Dim bal As Double = 0
            Double.TryParse(e.Value.ToString(), bal)
            If bal >= 0 Then
                e.CellStyle.ForeColor = Color.Green
                e.CellStyle.SelectionForeColor = Color.LightGreen
                e.Value = "+" & Math.Round(bal).ToString("0")
            Else
                e.CellStyle.ForeColor = Color.Red
                e.CellStyle.SelectionForeColor = Color.Pink
                e.Value = Math.Round(bal).ToString("0")
            End If
            e.CellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Printing Customer Ledger...", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
End Class