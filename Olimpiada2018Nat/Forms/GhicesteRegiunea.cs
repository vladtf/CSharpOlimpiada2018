using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class GhicesteRegiunea : Form
    {
        private Bitmap romaniaMare;
        private List<Point> pointsRomaniaMare;
        public GhicesteRegiunea()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            pointsRomaniaMare = DataProviders.RomaniaMap.GetPoints();
            pointsRomaniaMare.Add(pointsRomaniaMare[0]);

            int width = pointsRomaniaMare.Select(x => x.X).Max();
            int height = pointsRomaniaMare.Select(x => x.Y).Max();

            romaniaMare = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(romaniaMare))
            {
                Pen pen = new Pen(Color.Black, 3);
                Point curr = pointsRomaniaMare[0];
                for (int i = 1; i < pointsRomaniaMare.Count; i++)
                {
                    gr.DrawLine(pen, curr, pointsRomaniaMare[i]);
                    curr = pointsRomaniaMare[i];
                }

                //GraphicsPath path = new GraphicsPath();
                //path.AddPolygon(pointsRomaniaMare.ToArray());

                using (PathGradientBrush brush = new PathGradientBrush(pointsRomaniaMare.ToArray()))
                {
                    Color[] colors = { Color.Red, Color.Yellow, Color.Blue };
                    float[] relativePositions = { 0, 0.33f , 1};

                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Colors = colors;
                    colorBlend.Positions = relativePositions;

                    brush.InterpolationColors = colorBlend;

                    gr.FillRectangle(brush, pictureBox1.ClientRectangle);
                }


                //GraphicsPath path = new GraphicsPath();
                //path.AddPolygon(pointsRomaniaMare.ToArray());

                //Region region = new Region(path);

                //gr.FillRegion(brush,region);
            }



            pictureBox1.Size = new Size(width, height);
            pictureBox1.Image = romaniaMare;

        }


    }
}
