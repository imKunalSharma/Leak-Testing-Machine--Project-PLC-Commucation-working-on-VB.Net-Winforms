Public Class FlowChart

    Private Sub FlowChart_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Timer1.Enabled = True

        Timer1.Interval = 10



        If C_D658 = 1 Then
            PB658.Image = Image.FromFile(GreenImage)
        Else
            PB658.Image = Image.FromFile(RedImage)
        End If

        LB614.Text = C_D614

        If C_D724 = 1 Then
            Label3.BackColor = Color.Green
            Label4.BackColor = Color.Gray
        Else
            Label3.BackColor = Color.Gray
            Label4.BackColor = Color.Red
        End If

        If C_D721 = 1 Then
            PB721.Image = Image.FromFile(GreenImage)
        Else
            PB721.Image = Image.FromFile(RedImage)
        End If

        If C_D725 = 1 Then
            Label1.BackColor = Color.Green
            Label2.BackColor = Color.Gray
        Else
            Label1.BackColor = Color.Gray
            Label2.BackColor = Color.Red
        End If

        If C_D723 = 1 Then
            Label9.BackColor = Color.Green
            Label7.BackColor = Color.Green
        Else
            Label9.BackColor = Color.Red
            Label7.BackColor = Color.Red
        End If

        If C_D659 = 1 Then
            Label10.BackColor = Color.Green
        Else
            Label10.BackColor = Color.Red
        End If

        If C_D722 = 1 Then
            Label11.BackColor = Color.Green
            Label8.BackColor = Color.Green
        Else
            Label11.BackColor = Color.Red
            Label8.BackColor = Color.Red
        End If

        If C_D655 = 1 Then
            Label6.BackColor = Color.Green
            Label5.BackColor = Color.Gray
        Else
            Label6.BackColor = Color.Gray
            Label5.BackColor = Color.Red
        End If

        LB604.Text = C_D604

        

        If C_D657 = 1 Then
            PB657.Image = Image.FromFile(GreenImage)
        Else
            PB657.Image = Image.FromFile(RedImage)
        End If

        If C_D656 = 1 Then
            PB656.Image = Image.FromFile(GreenImage)
        Else
            PB656.Image = Image.FromFile(RedImage)
        End If

        LB586.Text = C_D586

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        If C_D658 = 1 Then
            PB658.Image = Image.FromFile(GreenImage)
        Else
            PB658.Image = Image.FromFile(RedImage)
        End If

        LB614.Text = C_D614

        If C_D724 = 1 Then
            Label3.BackColor = Color.Green
            Label4.BackColor = Color.Gray
        Else
            Label3.BackColor = Color.Gray
            Label4.BackColor = Color.Red
        End If

        If C_D721 = 1 Then
            PB721.Image = Image.FromFile(GreenImage)
        Else
            PB721.Image = Image.FromFile(RedImage)
        End If

        If C_D725 = 1 Then
            Label1.BackColor = Color.Green
            Label2.BackColor = Color.Gray
        Else
            Label1.BackColor = Color.Gray
            Label2.BackColor = Color.Red
        End If

        If C_D723 = 1 Then
            Label9.BackColor = Color.Green
            Label7.BackColor = Color.Green
        Else
            Label9.BackColor = Color.Red
            Label7.BackColor = Color.Red
        End If

        If C_D659 = 1 Then
            Label10.BackColor = Color.Green
        Else
            Label10.BackColor = Color.Red
        End If

        If C_D722 = 1 Then
            Label11.BackColor = Color.Green
            Label8.BackColor = Color.Green
        Else
            Label11.BackColor = Color.Red
            Label8.BackColor = Color.Red
        End If

        If C_D655 = 1 Then
            Label6.BackColor = Color.Green
            Label5.BackColor = Color.Gray
        Else
            Label6.BackColor = Color.Gray
            Label5.BackColor = Color.Red
        End If

        LB604.Text = C_D604



        If C_D657 = 1 Then
            PB657.Image = Image.FromFile(GreenImage)
        Else
            PB657.Image = Image.FromFile(RedImage)
        End If

        If C_D656 = 1 Then
            PB656.Image = Image.FromFile(GreenImage)
        Else
            PB656.Image = Image.FromFile(RedImage)
        End If

        LB586.Text = C_D586

    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class