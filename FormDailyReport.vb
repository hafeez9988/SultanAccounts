Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormDailyReport
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private pnlTopControls As New Panel()
    Private lblSelectDate As New Label()
    Private dtpFilterDate As New DateTimePicker()
    Private lblSearch As New Label()
    Private txtSearch As New TextBox()

    Private btnSearch As New Button()
    Private btnPrintSelected As New Button()
    Private btnEditEntry As New Button()
    Private btnDeleteEntry As New Button()
    Private btnClose As New Button()

    Private dgvReport As New DataGridView()

    ' Bottom Summary Section
    Private pnlBottomSummary As New Panel()

    Private lblOpningWeightHeader As New Label()
    Private txtOpningWeight As New TextBox()
    Private lblTotalBuyWeight As New Label()
    Private lblTotalSaleWeight As New Label()
    Private lblClosingWeight As New Label()

    Private lblOpningCashHeader As New Label()
    Private txtOpningCash As New TextBox()
    Private lblTotalCashIn As New Label()
    Private lblTotalCashOut As New Label()
    Private lblClosingCash As New Label()

    Private btnEditOpning As New Button()
    Private btnSaveOpning As New Button()

    Private WithEvents printDoc As New PrintDocument()
    Private printRowIndex As Integer = 0

    Public Sub New()
        Me.Text = "Sultan Perchi - Daily Report"
        Me.Size = New Size(1020, 680)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        EnsureSummaryTableExists()
        SetupUI()
        LoadDailyReportData()
    End Sub

    Private Sub EnsureSummaryTableExists()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim sql As String = "CREATE TABLE IF NOT EXISTS DailySummary (" &
                     "ReportDate TEXT PRIMARY KEY, " &
                     "OpningWeight REAL, OpningCash REAL, " &
                     "ClosingWeight REAL, ClosingCash REAL);"
                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SetupUI()
        ' Header
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Daily Transaction Report"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        headerPanel.Controls.Add(lblTitle)

        ' Footer
        footerPanel.Dock = DockStyle.Bottom
        footerPanel.Height = 35
        footerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblFooter.Text = "Created By SultanSoft 03172503282"
        lblFooter.ForeColor = Color.White
        lblFooter.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblFooter.Dock = DockStyle.Fill
        lblFooter.TextAlign = ContentAlignment.MiddleCenter
        footerPanel.Controls.Add(lblFooter)

        ' Top Controls
        pnlTopControls.Dock = DockStyle.Top
        pnlTopControls.Height = 75
        pnlTopControls.BackColor = Color.FromArgb(235, 240, 250)

        lblSelectDate.Text = "Select Date :"
        lblSelectDate.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblSelectDate.ForeColor = Color.DarkBlue
        lblSelectDate.Location = New Point(20, 12)
        lblSelectDate.AutoSize = True

        dtpFilterDate.Location = New Point(105, 9)
        dtpFilterDate.Size = New Size(120, 23)
        dtpFilterDate.Format = DateTimePickerFormat.Short
        AddHandler dtpFilterDate.ValueChanged, AddressOf LoadDailyReportData

        lblSearch.Text = "Search Name / Perchi :"
        lblSearch.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblSearch.ForeColor = Color.DarkBlue
        lblSearch.Location = New Point(245, 12)
        lblSearch.AutoSize = True

        txtSearch.Location = New Point(395, 9)
        txtSearch.Size = New Size(150, 23)
        txtSearch.Font = New Font("Segoe UI", 9)
        AddHandler txtSearch.TextChanged, AddressOf LoadDailyReportData

        btnSearch = Create3DButton("Search", New Point(20, 40), New Size(90, 28))
        btnPrintSelected = Create3DButton("Print Selected", New Point(118, 40), New Size(110, 28))
        btnEditEntry = Create3DButton("Edit Entry", New Point(236, 40), New Size(95, 28))
        btnDeleteEntry = Create3DButton("Delete Entry", New Point(339, 40), New Size(95, 28))
        btnClose = Create3DButton("Close", New Point(442, 40), New Size(85, 28))

        AddHandler btnSearch.Click, AddressOf LoadDailyReportData
        AddHandler btnPrintSelected.Click, AddressOf BtnPrintSelected_Click
        AddHandler btnEditEntry.Click, AddressOf BtnEditEntry_Click
        AddHandler btnDeleteEntry.Click, AddressOf BtnDeleteEntry_Click
        AddHandler btnClose.Click, Sub() Me.Close()

        pnlTopControls.Controls.Add(lblSelectDate)
        pnlTopControls.Controls.Add(dtpFilterDate)
        pnlTopControls.Controls.Add(lblSearch)
        pnlTopControls.Controls.Add(txtSearch)
        pnlTopControls.Controls.Add(btnSearch)
        pnlTopControls.Controls.Add(btnPrintSelected)
        pnlTopControls.Controls.Add(btnEditEntry)
        pnlTopControls.Controls.Add(btnDeleteEntry)
        pnlTopControls.Controls.Add(btnClose)

        ' Bottom Summary Panel
        pnlBottomSummary.Dock = DockStyle.Bottom
        pnlBottomSummary.Height = 120
        pnlBottomSummary.BackColor = Color.FromArgb(225, 232, 245)
        pnlBottomSummary.BorderStyle = BorderStyle.FixedSingle

        ' Left Side: Gold Section
        lblOpningWeightHeader.Text = "Opning weight"
        lblOpningWeightHeader.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblOpningWeightHeader.Location = New Point(20, 15)
        lblOpningWeightHeader.AutoSize = True

        txtOpningWeight.Location = New Point(280, 12)
        txtOpningWeight.Size = New Size(100, 25)
        txtOpningWeight.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtOpningWeight.Text = "0.000"
        txtOpningWeight.TextAlign = HorizontalAlignment.Center
        txtOpningWeight.ReadOnly = True

        lblTotalBuyWeight.Text = "Total Buy Weight: 0.000"
        lblTotalBuyWeight.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblTotalBuyWeight.Location = New Point(20, 40)
        lblTotalBuyWeight.AutoSize = True

        lblTotalSaleWeight.Text = "Total Sale Weight: 0.000"
        lblTotalSaleWeight.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblTotalSaleWeight.Location = New Point(20, 62)
        lblTotalSaleWeight.AutoSize = True

        lblClosingWeight.Text = "Closing Balance: 0.000"
        lblClosingWeight.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblClosingWeight.ForeColor = Color.DarkBlue
        lblClosingWeight.Location = New Point(20, 85)
        lblClosingWeight.AutoSize = True

        ' Right Side: Cash Section
        lblOpningCashHeader.Text = "Opning Cash"
        lblOpningCashHeader.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblOpningCashHeader.Location = New Point(420, 15)
        lblOpningCashHeader.AutoSize = True

        txtOpningCash.Location = New Point(680, 12)
        txtOpningCash.Size = New Size(120, 25)
        txtOpningCash.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtOpningCash.Text = "0"
        txtOpningCash.TextAlign = HorizontalAlignment.Center
        txtOpningCash.ReadOnly = True

        lblTotalCashIn.Text = "Total Cash In: 0"
        lblTotalCashIn.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblTotalCashIn.Location = New Point(420, 40)
        lblTotalCashIn.AutoSize = True

        lblTotalCashOut.Text = "Total Cash Out: 0"
        lblTotalCashOut.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        lblTotalCashOut.Location = New Point(420, 62)
        lblTotalCashOut.AutoSize = True

        lblClosingCash.Text = "Closing Cash: 0"
        lblClosingCash.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        lblClosingCash.ForeColor = Color.DarkGreen
        lblClosingCash.Location = New Point(420, 85)
        lblClosingCash.AutoSize = True

        btnEditOpning = Create3DButton("Edit Opning", New Point(830, 40), New Size(95, 28))
        btnSaveOpning = Create3DButton("Save Opning", New Point(830, 75), New Size(95, 28))

        AddHandler btnEditOpning.Click, AddressOf BtnEditOpning_Click
        AddHandler btnSaveOpning.Click, AddressOf BtnSaveOpning_Click

        pnlBottomSummary.Controls.Add(lblOpningWeightHeader)
        pnlBottomSummary.Controls.Add(txtOpningWeight)
        pnlBottomSummary.Controls.Add(lblTotalBuyWeight)
        pnlBottomSummary.Controls.Add(lblTotalSaleWeight)
        pnlBottomSummary.Controls.Add(lblClosingWeight)

        pnlBottomSummary.Controls.Add(lblOpningCashHeader)
        pnlBottomSummary.Controls.Add(txtOpningCash)
        pnlBottomSummary.Controls.Add(lblTotalCashIn)
        pnlBottomSummary.Controls.Add(lblTotalCashOut)
        pnlBottomSummary.Controls.Add(lblClosingCash)

        pnlBottomSummary.Controls.Add(btnEditOpning)
        pnlBottomSummary.Controls.Add(btnSaveOpning)

        ' DataGridView
        dgvReport.Dock = DockStyle.Fill
        dgvReport.BackgroundColor = Color.White
        dgvReport.ReadOnly = True
        dgvReport.AllowUserToAddRows = False
        dgvReport.AllowUserToDeleteRows = False
        dgvReport.MultiSelect = True
        dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvReport.RowHeadersVisible = False
        dgvReport.RowTemplate.Height = 25

        dgvReport.EnableHeadersVisualStyles = False
        dgvReport.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 102)
        dgvReport.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvReport.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvReport.ColumnHeadersHeight = 32

        AddHandler dgvReport.CellFormatting, AddressOf DgvReport_CellFormatting

        Me.Controls.Add(dgvReport)
        Me.Controls.Add(pnlBottomSummary)
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

    Private Sub LoadDailyReportData(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        Try
            Dim selDate As String = dtpFilterDate.Value.ToString("yyyy-MM-dd")

            ' Load Saved Opening Balances
            Dim opWeight As Double = 0
            Dim opCash As Double = 0
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim cmdOp As New SQLiteCommand("SELECT OpningWeight, OpningCash FROM DailySummary WHERE ReportDate = @rDate", conn)
                cmdOp.Parameters.AddWithValue("@rDate", selDate)
                Using drOp As SQLiteDataReader = cmdOp.ExecuteReader()
                    If drOp.Read() Then
                        opWeight = Convert.ToDouble(If(drOp("OpningWeight") IsNot DBNull.Value, drOp("OpningWeight"), 0))
                        opCash = Convert.ToDouble(If(drOp("OpningCash") IsNot DBNull.Value, drOp("OpningCash"), 0))
                    End If
                End Using
            End Using

            txtOpningWeight.Text = opWeight.ToString("0.000")
            txtOpningCash.Text = Math.Round(opCash).ToString("0")

            ' Load Daily Transactions
            Dim dt As New DataTable()
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT ROWID, Date, PerchiNo, CustomerName, Type, Weight, Rate, Amount, " &
                     "IFNULL(CashIn, 0) AS CashIn, IFNULL(CashOut, 0) AS CashOut, Balance " &
                     "FROM Transactions " &
                     "WHERE (Date = @SelDate OR Date LIKE @SelDateLike) " &
                     "AND (PerchiNo LIKE @Search OR CustomerName LIKE @Search) " &
                     "ORDER BY ROWID DESC"

                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@SelDate", selDate)
                    cmd.Parameters.AddWithValue("@SelDateLike", "%" & selDate & "%")
                    cmd.Parameters.AddWithValue("@Search", "%" & txtSearch.Text.Trim() & "%")
                    Using adapter As New SQLiteDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using

            Dim gridDt As New DataTable()
            gridDt.Columns.Add("Perchi No", GetType(String))
            gridDt.Columns.Add("Date", GetType(String))
            gridDt.Columns.Add("Name", GetType(String))
            gridDt.Columns.Add("Buy", GetType(Double))
            gridDt.Columns.Add("Sale", GetType(Double))
            gridDt.Columns.Add("Rate", GetType(Double))
            gridDt.Columns.Add("Amount", GetType(Double))
            gridDt.Columns.Add("CashIn", GetType(Double))
            gridDt.Columns.Add("CashOut", GetType(Double))
            gridDt.Columns.Add("Balance", GetType(Double))

            Dim totalBuy As Double = 0
            Dim totalSale As Double = 0
            Dim totalCashIn As Double = 0
            Dim totalCashOut As Double = 0

            For Each row As DataRow In dt.Rows
                Dim tType As String = If(row("Type") IsNot DBNull.Value, row("Type").ToString().ToUpper(), "BUY")
                Dim weight As Double = Convert.ToDouble(If(row("Weight") IsNot DBNull.Value, row("Weight"), 0))
                Dim rate As Double = Convert.ToDouble(If(row("Rate") IsNot DBNull.Value, row("Rate"), 0))
                Dim amount As Double = Convert.ToDouble(If(row("Amount") IsNot DBNull.Value, row("Amount"), 0))
                Dim bal As Double = Convert.ToDouble(If(row("Balance") IsNot DBNull.Value, row("Balance"), 0))

                Dim dbCashIn As Double = Convert.ToDouble(If(row("CashIn") IsNot DBNull.Value, row("CashIn"), 0))
                Dim dbCashOut As Double = Convert.ToDouble(If(row("CashOut") IsNot DBNull.Value, row("CashOut"), 0))

                Dim buyVal As Double = If(tType = "BUY", weight, 0)
                Dim saleVal As Double = If(tType = "SALE", weight, 0)

                Dim cashIn As Double = dbCashIn
                Dim cashOut As Double = dbCashOut

                If weight > 0 AndAlso cashIn = 0 AndAlso cashOut = 0 Then
                    If tType = "SALE" Then cashIn = amount
                    If tType = "BUY" Then cashOut = amount
                End If

                totalBuy += buyVal
                totalSale += saleVal
                totalCashIn += cashIn
                totalCashOut += cashOut

                Dim nRow As DataRow = gridDt.NewRow()
                nRow("Perchi No") = row("PerchiNo").ToString()
                nRow("Date") = Convert.ToDateTime(row("Date")).ToString("yyyy-MM-dd")
                nRow("Name") = row("CustomerName").ToString()
                nRow("Buy") = buyVal
                nRow("Sale") = saleVal
                nRow("Rate") = rate
                nRow("Amount") = amount
                nRow("CashIn") = cashIn
                nRow("CashOut") = cashOut
                nRow("Balance") = bal
                gridDt.Rows.Add(nRow)
            Next

            dgvReport.DataSource = gridDt

            ' Calculate Closings
            Dim closingWeight As Double = opWeight + totalBuy - totalSale
            Dim closingCash As Double = opCash + totalCashOut - totalCashIn

            lblTotalBuyWeight.Text = "Total Buy Weight: " & totalBuy.ToString("0.000")
            lblTotalSaleWeight.Text = "Total Sale Weight: " & totalSale.ToString("0.000")
            lblClosingWeight.Text = "Closing Balance: " & closingWeight.ToString("0.000")

            lblTotalCashIn.Text = "Total Cash In: " & Math.Round(totalCashIn).ToString("0")
            lblTotalCashOut.Text = "Total Cash Out: " & Math.Round(totalCashOut).ToString("0")
            lblClosingCash.Text = "Closing Cash: " & Math.Round(closingCash).ToString("0")

            ' Auto save closing to DailySummary table
            SaveSummaryData(selDate, opWeight, opCash, closingWeight, closingCash)

        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveSummaryData(rDate As String, opW As Double, opC As Double, clW As Double, clC As Double)
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim sql As String = "INSERT OR REPLACE INTO DailySummary (ReportDate, OpningWeight, OpningCash, ClosingWeight, ClosingCash) " &
                     "VALUES (@rDate, @opW, @opC, @clW, @clC)"
                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@rDate", rDate)
                    cmd.Parameters.AddWithValue("@opW", opW)
                    cmd.Parameters.AddWithValue("@opC", opC)
                    cmd.Parameters.AddWithValue("@clW", clW)
                    cmd.Parameters.AddWithValue("@clC", clC)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnEditOpning_Click(sender As Object, e As EventArgs)
        txtOpningWeight.ReadOnly = False
        txtOpningCash.ReadOnly = False
        txtOpningWeight.Focus()
        MessageBox.Show("Aap ab Opning Weight aur Opning Cash edit kar sakte hain.", "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BtnSaveOpning_Click(sender As Object, e As EventArgs)
        Try
            Dim opW As Double = 0
            Dim opC As Double = 0
            Double.TryParse(txtOpningWeight.Text, opW)
            Double.TryParse(txtOpningCash.Text, opC)

            Dim selDate As String = dtpFilterDate.Value.ToString("yyyy-MM-dd")

            ' Calculate current closings to save immediately
            Dim totalBuy As Double = 0
            Dim totalSale As Double = 0
            Dim totalCashIn As Double = 0
            Dim totalCashOut As Double = 0

            ' Re-calculate totals from current grid data
            For Each row As DataGridViewRow In dgvReport.Rows
                Dim bVal As Double = 0
                Dim sVal As Double = 0
                Dim cInVal As Double = 0
                Dim cOutVal As Double = 0

                Double.TryParse(row.Cells("Buy").Value.ToString(), bVal)
                Double.TryParse(row.Cells("Sale").Value.ToString(), sVal)
                Double.TryParse(row.Cells("CashIn").Value.ToString(), cInVal)
                Double.TryParse(row.Cells("CashOut").Value.ToString(), cOutVal)

                totalBuy += bVal
                totalSale += sVal
                totalCashIn += cInVal
                totalCashOut += cOutVal
            Next

            Dim closingWeight As Double = opW + totalBuy - totalSale
            Dim closingCash As Double = opC + totalCashOut - totalCashIn

            ' Save to DailySummary Table
            SaveSummaryData(selDate, opW, opC, closingWeight, closingCash)

            ' Lock boxes again so values remain visible and safe
            txtOpningWeight.ReadOnly = True
            txtOpningCash.ReadOnly = True

            ' Refresh display
            LoadDailyReportData()

            MessageBox.Show("Opning Balances kamyabi se save ho gaye hain!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvReport_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvReport.Columns(e.ColumnIndex).Name = "Balance" AndAlso e.Value IsNot DBNull.Value Then
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

    Private Sub BtnEditEntry_Click(sender As Object, e As EventArgs)
        If dgvReport.SelectedRows.Count = 0 Then Return

        Dim perchiNo As String = dgvReport.SelectedRows(0).Cells("Perchi No").Value.ToString()
        Dim frmAccounts As FormAccounts = CType(Application.OpenForms("FormAccounts"), FormAccounts)

        If frmAccounts Is Nothing Then
            frmAccounts = New FormAccounts()
            frmAccounts.Show()
        Else
            frmAccounts.Show()
            frmAccounts.BringToFront()
        End If

        frmAccounts.LoadPerchiForEdit(perchiNo)
        Me.Hide()
    End Sub

    Private Sub BtnDeleteEntry_Click(sender As Object, e As EventArgs)
        If dgvReport.SelectedRows.Count = 0 Then Return

        Dim count As Integer = dgvReport.SelectedRows.Count
        Dim msg As String = If(count = 1, "Kya aap is entry ko delete karna chahte hain?", $"Kya aap in {count} selected entries ko delete karna chahte hain?")

        If MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    Using trans = conn.BeginTransaction()
                        For Each row As DataGridViewRow In dgvReport.SelectedRows
                            Dim pNo As String = row.Cells("Perchi No").Value.ToString()
                            Using cmd As New SQLiteCommand("DELETE FROM Transactions WHERE PerchiNo = @pNo", conn, trans)
                                cmd.Parameters.AddWithValue("@pNo", pNo)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        trans.Commit()
                    End Using
                End Using

                LoadDailyReportData()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub BtnPrintSelected_Click(sender As Object, e As EventArgs)
        If dgvReport.SelectedRows.Count = 0 Then Return
        printRowIndex = 0
        Try
            printDoc.Print()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        If printRowIndex < dgvReport.SelectedRows.Count Then
            Dim row As DataGridViewRow = dgvReport.SelectedRows(printRowIndex)
            Dim data As New PrintData()

            data.PerchiNo = row.Cells("Perchi No").Value.ToString()
            data.CustomerName = row.Cells("Name").Value.ToString()
            data.PerchiDate = Convert.ToDateTime(row.Cells("Date").Value).ToString("dd/MM/yyyy")

            Double.TryParse(row.Cells("Buy").Value.ToString(), data.Buy)
            Double.TryParse(row.Cells("Sale").Value.ToString(), data.Sale)
            Double.TryParse(row.Cells("Rate").Value.ToString(), data.Rate)
            Double.TryParse(row.Cells("Amount").Value.ToString(), data.Amount)

            data.TransactionType = If(data.Buy > 0, "BUY", "SALE")

            PrintManager.PrintSlip(e.Graphics, data)

            printRowIndex += 1
            e.HasMorePages = (printRowIndex < dgvReport.SelectedRows.Count)
        End If
    End Sub
End Class