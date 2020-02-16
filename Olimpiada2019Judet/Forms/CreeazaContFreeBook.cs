using Olimpiada2019Judet.DataAcces;
using Olimpiada2019Judet.Models;
using System;
using System.Windows.Forms;

namespace Olimpiada2019Judet.Forms
{
    public partial class CreeazaContFreeBook : Form
    {
        public CreeazaContFreeBook()
        {
            InitializeComponent();
            textBox4.PasswordChar = '*';
            textBox5.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == textBox5.Text)
            {
                UtilizatorModel utilizator = new UtilizatorModel
                {
                    email = textBox1.Text,
                    nume = textBox2.Text,
                    prenume = textBox3.Text,
                    parola = textBox4.Text
                };
                try
                {
                    SqlDataAcces.Registrare(SqlDataAcces.ConnectionString, utilizator);
                    utilizator = SqlDataAcces.Logare(SqlDataAcces.ConnectionString, textBox1.Text, textBox4.Text);
                    MessageBox.Show("Inregistrare cu succes!");
                    this.Close();
                    this.Visible = false;
                    (Owner as FreeBookHome).Autentificat(utilizator);
                }
                catch (Exception)
                {
                    MessageBox.Show("Email-ul este deja utilizat!");
                }
            }
            else
            {
                MessageBox.Show("Confirmare parola nu corespunde");
            }
        }
    }
}