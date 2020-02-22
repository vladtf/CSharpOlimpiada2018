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

            WindowState = FormWindowState.Maximized;
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            Singleton<Autentificare>.Instance.Close();
        }

        private void administrareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utilizator == "Administrator")
            {
                Administrare page = new Administrare { MdiParent = this };
                page.Show();
            }
        }

        private void initializareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TuristForm page = new TuristForm { MdiParent = this };
            page.Show();
        }
    }
}
