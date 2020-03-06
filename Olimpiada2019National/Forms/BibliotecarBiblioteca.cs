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

        private UserModel _selectedUser;
        private List<RezervareModel> _rezervari;
        private List<ImprumutModel> _imprumutri;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            if (dataGridView1.Rows[row].Cells[column] is DataGridViewButtonCell)
            {
                int userId = Int32.Parse((string)dataGridView1.Rows[row].Cells["IdCititor"].Value);
                _selectedUser = utilizatori.Find(x => x.ID == userId);

                IsEnabled = true;
                _imprumutri = PersonalDataAcces.GetAllLaondsByUserId(_selectedUser.ID, SqlDataAcces.ConnectionString);
                _rezervari = PersonalDataAcces.GetAllRezervarationByUserId(_selectedUser.ID, SqlDataAcces.ConnectionString);
                InitiateCititor(_selectedUser);
                tabControl1.SelectedIndex++;
            }
        }

        private void InitiateCititor(UserModel selectedUser)
        {
            label8.Text = String.Format("IdCititor : {0}", selectedUser.ID.ToString());
            label9.Text = String.Format("Nume si Prenume : {0}", selectedUser.NumePenume);
            label10.Text = String.Format("Rezervari ramase : {0}", _rezervari.Where(x => x.StatusRezervare == 1).Count());
            label11.Text = String.Format("Imprumuturi ramase : {0}", _imprumutri.Where(x => x.DataRestituire == new DateTime()).Count());

            pictureBox3.Image = ImageProvider.GetImage(selectedUser.ID);

            InitiatiateImprumuturiGrid();
        }

        private void InitiatiateImprumuturiGrid()
        {
            DataTable table = new DataTable();

            table.Columns.Add("IdImprumut");
            table.Columns.Add("IdCarte");
            table.Columns.Add("Titlu");
            table.Columns.Add("Autori");
            table.Columns.Add("DataImprumut");
            table.Columns.Add("DataExpirareImprumut");

            foreach (ImprumutModel item in _imprumutri)
            {
                if (item.DataRestituire == new DateTime())
                {
                    DataRow row = table.NewRow();

                    row[0] = item.IdImprumut;
                    row[1] = item.IdCarte;
                    row[2] = item.Carte.Titlu;
                    row[3] = item.Carte.Autor;
                    row[4] = item.DataImprumut;
                    row[5] = item.DataImprumut.AddDays(7);

                    table.Rows.Add(row);
                }
            }

            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = table;

            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Restituire";
            button.Text = "Restituire";
            button.UseColumnTextForButtonValue = true;
            dataGridView2.Columns.Add(button);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
            {
                try
                {
                    int idImprumut = Int32.Parse((string)dataGridView2["IdImprumut", e.RowIndex].Value);
                    PersonalDataAcces.ReturneazaCarte(idImprumut, SqlDataAcces.ConnectionString);

                    _imprumutri = PersonalDataAcces.GetAllLaondsByUserId(_selectedUser.ID, SqlDataAcces.ConnectionString);
                    InitiateCititor(_selectedUser);
                }
                catch { }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var fisa = new FisaCititor(_selectedUser, _imprumutri);
            fisa.Show();
        }
    }
}