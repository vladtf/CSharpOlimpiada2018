using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static OlimpiadaCsharp2018.DataProviders.RomaniaMap;

namespace OlimpiadaCsharp2018.Models
{
    class RegionModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public Point CapitalPosition { get; set; }
        public List<Point> Points { get; set; }
        public RegionType RegionType { get; set; }
    }
}
