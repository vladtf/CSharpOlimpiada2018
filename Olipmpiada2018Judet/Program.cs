using Olipmpiada2018Judet.DataAcces;
using System;
using System.Windows.Forms;

namespace Olipmpiada2018Judet
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
            Application.Run(new eLearning1918_Start());
        }
    }
}