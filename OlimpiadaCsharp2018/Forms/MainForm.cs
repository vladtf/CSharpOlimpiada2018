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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializationDataAcces.InitializeDB();

            button3.Enabled = button4.Enabled = button5.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new VizualizareLectii();
            page.Tag = this;
            page.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var page = new LogareCentenar();
            page.Tag = this;
            page.Show();
            this.Hide();
        }

        public void EsteAunteficat()
        {
            button3.Enabled = button4.Enabled = button5.Enabled = true;
        }




    }
}
