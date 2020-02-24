using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OlimpiadaCsharp2018.DataProviders;

namespace OlimpiadaCsharp2018.Models
{
    public class RegionModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public Point CapitalPosition { get; set; }
        public List<Point> Points { get; set; }
        public RomaniaMap.RegionType RegionType { get; set; }

        private static List<string> allRegionsName = new string[] {"RomaniaMare", "Banat", "Basarabia", "Bucovina", "Crisana", "Dobrogea", "Maramures", "Moldova", "Muntenia", "Oltenia", "Transilvania" }.ToList();

        internal static List<RegionModel> GetJudete()
        {
            List<RegionModel> regions = new List<RegionModel>();

            for (int i = 0; i < allRegionsName.Count(); i++)
            {
                RegionModel region = RomaniaMap.GetRegion(allRegionsName[i], RomaniaMap.RegionType.Judet);
                regions.Add(region);
            }

            return regions;
        }
    }
}
