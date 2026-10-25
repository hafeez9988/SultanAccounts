Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms

Public Class FormBackupRestore
    Inherits Form

    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    Private btnBackup As New Button()
    Private btnRestore As New Button()
    Private btnClose As New Button()

    Private lblInfo As New Label()

    Public Sub New()
        Me.Text = "Google Drive Backup & Restore"
        Me.Size = New Size(500, 350)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
    End Sub

    Private Sub SetupUI()
        ' Header
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Database Backup & Restore"
        lblTitle.ForeColor = Color.White
        lblTitle.Font = New Font("Segoe UI", 15, FontStyle.Bold)
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

        ' Info Label
        lblInfo.Text = "Apnay database (perchi.db) ka backup Google Drive folder mein mehfooz karein" & vbCrLf & "ya zaroorat padne par wapas restore karein."
        lblInfo.Font = New Font("Segoe UI", 10, FontStyle.Regular)
        lblInfo.ForeColor = Color.DarkBlue
        lblInfo.Location = New Point(40, 70)
        lblInfo.Size = New Size(420, 50)
        lblInfo.TextAlign = ContentAlignment.MiddleCenter

        ' Buttons
        btnBackup = Create3DButton("Backup to Google Drive", New Point(100, 140), New Size(300, 40))
        btnRestore = Create3DButton("Restore Database", New Point(100, 195), New Size(300, 40))
        btnClose = Create3DButton("Close", New Point(100, 250), New Size(300, 40))

        AddHandler btnBackup.Click, AddressOf BtnBackup_Click
        AddHandler btnRestore.Click, AddressOf BtnRestore_Click
        AddHandler btnClose.Click, Sub() Me.Close()

        ' Add Controls
        Me.Controls.Add(headerPanel)
        Me.Controls.Add(footerPanel)
        Me.Controls.Add(lblInfo)
        Me.Controls.Add(btnBackup)
        Me.Controls.Add(btnRestore)
        Me.Controls.Add(btnClose)
    End Sub

    Private Function Create3DButton(text As String, loc As Point, sz As Size) As Button
        Dim btn As New Button()
        btn.Text = text
        btn.Location = loc
        btn.Size = sz
        btn.BackColor = Color.Maroon
        btn.ForeColor = Color.White
        btn.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        btn.FlatStyle = FlatStyle.Standard
        Return btn
    End Function

    Private Sub BtnBackup_Click(sender As Object, e As EventArgs)
        Try
            Dim dbFile As String = "perchi.db"
            If Not File.Exists(dbFile) Then
                MessageBox.Show("Database file (perchi.db) nahi mili!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Using sfd As New SaveFileDialog()
                sfd.Title = "Select Backup Location (Google Drive Folder select karein)"
                sfd.Filter = "Database File (*.db)|*.db"
                sfd.FileName = "perchi_backup_" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".db"

                ' Agar Google Drive desktop app installed hai toh user wahan ka path select kar sakta hai
                If sfd.ShowDialog() = DialogResult.OK Then
                    File.Copy(dbFile, sfd.FileName, True)
                    MessageBox.Show("Database ka backup kamyabi ke sath save ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Backup error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnRestore_Click(sender As Object, e As EventArgs)
        Try
            Using ofd As New OpenFileDialog()
                ofd.Title = "Select Backup File to Restore"
                ofd.Filter = "Database File (*.db)|*.db"

                If ofd.ShowDialog() = DialogResult.OK Then
                    Dim confirmResult = MessageBox.Show("Kya aap waqai database restore karna chahte hain? Purana data overwrite ho sakta hai!", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    If confirmResult = DialogResult.Yes Then
                        Dim dbFile As String = "perchi.db"
                        File.Copy(ofd.FileName, dbFile, True)
                        MessageBox.Show("Database kamyabi ke sath restore ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Restore error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class