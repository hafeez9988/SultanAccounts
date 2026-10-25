Imports System.Data.SQLite
Imports System.IO
Imports System.Windows.Forms

Public Module DatabaseModule
    Public dbPath As String = Application.StartupPath & "\perchi.db"
    Public connStr As String = "Data Source=" & dbPath & ";Version=3;"

    Public Sub InitializeDatabase()
        If Not File.Exists(dbPath) Then
            SQLiteConnection.CreateFile(dbPath)
        End If

        Using conn As New SQLiteConnection(connStr)
            conn.Open()

            ' Customer Table
            Dim cmd1 As New SQLiteCommand("CREATE TABLE IF NOT EXISTS Customers (" &
                "ID INTEGER PRIMARY KEY AUTOINCREMENT, " &
                "Name TEXT UNIQUE, " &
                "Mobile TEXT, " &
                "Address TEXT)", conn)
            cmd1.ExecuteNonQuery()

            ' Transactions Table
            Dim cmd2 As New SQLiteCommand("CREATE TABLE IF NOT EXISTS Transactions (" &
                "PerchiNo TEXT PRIMARY KEY, " &
                "CustomerName TEXT, " &
                "Date TEXT, " &
                "Time TEXT, " &
                "Type TEXT, " &
                "Buy REAL DEFAULT 0, " &
                "Sale REAL DEFAULT 0, " &
                "CurrentRate REAL DEFAULT 0, " &
                "Cut REAL DEFAULT 0, " &
                "Rate REAL DEFAULT 0, " &
                "Amount REAL DEFAULT 0, " &
                "CashIn REAL DEFAULT 0, " &
                "CashOut REAL DEFAULT 0, " &
                "Udhar REAL DEFAULT 0, " &
                "Advance REAL DEFAULT 0, " &
                "Balance REAL DEFAULT 0)", conn)
            cmd2.ExecuteNonQuery()

            ' Daily Opening Balance Table
            Dim cmd3 As New SQLiteCommand("CREATE TABLE IF NOT EXISTS DailyOpening (" &
                "Date TEXT PRIMARY KEY, " &
                "OpeningWeight REAL DEFAULT 0, " &
                "OpeningCash REAL DEFAULT 0)", conn)
            cmd3.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub LoadCustomersToCombo(cmb As ComboBox)
        cmb.Items.Clear()
        Using conn As New SQLiteConnection(connStr)
            conn.Open()
            Dim cmd As New SQLiteCommand("SELECT Name FROM Customers ORDER BY Name ASC", conn)
            Dim dr As SQLiteDataReader = cmd.ExecuteReader()
            While dr.Read()
                cmb.Items.Add(dr("Name").ToString())
            End While
        End Using
    End Sub
End Module