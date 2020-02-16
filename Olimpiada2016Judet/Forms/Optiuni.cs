using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2016Judet.Models;
using Olimpiada2016Judet.DataAcces;

namespace Olimpiada2016Judet.Forms
{
    public partial class Optiuni : Form
    {
        private UserModel utilizator;
        public Optiuni(UserModel utilizator)
        {
            this.utilizator = utilizator;
            InitializeComponent();

            InitializareTabela();
        }

        private void InitializareTabela()
        {
            dataGridView1.DataSource = DataAcces.SqlDataAcces.GetMeniu();

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].ReadOnly = true;
            }

            dataGridView1.Columns.Add("cantitate", "cantitate");

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Sales";
            btn.Text = "Adauga";
            btn.UseColumnTextForButtonValue = true;

            dataGridView1.Columns.Add(btn);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int s = 0;
            try
            {
                s = int.Parse(textBox1.Text) + int.Parse(textBox2.Text) + int.Parse(textBox3.Text);
            }
            catch (Exception)
            {

            }

            if (s < 250)
            {
                textBox4.Text = "1800";
            }
            else if (s <= 250)
            {
                textBox4.Text = "2200";
            }
            else
            {
                textBox4.Text = "2500";
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            int row = e.RowIndex;
            int column = e.ColumnIndex;

            DataGridViewCell cell = dgv.Rows[row].Cells[column];

            if (dgv.Rows[row].DefaultCellStyle.BackColor == Color.Green)
            {
                return;
            }

            if (cell is DataGridViewButtonCell)
            {
                int cantitate = 0;
                int pret = (int)dgv["pret", row].Value;
                int kcal = (int)dgv["kcal", row].Value;

                try
                {
                    cantitate = int.Parse((string)dgv["cantitate", row].Value);
                }
                catch (Exception)
                {
                    MessageBox.Show("Cantitate nevalida!");
                    return;
                }

                if (cantitate > 0)
                {
                    dgv.Rows[row].DefaultCellStyle.BackColor = Color.Green;

                    int calCurr = textBox6.Text != "" ? int.Parse(textBox6.Text) : 0;
                    textBox6.Text = (calCurr + kcal * cantitate).ToString();
                    dgv["kcal", row].Value = kcal * cantitate;

                    int pretCurr = textBox5.Text != "" ? int.Parse(textBox6.Text) : 0;
                    textBox5.Text = (pretCurr + cantitate * pret).ToString();
                    dgv["pret", row].Value = pret * cantitate;

                }
                else
                {
                    MessageBox.Show("Cantitate 0");
                }

            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            textBox7.Text = textBox4.Text;
            textBox10.Text = textBox4.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("idProdus");
            table.Columns.Add("Nume Produs");
            table.Columns.Add("Kcal");
            table.Columns.Add("Pret");
            table.Columns.Add("Cantitate");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.Green)
                {
                    DataRow newRow = table.NewRow();

                    newRow[0] = row.Cells["id_produs"].Value;
                    newRow[1] = row.Cells["denumire_produs"].Value;
                    newRow[2] = row.Cells["kcal"].Value;
                    newRow[3] = row.Cells["pret"].Value;
                    newRow[4] = row.Cells["cantitate"].Value;

                    table.Rows.Add(newRow);
                }
            }

            if (table.Rows.Count > 0)
            {
                var page = new Vizualizare_Comanda(table, textBox7.Text, textBox6.Text, textBox5.Text, utilizator.Id);
                page.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Comanda esuata!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable table = DataAcces.SqlDataAcces.GetMeniu();

            List<MeniuModel> mancaruri = new List<MeniuModel>();

            foreach (DataRow item in table.Rows)
            {
                MeniuModel men = new MeniuModel
                {
                    Id = (int)item["id_produs"],
                    DenumereaProdus = (string)item["denumire_produs"],
                    KCal = (int)item["kcal"],
                    Pret = (int)item["pret"],
                    Felul = (int)item["felul"]
                };

                mancaruri.Add(men);
            }


            DataTable newTable = new DataTable();
            newTable.Columns.Add("IdFelul1");
            newTable.Columns.Add("IdFelul2");
            newTable.Columns.Add("IdFelul3");
            newTable.Columns.Add("Felul1");
            newTable.Columns.Add("Felul2");
            newTable.Columns.Add("Felul3");
            newTable.Columns.Add("Total Kcal");
            newTable.Columns.Add("Pret total");

            int budget = 0;
            int kcalMax = 0;
            int.TryParse(textBox8.Text, out budget);
            int.TryParse(textBox10.Text, out kcalMax);

            List<MeniuModel> feluri1 = mancaruri.Where(x => x.Felul == 1).ToList();
            List<MeniuModel> feluri2 = mancaruri.Where(x => x.Felul == 2).ToList();
            List<MeniuModel> feluri3 = mancaruri.Where(x => x.Felul == 3).ToList();

            (from fel1 in feluri1
             from fel2 in feluri2
             from fel3 in feluri3
             where fel1.Pret + fel2.Pret + fel3.Pret <= budget && fel1.KCal + fel2.KCal + fel3.KCal <= kcalMax
             select new
             {
                 IdFelul1 = fel1.Id,
                 IdFelul2 = fel2.Id,
                 IdFelul3 = fel3.Id,
                 Felul1 = fel1.DenumereaProdus,
                 Felul2 = fel2.DenumereaProdus,
                 Felul3 = fel3.DenumereaProdus,
                 TotalKCal = fel1.KCal + fel2.KCal + fel3.KCal,
                 PretTotal = fel1.Pret + fel2.Pret + fel3.Pret

             }).ToList().Select(x =>
                                  {
                                      DataRow temp = newTable.NewRow();
                                      temp["IdFelul1"] = x.IdFelul1;
                                      temp["IdFelul2"] = x.IdFelul2;
                                      temp["IdFelul3"] = x.IdFelul3;
                                      temp["Felul1"] = x.Felul1;
                                      temp["Felul2"] = x.Felul2;
                                      temp["Felul3"] = x.Felul3;
                                      temp["Total Kcal"] = x.TotalKCal;
                                      temp["Pret total"] = x.PretTotal;

                                      return temp;

                                  }).ToList().ForEach(x => newTable.Rows.Add(x));

            dataGridView2.DataSource = newTable;

            dataGridView2.Columns["IdFelul1"].Visible = dataGridView2.Columns["IdFelul2"].Visible = dataGridView2.Columns["IdFelul3"].Visible = false;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Alege";
            btn.Text = "Alege";
            btn.UseColumnTextForButtonValue = true;

            dataGridView2.Columns.Add(btn);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            int row = e.RowIndex;
            int column = e.ColumnIndex;

            DataGridViewCell cell = dgv.Rows[row].Cells[column];

            if (cell is DataGridViewButtonCell)
            {
                DataGridViewRow rowSelected = dgv.Rows[row];

                int id1 = int.Parse((string)rowSelected.Cells["IdFelul1"].Value);
                int id2 = int.Parse((string)rowSelected.Cells["IdFelul2"].Value);
                int id3 = int.Parse((string)rowSelected.Cells["IdFelul3"].Value);
                List<int> idProduse = new int[] { id1, id2, id3 }.ToList();

                FinalizareComanda.Finalizare(utilizator.Id, idProduse, new int[] { 1, 1, 1 }.ToList());

                MessageBox.Show("Comanda trimisa!");

                this.Close();
                Start.GetInsance().Show();
            }

        }



    }
}
