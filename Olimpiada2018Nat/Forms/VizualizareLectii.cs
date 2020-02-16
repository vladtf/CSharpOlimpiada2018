using OlimpiadaCsharp2018.Helpers;
using OlimpiadaCsharp2018.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class VizualizareLectii : Form
    {
        private List<LectieModel> lectii;

        public VizualizareLectii()
        {
            InitializeComponent();
            lectii = DataAcces.GetLectii();
            listBox1.Items.AddRange(lectii.Select(x => x.NumeImagine.Split('.')[0]).ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                LectieModel lectiaSelectata = lectii.Find(x => x.IdLectie == listBox1.SelectedIndex + 1);
                Bitmap imageToDisplay = new Bitmap("ContinutLectii\\" + lectiaSelectata.NumeImagine);
                pictureBox1.Image = imageToDisplay;

                UserModel utilizatorCreator = DataAcces.GetUser(lectiaSelectata.IdUtilizator);

                label4.Text = utilizatorCreator.Nume;
                label5.Text = utilizatorCreator.Email;
                label6.Text = lectiaSelectata.Regiune;
                label7.Text = lectiaSelectata.Datacreare.ToString();
            }
            else
            {
                MessageBox.Show("Selectati lectia!");
            }
        }
    }
}