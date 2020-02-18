using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;

namespace Olimpiada2015Judet.Forms
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();

            ReadExcel();
        }

        private void ReadExcel()
        {
            //string filePath = @"C:\Users\vladu\Documents\visual studio 2010\Projects\OlimpiadaCsharp\Olimpiada2015Judet\bin\Debug\Resurse\Matrice.xlsx";

            //var excelApp = new Excel.Application();
            //excelApp.Visible = true;

            //Excel.Workbooks books = excelApp.Workbooks;

            //Excel.Workbook sheet = books.Open(filePath);
        }
    }
}
