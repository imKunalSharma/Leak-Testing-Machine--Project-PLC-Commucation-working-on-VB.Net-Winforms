Public Class AdminPage

    Private ButtonClickCount1 As Integer
    Private ButtonClickCount2 As Integer
    Private Sub AdminPage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Maximum = 27
        
       

        ViewData()

        

        GroupBox9.Enabled = False
        Label15.Text = ""

        ComboBox2.SelectedIndex = 0


        If Puch_Button = 0 Then
            ButtonClickCount1 = 0
            Button3.BackColor = Color.Green
            Button3.Text = "Panch ON"
        Else
            ButtonClickCount1 = 1
            Button3.BackColor = Color.Red
            Button3.Text = "Panch OFF"
        End If

        If Door_Security = 0 Then
            ButtonClickCount2 = 0
            Button9.BackColor = Color.Green
            Button9.Text = "Door Security ON"
        Else
            ButtonClickCount2 = 1
            Button9.BackColor = Color.Red
            Button9.Text = "Door Security OFF"
        End If

    End Sub
    '602
    
    Dim tx1(11) As Integer
    Dim txstr As String = ""
    Dim dealy_sd As Integer
    Dim counter As Integer
    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick

        Label15.Text = "Please Wait ..."

        dealy_sd = dealy_sd + 1

        If dealy_sd >= 1 Then
            dealy_sd = 0
            If counter < 26 Then
                counter = counter + 1
                Label15.Text = "Please Wait ..." & counter
                If counter = 1 Then

                    MyNonQueryFloat("0600", TextBox2.Text, 1)
                    cmdflagarr(0) = TextBox2.Text

                ElseIf counter = 2 Then

                    MyNonQueryFloat("602", TextBox3.Text, 2)
                    cmdflagarr(1) = TextBox3.Text

                ElseIf counter = 3 Then

                    MyNonQueryFloat("610", TextBox4.Text, 3)
                    cmdflagarr(2) = TextBox4.Text

                ElseIf counter = 4 Then

                    MyNonQueryFloat("612", TextBox1.Text, 4)
                    cmdflagarr(3) = TextBox1.Text

                ElseIf counter = 5 Then

                    MyNonQueryFloat("620", TextBox6.Text, 5)
                    cmdflagarr(4) = TextBox6.Text

                ElseIf counter = 6 Then

                    MyNonQueryFloat("622", TextBox5.Text, 6)
                    cmdflagarr(5) = TextBox5.Text

                ElseIf counter = 7 Then

                    MyNonQueryFloat("628", TextBox8.Text, 7)
                    cmdflagarr(6) = TextBox8.Text

                ElseIf counter = 8 Then

                    MyNonQueryFloat("630", TextBox7.Text, 8)
                    cmdflagarr(7) = TextBox7.Text

                ElseIf counter = 9 Then

                    MyNonQueryFloat("636", TextBox10.Text, 9)
                    cmdflagarr(8) = TextBox10.Text
                ElseIf counter = 10 Then

                    MyNonQueryFloat("638", TextBox9.Text, 10)
                    cmdflagarr(9) = TextBox9.Text

                ElseIf counter = 11 Then

                    MyNonQuery("650", TextBox11.Text, 11) 'Filling time
                    cmdflagarr(10) = TextBox11.Text

                ElseIf counter = 12 Then

                    MyNonQuery("652", TextBox14.Text, 12)   'Standing time
                    cmdflagarr(11) = TextBox14.Text

                ElseIf counter = 13 Then

                    MyNonQuery("651", TextBox16.Text, 13)   'Mes time
                    cmdflagarr(12) = TextBox16.Text


                ElseIf counter = 15 Then
                    MyNonQuery("676", TextBox13.Text, 15) 'Part Selection
                    cmdflagarr(14) = TextBox13.Text

                ElseIf counter = 16 Then

                    MyNonQuery("694", ComboBox2.SelectedIndex.ToString, 15) 'barcode Manual and auto
                    stopbarread = ComboBox2.SelectedIndex.ToString

                ElseIf counter = 17 Then

                    MyNonQueryFloat("0606", TextBox21.Text, 1)
                    'cmdflagarr(0) = TextBox2.Text

                ElseIf counter = 18 Then

                    MyNonQueryFloat("616", TextBox20.Text, 2)
                    'cmdflagarr(1) = TextBox3.Text

                ElseIf counter = 19 Then

                    MyNonQueryFloat("596", TextBox19.Text, 3)
                    'cmdflagarr(2) = TextBox4.Text

                ElseIf counter = 20 Then

                    MyNonQueryFloat("594", TextBox17.Text, 4)
                    ' cmdflagarr(3) = TextBox1.Text
                ElseIf counter = 21 Then

                    MyNonQuery("653", TextBox25.Text, 11) 'Filling time C
                    'cmdflagarr(10) = TextBox11.Text

                ElseIf counter = 22 Then

                    MyNonQuery("655", TextBox24.Text, 12)   'Standing time C
                    'cmdflagarr(11) = TextBox14.Text

                ElseIf counter = 23 Then

                    MyNonQuery("654", TextBox23.Text, 13)   'Mes time C
                    'cmdflagarr(12) = TextBox16.Text

                ElseIf counter = 24 Then

                    MyNonQueryFloat("584", TextBox22.Text, 4)

                ElseIf counter = 25 Then

                    MyNonQueryFloat("582", TextBox18.Text, 4)


                End If
                ProgressBar1.Value = counter
            Else
                Label15.Text = "Process Completed...!"
                counter = 0
                'If msg = 1 Then
                '    MsgBox("Model Insert Successfully...", MsgBoxStyle.Information)
                '    msg = 0
                'ElseIf msg = 2 Then
                '    MsgBox("Model Update Successfully...", MsgBoxStyle.Information)
                '    msg = 0
                'End If

                ProgressBar1.Value = 0
                Button5.Enabled = True


                Timer1.Stop()


                ViewData() ' Form view

                partselectarr()  ' Part Select Array




            End If
        End If

    End Sub

  
    
    'Private Sub TextBox2_Leave(sender As Object, e As System.EventArgs) Handles TextBox2.Leave
    '    If Len(TextBox2.Text) <> 0 Then
    '        MyNonQueryFloat("0601", TextBox2.Text)
    '        cmdflagarr(0) = 1
    '    End If
    'End Sub

    'Private Sub TextBox11_Leave(sender As Object, e As System.EventArgs) Handles TextBox11.Leave
    '    If Len(TextBox11.Text) <> 0 Then
    '        MyNonQuery("652", TextBox11.Text) 'Filling time
    '        cmdflagarr(10) = 1
    '    End If
    'End Sub

    'Private Sub TextBox14_Leave(sender As Object, e As System.EventArgs) Handles TextBox14.Leave
    '    If Len(TextBox14.Text) <> 0 Then
    '        MyNonQuery("656", TextBox14.Text)   'Standing time
    '        cmdflagarr(11) = 1
    '    End If
    'End Sub

    'Private Sub TextBox16_Leave(sender As Object, e As System.EventArgs) Handles TextBox16.Leave
    '    If Len(TextBox16.Text) <> 0 Then
    '        MyNonQuery("654", TextBox16.Text)   'Mes time
    '        cmdflagarr(12) = 1
    '    End If
    'End Sub

    'Private Sub TextBox18_Leave(sender As Object, e As System.EventArgs) Handles TextBox18.Leave
    '    If Len(TextBox18.Text) <> 0 Then
    '        MyNonQuery("658", TextBox18.Text)   'Job out time
    '        cmdflagarr(13) = 1
    '    End If

    'End Sub


    'Private Sub TextBox3_Leave(sender As Object, e As System.EventArgs) Handles TextBox3.Leave
    '    If Len(TextBox3.Text) <> 0 Then
    '        MyNonQueryFloat("603", TextBox3.Text)
    '        cmdflagarr(1) = 1
    '    End If
    'End Sub

    'Private Sub TextBox4_Leave(sender As Object, e As System.EventArgs) Handles TextBox4.Leave
    '    If Len(TextBox4.Text) <> 0 Then
    '        MyNonQueryFloat("611", TextBox4.Text)
    '        cmdflagarr(2) = 1
    '    End If
    'End Sub

    'Private Sub TextBox1_Leave(sender As Object, e As System.EventArgs) Handles TextBox1.Leave
    '    If Len(TextBox1.Text) <> 0 Then
    '        MyNonQueryFloat("613", TextBox1.Text)
    '        cmdflagarr(3) = 1
    '    End If
    'End Sub

    'Private Sub TextBox6_Leave(sender As Object, e As System.EventArgs) Handles TextBox6.Leave
    '    If Len(TextBox6.Text) <> 0 Then
    '        MyNonQueryFloat("621", TextBox6.Text)
    '        cmdflagarr(4) = 1
    '    End If
    'End Sub

    'Private Sub TextBox5_Leave(sender As Object, e As System.EventArgs) Handles TextBox5.Leave
    '    If Len(TextBox5.Text) <> 0 Then
    '        MyNonQueryFloat("623", TextBox5.Text)
    '        cmdflagarr(5) = 1
    '    End If
    'End Sub

    'Private Sub TextBox8_Leave(sender As Object, e As System.EventArgs) Handles TextBox8.Leave
    '    If Len(TextBox8.Text) <> 0 Then
    '        MyNonQueryFloat("629", TextBox8.Text)
    '        cmdflagarr(6) = 1
    '    End If
    'End Sub

    'Private Sub TextBox7_Leave(sender As Object, e As System.EventArgs) Handles TextBox7.Leave
    '    If Len(TextBox7.Text) <> 0 Then
    '        MyNonQueryFloat("631", TextBox7.Text)
    '        cmdflagarr(7) = 1
    '    End If
    'End Sub

    'Private Sub TextBox10_Leave(sender As Object, e As System.EventArgs) Handles TextBox10.Leave
    '    If Len(TextBox10.Text) <> 0 Then
    '        MyNonQueryFloat("637", TextBox10.Text)
    '        cmdflagarr(8) = 1
    '    End If
    'End Sub
    'Private Sub TextBox9_Leave(sender As Object, e As System.EventArgs) Handles TextBox9.Leave
    '    If Len(TextBox9.Text) <> 0 Then
    '        MyNonQueryFloat("639", TextBox9.Text)
    '        cmdflagarr(9) = 1
    '    End If
    'End Sub
    Dim clickflag As Integer
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        MyNonQuery("679", 1, 15)
        Button1.BackColor = Color.Green
        clickflag = 1
        Timer2.Interval = 1000
        Timer2.Enabled = True


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        MyNonQuery("680", 1, 17)
        Button2.BackColor = Color.Green
        clickflag = 2
        Timer2.Interval = 1000
        Timer2.Enabled = True

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click

        If ButtonClickCount1 = 1 Then
            MyNonQuery("666", 1, 21)
            ButtonClickCount1 = 0
            Button3.BackColor = Color.Red
            Button3.Text = "Panch OFF"
        Else
            MyNonQuery("666", 0, 22)
            Button3.BackColor = Color.Green
            Button3.Text = "Panch ON"
            ButtonClickCount1 += 1
        End If

    End Sub

    

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        GroupBox9.Enabled = True
        Button5.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = True

        TextBox12.Enabled = True
        TextBox12.Clear()
        TextBox15.Clear()
        TextBox13.Clear()
        TextBox2.Clear()
        TextBox3.Clear()

        TextBox4.Clear()
        TextBox1.Clear()

        TextBox6.Clear()
        TextBox5.Clear()

        TextBox8.Clear()
        TextBox7.Clear()

        TextBox10.Clear()
        TextBox9.Clear()

        TextBox11.Clear()
        TextBox14.Clear()
        TextBox16.Clear()


        TextBox25.Clear()
        TextBox24.Clear()
        TextBox23.Clear()



        TextBox21.Clear()
        TextBox20.Clear()
        TextBox19.Clear()
        TextBox17.Clear()

    End Sub
    Dim msg As Integer
    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        If Len(TextBox12.Text) = 0 Then
            MsgBox("Model Name Null is not acceptable", MsgBoxStyle.Information)
        Else
            If Len(TextBox3.Text) = 0 Or Len(TextBox4.Text) = 0 Or Len(TextBox1.Text) = 0 Or Len(TextBox6.Text) = 0 Or _
                Len(TextBox5.Text) = 0 Or Len(TextBox8.Text) = 0 Or Len(TextBox7.Text) = 0 Or Len(TextBox10.Text) = 0 Or _
                Len(TextBox9.Text) = 0 Or Len(TextBox11.Text) = 0 Or Len(TextBox14.Text) = 0 Or Len(TextBox16.Text) = 0 Or _
                Len(TextBox15.Text) = 0 Then

                MsgBox("Text Box Null is not acceptable", MsgBoxStyle.Information)
            Else

                Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & TextBox12.Text & "'")
                If dt.Rows.Count() > 0 Then
                    MsgBox("Model Name allready Exists...", MsgBoxStyle.Information)
                Else

                    Dim queryinsert As String
                    queryinsert = "INSERT INTO `AdminSettingTB`(`ModelName`, `ModelValue`, `ModelBardigit`, `HP_IN_MIN`, `HP_IN_MAX`,`HP_OUT_MIN`, `HP_OUT_MAX`, " & _
                                                                "`HP_IN_MIN_C`, `HP_IN_MAX_C`, `HP_OUT_MIN_C`, `HP_OUT_MAX_C`, `COIL_VOL_MIN`, `COIL_VOL_MAX`," & _
                                                                "`COIL_CUR_MIN`, `COIL_CUR_MAX`, `COIL_RES_MIN`, `COIL_RES_MAX`," & _
                                                                "`FILLING`, `STANDING`, `MEASURING`," & _
                                                                "`FILLING_C`, `STANDING_C`, `MEASURING_C`, `InputHighPressureMin`, `InputHighPressureMax`" & _
                                                                ") VALUES ('" & TextBox12.Text & "', '" & TextBox13.Text & "', '" & TextBox15.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "'," & _
                                                                "'" & TextBox4.Text & "', '" & TextBox1.Text & "', '" & TextBox21.Text & "', '" & TextBox20.Text & "', '" & TextBox19.Text & "', '" & TextBox17.Text & "', '" & TextBox6.Text & "', '" & TextBox5.Text & "'," & _
                                                                "'" & TextBox8.Text & "', '" & TextBox7.Text & "', '" & TextBox10.Text & "', '" & TextBox9.Text & "'," & _
                                                                "'" & TextBox11.Text & "', '" & TextBox14.Text & "', '" & TextBox16.Text & "'," & _
                                                                "'" & TextBox25.Text & "', '" & TextBox24.Text & "', '" & TextBox23.Text & "', '" & TextBox22.Text & "', '" & TextBox18.Text & "'" & _
                                                                ")"
                    InsertQuery(queryinsert)
                    Timer1.Interval = 100
                    Timer1.Enabled = True
                    msg = 1


                    Button8.Enabled = False
                End If
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)
        TextBox12.Text = row("ModelName").ToString
        TextBox13.Text = row("ModelValue").ToString

        Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & TextBox12.Text & "'")
        If dt.Rows.Count() > 0 Then
            TextBox15.Text = dt.Rows(0).Item("ModelBardigit")
            TextBox2.Text = dt.Rows(0).Item("HP_IN_MIN").ToString
            TextBox3.Text = dt.Rows(0).Item("HP_IN_MAX").ToString

            TextBox4.Text = dt.Rows(0).Item("HP_OUT_MIN").ToString
            TextBox1.Text = dt.Rows(0).Item("HP_OUT_MAX").ToString

            TextBox21.Text = dt.Rows(0).Item("HP_IN_MIN_C").ToString
            TextBox20.Text = dt.Rows(0).Item("HP_IN_MAX_C").ToString

            TextBox19.Text = dt.Rows(0).Item("HP_OUT_MIN_C").ToString
            TextBox17.Text = dt.Rows(0).Item("HP_OUT_MAX_C").ToString


            TextBox6.Text = dt.Rows(0).Item("COIL_VOL_MIN").ToString
            TextBox5.Text = dt.Rows(0).Item("COIL_VOL_MAX").ToString

            TextBox8.Text = dt.Rows(0).Item("COIL_CUR_MIN").ToString
            TextBox7.Text = dt.Rows(0).Item("COIL_CUR_MAX").ToString

            TextBox10.Text = dt.Rows(0).Item("COIL_RES_MIN").ToString
            TextBox9.Text = dt.Rows(0).Item("COIL_RES_MAX").ToString

            TextBox11.Text = dt.Rows(0).Item("FILLING").ToString
            TextBox14.Text = dt.Rows(0).Item("STANDING").ToString
            TextBox16.Text = dt.Rows(0).Item("MEASURING").ToString


            TextBox25.Text = dt.Rows(0).Item("FILLING_C").ToString
            TextBox24.Text = dt.Rows(0).Item("STANDING_C").ToString
            TextBox23.Text = dt.Rows(0).Item("MEASURING_C").ToString

            TextBox22.Text = dt.Rows(0).Item("InputHighPressureMin").ToString
            TextBox18.Text = dt.Rows(0).Item("InputHighPressureMax").ToString


        End If

        GroupBox9.Enabled = True
        Button8.Enabled = False
        TextBox12.Enabled = True
        Button5.Enabled = True
        Button7.Enabled = True
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Button5.Enabled = False
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)

        If Len(TextBox12.Text) = 0 Then
            MsgBox("Model Name Null is not acceptable", MsgBoxStyle.Information)
        Else
            If Len(TextBox3.Text) = 0 Or Len(TextBox4.Text) = 0 Or Len(TextBox1.Text) = 0 Or Len(TextBox6.Text) = 0 Or _
                Len(TextBox5.Text) = 0 Or Len(TextBox8.Text) = 0 Or Len(TextBox7.Text) = 0 Or Len(TextBox10.Text) = 0 Or _
                Len(TextBox9.Text) = 0 Or Len(TextBox11.Text) = 0 Or Len(TextBox14.Text) = 0 Or Len(TextBox16.Text) = 0 Then

                MsgBox("Text Box Null is not acceptable", MsgBoxStyle.Information)
            Else
                Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & row("ModelName").ToString & "'")
                If dt.Rows.Count() > 0 Then
                    Dim sr As String = dt.Rows(0).Item("ID")

                    Dim UpdateQuery As String
                    UpdateQuery = "UPDATE `AdminSettingTB` SET  ModelName = '" & TextBox12.Text & "'" & _
                                                            ", ModelValue = '" & TextBox13.Text & "'" & _
                                                            ", ModelBardigit = '" & TextBox15.Text & "'" & _
                                                            ", HP_IN_MIN = '" & TextBox2.Text & "'" & _
                                                            ", HP_IN_MAX = '" & TextBox3.Text & "'" & _
                                                            ", HP_OUT_MIN = '" & TextBox4.Text & "'" & _
                                                            ", HP_OUT_MAX = '" & TextBox1.Text & "'" & _
                                                            ", HP_IN_MIN_C = '" & TextBox21.Text & "'" & _
                                                            ", HP_IN_MAX_C = '" & TextBox20.Text & "'" & _
                                                            ", HP_OUT_MIN_C = '" & TextBox19.Text & "'" & _
                                                            ", HP_OUT_MAX_C = '" & TextBox17.Text & "'" & _
                                                            ", COIL_VOL_MIN = '" & TextBox6.Text & "'" & _
                                                            ", COIL_VOL_MAX = '" & TextBox5.Text & "'" & _
                                                            ", COIL_CUR_MIN = '" & TextBox8.Text & "'" & _
                                                            ", COIL_CUR_MAX = '" & TextBox7.Text & "'" & _
                                                            ", COIL_RES_MIN = '" & TextBox10.Text & "'" & _
                                                            ", COIL_RES_MAX = '" & TextBox9.Text & "'" & _
                                                            ", FILLING = '" & TextBox11.Text & "'" & _
                                                            ", STANDING = '" & TextBox14.Text & "'" & _
                                                            ", MEASURING = '" & TextBox16.Text & "'" & _
                                                            ", FILLING_C = '" & TextBox25.Text & "'" & _
                                                            ", STANDING_C = '" & TextBox24.Text & "'" & _
                                                            ", MEASURING_C = '" & TextBox23.Text & "'" & _
                                                            ", InputHighPressureMin = '" & TextBox22.Text & "'" & _
                                                            ", InputHighPressureMax = '" & TextBox18.Text & "'" & _
                                                            "WHERE ID= " & sr & " "

                    InsertQuery(UpdateQuery)


                    Timer1.Interval = 100
                    Timer1.Enabled = True

                    msg = 2





                End If
            End If
        End If
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)

        If Len(TextBox12.Text) = 0 Then
            MsgBox("Model Name Null is not acceptable", MsgBoxStyle.Information)
        Else

            Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & row("ModelName").ToString & "'")
            If dt.Rows.Count() > 0 Then
                Dim sr As String = dt.Rows(0).Item("ID")
                InsertQuery("DELETE FROM `AdminSettingTB` WHERE ID= " & sr & " ")
                MsgBox("Model DELETE Successfully...", MsgBoxStyle.Information)

                ViewData()

            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs)



    End Sub

    Public Function ViewData()
        Application.DoEvents()
        Dim dt2 As DataTable = GetData("SELECT * FROM `AdminSettingTB`")
        ComboBox1.DataSource = dt2
        ComboBox1.ValueMember = "ModelValue"
        ComboBox1.DisplayMember = "ModelName"

        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)
        TextBox12.Text = row("ModelName").ToString
        TextBox13.Text = row("ModelValue").ToString

        Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & TextBox12.Text & "'")
        If dt.Rows.Count() > 0 Then
            TextBox15.Text = dt.Rows(0).Item("ModelBardigit")
            TextBox2.Text = dt.Rows(0).Item("HP_IN_MIN")
            TextBox3.Text = dt.Rows(0).Item("HP_IN_MAX")

            TextBox4.Text = dt.Rows(0).Item("HP_OUT_MIN").ToString
            TextBox1.Text = dt.Rows(0).Item("HP_OUT_MAX").ToString


            TextBox21.Text = dt.Rows(0).Item("HP_IN_MIN_C").ToString
            TextBox20.Text = dt.Rows(0).Item("HP_IN_MAX_C").ToString

            TextBox19.Text = dt.Rows(0).Item("HP_OUT_MIN_C").ToString
            TextBox17.Text = dt.Rows(0).Item("HP_OUT_MAX_C").ToString


            TextBox6.Text = dt.Rows(0).Item("COIL_VOL_MIN").ToString
            TextBox5.Text = dt.Rows(0).Item("COIL_VOL_MAX").ToString

            TextBox8.Text = dt.Rows(0).Item("COIL_CUR_MIN").ToString
            TextBox7.Text = dt.Rows(0).Item("COIL_CUR_MAX").ToString

            TextBox10.Text = dt.Rows(0).Item("COIL_RES_MIN").ToString
            TextBox9.Text = dt.Rows(0).Item("COIL_RES_MAX").ToString

            TextBox11.Text = dt.Rows(0).Item("FILLING").ToString
            TextBox14.Text = dt.Rows(0).Item("STANDING").ToString
            TextBox16.Text = dt.Rows(0).Item("MEASURING").ToString


            TextBox25.Text = dt.Rows(0).Item("FILLING_C").ToString
            TextBox24.Text = dt.Rows(0).Item("STANDING_C").ToString
            TextBox23.Text = dt.Rows(0).Item("MEASURING_C").ToString

            TextBox22.Text = dt.Rows(0).Item("InputHighPressureMin").ToString
            TextBox18.Text = dt.Rows(0).Item("InputHighPressureMax").ToString


        End If
        Return Nothing
    End Function

    Public Function SendTOPLCData()

        

        'MyNonQuery("652", TextBox11.Text) 'Filling time
        'cmdflagarr(10) = 1

        'MyNonQuery("656", TextBox14.Text)   'Standing time
        'cmdflagarr(11) = 1

        'MyNonQuery("654", TextBox16.Text)   'Mes time
        'cmdflagarr(12) = 1

        'MyNonQuery("658", TextBox18.Text)   'Job out time
        'cmdflagarr(13) = 1

        'MyNonQueryFloat("603", TextBox3.Text)
        'cmdflagarr(1) = 1

        'MyNonQueryFloat("611", TextBox4.Text)
        'cmdflagarr(2) = 1

        'MyNonQueryFloat("613", TextBox1.Text)
        'cmdflagarr(3) = 1

        'MyNonQueryFloat("621", TextBox6.Text)
        'cmdflagarr(4) = 1

        'MyNonQueryFloat("623", TextBox5.Text)
        'cmdflagarr(5) = 1

        'MyNonQueryFloat("629", TextBox8.Text)
        'cmdflagarr(6) = 1

        'MyNonQueryFloat("631", TextBox7.Text)
        'cmdflagarr(7) = 1

        'MyNonQueryFloat("637", TextBox10.Text)
        'cmdflagarr(8) = 1

        'MyNonQueryFloat("639", TextBox9.Text)
        'cmdflagarr(9) = 1
      
      
    
        Return Nothing
    End Function

   

  Private Sub TextBox2_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox8_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox10_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox9_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox9.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox11_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub TextBox14_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox14.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox16_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox16.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox18_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    
    Private Sub Button9_Click_1(sender As System.Object, e As System.EventArgs)
        'MyNonQueryFloat(TextBox17.Text, TextBox15.Text)


    End Sub

    
    Private Sub TextBox2_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub
    Dim dealy_sd2 As Integer
    Dim counter2 As Integer

    Private Sub Timer2_Tick(sender As Object, e As System.EventArgs) Handles Timer2.Tick


        dealy_sd2 = dealy_sd2 + 1

        If dealy_sd2 >= 2 Then

            dealy_sd2 = 0
            If counter2 < 1 Then
                counter2 = counter2 + 1

                If clickflag = 1 Then
                    MyNonQuery("679", 0, 16)
                    Button1.BackColor = Color.LightGray
                ElseIf clickflag = 2 Then
                    MyNonQuery("680", 0, 18)
                    Button2.BackColor = Color.LightGray
                ElseIf clickflag = 3 Then
                    MyNonQuery("681", 0, 20)
                    Button3.BackColor = Color.LightGray
                End If
            Else
                counter2 = 0

                Timer2.Stop()
            End If
        End If

    End Sub

    Private Sub Button9_Click_2(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        If ButtonClickCount2 = 1 Then
            MyNonQuery("667", 1, 21)
            ButtonClickCount2 = 0
            Button9.BackColor = Color.Red
            Button9.Text = "Door Security OFF"
        Else
            MyNonQuery("667", 0, 22)
            Button9.BackColor = Color.Green
            Button9.Text = "Door Security ON"
            ButtonClickCount2 += 1
        End If
    End Sub

    Private Sub GroupBox6_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox6.Enter

    End Sub

    Private Sub TextBox22_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox22.KeyPress
        CHeckFuncation(sender, e)
    End Sub

    Private Sub TextBox18_KeyPress1(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox18.KeyPress
        CHeckFuncation(sender, e)
    End Sub
End Class