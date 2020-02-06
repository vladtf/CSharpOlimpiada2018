using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OlimpiadaCsharp2018.Models;
using OlimpiadaCsharp2018.Helpers;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class LogareCentenar : Form
    {
        public LogareCentenar()
        {
            InitializeComponent();

            //tabControl1.Appearance = TabAppearance.FlatButtons;
            //tabControl1.ItemSize = new Size(0, 1);
            //tabControl1.SizeMode = TabSizeMode.Fixed;

            //textBox1.Text = "popescu@gmail.com";
            //textBox2.Text = "popIon";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string parola = textBox2.Text;

            UserModel user = LoghingDB.Autentificare(email);

            if (user != null)
            {
                if (user.Parola == parola)
                {
                    (Tag as MainForm).Visible = true;
                    (Tag as MainForm).EsteAunteficat();
                    MessageBox.Show("Sunteti autentificat!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Eroare de autentificare!");
                }
            }
            else
            {
                MessageBox.Show("Eroare de autentificare!");
            }

            textBox2.Text = textBox1.Text = "";
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            (Tag as MainForm).Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBox1.Text))
            {
                UserModel user = LoghingDB.Autentificare(textBox1.Text);
                if (user != null)
                {
                    var page = new AmUitatParolaForm(user);
                    page.Tag = Tag;
                    page.Show();

                }
                else
                {
                    MessageBox.Show("Introduceti un email valid!");
                }
            }
            else
            {
                MessageBox.Show("Introduceti un email valid!");
            }
        }


    }
}
