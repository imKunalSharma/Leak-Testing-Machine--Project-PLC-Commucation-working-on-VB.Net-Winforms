Public Class IO

    Private Sub IO_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'DataGridView1.ColumnCount = 3
        'DataGridView1.Columns(0).Name = "SrNO"
        'DataGridView1.Columns(0).HeaderText = "Sr No"

        'DataGridView1.Columns(1).Name = "RegisterName"
        'DataGridView1.Columns(1).HeaderText = "Register Name"

        'DataGridView1.Columns(2).Name = "Status"
        'DataGridView1.Columns(2).HeaderText = "Status"

        'DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'DataGridView1.ColumnHeadersHeight = DataGridView1.ColumnHeadersHeight
        'DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        



        'DataGridView2.ColumnCount = 3
        'DataGridView2.Columns(0).Name = "SrNO"
        'DataGridView2.Columns(0).HeaderText = "Sr No"

        'DataGridView2.Columns(1).Name = "RegisterName"
        'DataGridView2.Columns(1).HeaderText = "Register Name"

        'DataGridView2.Columns(2).Name = "Status"
        'DataGridView2.Columns(2).HeaderText = "Status"

        'DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        'DataGridView2.ColumnHeadersHeight = DataGridView1.ColumnHeadersHeight
        'DataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        'DataGridView2.Columns(0).Width = 80
        'DataGridView2.Columns(1).Width = 300
        'DataGridView2.Columns(2).Width = 100

        'ReadIO()

        

        'Dim sq As String = "SELECT * FROM IO Where Register_Address = '" & D735 & "'"
        'Dim dt As DataTable = GetData(sq)
        'If dt.Rows.Count() > 0 Then
        '    Dim ID As String = dt.Rows(0).Item("ID")

        '    Dim UpdateQuery As String
        '    UpdateQuery = "UPDATE `IO` SET `Status` = '" & D735 & "' WHERE Register_Address = '" & D735 & "'"

        'End If

        'Read_Input()

        Timer1.Enabled = True
        Timer1.Interval = 10


        
        

    End Sub

    Public Function formating()

        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(i).Cells("Status").Value = "1" Then
                DataGridView1.Rows(i).Cells("Status").Style.BackColor = Color.FromArgb(0, 255, 0)
                'DataGridView1.Rows(i).Cells("RegisterAddress").Style.BackColor = Color.FromArgb(0, 255, 0)
                'DataGridView1.Rows(i).Cells("Register_Name").Style.BackColor = Color.FromArgb(0, 255, 0)
            ElseIf DataGridView1.Rows(i).Cells("Status").Value = "0" Then
                DataGridView1.Rows(i).Cells("Status").Style.BackColor = Color.Red
                'DataGridView1.Rows(i).Cells("RegisterAddress").Style.BackColor = Color.Red
                'DataGridView1.Rows(i).Cells("Register_Name").Style.BackColor = Color.Red
            End If
        Next
        Return Nothing
    End Function

    Public Function formating2()

        For i As Integer = 0 To DataGridView2.Rows.Count - 1
            If DataGridView2.Rows(i).Cells("Status").Value = "1" Then
                DataGridView2.Rows(i).Cells("Status").Style.BackColor = Color.FromArgb(0, 255, 0)
                'DataGridView2.Rows(i).Cells("RegisterAddress").Style.BackColor = Color.FromArgb(0, 255, 0)
                'DataGridView2.Rows(i).Cells("Register_Name").Style.BackColor = Color.FromArgb(0, 255, 0)
            ElseIf DataGridView2.Rows(i).Cells("Status").Value = "0" Then
                DataGridView2.Rows(i).Cells("Status").Style.BackColor = Color.Red
                'DataGridView2.Rows(i).Cells("RegisterAddress").Style.BackColor = Color.Red
                'DataGridView2.Rows(i).Cells("Register_Name").Style.BackColor = Color.Red
            End If
        Next

        Return Nothing
    End Function

    

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        Dim dt As DataTable = GetData("SELECT RegisterAddress, Register_Name, Status  FROM `IO_TB` where Option1='INPUT'")
        DataGridView1.DataSource = dt
        formating()
        DataGridView1.Columns(0).Width = 130
        DataGridView1.Columns(1).Width = 270
        DataGridView1.Columns(2).Width = 100

        Dim dt2 As DataTable = GetData("SELECT RegisterAddress, Register_Name, Status  FROM `IO_TB` where Option1='OUTPUT'")
        DataGridView2.DataSource = dt2
        DataGridView2.Columns(0).Width = 130
        DataGridView2.Columns(1).Width = 270
        DataGridView2.Columns(2).Width = 100
        formating2()
    End Sub

    Public D735 As Integer
    Public D736 As Integer
    Public D737 As Integer
    Public D738 As Integer
    Public D739 As Integer
    Public D740 As Integer
    Public D741 As Integer
    Public D742 As Integer

    Public D743 As Integer
    Public D744 As Integer
    Public D745 As Integer
    Public D746 As Integer
    Public D747 As Integer
    Public D748 As Integer
    Public D749 As Integer
    Public D750 As Integer

    Public D751 As Integer
    Public D752 As Integer
    Public D753 As Integer
    Public D754 As Integer
    Public D755 As Integer
    Public D756 As Integer
    Public D757 As Integer
    Public D758 As Integer

    Public D759 As Integer
    Public D760 As Integer
    Public D761 As Integer
    Public D762 As Integer
    Public D763 As Integer
    Public D764 As Integer
    Public D765 As Integer
    Public D766 As Integer

    Public D767 As Integer
    Public D768 As Integer
    Public D769 As Integer
    Public D770 As Integer
    Public D771 As Integer
    Public D772 As Integer
    Public D773 As Integer
    Public D774 As Integer


    Public Function Read_Input()

        


        Return Nothing
    End Function
End Class