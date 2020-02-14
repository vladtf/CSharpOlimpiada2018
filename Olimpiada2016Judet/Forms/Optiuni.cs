using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2016Judet.Models;

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
            int s = int.Parse(textBox1.Text) + int.Parse(textBox2.Text) + int.Parse(textBox3.Text);

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

                    int calCurr = textBox6.Text !="" ? int.Parse(textBox6.Text) : 0;
                    textBox6.Text = (calCurr + kcal*cantitate).ToString();

                    int pretCurr = textBox5.Text != "" ? int.Parse(textBox6.Text) : 0;
                    textBox5.Text = (pretCurr + cantitate * pret).ToString();
 
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
        }



    }
}
