using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2019Judet.Models;
using Olimpiada2019Judet.DataAcces;

namespace Olimpiada2019Judet.Forms
{
    public partial class MeniuFreeBook : Form
    {
        public UtilizatorModel utilizator { get; set; }
        private List<ImprumutModel> imprumuturi;
        DataTable table;
        public MeniuFreeBook()
        {
            InitializeComponent();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();

            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);

            progressBar1.Maximum = 3;
            progressBar1.Minimum = 0;
            progressBar1.Value = 3;

        }

        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.Columns[e.ColumnIndex] is DataGridViewColumn)
            {
                int nrCartiImprumutate = SqlDataAcces.VerificaImprumuturi(utilizator);
                if (nrCartiImprumutate < 3)
                {
                    int idCarte = Int32.Parse((string)dgv.Rows[e.RowIndex].Cells["id_carte"].Value);
                    SqlDataAcces.ImprumutaCarte(idCarte, utilizator);

                    //dgv.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Green;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    MessageBox.Show("Aveti deja imprumutate 3 carti in ultimele 30 de zile.");
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            (Owner as FreeBookHome).Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            dataGridView1.DataSource = InitializareImprumuturiDB.GetTable(SqlDataAcces.ConnectionString);

            dataGridView1.Columns[0].Visible = false;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            btn.HeaderText = "Click data";
            btn.Text = "Click";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 3;
            try
            {
                table.Clear();
            }
            catch { }

            imprumuturi = SqlDataAcces.GetImprumuturi(utilizator);
            imprumuturi = imprumuturi.OrderByDescending(x => x.DataImprumut).ToList();
            List<int> expirate = new List<int>();
            table = new DataTable();

            table.Columns.Add("Index");
            table.Columns.Add("Titlu");
            table.Columns.Add("Autor");
            table.Columns.Add("DataImprumut");
            table.Columns.Add("DataDisponibiliate");

            //table.DefaultView.Sort = "DataImprumut ASC";

            for (int i = 0; i < imprumuturi.Count; i++)
            {
                DataRow newRow = table.NewRow();
                int index = i + 1;
                newRow[0] = index;
                newRow[1] = imprumuturi[i].Titlu;
                newRow[2] = imprumuturi[i].Autor;
                newRow[3] = imprumuturi[i].DataImprumut;
                newRow[4] = imprumuturi[i].DataDisponibilitate;
                table.Rows.Add(newRow);
                if (imprumuturi[i].DataDisponibilitate < DateTime.Now)
                {
                    expirate.Add(index);
                }
                
            }

            dataGridView2.DataSource = table;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                if (expirate.Contains(Int32.Parse((string)dataGridView2.Rows[i].Cells[0].Value)))
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    progressBar1.Value--;
                }
            }

            label3.Text = progressBar1.Value.ToString() + " / 3 ";

        }
    }
}
