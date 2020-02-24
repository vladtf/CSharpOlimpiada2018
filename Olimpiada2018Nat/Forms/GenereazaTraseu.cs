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

        public GenereazaTraseu()
        {
            InitializeComponent();

            regions = RegionModel.GetJudete();
        }

        private void GenereazaTraseu_Load(object sender, EventArgs e)
        {
            romaniaMare = RomaniaMap.GetRegion(regions[0].Name, RomaniaMap.RegionType.RomaniaMare);

            romaniaMap = MapRenderer.GetRomanianMap(romaniaMare, pictureBox1.ClientRectangle, regions);

            pictureBox1.Size = new Size(romaniaMap.Width, romaniaMap.Height);
            pictureBox1.Image = romaniaMap;
            pictureBox1.SendToBack();

        }


    }
}
