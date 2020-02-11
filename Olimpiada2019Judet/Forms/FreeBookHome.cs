using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2019Judet.DataAcces;

namespace Olimpiada2019Judet.Forms
{
    public partial class FreeBookHome : Form
    {
        public FreeBookHome()
        {
            InitializeComponent();
        }

        private void FreeBookHome_Load(object sender, EventArgs e)
        {
            InitializatorDB.Initializare(SqlDataAcces.ConnectionString);
        }
    }
}
