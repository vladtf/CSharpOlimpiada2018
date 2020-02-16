using Olimpiada2016Judet.DataAcces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Olimpiada2016Judet.Forms
{
    public partial class Vizualizare_Comanda : Form
    {
        private int idClient;

        public Vizualizare_Comanda(DataTable table, string necesar, string kcal, string pret, int idClient)
        {
            this.idClient = idClient;

            InitializeComponent();

            dataGridView1.DataSource = table;

            textBox7.Text = necesar;
            textBox6.Text = kcal;
            textBox5.Text = pret;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Text = "Elimina";
            btn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(btn);

            dataGridView1.ReadOnly = true;

            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            DataGridView dgv = (DataGridView)sender;

            if (dgv[column, row] is DataGridViewButtonCell)
            {
                int pret = int.Parse((string)dgv["Pret", row].Value);
                int kcal = int.Parse((string)dgv["Kcal", row].Value);

                int calCurr = int.Parse(textBox6.Text);
                textBox6.Text = (calCurr - kcal).ToString();

                int pretCurr = int.Parse(textBox6.Text);
                textBox5.Text = (pretCurr - pret).ToString();

                dgv.Rows.RemoveAt(row);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> cantitati = new List<int>();
            List<int> idProduse = new List<int>();

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                try
                {
                    cantitati.Add(int.Parse((string)item.Cells["Cantitate"].Value));
                    idProduse.Add(int.Parse((string)item.Cells["idProdus"].Value));
                }
                catch { }
            }

            FinalizareComanda.Finalizare(idClient, idProduse, cantitati);

            MessageBox.Show("Comanda trimisa!");

            Start.GetInsance().Show();

            this.Close();
        }
    }
}