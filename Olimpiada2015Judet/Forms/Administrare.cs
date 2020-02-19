using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2015Judet.DataAcces;

namespace Olimpiada2015Judet.Forms
{
    public partial class Administrare : Form
    {
        List<Point> porturi;
        public Administrare()
        {
            InitializeComponent();

            pictureBox1.Click += new EventHandler(pictureBox1_Click);
        }

        void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs arg = (MouseEventArgs)e;

            if (button1.BackColor == Color.LightPink && porturi.Count < 13)
            {
                porturi.Add(new Point(arg.X, arg.Y));

                label1.Text = arg.X.ToString();
                label2.Text = arg.Y.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor != Color.LightPink)
            {
                porturi = new List<Point>();
                button1.BackColor = Color.LightPink;
            }
            else
            {
                button1.BackColor = Color.LightGreen;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InitializarePorturi.Salvare(SqlDataAcces.Connectionstring, porturi);

            MessageBox.Show("Salvare cu succes!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActualizareDistatante.Actualizeaza(SqlDataAcces.Connectionstring);
            MessageBox.Show("Date actualizate!");
        }



    }
}
