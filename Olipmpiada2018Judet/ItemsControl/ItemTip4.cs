using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olipmpiada2018Judet.Forms;

namespace Olipmpiada2018Judet.ItemsControl
{
    public partial class ItemTip4 : UserControl, IItemType
    {
        public string NrItem { get; set; }
        public string EnuntItem { get; set; }
        public int RaspunsCorect { get; set; }

        public ItemTip4()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int raspuns = -1;
            if (radioButton1.Checked == true)
            {
                raspuns = 1;
            }
            if (radioButton2.Checked == true)
            {
                raspuns = 0;
            }

            if (raspuns == RaspunsCorect)
            {
                (Tag as eLearning_Elev).RaspunsCorect();
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }
            button1.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            label2.Text = NrItem.ToString();
            textBox1.Text = EnuntItem;
        }


    }
}
