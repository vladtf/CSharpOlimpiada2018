using Olimpiada2019National.DataAcces;
using Olimpiada2019National.DataProvider;
using Olimpiada2019National.Helpers;
using Olimpiada2019National.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Olimpiada2019National.Forms
{
    public partial class BibliotecarBiblioteca : Form
    {
        public UserModel Utilizator;

        private Timer timer;
        private bool IsEnabled = false;
        private Bitmap selectedImage;
        private List<UserModel> utilizatori;

        public BibliotecarBiblioteca()
        {
            InitializeComponent();

            utilizatori = SqlDataAcces.GetAllUsers();
            InitiateAfisareCititori(utilizatori);
            AddCollumnButton();

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            timer.Start();

            textBox3.Enabled = textBox4.Enabled = false;
            textBox3.PasswordChar = textBox4.PasswordChar = '*';

            button1.Enabled = false;
        }

        private void AddCollumnButton()
        {
            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Afisieaza";
            button.Text = "Afiseaza";
            button.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(button);
        }

        private void InitiateAfisareCititori(List<UserModel> utilizatori)
        {
            DataTable table = new DataTable();
            table.Columns.Add("IdCititor");
            table.Columns.Add("NumePrenume");
            table.Columns.Add("Email");

            foreach (UserModel item in utilizatori)
            {
                DataRow row = table.NewRow();
                row["IdCititor"] = item.ID;
                row["NumePrenume"] = item.NumePenume;
                row["Email"] = item.Email;

                table.Rows.Add(row);
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = table;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt");
            timeLabel.Text = time;
        }

        protected override void OnLoad(EventArgs e)
        {
            pictureBox1.Image = ImageProvider.GetImage(Utilizator.ID);

            label2.Text = Utilizator.NumePenume;
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

            textBox_TextChanged(null, null);
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            bool areFilled = true;
            foreach (var item in groupBox2.Controls)
            {
                if (item is TextBox && String.IsNullOrWhiteSpace((item as TextBox).Text))
                {
                    areFilled = false;
                }
            }

            if (radioButton2.Checked == true && !String.IsNullOrWhiteSpace(textBox1.Text) && !String.IsNullOrWhiteSpace(textBox2.Text))
            {
                areFilled = true;
            }

            button1.Enabled = areFilled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Confirmare parola esuata!");
                return;
            }

            UserModel utilizatorNou = new UserModel
            {
                TipUtilizator = radioButton1.Checked == true ? 1 : 0,
                NumePenume = textBox1.Text,
                Email = textBox2.Text,
                Parola = CriptareParola.Criptare(textBox3.Text)
            };

            SqlDataAcces.InregistreazaUtilizator(utilizatorNou);

            utilizatorNou.ID = SqlDataAcces.GetUserIDByEmail(utilizatorNou.Email);

            if (pictureBox2.Image != null)
            {
                string filePath = "Resurse\\Imagini\\utilizatori\\" + utilizatorNou.ID.ToString() + ".jpeg";
                Bitmap imagine = new Bitmap(pictureBox2.Image);

                imagine.Save(filePath, ImageFormat.Jpeg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            foreach (var item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    (item as TextBox).Text = "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filter = textBox5.Text;

            List<UserModel> peopleFound = utilizatori.Where(x => x.NumePenume.Contains(filter)).ToList();
            InitiateAfisareCititori(peopleFound);
        }
    }
}