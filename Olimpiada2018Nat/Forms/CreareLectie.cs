using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class CreareLectie : Form
    {
        public CreareLectie()
        {
            InitializeComponent();

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            //tableLayoutPanel1.RowCount++;
            //tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            (Tag as MainForm).Visible = true;
        }


        private void button11_Click(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if( tableLayoutPanel1.Controls.Count == tableLayoutPanel1.RowCount * tableLayoutPanel1.ColumnCount)
            {
                tableLayoutPanel1.Controls.Add(new TextBox { Multiline = true, Size = new Size(100, 100) });
            }
            else
            {
                MessageBox.Show("Table is full");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Width =
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Height += 50;
            RowStyle temp = tableLayoutPanel1.RowStyles[tableLayoutPanel1.RowCount - 1];
            tableLayoutPanel1.RowCount++;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(temp.SizeType, 50));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}