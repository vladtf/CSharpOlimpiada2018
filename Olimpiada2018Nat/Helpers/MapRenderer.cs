using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OlimpiadaCsharp2018.Models;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OlimpiadaCsharp2018.DataProviders;

namespace OlimpiadaCsharp2018.Helpers
{
    public class MapRenderer
    {
        public static Bitmap GetRomanianMap(RegionModel romania, Rectangle rectangleDrawn, List<RegionModel> regions)
        {
            int width = romania.Points.Select(x => x.X).Max();
            int height = romania.Points.Select(x => x.Y).Max();

            Bitmap map = new Bitmap(width, height);

            using (Graphics gr = Graphics.FromImage(map))
            {
                DrawRegion(gr, romania, Color.Black);

                //GraphicsPath path = new GraphicsPath();
                //path.AddPolygon(pointsRomaniaMare.ToArray());

                using (PathGradientBrush brush = new PathGradientBrush(romania.Points.ToArray()))
                {
                    Color[] colors = { Color.Red, Color.Yellow, Color.Blue };
                    float[] relativePositions = { 0, 0.33f, 1 };

                    ColorBlend colorBlend = new ColorBlend();
                    colorBlend.Colors = colors;
                    colorBlend.Positions = relativePositions;

                    brush.InterpolationColors = colorBlend;

                    gr.FillRectangle(brush, rectangleDrawn);
                }

                //GraphicsPath path = new GraphicsPath();
                //path.AddPolygon(pointsRomaniaMare.ToArray());

                //Region region = new Region(path);

                //gr.FillRegion(brush,region);

                for (int i = 0; i < regions.Count; i++)
                {
                    RegionModel region = RomaniaMap.GetRegion(regions[i].Name, RomaniaMap.RegionType.Judet);
                    DrawRegion(gr, region, Color.White);
                }
            }

            return map;
        }

        private static void DrawRegion(Graphics gr, RegionModel region, Color color)
        {
            Pen pen = new Pen(color, 3);
            Point curr = region.Points[0];
            for (int i = 1; i < region.Points.Count; i++)
            {
                gr.DrawLine(pen, curr, region.Points[i]);
                curr = region.Points[i];
            }
        }

        public static Bitmap DrawCities(Bitmap romania, List<RegionModel> judete)
        {
            using (Graphics gr = Graphics.FromImage(romania))
            {

                Pen linePen = new Pen(Color.Green, 3);
                Pen cityPen = new Pen(Color.Blue, 2);
                SolidBrush cityBrush = new SolidBrush(Color.Blue);

                for (int i = 0; i < judete.Count; i++)
                {
                    RegionModel current = judete[i];
                    Point cityPosition = new Point(current.CapitalPosition.X, current.CapitalPosition.Y);
                    gr.DrawEllipse(cityPen, cityPosition.X, cityPosition.Y, 5, 5);
                    gr.DrawString(current.Capital, new Font("Arial", 8), cityBrush, cityPosition.X + 5, cityPosition.Y);

                }
            }

            return romania;
        }

        public static Bitmap DrawRoute(Bitmap romania, RegionModel regionStart, RegionModel regionEnd)
        {
            using (Graphics gr = Graphics.FromImage(romania))
            {
                Pen pen = new Pen(Color.Green, 3);

                gr.DrawLine(pen, regionEnd.CapitalPosition, regionStart.CapitalPosition);
            }

            return romania;
        }
    }
}
