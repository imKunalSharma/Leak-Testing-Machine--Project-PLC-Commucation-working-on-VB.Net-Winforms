Public Class Bypass

    Private ButtonClickCount1 As Integer = 0
    Private ButtonClickCount2 As Integer = 0
    Private ButtonClickCount3 As Integer = 0
    Private ButtonClickCount4 As Integer = 0

    Private Sub Bypass_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If D728 = 0 Then
            ButtonClickCount1 = 0
            Button1.BackColor = Color.LightGray
        Else
            ButtonClickCount1 = 1
            Button1.BackColor = Color.Green
        End If

        If D730 = 0 Then
            ButtonClickCount2 = 0
            Button2.BackColor = Color.LightGray
        Else
            ButtonClickCount2 = 1
            Button2.BackColor = Color.Green
        End If

        If D729 = 0 Then
            ButtonClickCount3 = 0
            Button3.BackColor = Color.LightGray
        Else
            ButtonClickCount3 = 1
            Button3.BackColor = Color.Green
        End If

        If D731 = 0 Then
            ButtonClickCount4 = 0
            Button4.BackColor = Color.LightGray
        Else
            ButtonClickCount4 = 1
            Button4.BackColor = Color.Green
        End If

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ButtonClickCount1 = 1 Then
            MyNonQuery("728", 0, 21)
            ButtonClickCount1 = 0
            Button1.BackColor = Color.LightGray
        Else
            MyNonQuery("728", 1, 22)
            Button1.BackColor = Color.Green
            ButtonClickCount1 += 1
        End If
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If ButtonClickCount2 = 1 Then
            MyNonQuery("730", 0, 22)
            ButtonClickCount2 = 0
            Button2.BackColor = Color.LightGray
        Else
            MyNonQuery("730", 1, 22)
            Button2.BackColor = Color.Green
            ButtonClickCount2 += 1
        End If
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If ButtonClickCount3 = 1 Then
            MyNonQuery("731", 0, 23)
            ButtonClickCount3 = 0
            Button3.BackColor = Color.LightGray
        Else
            MyNonQuery("731", 1, 23)
            Button3.BackColor = Color.Green
            ButtonClickCount3 += 1
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        If ButtonClickCount4 = 1 Then
            MyNonQuery("729", 0, 24)
            ButtonClickCount4 = 0
            Button4.BackColor = Color.LightGray
        Else
            MyNonQuery("729", 1, 24)
            Button4.BackColor = Color.Green
            ButtonClickCount4 += 1
        End If
    End Sub
End Class