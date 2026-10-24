Public Class LoginPage

    Private Sub LoginPage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        TextBox2.Clear()
        TextBox1.Clear()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click




        Dim dt As DataTable = GetData("SELECT * FROM `UsersTB` where Name = '" & TextBox2.Text & "' and Password = '" & TextBox1.Text & "'")
        If dt.Rows.Count() > 0 Then
            User = dt.Rows(0).Item("Name")
            Dim Password As String = dt.Rows(0).Item("Password")
            Auth = dt.Rows(0).Item("UserType1")
            TextBox2.Clear()
            TextBox1.Clear()
            Label4.Text = ""
            If Auth = 1 Then
                Me.Hide()

                Form3.Button1.Visible = True                         ' Power setting
                Form3.Button4.Visible = True                         ' Operator 
                Form3.UserSettingToolStripMenuItem.Visible = True   'reg
                Form3.ReportToolStripMenuItem.Visible = True       ' report
                Form3.ToolStripStatusLabel4.Text = User
                Form3.Show()
            ElseIf Auth = 2 Then
                Me.Hide()
                Form3.Button1.Visible = True
                Form3.Button4.Visible = True
                Form3.UserSettingToolStripMenuItem.Visible = True
                Form3.ReportToolStripMenuItem.Visible = True
                Form3.ToolStripStatusLabel4.Text = User
                Form3.Show()
            ElseIf Auth = 3 Then
                Me.Hide()

                Form3.Button1.Visible = True
                Form3.Button4.Visible = False
                Form3.UserSettingToolStripMenuItem.Visible = True
                Form3.ReportToolStripMenuItem.Visible = False
                Form3.ToolStripStatusLabel4.Text = User
                Form3.Show()
            ElseIf Auth = 4 Then
                Me.Hide()

                Form3.Button1.Visible = False
                Form3.Button4.Visible = False
                Form3.UserSettingToolStripMenuItem.Visible = False
                Form3.ReportToolStripMenuItem.Visible = False
                Form3.ToolStripStatusLabel4.Text = User
                Form3.Show()
            End If

        Else
            MsgBox("Worng Password")
            Label4.Text = "Worng Password"
        End If

        
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class