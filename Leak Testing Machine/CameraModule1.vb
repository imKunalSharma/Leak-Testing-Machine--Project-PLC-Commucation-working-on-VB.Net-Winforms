Imports System.Data
Imports System.Data.OleDb
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.IO

Module CameraModule1

    Public conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Test\Leak_DB.accdb" & ";Persist Security Info=True")
    Public RedImage As String = "C:\Test\red.png"
    Public GreenImage As String = "C:\Test\Green.png"
    Public VedioPath As String = "C:\Leak Vedio\"




    'Public conn As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Softech\Leak_DB.accdb" & ";Persist Security Info=True")
    'Public RedImage As String = "D:\Softech\red.png"
    'Public GreenImage As String = "D:\Softech\Green.png"
    'Public VedioPath As String = "D:\Leak Vedio\"




    Public ServerIP As String = "192.168.1.10"
    'Public ServerIP As String = "192.168.0.101"
    Public ServerPort As String = "502"
    Public SalveId As Integer = 1

    Public Address As String = "0570" 'float
    Public Lenght As Integer = 76

    Public lenght_Address As String = "0644" 'integer
    Public lenght_Integer As Integer = 99

    Public lenght_Address3 As String = "0735" 'integer
    Public lenght_Integer3 As Integer = 99

    Public Address2 As String = "0700"  'barcode
    Public Lenght2 As Integer = 8
    Public TX As String
    Public RX As String

    Public ScanBarcode As String
    Public PartSelection As Integer
    Public TotalCycleTime As String

    Public HP_IN_Min As Double
    Public HP_IN_Max As Double
    Public HP_IN_AV As Double

    Public HP_Out_Min As Double
    Public HP_Out_Max As Double
    Public HP_Out_AV As Double


    Public HP_IN_Min_C As Double
    Public HP_IN_Max_C As Double
    Public HP_IN_AV_C As Double

    Public HP_Out_Min_C As Double
    Public HP_Out_Max_C As Double
    Public HP_Out_AV_C As Double

    Public Result_Voltage As Double
    Public Result_Current As Double
    Public Result_Res As Double

    Public JobInPre As Double
    Public JobOutPre As Double

    Public Set_FillingTime As Integer
    Public Set_Standing As Integer
    Public Set_MeasuringTime As Integer

    Public Set_FillingTime_C As Integer
    Public Set_Standing_C As Integer
    Public Set_MeasuringTime_C As Integer

    Public Act_FillingTime As Integer
    Public Act_Standing As Integer
    Public Act_MeasuringTime As Integer

    Public Act_FillingTime_C As Integer
    Public Act_Standing_C As Integer
    Public Act_MeasuringTime_C As Integer



    Public Result_OK_Count As Double
    Public Result_NG_Count As Double
    Public TotalCount As Double

    Public Coil_Current As Double
    Public Coil_Voltage As Double
    Public Coil_Res As Double
    Public JobInPress As Double
    Public JobOutPress As Double



    Public viewstr As Integer

    Public CoilVolMin As Double
    Public CoilVolMax As Double
    Public CoilCurrentMin As Double
    Public CoilCurrentMax As Double
    Public CoilResMin As Double
    Public CoilResMax As Double
    Public flag As Integer

    Public cmdflagarr(50) As Integer

    Public FinalResult As String

    Public VideoStart As Integer
    Public VideoStop As Integer

    Public flagvideo As Integer


    Public barcodeArr(20) As String
    Public FloatArr(50) As String
    Public IntArr(90) As String
    Public IntArr2(90) As String

    Public Selectmodel(10) As String
    Public modelbardigit(10) As String

    Public Bar_AutoMaual As Integer

    Public stopbarread As Integer

    Public PARR(40) As String

    Public Puch_Button As Integer
    Public Door_Security As Integer

    Public modelarr(10) As Integer


    Public User As String
    Public Auth As Integer

    Public HP_IN_AV_O As Double
    Public HP_Out_AV_O As Double

    Public InputHighPressure As Double
    Public InputHighPressureMin As Double
    Public InputHighPressureMax As Double

    Public KNOBCLOSED As Integer
    Public KNOBOPEN As Integer

    Public Sub MyNonQuery(ByVal HR As String, ByVal Value As Integer, ByVal num As Integer)
        On Error Resume Next
        Dim tx1(11) As Integer
        Dim txstr As String = ""
        tx1(0) = num
        tx1(1) = 0
        tx1(2) = 0
        tx1(3) = 0
        tx1(4) = 0
        tx1(5) = 6

        tx1(6) = 1 'Device Id
        tx1(7) = 6 'Funcation code single res
        tx1(8) = Val(HR) \ 256
        tx1(9) = Val(HR) Mod 256
        tx1(10) = Val(Value) \ 256
        tx1(11) = Val(Value) Mod 256

        For i = 0 To 11
            txstr = txstr + Chr(tx1(i))
        Next
        Form3.AxWinsock1().SendData(txstr)

        TX = ""
        TX = "Tx - "

        For i = 1 To tx1.Length - 1
            TX = TX + CStr((tx1(i)) & " ")
        Next
        Form3.ToolStripStatusLabel1.Text = TX

    End Sub
    Dim tx2(16) As Integer
    Public Sub MyNonQueryFloat(ByVal HR As String, ByVal Value As Double, ByVal num As Integer)
        'Form1.Timer1.Stop()
        On Error Resume Next
        Dim txstr As String = ""
        tx2(0) = num
        tx2(1) = 0
        tx2(2) = 0
        tx2(3) = 0
        tx2(4) = 0
        tx2(5) = 11

        tx2(6) = 1 'Device Id
        tx2(7) = 16 'Funcation code single res
        tx2(8) = Val(HR) \ 256
        tx2(9) = Val(HR) Mod 256

        tx2(10) = 0
        tx2(11) = 2
        tx2(12) = 4

        ConvertFloatToByte(Value, 13)

        For i = 0 To 16
            txstr = txstr + Chr(tx2(i))
        Next
        Dim jj As Integer = Len(txstr)
        Form3.AxWinsock1().SendData(txstr)

        TX = ""
        TX = "Tx - "

        For i = 1 To tx2.Length - 1
            TX = TX + CStr((tx2(i)) & " ")
        Next
        Form3.ToolStripStatusLabel1.Text = TX
        'Form1.Timer1.Start()
    End Sub

    Public Function ConvertFloatToByte(ByVal Value As Double, ByVal pos1 As Integer) 'ByVal Value As Double
        Dim doubleString As String = Value '"0.0500"
        Dim input As Decimal = Convert.ToDecimal(doubleString)
        'Dim flt As Single = CSng(input)
        'Dim bytes As Byte() = BitConverter.GetBytes(flt)
        Dim bytes As Byte() = BitConverter.GetBytes(input)
        'Dim i As Integer = BitConverter.ToInt32(bytes, 0)
        'Dim x As String = i.ToString("X")



        tx2(pos1) = bytes(1)
        tx2(pos1 + 1) = bytes(0)
        tx2(pos1 + 2) = bytes(3)
        tx2(pos1 + 3) = bytes(2)

        Return Nothing
    End Function

    Public repectbarcode As Integer

    Public Function ResultInsert()

        Dim barcode As String = Form3.TextBox1.Text
        Dim Insertstr As String
        If barcode.Length = 13 Then

            Dim sq As String = "SELECT * FROM ResultTB where Barcode = '" & barcode.ToString & "'"

            Dim dt As DataTable = GetData(sq)
            If dt.Rows.Count() > 0 Then
                Dim sr As String = dt.Rows(0).Item("ID")
                'MyNonQuery("698", 1, 34) 

                If repectbarcode = 1 Then

                    Insertstr = "UPDATE `ResultTB` SET  `HP_IN_Min`='" & HP_IN_Min & "',`HP_IN_Max`='" & HP_IN_Max & "',`HP_IN_AV`='" & HP_IN_AV & "', " & _
                                        "`HP_Out_Min`='" & HP_Out_Min & "',`HP_Out_Max`='" & HP_Out_Max & "',`HP_Out_AV`='" & HP_Out_AV & "', " & _
                                        "`HP_IN_Min_C`='" & HP_IN_Min_C & "',`HP_IN_Max_C`='" & HP_IN_Max_C & "',`HP_IN_AV_C`='" & HP_IN_AV_C & "', " & _
                                        "`HP_Out_Min_C`='" & HP_Out_Min_C & "',`HP_Out_Max_C`='" & HP_Out_Max_C & "',`HP_Out_AV_C`='" & HP_Out_AV_C & "', " & _
                                        "`Result_Voltage`='" & Result_Voltage & "',`Result_Current`='" & Result_Current & "',`Result_Res`='" & Result_Res & "'," & _
                                        "`CoilVolMin`='" & CoilVolMin & "',`CoilVolMax`='" & CoilVolMax & "',`CoilCurrentMin`='" & CoilCurrentMin & "', `CoilCurrentMax`='" & CoilCurrentMax & "'," & _
                                        "`CoilResMin`='" & CoilResMin & "',`CoilResMax`='" & CoilResMax & "',`PartSelection`='" & PartSelection & "',`TotalCycleTime`='" & TotalCycleTime & "'," & _
                                        "`Coil_Current`='" & Coil_Current & "', `Coil_Voltage`='" & Coil_Voltage & "',`Coil_Res`='" & Coil_Res & "',`JobInPress`='" & JobInPress & "',`JobOutPress`='" & JobOutPress & "'," & _
                                        "`Set_FillingTime`='" & Set_FillingTime & "', `Set_Standing`='" & Set_Standing & "',`Set_MeasuringTime`='" & Set_MeasuringTime & "'," & _
                                        "`Act_FillingTime`='" & Act_FillingTime & "', `Act_Standing`='" & Act_Standing & "',`Act_MeasuringTime`='" & Act_MeasuringTime & "'," & _
                                        "`Result_OK_Count`='" & Result_OK_Count & "', `Result_NG_Count`='" & Result_NG_Count & "',`TotalCount`='" & TotalCount & "',`Result`='" & FinalResult & "', `User`='" & User & "',`Auth`='" & Auth & "'," & _
                                        "`HP_IN_AV_O`='" & HP_IN_AV_O & "', `HP_Out_AV_O`='" & HP_Out_AV_O & "',`InputHighPressure`='" & InputHighPressure & "', `KNOBCLOSED`='" & KNOBCLOSED & "',`KNOBOPEN`='" & KNOBOPEN & "',`DateTime1`=now() WHERE Barcode= '" & barcode & "'"


                    InsertQuery(Insertstr)

                End If

            Else

                Insertstr = "INSERT INTO `ResultTB` " & _
                            "(`Barcode`, `HP_IN_Min`,`HP_IN_Max`,`HP_IN_AV`, " & _
                                        "`HP_Out_Min`,`HP_Out_Max`,`HP_Out_AV`, " & _
                                        "`HP_IN_Min_C`,`HP_IN_Max_C`,`HP_IN_AV_C`, " & _
                                        "`HP_Out_Min_C`,`HP_Out_Max_C`,`HP_Out_AV_C`, " & _
                                        "`Result_Voltage`,`Result_Current`,`Result_Res`," & _
                                        "`CoilVolMin`,`CoilVolMax`,`CoilCurrentMin`, `CoilCurrentMax`," & _
                                        "`CoilResMin`,`CoilResMax`,`PartSelection`,`TotalCycleTime`," & _
                                        "`Coil_Current`, `Coil_Voltage`,`Coil_Res`,`JobInPress`,`JobOutPress`," & _
                                        "`Set_FillingTime`, `Set_Standing`,`Set_MeasuringTime`," & _
                                        "`Act_FillingTime`, `Act_Standing`,`Act_MeasuringTime`," & _
                                        "`Result_OK_Count`, `Result_NG_Count`,`TotalCount`,`Result`,`User`,`Auth`," & _
                                        "`HP_IN_AV_O`, `HP_Out_AV_O`,`InputHighPressure`, `KNOBCLOSED`,`KNOBOPEN`" & _
                             ") VALUES (" & _
                             "'" & barcode & "', '" & HP_IN_Min & "' ,'" & HP_IN_Max & "', '" & HP_IN_AV & "' , " & _
                                        "'" & HP_Out_Min & "','" & HP_Out_Max & "','" & HP_Out_AV & "', " & _
                                        "'" & HP_IN_Min_C & "' ,'" & HP_IN_Max_C & "', '" & HP_IN_AV_C & "' , " & _
                                        "'" & HP_Out_Min_C & "','" & HP_Out_Max_C & "','" & HP_Out_AV_C & "', " & _
                                        "'" & Result_Voltage & "','" & Result_Current & "','" & Result_Res & "'," & _
                                        "'" & CoilVolMin & "','" & CoilVolMax & "','" & CoilCurrentMin & "', '" & CoilCurrentMax & "'," & _
                                        "'" & CoilResMin & "' ,'" & CoilResMax & "','" & PartSelection & "','" & TotalCycleTime & "'," & _
                                        "'" & Coil_Current & "', '" & Coil_Voltage & "','" & Coil_Res & "','" & JobInPress & "','" & JobOutPress & "'," & _
                                        "'" & Set_FillingTime & "', '" & Set_Standing & "','" & Set_MeasuringTime & "'," & _
                                        "'" & Act_FillingTime & "', '" & Act_Standing & "','" & Act_MeasuringTime & "'," & _
                                        "'" & Result_OK_Count & "', '" & Result_NG_Count & "','" & TotalCount & "','" & FinalResult & "','" & User & "','" & Auth & "'," & _
                                        "'" & HP_IN_AV_O & "', '" & HP_Out_AV_O & "','" & InputHighPressure & "', '" & KNOBCLOSED & "','" & KNOBOPEN & "')"
                InsertQuery(Insertstr)

            End If
        End If



        Return Nothing
    End Function
    Public Function AlertInsert()
        Dim Insertstr As String
        Insertstr = "INSERT INTO `AlertReport` (`Alert`, `Color`) VALUES('" & AlertMsg(viewstr) & "', '" & AlertColor(viewstr) & "')"
        InsertQuery(Insertstr)
        Return Nothing
    End Function


    'create new DataTable object to hold results
    Public Function GetData(ByVal query As String) As DataTable
        Dim cmd As OleDb.OleDbCommand = New OleDb.OleDbCommand(query)
        Using sda As OleDb.OleDbDataAdapter = New OleDb.OleDbDataAdapter()
            cmd.Connection = conn
            sda.SelectCommand = cmd
            Using dt As DataTable = New DataTable()
                sda.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

    'insert funcation
    Public Sub InsertQuery(ByVal SQCommand As String)
        Dim SQLCMD As New OleDb.OleDbCommand(SQCommand, conn)
        conn.Open()
        SQLCMD.ExecuteNonQuery()
        conn.Close()
    End Sub

    Public Sub CHeckFuncation(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Dim numbers As Windows.Forms.TextBox = sender
        If InStr("1234567890.", e.KeyChar) = 0 And Asc(e.KeyChar) <> 8 Or (e.KeyChar = "." And InStr(numbers.Text, ".") > 0) Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If

    End Sub

    Public Sub partselectarr()

        conn.Open()
        Dim cmd As New OleDb.OleDbCommand("SELECT * FROM `AdminSettingTB`", conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader

        While rd.Read

            Selectmodel(rd("ModelValue")) = rd("ModelName").ToString
            modelbardigit(rd("ModelValue")) = rd("ModelBardigit").ToString

        End While
        rd.Close()
        conn.Close()

    End Sub

    Public AlertMsg(100) As String
    Public AlertColor(100) As String
    Public AlertEam(100) As String
    Public OldAlert As Integer
    Public Sub AlertFuncation()

        conn.Open()
        Dim cmd As New OleDb.OleDbCommand("SELECT * FROM `Alert`", conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader

        While rd.Read

            AlertMsg(rd("Sequence")) = rd("Alert")
            AlertColor(rd("Sequence")) = rd("Color").ToString
            AlertEam(rd("Sequence")) = rd("Option").ToString

        End While
        rd.Close()
        conn.Close()

    End Sub

    Public R_Name(100) As String
    Public R_Address(100) As String
    Public Sub ReadIO()

        conn.Open()
        Dim cmd As New OleDb.OleDbCommand("SELECT * FROM `IO_TB`", conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader
        Dim x As Integer
        x = 0
        While rd.Read

            R_Name(x) = rd("Register_Address").ToString
            R_Address(x) = rd("Register_Name").ToString
            x = x + 1

        End While
        rd.Close()
        conn.Close()

    End Sub


    Public clrConverter As New ColorConverter

    Public flagPart As Integer
    Public flagPartok As Integer

    Public txtclear As Integer

    Public D685 As Integer
    Public D686 As Integer
    Public D687 As Integer
    Public D688 As Integer
    Public D689 As Integer
    Public D690 As Integer
    Public D691 As Integer
    Public D692 As Integer
    Public D695 As Integer
    Public D696 As Integer
    Public D697 As Integer



    Public C_D614 As Double
    Public C_D658 As Integer
    Public C_D721 As Integer
    Public C_D725 As Integer
    Public C_D723 As Integer
    Public C_D659 As Integer
    Public C_D722 As Integer
    Public C_D655 As Integer
    Public C_D604 As Double
    Public C_D657 As Integer
    Public C_D656 As Integer
    Public C_D586 As Double
    Public C_D724 As Integer


    Public D728 As Integer
    Public D730 As Integer
    Public D729 As Integer
    Public D731 As Integer

    Public Sub CSVSAVE()
        Dim barcode As String = Form3.TextBox1.Text
        Dim GridHeader(35) As String

        GridHeader(0) = "SR NO"
        GridHeader(1) = "DATE"
        GridHeader(2) = "TIME"
        GridHeader(3) = "OPERATOR NAME"
        GridHeader(4) = "PART NAME"
        GridHeader(5) = "BARCODE"

        GridHeader(6) = "INTERNAL LEAK JOB HP IN PRE MIN"
        GridHeader(7) = "INTERNAL LEAK JOB HP IN PRE MAX"
        GridHeader(8) = "INTERNAL LEAK KNOB CLOSED JOB HP IN PRE RESULT"
        GridHeader(9) = "INTERNAL LEAK KNOB OPEN  JOB HP IN PRE RESULT"

        GridHeader(10) = "INTERNAL LEAK JOB HP OUT PRE MIN"
        GridHeader(11) = "INTERNAL LEAK JOB HP OUT PRE MAX"
        GridHeader(12) = "INTERNAL LEAK KNOB CLOSED JOB HP OUT PRE RESULT"
        GridHeader(13) = "INTERNAL LEAK KNOB OPEN  JOB HP OUT PRE RESULT"

        GridHeader(14) = "INTERNAL LEAK KNOB CLOSED ON/OFF" ' 1-ON, 2 -OFF"  ' 668  KNOBCLOSED
        GridHeader(15) = "INTERNAL LEAK KNOB OPEN  ON/OFF" ' 1-ON, 2 -OFF" '669     KNOBOPEN

        GridHeader(16) = "EXTERNAL LEAK JOB HP IN PRE MIN"
        GridHeader(17) = "EXTERNAL LEAK JOB HP IN PRE MAX"
        GridHeader(18) = "EXTERNAL LEAK JOB HP IN PRE RESULT"

        GridHeader(19) = "EXTERNAL LEAK JOB HP OUT PRE MIN"
        GridHeader(20) = "EXTERNAL LEAK JOB HP IN PRE MAX"
        GridHeader(21) = "EXTERNAL LEAK JOB HP OUT PRE RESULT"

        GridHeader(22) = "COIL VOLTAGE MIN"
        GridHeader(23) = "COIL VOLTAGE MAX"
        GridHeader(24) = "COIL VOLTAGE RESULT"
        GridHeader(25) = "COIL CURRENT MIN"
        GridHeader(26) = "COIL CURRENT MAX"
        GridHeader(27) = "COIL CURRENT RESULT"
        GridHeader(28) = "COIL RESISTENCE MIN"
        GridHeader(29) = "COIL RESISTENCE MAX"
        GridHeader(30) = "COIL RESISTENCE RESULT"
        GridHeader(31) = "RESULT"
        GridHeader(32) = "NO OF PARTS OK"
        GridHeader(33) = "NO OF PARTS NG"
        GridHeader(34) = "NO OF PARTS TOTAL"

        Dim GridValue(35) As String

        GridValue(0) = ""
        GridValue(1) = DateTime.Now.ToString("dd-MM-yyy") 'DATE
        GridValue(2) = DateTime.Now.ToString("hh:mm:ss tt")  'TIME
        GridValue(3) = User
        GridValue(4) = PartSelection
        GridValue(5) = barcode 'BARCODE

        GridValue(6) = HP_IN_Min 'hP in 
        GridValue(7) = HP_IN_Max
        GridValue(8) = HP_IN_AV
        GridValue(9) = HP_IN_AV_O

        GridValue(10) = HP_Out_Min 'hP in"
        GridValue(11) = HP_Out_Max
        GridValue(12) = HP_Out_AV
        GridValue(13) = HP_Out_AV_O

        Dim KC As String
        If KNOBCLOSED = "1" Then
            KC = "ON"
        ElseIf KNOBCLOSED = "2" Then
            KC = "OFF"
        Else
            KC = "" 'rd("Result").ToString
        End If
        Dim KO As String
        If KNOBOPEN = "1" Then
            KO = "ON"
        ElseIf KNOBOPEN = "2" Then
            KO = "OFF"
        Else
            KO = "" 'rd("Result").ToString
        End If

        GridValue(14) = KC 'rd("KNOBCLOSED").ToString
        GridValue(15) = KO ' rd("KNOBOPEN").ToString


        GridValue(16) = HP_IN_Min_C 'hP in  C
        GridValue(17) = HP_IN_Max_C
        GridValue(18) = HP_IN_AV_C

        GridValue(19) = HP_Out_Min_C 'hP OUT C
        GridValue(20) = HP_Out_Max_C
        GridValue(21) = HP_Out_AV_C




        GridValue(22) = CoilVolMin 'cOIL voL
        GridValue(23) = CoilVolMax
        GridValue(24) = Result_Voltage

        GridValue(25) = CoilCurrentMin 'COIL CURRENT
        GridValue(26) = CoilCurrentMax
        GridValue(27) = Result_Current

        GridValue(28) = CoilResMin 'COIL RES
        GridValue(29) = CoilResMax
        GridValue(30) = Result_Res

        Dim OkNG As String
        If FinalResult = "1" Then
            OkNG = "OK"
        ElseIf FinalResult = "2" Then
            OkNG = "NG"
        Else
            OkNG = "" 'rd("Result").ToString
        End If

        GridValue(31) = OkNG 'OK/NG
        GridValue(32) = Result_OK_Count 'OK
        GridValue(33) = Result_NG_Count 'NG
        GridValue(34) = TotalCount 'TOTAL


        Dim Header As String = "" '= "Heder"
        Dim DataString As String = "" '= "one"
        For i = 0 To GridHeader.Length - 1 - 1
            'DataGridView1.Columns(i).Name = GridHeader(i)
            'DataGridView1.Columns(i).HeaderText = GridHeader(i)
            ''DataGridView1.Columns(i).DataPropertyName = "SerialNo"

            Header = Header + GridHeader(i) & ", "
            DataString = DataString + GridValue(i) & ", "
        Next



        'CREATE FOLDER AND CSV Andon 
        Dim folderPath As String = "C:\Reports\" + DateTime.Now.ToString("yyyy") + "\" + DateTime.Now.ToString("MM") + "\"
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)

        Else
            Dim Exists_File As String = folderPath + CStr(DateTime.Now.ToString("dd-MM-yyy")) + ".csv"
            If Not File.Exists(Exists_File) Then 'IF CSV FILE EXISTS THEN DATA ADD
                'My.Computer.FileSystem.WriteAllText(Exists_File, "LogTime, Fixture Name, Serial No, Vacuum Test Leak Rate, Pressure Test Leak Rate, Result" & vbCrLf & "", False) 'add
                My.Computer.FileSystem.WriteAllText(Exists_File, Header & vbCrLf & DataString & "" & vbCrLf & "", False) 'add
            Else
                File.AppendAllText(Exists_File, DataString & vbCrLf & "")

            End If
        End If

    End Sub

End Module
