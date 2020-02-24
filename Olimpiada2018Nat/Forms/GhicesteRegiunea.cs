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
using OlimpiadaCsharp2018.Helpers;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class GhicesteRegiunea : Form
    {
        private Bitmap romaniaMap;
        private RegionModel romaniaMare;
        private List<RegionModel> regions;
        private TextBox currentQuestion;

        public GhicesteRegiunea()
        {
            InitializeComponent();

            regions = RegionModel.GetJudete();

            button2.Enabled = button3.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            romaniaMare = regions[0];

            romaniaMap = MapRenderer.GetRomanianMap(romaniaMare, pictureBox1.ClientRectangle, regions);

            pictureBox1.Size = new Size(romaniaMap.Width, romaniaMap.Height);
            pictureBox1.Image = romaniaMap;
            pictureBox1.SendToBack();

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
