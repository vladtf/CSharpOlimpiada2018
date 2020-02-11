using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2019Judet.Models;
using Olimpiada2019Judet.DataAcces;

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
                    MessageBox.Show("Inregistrare cu succes!");
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
