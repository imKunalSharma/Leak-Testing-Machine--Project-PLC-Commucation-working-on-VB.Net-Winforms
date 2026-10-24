Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Report

    Private Sub Report_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        FxMSFlexGrid1Format()
    End Sub

    Public Function FxMSFlexGrid1Format()

        AxMSFlexGrid1.MergeCells = MSFlexGridLib.MergeCellsSettings.flexMergeRestrictColumns
        AxMSFlexGrid1.set_TextMatrix(0, 0, "SR No")
        AxMSFlexGrid1.set_TextMatrix(1, 0, "SR No")
        AxMSFlexGrid1.set_TextMatrix(2, 0, "SR No")
        AxMSFlexGrid1.set_MergeCol(0, True)

        AxMSFlexGrid1.set_TextMatrix(0, 1, "DATE1")
        AxMSFlexGrid1.set_TextMatrix(1, 1, "DATE1")
        AxMSFlexGrid1.set_TextMatrix(2, 1, "DATE1")
        AxMSFlexGrid1.set_MergeCol(1, True)
        AxMSFlexGrid1.set_TextMatrix(0, 2, "TIME1")
        AxMSFlexGrid1.set_TextMatrix(1, 2, "TIME1")
        AxMSFlexGrid1.set_TextMatrix(2, 2, "TIME1")
        AxMSFlexGrid1.set_MergeCol(2, True)
        AxMSFlexGrid1.set_TextMatrix(0, 3, "PART NAME")
        AxMSFlexGrid1.set_TextMatrix(1, 3, "PART NAME")
        AxMSFlexGrid1.set_TextMatrix(2, 3, "PART NAME")
        AxMSFlexGrid1.set_MergeCol(3, True)
        AxMSFlexGrid1.set_TextMatrix(0, 4, "BARCODE")
        AxMSFlexGrid1.set_TextMatrix(1, 4, "BARCODE")
        AxMSFlexGrid1.set_TextMatrix(2, 4, "BARCODE")
        AxMSFlexGrid1.set_MergeCol(4, True)


        AxMSFlexGrid1.set_TextMatrix(0, 5, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_TextMatrix(0, 6, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_TextMatrix(0, 7, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_TextMatrix(0, 8, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_TextMatrix(0, 9, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_TextMatrix(0, 10, "KNOB OPEN / INTERNAL LEAK")
        AxMSFlexGrid1.set_MergeRow(0, True)

        AxMSFlexGrid1.set_TextMatrix(1, 5, "JOB HP IN PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 6, "JOB HP IN PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 7, "JOB HP IN PRE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(1, 8, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 9, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 10, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(2, 5, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 6, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 7, "RESULT")
        AxMSFlexGrid1.set_TextMatrix(2, 8, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 9, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 10, "RESULT")


        AxMSFlexGrid1.set_TextMatrix(0, 11, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_TextMatrix(0, 12, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_TextMatrix(0, 13, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_TextMatrix(0, 14, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_TextMatrix(0, 15, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_TextMatrix(0, 16, "KNOB CLOSED / EXTERNAL LEAK ")
        AxMSFlexGrid1.set_MergeRow(0, True)

        AxMSFlexGrid1.set_TextMatrix(1, 11, "JOB HP IN PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 12, "JOB HP IN PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 13, "JOB HP IN PRE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(1, 14, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 15, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_TextMatrix(1, 16, "JOB HP OUT PRE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(2, 11, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 12, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 13, "RESULT")
        AxMSFlexGrid1.set_TextMatrix(2, 14, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 15, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 16, "RESULT")

        AxMSFlexGrid1.set_TextMatrix(0, 17, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 18, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 19, "-")
        AxMSFlexGrid1.set_MergeRow(0, True)
        AxMSFlexGrid1.set_TextMatrix(1, 17, "COIL VOLTAGE")
        AxMSFlexGrid1.set_TextMatrix(1, 18, "COIL VOLTAGE")
        AxMSFlexGrid1.set_TextMatrix(1, 19, "COIL VOLTAGE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(0, 20, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 21, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 22, "-")
        AxMSFlexGrid1.set_MergeRow(0, True)
        AxMSFlexGrid1.set_TextMatrix(1, 20, "COIL CURRENT")
        AxMSFlexGrid1.set_TextMatrix(1, 21, "COIL CURRENT")
        AxMSFlexGrid1.set_TextMatrix(1, 22, "COIL CURRENT")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(0, 23, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 24, "-")
        AxMSFlexGrid1.set_TextMatrix(0, 25, "-")
        AxMSFlexGrid1.set_MergeRow(0, True)
        AxMSFlexGrid1.set_TextMatrix(1, 23, "COIL RESISTENCE")
        AxMSFlexGrid1.set_TextMatrix(1, 24, "COIL RESISTENCE")
        AxMSFlexGrid1.set_TextMatrix(1, 25, "COIL RESISTENCE")
        AxMSFlexGrid1.set_MergeRow(1, True)

        AxMSFlexGrid1.set_TextMatrix(2, 17, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 18, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 19, "RESULT")
        AxMSFlexGrid1.set_TextMatrix(2, 20, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 21, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 22, "RESULT")
        AxMSFlexGrid1.set_TextMatrix(2, 23, "MIN")
        AxMSFlexGrid1.set_TextMatrix(2, 24, "MAX")
        AxMSFlexGrid1.set_TextMatrix(2, 25, "RESULT")

        AxMSFlexGrid1.set_TextMatrix(0, 26, "OK/NG")
        AxMSFlexGrid1.set_TextMatrix(1, 26, "OK/NG")
        AxMSFlexGrid1.set_TextMatrix(2, 26, "OK/NG")
        AxMSFlexGrid1.set_MergeCol(26, True)


        AxMSFlexGrid1.set_TextMatrix(0, 27, "NO OF PARTS OK")
        AxMSFlexGrid1.set_TextMatrix(1, 27, "NO OF PARTS OK")
        AxMSFlexGrid1.set_TextMatrix(2, 27, "NO OF PARTS OK")
        AxMSFlexGrid1.set_MergeCol(27, True)



        AxMSFlexGrid1.set_TextMatrix(0, 28, "NO OF PARTS NG")
        AxMSFlexGrid1.set_TextMatrix(1, 28, "NO OF PARTS NG")
        AxMSFlexGrid1.set_TextMatrix(2, 28, "NO OF PARTS NG")
        AxMSFlexGrid1.set_MergeCol(28, True)


        AxMSFlexGrid1.set_TextMatrix(0, 29, "NO OF PARTS TOTAL")
        AxMSFlexGrid1.set_TextMatrix(1, 29, "NO OF PARTS TOTAL")
        AxMSFlexGrid1.set_TextMatrix(2, 29, "NO OF PARTS TOTAL")
        AxMSFlexGrid1.set_MergeCol(29, True)





        For i = 0 To AxMSFlexGrid1.Cols - 1
            AxMSFlexGrid1.set_ColAlignment(i, 4) 'centercenter
        Next

        Return Nothing
    End Function
    Dim GridValue(24) As String
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

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

        Dim query As String = "SELECT * FROM `ResultTB` WHERE DateTime1 BETWEEN #" & fromdt & " 00:00:00# AND #" & todt & " 23:59:59#"


        'Application.DoEvents()
        conn.Open()
        Dim cmd As New OleDb.OleDbCommand(query, conn)
        Dim rd As OleDb.OleDbDataReader
        rd = cmd.ExecuteReader
        sr = 1
        While rd.Read

            GridValue(0) = sr
            GridValue(1) = Date.Parse(rd(36).ToString).ToString("yyyy-MM-dd") 'DATE
            GridValue(2) = Date.Parse(rd(36).ToString).ToString("hh:mm:ss tt")  'TIME

            If Len(rd(17).ToString) <> 0 Then
                GridValue(3) = Selectmodel(rd(17)) 'SELECT PART
            Else
                GridValue(3) = rd(17).ToString
            End If
            GridValue(4) = rd(1).ToString 'BARCODE

            GridValue(5) = rd(2).ToString 'hP in 
            GridValue(6) = rd(3).ToString
            GridValue(7) = rd(4).ToString

            GridValue(8) = rd(5).ToString 'hP OUT 
            GridValue(9) = rd(6).ToString
            GridValue(10) = rd(7).ToString

            GridValue(11) = rd(11).ToString 'cOIL voL
            GridValue(12) = rd(12).ToString
            GridValue(13) = rd(8).ToString

            GridValue(14) = rd(13).ToString 'COIL CURRENT
            GridValue(15) = rd(14).ToString
            GridValue(16) = rd(9).ToString

            GridValue(17) = rd(15).ToString 'COIL RES
            GridValue(18) = rd(16).ToString
            GridValue(19) = rd(10).ToString

            GridValue(20) = rd(35).ToString 'OK/NG
            GridValue(21) = rd(32).ToString 'OK
            GridValue(22) = rd(33).ToString 'NG
            GridValue(23) = rd(34).ToString 'TOTAL

            Dim col As Integer = GridValue.Count
            'DataGridView1.Rows.Add(GridValue(0), GridValue(1), GridValue(2), GridValue(3), GridValue(4), GridValue(5), GridValue(6), GridValue(7), GridValue(8), GridValue(9), GridValue(10), GridValue(11), GridValue(12), GridValue(13), GridValue(14), GridValue(15), GridValue(16), GridValue(17), GridValue(18), GridValue(19), GridValue(20), GridValue(21), GridValue(22), GridValue(23))
            For i = 0 To GridValue.Length - 1
                AxMSFlexGrid1.set_TextMatrix(sr + 2, i, GridValue(i))
            Next



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
        xlWorkSheet.Name = ("Reports")
        xlWorkSheet.Columns.ColumnWidth = 35

        Dim rangeToFormat As Excel.Range
        rangeToFormat = xlWorkSheet.Range("A1", "X1")
        rangeToFormat.Interior.Color = ColorTranslator.ToOle(Color.Blue)
        rangeToFormat.Borders.Color = Color.Black

        rangeToFormat.HorizontalAlignment = 3
        rangeToFormat.VerticalAlignment = 3
        rangeToFormat.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
        rangeToFormat.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        rangeToFormat.Font.Size = 10
        rangeToFormat.Font.Bold = 2


        'rangeToFormat = xlWorkSheet.Range("A1", "F1")
        'rangeToFormat.MergeCells = True
        'rangeToFormat.FormulaR1C1 = "All Material Reports"
        'rangeToFormat.HorizontalAlignment = 3
        'rangeToFormat.VerticalAlignment = 3
        'rangeToFormat.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue)
        'rangeToFormat.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)
        'rangeToFormat.Font.Size = 20

        'For Each col As DataGridViewColumn In DataGridView1.Columns
        '    xlWorkSheet.Cells(1, col.Index + 1) = col.HeaderText
        '    For Each rowa As DataGridViewRow In DataGridView1.Rows
        '        xlWorkSheet.Cells(rowa.Index + 2, col.Index + 1) = rowa.Cells(col.Index).Value
        '    Next
        'Next


        For intRow = 1 To AxMSFlexGrid1.Rows - 1
            For intCol = 1 To AxMSFlexGrid1.Cols - 1
                With AxMSFlexGrid1
                    xlWorkSheet.Cells(intRow, intCol).Value = _
                    .get_TextMatrix(intRow - 1, intCol - 1) & ""
                End With
            Next
        Next





        xlWorkSheet.Columns.AutoFit()

        Dim filename As String

        'filename = "Material Reports" + DateTimePicker1.Value.ToString("MM dd yyyy") + " To" + DateTimePicker2.Value.ToString("MM dd yyyy") + DateTime.Now.ToString("ddMMyyyy HHmm ss") + ".xls"

        'filename = "Date " + DateTimePicker1.Value.ToString("MM dd yyyy") + " To " + DateTimePicker2.Value.ToString("MM dd yyyy") + " Reports " + DateTime.Now.ToString("dd MM yyyy HHmmss") + ".xls"
        filename = "hello.xls"
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