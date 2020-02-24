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

        private static List<string> allRegionsName = new string[] {"RomaniaMare", "Transilvania", "Crisana", "Maramures", "Bucovina", "Moldova","Basarabia", "Dobrogea", "Muntenia", "Oltenia","Banat"}.ToList();

        public static List<RegionModel> GetJudete()
        {
            List<RegionModel> regions = new List<RegionModel>();

            for (int i = 0; i < allRegionsName.Count(); i++)
            {
                RegionModel region = RomaniaMap.GetRegion(allRegionsName[i], RomaniaMap.RegionType.Judet);
                regions.Add(region);
            }

            return regions;
        }

        public static List<string> GetNumeJudete()
        {
            return allRegionsName.Where((value, index)=> index>=1).ToList();
        }
    }
}
