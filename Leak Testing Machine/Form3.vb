Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports Accord.Video.VFW
Imports Accord.Video.FFMPEG
Imports System.IO
Imports System.Drawing.Size
'Modbus TCP
Imports System.Net.Sockets
Imports System.Text
Imports System.Drawing.Color
Public Class Form3

    Private Sub Form3_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try

            AxWinsock1.Close()
            VideoCaptureSource.Stop()
            LoginPage.Close()
            Do Until AxWinsock1.CtlState = 0
                Application.DoEvents()
            Loop
        Catch ex As Exception
            Me.Dispose()
        End Try
    End Sub

    Public Sub Connection()
        'Timer2.Enabled = False
        AxWinsock1.Protocol = MSWinsockLib.ProtocolConstants.sckTCPProtocol
        AxWinsock1.Connect(ServerIP, ServerPort)
        PictureBox6.Image = Nothing
        Timer1.Enabled = True
        Timer1.Interval = 100
        Label40.Text = "Connecting ...."
        Label40.ForeColor = Color.Black

        On Error Resume Next

        Do Until AxWinsock1.CtlState = 7
            Application.DoEvents()
        Loop

        'If AxWinsock1.CtlState <> MSWinsockLib.StateConstants.sckConnecte Then

        'End If
    End Sub

    Function ModelView()
        Dim dt2 As DataTable = GetData("SELECT * FROM `AdminSettingTB`")
        ComboBox1.DataSource = dt2
        ComboBox1.ValueMember = "ModelValue"
        ComboBox1.DisplayMember = "ModelName"

        Return Nothing
    End Function
    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Form3.Show()
        OpenCamera()
        'ServerIP = "192.168.1.10"
        'ServerPort = "502"



        Dim sq As String = "SELECT * FROM BarRepeat_tb ORDER BY `date1` DESC"
        Dim dt As DataTable = GetData(sq)
        If dt.Rows.Count() > 0 Then
            Dim barcode1 As String = dt.Rows(0).Item("Barcode")
            TextBox1.Text = barcode1
        Else
            TextBox1.Text = ""
        End If


        AlertFuncation() 'Funcation Alert msg and color
        Connection() 'tcp connection request

        Label41.Text = ""
        TextBox1.ReadOnly = False
        ModelView()
        partselectarr()  ' Part Select Array


        'ProgressBar1.Maximum = 22


        'PARR(1) = "EMERGENCY PRESSED"
        'PARR(2) = "AIR PRESSURE LOW"
        'PARR(3) = "HIGH PRESSURE LOW"
        'PARR(4) = "OPERATOR IS NOT PRESENT "
        'PARR(5) = "PART IS NOT PRESENT"
        'PARR(6) = "FIXTURE IS NOT MATCHED"
        'PARR(7) = "BARCODE IS NG "
        'PARR(8) = "PART IS NOT CORRECT"
        'PARR(9) = "MACHINE IS HEALTHY "
        'PARR(10) = "PRESS CYCLE START PUSH BUTTON/ AUTO CYCLE START"
        'PARR(11) = "DOOR IS CLOSING "
        'PARR(12) = "JOB IN PRESSURE IS FILLING / FILLING TIME START"
        'PARR(13) = "JOB IN PRESSURE IS FILLED "
        'PARR(14) = "LEAK MEASURING TIME START "
        'PARR(15) = "PART IS OK / CHECK MANUALLY"
        'PARR(16) = "STANDING TIME IS START"
        'PARR(17) = "OPEN THE KNOB"
        'PARR(18) = "PRESS CYCLE START PUSH BUTTON / COIL RESISTANCE CHECK "
        'PARR(19) = "JOB OUT PRESSURE IS FILLING "
        'PARR(20) = "JOB OUT PRESSURE IS FILLED"
        'PARR(21) = "VISUAL CHECK TIME START "
        'PARR(22) = "LEAK DATA CAPTURE _ PRESS OK PB OR NG PB"
        'PARR(23) = "PART IS OK"
        'PARR(24) = "PART IS NG"
        'PARR(25) = "PUT PART IN NG BIN"
        'PARR(26) = "PART IS REPEATED"
        'PARR(27) = "PART IS NOT OK ACC. TO SELECTION"
        'PARR(28) = "LEFT DOOR IS OPEN"
        'PARR(29) = "RIGHT DOOR IS OPEN"
        'PARR(30) = "MANUAL"

        Timer3.Interval = 100
        Timer3.Enabled = True
        stopbarread = 1



        
       
       
    End Sub

    
    Private Sub AxWinsock1_ConnectionRequest(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_ConnectionRequestEvent) Handles AxWinsock1.ConnectionRequest

        If AxWinsock1.CtlState <> MSWinsockLib.StateConstants.sckClosed Then 'sckConnected Then
            AxWinsock1.Close()
            PictureBox6.Image = Image.FromFile(RedImage)
            'MsgBox("Error in Socket" & MsgBoxStyle.OkOnly)
            Label40.Text = "Disconnected"
            Label40.ForeColor = Color.Red
            Timer2.Interval = 1000
            Timer2.Enabled = True
            Timer1.Stop()
        End If

        AxWinsock1.Accept(e.requestID)

    End Sub

    Dim MbusQuery(11) As Byte
    Dim StartLow As Byte
    Dim StartHigh As Byte
    Dim LengthLow As Byte
    Dim LengthHigh As Byte

    Dim sendcmdarr(4, 1) As String
    Private Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        On Error Resume Next

        ToolStripStatusLabel3.Text = flag ' Status show


        Label44.Text = AxWinsock1.CtlState().ToString
        If AxWinsock1.CtlState = 8 Then
            PictureBox6.Image = Image.FromFile(RedImage)
            'MsgBox("Error in Socket" & MsgBoxStyle.OkOnly)
            Label40.Text = "Disconnected"
            Label40.ForeColor = Color.Red
            Timer2.Interval = 1000
            Timer2.Enabled = True
            Timer1.Stop()
        End If

        Dim x11 As String
        If flag = 0 Then

            If stopbarread = 1 Then
                flag = 1
            Else
                StartLow = Val(Address2) Mod 256       'Address
                StartHigh = Val(Address2) \ 256
                LengthLow = Val(Lenght2) Mod 256      'Lenght
                LengthHigh = Val(Lenght2) \ 256
                MbusQuery(0) = 0
                MbusQuery(1) = 0
                MbusQuery(2) = 0
                MbusQuery(3) = 0
                MbusQuery(4) = 0
                MbusQuery(5) = 6
                MbusQuery(6) = SalveId    'Salve ID
                MbusQuery(7) = 3  '3
                MbusQuery(8) = StartHigh
                MbusQuery(9) = StartLow
                MbusQuery(10) = LengthHigh
                MbusQuery(11) = LengthLow
                AxWinsock1.SendData(MbusQuery)

            End If

        ElseIf flag = 1 Then
            StartLow = Val(Address) Mod 256       'Address
            StartHigh = Val(Address) \ 256
            LengthLow = Val(Lenght) Mod 256      'Lenght
            LengthHigh = Val(Lenght) \ 256
            MbusQuery(0) = 0
            MbusQuery(1) = 0
            MbusQuery(2) = 0
            MbusQuery(3) = 0
            MbusQuery(4) = 0
            MbusQuery(5) = 6
            MbusQuery(6) = SalveId    'Salve ID
            MbusQuery(7) = 3  '3
            MbusQuery(8) = StartHigh
            MbusQuery(9) = StartLow
            MbusQuery(10) = LengthHigh
            MbusQuery(11) = LengthLow
            AxWinsock1.SendData(MbusQuery)
        ElseIf flag = 2 Then
            StartLow = Val(lenght_Address) Mod 256       'Address
            StartHigh = Val(lenght_Address) \ 256
            LengthLow = Val(lenght_Integer) Mod 256      'Lenght
            LengthHigh = Val(lenght_Integer) \ 256
            MbusQuery(0) = 0
            MbusQuery(1) = 0
            MbusQuery(2) = 0
            MbusQuery(3) = 0
            MbusQuery(4) = 0
            MbusQuery(5) = 6
            MbusQuery(6) = SalveId    'Salve ID
            MbusQuery(7) = 3  '3
            MbusQuery(8) = StartHigh
            MbusQuery(9) = StartLow
            MbusQuery(10) = LengthHigh
            MbusQuery(11) = LengthLow
            AxWinsock1.SendData(MbusQuery)

        'ElseIf flag = 3 Then
        '    StartLow = Val(lenght_Address3) Mod 256       'Address
        '    StartHigh = Val(lenght_Address3) \ 256
        '    LengthLow = Val(lenght_Integer3) Mod 256      'Lenght
        '    LengthHigh = Val(lenght_Integer3) \ 256
        '    MbusQuery(0) = 0
        '    MbusQuery(1) = 0
        '    MbusQuery(2) = 0
        '    MbusQuery(3) = 0
        '    MbusQuery(4) = 0
        '    MbusQuery(5) = 6
        '    MbusQuery(6) = SalveId    'Salve ID
        '    MbusQuery(7) = 3  '3
        '    MbusQuery(8) = StartHigh
        '    MbusQuery(9) = StartLow
        '    MbusQuery(10) = LengthHigh
        '    MbusQuery(11) = LengthLow
        '    AxWinsock1.SendData(MbusQuery)

        Else 'flag = 3 Then
            x11 = flag
            flag = 0
        End If




        TX = ""
        TX = "Tx - "

        For i = 1 To MbusQuery.Length - 1
            TX = TX + CStr((MbusQuery(i)) & " ")
            'Form3.DataGridView1.Rows(i - 1).Cells(1).Value = MbusQuery(i)
        Next
        ToolStripStatusLabel1.Text = TX





    End Sub

    Private Sub AxWinsock1_DataArrival(ByVal sender As Object, ByVal e As AxMSWinsockLib.DMSWinsockControlEvents_DataArrivalEvent) Handles AxWinsock1.DataArrival

        On Error Resume Next

        Dim sData As String = ""
        Dim recivedbytes(1024) As Integer
        AxWinsock1.GetData(sData, vbString, 256)

        PictureBox6.Image = Image.FromFile(GreenImage)
        Label40.Text = "Connected..."
        Label40.ForeColor = Color.Green

        RX = ""
        RX = "Rx - "
        For i = 1 To Len(sData)
            recivedbytes(i) = Asc(Mid(sData, i))
            RX = RX + CStr(recivedbytes(i) & " ")
        Next

        ToolStripStatusLabel2.Text = RX

        Dim vIn() As Byte
        'Dim x(500) As String
        Dim c1 As Integer = 1
        Dim dd As String = Len(sData)

        If recivedbytes(8) = 6 Or recivedbytes(8) = 16 Then

            If recivedbytes(1) = 1 Then

                TextBox2.Text = cmdflagarr(0)

            ElseIf recivedbytes(1) = 2 Then

                TextBox3.Text = cmdflagarr(1)

            ElseIf recivedbytes(1) = 3 Then

                TextBox7.Text = cmdflagarr(2)

            ElseIf recivedbytes(1) = 4 Then

                TextBox6.Text = cmdflagarr(3)

            ElseIf recivedbytes(1) = 5 Then

                Label34.Text = cmdflagarr(4) & " - " & CoilVolMax

            ElseIf recivedbytes(1) = 6 Then

                Label34.Text = CoilVolMin & " - " & cmdflagarr(5)

            ElseIf recivedbytes(1) = 7 Then

                Label37.Text = cmdflagarr(6) & " - " & CoilCurrentMax

            ElseIf recivedbytes(1) = 8 Then

                Label37.Text = CoilCurrentMin & " - " & cmdflagarr(7)

            ElseIf recivedbytes(1) = 9 Then

                Label38.Text = cmdflagarr(8) & " - " & CoilResMax

            ElseIf recivedbytes(1) = 10 Then

                Label38.Text = CoilResMin & " - " & cmdflagarr(9)


            ElseIf recivedbytes(1) = 11 Then ' interger

                TextBox11.Text = cmdflagarr(10)


            ElseIf recivedbytes(1) = 12 Then ' interger

                TextBox14.Text = cmdflagarr(11)

            ElseIf recivedbytes(1) = 13 Then ' interger

                TextBox16.Text = cmdflagarr(12)

           

            ElseIf recivedbytes(1) = 15 Then ' interger

                Label39.Text = cmdflagarr(14)

            End If

        Else

            If flag = 0 Then
                flag = 1
                For v1 = 9 To Len(sData) - 1 Step 1
                    'vIn = New Byte() {recivedbytes(v1 + 2), recivedbytes(v1 + 1), recivedbytes(v1 + 4), recivedbytes(v1 + 3), recivedbytes(v1 + 6), recivedbytes(v1 + 5), recivedbytes(v1 + 8), recivedbytes(v1 + 7), recivedbytes(v1 + 10), recivedbytes(v1 + 9), recivedbytes(v1 + 12), recivedbytes(v1 + 11)} ', recivedbytes(v1 + 14), recivedbytes(v1 + 13)}
                    vIn = New Byte() {recivedbytes(v1 + 1)}
                    barcodeArr(c1) = System.Text.Encoding.UTF8.GetString(vIn)
                    ScanBarcode = barcodeArr(2) + barcodeArr(1) + barcodeArr(4) + barcodeArr(3) + barcodeArr(6) + barcodeArr(5) + barcodeArr(8) + barcodeArr(7) + barcodeArr(10) + barcodeArr(9) + barcodeArr(12) + barcodeArr(11) + barcodeArr(14) + barcodeArr(13)
                    c1 = c1 + 1

                    If ScanBarcode.Length = 0 Then
                        ScanBarcode = TextBox1.Text
                    Else
                        TextBox1.ReadOnly = True
                        TextBox1.Text = ScanBarcode.Trim
                    End If
                Next
            ElseIf flag = 1 Then
                flag = 2
                For v1 = 9 To Len(sData) - 1 Step 4
                    vIn = New Byte() {recivedbytes(v1 + 2), recivedbytes(v1 + 1), recivedbytes(v1 + 4), recivedbytes(v1 + 3)}

                    'vIn = New Byte() {recivedbytes(v1 + 3), recivedbytes(v1 + 4), recivedbytes(v1 + 1), recivedbytes(v1 + 2)}
                    Dim s As Single = BitConverter.ToSingle(vIn, 0)
                    FloatArr(c1) = s

                    c1 = c1 + 1

                    

                    HP_IN_Min = Convert.ToDouble(FloatArr(16))       '600
                    HP_IN_Max = Convert.ToDouble(FloatArr(17))       '602
                    HP_IN_AV = Convert.ToDouble(FloatArr(20))       '608
                    HP_IN_AV_O = Convert.ToDouble(FloatArr(11))    '590


                    HP_Out_Max = Convert.ToDouble(FloatArr(22))     '612
                    HP_Out_Min = Convert.ToDouble(FloatArr(21))     '610
                    HP_Out_AV = Convert.ToDouble(FloatArr(25))      '618
                    HP_Out_AV_O = Convert.ToDouble(FloatArr(10))     '588


                    HP_IN_Min_C = Convert.ToDouble(FloatArr(19))     '606
                    HP_IN_Max_C = Convert.ToDouble(FloatArr(24))    '616
                    HP_IN_AV_C = Convert.ToDouble(FloatArr(15))      '598

                    HP_Out_Max_C = Convert.ToDouble(FloatArr(13))    '594
                    HP_Out_Min_C = Convert.ToDouble(FloatArr(14))    '596
                    HP_Out_AV_C = Convert.ToDouble(FloatArr(12))     '592



                    Result_Voltage = Convert.ToDouble(FloatArr(29))     '626
                    Result_Current = Convert.ToDouble(FloatArr(33))     '634
                    Result_Res = Convert.ToDouble(FloatArr(37))         '642

                    Coil_Voltage = Convert.ToDouble(FloatArr(28))       '624
                    Coil_Current = Convert.ToDouble(FloatArr(32))       '632
                    Coil_Res = Convert.ToDouble(FloatArr(36))           '640
                    JobInPress = Convert.ToDouble(FloatArr(18))          '604
                    JobOutPress = Convert.ToDouble(FloatArr(23))        '614

                    CoilVolMin = Math.Round(Convert.ToDouble(FloatArr(26)), 2)      '620
                    CoilVolMax = Math.Round(Convert.ToDouble(FloatArr(27)), 2)      '622

                    CoilCurrentMin = Math.Round(Convert.ToDouble(FloatArr(30)), 2)      '628
                    CoilCurrentMax = Math.Round(Convert.ToDouble(FloatArr(31)), 2)      '630

                    CoilResMin = Math.Round(Convert.ToDouble(FloatArr(34)), 2)          '636
                    CoilResMax = Math.Round(Convert.ToDouble(FloatArr(35)), 2)          '638

                    InputHighPressure = Convert.ToDouble(FloatArr(9))          '586

                    Label72.Text = Math.Round(InputHighPressure, 2)
                    Dim inputPMin As Double = Convert.ToDouble(FloatArr(7)) - Convert.ToDouble(FloatArr(8))  '584 - 582
                    Dim InputPMax As Double = Convert.ToDouble(FloatArr(7)) + Convert.ToDouble(FloatArr(8))  '584 + 582

                    'chart
                    C_D614 = JobOutPress
                    C_D604 = JobInPress
                    C_D586 = InputHighPressure

                    


                    If Math.Round(InputHighPressure, 2) > Math.Round(inputPMin, 2) And Math.Round(InputHighPressure, 2) < Math.Round(InputPMax, 2) Then
                        Label72.BackColor = Color.Green
                    Else
                        Label72.BackColor = Color.Red
                    End If

                    Label34.Text = CoilVolMin & " - " & CoilVolMax
                    Label37.Text = CoilCurrentMin & " - " & CoilCurrentMax
                    Label38.Text = CoilResMin & " - " & CoilResMax

                    TextBox2.Text = Math.Round(HP_IN_Min, 2)
                    TextBox3.Text = Math.Round(HP_IN_Max, 2)
                    TextBox4.Text = Math.Round(HP_IN_AV, 2)
                    TextBox17.Text = Math.Round(HP_IN_AV_O, 2)
                    If Math.Round(HP_IN_AV, 2) > Math.Round(HP_IN_Min, 2) And Math.Round(HP_IN_AV, 2) < Math.Round(HP_IN_Max, 2) Then
                        TextBox4.BackColor = Color.Green
                    Else
                        TextBox4.BackColor = Color.Red
                    End If

                    TextBox7.Text = Math.Round(HP_Out_Min, 2)
                    TextBox6.Text = Math.Round(HP_Out_Max, 2)
                    TextBox5.Text = Math.Round(HP_Out_AV, 2)
                    TextBox18.Text = Math.Round(HP_Out_AV_O, 2)
                    If Math.Round(HP_Out_AV, 2) > Math.Round(HP_Out_Min, 2) And Math.Round(HP_Out_AV, 2) < Math.Round(HP_Out_Max, 2) Then
                        TextBox5.BackColor = Color.Green
                    Else
                        TextBox5.BackColor = Color.Red
                    End If


                    TextBox30.Text = Math.Round(HP_IN_Min_C, 2)
                    TextBox29.Text = Math.Round(HP_IN_Max_C, 2)
                    TextBox23.Text = Math.Round(HP_IN_AV_C, 2)

                    TextBox33.Text = Math.Round(HP_Out_Min_C, 2)
                    TextBox32.Text = Math.Round(HP_Out_Max_C, 2)
                    TextBox31.Text = Math.Round(HP_Out_AV_C, 2)

                    TextBox10.Text = Math.Round(Result_Voltage, 2)
                    TextBox9.Text = Math.Round(Result_Current, 2)
                    TextBox8.Text = Math.Round(Result_Res, 2)

                    If Math.Round(Result_Voltage, 2) > Math.Round(CoilVolMin, 2) And Math.Round(Result_Voltage, 2) < Math.Round(CoilVolMax, 2) Then
                        TextBox10.BackColor = Color.Green
                    Else
                        TextBox10.BackColor = Color.Red
                    End If

                    If Math.Round(Result_Current, 2) > Math.Round(CoilCurrentMin, 2) And Math.Round(Result_Current, 2) < Math.Round(CoilCurrentMax, 2) Then
                        TextBox9.BackColor = Color.Green
                    Else
                        TextBox9.BackColor = Color.Red
                    End If

                    If Math.Round(Result_Res, 2) > Math.Round(CoilResMin, 2) And Math.Round(Result_Res, 2) < Math.Round(CoilResMax, 2) Then
                        TextBox8.BackColor = Color.Green
                    Else
                        TextBox8.BackColor = Color.Red
                    End If


                    TextBox27.Text = Math.Round(Coil_Current, 2)
                    TextBox26.Text = Math.Round(Coil_Voltage, 2)
                    TextBox25.Text = Math.Round(Coil_Res, 2)
                    TextBox24.Text = Math.Round(JobInPress, 2)
                    TextBox28.Text = Math.Round(JobOutPress, 2)





                Next
            ElseIf flag = 2 Then
                flag = 0
                For v1 = 9 To Len(sData) - 1 Step 2

                    vIn = New Byte() {recivedbytes(v1 + 2), recivedbytes(v1 + 1)}
                    Dim vOut As Short = BitConverter.ToInt16(vIn, 0)

                    IntArr(c1) = vOut
                    c1 = c1 + 1

                    'Chart 

                    C_D658 = IntArr(15)
                    C_D721 = IntArr(78)
                    C_D724 = IntArr(81)
                    C_D723 = IntArr(80)
                    C_D659 = IntArr(16)
                    C_D722 = IntArr(79)
                    C_D655 = IntArr(12)
                    C_D725 = IntArr(82)
                    C_D657 = IntArr(14)
                    C_D656 = IntArr(13)

                    D728 = IntArr(85)
                    D730 = IntArr(87)
                    D729 = IntArr(86)
                    D731 = IntArr(88)



                    Act_FillingTime = IntArr(1)
                    Act_Standing = IntArr(2)
                    Act_MeasuringTime = IntArr(3)
                    'Act_JobOutTime = IntArr(7)

                    Set_FillingTime = IntArr(7)
                    Set_Standing = IntArr(9)
                    Set_MeasuringTime = IntArr(8)

                    Act_FillingTime_C = IntArr(4)
                    Act_Standing_C = IntArr(6)
                    Act_MeasuringTime_C = IntArr(5)

                    Set_FillingTime_C = IntArr(10)
                    Set_Standing_C = IntArr(12)
                    Set_MeasuringTime_C = IntArr(11)

                    KNOBCLOSED = IntArr(25)
                    KNOBOPEN = IntArr(26)

                    If IntArr(19) = 1 Then  'Reset cycle
                        If txtclear = 0 Then
                            flagPartok = 0
                            TextBox1.Text = ""
                            txtclear = 1
                        End If
                    End If

                    If IntArr(20) = 0 Then
                        Button2.BackColor = Color.Yellow
                    Else
                        Button2.BackColor = Color.Green
                    End If

                    Puch_Button = IntArr(23)  'Push Button on off
                    Door_Security = IntArr(24) ' Door_Security on off

                    Result_OK_Count = IntArr(27)
                    Result_NG_Count = IntArr(29)

                    Label39.Text = IntArr(33) ' PartSelection

                    
                    'ModelView()
                    'ComboBox1.SelectedIndex = IntArr(33)

                    Label39.Text = Selectmodel(IntArr(33))
                    PartSelection = IntArr(33)


                    If Len(TextBox1.Text) = 13 Then

                        If modelbardigit(IntArr(33)) <> TextBox1.Text.Substring(0, 3) Then
                            Label46.Text = "NO"
                            If flagPart = 0 Then
                                MyNonQuery("699", 2, 34)
                                flagPart = 1
                            End If
                        Else
                            Label46.Text = flagPart
                            flagPart = 0
                            If flagPartok = 0 Then
                                MyNonQuery("699", 1, 34)
                                flagPartok = 1
                            End If
                        End If
                    End If

                    TotalCount = IntArr(31)

                    TotalCycleTime = IntArr(17)

                    FinalResult = IntArr(40) ' 
                    Label43.Text = FinalResult

                    VideoStart = IntArr(39)

                    'Label46.Text = "Video Status " & IntArr(39)

                    Bar_AutoMaual = IntArr(51)

                    If Bar_AutoMaual = 0 Then
                        Label2.Text = "Scan Barcode"
                        TextBox1.ReadOnly = True
                        stopbarread = 0
                    Else
                        'TextBox1.Focus()
                        'TextBox1.Clear()
                        Label2.Text = "Manual Barcode"
                        TextBox1.ReadOnly = False

                        stopbarread = 1
                    End If

                    'Label48.Text = "Barcode S " & Bar_AutoMaual
                    viewstr = IntArr(41)

                    Label45.Text = AlertMsg(viewstr)

                    If OldAlert <> viewstr Then
                        If AlertEam(viewstr) = "Emergency Alert" Then
                            AlertInsert()   'Funcation
                            OldAlert = viewstr
                        End If
                    End If
                   

                    If AlertColor(viewstr) <> "" Then
                        Label45.BackColor = CType(clrConverter.ConvertFromString(AlertColor(viewstr)), Color)
                    Else
                        Label45.BackColor = Color.LightGray
                    End If

                    'Label42.Text = " Status : " & viewstr
                    'If (IntArr(41) >= 1 And IntArr(41) <= 8) Then
                    '    Label45.BackColor = Color.Red
                    'ElseIf (IntArr(41) >= 24 And IntArr(41) <= 25) Then
                    '    Label45.BackColor = Color.Red
                    'ElseIf IntArr(41) = 0 Then
                    '    Label45.BackColor = Color.LightGray
                    'Else
                    '    Label45.BackColor = Color.Green
                    'End If

                    D685 = IntArr(42)
                    D686 = IntArr(43)
                    D687 = IntArr(44)
                    D688 = IntArr(45)
                    D689 = IntArr(46)
                    D690 = IntArr(47)
                    D691 = IntArr(48)
                    D692 = IntArr(49)
                    D695 = IntArr(52)
                    D696 = IntArr(53)
                    D697 = IntArr(54)

                    repectbarcode = IntArr(83)  ' if repect barcode res no 726 is 1 then update the barcode value



                    TextBox12.Text = Act_FillingTime
                    TextBox13.Text = Act_Standing
                    TextBox15.Text = Act_MeasuringTime

                    TextBox11.Text = Set_FillingTime
                    TextBox14.Text = Set_Standing
                    TextBox16.Text = Set_MeasuringTime


                    TextBox38.Text = Act_FillingTime_C
                    TextBox36.Text = Act_Standing_C
                    TextBox34.Text = Act_MeasuringTime_C

                    TextBox39.Text = Set_FillingTime_C
                    TextBox37.Text = Set_Standing_C
                    TextBox35.Text = Set_MeasuringTime_C



                    TextBox21.Text = Result_OK_Count
                    TextBox20.Text = Result_NG_Count
                    TextBox19.Text = TotalCount

                    TextBox22.Text = TotalCycleTime




                    If VideoStart = 1 Then
                        'Label41.Text = ""
                        If flagvideo = 0 Then
                            If TextBox1.Text.Length <> 0 Then
                                If Not Directory.Exists(VedioPath) Then
                                    Directory.CreateDirectory(VedioPath)
                                End If
                                Dim path As String = VedioPath & TextBox1.Text & ".avi"
                                Dim h As Integer = VideoCaptureSource.VideoResolution.FrameSize.Height
                                Dim w As Integer = VideoCaptureSource.VideoResolution.FrameSize.Width
                                'Dim framesPerSecond As Integer = Math.Min(VideoCaptureSource.VideoResolution.MaximumFrameRate, 10)
                                'Dim bitRate As Integer = ((w * h) * framesPerSecond)
                                videowriter.Open(path, w, h, 25)
                                'videowriter.Open(path, w, h, framesPerSecond, VideoCodec.Default, bitRate)
                                flagvideo = 1
                                Label41.Text = " File Create " & path
                                txtclear = 0

                            End If
                        End If
                    End If

                    If VideoStart = 2 Then
                        If flagvideo = 1 Then
                            videowriter.Close()
                            If VideoCaptureSource Is Nothing Then
                                Return
                            End If
                            flagvideo = 0

                            If VideoCaptureSource.IsRunning Then
                                PictureBox1.Image = Nothing
                            End If

                            Label41.Text = " File Create Sucusfully "

                        End If
                    End If

                    If FinalResult = "1" Then
                        Label36.Text = "OK"
                        Label36.BackColor = Color.Green

                        ResultInsert()

                    ElseIf FinalResult = "2" Then
                        Label36.Text = "NG"
                        Label36.BackColor = Color.Red

                        ResultInsert()
                    Else
                        Label41.Text = ""
                        Label36.Text = ""
                        Label36.BackColor = Color.Black
                    End If

                    'for conform 
                    If VideoStart = 1 Then
                        MyNonQuery("732", 1, 15)
                    ElseIf VideoStop = 2 Then
                        MyNonQuery("733", 2, 15)
                    End If


                Next

                'ElseIf flag = 3 Then
                '    flag = 0
                '    For v1 = 9 To Len(sData) - 1 Step 2

                '        vIn = New Byte() {recivedbytes(v1 + 2), recivedbytes(v1 + 1)}
                '        Dim vOut As Short = BitConverter.ToInt16(vIn, 0)

                '        IntArr2(c1) = vOut
                '        c1 = c1 + 1

                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(92) & "' WHERE Register_Address = 'D735'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(93) & "' WHERE Register_Address = 'D736'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(94) & "' WHERE Register_Address = 'D737'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(95) & "' WHERE Register_Address = 'D738'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(96) & "' WHERE Register_Address = 'D739'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(97) & "' WHERE Register_Address = 'D740'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(98) & "' WHERE Register_Address = 'D741'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(99) & "' WHERE Register_Address = 'D742'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(100) & "' WHERE Register_Address = 'D743'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(101) & "' WHERE Register_Address = 'D744'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(102) & "' WHERE Register_Address = 'D745'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(103) & "' WHERE Register_Address = 'D746'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(104) & "' WHERE Register_Address = 'D747'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(105) & "' WHERE Register_Address = 'D748'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(106) & "' WHERE Register_Address = 'D749'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(107) & "' WHERE Register_Address = 'D750'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(108) & "' WHERE Register_Address = 'D751'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(109) & "' WHERE Register_Address = 'D752'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(110) & "' WHERE Register_Address = 'D753'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(111) & "' WHERE Register_Address = 'D754'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(112) & "' WHERE Register_Address = 'D755'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(113) & "' WHERE Register_Address = 'D756'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(114) & "' WHERE Register_Address = 'D757'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(115) & "' WHERE Register_Address = 'D758'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(116) & "' WHERE Register_Address = 'D759'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(117) & "' WHERE Register_Address = 'D760'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(118) & "' WHERE Register_Address = 'D761'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(119) & "' WHERE Register_Address = 'D762'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(120) & "' WHERE Register_Address = 'D763'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(121) & "' WHERE Register_Address = 'D764'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(122) & "' WHERE Register_Address = 'D765'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(123) & "' WHERE Register_Address = 'D766'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(124) & "' WHERE Register_Address = 'D767'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(125) & "' WHERE Register_Address = 'D768'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(126) & "' WHERE Register_Address = 'D769'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(127) & "' WHERE Register_Address = 'D770'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(128) & "' WHERE Register_Address = 'D771'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(129) & "' WHERE Register_Address = 'D772'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(130) & "' WHERE Register_Address = 'D773'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(131) & "' WHERE Register_Address = 'D774'")


                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(132) & "' WHERE Register_Address = 'D775'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(133) & "' WHERE Register_Address = 'D776'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(134) & "' WHERE Register_Address = 'D777'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(135) & "' WHERE Register_Address = 'D778'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(136) & "' WHERE Register_Address = 'D779'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(137) & "' WHERE Register_Address = 'D780'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(138) & "' WHERE Register_Address = 'D781'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(139) & "' WHERE Register_Address = 'D782'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(140) & "' WHERE Register_Address = 'D783'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(141) & "' WHERE Register_Address = 'D784'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(142) & "' WHERE Register_Address = 'D785'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(143) & "' WHERE Register_Address = 'D786'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(144) & "' WHERE Register_Address = 'D787'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(145) & "' WHERE Register_Address = 'D788'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(146) & "' WHERE Register_Address = 'D789'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(147) & "' WHERE Register_Address = 'D790'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(148) & "' WHERE Register_Address = 'D791'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(149) & "' WHERE Register_Address = 'D792'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(150) & "' WHERE Register_Address = 'D793'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(151) & "' WHERE Register_Address = 'D794'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(152) & "' WHERE Register_Address = 'D795'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(153) & "' WHERE Register_Address = 'D796'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(154) & "' WHERE Register_Address = 'D797'")
                '        InsertQuery("UPDATE `IO_TB` SET `Status` = '" & IntArr2(155) & "' WHERE Register_Address = 'D798'")

                '    Next

            End If


        End If






    End Sub

    Public videowriter As New VideoFileWriter
    Public VideoCaptureSource As VideoCaptureDevice
    Public VideoDevices As New FilterInfoCollection(FilterCategory.VideoInputDevice)
    Public Property VideoCapabilities As VideoCapabilities()
    Public captureDevice As VideoCaptureDeviceForm
    Public video As Bitmap
    Public Video_Camera As String

    Public Function OpenCamera()
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        captureDevice = New VideoCaptureDeviceForm()
        'captureDevice.Hide()
        'SendKeys.Send("{ENTER}")
        'Dim sss As String = VideoDevices(1).MonikerString
        'If captureDevice.ShowDialog(Me) = DialogResult.OK Then
        VideoCaptureSource = New VideoCaptureDevice(VideoDevices(0).MonikerString)
        VideoCaptureSource.VideoResolution = VideoCaptureSource.VideoCapabilities(0)
        AddHandler VideoCaptureSource.NewFrame, New NewFrameEventHandler(AddressOf FinalVideo_NewFrame)
        VideoCaptureSource.Start()
        
        'End If
        Return Nothing
    End Function

    Private Sub FinalVideo_NewFrame(ByVal sender As Object, ByVal eventArgs As NewFrameEventArgs)

        'Me.Invoke(CType(Function()
        On Error GoTo exit1

        If flagvideo = 1 Then

            If video IsNot Nothing Then
                video.Dispose()
            End If
            video = New Bitmap(eventArgs.Frame)
            PictureBox1.Image = New Bitmap(eventArgs.Frame)
            'If video IsNot Nothing Then
            '    Dim g As Graphics = Graphics.FromImage(PictureBox1.Image)
            '    g.DrawString(New String("Video Recoding.."), New Font("Arial", 15), Brushes.Red, New Rectangle(10, 10, 1000, 50))
            '    g.Dispose()
            'End If
            If videowriter.IsOpen = True Then
                videowriter.WriteVideoFrame(video)
            End If

        Else

            If video IsNot Nothing Then
                video.Dispose()
            End If
            video = New Bitmap(eventArgs.Frame)
            PictureBox1.Image = New Bitmap(eventArgs.Frame)
            'If video IsNot Nothing Then
            '    Dim g As Graphics = Graphics.FromImage(PictureBox1.Image)
            '    g.DrawString(New String("Video Live.."), New Font("Arial", 15), Brushes.Red, New Rectangle(10, 10, 1000, 50))
            '    g.Dispose()
            'End If

        End If

        'Me.Invoke(CType(Function()
        '                End Function, MethodInvoker))
        'GC.Collect()

        '                End Function, MethodInvoker))

