Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel
Imports System.Text

Public Class Form2
    Dim SR As Integer
    Dim query As String
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

       

        Cursor.Current = Cursors.WaitCursor
        DataGridView1.Rows.Clear()
        Label5.Text = "Please wait..."
       
        Button1.Enabled = False
        Dim fromdt As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim todt As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")

        Dim BarcodeR As String = ""
        Dim JOBINHPMINR As String = ""
        Dim JOBINHPMAXR As String = ""
        Dim JOBINHPACTR As String = ""
        Dim JOBOUTHPMINR As String = ""
        Dim JOBOUTHPMAXR As String = ""
        Dim JOBOUTHPACTR As String = ""
        Dim sr As Integer

        If ComboBox1.SelectedIndex = 0 Then
            query = "SELECT * FROM `ResultTB` WHERE  DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"
        ElseIf ComboBox1.SelectedIndex = 1 Then
            query = "SELECT * FROM `ResultTB` WHERE Result = '1' and  DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"
        ElseIf ComboBox1.SelectedIndex = 2 Then
            query = "SELECT * FROM `ResultTB` WHERE Result = '2' and  DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"
        End If



        System.Windows.Forms.Application.DoEvents()
        conn.Open()
        Dim cmd As New OleDb.OleDbCommand(query, conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader
        sr = 1
        While rd.Read

            GridValue(0) = sr


            GridValue(1) = Date.Parse(rd("DateTime1").ToString).ToString("yyyy-MM-dd") 'DATE
            GridValue(2) = Date.Parse(rd("DateTime1").ToString).ToString("hh:mm:ss tt")  'TIME
            GridValue(3) = rd("User").ToString
            If Len(rd(23).ToString) <> 0 Then
                GridValue(4) = Selectmodel(rd("PartSelection")) 'SELECT PART
            Else
                GridValue(4) = rd("PartSelection").ToString
            End If
            GridValue(5) = rd("Barcode").ToString 'BARCODE

            GridValue(6) = rd("HP_IN_Min").ToString 'hP in 
            GridValue(7) = rd("HP_IN_Max").ToString
            GridValue(8) = rd("HP_IN_AV").ToString
            GridValue(9) = rd("HP_IN_AV_O").ToString

            GridValue(10) = rd("HP_Out_Min").ToString 'hP in
            GridValue(11) = rd("HP_Out_Max").ToString
            GridValue(12) = rd("HP_Out_AV").ToString
            GridValue(13) = rd("HP_Out_AV_O").ToString

            Dim KC As String
            If rd("KNOBCLOSED").ToString = "1" Then
                KC = "ON"
            ElseIf rd("KNOBCLOSED").ToString = "2" Then
                KC = "OFF"
            Else
                KC = "" 'rd("Result").ToString
            End If
            Dim KO As String
            If rd("KNOBOPEN").ToString = "1" Then
                KO = "ON"
            ElseIf rd("KNOBOPEN").ToString = "2" Then
                KO = "OFF"
            Else
                KO = "" 'rd("Result").ToString
            End If

            GridValue(14) = KC 'rd("KNOBCLOSED").ToString
            GridValue(15) = KO ' rd("KNOBOPEN").ToString


            GridValue(16) = rd("HP_IN_Min_C").ToString 'hP in  C
            GridValue(17) = rd("HP_IN_Max_C").ToString
            GridValue(18) = rd("HP_IN_AV_C").ToString

            GridValue(19) = rd("HP_Out_Min_C").ToString 'hP OUT C
            GridValue(20) = rd("HP_Out_Max_C").ToString
            GridValue(21) = rd("HP_Out_AV_C").ToString




            GridValue(22) = rd("CoilVolMin").ToString 'cOIL voL
            GridValue(23) = rd("CoilVolMax").ToString
            GridValue(24) = rd("Result_Voltage").ToString

            GridValue(25) = rd("CoilCurrentMin").ToString 'COIL CURRENT
            GridValue(26) = rd("CoilCurrentMax").ToString
            GridValue(27) = rd("Result_Current").ToString

            GridValue(28) = rd("CoilResMin").ToString 'COIL RES
            GridValue(29) = rd("CoilResMax").ToString
            GridValue(30) = rd("Result_Res").ToString

            Dim OkNG As String
            If rd("Result").ToString = "1" Then
                OkNG = "OK"
            ElseIf rd("Result").ToString = "2" Then
                OkNG = "NG"
            Else
                OkNG = "" 'rd("Result").ToString
            End If

            GridValue(31) = OkNG 'OK/NG
            GridValue(32) = rd("Result_OK_Count").ToString 'OK
            GridValue(33) = rd("Result_NG_Count").ToString 'NG
            GridValue(34) = rd("TotalCount").ToString 'TOTAL


            DataGridView1.Rows.Add(GridValue)
            'table.Rows.Add(GridValue)


            sr = sr + 1

        End While
        rd.Close()
        conn.Close()
        Label5.Text = ""
        Button1.Enabled = True
        Button2.Enabled = True

        Timer1.Stop()

    End Sub
    Dim table As DataTable
    Dim GridHeader(35) As String
    Dim GridHeader2(35) As String
    Dim GridValue(35) As String
    Dim names() As String = {"KNOB OPEN / INTERNAL LEAK", "KNOB CLOSED / EXTERNAL LEAK ", ""}
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        partselectarr()

        ComboBox1.SelectedIndex = 0


        Button2.Enabled = False

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


        'Dim Grid1() As String = {"SR NO", "DATE", "TIME", "PART NAME", "BARCODE", "KNOB OPEN / INTERNAL LEAK", "KNOB CLOSED / EXTERNAL LEAK "}
        'Dim Grid2() As String = {"SR NO", "DATE", "TIME", "PART NAME", "BARCODE"\\
        'Dim Grid3() As String = {"SR NO", "DATE", "TIME", "PART NAME", "BARCODE"}

        GridHeader2(0) = "MIN"
        GridHeader2(1) = "MAX"
        GridHeader2(2) = "RESULT"
        GridHeader2(3) = "MIN"
        GridHeader2(4) = "MAX"
        GridHeader2(5) = "RESULT"
        GridHeader2(6) = "MIN"
        GridHeader2(7) = "MAX"
        GridHeader2(8) = "RESULT"
        GridHeader2(9) = "MIN"
        GridHeader2(10) = "MAX"
        GridHeader2(11) = "RESULT"
        GridHeader2(12) = "MIN"
        GridHeader2(13) = "MAX"
        GridHeader2(14) = "RESULT"
        GridHeader2(15) = "OK"
        GridHeader2(16) = "OK"
        GridHeader2(17) = "NG"
        GridHeader2(18) = "TOTAL"


        DataGridView1.ColumnCount = CInt(GridHeader.Length - 1)



        For i = 0 To DataGridView1.ColumnCount - 1
            DataGridView1.Columns(i).Name = GridHeader(i)
            DataGridView1.Columns(i).HeaderText = GridHeader(i)
            'DataGridView1.Columns(i).DataPropertyName = "SerialNo"


        Next

        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridView1.ColumnHeadersHeight = DataGridView1.ColumnHeadersHeight * 2
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter




        'DataGridView1.Columns(0).Width = 130
        'DataGridView1.Columns(1).Width = 110
        'DataGridView1.Columns(2).Width = 120
        'DataGridView1.Columns(3).Width = 110
        'DataGridView1.Columns(4).Width = 110
        'DataGridView1.Columns(5).Width = 120
        'DataGridView1.Columns(6).Width = 140
        'DataGridView1.Columns(7).Width = 140
        'DataGridView1.Columns(8).Width = 140
        'DataGridView1.Columns(9).Width = 80
        'DataGridView1.Columns(10).Width = 130

    End Sub
    Dim count As Integer = 0
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Button2.Enabled = False
        Label5.Text = "Please wait.."
        ReadCSV()
        Button2.Enabled = True

        'Cursor.Current = Cursors.WaitCursor



        'Button2.Enabled = False
        'Label5.Text = "Please wait.."
        'Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        'Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        'Dim misValue As Object = System.Reflection.Missing.Value
        '' Dim i As Integer
        '' Dim j As Integer
        'Dim xlApp As Excel.Application
        'xlApp = New Microsoft.Office.Interop.Excel.Application
        'xlWorkBook = xlApp.Workbooks.Add(misValue)
        'xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        'xlWorkSheet.Name = ("Reports")
        'xlWorkSheet.Columns.ColumnWidth = 12


        ''Dim lastrow As Excel.Range = xlApp.Rows.End(XlDirection.xlToLeft)
        ''Dim findme As Range = ExcelApp.Range("A1:E" & lastrow.Row)

        'Dim rangeToFormat As Excel.Range




        'rangeToFormat = xlWorkSheet.Range("A1", "AI1")
        'rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Blue)
        'rangeToFormat.Borders.Color = Color.Black

        'rangeToFormat.HorizontalAlignment = 3
        'rangeToFormat.VerticalAlignment = 2
        'rangeToFormat.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
        'rangeToFormat.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        'rangeToFormat.Font.Size = 10
        'rangeToFormat.Font.Bold = True
        'rangeToFormat.Rows("1:1").rowheight = 40
        'rangeToFormat.Rows.WrapText = True






        'For Each col As DataGridViewColumn In DataGridView1.Columns
        '    xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText
        '    For Each rowa As DataGridViewRow In DataGridView1.Rows
        '        xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1) = rowa.Cells(col.Index).Value

        '    Next
        'Next




        'xlWorkSheet.Columns.AutoFit()

        'Dim filename As String

        ''filename = "Material Reports" + DateTimePicker1.Value.ToString("MM dd yyyy") + " To" + DateTimePicker2.Value.ToString("MM dd yyyy") + DateTime.Now.ToString("ddMMyyyy HHmm ss") + ".xls"

        'filename = "Date " + DateTimePicker1.Value.ToString("MM dd yyyy") + " To " + DateTimePicker2.Value.ToString("MM dd yyyy") + " Reports " + DateTime.Now.ToString("dd MM yyyy HHmmss") + ".xls"
        'Dim folderPath As String = "C:\Reports\"
        'If Not Directory.Exists(folderPath) Then
        '    Directory.CreateDirectory(folderPath)
        'End If

        'xlWorkBook.SaveAs("C:\Reports\" + filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
        '    Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        'xlWorkBook.Close(True, misValue, misValue)
        'xlApp.Quit()
        'releaseObject(xlApp)
        'releaseObject(xlWorkBook)
        'releaseObject(xlWorkSheet)
        ''MsgBox("File Path : " & folderPath & vbCr & " Export Successfully", MsgBoxStyle.Information, Title:="Export in Excel")



        'Label5.Text = "File Path : " & folderPath + filename & vbCr & " Export Successfully"
        'Button2.Enabled = True
    End Sub
    Private Sub DataTableToExcel(dt As DataTable, ws As Excel.Worksheet, TabName As String)
        Dim arr(dt.Rows.Count, dt.Columns.Count) As Object
        Dim r As Int32, c As Int32
        'copy the datatable to an array
        For r = 0 To dt.Rows.Count - 1
            For c = 0 To dt.Columns.Count - 1
                arr(r, c) = dt.Rows(r).Item(c)
            Next
        Next

        ws.Name = TabName   'name the worksheet
        'add the column headers starting in A1
        c = 0
        For Each column As DataColumn In dt.Columns
            ws.Cells(1, c + 1) = column.ColumnName
            c += 1
        Next
        'add the data starting in cell A2
        ws.Range(ws.Cells(2, 1), ws.Cells(dt.Rows.Count, dt.Columns.Count)).Value = arr
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs)
        
    End Sub

    
    Private Sub ReadCSV()

        Dim filename As String

        filename = "Date " + DateTimePicker1.Value.ToString("MM dd yyyy") + " To " + DateTimePicker2.Value.ToString("MM dd yyyy") + " Reports " + DateTime.Now.ToString("dd MM yyyy HHmmss") + ".csv"
        Dim folderPath As String = "C:\Reports\"
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        Dim headers = (From header As DataGridViewColumn In DataGridView1.Columns.Cast(Of DataGridViewColumn)() _
               Select header.HeaderText).ToArray
        Dim rows = From row As DataGridViewRow In DataGridView1.Rows.Cast(Of DataGridViewRow)() _
                   Where Not row.IsNewRow _
                   Select Array.ConvertAll(row.Cells.Cast(Of DataGridViewCell).ToArray, Function(c) If(c.Value IsNot Nothing, c.Value.ToString, ""))
        Using sw As New System.IO.StreamWriter(folderPath & filename)
            sw.WriteLine(String.Join(",", headers))
            For Each r In rows
                sw.WriteLine(String.Join(",", r))
            Next
        End Using
        Label5.Text = "File Path : " & folderPath + filename & vbCr & " Export Successfully"
    End Sub
   

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        DataGridView1.Rows.Clear()

        Dim fromdt As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim todt As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")

        Dim BarcodeR As String = ""
        Dim JOBINHPMINR As String = ""
        Dim JOBINHPMAXR As String = ""
        Dim JOBINHPACTR As String = ""
        Dim JOBOUTHPMINR As String = ""
        Dim JOBOUTHPMAXR As String = ""
        Dim JOBOUTHPACTR As String = ""
        Dim sr As Integer

        'Dim query As String = "SELECT * FROM `ResultTB` WHERE Barcode like '%" & TextBox1.Text & "%'" 'and DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"

        Dim query As String = "SELECT * FROM `ResultTB` WHERE Barcode = '" & TextBox1.Text & "'" 'and DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"
        conn.Open()
        Dim cmd As New OleDb.OleDbCommand(query, conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader
        sr = 1
        While rd.Read

            GridValue(0) = sr


            GridValue(1) = Date.Parse(rd("DateTime1").ToString).ToString("yyyy-MM-dd") 'DATE
            GridValue(2) = Date.Parse(rd("DateTime1").ToString).ToString("hh:mm:ss tt")  'TIME
            GridValue(3) = rd("User").ToString
            If Len(rd(23).ToString) <> 0 Then
                GridValue(4) = Selectmodel(rd("PartSelection")) 'SELECT PART
            Else
                GridValue(4) = rd("PartSelection").ToString
            End If
            GridValue(5) = rd("Barcode").ToString 'BARCODE

            GridValue(6) = rd("HP_IN_Min").ToString 'hP in 
            GridValue(7) = rd("HP_IN_Max").ToString
            GridValue(8) = rd("HP_IN_AV").ToString
            GridValue(9) = rd("HP_IN_AV_O").ToString

            GridValue(10) = rd("HP_Out_Min").ToString 'hP in
            GridValue(11) = rd("HP_Out_Max").ToString
            GridValue(12) = rd("HP_Out_AV").ToString
            GridValue(13) = rd("HP_Out_AV_O").ToString

            Dim KC As String
            If rd("KNOBCLOSED").ToString = "1" Then
                KC = "ON"
            ElseIf rd("KNOBCLOSED").ToString = "2" Then
                KC = "OFF"
            Else
                KC = "" 'rd("Result").ToString
            End If
            Dim KO As String
            If rd("KNOBOPEN").ToString = "1" Then
                KO = "ON"
            ElseIf rd("KNOBOPEN").ToString = "2" Then
                KO = "OFF"
            Else
                KO = "" 'rd("Result").ToString
            End If

            GridValue(14) = KC 'rd("KNOBCLOSED").ToString
            GridValue(15) = KO ' rd("KNOBOPEN").ToString


            GridValue(16) = rd("HP_IN_Min_C").ToString 'hP in  C
            GridValue(17) = rd("HP_IN_Max_C").ToString
            GridValue(18) = rd("HP_IN_AV_C").ToString

            GridValue(19) = rd("HP_Out_Min_C").ToString 'hP OUT C
            GridValue(20) = rd("HP_Out_Max_C").ToString
            GridValue(21) = rd("HP_Out_AV_C").ToString




            GridValue(22) = rd("CoilVolMin").ToString 'cOIL voL
            GridValue(23) = rd("CoilVolMax").ToString
            GridValue(24) = rd("Result_Voltage").ToString

            GridValue(25) = rd("CoilCurrentMin").ToString 'COIL CURRENT
            GridValue(26) = rd("CoilCurrentMax").ToString
            GridValue(27) = rd("Result_Current").ToString

            GridValue(28) = rd("CoilResMin").ToString 'COIL RES
            GridValue(29) = rd("CoilResMax").ToString
            GridValue(30) = rd("Result_Res").ToString

            Dim OkNG As String
            If rd("Result").ToString = "1" Then
                OkNG = "OK"
            ElseIf rd("Result").ToString = "2" Then
                OkNG = "NG"
            Else
                OkNG = "" 'rd("Result").ToString
            End If

            GridValue(31) = OkNG 'OK/NG
            GridValue(32) = rd("Result_OK_Count").ToString 'OK
            GridValue(33) = rd("Result_NG_Count").ToString 'NG
            GridValue(34) = rd("TotalCount").ToString 'TOTAL


            DataGridView1.Rows.Add(GridValue)

            sr = sr + 1
        End While
        rd.Close()
        conn.Close()
        Button2.Enabled = True
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class

