using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2016Judet.Models;

namespace Olimpiada2016Judet.Forms
{
    public partial class Autentificare_client : Form
    {
        public Autentificare_client()
        {
            InitializeComponent();

            textBox2.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserModel utilizator = DataAcces.SqlDataAcces.Autentificare(textBox1.Text, textBox2.Text);

            if (utilizator.Email == null)
            {
                MessageBox.Show("Eroare autentificare!");
            }
            else
            {
                var page = new Optiuni(utilizator);
                page.Show();
                this.Close();
            }
        }
    }
}
