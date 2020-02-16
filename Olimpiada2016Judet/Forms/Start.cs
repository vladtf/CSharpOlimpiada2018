using System;
using System.Windows.Forms;

namespace Olimpiada2016Judet.Forms
{
    public partial class Start : Form
    {
        private static Start instance;

        public Start()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var page = new Creare_cont_client();
            page.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var page = new Autentificare_client();
            page.ShowDialog(this);
        }

        public static Start GetInsance()
        {
            if (instance == null)
                instance = new Start();
            return instance;
        }
    }
}