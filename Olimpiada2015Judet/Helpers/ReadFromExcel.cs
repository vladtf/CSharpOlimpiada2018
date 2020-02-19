using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace Olimpiada2015Judet.Helpers
{
    class ReadFromExcel
    {
        public void ReadExcel()
        {
            string filePath = @"C:\Users\vladu\Documents\visual studio 2010\Projects\OlimpiadaCsharp\Olimpiada2015Judet\bin\Debug\Resurse\Matrice.xls";

            var excelApp = new Excel.Application();

            //excelApp.Visible = true;

            Excel.Workbooks books = excelApp.Workbooks;

            Excel.Workbook book = books.Open(filePath);
            Excel._Worksheet sheet = book.Sheets[1];
            Excel.Range range = sheet.UsedRange;

            int rowCount = range.Rows.Count;
            int colCount = range.Columns.Count;

            for (int i = 1; i <= rowCount; i++)
            {
                for (int j = 1; j <= colCount; j++)
                {
                    if (range.Cells[i, j] != null && range.Cells[i, j].Value2 != null)
                    {
                        string ans = range[i, j].Value2.ToString();
                    }
                }
            }

            excelApp.Quit();
            books.Close();

            Marshal.ReleaseComObject(excelApp);
            Marshal.ReleaseComObject(books);
            Marshal.ReleaseComObject(sheet);
            Marshal.ReleaseComObject(book);
            Marshal.ReleaseComObject(range);
        }
    }
}
