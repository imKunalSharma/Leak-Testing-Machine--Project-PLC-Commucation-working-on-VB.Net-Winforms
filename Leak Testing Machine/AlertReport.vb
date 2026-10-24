Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel
Public Class AlertReport

    Private Sub AlertReport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        DataGridView1.ColumnCount = 5

        DataGridView1.Columns(0).Name = "SrNO"
        DataGridView1.Columns(0).HeaderText = "Sr No"

        DataGridView1.Columns(1).Name = "Date"
        DataGridView1.Columns(1).HeaderText = "Date"

        DataGridView1.Columns(2).Name = "Time"
        DataGridView1.Columns(2).HeaderText = "Time"

        DataGridView1.Columns(3).Name = "Alert"
        DataGridView1.Columns(3).HeaderText = "Alert"

        DataGridView1.Columns(4).Name = "Color"
        DataGridView1.Columns(4).HeaderText = "Color"

        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        DataGridView1.ColumnHeadersHeight = DataGridView1.ColumnHeadersHeight
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter

        DataGridView1.Columns(0).Width = 80
        DataGridView1.Columns(1).Width = 120
        DataGridView1.Columns(2).Width = 120
        DataGridView1.Columns(3).Width = 550
        DataGridView1.Columns(4).Width = 100

    End Sub
    Dim sr As Integer
    Dim GridValue(5) As String
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()
        Dim fromdt As String = DateTimePicker1.Value.ToString("yyyy-MM-dd")
        Dim todt As String = DateTimePicker2.Value.ToString("yyyy-MM-dd")

        Dim query As String = "SELECT * FROM `AlertReport` WHERE DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"


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
            GridValue(3) = rd("Alert").ToString
            GridValue(4) = rd("Color").ToString

            DataGridView1.Rows.Add(GridValue)

            sr = sr + 1
        End While
        rd.Close()
        conn.Close()

        Button2.Enabled = True


    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim xlWorkBook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        ' Dim i As Integer
        ' Dim j As Integer
        Dim xlApp As Excel.Application
        xlApp = New Microsoft.Office.Interop.Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        xlWorkSheet = CType(xlWorkBook.Sheets(1), Excel.Worksheet)
        xlWorkSheet.Name = ("Alert Reports")
        'xlWorkSheet.Columns.ColumnWidth = 12


        'Dim lastrow As Excel.Range = xlApp.Rows.End(XlDirection.xlToLeft)
        'Dim findme As Range = ExcelApp.Range("A1:E" & lastrow.Row)

        Dim rangeToFormat As Excel.Range


        'rangeToFormat = xlWorkSheet.Range("A1", "E")
        'rangeToFormat.MergeCells = True
        'rangeToFormat.FormulaR1C1 = "All Material Reports"
        'rangeToFormat.HorizontalAlignment = 3
        'rangeToFormat.VerticalAlignment = 3
        'rangeToFormat.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
        'rangeToFormat.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        'rangeToFormat.Font.Size = 20


        rangeToFormat = xlWorkSheet.Range("A1", "E1")
        rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Blue)
        rangeToFormat.Borders.Color = Color.Black

        rangeToFormat.HorizontalAlignment = 3
        rangeToFormat.VerticalAlignment = 2
        rangeToFormat.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
        rangeToFormat.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        rangeToFormat.Font.Size = 10
        rangeToFormat.Font.Bold = True
        'rangeToFormat.Rows("1:1").rowheight = 80
        rangeToFormat.Rows.WrapText = True






        For Each col As DataGridViewColumn In DataGridView1.Columns
            xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText
            For Each rowa As DataGridViewRow In DataGridView1.Rows
                xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1) = rowa.Cells(col.Index).Value
            Next
        Next
        xlWorkSheet.Columns.AutoFit()

        Dim filename As String

        'filename = "Material Reports" + DateTimePicker1.Value.ToString("MM dd yyyy") + " To" + DateTimePicker2.Value.ToString("MM dd yyyy") + DateTime.Now.ToString("ddMMyyyy HHmm ss") + ".xls"

        filename = "Alert Report Date " + DateTimePicker1.Value.ToString("MM dd yyyy") + " To " + DateTimePicker2.Value.ToString("MM dd yyyy") + " Reports " + DateTime.Now.ToString("dd MM yyyy HHmmss") + ".xls"
        Dim folderPath As String = "C:\Reports\"
        If Not Directory.Exists(folderPath) Then
            Directory.CreateDirectory(folderPath)
        End If

        xlWorkBook.SaveAs("C:\Reports\" + filename, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, _
            Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
        xlWorkBook.Close(True, misValue, misValue)
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
        MsgBox("File Path : " & folderPath & vbCr & " Export Successfully", MsgBoxStyle.Information, Title:="Export in Excel")

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

End Class