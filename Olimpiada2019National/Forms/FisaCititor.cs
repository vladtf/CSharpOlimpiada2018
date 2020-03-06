using Olimpiada2019National.DataProvider;
using Olimpiada2019National.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Olimpiada2019National.Forms
{
    public partial class FisaCititor : Form
    {
        private readonly UserModel user;
        private readonly List<ImprumutModel> imprumuturi;

        public FisaCititor(UserModel user, List<ImprumutModel> imprumuturi)
        {
            InitializeComponent();
            this.user = user;
            this.imprumuturi = imprumuturi;

            InitiateDataGridView();
            InitiateUserInfo();
        }

        private void InitiateUserInfo()
        {
            label3.Text = user.NumePenume;

            pictureBox2.Image = ImageProvider.GetImage(user.ID);
        }

        private void InitiateDataGridView()
        {
            DataTable table = new DataTable();

            table.Columns.Add("Denumire carte");
            table.Columns.Add("Autor");
            table.Columns.Add("Data imprumut");
            table.Columns.Add("Data restuirii");

            foreach (ImprumutModel item in imprumuturi)
            {
                DataRow row = table.NewRow();

                row[0] = item.Carte.Titlu;
                row[1] = item.Carte.Autor;
                row[2] = item.DataImprumut;
                row[3] = item.DataRestituire;

                table.Rows.Add(row);
            }

            dataGridView1.DataSource = table;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}