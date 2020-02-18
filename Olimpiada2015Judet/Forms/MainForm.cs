using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Olimpiada2015Judet.Forms
{
    public partial class MainForm : Form
    {
        public string Utilizator { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Singleton<Autentificare>.Instance.Close();
        }
    }
}
