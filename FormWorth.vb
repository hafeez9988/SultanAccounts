Imports System.Data.SQLite
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Public Class FormWorth
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    ' Left Side Controls
    Private lblTotAdvance As New Label()
    Private txtTotAdvance As New TextBox()

    Private lblTotUdhar As New Label()
    Private txtTotUdhar As New TextBox()

    Private lblCashInHand As New Label()
    Private txtCashInHand As New TextBox()

    Private lblTotalGold As New Label()
    Private txtTotalGold As New TextBox()

    Private lblRate As New Label()
    Private txtRate As New TextBox()

    Private lblGoldAmount As New Label()
    Private txtGoldAmount As New TextBox()

    Private lblWorth As New Label()
    Private txtWorth As New TextBox()

    Private btnPrint As New Button()
    Private btnClose As New Button()

    ' Right Side Controls
    Private lblHeaderUdhar As New Label()
    Private lblHeaderAdvance As New Label()

    Private pnlCustomersList As New Panel()

    Private lblTotalUdharAdvHeader As New Label()
    Private txtBottomTotalUdhar As New TextBox()
    Private txtBottomTotalAdvance As New TextBox()

    ' Arrays to store 15 ComboBoxes and TextBoxes
    Private cmbCustomers(14) As ComboBox
    Private txtUdhar(14) As TextBox
    Private txtAdvance(14) As TextBox

    Private customerList As New Dictionary(Of String, Double)()
    Private isUpdatingDropdowns As Boolean = False

    Private WithEvents printDoc As New PrintDocument()

    Public Sub New()
        Me.Text = "Worth Calculation"
        Me.Size = New Size(850, 650)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        LoadCustomerListAndData()

        AddHandler txtRate.TextChanged, AddressOf CalculateWorth
    End Sub

    Private Sub SetupUI()
        Dim leftX As Integer = 30
        Dim startY As Integer = 30
        Dim gapY As Integer = 40

        ' Left Side Controls
        CreateLabel(lblTotAdvance, "Total Advance:", New Point(leftX, startY))
        CreateTextBox(txtTotAdvance, New Point(leftX + 110, startY - 3), 110, True)

        CreateLabel(lblTotUdhar, "Total udhar:", New Point(leftX, startY + gapY))
        CreateTextBox(txtTotUdhar, New Point(leftX + 110, startY + gapY - 3), 110, True)

        CreateLabel(lblCashInHand, "Cash in Hand:", New Point(leftX, startY + (gapY * 2)))
        CreateTextBox(txtCashInHand, New Point(leftX + 110, startY + (gapY * 2) - 3), 110, True)

        CreateLabel(lblTotalGold, "Total Gold:", New Point(leftX, startY + (gapY * 3)))
        CreateTextBox(txtTotalGold, New Point(leftX + 110, startY + (gapY * 3) - 3), 110, True)

        CreateLabel(lblRate, "Rate:", New Point(leftX, startY + (gapY * 4)))
        CreateTextBox(txtRate, New Point(leftX + 110, startY + (gapY * 4) - 3), 110, False)
        txtRate.BackColor = Color.White

        CreateLabel(lblGoldAmount, "Gold Amount:", New Point(leftX, startY + (gapY * 5)))
        CreateTextBox(txtGoldAmount, New Point(leftX + 110, startY + (gapY * 5) - 3), 110, True)

        CreateLabel(lblWorth, "Worth:", New Point(leftX, startY + (gapY * 6)))
        CreateTextBox(txtWorth, New Point(leftX + 110, startY + (gapY * 6) - 3), 110, True)
        txtWorth.Font = New Font("Segoe UI", 10.5F, FontStyle.Bold)

        ' Buttons
        btnPrint = Create3DButton("Print", New Point(leftX, startY + (gapY * 7) + 10), New Size(100, 32))
        btnClose = Create3DButton("Close", New Point(leftX + 120, startY + (gapY * 7) + 10), New Size(100, 32))

        AddHandler btnPrint.Click, AddressOf BtnPrint_Click
        AddHandler btnClose.Click, Sub() Me.Close()

        ' Right Side Section Header
        Dim rightX As Integer = 270

        lblHeaderUdhar.Text = "Udhar"
        lblHeaderUdhar.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblHeaderUdhar.ForeColor = Color.DarkBlue
        lblHeaderUdhar.Location = New Point(rightX + 220, 12)
        lblHeaderUdhar.AutoSize = True

        lblHeaderAdvance.Text = "Advance"
        lblHeaderAdvance.Font = New Font("Segoe UI", 14, FontStyle.Bold)
        lblHeaderAdvance.ForeColor = Color.DarkBlue
        lblHeaderAdvance.Location = New Point(rightX + 370, 12)
        lblHeaderAdvance.AutoSize = True

        ' Scrollable Panel for 15 Rows
        pnlCustomersList.Location = New Point(rightX, 45)
        pnlCustomersList.Size = New Size(540, 490)
        pnlCustomersList.AutoScroll = True
        pnlCustomersList.BackColor = Color.FromArgb(235, 240, 250)

        ' Dynamic creation of 15 rows with DropDown style for clearing text
        Dim rowY As Integer = 5
        For i As Integer = 0 To 14
            cmbCustomers(i) = New ComboBox()
            cmbCustomers(i).Location = New Point(10, rowY)
            cmbCustomers(i).Size = New Size(160, 24)
            cmbCustomers(i).DropDownStyle = ComboBoxStyle.DropDown ' Changed to DropDown to allow typing/deleting/backspace
            cmbCustomers(i).AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbCustomers(i).AutoCompleteSource = AutoCompleteSource.ListItems
            cmbCustomers(i).BackColor = Color.White
            cmbCustomers(i).ForeColor = Color.Blue
            cmbCustomers(i).Font = New Font("Segoe UI", 10, FontStyle.Regular)
            cmbCustomers(i).Tag = i

            txtUdhar(i) = New TextBox()
            CreateTextBox(txtUdhar(i), New Point(180, rowY), 150, True)

            txtAdvance(i) = New TextBox()
            CreateTextBox(txtAdvance(i), New Point(345, rowY), 150, True)

            AddHandler cmbCustomers(i).SelectedIndexChanged, AddressOf CmbCustomer_SelectedIndexChanged
            AddHandler cmbCustomers(i).TextChanged, AddressOf CmbCustomer_TextChanged

            pnlCustomersList.Controls.Add(cmbCustomers(i))
            pnlCustomersList.Controls.Add(txtUdhar(i))
            pnlCustomersList.Controls.Add(txtAdvance(i))

            rowY += 32
        Next

        ' Bottom Totals
        lblTotalUdharAdvHeader.Text = "Total Udhar Advance:"
        lblTotalUdharAdvHeader.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblTotalUdharAdvHeader.ForeColor = Color.DarkBlue
        lblTotalUdharAdvHeader.Location = New Point(rightX + 20, 545)
        lblTotalUdharAdvHeader.AutoSize = True

        CreateTextBox(txtBottomTotalUdhar, New Point(rightX + 180, 542), 150, True)
        CreateTextBox(txtBottomTotalAdvance, New Point(rightX + 345, 542), 150, True)

        ' Add Controls to Form
        Me.Controls.Add(lblTotAdvance)
        Me.Controls.Add(txtTotAdvance)
        Me.Controls.Add(lblTotUdhar)
        Me.Controls.Add(txtTotUdhar)
        Me.Controls.Add(lblCashInHand)
        Me.Controls.Add(txtCashInHand)
        Me.Controls.Add(lblTotalGold)
        Me.Controls.Add(txtTotalGold)
        Me.Controls.Add(lblRate)
        Me.Controls.Add(txtRate)
        Me.Controls.Add(lblGoldAmount)
        Me.Controls.Add(txtGoldAmount)
        Me.Controls.Add(lblWorth)
        Me.Controls.Add(txtWorth)
        Me.Controls.Add(btnPrint)
        Me.Controls.Add(btnClose)

        Me.Controls.Add(lblHeaderUdhar)
        Me.Controls.Add(lblHeaderAdvance)
        Me.Controls.Add(pnlCustomersList)
        Me.Controls.Add(lblTotalUdharAdvHeader)
        Me.Controls.Add(txtBottomTotalUdhar)
        Me.Controls.Add(txtBottomTotalAdvance)
    End Sub

    Private Sub CreateLabel(lbl As Label, text As String, loc As Point)
        lbl.Text = text
        lbl.Location = loc
        lbl.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        lbl.ForeColor = Color.DarkBlue
        lbl.AutoSize = True
    End Sub

    Private Sub CreateTextBox(txt As TextBox, loc As Point, width As Integer, isReadOnly As Boolean)
        txt.Location = loc
        txt.Size = New Size(width, 25)
        txt.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        txt.TextAlign = HorizontalAlignment.Center
        txt.BorderStyle = BorderStyle.Fixed3D
        txt.ReadOnly = isReadOnly
        If isReadOnly Then txt.BackColor = Color.LightYellow
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

    Private Sub LoadCustomerListAndData()
        Try
            customerList.Clear()
            isUpdatingDropdowns = True

            Using conn As New SQLiteConnection(connStr)
                conn.Open()

                Dim query As String = "SELECT Name, IFNULL(CashBalance, 0) As CashBal FROM Customers ORDER BY Name ASC"
                Using cmd As New SQLiteCommand(query, conn)
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            Dim cName As String = dr("Name").ToString()
                            Dim bal As Double = Convert.ToDouble(dr("CashBal"))
                            If Not customerList.ContainsKey(cName) Then
                                customerList.Add(cName, bal)
                            End If
                        End While
                    End Using
                End Using

                Dim todayDate As String = DateTime.Now.ToString("yyyy-MM-dd")
                Dim cashInHand As Double = 0
                Dim totalGold As Double = 0

                Dim sumQuery As String = "SELECT IFNULL(ClosingCash, 0) As ClosingCash, IFNULL(ClosingWeight, 0) As ClosingWeight FROM DailySummary WHERE ReportDate = @rDate"
                Using cmdSum As New SQLiteCommand(sumQuery, conn)
                    cmdSum.Parameters.AddWithValue("@rDate", todayDate)
                    Using drSum As SQLiteDataReader = cmdSum.ExecuteReader()
                        If drSum.Read() Then
                            cashInHand = Convert.ToDouble(drSum("ClosingCash"))
                            totalGold = Convert.ToDouble(drSum("ClosingWeight"))
                        End If
                    End Using
                End Using

                txtCashInHand.Text = Math.Round(cashInHand).ToString("0")
                txtTotalGold.Text = totalGold.ToString("0.000")
            End Using

            For i As Integer = 0 To 14
                cmbCustomers(i).Items.Clear()
                For Each custName As String In customerList.Keys
                    cmbCustomers(i).Items.Add(custName)
                Next

                cmbCustomers(i).Text = ""
                txtUdhar(i).Text = "0"
                txtAdvance(i).Text = "0"
            Next

        Catch ex As Exception
        Finally
            isUpdatingDropdowns = False
        End Try

        RecalculateRightSideTotals()
    End Sub

    Private Sub CmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs)
        If isUpdatingDropdowns Then Return
        ProcessCustomerSelection(CType(sender, ComboBox))
    End Sub

    Private Sub CmbCustomer_TextChanged(sender As Object, e As EventArgs)
        If isUpdatingDropdowns Then Return
        ProcessCustomerSelection(CType(sender, ComboBox))
    End Sub

    Private Sub ProcessCustomerSelection(cmb As ComboBox)
        Dim index As Integer = Convert.ToInt32(cmb.Tag)
        Dim selectedName As String = cmb.Text.Trim()

        If String.IsNullOrEmpty(selectedName) OrElse Not customerList.ContainsKey(selectedName) Then
            txtUdhar(index).Text = "0"
            txtAdvance(index).Text = "0"
        Else
            Dim bal As Double = customerList(selectedName)
            If bal > 0 Then
                txtUdhar(index).Text = Math.Round(bal).ToString("0")
                txtAdvance(index).Text = "0"
            ElseIf bal < 0 Then
                txtUdhar(index).Text = "0"
                txtAdvance(index).Text = Math.Round(Math.Abs(bal)).ToString("0")
            Else
                txtUdhar(index).Text = "0"
                txtAdvance(index).Text = "0"
            End If
        End If

        RecalculateRightSideTotals()
    End Sub

    Private Sub RecalculateRightSideTotals()
        Dim sumUdhar As Double = 0
        Dim sumAdvance As Double = 0

        For i As Integer = 0 To 14
            Dim uVal As Double = 0
            Dim aVal As Double = 0

            Double.TryParse(txtUdhar(i).Text, uVal)
            Double.TryParse(txtAdvance(i).Text, aVal)

            sumUdhar += uVal
            sumAdvance += aVal
        Next

        txtTotUdhar.Text = Math.Round(sumUdhar).ToString("0")
        txtTotAdvance.Text = Math.Round(sumAdvance).ToString("0")
        txtBottomTotalUdhar.Text = Math.Round(sumUdhar).ToString("0")
        txtBottomTotalAdvance.Text = Math.Round(sumAdvance).ToString("0")

        CalculateWorth()
    End Sub

    Private Sub CalculateWorth(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        Try
            Dim totalGold As Double = 0
            Dim rate As Double = 0
            Dim cashInHand As Double = 0
            Dim totalUdhar As Double = 0
            Dim totalAdvance As Double = 0

            Double.TryParse(txtTotalGold.Text, totalGold)
            Double.TryParse(txtRate.Text, rate)
            Double.TryParse(txtCashInHand.Text, cashInHand)
            Double.TryParse(txtTotUdhar.Text, totalUdhar)
            Double.TryParse(txtTotAdvance.Text, totalAdvance)

            Dim goldAmt As Double = 0
            If totalGold > 0 AndAlso rate > 0 Then
                goldAmt = (totalGold * rate) / 11.664
            End If
            txtGoldAmount.Text = Math.Round(goldAmt).ToString("0")

            Dim netWorth As Double = goldAmt + cashInHand + totalUdhar - totalAdvance
            txtWorth.Text = Math.Round(netWorth).ToString("0")

            If netWorth >= 0 Then
                txtWorth.ForeColor = Color.Green
            Else
                txtWorth.ForeColor = Color.Red
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Try
            Dim paperSize As New PaperSize("ThermalSlip", 280, 500)
            printDoc.DefaultPageSettings.PaperSize = paperSize
            printDoc.DefaultPageSettings.Margins = New Margins(5, 5, 5, 5)

            printDoc.Print()
        Catch ex As Exception
            MessageBox.Show("Printing error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs) Handles printDoc.PrintPage
        Dim g As Graphics = e.Graphics
        Dim fontHeader As New Font("Segoe UI", 11, FontStyle.Bold)
        Dim fontSubHeader As New Font("Segoe UI", 8, FontStyle.Bold)
        Dim fontRegular As New Font("Segoe UI", 8.5F, FontStyle.Regular)
        Dim fontBold As New Font("Segoe UI", 9, FontStyle.Bold)

        Dim formatCenter As New StringFormat() With {.Alignment = StringAlignment.Center}
        Dim formatLeft As New StringFormat() With {.Alignment = StringAlignment.Near}
        Dim formatRight As New StringFormat() With {.Alignment = StringAlignment.Far}

        Dim pageWidth As Integer = 270
        Dim y As Integer = 10

        g.DrawString("Sultan Gold Testing Lab", fontHeader, Brushes.Black, New RectangleF(0, y, pageWidth, 20), formatCenter)
        y += 22
        g.DrawString("NET WORTH REPORT", fontSubHeader, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
        y += 18
        g.DrawString("Date: " & DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"), fontRegular, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
        y += 20

        g.DrawLine(Pens.Black, 10, y, pageWidth - 10, y)
        y += 10

        DrawPrintRow(g, "Total Advance:", txtTotAdvance.Text, fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 20
        DrawPrintRow(g, "Total Udhar:", txtTotUdhar.Text, fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 20
        DrawPrintRow(g, "Cash in Hand:", txtCashInHand.Text, fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 20
        DrawPrintRow(g, "Total Gold:", txtTotalGold.Text & " Tola", fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 20
        DrawPrintRow(g, "Current Rate:", txtRate.Text, fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 20
        DrawPrintRow(g, "Gold Amount:", txtGoldAmount.Text, fontRegular, fontBold, y, pageWidth, formatLeft, formatRight)
        y += 22

        g.DrawLine(Pens.Black, 10, y, pageWidth - 10, y)
        y += 10

        g.DrawString("NET WORTH:", fontBold, Brushes.Black, New RectangleF(10, y, 110, 20), formatLeft)
        g.DrawString(txtWorth.Text, fontHeader, Brushes.Black, New RectangleF(120, y - 2, pageWidth - 130, 22), formatRight)
        y += 25

        g.DrawLine(Pens.Black, 10, y, pageWidth - 10, y)
        y += 8
        g.DrawString("Created By SultanSoft 03172503282", fontSubHeader, Brushes.Black, New RectangleF(0, y, pageWidth, 16), formatCenter)
    End Sub

    Private Sub DrawPrintRow(g As Graphics, label As String, value As String, fontL As Font, fontV As Font, y As Integer, pWidth As Integer, fLeft As StringFormat, fRight As StringFormat)
        g.DrawString(label, fontL, Brushes.Black, New RectangleF(10, y, 120, 18), fLeft)
        g.DrawString(value, fontV, Brushes.Black, New RectangleF(130, y, pWidth - 140, 18), fRight)
    End Sub
End Class