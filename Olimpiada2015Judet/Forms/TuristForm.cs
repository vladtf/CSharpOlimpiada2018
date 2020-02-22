using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2015Judet.DataAcces;

namespace Olimpiada2015Judet.Forms
{
    public partial class TuristForm : Form
    {
        private DataTable _table;
        private int selectedRow;

        public TuristForm()
        {
            InitializeComponent();
            InitiateDataGridView();

            comboBox1.Items.AddRange(new string[] { "3 zile", "5 zile", "8 zile" });
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            FilterTable((senderComboBox.Items[senderComboBox.SelectedIndex].ToString()));
        }

        private void FilterTable(string tipLike)
        {
            string tipCroaziera = tipLike[0].ToString();
            string filterField = "Tip_Croaziera";
            _table.DefaultView.RowFilter = String.Format("{0} = {1}", filterField, tipCroaziera);

            //dataGridView1.DataSource = _table;
        }

        private void InitiateDataGridView()
        {
            _table = ListaCroaziera.GetCroaziere(SqlDataAcces.ConnectionString);

            dataGridView1.DataSource = _table;

            dataGridView1.Columns["ID_Croaziera"].HeaderText = "ID";
            dataGridView1.Columns["Tip_Croaziera"].Visible = false;
            dataGridView1.Columns["distanta"].Visible = false;
            dataGridView1.Columns["Circuit"].DisplayIndex = 1;
            dataGridView1.Columns["Circuit"].ReadOnly = dataGridView1.Columns["ID_Croaziera"].ReadOnly = dataGridView1.Columns["Data_Start"].ReadOnly = dataGridView1.Columns["Data_Final"].ReadOnly = dataGridView1.Columns["Pret"].ReadOnly = false;

            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Introduceti un nr. valid de pasageri!");
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            (sender as DataGridView).Rows[row].Cells[column].Value = 0;

            e.ThrowException = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int nrPasageri = 0;

            try
            {
                nrPasageri = (int)dataGridView1.Rows[selectedRow].Cells["Nr_Pasageri"].Value;
            }
            catch
            {
                MessageBox.Show("Introduceti un nr. valid de pasageri!");
                return;
            }

            int idCroaziera = (int)dataGridView1.Rows[selectedRow].Cells["ID_Croaziera"].Value;
            DateTime dataStart;
            DateTime dataFinal;
            try
            {
                dataStart = (DateTime)dataGridView1.Rows[selectedRow].Cells["Data_Start"].Value;
                dataFinal = (DateTime)dataGridView1.Rows[selectedRow].Cells["Data_Final"].Value;
            }
            catch
            {
                MessageBox.Show("Selectati o data valida !");
                return;
            }

            SqlDataAcces.UpdateCroaziera(idCroaziera, nrPasageri, dataStart, dataFinal);

            List<int> listaPorturi = ((string)dataGridView1.Rows[selectedRow].Cells["Lista_Porturi"].Value).ToString().Split(',').Select(x=>Int32.Parse(x)).ToList();

            VizualizareCroaziera page = new VizualizareCroaziera(listaPorturi);
            page.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (selectedRow >= 0)
            {
                dataGridView1.Rows[selectedRow].Cells["Data_Start"].Value = dateTimePicker1.Value;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (selectedRow >= 0)
            {
                dataGridView1.Rows[selectedRow].Cells["Data_Final"].Value = dateTimePicker2.Value;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
        }

    }
}
