using OlimpiadaCSharp.Forms;
using OlimpiadaCSharp.Helpers;
using System;
using System.Windows.Forms;

namespace OlimpiadaCSharp
{
    public partial class Initializationform : Form
    {
        #region Field declarations

        private string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\OlimpiadaCSharp.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
        private string filePath = "planificari.txt";

        #endregion Field declarations

        public Initializationform()
        {
            InitializeComponent();

            MinimizeBox = false;
            MaximizeBox = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataAcces.SaveData(connectionString, filePath);
                MessageBox.Show("Baza de date initializata cu succes!");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Eroare initializare");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var page = new GenereazaPostare(connectionString);
            page.Tag = this;
            page.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            var page = new VizualizareExcursie(connectionString);
            page.Tag = this;
            page.Show();
        }
    }
}