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
    public partial class ItemTip2 : UserControl
    {
        public string NrItem { get; set; }
        public string EnuntItem { get; set; }
        public string Var1 { get; set; }
        public string Var2 { get; set; }
        public string Var3 { get; set; }
        public string Var4 { get; set; }

        public string RaspunsCorect { get; set; }

        public ItemTip2()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            label2.Text = NrItem.ToString();
            textBox1.Text = EnuntItem;

            radioButton1.Text = Var1;
            radioButton2.Text = Var2;
            radioButton3.Text = Var3;
            radioButton4.Text = Var4;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string raspuns = "";
            foreach (var c in groupBox1.Controls)
            {
                if (c is RadioButton)
                {
                    RadioButton radB = (RadioButton)c;
                    if (radB.Checked == true)
                    {
                        raspuns = radB.Name.First(x => (x >= '1' && x <= '9')).ToString();
                    }
                }
            }

            if (raspuns == RaspunsCorect)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }
            (Tag as eLearning_Elev).Raspunde(RaspunsCorect, raspuns);
            button1.Enabled = false;
        }

    }
}
