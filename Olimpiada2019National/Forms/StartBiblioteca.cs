﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Olimpiada2019National.Forms
{
    public partial class StartBiblioteca : Form
    {
        public StartBiblioteca()
        {
            InitializeComponent();
        }

        private void Logare_Click(object sender, EventArgs e)
        {
            Singleton<LogareBiblioteca>.Instance.Show();

            this.Hide();
        }
        
    }
}
