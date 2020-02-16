using Olimpiada2016Judet.Models;
using System;
using System.Windows.Forms;

namespace Olimpiada2016Judet.Forms
{
    public partial class Autentificare_client : Form
    {
        public Autentificare_client()
        {
            InitializeComponent();

            textBox2.PasswordChar = '*';

            textBox1.Text = textBox2.Text = "asd";
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
                (Owner as Start).Visible = false;
                page.Show();
                this.Close();
            }
        }
    }
}