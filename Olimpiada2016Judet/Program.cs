using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Olimpiada2016Judet.Forms;
using Olimpiada2016Judet.DataAcces;

namespace Olimpiada2016Judet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializareDB.Initializare();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Start.GetInsance());

        }
    }
}
