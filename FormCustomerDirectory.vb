Imports System.Data.SQLite
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class FormCustomerDirectory
    Inherits Form

    Private connStr As String = "Data Source=perchi.db;Version=3;"

    ' UI Controls - (All Declared Here)
    Private headerPanel As New Panel()
    Private footerPanel As New Panel()
    Private lblTitle As New Label()
    Private lblFooter As New Label()

    ' Input Controls
    Private lblName As New Label()
    Private txtName As New TextBox()
    Private lblPhone As New Label()
    Private txtPhone As New TextBox()
    Private lblAddress As New Label()
    Private txtAddress As New TextBox()

    ' Search Controls
    Private lblSearch As New Label()
    Private txtSearch As New TextBox()

    ' Action Buttons
    Private btnAdd As New Button()
    Private btnEdit As New Button()
    Private btnDelete As New Button()
    Private btnImportCSV As New Button()
    Private btnClose As New Button()

    ' Main DataGridView Variable
    Private dgvCustomers As New DataGridView()
    Private selectedCustomerId As String = ""

    Public Sub New()
        InitializeComponent()
        Me.Text = "Customer Directory"
        Me.Size = New Size(950, 680)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = Color.FromArgb(235, 240, 250)

        SetupUI()
        EnsureCustomersTable()
        LoadCustomers()
    End Sub

    Private Sub EnsureCustomersTable()
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim sql As String = "CREATE TABLE IF NOT EXISTS Customers (" &
                                   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " &
                                   "Name TEXT NOT NULL, " &
                                   "Mobile TEXT, " &
                                   "Address TEXT);"
                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Database setup error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetupUI()
        ' 1. Header & Footer
        headerPanel.Dock = DockStyle.Top
        headerPanel.Height = 50
        headerPanel.BackColor = Color.FromArgb(0, 0, 128)

        lblTitle.Text = "Customer Directory & Contacts"
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
        lblFooter.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblFooter.Dock = DockStyle.Fill
        lblFooter.TextAlign = ContentAlignment.MiddleCenter
        footerPanel.Controls.Add(lblFooter)

        Me.Controls.Add(headerPanel)
        Me.Controls.Add(footerPanel)

        ' 2. Input Fields Area
        lblName.Text = "Name:"
        lblName.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblName.Location = New Point(20, 65)
        lblName.AutoSize = True
        Me.Controls.Add(lblName)

        txtName.Location = New Point(90, 62)
        txtName.Size = New Size(180, 25)
        txtName.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        Me.Controls.Add(txtName)

        lblPhone.Text = "Mobile:"
        lblPhone.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblPhone.Location = New Point(285, 65)
        lblPhone.AutoSize = True
        Me.Controls.Add(lblPhone)

        txtPhone.Location = New Point(345, 62)
        txtPhone.Size = New Size(160, 25)
        txtPhone.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        Me.Controls.Add(txtPhone)

        lblAddress.Text = "Address:"
        lblAddress.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblAddress.Location = New Point(520, 65)
        lblAddress.AutoSize = True
        Me.Controls.Add(lblAddress)

        txtAddress.Location = New Point(585, 62)
        txtAddress.Size = New Size(210, 25)
        txtAddress.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        Me.Controls.Add(txtAddress)

        ' 3. Search Box
        lblSearch.Text = "Search Customer:"
        lblSearch.Font = New Font("Segoe UI", 9.5F, FontStyle.Bold)
        lblSearch.ForeColor = Color.FromArgb(0, 0, 128)
        lblSearch.Location = New Point(20, 105)
        lblSearch.AutoSize = True
        Me.Controls.Add(lblSearch)

        txtSearch.Location = New Point(145, 102)
        txtSearch.Size = New Size(220, 25)
        txtSearch.Font = New Font("Segoe UI", 9.5F, FontStyle.Regular)
        AddHandler txtSearch.TextChanged, Sub(s, e) LoadCustomers(txtSearch.Text.Trim())
        Me.Controls.Add(txtSearch)

        ' 4. Buttons Row
        Dim btnY As Integer = 100
        Dim btnWidth As New Size(115, 30)
        Dim maroonColor As Color = Color.FromArgb(128, 0, 0)

        ' Add / Save Button
        btnAdd.Text = "Add / Save"
        btnAdd.Location = New Point(380, btnY)
        btnAdd.Size = btnWidth
        btnAdd.BackColor = maroonColor
        btnAdd.ForeColor = Color.White
        btnAdd.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnAdd.FlatStyle = FlatStyle.Flat
        AddHandler btnAdd.Click, AddressOf HandleAddClick
        Me.Controls.Add(btnAdd)

        ' Edit Button
        btnEdit.Text = "Edit Entry"
        btnEdit.Location = New Point(502, btnY)
        btnEdit.Size = btnWidth
        btnEdit.BackColor = maroonColor
        btnEdit.ForeColor = Color.White
        btnEdit.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnEdit.FlatStyle = FlatStyle.Flat
        AddHandler btnEdit.Click, AddressOf HandleEditClick
        Me.Controls.Add(btnEdit)

        ' Delete Button
        btnDelete.Text = "Delete Entry"
        btnDelete.Location = New Point(624, btnY)
        btnDelete.Size = btnWidth
        btnDelete.BackColor = maroonColor
        btnDelete.ForeColor = Color.White
        btnDelete.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnDelete.FlatStyle = FlatStyle.Flat
        AddHandler btnDelete.Click, AddressOf HandleDeleteClick
        Me.Controls.Add(btnDelete)

        ' Import CSV Button
        btnImportCSV.Text = "Import CSV"
        btnImportCSV.Location = New Point(746, btnY)
        btnImportCSV.Size = New Size(100, 30)
        btnImportCSV.BackColor = Color.DarkGreen
        btnImportCSV.ForeColor = Color.White
        btnImportCSV.Font = New Font("Segoe UI", 8.5F, FontStyle.Bold)
        btnImportCSV.FlatStyle = FlatStyle.Flat
        AddHandler btnImportCSV.Click, AddressOf HandleImportCSVClick
        Me.Controls.Add(btnImportCSV)

        ' Close Button
        btnClose.Text = "Close"
        btnClose.Location = New Point(853, btnY)
        btnClose.Size = New Size(65, 30)
        btnClose.BackColor = maroonColor
        btnClose.ForeColor = Color.White
        btnClose.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnClose.FlatStyle = FlatStyle.Flat
        AddHandler btnClose.Click, Sub(s, e) Me.Close()
        Me.Controls.Add(btnClose)

        ' 5. DataGridView Setup
        dgvCustomers.Location = New Point(20, 142)
        dgvCustomers.Size = New Size(898, 470)
        dgvCustomers.AllowUserToAddRows = False
        dgvCustomers.ReadOnly = True
        dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCustomers.MultiSelect = True ' Multi-select enabled
        dgvCustomers.BackgroundColor = Color.White
        dgvCustomers.RowHeadersVisible = False
        dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102)
        dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgvCustomers.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvCustomers.EnableHeadersVisualStyles = False
        AddHandler dgvCustomers.CellClick, AddressOf HandleGridCellClick
        Me.Controls.Add(dgvCustomers)
    End Sub

    ' --- Load Customers Data ---
    Public Sub LoadCustomers(Optional search As String = "")
        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                Dim qry As String = "SELECT ID, Name, Mobile, Address FROM Customers "
                If Not String.IsNullOrEmpty(search) Then
                    qry &= "WHERE Name LIKE '%" & search & "%' OR Mobile LIKE '%" & search & "%' OR Address LIKE '%" & search & "%' "
                End If
                qry &= "ORDER BY Name ASC"

                Dim da As New SQLiteDataAdapter(qry, conn)
                Dim dt As New DataTable()
                da.Fill(dt)
                dgvCustomers.DataSource = dt
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading customers: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- Grid Click Event ---
    Private Sub HandleGridCellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvCustomers.Rows(e.RowIndex)
            selectedCustomerId = row.Cells("ID").Value.ToString()
            txtName.Text = row.Cells("Name").Value.ToString()
            txtPhone.Text = row.Cells("Mobile").Value.ToString()
            txtAddress.Text = row.Cells("Address").Value.ToString()
            btnAdd.Text = "Update"
        End If
    End Sub

    ' --- Add / Update Record ---
    Private Sub HandleAddClick(sender As Object, e As EventArgs)
        If String.IsNullOrWhiteSpace(txtName.Text) Then
            MessageBox.Show("Barah-e-karam Customer Name darj karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Using conn As New SQLiteConnection(connStr)
                conn.Open()
                If btnAdd.Text = "Update" AndAlso Not String.IsNullOrEmpty(selectedCustomerId) Then
                    Dim sql As String = "UPDATE Customers SET Name = @Name, Mobile = @Mobile, Address = @Address WHERE ID = @ID"
                    Using cmd As New SQLiteCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim())
                        cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text.Trim())
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim())
                        cmd.Parameters.AddWithValue("@ID", selectedCustomerId)
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Customer details update ho gayi hain!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    Dim sql As String = "INSERT INTO Customers (Name, Mobile, Address) VALUES (@Name, @Mobile, @Address)"
                    Using cmd As New SQLiteCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim())
                        cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text.Trim())
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim())
                        cmd.ExecuteNonQuery()
                    End Using
                    MessageBox.Show("Naya Customer kamyabi se add ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using

            ClearForm()
            LoadCustomers()
        Catch ex As Exception
            MessageBox.Show("Save error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- Edit Selected Entry ---
    Private Sub HandleEditClick(sender As Object, e As EventArgs)
        If dgvCustomers.SelectedRows.Count = 0 Then
            MessageBox.Show("Barah-e-karam edit ke liye kisi customer ko select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim row As DataGridViewRow = dgvCustomers.SelectedRows(0)
        selectedCustomerId = row.Cells("ID").Value.ToString()
        txtName.Text = row.Cells("Name").Value.ToString()
        txtPhone.Text = row.Cells("Mobile").Value.ToString()
        txtAddress.Text = row.Cells("Address").Value.ToString()
        btnAdd.Text = "Update"
        txtName.Focus()
    End Sub

    ' --- Delete Multi-Selected Entries ---
    Private Sub HandleDeleteClick(sender As Object, e As EventArgs)
        If dgvCustomers.SelectedRows.Count = 0 Then
            MessageBox.Show("Barah-e-karam delete ke liye kam az kam ek customer select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If MessageBox.Show("Kya aap waqai select kiye gaye customer(s) ko delete karna chahte hain?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Using conn As New SQLiteConnection(connStr)
                    conn.Open()
                    For Each row As DataGridViewRow In dgvCustomers.SelectedRows
                        Dim id As String = row.Cells("ID").Value.ToString()
                        Dim cmd As New SQLiteCommand("DELETE FROM Customers WHERE ID = @ID", conn)
                        cmd.Parameters.AddWithValue("@ID", id)
                        cmd.ExecuteNonQuery()
                    Next
                End Using

                MessageBox.Show("Selected customer records delete ho gaye!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ClearForm()
                LoadCustomers()
            Catch ex As Exception
                MessageBox.Show("Delete error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- CSV Import Logic ---
    Private Sub HandleImportCSVClick(sender As Object, e As EventArgs)
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*"
        ofd.Title = "Select Customer CSV File"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim importedCount As Integer = 0
                Dim lines As String() = File.ReadAllLines(ofd.FileName)

                Using conn As New SQLiteConnection(connStr)
                    conn.Open()

                    For i As Integer = 0 To lines.Length - 1
                        Dim line As String = lines(i).Trim()
                        If String.IsNullOrWhiteSpace(line) Then Continue For

                        Dim parts As String() = line.Split(","c)
                        If i = 0 AndAlso (parts(0).ToLower().Contains("name") OrElse parts(0).ToLower().Contains("customer")) Then
                            Continue For
                        End If

                        Dim cName As String = parts(0).Replace("""", "").Trim()
                        Dim cMobile As String = If(parts.Length > 1, parts(1).Replace("""", "").Trim(), "")
                        Dim cAddress As String = If(parts.Length > 2, parts(2).Replace("""", "").Trim(), "")

                        If Not String.IsNullOrEmpty(cName) Then
                            Dim sql As String = "INSERT INTO Customers (Name, Mobile, Address) VALUES (@Name, @Mobile, @Address)"
                            Using cmd As New SQLiteCommand(sql, conn)
                                cmd.Parameters.AddWithValue("@Name", cName)
                                cmd.Parameters.AddWithValue("@Mobile", cMobile)
                                cmd.Parameters.AddWithValue("@Address", cAddress)
                                cmd.ExecuteNonQuery()
                            End Using
                            importedCount += 1
                        End If
                    Next
                End Using

                MessageBox.Show(importedCount.ToString() & " Customers CSV file se kamyabi se import ho gaye hain!", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadCustomers()

            Catch ex As Exception
                MessageBox.Show("CSV Import error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ClearForm()
        txtName.Clear()
        txtPhone.Clear()
        txtAddress.Clear()
        selectedCustomerId = ""
        btnAdd.Text = "Add / Save"
    End Sub
End Class