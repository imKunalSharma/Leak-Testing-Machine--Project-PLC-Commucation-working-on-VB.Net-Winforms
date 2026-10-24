Public Class Registration

    Private Sub Registration_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        

        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

        dataview()
       
    End Sub
    Public Function dataview()
        If Auth = 3 Then
            Dim dt2 As DataTable = GetData("SELECT * FROM `UserTB` where Auth = '" & Auth + 1 & "'  order by ID asc")
            ComboBox1.DataSource = dt2
            ComboBox1.ValueMember = "Auth"
            ComboBox1.DisplayMember = "User"

            Dim dt As DataTable = GetData("SELECT `ID`, `Name`, `Designation`,`UserType` FROM `UsersTB` where UserType1 = '" & Auth + 1 & "' order by ID asc")
            DataGridView1.DataSource = dt
        ElseIf Auth = 2 Then
            Dim dt2 As DataTable = GetData("SELECT * FROM `UserTB` where Auth = '" & Auth + 1 & "' order by ID asc")
            ComboBox1.DataSource = dt2
            ComboBox1.ValueMember = "Auth"
            ComboBox1.DisplayMember = "User"

            Dim dt As DataTable = GetData("SELECT `ID`, `Name`, `Designation`,`UserType` FROM `UsersTB` where UserType1 = '" & Auth + 1 & "' order by ID asc")
            DataGridView1.DataSource = dt
        Else
            Dim dt2 As DataTable = GetData("SELECT * FROM `UserTB` order by Auth asc")
            ComboBox1.DataSource = dt2
            ComboBox1.ValueMember = "Auth"
            ComboBox1.DisplayMember = "User"

            Dim dt As DataTable = GetData("SELECT `ID`, `Name`, `Designation`,`UserType`,`Password` FROM `UsersTB` order by ID asc")
            DataGridView1.DataSource = dt
        End If
        Return Nothing
    End Function

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Button2.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)
        Dim dt As DataTable = GetData("SELECT * FROM `UsersTB` where Name = '" & TextBox1.Text & "'")
        If dt.Rows.Count() > 0 Then
            Dim User As String = dt.Rows(0).Item("Name")
            Dim Password As String = dt.Rows(0).Item("Password")
            Dim Auth As Integer = dt.Rows(0).Item("Auth")
            MsgBox("User All ready exists..", MsgBoxStyle.Information)
        Else

            Dim Insertstr As String

            Insertstr = "INSERT INTO `UsersTB` " & _
                        "(`Name`, `Designation`,`UserType`, `UserType1`,`Password`" & _
                         ") VALUES (" & _
                         "'" & TextBox1.Text & "', '" & TextBox2.Text & "' ,'" & row("User").ToString & "','" & row("Auth").ToString & "', '" & TextBox3.Text & "')"


            InsertQuery(Insertstr)
            dataview()
            'MsgBox("User inserted successfully..", MsgBoxStyle.Information)
            Label8.Text = "User inserted successfully.."
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False

        End If
    End Sub

    

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

            Button1.Enabled = False

            Button3.Enabled = True
            Button4.Enabled = True

            TextBox1.Enabled = True
            TextBox2.Enabled = True
            TextBox3.Enabled = True

            Label6.Text = ""
            Label7.Text = row.Cells(0).Value.ToString
            TextBox1.Text = row.Cells(1).Value.ToString
            TextBox2.Text = row.Cells(2).Value.ToString
            TextBox3.Text = row.Cells(4).Value.ToString

        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)
        Dim UpdateQuery As String

        UpdateQuery = "UPDATE `UsersTB` SET `Name` = '" & TextBox1.Text & "', `Designation` = '" & TextBox2.Text & "', `UserType` ='" & row("User").ToString & "', `UserType1` ='" & row("Auth").ToString & "', `Password` = '" & TextBox3.Text & "' WHERE `ID`= " & Label7.Text & ""
        

        InsertQuery(UpdateQuery)
        Label8.Text = "User Update successfully.."
        'MsgBox("User Update successfully..", MsgBoxStyle.Information)
        dataview()
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)

       
            
        InsertQuery("DELETE FROM `UsersTB` WHERE ID= " & Label7.Text & " ")
        'MsgBox("User DELETE Successfully...", MsgBoxStyle.Information)
        Label8.Text = "User DELETE Successfully..."
        dataview()
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False


    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If TextBox4.Text = "0007" Then
            TextBox4.Clear()
            Security.Show()
        Else
            Label8.Text = "Worng Password.."
        End If
    End Sub
End Class