Exit1:
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        AdminPage.Show()
    End Sub
    Private Sub UpdateProgressBar(ByVal percent As Integer)

        'Dim percentText As String = PARR(percent)
        'Dim font As Font = SystemFonts.DefaultFont

        'ProgressBar1.Value = percent
        'ProgressBar1.BackColor = Color.Red

        'Using gr = ProgressBar1.CreateGraphics()
        '    Dim X As Single = ProgressBar1.Width / 2 - (gr.MeasureString(percentText, font).Width / 2.0F)
        '    Dim Y As Single = ProgressBar1.Height / 2 - (gr.MeasureString(percentText, font).Height / 2.0F)
        '    gr.DrawString(percentText, font, Brushes.Black, New PointF(X, Y))

        'End Using

    End Sub
    Dim dealy_sd As Integer
    Dim counter As Integer
    Dim flagstop As Integer
    Private Sub Timer2_Tick(sender As Object, e As System.EventArgs) Handles Timer2.Tick

        'If AxWinsock1.CtlState = MSWinsockLib.StateConstants.sckClosed Then 'sckConnected Then
        '    ' ToolStripStatusLabel3.Text = flag ' Status show 
        '    PictureBox6.Image = Image.FromFile("C:\Test\Red.png")
        '    AxWinsock1.Close()
        '    Connection()
        'Else
        '    'AxWinsock1.Accept(e.requestID)
        '    PictureBox6.Image = Image.FromFile("C:\Test\Green.png")
        '    ToolStripStatusLabel3.Text = flag ' Status show 
        'End If

        'If AxWinsock1.CtlState = MSWinsockLib.StateConstants.sckConnectionPending Then
        '    PictureBox6.Image = Image.FromFile("C:\Test\Red.png")

        'End If
        dealy_sd = dealy_sd + 1
        If dealy_sd >= 2 Then
            dealy_sd = 0
            If counter < 1 Then
                counter = counter + 1
                AxWinsock1.Close()
                Connection()
                Timer2.Stop()
            Else

                counter = 0

            End If
        End If

    End Sub
    Private Sub ReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ReportToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub AxWinsock1_Error(sender As Object, e As AxMSWinsockLib.DMSWinsockControlEvents_ErrorEvent) Handles AxWinsock1.Error
        'MsgBox("Error in Socket" & MsgBoxStyle.OkOnly)

        PictureBox6.Image = Image.FromFile(RedImage)
        'MsgBox("Error in Socket" & MsgBoxStyle.OkOnly)
        Label40.Text = "Disconnected"
        Label40.ForeColor = Color.Red
        Timer2.Interval = 1000
        Timer2.Enabled = True
        Timer1.Stop()

    End Sub



    Private Sub Panel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub


    Dim d1 As Integer
    Dim c1 As Integer

    Private Sub Timer3_Tick(sender As Object, e As System.EventArgs) Handles Timer3.Tick

        If TextBox1.Text.Trim.Length = 13 Then
            Dim sq As String = "SELECT * FROM BarRepeat_tb where Barcode = '" & TextBox1.Text & "'"
            Dim dt As DataTable = GetData(sq)
            If dt.Rows.Count() > 0 Then
                Dim sr As String = dt.Rows(0).Item("ID")
                Dim Status As String = dt.Rows(0).Item("Status")
                If Status = 1 Then
                    'CHECK RESUTL TABLE
                    If txtclear = 1 Then
                        Dim sq2 As String = "SELECT * FROM ResultTB where Barcode = '" & TextBox1.Text & "'"
                        Dim dt2 As DataTable = GetData(sq2)
                        If dt2.Rows.Count() > 0 Then
                            Dim sr2 As String = dt2.Rows(0).Item("ID")
                            MyNonQuery("698", 2, 34)
                            'Label73.Text = 2
                            InsertQuery("UPDATE `BarRepeat_tb` SET Status = '2' WHERE ID= " & sr & "")

                            'd1 = d1 + 1
                            'If d1 >= 2 Then
                            '    d1 = 0
                            '    If modelbardigit(IntArr(33)) <> TextBox1.Text.Substring(0, 3) Then
                            '        MyNonQuery("699", 2, 34)
                            '    Else
                            '        MyNonQuery("699", 1, 34)
                            '    End If
                            'End If
                        Else
                            MyNonQuery("698", 1, 34)
                            'Label73.Text = 2
                            InsertQuery("UPDATE `BarRepeat_tb` SET Status = '1' WHERE ID= " & sr & "")

                            'd1 = d1 + 1
                            'If d1 >= 2 Then
                            '    d1 = 0
                            '    If modelbardigit(IntArr(33)) <> TextBox1.Text.Substring(0, 3) Then
                            '        MyNonQuery("699", 2, 34)
                            '    Else
                            '        MyNonQuery("699", 1, 34)
                            '    End If
                            'End If
                        End If
                    End If

                ElseIf Status = 2 Then
                    Dim bar As String = ""
                    If bar <> TextBox1.Text Then
                        MyNonQuery("698", 2, 34)
                        bar = TextBox1.Text
                    End If
                End If
            Else
                'MsgBox("Insert")
                InsertQuery("INSERT INTO `BarRepeat_tb`(`Barcode`,`Status`) VALUES ('" & TextBox1.Text & "', '1')")
                MyNonQuery("698", 1, 34)
                'Label47.Text = 1
                'd1 = d1 + 1
                'If d1 >= 2 Then
                '    d1 = 0
                '    If modelbardigit(IntArr(33)) <> TextBox1.Text.Substring(0, 3) Then
                '        MyNonQuery("699", 2, 34)
                '    Else
                '        MyNonQuery("699", 1, 34)
                '    End If
                'End If
            End If
        End If

    End Sub




    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        MyNonQuery("663", 1, 33)
        Button2.BackColor = Color.Green
        'Timer4.Enabled = True
        'Timer4.Interval = 1000
    End Sub

    'Dim dealy_sd2 As Integer
    'Dim counter_sd2 As Integer
    'Private Sub Timer4_Tick(sender As Object, e As System.EventArgs) Handles Timer4.Tick


    '    dealy_sd2 = dealy_sd2 + 1
    '    If dealy_sd2 >= 2 Then
    '        dealy_sd2 = 0
    '        If counter_sd2 < 8 Then
    '            counter_sd2 = counter_sd2 + 1

    '            If counter_sd2 = 2 Then
    '                MyNonQuery("663", 1, 33)
    '            End If
    '            If counter_sd2 = 7 Then
    '                MyNonQuery("663", 0, 33)
    '            End If
    '            If counter_sd2 = 8 Then
    '                Timer4.Stop()
    '            End If
    '        Else
    '            counter_sd2 = 0
    '        End If
    '    End If


    'End Sub



    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)

    End Sub
    Dim dealy_sd2 As Integer
    Dim counter2 As Integer
    Private Sub Timer4_Tick(sender As System.Object, e As System.EventArgs) Handles Timer4.Tick
        Dim row As DataRowView = DirectCast(ComboBox1.SelectedItem, DataRowView)
        'TextBox12.Text = row("ModelName").ToString
        'TextBox13.Text = row("ModelValue").ToString
        Label19.Text = "Please Wait ."


        Dim HP_IN_MIN As Double
        Dim HP_IN_MAX As Double

        Dim HP_OUT_MIN As Double
        Dim HP_OUT_MAX As Double

        Dim HP_IN_MIN_C As Double
        Dim HP_IN_MAX_C As Double

        Dim HP_OUT_MIN_C As Double
        Dim HP_OUT_MAX_C As Double


        Dim COIL_VOL_MIN As Double
        Dim COIL_VOL_MAX As Double

        Dim COIL_CUR_MIN As Double
        Dim COIL_CUR_MAX As Double

        Dim COIL_RES_MIN As Double
        Dim COIL_RES_MAX As Double

        Dim FILLING As Double
        Dim STANDING As Double
        Dim MEASURING As Double


        Dim FILLING_C As Double
        Dim STANDING_C As Double
        Dim MEASURING_C As Double


        Dim dt As DataTable = GetData("SELECT * FROM `AdminSettingTB` where ModelName = '" & row("ModelName").ToString & "'")
        If dt.Rows.Count() > 0 Then
            'TextBox15.Text = dt.Rows(0).Item("ModelBardigit")
            HP_IN_MIN = dt.Rows(0).Item("HP_IN_MIN").ToString
            HP_IN_MAX = dt.Rows(0).Item("HP_IN_MAX").ToString

            HP_OUT_MIN = dt.Rows(0).Item("HP_OUT_MIN").ToString
            HP_OUT_MAX = dt.Rows(0).Item("HP_OUT_MAX").ToString

            HP_IN_MIN_C = dt.Rows(0).Item("HP_IN_MIN_C").ToString
            HP_IN_MAX_C = dt.Rows(0).Item("HP_IN_MAX_C").ToString

            HP_OUT_MIN_C = dt.Rows(0).Item("HP_OUT_MIN_C").ToString
            HP_OUT_MAX_C = dt.Rows(0).Item("HP_OUT_MAX_C").ToString


            COIL_VOL_MIN = dt.Rows(0).Item("COIL_VOL_MIN").ToString
            COIL_VOL_MAX = dt.Rows(0).Item("COIL_VOL_MAX").ToString

            COIL_CUR_MIN = dt.Rows(0).Item("COIL_CUR_MIN").ToString
            COIL_CUR_MAX = dt.Rows(0).Item("COIL_CUR_MAX").ToString

            COIL_RES_MIN = dt.Rows(0).Item("COIL_RES_MIN").ToString
            COIL_RES_MAX = dt.Rows(0).Item("COIL_RES_MAX").ToString

            FILLING = dt.Rows(0).Item("FILLING").ToString
            STANDING = dt.Rows(0).Item("STANDING").ToString
            MEASURING = dt.Rows(0).Item("MEASURING").ToString


            FILLING_C = dt.Rows(0).Item("FILLING_C").ToString
            STANDING_C = dt.Rows(0).Item("STANDING_C").ToString
            MEASURING_C = dt.Rows(0).Item("MEASURING_C").ToString

            InputHighPressureMin = dt.Rows(0).Item("InputHighPressureMin").ToString
            InputHighPressureMax = dt.Rows(0).Item("InputHighPressureMax").ToString

        End If



        dealy_sd = dealy_sd + 1

        If dealy_sd >= 1 Then
            dealy_sd = 0
            If counter < 26 Then
                counter = counter + 1
                Label19.Text = "Please Wait ..." & counter
                If counter = 1 Then

                    MyNonQueryFloat("0600", HP_IN_MIN, 1)
                    ' cmdflagarr(0) = TextBox2.Text
                    '
                ElseIf counter = 2 Then

                    MyNonQueryFloat("602", HP_IN_MAX, 2)
                    'cmdflagarr(1) = TextBox3.Text

                ElseIf counter = 3 Then

                    MyNonQueryFloat("610", HP_OUT_MIN, 3)
                    'cmdflagarr(2) = TextBox4.Text

                ElseIf counter = 4 Then

                    MyNonQueryFloat("612", HP_OUT_MAX, 4)
                    ' cmdflagarr(3) = TextBox1.Text

                ElseIf counter = 5 Then

                    MyNonQueryFloat("620", COIL_VOL_MIN, 5)
                    'cmdflagarr(4) = TextBox6.Text

                ElseIf counter = 6 Then

                    MyNonQueryFloat("622", COIL_VOL_MAX, 6)
                    cmdflagarr(5) = TextBox5.Text

                ElseIf counter = 7 Then

                    MyNonQueryFloat("628", COIL_CUR_MIN, 7)
                    'cmdflagarr(6) = TextBox8.Text

                ElseIf counter = 8 Then

                    MyNonQueryFloat("630", COIL_CUR_MAX, 8)
                    'cmdflagarr(7) = TextBox7.Text

                ElseIf counter = 9 Then

                    MyNonQueryFloat("636", COIL_RES_MIN, 9)
                    ' cmdflagarr(8) = TextBox10.Text
                ElseIf counter = 10 Then

                    MyNonQueryFloat("638", COIL_RES_MAX, 10)
                    'cmdflagarr(9) = TextBox9.Text

                ElseIf counter = 11 Then

                    MyNonQuery("650", FILLING, 11) 'Filling time
                    'cmdflagarr(10) = TextBox11.Text

                ElseIf counter = 12 Then

                    MyNonQuery("652", STANDING, 12)   'Standing time
                    'cmdflagarr(11) = TextBox14.Text

                ElseIf counter = 13 Then

                    MyNonQuery("651", MEASURING, 13)   'Mes time
                    'cmdflagarr(12) = TextBox16.Text


                ElseIf counter = 15 Then
                    MyNonQuery("676", row("ModelValue"), 15) 'Part Selection
                    'cmdflagarr(14) = TextBox13.Text

                ElseIf counter = 16 Then

                    'MyNonQuery("694", ComboBox2.SelectedIndex.ToString, 15) 'barcode Manual and auto
                    'stopbarread = ComboBox2.SelectedIndex.ToString

                ElseIf counter = 17 Then

                    MyNonQueryFloat("0606", HP_IN_MIN_C, 1) 'HP_IN_MIN_C
                    'cmdflagarr(0) = TextBox2.Text

                ElseIf counter = 18 Then

                    MyNonQueryFloat("616", HP_IN_MAX_C, 2) 'HP_IN_MAX_C
                    'cmdflagarr(1) = TextBox3.Text

                ElseIf counter = 19 Then

                    MyNonQueryFloat("596", HP_OUT_MIN_C, 3) 'HP_OUT_MIN_C
                    'cmdflagarr(2) = TextBox4.Text

                ElseIf counter = 20 Then

                    MyNonQueryFloat("594", HP_OUT_MAX_C, 4) ' HP_OUT_MAX_C
                    ' cmdflagarr(3) = TextBox1.Text
                ElseIf counter = 21 Then

                    MyNonQuery("653", FILLING_C, 11) 'Filling time C
                    'cmdflagarr(10) = TextBox11.Text

                ElseIf counter = 22 Then

                    MyNonQuery("655", STANDING_C, 12)   'Standing time C
                    'cmdflagarr(11) = TextBox14.Text

                ElseIf counter = 23 Then

                    MyNonQuery("654", MEASURING_C, 13)   'Mes time C
                    'cmdflagarr(12) = TextBox16.Text

                ElseIf counter = 24 Then

                    MyNonQueryFloat("584", InputHighPressureMin, 4)

                ElseIf counter = 25 Then

                    MyNonQueryFloat("582", InputHighPressureMax, 4)

                End If

            Else
                Label19.Text = "Process Completed...!"
                Timer4.Stop()
                counter = 0

            End If
        End If

    End Sub


    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        'Label19.Text = "Please Wait ."
        Timer4.Interval = 100
        Timer4.Enabled = True
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        OperatorPage.Show()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LogoutToolStripMenuItem.Click
        Me.Hide()
        LoginPage.Show()

    End Sub

    Private Sub UserSettingToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UserSettingToolStripMenuItem.Click
        Registration.Show()
    End Sub

    Private Sub GroupBox1_Enter(sender As System.Object, e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub FlowChartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles FlowChartToolStripMenuItem.Click
        FlowChart.Show()
    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub AlertReportToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AlertReportToolStripMenuItem.Click
        AlertReport.Show()
    End Sub

    Private Sub TextBox33_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox33.TextChanged

    End Sub

    Private Sub InputToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub OutputRegisterToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OutputRegisterToolStripMenuItem.Click
        IO.Show()
    End Sub

   

    

    
End Class