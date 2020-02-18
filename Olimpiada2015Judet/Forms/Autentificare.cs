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
    public partial class Autentificare : Form
    {
        public Autentificare()
        {
            InitializeComponent();

            textBox2.PasswordChar = '*';

            textBox1.Text = "Administrator";
            textBox2.Text = "agentie2015";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string utilizator = "Neautentificat";
            if (textBox1.Text == "Administrator" && textBox2.Text == "agentie2015")
            {
                utilizator = "Administrator";
            }
            else if (textBox1.Text == "Turist" && textBox2.Text == "oti2015")
            {
                utilizator = "Turist";
            }

            if (utilizator != "Neautentificat")
            {
                MainForm page = Singleton<MainForm>.Instance;
                page.Utilizator = utilizator;
                page.Show();
                this.Hide();
            }

        }

        private void administratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Administrator";
        }

        private void turistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Turist";
        }
    }
}
