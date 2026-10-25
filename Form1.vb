Imports System.Data
Imports System.Data.SQLite
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.IO.Ports
Imports System.Windows.Forms

Public Class Form1

    ' ==========================================
    ' CLASS LEVEL VARIABLES
    ' ==========================================
    Private WithEvents serialPort As New SerialPort()
    Private CurrentPrintData As PrintData
    Private currentEditingPerchiNo As String = ""
    Private isUpdating As Boolean = False

    ' ==========================================
    ' FORM LOAD & INITIALIZATION
    ' ==========================================

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DatabaseManager.EnsureGinniTableSchema()
        Try
            ' 1. Database & Security Initialization
            DatabaseManager.Initialize()
        Catch ex As Exception
            MessageBox.Show("Startup Error: " & ex.Message, "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ' 2. License Security Check
        If Not LicenseManager.CheckAndValidateLicense() Then
            Application.Exit()
            End
            Return
        End If

        ' 3. Controls Settings & Data Loading
        LoadCustomerComboBox()

        If cmbCustomer IsNot Nothing Then
            cmbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbCustomer.AutoCompleteSource = AutoCompleteSource.ListItems
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDown
        End If

        If txtDate IsNot Nothing Then
            txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy")
            txtDate.ReadOnly = True
        End If

        If txtPerchiNo IsNot Nothing Then
            txtPerchiNo.Text = DatabaseManager.GetNextPerchiNo()
        End If

        ' Scale COM Ports aur Baud Rates
        If cboPortName IsNot Nothing Then
            cboPortName.Items.Clear()
            cboPortName.Items.AddRange(IO.Ports.SerialPort.GetPortNames())
            If cboPortName.Items.Count > 0 Then cboPortName.SelectedIndex = 0
        End If

        If cboBaudRate IsNot Nothing Then
            cboBaudRate.Items.Clear()
            cboBaudRate.Items.AddRange(New Object() {"2400", "9600", "4800", "1200"})
            cboBaudRate.SelectedItem = "2400"
        End If

        ' Timer1 Start
        If Timer1 IsNot Nothing Then Timer1.Start()

        ' Closing Balances Load
        LoadDailyReportTotals()
        RefreshClosingBalances()


    End Sub
    Public Sub LoadCustomerComboBox()
        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                If con.State <> ConnectionState.Open Then con.Open()

                Dim dt As New DataTable()
                Dim query As String = "SELECT DISTINCT CustomerName FROM Customers WHERE CustomerName IS NOT NULL AND CustomerName <> '' ORDER BY CustomerName ASC"

                Using cmd As New SQLiteCommand(query, con)
                    Using da As New SQLiteDataAdapter(cmd)
                        da.Fill(dt)
                    End Using
                End Using

                ' Form1 par ComboBox ka naam cmbCustomer hona chahiye
                cmbCustomer.DataSource = Nothing
                cmbCustomer.Items.Clear()

                If dt.Rows.Count > 0 Then
                    cmbCustomer.DataSource = dt
                    cmbCustomer.DisplayMember = "CustomerName"
                    cmbCustomer.ValueMember = "CustomerName"
                    cmbCustomer.SelectedIndex = -1
                End If
            End Using
        Catch ex As Exception
            ' Ignore or log error
        End Try
    End Sub


    Public Class DriveBackupManager

        ' System me Google Drive Folder Path detect karna
        Private Shared Function GetGoogleDriveFolderPath() As String
            ' Aapka Primary Path (K: Drive)
            Dim primaryKPath As String = "K:\My Drive\Sultan Accounts"
            Dim driveKRoot As String = "K:\My Drive"
            Dim driveGPath As String = "G:\My Drive"

            ' User Profile level paths (Fallback ke liye)
            Dim userPath As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
            Dim myDrivePath As String = Path.Combine(userPath, "My Drive")

            If Directory.Exists(primaryKPath) Then
                Return primaryKPath
            ElseIf Directory.Exists(driveKRoot) Then
                Return driveKRoot
            ElseIf Directory.Exists(driveGPath) Then
                Return driveGPath
            ElseIf Directory.Exists(myDrivePath) Then
                Return myDrivePath
            Else
                ' Drive Folder nahi mila to Empty Return karega (Manual Dialog browse hoga)
                Return ""
            End If
        End Function

        '=========================================
        ' 1. BACKUP TO GOOGLE DRIVE
        '=========================================
        Public Shared Sub BackupToGoogleDrive(localDbFilePath As String)
            Try
                If Not File.Exists(localDbFilePath) Then
                    MessageBox.Show("Local Database file nahi mili!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If

                Dim driveFolder As String = GetGoogleDriveFolderPath()

                ' Agar auto-detect na ho to manual browse folder
                If String.IsNullOrEmpty(driveFolder) Then
                    Dim fbd As New FolderBrowserDialog()
                    fbd.Description = "K:\ (Sultan Accounts) select karen:"
                    If fbd.ShowDialog() = DialogResult.OK Then
                        driveFolder = fbd.SelectedPath
                    Else
                        Return
                    End If
                End If

                ' Destination Directory Handle karen
                Dim backupDir As String = driveFolder

                ' Agar path direct 'Sultan Accounts' tak nahi hai to 'Sultan Accounts' folder banayein
                If Not driveFolder.EndsWith("Sultan Accounts", StringComparison.OrdinalIgnoreCase) Then
                    backupDir = Path.Combine(driveFolder, "Sultan Accounts")
                End If

                If Not Directory.Exists(backupDir) Then
                    Directory.CreateDirectory(backupDir)
                End If

                ' File Name with Timestamp (SultanAccountsDB_Backup_YYYYMMDD_HHMMSS.db)
                Dim fileName As String = "perchi.db" & DateTime.Now.ToString("yyyyMMdd_HHmmss") & ".db"
                Dim destPath As String = Path.Combine(backupDir, fileName)

                ' Copy file to Google Drive
                File.Copy(localDbFilePath, destPath, True)

                MessageBox.Show("Backup kamyabi se Google Drive me save ho gaya!" & vbCrLf & vbCrLf & "Location: " & destPath, "Backup Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Backup error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        '=========================================
        ' 2. RESTORE FROM GOOGLE DRIVE
        '=========================================
        Public Shared Sub RestoreFromGoogleDrive(targetDbFilePath As String)
            Try
                Dim driveFolder As String = GetGoogleDriveFolderPath()
                Dim initialPath As String = driveFolder

                ' Open File Dialog to choose backup file
                Dim ofd As New OpenFileDialog()
                ofd.Title = "Google Drive (Sultan Accounts) se Backup File Select Karen"
                ofd.Filter = "Database Files (perchi.db)"

                If Directory.Exists(initialPath) Then
                    ofd.InitialDirectory = initialPath
                End If

                If ofd.ShowDialog() = DialogResult.OK Then
                    Dim selectedBackupFile As String = ofd.FileName

                    Dim confirm As DialogResult = MessageBox.Show("Kya aap waqai database restore karna chahte hain? Purana data replace ho jaye ga!", "Restore Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

                    If confirm = DialogResult.Yes Then
                        ' SQLite Active Connection Close karne ke liye Garbage Collector Run karen
                        GC.Collect()
                        GC.WaitForPendingFinalizers()

                        ' Replace local db file
                        File.Copy(selectedBackupFile, targetDbFilePath, True)

                        MessageBox.Show("Database successfully restore ho gaya! App restart hone lagi he.", "Restore Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Application.Restart()
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Restore error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

    End Class
    Public Sub EnsureGinniTableExists()
        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                If con.State <> ConnectionState.Open Then con.Open()

                ' SQLite Query to auto-create Ginni table if it doesn't exist
                Dim createTableQuery As String = "
                CREATE TABLE IF NOT EXISTS Ginni (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Ginni REAL DEFAULT 0,
                    Pure REAL DEFAULT 0,
                    RattiIn REAL DEFAULT 0,
                    CreatedDate TEXT
                );"

                Using cmd As New SQLiteCommand(createTableQuery, con)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Ginni table create karne men error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    ' ==========================================
    ' DAILY REPORT TOTALS SHOW FUNCTION
    ' ==========================================
    Public Sub LoadDailyReportTotals()
        Try
            ' Direct Live Balances Refresh Karein
            RefreshClosingBalances()
        Catch ex As Exception
            ' Silent catch
        End Try
    End Sub


    ' ==========================================
    ' SCALE INTEGRATION FUNCTIONS
    ' ==========================================
    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        Try
            If serialPort.IsOpen Then
                If Timer2 IsNot Nothing Then Timer2.Stop()
                serialPort.Close()
                btnConnect.Text = "Connect Scale"
                btnConnect.BackColor = Color.LightGray
                MessageBox.Show("Scale Disconnected!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                If cboPortName.SelectedItem Is Nothing Then
                    MessageBox.Show("Pehle COM Port select karein!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

                serialPort.PortName = cboPortName.SelectedItem.ToString()
                serialPort.BaudRate = Integer.Parse(cboBaudRate.SelectedItem.ToString())
                serialPort.Parity = Parity.Even
                serialPort.DataBits = 7
                serialPort.StopBits = StopBits.One
                serialPort.NewLine = vbCrLf

                serialPort.Open()
                btnConnect.Text = "Disconnect Scale"
                btnConnect.BackColor = Color.LightGreen

                If chkAutoRead IsNot Nothing AndAlso chkAutoRead.Checked Then
                    If Timer2 IsNot Nothing Then Timer2.Start()
                End If

                MessageBox.Show("Scale Connected Successfully!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Connection Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub RequestWeightFromScale()
        Try
            If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
                serialPort.Write("Q" & vbCrLf)
            End If
        Catch ex As Exception
            ' Silent catch for scale polling
        End Try
    End Sub

    Private Sub btnGetWeight_Click(sender As Object, e As EventArgs) Handles btnGetWeight.Click
        RequestWeightFromScale()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        RequestWeightFromScale()
    End Sub

    Private Sub chkAutoRead_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoRead.CheckedChanged
        If chkAutoRead.Checked AndAlso serialPort.IsOpen Then
            If Timer2 IsNot Nothing Then Timer2.Start()
        Else
            If Timer2 IsNot Nothing Then Timer2.Stop()
        End If
    End Sub

    Private Sub serialPort_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles serialPort.DataReceived
        Try
            Dim rawData As String = serialPort.ReadLine()

            Me.Invoke(Sub()
                          If String.IsNullOrWhiteSpace(rawData) Then Exit Sub

                          Dim match As Match = Regex.Match(rawData, "[-+]?\d*\.?\d+")
                          If match.Success Then
                              Dim cleanWeight As String = match.Value.Trim()
                              If Decimal.TryParse(cleanWeight, Nothing) Then
                                  If txtWeight IsNot Nothing Then txtWeight.Text = cleanWeight
                              End If
                          End If
                      End Sub)
        Catch ex As Exception
            ' Safe invoke catch
        End Try
    End Sub

    ' ==========================================
    ' CUSTOMER LOAD & SEARCH SYSTEM
    ' ==========================================
    Public Sub LoadCustomersToComboBox()
        Try
            cmbCustomer.Items.Clear()
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim query As String = "SELECT CustomerName FROM Customers ORDER BY CustomerName ASC"
                Using cmd As New SQLiteCommand(query, con)
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        While dr.Read()
                            cmbCustomer.Items.Add(dr("CustomerName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Customer load error handling
        End Try
    End Sub

    Private Sub cmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCustomer.SelectedIndexChanged
        If String.IsNullOrWhiteSpace(cmbCustomer.Text) Then Exit Sub

        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim sql As String = "SELECT Mobile, Address FROM Customers WHERE CustomerName=@Name LIMIT 1"
                Using cmd As New SQLiteCommand(sql, con)
                    cmd.Parameters.AddWithValue("@Name", cmbCustomer.Text.Trim())
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            txtMobile.Text = dr("Mobile").ToString()
                            txtAddress.Text = dr("Address").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Silent catch
        End Try
    End Sub

    Private Sub txtCustomerSearch_TextChanged(sender As Object, e As EventArgs) Handles txtCustomerSearch.TextChanged
        Dim searchKeyword As String = txtCustomerSearch.Text.Trim()
        If searchKeyword.Length > 0 Then
            SearchAndFillCustomer(searchKeyword)
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim searchKeyword As String = txtCustomerSearch.Text.Trim()
        If searchKeyword <> "" Then
            SearchAndFillCustomer(searchKeyword)
        Else
            MessageBox.Show("Baraye meherbani search karne ke liye naam ya mobile number likhein.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub SearchAndFillCustomer(keyword As String)
        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim query As String = "SELECT CustomerName, Mobile, Address FROM Customers WHERE CustomerName LIKE @Search OR Mobile LIKE @Search LIMIT 1"
                Using cmd As New SQLiteCommand(query, con)
                    cmd.Parameters.AddWithValue("@Search", "%" & keyword & "%")
                    Using dr As SQLiteDataReader = cmd.ExecuteReader()
                        If dr.Read() Then
                            cmbCustomer.Text = dr("CustomerName").ToString()
                            txtMobile.Text = dr("Mobile").ToString()
                            txtAddress.Text = dr("Address").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Silent catch
        End Try
    End Sub

    Private Sub btnAddCustomer_Click(sender As Object, e As EventArgs) Handles btnAddCustomer.Click
        Dim nameInput As String = txtNewCustomerName.Text.Trim()
        Dim mobileInput As String = txtNewMobile.Text.Trim()
        Dim addressInput As String = txtNewAddress.Text.Trim()

        If String.IsNullOrEmpty(nameInput) Then
            MessageBox.Show("Baraye meherbani Customer Name darj karein!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim formattedName As String = nameInput
        If mobileInput.Length >= 3 Then
            Dim last3Digits As String = mobileInput.Substring(mobileInput.Length - 3)
            formattedName = nameInput & " " & last3Digits
        End If

        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim sql As String = "INSERT INTO Customers (CustomerName, Mobile, Address, RegistrationDate) VALUES (@Name, @Mobile, @Address, @Date)"
                Using cmd As New SQLiteCommand(sql, con)
                    cmd.Parameters.AddWithValue("@Name", formattedName)
                    cmd.Parameters.AddWithValue("@Mobile", mobileInput)
                    cmd.Parameters.AddWithValue("@Address", addressInput)
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd-MM-yyyy"))
                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Customer (" & formattedName & ") kamyabi se add ho gaya hai!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            txtNewCustomerName.Clear()
            txtNewAddress.Clear()
            txtNewMobile.Text = "03"
            txtNewMobile.SelectionStart = txtNewMobile.Text.Length

            LoadCustomersToComboBox()

        Catch ex As Exception
            MessageBox.Show("Customer add karne mein error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ==========================================
    ' AMOUNT CALCULATION (Weight, Rate & BUY/SALE Auto-Fill)
    ' ==========================================
    Private Sub CalculateAmount()
        Try
            Dim weight As Double = 0
            Dim currentRate As Double = 0
            Dim cut As Double = 0

            If txtWeight IsNot Nothing Then Double.TryParse(txtWeight.Text, weight)
            If txtCurrentRate IsNot Nothing Then Double.TryParse(txtCurrentRate.Text, currentRate)
            If txtCut IsNot Nothing Then Double.TryParse(txtCut.Text, cut)

            ' 1. Cut ko Current Rate me se minus karke Net Rate nikalein
            Dim netRate As Double = currentRate - cut
            If netRate < 0 Then netRate = 0

            ' Agar Form1 par Final Rate show karne ke liye txtRate textbox hai:
            If txtRate IsNot Nothing Then
                txtRate.Text = netRate.ToString("0")
            End If

            ' 2. Amount Calculation: (Weight * Net Rate) / 11.664
            Dim totalAmount As Double = (weight * netRate) / 11.664

            If txtAmount IsNot Nothing Then
                txtAmount.Text = Math.Round(totalAmount).ToString("0")
            End If



            ' BUY / SALE ki bunyad par CashIn aur CashOut auto-fill
            Dim selectedType As String = If(cmbType IsNot Nothing AndAlso cmbType.SelectedItem IsNot Nothing, cmbType.SelectedItem.ToString().ToUpper(), "")

            If selectedType = "BUY" Then
                ' BUY: Dukaan Cash degi -> CashOut = Amount, CashIn = 0
                If txtCashOut IsNot Nothing Then txtCashOut.Text = txtAmount.Text
                If txtCashIn IsNot Nothing Then txtCashIn.Text = "0"
            ElseIf selectedType = "SALE" Then
                ' SALE: Dukaan Cash vasool karegi -> CashIn = Amount, CashOut = 0
                If txtCashIn IsNot Nothing Then txtCashIn.Text = txtAmount.Text
                If txtCashOut IsNot Nothing Then txtCashOut.Text = "0"
            End If

            ' Balance Refresh
            CalculateBalance()

        Catch ex As Exception
            ' Calculation safety
        End Try
    End Sub

    Private Sub CalculateBalance()
        Dim amount As Double = 0
        Dim cashIn As Double = 0
        Dim cashOut As Double = 0

        If txtAmount IsNot Nothing Then Double.TryParse(txtAmount.Text, amount)
        If txtCashIn IsNot Nothing Then Double.TryParse(txtCashIn.Text, cashIn)
        If txtCashOut IsNot Nothing Then Double.TryParse(txtCashOut.Text, cashOut)

        If txtBalance IsNot Nothing Then
            If amount = 0 AndAlso cashIn = 0 AndAlso cashOut = 0 Then
                txtBalance.Clear()
            Else
                Dim finalBalance As Double = 0
                Dim transType As String = ""

                If cmbType IsNot Nothing Then
                    transType = cmbType.Text.Trim().ToUpper()
                End If

                If transType = "BUY" Then
                    ' Buy mein: Amount me se CashOut minus hoga
                    finalBalance = amount - cashOut
                ElseIf transType = "SALE" Then
                    ' Sale mein: Amount me se CashIn minus hoga
                    finalBalance = amount - cashIn
                Else
                    If cashIn > 0 Then finalBalance = amount - cashIn
                    If cashOut > 0 Then finalBalance = amount + cashOut
                End If

                txtBalance.Text = Math.Round(finalBalance).ToString("0")
            End If
        End If
    End Sub



    Private Sub txtCashOut_TextChanged(sender As Object, e As EventArgs) Handles txtCashOut.TextChanged
        CalculateBalance()
    End Sub
    Private Sub txtCashIn_TextChanged(sender As Object, e As EventArgs) Handles txtCashIn.TextChanged
        CalculateBalance()
    End Sub
    Private Sub txtUdhar_TextChanged(sender As Object, e As EventArgs) Handles txtUdhar.TextChanged
        CalculateBalance()
    End Sub

    Private Sub txtAdvance_TextChanged(sender As Object, e As EventArgs) Handles txtAdvance.TextChanged
        CalculateBalance()
    End Sub

    Private Sub txtAmount_TextChanged(sender As Object, e As EventArgs) Handles txtAmount.TextChanged
        CalculateBalance()
    End Sub

    Private Sub cmbType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbType.SelectedIndexChanged
        CalculateAmount()
    End Sub

    Private Sub cmbType_TextChanged(sender As Object, e As EventArgs) Handles cmbType.TextChanged
        CalculateAmount()
    End Sub

    Private Sub txtWeight_TextChanged(sender As Object, e As EventArgs) Handles txtWeight.TextChanged
        CalculateAmount()
    End Sub

    Private Sub txtCurrentRate_TextChanged(sender As Object, e As EventArgs) Handles txtCurrentRate.TextChanged
        CalculateAmount()
    End Sub

    Private Sub txtCut_TextChanged(sender As Object, e As EventArgs) Handles txtCut.TextChanged
        CalculateAmount()
    End Sub

    Private Sub MoveNext(ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Me.SelectNextControl(Me.ActiveControl, True, True, True, True)
        End If
    End Sub

    Private Sub txtWeight_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWeight.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtCurrentRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCurrentRate.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtCut_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCut.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtRate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRate.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtCashIn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCashIn.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtCashOut_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCashOut.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtAmount_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAmount.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtAdvance_KeyDown(sender As Object, e As KeyEventArgs) Handles txtAdvance.KeyDown
        MoveNext(e)
    End Sub

    Private Sub txtUdhar_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUdhar.KeyDown
        MoveNext(e)
    End Sub
    ' 1. Name Box me Enter dabane par Ginni par jaye
    Private Sub txtName1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName1.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True ' Beep sound aur blank hone se rokne ke liye
            txtGinni.Focus()
        End If
    End Sub

    ' 2. Ginni Box me Enter dabane par Pure par jaye
    Private Sub txtGinni_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGinni.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtPure.Focus()
        End If
    End Sub

    ' 3. Pure Box me Enter dabane par Ratti par jaye
    Private Sub txtPure_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPure.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            txtRattiIn.Focus()
        End If
    End Sub

    ' 4. Ratti Box me Enter dabane par Submit Button click ho jaye
    Private Sub txtRattiIn_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRattiIn.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            btnSubmitGinni.PerformClick() ' Automatically Submit button press ho jayega
        End If
    End Sub

    Private Sub SelectAllText(sender As Object, e As EventArgs)
        If TypeOf sender Is TextBox Then
            DirectCast(sender, TextBox).SelectAll()
        End If
    End Sub

    Private Sub txtWeight_Enter(sender As Object, e As EventArgs) Handles txtWeight.Enter
        SelectAllText(sender, e)
    End Sub

    Private Sub txtCurrentRate_Enter(sender As Object, e As EventArgs) Handles txtCurrentRate.Enter
        SelectAllText(sender, e)
    End Sub

    Private Sub txtCut_Enter(sender As Object, e As EventArgs) Handles txtCut.Enter
        SelectAllText(sender, e)
    End Sub

    Private Sub txtWeight_Leave(sender As Object, e As EventArgs) Handles txtWeight.Leave
        If txtWeight.Text.Trim <> "" Then
            Dim w As Double
            If Double.TryParse(txtWeight.Text, w) Then
                txtWeight.Text = w.ToString("0.000")
            End If
        End If
    End Sub

    Private Sub txtCurrentRate_Leave(sender As Object, e As EventArgs) Handles txtCurrentRate.Leave
        If txtCurrentRate.Text.Trim <> "" Then
            Dim r As Double
            If Double.TryParse(txtCurrentRate.Text, r) Then
                txtCurrentRate.Text = Math.Round(r).ToString("0")
            End If
        End If
    End Sub

    Private Sub txtCut_Leave(sender As Object, e As EventArgs) Handles txtCut.Leave
        If txtCut.Text.Trim <> "" Then
            Dim c As Double
            If Double.TryParse(txtCut.Text, c) Then
                txtCut.Text = Math.Round(c).ToString("0")
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If txtTime IsNot Nothing Then
            txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        End If
    End Sub

    ' ==========================================
    ' SAVE & CLEAR
    ' ==========================================
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If cmbCustomer.Text.Trim() = "" Then
                MessageBox.Show("Please select or enter Customer Name!", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim transType As String = cmbType.Text.Trim().ToUpper()
            Dim buyVal As Double = 0
            Dim saleVal As Double = 0

            If transType = "BUY" Then
                buyVal = Val(txtWeight.Text)
            Else
                saleVal = Val(txtWeight.Text)
            End If

            ' Yahan hum naya calculated balance nikal rahe hain aapke rule ke mutabiq
            Dim amt As Double = Val(txtAmount.Text)
            Dim cIn As Double = Val(txtCashIn.Text)
            Dim cOut As Double = Val(txtCashOut.Text)
            Dim finalBalance As Double = DatabaseManager.GetCalculatedBalance(amt, cIn, cOut, transType)

            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim sql As String = ""

                If Not String.IsNullOrEmpty(currentEditingPerchiNo) Then
                    sql = "UPDATE Perchi SET " &
                          "PerchiDate=@PerchiDate, PerchiTime=@PerchiTime, CustomerName=@CustomerName, Mobile=@Mobile, Address=@Address, " &
                          "TransactionType=@TransactionType, Buy=@Buy, Sale=@Sale, PieceRate=@PieceRate, Cut=@Cut, Rate=@Rate, " &
                          "CashIn=@CashIn, CashOut=@CashOut, Advance=@Advance, Udhar=@Udhar, Amount=@Amount, Balance=@Balance " &
                          "WHERE PerchiNo=@PerchiNo"
                Else
                    sql = "INSERT INTO Perchi " &
                          "(PerchiNo, PerchiDate, PerchiTime, CustomerName, Mobile, Address, TransactionType, Buy, Sale, PieceRate, Cut, Rate, CashIn, CashOut, Advance, Udhar, Amount, Balance, CreatedOn) " &
                          "VALUES " &
                          "(@PerchiNo, @PerchiDate, @PerchiTime, @CustomerName, @Mobile, @Address, @TransactionType, @Buy, @Sale, @PieceRate, @Cut, @Rate, @CashIn, @CashOut, @Advance, @Udhar, @Amount, @Balance, @CreatedOn)"
                End If

                Using cmd As New SQLiteCommand(sql, con)
                    cmd.Parameters.AddWithValue("@PerchiNo", txtPerchiNo.Text.Trim())
                    cmd.Parameters.AddWithValue("@PerchiDate", txtDate.Text.Trim())
                    cmd.Parameters.AddWithValue("@PerchiTime", txtTime.Text.Trim())
                    cmd.Parameters.AddWithValue("@CustomerName", cmbCustomer.Text.Trim())
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim())
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim())

                    cmd.Parameters.AddWithValue("@TransactionType", transType)
                    cmd.Parameters.AddWithValue("@Buy", buyVal)
                    cmd.Parameters.AddWithValue("@Sale", saleVal)

                    cmd.Parameters.AddWithValue("@PieceRate", Val(txtCurrentRate.Text))
                    cmd.Parameters.AddWithValue("@Cut", Val(txtCut.Text))
                    cmd.Parameters.AddWithValue("@Rate", Val(txtRate.Text))

                    cmd.Parameters.AddWithValue("@CashIn", Val(txtCashIn.Text))
                    cmd.Parameters.AddWithValue("@CashOut", Val(txtCashOut.Text))
                    cmd.Parameters.AddWithValue("@Advance", Val(txtAdvance.Text))
                    cmd.Parameters.AddWithValue("@Udhar", Val(txtUdhar.Text))

                    cmd.Parameters.AddWithValue("@Amount", amt)
                    cmd.Parameters.AddWithValue("@Balance", finalBalance) ' Yeh naya calculated balance save karega

                    If String.IsNullOrEmpty(currentEditingPerchiNo) Then
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    End If

                    cmd.ExecuteNonQuery()
                End Using
            End Using

            MessageBox.Show("Record kamyabi se save ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ResetFormToDefault()
            RefreshClosingBalances()

        Catch ex As Exception
            MessageBox.Show("Save Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ClearData()
        ResetFormToDefault()
    End Sub

    ' ==========================================
    ' CANCEL / RESET BUTTON & DEFAULT FORM STATE
    ' ==========================================
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        ResetFormToDefault()
    End Sub

    Public Sub ResetFormToDefault()
        currentEditingPerchiNo = ""

        ' Type ComboBox Clear Mode (Default Blank)
        If cmbType IsNot Nothing Then
            cmbType.SelectedIndex = -1
            cmbType.Text = ""
        End If

        ' Customer & Search fields clear
        If txtCustomerSearch IsNot Nothing Then txtCustomerSearch.Clear()
        If cmbCustomer IsNot Nothing Then
            cmbCustomer.SelectedIndex = -1
            cmbCustomer.Text = ""
        End If
        If txtMobile IsNot Nothing Then txtMobile.Clear()
        If txtAddress IsNot Nothing Then txtAddress.Clear()

        ' Weight & Rate fields clear
        If txtWeight IsNot Nothing Then txtWeight.Clear()
        If txtCurrentRate IsNot Nothing Then txtCurrentRate.Clear()
        If txtCut IsNot Nothing Then txtCut.Clear()
        If txtRate IsNot Nothing Then txtRate.Clear()
        If txtAmount IsNot Nothing Then txtAmount.Clear()

        ' Cash fields clear
        If txtCashIn IsNot Nothing Then txtCashIn.Clear()
        If txtCashOut IsNot Nothing Then txtCashOut.Clear()
        If txtAdvance IsNot Nothing Then txtAdvance.Clear()
        If txtUdhar IsNot Nothing Then txtUdhar.Clear()

        ' Date, Time & Perchi Number Reset
        If txtDate IsNot Nothing Then txtDate.Text = DateTime.Now.ToString("dd-MM-yyyy")
        If txtTime IsNot Nothing Then txtTime.Text = DateTime.Now.ToString("hh:mm:ss tt")
        If txtPerchiNo IsNot Nothing Then txtPerchiNo.Text = DatabaseManager.GetNextPerchiNo()

        ' Load Daily Totals for Gold and Cash
        LoadDailyReportTotals()

        If cmbCustomer IsNot Nothing Then cmbCustomer.Focus()
    End Sub

    ' ==========================================
    ' PRINTING & OTHER BUTTONS
    ' ==========================================
    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        If CurrentPrintData IsNot Nothing Then
            PrintManager.PrintSlip(e.Graphics, CurrentPrintData)
        End If
    End Sub

    Public Sub ShowPrintPreview(data As PrintData)
        CurrentPrintData = data
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Public Sub PrintSlip(data As PrintData)
        CurrentPrintData = data
        PrintDocument1.Print()
    End Sub

    Private Sub PopulatePrintData()
        CurrentPrintData = New PrintData()
        With CurrentPrintData
            .PerchiNo = txtPerchiNo.Text
            .PerchiDate = txtDate.Text
            .PerchiTime = txtTime.Text
            .CustomerName = cmbCustomer.Text
            .Mobile = txtMobile.Text
            .Address = txtAddress.Text

            .TransactionType = cmbType.Text.Trim().ToUpper()

            If .TransactionType = "BUY" Then
                .Buy = Val(txtWeight.Text)
                .Sale = 0
            Else
                .Buy = 0
                .Sale = Val(txtWeight.Text)
            End If

            .CurrentRate = Val(txtCurrentRate.Text)
            .Cut = Val(txtCut.Text)
            .Rate = Val(txtRate.Text)
            .Amount = Val(txtAmount.Text)
            .CashIn = Val(txtCashIn.Text)
            .CashOut = Val(txtCashOut.Text)
            .Advance = Val(txtAdvance.Text)
            .Udhar = Val(txtUdhar.Text)
        End With
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        PopulatePrintData()
        PrintPreviewDialog1.ShowDialog()
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PopulatePrintData()
        btnSave.PerformClick()
        PrintDocument1.Print()
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Dim frm As New FormHistory()
        frm.ShowDialog()
    End Sub

    Private Sub btnDailyReport_Click(sender As Object, e As EventArgs) Handles btnDailyReport.Click
        Dim frmReport As New FormDailyReport()
        frmReport.ShowDialog()
    End Sub

    Private Sub btnCustomers_Click(sender As Object, e As EventArgs) Handles btnCustomers.Click
        Dim frm As New FormCustomers()
        frm.ShowDialog()
    End Sub


    Public Sub DirectPrint(data As PrintData)
        CurrentPrintData = data
        PrintDocument1.Print()
    End Sub

    Private Sub btnKhata_Click(sender As Object, e As EventArgs) Handles btnKhata.Click
        Try
            Dim khataForm As New FormKhata()
            khataForm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Khata Form kholne mein masla aaya: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SendPerchiToWhatsApp()
        Dim phoneNo As String = txtMobile.Text.Trim()

        If String.IsNullOrEmpty(phoneNo) Then
            MessageBox.Show("Customer mobile number enter karein!", "Mobile Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        phoneNo = phoneNo.Replace(" ", "").Replace("-", "")

        If phoneNo.StartsWith("0") Then
            phoneNo = "92" & phoneNo.Substring(1)
        End If

        Dim msgText As String = "*⚖️ SULTAN GOLD TESTING LAB ⚖️*" & vbCrLf &
                               "----------------------------" & vbCrLf &
                               "Perchi No: " & txtPerchiNo.Text & vbCrLf &
                               "Date: " & txtDate.Text & vbCrLf &
                               "Time: " & txtTime.Text & vbCrLf &
                               "Customer: " & cmbCustomer.Text & vbCrLf &
                               "" & cmbType.Text & vbCrLf &
                               "Net Weight: " & txtWeight.Text & vbCrLf &
                               "Rate: " & txtRate.Text & vbCrLf &
                               "Amount: " & txtAmount.Text & vbCrLf &
                               "Advance: " & txtAdvance.Text & vbCrLf &
                               "Udhar: " & txtUdhar.Text & vbCrLf &
                               "----------------------------" & vbCrLf &
                               "*CREATED BY SULTANSOFT*" & vbCrLf &
                               "☎ +923172503282"

        Dim url As String = "https://web.whatsapp.com/send?phone=" & phoneNo & "&text=" & Uri.EscapeDataString(msgText)

        Try
            Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnWhatsApp_Click(sender As Object, e As EventArgs) Handles btnWhatsApp.Click
        SendPerchiToWhatsApp()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Timer2 IsNot Nothing Then Timer2.Stop()
        If serialPort IsNot Nothing AndAlso serialPort.IsOpen Then
            serialPort.Close()
        End If
    End Sub

    Private Sub CapitalizeFirstLetter(ByRef ctrl As Control)
        If isUpdating OrElse String.IsNullOrWhiteSpace(ctrl.Text) Then Exit Sub

        Try
            isUpdating = True

            Dim selStart As Integer = 0
            If TypeOf ctrl Is TextBox Then
                selStart = DirectCast(ctrl, TextBox).SelectionStart
            ElseIf TypeOf ctrl Is ComboBox Then
                selStart = DirectCast(ctrl, ComboBox).SelectionStart
            End If

            Dim cultureInfo As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture
            Dim textInfo As System.Globalization.TextInfo = cultureInfo.TextInfo

            ctrl.Text = textInfo.ToTitleCase(ctrl.Text)

            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).SelectionStart = selStart
            ElseIf TypeOf ctrl Is ComboBox Then
                DirectCast(ctrl, ComboBox).SelectionStart = selStart
            End If

        Finally
            isUpdating = False
        End Try
    End Sub

    Private Sub cmbCustomer_TextChanged(sender As Object, e As EventArgs) Handles cmbCustomer.TextChanged
        CapitalizeFirstLetter(cmbCustomer)
    End Sub

    Private Sub txtNewCustomerName_TextChanged(sender As Object, e As EventArgs) Handles txtNewCustomerName.TextChanged
        CapitalizeFirstLetter(txtNewCustomerName)
    End Sub

    Private Sub txtNewAddress_TextChanged(sender As Object, e As EventArgs) Handles txtNewAddress.TextChanged
        CapitalizeFirstLetter(txtNewAddress)
    End Sub

    ' =========================================
    ' LOAD RECORD FOR EDITING
    ' =========================================
    Public Sub LoadRecordForEdit(perchiNo As String)
        Try
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                Dim query As String = "SELECT * FROM Perchi WHERE PerchiNo = @PerchiNo LIMIT 1"

                Using cmd As New SQLiteCommand(query, con)
                    cmd.Parameters.AddWithValue("@PerchiNo", perchiNo)

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            txtPerchiNo.Text = reader("PerchiNo").ToString()
                            cmbCustomer.Text = reader("CustomerName").ToString()
                            txtMobile.Text = reader("Mobile").ToString()
                            txtAddress.Text = reader("Address").ToString()
                            txtDate.Text = reader("PerchiDate").ToString()
                            txtTime.Text = reader("PerchiTime").ToString()

                            Dim typeStr As String = reader("TransactionType").ToString()
                            cmbType.Text = typeStr

                            Dim buyVal As Double = Convert.ToDouble(reader("Buy"))
                            Dim saleVal As Double = Convert.ToDouble(reader("Sale"))

                            If buyVal > 0 Then
                                txtWeight.Text = buyVal.ToString("0.000")
                            Else
                                txtWeight.Text = saleVal.ToString("0.000")
                            End If

                            txtCurrentRate.Text = Convert.ToDouble(reader("PieceRate")).ToString("0")
                            txtCut.Text = Convert.ToDouble(reader("Cut")).ToString("0")
                            txtRate.Text = Convert.ToDouble(reader("Rate")).ToString("0")
                            txtAmount.Text = Convert.ToDouble(reader("Amount")).ToString("0")
                            txtCashIn.Text = Convert.ToDouble(reader("CashIn")).ToString("0")
                            txtCashOut.Text = Convert.ToDouble(reader("CashOut")).ToString("0")
                            txtAdvance.Text = Convert.ToDouble(reader("Advance")).ToString("0")
                            txtUdhar.Text = Convert.ToDouble(reader("Udhar")).ToString("0")

                            currentEditingPerchiNo = perchiNo

                            MessageBox.Show("Perchi No [" & perchiNo & "] edit ke liye load ho gayi hai.", "Record Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Record nahi mila!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                    End Using
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Record Load Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ' ========================================================
    ' Application Launch & Live Real-Time Refresh Function
    ' ========================================================
    Public Sub RefreshClosingBalances()
        Try
            Dim todayDate As DateTime = DateTime.Now
            Dim todayFormatted As String = todayDate.ToString("dd-MM-yyyy")
            Dim y As String = todayDate.ToString("yyyy")
            Dim m As String = todayDate.ToString("MM")
            Dim d As String = todayDate.ToString("dd")

            Dim totalBuy As Double = 0
            Dim totalSale As Double = 0
            Dim totalCashIn As Double = 0
            Dim totalCashOut As Double = 0

            ' 1. FormDailyReport ka direct instance banayen opening lene ke liye
            Dim tempReport As New FormDailyReport()
            ' Note: Kyunki aapka GetOpeningBalances "Private" hai, hum usko FormDailyReport ke Load method ki tarah same logic se call karte hain
            ' Agar aap GetOpeningBalances ko Public kar denge FormDailyReport.vb me to ye line directly chal jayegi:
            ' Dim opening = tempReport.GetOpeningBalances(todayDate)

            ' Direct SQLite Query for Today's Perchi Totals
            Using connection As SQLiteConnection = DatabaseManager.GetConnection()
                Dim query As String = "SELECT " &
                                      "IFNULL(SUM(Buy), 0) AS TBuy, " &
                                      "IFNULL(SUM(Sale), 0) AS TSale, " &
                                      "IFNULL(SUM(CashIn), 0) AS TCashIn, " &
                                      "IFNULL(SUM(CashOut), 0) AS TCashOut " &
                                      "FROM Perchi " &
                                      "WHERE (PerchiDate LIKE @d1 OR PerchiDate LIKE @d2 OR PerchiDate LIKE @d3)"

                Using cmd As New SQLiteCommand(query, connection)
                    cmd.Parameters.AddWithValue("@d1", "%" & y & "-" & m & "-" & d & "%")
                    cmd.Parameters.AddWithValue("@d2", "%" & d & "-" & m & "-" & y & "%")
                    cmd.Parameters.AddWithValue("@d3", "%" & d & "/" & m & "/" & y & "%")

                    Using reader As SQLiteDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            totalBuy = Convert.ToDouble(reader("TBuy"))
                            totalSale = Convert.ToDouble(reader("TSale"))
                            totalCashIn = Convert.ToDouble(reader("TCashIn"))
                            totalCashOut = Convert.ToDouble(reader("TCashOut"))
                        End If
                    End Using
                End Using
            End Using

            ' 2. Opening Balance Fetch (FormDailyReport ke function se)
            ' Tip: FormDailyReport.vb me "Private Function GetOpeningBalances" ko "Public Function GetOpeningBalances" kar dein.
            Dim opening = tempReport.GetOpeningBalances(todayDate)
            Dim openingWeight As Double = opening.OpeningWeight
            Dim openingCash As Double = opening.OpeningCash

            ' 3. Final Closing Calculation
            Dim closingWeight As Double = openingWeight + totalBuy - totalSale
            Dim closingCash As Double = openingCash + totalCashIn - totalCashOut

            ' 4. Direct Form1 Labels / TextBoxes Update
            If txtGold IsNot Nothing Then txtGold.Text = closingWeight.ToString("0.000")
            If txtCash IsNot Nothing Then txtCash.Text = closingCash.ToString("0")

        Catch ex As Exception
            ' Silent Catch
        End Try
    End Sub

    Private Sub btnOpenWorth_Click(sender As Object, e As EventArgs) Handles btnOpenWorth.Click
        Dim frmReport As New FormWorth()
        frmReport.ShowDialog()
    End Sub


    ' ====================================================
    ' 1. Auto Calculation Method & Events
    ' Formula: ((Ginni - Pure) / Ginni) * 96
    ' ====================================================
    Private Sub CalculateGinniRattiIn()
        Try
            Dim ginniVal As Double = 0
            Dim pureVal As Double = 0

            Double.TryParse(txtGinni.Text.Trim(), ginniVal)
            Double.TryParse(txtPure.Text.Trim(), pureVal)

            If ginniVal > 0 Then
                Dim rattiInVal As Double = ((ginniVal - pureVal) / ginniVal) * 96
                txtRattiIn.Text = rattiInVal.ToString("N2")
            Else
                txtRattiIn.Text = "0.00"
            End If
        Catch ex As Exception
            txtRattiIn.Text = "0.00"
        End Try
    End Sub

    Private Sub txtGinni_TextChanged(sender As Object, e As EventArgs) Handles txtGinni.TextChanged
        CalculateGinniRattiIn()
    End Sub

    Private Sub txtPure_TextChanged(sender As Object, e As EventArgs) Handles txtPure.TextChanged
        CalculateGinniRattiIn()
    End Sub

    ' ====================================================
    ' 2. Form Open & Drive Backup Buttons
    ' ====================================================
    Private Sub btnGinni_Click(sender As Object, e As EventArgs) Handles btnGinni.Click
        Dim frmReport As New FormGinni()
        frmReport.ShowDialog()
    End Sub
    Public Sub btnBackup_Click(sender As Object, e As EventArgs) Handles btnBackup.Click
        Dim dbPath As String = IO.Path.Combine(Application.StartupPath, "perchi.db")
        DriveBackupManager.BackupToGoogleDrive(dbPath)
    End Sub

    Private Sub btnRestore_Click(sender As Object, e As EventArgs) Handles btnRestore.Click
        Try
            ' 1. OpenFileDialog se Restore karne wali File Chuna
            Dim openFileDialog1 As New OpenFileDialog()
            openFileDialog1.Filter = "SQLite Database (*.db)|*.db|All Files (*.*)|*.*"
            openFileDialog1.Title = "Restore karne ke liye Backup File Select Karein"

            If openFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim selectedFile As String = openFileDialog1.FileName

                ' 2. Confirmation Popup (File Name Dikhate Hue)
                Dim confirmResult As DialogResult = MessageBox.Show(
                "Kya aap is file se restore karna chahte hain?" & vbCrLf & vbCrLf & selectedFile & vbCrLf & vbCrLf & "Note: Current database replace ho jayega!",
                "Confirm Restore",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            )

                If confirmResult = DialogResult.Yes Then
                    Dim targetDbPath As String = IO.Path.Combine(Application.StartupPath, "perchi.db")

                    ' File Copy / Replace logic
                    IO.File.Copy(selectedFile, targetDbPath, True)

                    ' Google Drive Restore call (agar zaroorat ho)
                    DriveBackupManager.RestoreFromGoogleDrive(selectedFile)

                    ' 3. Success Popup
                    MessageBox.Show(
                    "Restore Successful! Database successfully update ho gaya hai.",
                    "Restore Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                )
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Restore Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ====================================================
    ' 3. Single Submit Button Handler (With Table Ensure & Date)
    ' ====================================================
    Private Sub btnSubmitGinni_Click(sender As Object, e As EventArgs) Handles btnSubmitGinni.Click
        ' Validation Check
        If String.IsNullOrWhiteSpace(txtName1.Text) Then
            MessageBox.Show("Meharbani karke Name darj karen!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtName1.Focus()
            Return
        End If

        Try
            Dim gName As String = txtName1.Text.Trim()
            Dim gGinni As Double = If(IsNumeric(txtGinni.Text), Convert.ToDouble(txtGinni.Text), 0)
            Dim gPure As Double = If(IsNumeric(txtPure.Text), Convert.ToDouble(txtPure.Text), 0)
            Dim gRattiIn As Double = If(IsNumeric(txtRattiIn.Text), Convert.ToDouble(txtRattiIn.Text), 0)
            Dim currentDate As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

            If gGinni <= 0 Then
                MessageBox.Show("Ginni ki sahi value darj karein!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtGinni.Focus()
                Return
            End If

            ' Save to SQLite Database
            Using con As SQLiteConnection = DatabaseManager.GetConnection()
                If con.State <> ConnectionState.Open Then con.Open()

                ' Table Ensure
                Dim createTableSql As String = "
                CREATE TABLE IF NOT EXISTS Ginni (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Ginni REAL DEFAULT 0,
                    Pure REAL DEFAULT 0,
                    RattiIn REAL DEFAULT 0,
                    CreatedDate TEXT
                );"
                Using cmdCreate As New SQLiteCommand(createTableSql, con)
                    cmdCreate.ExecuteNonQuery()
                End Using

                ' Insert Record
                Dim insertSql As String = "INSERT INTO Ginni (Name, Ginni, Pure, RattiIn, CreatedDate) VALUES (@Name, @Ginni, @Pure, @RattiIn, @CreatedDate)"
                Using cmdInsert As New SQLiteCommand(insertSql, con)
                    cmdInsert.Parameters.AddWithValue("@Name", gName)
                    cmdInsert.Parameters.AddWithValue("@Ginni", gGinni)
                    cmdInsert.Parameters.AddWithValue("@Pure", gPure)
                    cmdInsert.Parameters.AddWithValue("@RattiIn", gRattiIn)
                    cmdInsert.Parameters.AddWithValue("@CreatedDate", currentDate)
                    cmdInsert.ExecuteNonQuery()
                End Using
            End Using

            ' Refresh FormGinni Grid if active
            Dim ginniForm As FormGinni = Application.OpenForms.OfType(Of FormGinni)().FirstOrDefault()
            If ginniForm IsNot Nothing Then
                ginniForm.LoadGinniData()
            End If

            ' Clear Fields & Focus
            txtName1.Clear()
            txtGinni.Clear()
            txtPure.Clear()
            txtRattiIn.Text = "0.00"
            txtName1.Focus()

            MessageBox.Show("Ginni Record Kamyabi se Save ho gaya!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Submit Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ' Form1 par Ginni Input fields clear karne ke liye
    Private Sub ClearGinniInputs()
        txtName1.Clear()
        txtGinni.Clear()
        txtPure.Clear()
        txtRattiIn.Text = "0.00"
        txtName1.Focus()
    End Sub

    Private Sub btnOpenDashboard_Click(sender As Object, e As EventArgs) Handles btnOpenDashboard.Click
        ' Sirf iss Form1 ko close karein, purana Dashboard khud samne aa jayega
        Me.Close()
    End Sub
    Private Sub btnPerchi_Click(sender As Object, e As EventArgs) Handles btnOpenperchi.Click
        ' Sultan Perchi ke main form ko call karne ke liye:
        Dim frmPerchi As New Sultan_perchi_Final.Form1()
        frmPerchi.Show()

    End Sub
End Class