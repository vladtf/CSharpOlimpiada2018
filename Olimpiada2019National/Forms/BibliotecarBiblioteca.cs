using Olimpiada2019National.DataProvider;
using Olimpiada2019National.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Olimpiada2019National.Forms
{
    public partial class BibliotecarBiblioteca : Form
    {
        public UserModel utilizator;

        private Timer timer;
        private bool IsEnabled = false;
        private Bitmap selectedImage;
        public BibliotecarBiblioteca()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            timer.Start();

            textBox3.Enabled = textBox4.Enabled = false;
            textBox3.PasswordChar = textBox4.PasswordChar = '*';
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            timeLabel.Text = time;
        }

        protected override void OnLoad(EventArgs e)
        {
            pictureBox1.Image = ImageProvider.GetImage(utilizator.ID);

            label2.Text = utilizator.NumePenume;
        }

        private void iesireDinAplicatieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Singleton<LogareBiblioteca>.Instance.Close();
        }

        private void delogareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Singleton<LogareBiblioteca>.Instance.Visible = true;
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsEnabled && tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 1;
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox3.Enabled = textBox4.Enabled = true;
            }
            else
            {
                textBox3.Enabled = textBox4.Enabled = false;
                textBox3.Text = textBox4.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string directoryPath = Directory.GetCurrentDirectory() + "\\Resurse\\Imagini\\altele";
            fileDialog.InitialDirectory = directoryPath;
            fileDialog.Filter = "Image files|*.jpg*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedImage = new Bitmap(fileDialog.FileName);

                pictureBox2.Image = new Bitmap(selectedImage);
            }
        }
    }
}
