using OlimpiadaCsharp2018.Helpers;
using OlimpiadaCsharp2018.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class AmUitatParolaForm : Form
    {
        private List<Bitmap> imagini;
        private List<int> listaOameni;
        private Random r = new Random();

        public UserModel User { get; set; }

        public AmUitatParolaForm(UserModel user)
        {
            InitializeComponent();

            User = user;
            label1.Text += User.Email;

            button2.Enabled = false;
            InitiliazeazaImagini();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                LoghingDB.SalvareParolaNoua(textBox1.Text, User.Email);
                MessageBox.Show("Parola a fost modificata!");
                (Tag as MainForm).Visible = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Confirmare parola nu corespunde!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (Tag as MainForm).Visible = true;
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            (Tag as MainForm).Visible = true;
        }

        private void InitiliazeazaImagini()
        {
            pictureBox1.BackColor = pictureBox2.BackColor = pictureBox3.BackColor = pictureBox4.BackColor = pictureBox5.BackColor = pictureBox6.BackColor = Color.Transparent;
            imagini = GetImages.GetAllImages();
            listaOameni = GetImages.ImaginiOameni();

            #region trash

            int index = r.Next(0, 19);
            pictureBox1.Image = imagini[index];
            pictureBox1.Tag = imagini[index].Tag;

            index = r.Next(0, 19);
            pictureBox2.Image = imagini[index];
            pictureBox2.Tag = imagini[index].Tag;

            index = r.Next(0, 19);
            pictureBox3.Image = imagini[index];
            pictureBox3.Tag = imagini[index].Tag;

            index = r.Next(0, 19);
            pictureBox4.Image = imagini[index];
            pictureBox4.Tag = imagini[index].Tag;

            index = r.Next(0, 19);
            pictureBox5.Image = imagini[index];
            pictureBox5.Tag = imagini[index].Tag;

            index = r.Next(0, 19);
            pictureBox6.Image = imagini[index];
            pictureBox6.Tag = imagini[index].Tag;

            #endregion trash
        }

        private void pictureBox_Select(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox.BackColor == Color.Transparent)
            {
                pictureBox.BackColor = Color.Blue;
            }
            else
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ok = true;

            #region ABSOLUTE TRASH CODE

            if (pictureBox1.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox1.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox1.Tag) != -1) ok = false;

            if (pictureBox2.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox2.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox2.Tag) != -1) ok = false;

            if (pictureBox3.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox3.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox3.Tag) != -1) ok = false;

            if (pictureBox4.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox4.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox4.Tag) != -1) ok = false;

            if (pictureBox5.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox5.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox5.Tag) != -1) ok = false;

            if (pictureBox6.BackColor == Color.Blue)
            {
                if (listaOameni.IndexOf((int)pictureBox6.Tag) == -1)
                {
                    ok = false;
                }
            }
            else if (listaOameni.IndexOf((int)pictureBox6.Tag) != -1) ok = false;

            #endregion ABSOLUTE TRASH CODE

            if (ok)
            {
                MessageBox.Show("Identitate confirmata!");
                button2.Enabled = true;
            }
            else
            {
                MessageBox.Show("Mai incearca!");
                button2.Enabled = false;
            }
            InitiliazeazaImagini();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}