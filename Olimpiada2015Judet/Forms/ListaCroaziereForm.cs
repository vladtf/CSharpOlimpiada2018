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
    public partial class ListaCroaziereForm : Form
    {
        private DataTable _table;
        public ListaCroaziereForm()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new string[] {"3 zile", "5 zile", "8 zile"});
            _table = ListaCroaziera.GetCroaziere(SqlDataAcces.ConnectionString);

            dataGridView1.DataSource = _table;

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox) sender;
            FilterTable((senderComboBox.Items[senderComboBox.SelectedIndex].ToString()));
        }

        private void FilterTable(string tipLike)
        {
            string tipCroaziera = tipLike[0].ToString();
            string filterField = "Tip_Croaziera";
            _table.DefaultView.RowFilter = String.Format("{0} = {1}", filterField, tipCroaziera);

            //dataGridView1.DataSource = _table;
        }
    }
}
