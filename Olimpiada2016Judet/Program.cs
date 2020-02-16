using Olimpiada2016Judet.DataAcces;
using Olimpiada2016Judet.Forms;
using System;
using System.Windows.Forms;

namespace Olimpiada2016Judet
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            InitializareDB.Initializare();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Start.GetInsance());
        }
    }
}