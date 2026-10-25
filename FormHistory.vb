Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormHistory
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private pnlTopControls As New Panel()
    Private lblSearch As New Label()
    Private txtSearch As New TextBox()
    Private btnSearch As New Button()
    Private btnPrintSelected As New Button()
    Private btnEditEntry As New Button()
    Private btnDeleteEntry As New Button()
    Private btnClose As New Button()

    Private dgvHistory As New DataGridView()
    Private WithEvents printDoc As New PrintDocument()
    Private printRowIndex As Integer = 0

    Public Sub New()
        Me.Text = "Customer History"
        Me.Size = New Size(1020, 620)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        LoadHistoryData()
    End Sub

    Private Sub SetupUI()
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "History Record"
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
        pnlTopControls.Height = 75
        pnlTopControls.BackColor = Color.FromArgb(235, 240, 250)

        lblSearch.Text = "Search Customer / Slip No :"
        lblSearch.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblSearch.ForeColor = Color.DarkBlue
        lblSearch.Location = New Point(20, 12)
        lblSearch.AutoSize = True

        txtSearch.Location = New Point(200, 9)
        txtSearch.Size = New Size(180, 23)
        txtSearch.Font = New Font("Segoe UI", 9)
        AddHandler txtSearch.TextChanged, AddressOf LoadHistoryData

        btnSearch = Create3DButton("Search", New Point(20, 40), New Size(90, 28))
        btnPrintSelected = Create3DButton("Print Selected", New Point(118, 40), New Size(110, 28))
        btnEditEntry = Create3DButton("Edit Entry", New Point(236, 40), New Size(95, 28))
        btnDeleteEntry = Create3DButton("Delete Entry", New Point(339, 40), New Size(95, 28))
        btnClose = Create3DButton("Close", New Point(442, 40), New Size(85, 28))

        AddHandler btnSearch.Click, AddressOf LoadHistoryData
        AddHandler btnPrintSelected.Click, AddressOf BtnPrintSelected_Click
        AddHandler btnEditEntry.Click, AddressOf BtnEditEntry_Click
        AddHandler btnDeleteEntry.Click, AddressOf BtnDeleteEntry_Click
        AddHandler btnClose.Click, Sub() Me.Close()

        pnlTopControls.Controls.Add(lblSearch)
        pnlTopControls.Controls.Add(txtSearch)
        pnlTopControls.Controls.Add(btnSearch)
        pnlTopControls.Controls.Add(btnPrintSelected)
        pnlTopControls.Controls.Add(btnEditEntry)
        pnlTopControls.Controls.Add(btnDeleteEntry)
        pnlTopControls.Controls.Add(btnClose)

        dgvHistory.Dock = DockStyle.Fill
        dgvHistory.BackgroundColor = Color.White
        dgvHistory.ReadOnly = True
        dgvHistory.AllowUserToAddRows = False
        dgvHistory.AllowUserToDeleteRows = False
        dgvHistory.MultiSelect = True
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvHistory.RowHeadersVisible = False
        dgvHistory.RowTemplate.Height = 25

        dgvHistory.EnableHeadersVisualStyles = False
        dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 102)
        dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvHistory.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvHistory.ColumnHeadersHeight = 32

        AddHandler dgvHistory.CellFormatting, AddressOf DgvHistory_CellFormatting

        Me.Controls.Add(dgvHistory)
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

    Private Sub LoadHistoryData(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()

                Dim query As String = "SELECT ROWID, Date, PerchiNo, CustomerName, Type, Weight, Rate, Amount, Balance " &
                                      "FROM Transactions " &
                                      "WHERE PerchiNo LIKE @Search OR CustomerName LIKE @Search " &
                                      "ORDER BY ROWID DESC"

                Dim dt As New DataTable()
                Using cmd As New SQLiteCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Search", "%" & txtSearch.Text.Trim() & "%")
                    Using adapter As New SQLiteDataAdapter(cmd)
                        adapter.Fill(dt)
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

                For Each row As DataRow In dt.Rows
                    Dim tType As String = If(row("Type") IsNot DBNull.Value, row("Type").ToString().ToUpper(), "BUY")
                    Dim weight As Double = Convert.ToDouble(If(row("Weight") IsNot DBNull.Value, row("Weight"), 0))
                    Dim rate As Double = Convert.ToDouble(If(row("Rate") IsNot DBNull.Value, row("Rate"), 0))
                    Dim amount As Double = Convert.ToDouble(If(row("Amount") IsNot DBNull.Value, row("Amount"), 0))
                    Dim bal As Double = Convert.ToDouble(If(row("Balance") IsNot DBNull.Value, row("Balance"), 0))

                    Dim buyVal As Double = If(tType = "BUY", weight, 0)
                    Dim saleVal As Double = If(tType = "SALE", weight, 0)

                    Dim cashIn As Double = 0
                    Dim cashOut As Double = 0

                    If weight > 0 Then
                        If tType = "SALE" Then cashIn = amount
                        If tType = "BUY" Then cashOut = amount
                    Else
                        If bal < 0 Then
                            cashIn = Math.Abs(bal)
                        Else
                            cashOut = Math.Abs(bal)
                        End If
                    End If

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

                dgvHistory.DataSource = gridDt
            End Using

            For Each col As DataGridViewColumn In dgvHistory.Columns
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DgvHistory_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvHistory.Columns(e.ColumnIndex).Name = "Balance" AndAlso e.Value IsNot DBNull.Value Then
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
        If dgvHistory.SelectedRows.Count = 0 Then Return

        Dim perchiNo As String = dgvHistory.SelectedRows(0).Cells("Perchi No").Value.ToString()
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
        If dgvHistory.SelectedRows.Count = 0 Then Return

        Dim count As Integer = dgvHistory.SelectedRows.Count
        Dim msg As String = If(count = 1, "Kya aap is entry ko delete karna chahte hain?", $"Kya aap in {count} selected entries ko delete karna chahte hain?")

        If MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    Using trans = conn.BeginTransaction()
                        For Each row As DataGridViewRow In dgvHistory.SelectedRows
                            Dim pNo As String = row.Cells("Perchi No").Value.ToString()
                            Using cmd As New SQLiteCommand("DELETE FROM Transactions WHERE PerchiNo = @pNo", conn, trans)
                                cmd.Parameters.AddWithValue("@pNo", pNo)
                                cmd.ExecuteNonQuery()
                            End Using
                        Next
                        trans.Commit()
                    End Using
                End Using

                LoadHistoryData()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub BtnPrintSelected_Click(sender As Object, e As EventArgs)
        If dgvHistory.SelectedRows.Count = 0 Then Return
        printRowIndex = 0
        Try
            printDoc.Print()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        If printRowIndex < dgvHistory.SelectedRows.Count Then
            Dim row As DataGridViewRow = dgvHistory.SelectedRows(printRowIndex)
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
            e.HasMorePages = (printRowIndex < dgvHistory.SelectedRows.Count)
        End If
    End Sub
End Class