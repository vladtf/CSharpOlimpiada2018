using System.Drawing;
using System.Windows.Forms;

namespace Olipmpiada2018Judet.Forms
{
    public partial class RaportForm : Form
    {
        public RaportForm()
        {
            InitializeComponent();
        }

        public void AddRow(string raspuns, string raspunsCorect)
        {
            tableLayoutPanel1.Controls.Add(new Label { Text = raspuns, Font = new Font(label1.Font.FontFamily, 12) });
            tableLayoutPanel1.Controls.Add(new Label { Text = raspunsCorect, Font = new Font(label1.Font.FontFamily, 12) });
        }
    }
}