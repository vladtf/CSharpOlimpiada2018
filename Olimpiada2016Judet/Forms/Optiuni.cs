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
    public partial class Optiuni : Form
    {
        public Optiuni(UserModel utilizator)
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int s = int.Parse(textBox1.Text) + int.Parse(textBox2.Text) + int.Parse(textBox3.Text);

            if (s < 250)
            {
                textBox4.Text = "1800";
            }
            else if (s<=250)
            {
                textBox4.Text = "2200";
            }
            else
            {
                textBox4.Text = "2500";
            }

        }

        
    }
}
