using OlimpiadaCsharp2018.DataProviders;
using OlimpiadaCsharp2018.Models;
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
        private Bitmap romaniaMap;
        private RegionModel romaniaMare;
        private List<string> regionsName;

        public GhicesteRegiunea()
        {
            InitializeComponent();

            regionsName = new string[] {"RomaniaMare", "Banat", "Basarabia", "Bucovina", "Crisana", "Dobrogea", "Maramures", "Moldova", "Muntenia", "Oltenia", "Transilvania" }.ToList();

        }

        protected override void OnLoad(EventArgs e)
        {
            romaniaMare = RomaniaMap.GetRegion(regionsName[0], RomaniaMap.RegionType.RomaniaMare);

            int width = romaniaMare.Points.Select(x => x.X).Max();
            int height = romaniaMare.Points.Select(x => x.Y).Max();

            romaniaMap = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(romaniaMap))
            {
                DrawRegion(gr, romaniaMare, Color.Black);

                //GraphicsPath path = new GraphicsPath();
                //path.AddPolygon(pointsRomaniaMare.ToArray());

                using (PathGradientBrush brush = new PathGradientBrush(romaniaMare.Points.ToArray()))
                {
                    Color[] colors = { Color.Red, Color.Yellow, Color.Blue };
                    float[] relativePositions = { 0, 0.33f, 1 };

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

                for (int i = 1; i < regionsName.Count(); i++)
                {
                    RegionModel region = RomaniaMap.GetRegion(regionsName[i], RomaniaMap.RegionType.Judet);

                    DrawRegion(gr, region, Color.White);
                }
            }

            pictureBox1.Size = new Size(width, height);
            pictureBox1.Image = romaniaMap;

        }

        private void DrawRegion(Graphics gr, RegionModel region, Color color)
        {
            Pen pen = new Pen(color, 3);
            Point curr = region.Points[0];
            for (int i = 1; i < region.Points.Count; i++)
            {
                gr.DrawLine(pen, curr, region.Points[i]);
                curr = region.Points[i];
            }
        }
    }
}
