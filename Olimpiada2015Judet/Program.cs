using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Olimpiada2015Judet.Forms;

namespace Olimpiada2015Judet
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
            Application.Run(Singleton<Autentificare>.Instance);
        }
    }
}
