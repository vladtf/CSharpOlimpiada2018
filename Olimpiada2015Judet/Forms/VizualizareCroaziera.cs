using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Olimpiada2015Judet.DataAcces;

namespace Olimpiada2015Judet.Forms
{
    public partial class VizualizareCroaziera : Form
    {
        private List<Point> listaPoints;
        public VizualizareCroaziera(List<int> listaPorturi)
        {
            InitializeComponent();

            listaPoints = SqlDataAcces.GetListaPorturi(listaPorturi);

            InitiateMap();
        }

        private void InitiateMap()
        {
            Bitmap map = new Bitmap(pictureBox1.Image, new Size(800, 600));

            Pen pen = new Pen(Color.Red, 5);
            using (Graphics gr = Graphics.FromImage(map))
            {
                Point currentPoint = listaPoints[0];

                for (int i = 1; i < listaPoints.Count; i++)
                {
                    gr.DrawLine(pen, currentPoint, listaPoints[i]);

                    currentPoint = listaPoints[i];
                }
                gr.DrawLine(pen, currentPoint, listaPoints[0]);
            }

            pictureBox1.Image = map;
        }
    }
}
