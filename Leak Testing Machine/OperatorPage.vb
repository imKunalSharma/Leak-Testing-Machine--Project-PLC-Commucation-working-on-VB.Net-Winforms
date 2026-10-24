Public Class OperatorPage
    Private ButtonClickCount1 As Integer = 0
    Private ButtonClickCount2 As Integer = 0
    Private ButtonClickCount3 As Integer = 0
    Private ButtonClickCount4 As Integer = 0
    Private ButtonClickCount5 As Integer = 0
    Private ButtonClickCount6 As Integer = 0
    Private ButtonClickCount7 As Integer = 0
    Private ButtonClickCount8 As Integer = 0
    Private ButtonClickCount9 As Integer = 0
    Private ButtonClickCount10 As Integer = 0
    Private ButtonClickCount11 As Integer = 0
    Private ButtonClickCount12 As Integer = 0
    Private ButtonClickCount13 As Integer = 0

    Private Sub OperatorPage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If D685 = 0 Then
            ButtonClickCount1 = 0
            Button1.BackColor = Color.LightGray
        Else
            ButtonClickCount1 = 1
            Button1.BackColor = Color.Green
        End If

        If D686 = 0 Then
            ButtonClickCount2 = 0
            Button2.BackColor = Color.LightGray
        Else
            ButtonClickCount2 = 1
            Button2.BackColor = Color.Green
        End If

        If D687 = 0 Then
            ButtonClickCount3 = 0
            Button3.BackColor = Color.LightGray
        Else
            ButtonClickCount3 = 1
            Button3.BackColor = Color.Green
        End If


        If D688 = 0 Then
            ButtonClickCount4 = 0
            Button4.BackColor = Color.LightGray
        Else
            ButtonClickCount4 = 1
            Button4.BackColor = Color.Green
        End If

        If D689 = 0 Then
            ButtonClickCount5 = 0
            Button5.BackColor = Color.LightGray
        Else
            ButtonClickCount5 = 1
            Button5.BackColor = Color.Green
        End If

        If D690 = 0 Then
            ButtonClickCount6 = 0
            Button6.BackColor = Color.LightGray
        Else
            ButtonClickCount6 = 1
            Button6.BackColor = Color.Green
        End If


        If D691 = 0 Then
            ButtonClickCount7 = 0
            Button7.BackColor = Color.LightGray
        Else
            ButtonClickCount7 = 1
            Button7.BackColor = Color.Green
        End If

        If D692 = 0 Then
            ButtonClickCount8 = 0
            Button8.BackColor = Color.LightGray
        Else
            ButtonClickCount8 = 1
            Button8.BackColor = Color.Green
        End If


        If D695 = 0 Then
            ButtonClickCount9 = 0
            Button9.BackColor = Color.LightGray
        Else
            ButtonClickCount9 = 1
            Button9.BackColor = Color.Green
        End If


        If D696 = 0 Then
            ButtonClickCount10 = 0
            Button11.BackColor = Color.LightGray
        Else
            ButtonClickCount10 = 1
            Button11.BackColor = Color.Green
        End If


        If D697 = 0 Then
            ButtonClickCount11 = 0
            Button10.BackColor = Color.LightGray
        Else
            ButtonClickCount11 = 1
            Button10.BackColor = Color.Green
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ButtonClickCount1 = 1 Then
            MyNonQuery("685", 0, 21)
            ButtonClickCount1 = 0
            Button1.BackColor = Color.LightGray
        Else
            MyNonQuery("685", 1, 22)
            Button1.BackColor = Color.Green
            ButtonClickCount1 += 1
        End If

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If ButtonClickCount2 = 1 Then
            MyNonQuery("686", 0, 23)
            ButtonClickCount2 = 0
            Button2.BackColor = Color.LightGray
        Else
            MyNonQuery("686", 1, 24)
            Button2.BackColor = Color.Green
            ButtonClickCount2 += 1
        End If

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If ButtonClickCount3 = 1 Then
            MyNonQuery("687", 0, 25)
            ButtonClickCount3 = 0
            Button3.BackColor = Color.LightGray
        Else
            MyNonQuery("687", 1, 26)
            Button3.BackColor = Color.Green
            ButtonClickCount3 += 1
        End If

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If ButtonClickCount4 = 1 Then
            MyNonQuery("688", 0, 27)
            ButtonClickCount4 = 0
            Button4.BackColor = Color.LightGray
        Else
            MyNonQuery("688", 1, 28)
            Button4.BackColor = Color.Green
            ButtonClickCount4 += 1
        End If

    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If ButtonClickCount5 = 1 Then
            MyNonQuery("689", 0, 29)
            ButtonClickCount5 = 0
            Button5.BackColor = Color.LightGray
        Else
            MyNonQuery("689", 1, 30)
            Button5.BackColor = Color.Green
            ButtonClickCount5 += 1
        End If

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If ButtonClickCount6 = 1 Then
            MyNonQuery("690", 0, 31)
            ButtonClickCount6 = 0
            Button6.BackColor = Color.LightGray
        Else
            MyNonQuery("690", 1, 32)
            Button6.BackColor = Color.Green
            ButtonClickCount6 += 1
        End If

    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        If ButtonClickCount7 = 1 Then
            MyNonQuery("691", 0, 33)
            ButtonClickCount7 = 0
            Button7.BackColor = Color.LightGray
        Else
            MyNonQuery("691", 1, 34)
            Button7.BackColor = Color.Green
            ButtonClickCount7 += 1
        End If

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        If ButtonClickCount8 = 1 Then
            MyNonQuery("692", 0, 35)
            ButtonClickCount8 = 0
            Button8.BackColor = Color.LightGray
        Else
            MyNonQuery("692", 1, 36)
            Button8.BackColor = Color.Green
            ButtonClickCount8 += 1
        End If

    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click

        If ButtonClickCount9 = 1 Then
            MyNonQuery("695", 0, 37)
            ButtonClickCount9 = 0
            Button9.BackColor = Color.LightGray
        Else
            MyNonQuery("695", 1, 38)
            Button9.BackColor = Color.Green
            ButtonClickCount9 += 1
        End If

    End Sub

    

    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        If ButtonClickCount10 = 1 Then
            MyNonQuery("696", 0, 39)
            ButtonClickCount10 = 0
            Button11.BackColor = Color.LightGray
        Else
            MyNonQuery("696", 1, 40)
            Button11.BackColor = Color.Green
            ButtonClickCount10 += 1
        End If

    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        If ButtonClickCount11 = 1 Then
            MyNonQuery("697", 0, 41)
            ButtonClickCount11 = 0
            Button10.BackColor = Color.LightGray
        Else
            MyNonQuery("697", 1, 42)
            Button10.BackColor = Color.Green
            ButtonClickCount11 += 1

        End If

    End Sub

    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        If ButtonClickCount12 = 1 Then
            MyNonQuery("664", 0, 41)
            ButtonClickCount12 = 0
            Button12.BackColor = Color.LightGray
        Else
            MyNonQuery("664", 1, 42)
            Button12.BackColor = Color.Green
            ButtonClickCount12 += 1

        End If
    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        If ButtonClickCount13 = 1 Then
            MyNonQuery("665", 0, 41)
            ButtonClickCount13 = 0
            Button13.BackColor = Color.LightGray
        Else
            MyNonQuery("665", 1, 42)
            Button13.BackColor = Color.Green
            ButtonClickCount13 += 1

        End If
    End Sub

    Private Sub Button14_Click(sender As System.Object, e As System.EventArgs) Handles Button14.Click
        Bypass.Show()
    End Sub
End Class