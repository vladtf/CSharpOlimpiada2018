using Olipmpiada2018Judet.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Olipmpiada2018Judet.ItemsControl
{
    public partial class ItemTip4 : UserControl
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
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }

            (Tag as eLearning_Elev).Raspunde(RaspunsCorect.ToString(), raspuns.ToString());

            button1.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            label2.Text = NrItem.ToString();
            textBox1.Text = EnuntItem;
        }
    }
}