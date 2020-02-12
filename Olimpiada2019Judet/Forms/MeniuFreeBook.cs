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
        public MeniuFreeBook()
        {
            InitializeComponent();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;

            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(dataGridView1_CellContentClick);
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

                    dgv.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Green;
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
    }
}
