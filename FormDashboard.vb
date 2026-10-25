Imports System.Drawing
Imports System.Windows.Forms

Public Class FormDashboard

    Inherits Form

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    ' WithEvents keyword add kiya gaya hai taake Handles error khatam ho jaye
    Private WithEvents btnAccounts As New Button()
    Private WithEvents btnPerchi As New Button()
    Private WithEvents btnBackup As New Button()
    Private WithEvents btnRestore As New Button()

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        InitializeDatabase()
        Me.Text = "FormDashboard"
        Me.Size = New Size(520, 380)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
    End Sub

    Private Sub SetupUI()
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 60
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Sultan Soft Perchi System"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 16, FontStyle.Bold)
        lblTitle.Dock = DockStyle.Fill
        lblTitle.TextAlign = ContentAlignment.MiddleCenter
        headerPanel.Controls.Add(lblTitle)

        footerPanel.Dock = DockStyle.Bottom
        footerPanel.Height = 40
        footerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblFooter.Text = "Created By SultanSoft 03172503282"
        lblFooter.ForeColor = Color.White
        lblFooter.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblFooter.Dock = DockStyle.Fill
        lblFooter.TextAlign = ContentAlignment.MiddleCenter
        footerPanel.Controls.Add(lblFooter)

        Dim buttons() As Button = {btnAccounts, btnPerchi, btnBackup, btnRestore}
        Dim buttonTexts() As String = {"Accounts", "Perchi", "Backup", "Restore"}

        Dim startY As Integer = 90
        For i As Integer = 0 To buttons.Length - 1
            Dim btn As Button = buttons(i)
            btn.Text = buttonTexts(i)
            btn.Size = New Size(120, 35)
            btn.Location = New Point(40, startY + (i * 50))
            btn.BackColor = Color.FromArgb(128, 0, 0)
            btn.ForeColor = Color.White
            btn.Font = New Font("Segoe UI", 11, FontStyle.Bold)
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.Cursor = Cursors.Hand
            Me.Controls.Add(btn)
        Next

        Me.Controls.Add(headerPanel)
        Me.Controls.Add(footerPanel)
    End Sub

    ' Event Handlers without Handles clause conflicts
    Private Sub btnAccounts_Click(sender As Object, e As EventArgs) Handles btnAccounts.Click
        Dim f As New FormAccounts()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnPerchi_Click(sender As Object, e As EventArgs) Handles btnPerchi.Click
        Dim f As New FormPerchi()
        f.Show()
        Me.Hide()
    End Sub

    Private Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Try
            Dim backupDir As String = "K:\My Drive\Sultan Accounts\"
            If Not System.IO.Directory.Exists(backupDir) Then
                System.IO.Directory.CreateDirectory(backupDir)
            End If
            Dim destFile As String = backupDir & "perchi.db" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".db"
            System.IO.File.Copy(dbPath, destFile, True)
            MessageBox.Show("Backup kamyabi se Google Drive me save ho gaya!" & vbCrLf & "Location: " & destFile, "Backup Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Backup error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "Database Files (*.db)|*.db"
        If ofd.ShowDialog() = DialogResult.OK Then
            System.IO.File.Copy(ofd.FileName, dbPath, True)
            MessageBox.Show("Database restored successfully!", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class