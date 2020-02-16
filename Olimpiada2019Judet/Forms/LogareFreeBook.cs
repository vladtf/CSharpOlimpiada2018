using Olimpiada2019Judet.DataAcces;
using Olimpiada2019Judet.Models;
using System;
using System.Windows.Forms;

namespace Olimpiada2019Judet.Forms
{
    public partial class LogareFreeBook : Form
    {
        public LogareFreeBook()
        {
            InitializeComponent();

            textBox4.PasswordChar = '*';

            textBox1.Text = "ana@gmail.com";
            textBox4.Text = "ana";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UtilizatorModel utilizator = SqlDataAcces.Logare(SqlDataAcces.ConnectionString, textBox1.Text, textBox4.Text);
            if (utilizator.email == textBox1.Text)
            {
                this.Close();
                this.Visible = false;
                (Owner as FreeBookHome).Autentificat(utilizator);
            }
            else
            {
                MessageBox.Show("Eroare autentificare!");
            }
        }
    }
}