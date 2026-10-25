Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormGinni
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private dgvGinni As New DataGridView()

    Private pnlBottom As New Panel()
    Private lblTotGinniHead As New Label()
    Private txtTotGinni As New TextBox()
    Private lblTotPureHead As New Label()
    Private txtTotPure As New TextBox()
    Private lblRattiInHead As New Label()
    Private txtRattiIn As New TextBox()

    Private btnEdit As New Button()
    Private btnDelete As New Button()
    Private btnClose As New Button()
    Private btnPrint As New Button()
    Private btnClear As New Button()

    Private WithEvents printDoc As New PrintDocument()

    Public Sub New()
        Me.Text = "Ginni Gold Reports"
        Me.Size = New Size(750, 580)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        LoadGinniData()
    End Sub

    Private Sub SetupUI()
        ' Header
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Ginni Gold Records"
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

        ' DataGridView Clean Setup
        dgvGinni.Location = New Point(15, 60)
        dgvGinni.Size = New Size(705, 340)
        dgvGinni.BackgroundColor = Color.White
        dgvGinni.AllowUserToAddRows = False
        dgvGinni.AllowUserToDeleteRows = False
        dgvGinni.ReadOnly = True
        dgvGinni.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvGinni.MultiSelect = False
        dgvGinni.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvGinni.RowHeadersVisible = False
        dgvGinni.RowTemplate.Height = 28

        dgvGinni.EnableHeadersVisualStyles = False
        dgvGinni.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 102)
        dgvGinni.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvGinni.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        dgvGinni.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvGinni.ColumnHeadersHeight = 35

        ' Bottom Summary & Action Panel
        pnlBottom.Dock = DockStyle.Bottom
        pnlBottom.Height = 100
        pnlBottom.BackColor = Color.FromArgb(225, 232, 245)

        lblTotGinniHead.Text = "Total Ginni:"
        lblTotGinniHead.Location = New Point(15, 15)
        lblTotGinniHead.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblTotGinniHead.ForeColor = Color.DarkBlue
        lblTotGinniHead.AutoSize = True

        txtTotGinni.Location = New Point(95, 12)
        txtTotGinni.Size = New Size(100, 26)
        txtTotGinni.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtTotGinni.TextAlign = HorizontalAlignment.Center
        txtTotGinni.ReadOnly = True
        txtTotGinni.BackColor = Color.LightYellow

        lblTotPureHead.Text = "Total Pure:"
        lblTotPureHead.Location = New Point(215, 15)
        lblTotPureHead.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblTotPureHead.ForeColor = Color.DarkBlue
        lblTotPureHead.AutoSize = True

        txtTotPure.Location = New Point(290, 12)
        txtTotPure.Size = New Size(100, 26)
        txtTotPure.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtTotPure.TextAlign = HorizontalAlignment.Center
        txtTotPure.ReadOnly = True
        txtTotPure.BackColor = Color.LightYellow

        lblRattiInHead.Text = "Ratti / In:"
        lblRattiInHead.Location = New Point(415, 15)
        lblRattiInHead.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblRattiInHead.ForeColor = Color.DarkBlue
        lblRattiInHead.AutoSize = True

        txtRattiIn.Location = New Point(485, 12)
        txtRattiIn.Size = New Size(100, 26)
        txtRattiIn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        txtRattiIn.TextAlign = HorizontalAlignment.Center
        txtRattiIn.ReadOnly = True
        txtRattiIn.BackColor = Color.LightYellow

        ' Bottom Action Buttons in One Row
        btnEdit = Create3DButton("Edit", New Point(15, 58), New Size(85, 30))
        btnDelete = Create3DButton("Delete", New Point(110, 58), New Size(85, 30))
        btnPrint = Create3DButton("Print", New Point(205, 58), New Size(85, 30))
        btnClear = Create3DButton("Clear", New Point(300, 58), New Size(85, 30))
        btnClose = Create3DButton("Close", New Point(395, 58), New Size(85, 30))

        AddHandler btnEdit.Click, AddressOf BtnEdit_Click
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        AddHandler btnPrint.Click, AddressOf BtnPrint_Click
        AddHandler btnClear.Click, AddressOf BtnClear_Click
        AddHandler btnClose.Click, Sub() Me.Close()

        pnlBottom.Controls.Add(lblTotGinniHead)
        pnlBottom.Controls.Add(txtTotGinni)
        pnlBottom.Controls.Add(lblTotPureHead)
        pnlBottom.Controls.Add(txtTotPure)
        pnlBottom.Controls.Add(lblRattiInHead)
        pnlBottom.Controls.Add(txtRattiIn)
        pnlBottom.Controls.Add(btnEdit)
        pnlBottom.Controls.Add(btnDelete)
        pnlBottom.Controls.Add(btnPrint)
        pnlBottom.Controls.Add(btnClear)
        pnlBottom.Controls.Add(btnClose)

        Me.Controls.Add(dgvGinni)
        Me.Controls.Add(pnlBottom)
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
        btn.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Standard
        Return btn
    End Function

    Private Sub LoadGinniData()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim query As String = "SELECT ROWID as ID, Name, Ginni, Pure, RattiIn FROM Ginni"
                Dim dt As New DataTable()
                Using cmd As New SQLiteCommand(query, conn)
                    Using adapter As New SQLiteDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using

                dgvGinni.DataSource = dt

                Dim sumGinni As Double = 0
                Dim sumPure As Double = 0
                Dim sumRatti As Double = 0

                For Each row As DataRow In dt.Rows
                    sumGinni += Convert.ToDouble(If(row("Ginni") IsNot DBNull.Value, row("Ginni"), 0))
                    sumPure += Convert.ToDouble(If(row("Pure") IsNot DBNull.Value, row("Pure"), 0))
                    sumRatti += Convert.ToDouble(If(row("RattiIn") IsNot DBNull.Value, row("RattiIn"), 0))
                Next

                txtTotGinni.Text = sumGinni.ToString("0.000")
                txtTotPure.Text = sumPure.ToString("0.000")
                txtRattiIn.Text = sumRatti.ToString("0.00")
            End Using
        Catch ex As Exception
        End Try
    End Sub

    ' Edit Button: Hide FormGinni and open FormAccounts with entry details
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs)
        If dgvGinni.SelectedRows.Count = 0 Then
            MessageBox.Show("Barah-e-karam edit karne ke liye row select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim selRow As DataGridViewRow = dgvGinni.SelectedRows(0)
        Dim rowId As Integer = Convert.ToInt32(selRow.Cells("ID").Value)
        Dim nameVal As String = selRow.Cells("Name").Value.ToString()
        Dim ginniVal As Double = Convert.ToDouble(selRow.Cells("Ginni").Value)
        Dim pureVal As Double = Convert.ToDouble(selRow.Cells("Pure").Value)

        Dim frmAccounts As FormAccounts = CType(Application.OpenForms("FormAccounts"), FormAccounts)

        If frmAccounts Is Nothing Then
            frmAccounts = New FormAccounts()
            frmAccounts.Show()
        Else
            frmAccounts.Show()
            frmAccounts.BringToFront()
        End If

        frmAccounts.LoadGinniForEdit(rowId, nameVal, ginniVal, pureVal)
        Me.Hide()
    End Sub

    ' Delete selected entry
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If dgvGinni.SelectedRows.Count = 0 Then
            MessageBox.Show("Barah-e-karam delete karne ke liye row select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Kya aap selected Ginni record ko delete karna chahte hain?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim rowId As Integer = Convert.ToInt32(dgvGinni.SelectedRows(0).Cells("ID").Value)
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    Using cmd As New SQLiteCommand("DELETE FROM Ginni WHERE ROWID = @id", conn)
                        cmd.Parameters.AddWithValue("@id", rowId)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                LoadGinniData()
            Catch ex As Exception
                MessageBox.Show("Error deleting: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs)
        If MessageBox.Show("Kya aap saare Ginni records delete karna chahte hain?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            Try
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    Using cmd As New SQLiteCommand("DELETE FROM Ginni", conn)
                        cmd.ExecuteNonQuery()
                    End Using
                End Using
                LoadGinniData()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Try
            Dim paperSize As New PaperSize("ThermalSlip", 280, 500)
            printDoc.DefaultPageSettings.PaperSize = paperSize
            printDoc.DefaultPageSettings.Margins = New Margins(5, 5, 5, 5)
            printDoc.Print()
        Catch ex As Exception
            MessageBox.Show("Print error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim g As Graphics = e.Graphics
        Dim fontHeader As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fontSub As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim fontRegular As New Font("Segoe UI", 8.5F, FontStyle.Regular)

        Dim formatCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim pageWidth As Integer = 270
        Dim y As Integer = 10

        g.DrawString("Sultan Gold Testing Lab", fontHeader, Brushes.Black, New RectangleF(0, y, pageWidth, 20), formatCenter)
        y += 22
        g.DrawString("GINNI REPORT SUMMARY", fontSub, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
        y += 18
        g.DrawString("Date: " & DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fontRegular, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
        y += 20

        g.DrawLine(Pens.Black, 10, y, pageWidth - 10, y)
        y += 15

        g.DrawString("Total Ginni: " & txtTotGinni.Text, fontSub, Brushes.Black, New PointF(15, y)) : y += 22
        g.DrawString("Total Pure: " & txtTotPure.Text, fontSub, Brushes.Black, New PointF(15, y)) : y += 22
        g.DrawString("Total Ratti/In: " & txtRattiIn.Text, fontSub, Brushes.Black, New PointF(15, y)) : y += 25

        g.DrawLine(Pens.Black, 10, y, pageWidth - 10, y)
        y += 10
        g.DrawString("Created By SultanSoft 03172503282", fontSub, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
    End Sub
End Class