using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2016Judet.Models;
using Olimpiada2016Judet.DataAcces;

namespace Olimpiada2016Judet.Forms
{
    public partial class Creare_cont_client : Form
    {
        public Creare_cont_client()
        {
            InitializeComponent();

            textBox5.PasswordChar = textBox4.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == textBox5.Text)
            { 
                UserModel utilizator = new UserModel
                {
                    Parola = textBox4.Text,
                    Nume = textBox1.Text,
                    Prenume = textBox2.Text,
                    Adresa = textBox3.Text,
                    Email = textBox6.Text
                };

                SqlDataAcces.RegistrareUtilizator(utilizator);
            }
            else
            {
                MessageBox.Show("Confirmare parola a esuat!");
            }

        }
    }
}
