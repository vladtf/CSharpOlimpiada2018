﻿using Olipmpiada2018Judet.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Olipmpiada2018Judet.ItemsControl
{
    public partial class ItemTip3 : UserControl
    {
        public string NrItem { get; set; }
        public string EnuntItem { get; set; }
        public string Var1 { get; set; }
        public string Var2 { get; set; }
        public string Var3 { get; set; }
        public string Var4 { get; set; }

        public string RaspunsCorect { get; set; }

        public ItemTip3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string raspuns = "";
            string raspunsCorect = String.Join("", RaspunsCorect.Select(x => x).Reverse());
            foreach (var c in groupBox1.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox radB = (CheckBox)c;
                    if (radB.Checked == true)
                    {
                        raspuns += radB.Name.First(x => (x >= '1' && x <= '9')).ToString();
                    }
                }
            }

            if (raspuns == raspunsCorect)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }
            (Tag as eLearning_Elev).Raspunde(raspunsCorect, raspuns);
            button1.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            label2.Text = NrItem.ToString();
            textBox1.Text = EnuntItem;

            checkBox1.Text = Var1;
            checkBox2.Text = Var2;
            checkBox3.Text = Var3;
            checkBox4.Text = Var4;
        }
    }
}