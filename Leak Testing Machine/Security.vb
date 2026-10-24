Public Class Security

    Private Sub Security_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim dt2 As DataTable = GetData("SELECT * FROM `Alert` order by ID")
        DataGridView1.DataSource = dt2
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False

        DataGridView1.Columns(0).Width = 50
        DataGridView1.Columns(1).Width = 100
        DataGridView1.Columns(2).Width = 400
        DataGridView1.Columns(3).Width = 100

        ComboBox1.Items.Add("Normal Alert")
        ComboBox1.Items.Add("Emergency Alert")
        ComboBox1.SelectedIndex = 0

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If Len(TextBox1.Text) = 0 Then
            MsgBox("Enter Sequence")
        Else
            If Len(TextBox2.Text) = 0 And Len(TextBox3.Text) = 0 Then
                MsgBox("Enter Alert")
            Else

                Dim dt As DataTable = GetData("SELECT * FROM `Alert` where Sequence = '" & TextBox1.Text & "'")
                If dt.Rows.Count() > 0 Then
                    MsgBox("Sequence Name allready Exists...", MsgBoxStyle.Information)
                Else
                    Dim queryinsert As String
                    queryinsert = "INSERT INTO `Alert`(`Sequence`, `Alert`, `Color` , `Option`) VALUES ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & ComboBox1.SelectedItem.ToString & "')"
                    InsertQuery(queryinsert)

                    Dim dt2 As DataTable = GetData("SELECT * FROM `Alert`")
                    DataGridView1.DataSource = dt2
                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    MsgBox("Alert Insert Successfully...", MsgBoxStyle.Information)
                    AlertFuncation() 'Funcation Alert msg and color
                End If
            End If
        End If
    End Sub

   

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            GroupBox1.Text = "Update/Delete Station"
            Button2.Enabled = False
            Button3.Enabled = True
            Button4.Enabled = True
            Label4.Text = row.Cells(0).Value.ToString
            TextBox1.Text = row.Cells(1).Value.ToString
            TextBox2.Text = row.Cells(2).Value.ToString
            TextBox3.Text = row.Cells(3).Value.ToString
            ComboBox1.SelectedItem = row.Cells(4).Value.ToString
        End If
    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        Button2.Enabled = True
        Button3.Enabled = False
        Button4.Enabled = False
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If Len(TextBox1.Text) = 0 Then
            MsgBox("Enter Sequence")
        Else
            If Len(TextBox2.Text) = 0 Then
                MsgBox("Enter Alert")
            Else

                
                Dim queryinsert As String
                queryinsert = "UPDATE `Alert` SET  Sequence ='" & TextBox1.Text & "', Alert ='" & TextBox2.Text & "', Color ='" & TextBox3.Text & "', `Option`='" & ComboBox1.SelectedItem.ToString & "' WHERE ID= " & Label4.Text & ""
                InsertQuery(queryinsert)

                Dim dt2 As DataTable = GetData("SELECT * FROM `Alert`")
                DataGridView1.DataSource = dt2
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                MsgBox("Alert Update Successfully...", MsgBoxStyle.Information)
                AlertFuncation() 'Funcation Alert msg and color
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If Len(TextBox1.Text) = 0 Then
            MsgBox("Enter Sequence")
        Else
            If Len(TextBox2.Text) = 0 Then
                MsgBox("Enter Alert")
            Else


                InsertQuery("DELETE FROM `Alert` WHERE ID= " & Label4.Text & " ")
                MsgBox("Alert DELETE Successfully...", MsgBoxStyle.Information)

                Dim dt2 As DataTable = GetData("SELECT * FROM `Alert`")
                DataGridView1.DataSource = dt2
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                AlertFuncation() 'Funcation Alert msg and color
            End If
        End If
    End Sub
End Class