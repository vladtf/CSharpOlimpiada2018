using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Olimpiada2019National.Forms;

namespace Olimpiada2019National
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataAcces.SqlDataAcces.InitiateDB();

            Application.Run(Singleton<StartBiblioteca>.Instance);

        }
    }
}
