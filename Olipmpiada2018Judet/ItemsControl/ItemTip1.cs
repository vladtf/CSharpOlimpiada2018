using Olipmpiada2018Judet.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Olipmpiada2018Judet.ItemsControl
{
    public partial class ItemTip1 : UserControl
    {
        public string NrItem { get; set; }
        public string EnuntItem { get; set; }
        public string RaspunsCorect { get; set; }

        public ItemTip1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string raspuns = string.Join("", textBox2.Text.Split(' ').ToList()).ToLower();
            string raspungCorect = string.Join("", RaspunsCorect.Split(' ').ToList()).ToLower();
            if (raspuns == raspungCorect)
            {
                button1.BackColor = Color.Green;
            }
            else
            {
                button1.BackColor = Color.Red;
            }
            button1.Enabled = false;
            (Tag as eLearning_Elev).Raspunde(raspungCorect, raspuns);
        }

        protected override void OnLoad(EventArgs e)
        {
            label2.Text = NrItem.ToString();
            textBox1.Text = EnuntItem;
        }
    }
}