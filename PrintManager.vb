Imports System.Drawing

Public Class PrintData
    Public Property PerchiNo As String = ""
    Public Property PerchiDate As String = ""
    Public Property PerchiTime As String = ""
    Public Property CustomerName As String = ""
    Public Property Mobile As String = ""
    Public Property TransactionType As String = "BUY"
    Public Property Buy As Double = 0
    Public Property Sale As Double = 0
    Public Property CurrentRate As Double = 0
    Public Property Cut As Double = 0
    Public Property Rate As Double = 0
    Public Property Amount As Double = 0
End Class

Public Class PrintManager

    Private Const MarginLeft As Integer = 0
    Private Const TotalWidth As Integer = 275

    ' 35% / 65%
    Private Const LabelWidth As Integer = 96
    Private Const ValueWidth As Integer = 179

    ' RowHeight 21
    Private Const RowHeight As Integer = 21

    ' Mobile Number Masking Helper (e.g. 3123577073 -> 312****073)
    Private Shared Function MaskMobileNumber(rawMobile As String) As String
        If String.IsNullOrWhiteSpace(rawMobile) Then Return ""

        Dim cleanNum As String = rawMobile.Trim()

        If cleanNum.Length >= 7 Then
            Dim first3 As String = cleanNum.Substring(0, 3)
            Dim last3 As String = cleanNum.Substring(cleanNum.Length - 3)
            Return first3 & "****" & last3
        Else
            Return cleanNum
        End If
    End Function

    Public Shared Sub PrintSlip(g As Graphics, data As PrintData)

        Dim y As Integer = 5

        ' Header Box (Compact Height)
        DrawHeader(g, y)
        y += 30

        DrawRow(g, "Perchi No", data.PerchiNo, y)
        y += RowHeight

        DrawRow(g, "Date", data.PerchiDate, y)
        y += RowHeight

        DrawRow(g, "Time", data.PerchiTime, y)
        y += RowHeight

        DrawRow(g, "Name", data.CustomerName, y)
        y += RowHeight

        ' Mobile Masking (312****073 format)
        DrawRow(g, "Mobile", MaskMobileNumber(data.Mobile), y)
        y += RowHeight

        ' Transaction Type Weight Logic
        Dim printWeight As Double = If(data.TransactionType.ToUpper() = "BUY", data.Buy, data.Sale)
        Dim weightLabel As String = If(data.TransactionType.ToUpper() = "BUY", "Buy Wt", "Sale Wt")

        DrawRow(g, weightLabel, printWeight.ToString("0.000") & " gm", y)
        y += RowHeight

        DrawRow(g, "Current Rate", data.CurrentRate.ToString("N0"), y)
        y += RowHeight

        DrawRow(g, "Cut", data.Cut.ToString("N0"), y)
        y += RowHeight

        DrawRow(g, "Rate", data.Rate.ToString("N0"), y)
        y += RowHeight

        DrawRow(g, "Amount", data.Amount.ToString("N0"), y, True)
        y += RowHeight + 4

        DrawFooter(g, y)

    End Sub

    Private Shared Sub DrawHeader(g As Graphics, y As Integer)

        Using f As New Font("Segoe UI", 12, FontStyle.Bold)

            Dim sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center

            g.DrawRectangle(Pens.Black, MarginLeft, y, TotalWidth, 28)

            g.DrawString(
                "SULTAN GOLD TESTING LAB",
                f,
                Brushes.Black,
                New RectangleF(MarginLeft, y, TotalWidth, 28),
                sf)

        End Using

    End Sub

    Private Shared Sub DrawRow(
        g As Graphics,
        labelText As String,
        valueText As String,
        y As Integer,
        Optional amountRow As Boolean = False)

        DrawCell(
            g,
            MarginLeft,
            y,
            LabelWidth,
            RowHeight,
            Color.LightGray)

        DrawCell(
            g,
            MarginLeft + LabelWidth,
            y,
            ValueWidth,
            RowHeight,
            Color.White)

        Using labelFont As New Font("Segoe UI", 9, FontStyle.Bold)

            ' Label Vertically Centered
            Dim sfLabel As New StringFormat()
            sfLabel.LineAlignment = StringAlignment.Center
            sfLabel.Alignment = StringAlignment.Near

            g.DrawString(
                labelText,
                labelFont,
                Brushes.Black,
                New RectangleF(MarginLeft + 5, y, LabelWidth - 5, RowHeight),
                sfLabel)

            Dim valueFontStyle As FontStyle =
                If(amountRow OrElse
                    labelText.Contains("Wt") OrElse
                    labelText = "Weight" OrElse
                    labelText = "Current Rate" OrElse
                    labelText = "Cut" OrElse
                    labelText = "Rate",
                    FontStyle.Bold,
                    FontStyle.Regular)

            Dim valueFontSize As Single =
                If(valueFontStyle = FontStyle.Bold, 10.5F, 9.0F)

            Using valueFont As New Font("Segoe UI", valueFontSize, valueFontStyle)

                Dim sf As New StringFormat()
                sf.Alignment = StringAlignment.Center
                sf.LineAlignment = StringAlignment.Center

                g.DrawString(
                    valueText,
                    valueFont,
                    Brushes.Black,
                    New RectangleF(
                        MarginLeft + LabelWidth,
                        y,
                        ValueWidth,
                        RowHeight),
                    sf)

            End Using

        End Using
    End Sub

    Private Shared Sub DrawFooter(g As Graphics, ByRef y As Integer)
        Using fontUrdu As New Font("Jameel Noori Nastaleeq", 9.5F, FontStyle.Regular),
              fontUrduBranding As New Font("Jameel Noori Nastaleeq", 9.5F, FontStyle.Bold),
              fontEngBranding As New Font("Arial", 8.5F, FontStyle.Bold)

            Dim sfCenter As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}

            y += 4
            Dim footerBoxHeight As Integer = 78
            g.DrawRectangle(Pens.Black, MarginLeft, y, TotalWidth, footerBoxHeight)

            Dim currentY As Integer = y + 2

            Dim line1 As String = "نوٹ :- کاؤنٹر چھوڑنے سے پہلے سونا اور رقم"
            g.DrawString(line1, fontUrdu, Brushes.Black, New RectangleF(MarginLeft + 2, currentY, TotalWidth - 4, 18), sfCenter)
            currentY += 18

            Dim line2 As String = "چیک کر لیں بعد میں ہماری کوئی ذمہ داری نہیں ہوگی۔"
            g.DrawString(line2, fontUrdu, Brushes.Black, New RectangleF(MarginLeft + 2, currentY, TotalWidth - 4, 18), sfCenter)
            currentY += 18

            Dim line3 As String = "کسی بھی قسم کا سافٹ ویئر بنوانے کے لیے رابطہ کریں"
            g.DrawString(line3, fontUrduBranding, Brushes.Black, New RectangleF(MarginLeft + 2, currentY, TotalWidth - 4, 18), sfCenter)
            currentY += 18

            Dim line4 As String = "SultanSoft 03172503282"
            g.DrawString(line4, fontEngBranding, Brushes.Black, New RectangleF(MarginLeft + 2, currentY, TotalWidth - 4, 16), sfCenter)

            y += footerBoxHeight + 6
        End Using
    End Sub

    Private Shared Sub DrawCell(
        g As Graphics,
        x As Integer,
        y As Integer,
        w As Integer,
        h As Integer,
        backColor As Color)

        Using br As New SolidBrush(backColor)
            g.FillRectangle(br, x, y, w, h)
        End Using

        g.DrawRectangle(Pens.Black, x, y, w, h)

    End Sub

End Class