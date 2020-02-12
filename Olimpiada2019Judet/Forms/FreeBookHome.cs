using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2019Judet.DataAcces;
using Olimpiada2019Judet.Models;

namespace Olimpiada2019Judet.Forms
{
    public partial class FreeBookHome : Form
    {
        public FreeBookHome()
        {
            InitializeComponent();
        }

        private void FreeBookHome_Load(object sender, EventArgs e)
        {
            InitializatorDB.Initializare(SqlDataAcces.ConnectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new LogareFreeBook();
            page.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var page = new CreeazaContFreeBook();
            page.ShowDialog(this);
        }

        public void Autentificat(UtilizatorModel utilizator)
        {
            var page = new MeniuFreeBook { utilizator = utilizator };
            //MessageBox.Show("Logare cu succes!");
            this.Visible = false;
            page.ShowDialog(this);
        }
    }
}
