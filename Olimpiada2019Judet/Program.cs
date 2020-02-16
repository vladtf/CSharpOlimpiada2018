using Olimpiada2019Judet.Forms;
using System;
using System.Windows.Forms;

namespace Olimpiada2019Judet
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FreeBookHome());
        }
    }
}