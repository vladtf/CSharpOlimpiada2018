using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OlimpiadaCsharp2018.Helpers;
using OlimpiadaCsharp2018.Forms;

namespace OlimpiadaCsharp2018
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializationDataAcces.InitializeDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new VizualizareLectii();
            page.Tag = this;
            page.Show();
            this.Hide();
        }




    }
}
