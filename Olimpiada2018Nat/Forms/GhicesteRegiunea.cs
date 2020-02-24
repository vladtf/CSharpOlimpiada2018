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
        private List<RegionModel> regions;
        private TextBox currentQuestion;

        public GhicesteRegiunea()
        {
            InitializeComponent();

            regionsName = new string[] {"RomaniaMare", "Banat", "Basarabia", "Bucovina", "Crisana", "Dobrogea", "Maramures", "Moldova", "Muntenia", "Oltenia", "Transilvania" }.ToList();

            regions = new List<RegionModel>();

            button2.Enabled = button3.Enabled = false;
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
                    regions.Add(region);

                    DrawRegion(gr, region, Color.White);
                }
            }

            pictureBox1.Size = new Size(width, height);
            pictureBox1.Image = romaniaMap;
            pictureBox1.SendToBack();

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

        private void QuestionRegion(RegionModel region)
        {
            
            TextBox response = new TextBox();
            //response.Location = new Point(0, 0);
            response.Location = new Point(pictureBox1.Location.X + region.CapitalPosition.X, pictureBox1.Location.Y + region.CapitalPosition.Y);

            response.Font = new Font("Arial", 10);
            response.Size = new Size(80,10);
            response.BackColor = Color.White;
            response.Tag = region.Name;

            this.Controls.Add(response);

            response.BringToFront();

            currentQuestion = response;

            regions.Remove(region);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;

            QuestionRegion(regions[new Random().Next(0,regions.Count - 1)]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentQuestion.Text == (string)currentQuestion.Tag)
            {
                textBox1.Text = (Int32.Parse(textBox1.Text) + 1).ToString();
                currentQuestion.BackColor = Color.LightGreen;
            }
            else
            {
                currentQuestion.BackColor = Color.Red;
                currentQuestion.Text = (string)currentQuestion.Tag;

                using (Font font = new Font(currentQuestion.Font, FontStyle.Strikeout))
                {
                    currentQuestion.Font = font;
                }
            }
            currentQuestion.ForeColor = Color.White;
            currentQuestion.Enabled = false;

            if (regions.Count < 1)
            {
                button3.Enabled = true;
                button2.Enabled = false;
                return;
            }

            QuestionRegion(regions[new Random().Next(0, regions.Count - 1)]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var page = new Diploma(Int32.Parse(textBox1.Text));
            page.Show();
        }
    }
}
