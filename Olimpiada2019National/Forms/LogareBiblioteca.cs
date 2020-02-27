using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2019National.Models;
using Olimpiada2019National.DataAcces;
using Olimpiada2019National.Helpers;

namespace Olimpiada2019National.Forms
{
    public partial class LogareBiblioteca : Form
    {
        public LogareBiblioteca()
        {
            InitializeComponent();

            textBox2.PasswordChar = '*';

            textBox1.Text =  "tutor@gmail.com";
            textBox2.Text = "tudor";
        }

        protected override void OnClosed(EventArgs e)
        {
            Singleton<StartBiblioteca>.Instance.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string parola = CriptareParola.Criptare(textBox2.Text);

            if( String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Introduceti datele.");
                return;
            }

            UserModel utilizator = SqlDataAcces.Autentificare(email, parola);

            if (utilizator.Parola == parola)
            {
                MessageBox.Show("Autentificat");
            }
            else
            {
                MessageBox.Show("Email sau parola gresita!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
