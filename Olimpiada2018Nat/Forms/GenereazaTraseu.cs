using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OlimpiadaCsharp2018.Models;
using OlimpiadaCsharp2018.DataProviders;
using OlimpiadaCsharp2018.Helpers;

namespace OlimpiadaCsharp2018.Forms
{
    public partial class GenereazaTraseu : Form
    {
        private Bitmap romaniaMap;
        private RegionModel romaniaMare;
        private List<RegionModel> regions;
        private Timer timer;
        private List<RegionModel> regionsRoute;
        private int currentRegion;
        private Bitmap initialMap;

        public GenereazaTraseu()
        {
            InitializeComponent();

            regions = RegionModel.GetJudete();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(RegionModel.GetNumeJudete().ToArray());

            timer = new Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (currentRegion == regionsRoute.Count)
            {
                timer.Stop();
                return;
            }

            romaniaMap = MapRenderer.DrawRoute(romaniaMap, regionsRoute[currentRegion], regionsRoute[currentRegion - 1]);
            pictureBox1.Image = romaniaMap;

            currentRegion++;
        }

        private void GenereazaTraseu_Load(object sender, EventArgs e)
        {
            romaniaMare = RomaniaMap.GetRegion(regions[0].Name, RomaniaMap.RegionType.RomaniaMare);

            romaniaMap = MapRenderer.GetRomanianMap(romaniaMare, pictureBox1.ClientRectangle, regions);
            romaniaMap = MapRenderer.DrawCities(romaniaMap, regions.Where((value, index) => index >= 1).ToList());

            pictureBox1.Size = new Size(romaniaMap.Width, romaniaMap.Height);

            initialMap = new Bitmap(romaniaMap);
            pictureBox1.Image = romaniaMap;
            pictureBox1.SendToBack();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            romaniaMap = new Bitmap(initialMap);
            int indexSelected = listBox1.SelectedIndex;
            if (indexSelected == -1)
            {
                MessageBox.Show("Alegeti o capitala!");
                return;
            }

            regionsRoute = new List<RegionModel>();
            for (int i = indexSelected+1; i < regions.Count; i++)
            {
                regionsRoute.Add(regions[i]);
            }
            for (int i = 1; i <= indexSelected+1; i++)
            {
                regionsRoute.Add(regions[i]);
            }

            currentRegion = 1;

            timer.Start();
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
