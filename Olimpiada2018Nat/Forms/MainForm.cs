using OlimpiadaCsharp2018.Forms;
using OlimpiadaCsharp2018.Helpers;
using System;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializationDataAcces.InitializeDB();

            button3.Enabled = button4.Enabled = button5.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new VizualizareLectii();
            page.ShowDialog(this);
            //page.Tag = this;
            //page.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var page = new LogareCentenar();
            page.Tag = this;
            page.Show();
            this.Hide();
        }

        public void EsteAunteficat()
        {
            button3.Enabled = button4.Enabled = button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var page = new CreareLectie();
            page.Tag = this;
            page.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var page = new GhicesteRegiunea();
            page.ShowDialog();
        }
    }
}