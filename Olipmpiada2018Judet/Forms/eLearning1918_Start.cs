using Olipmpiada2018Judet.Forms;
using Olipmpiada2018Judet.Helpers;
using Olipmpiada2018Judet.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Olipmpiada2018Judet.DataAcces
{
    public partial class eLearning1918_Start : Form
    {
        private List<Bitmap> imagini;
        private Timer timer = new Timer();

        public eLearning1918_Start()
        {
            InitializeComponent();

            textBox3.PasswordChar = '*';

            imagini = ImagesProcessor.GetAllImages();

            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);
            progressBar1.Maximum = imagini.Count() - 1;
            progressBar1.Value = 0;
            pictureBox1.Image = imagini.First();

            timer.Start();

            textBox2.Text = "elev1@yahoo.com";
            textBox3.Text = "elev1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Manual")
            {
                timer.Stop();
                button2.Text = "Automat";
                button1.Enabled = button3.Enabled = true;
            }
            else
            {
                timer.Start();
                button2.Text = "Manual";
                button1.Enabled = button3.Enabled = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                pictureBox1.Image = imagini[progressBar1.Value];
            }
            else
            {
                timer.Stop();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            timer.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                button3.Enabled = true;
                progressBar1.Value++;
                pictureBox1.Image = imagini[progressBar1.Value];
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    button3.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (progressBar1.Value > progressBar1.Minimum)
            {
                button3.Enabled = true;
                progressBar1.Value--;
                pictureBox1.Image = imagini[progressBar1.Value];
                if (progressBar1.Value == progressBar1.Maximum)
                {
                    button3.Enabled = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Autentificare();
        }

        public void Autentificare()
        {
            UserModel utilizator = SqlDataAcces.Autentificare(SqlDataAcces.ConnectionString, textBox2.Text);
            if (utilizator.Parola == textBox3.Text)
            {
                var page = new eLearning_Elev { Tag = this, UserLoged = utilizator };
                this.Hide();
                page.Show();
            }
            else
            {
                MessageBox.Show("Eroare de autentificare!");
                textBox2.Text = textBox3.Text = "";
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            InitializeFromFile.Initialize();
        }
    }
}