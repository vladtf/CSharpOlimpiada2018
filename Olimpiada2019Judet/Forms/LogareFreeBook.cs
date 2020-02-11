﻿using System;
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
                MessageBox.Show("Logare cu succes!");
            }
            else
            {
                MessageBox.Show("Eroare autentificare!");
            }
        }
    }
}
