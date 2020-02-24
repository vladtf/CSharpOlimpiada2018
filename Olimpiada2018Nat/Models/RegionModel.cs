using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OlimpiadaCsharp2018.DataProviders;

namespace OlimpiadaCsharp2018.Models
{
    class RegionModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public Point CapitalPosition { get; set; }
        public List<Point> Points { get; set; }
        public RomaniaMap.RegionType RegionType { get; set; }
    }
}
