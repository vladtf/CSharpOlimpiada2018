using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olipmpiada2018Judet.DataAcces;

namespace Olipmpiada2018Judet.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeFromFile.Initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new eLearning1918_Start();
            page.ShowDialog();
        }
    }
}
