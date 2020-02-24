using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using OlimpiadaCsharp2018.Models;

namespace OlimpiadaCsharp2018.DataProviders
{
    public class RomaniaMap
    {
        public enum RegionType
        {
            RomaniaMare, Judet
        }
        public static RegionModel GetRegion(string fileName, RegionType regionType)
        {
            RegionModel regiune = new RegionModel
            {
                Name = fileName,
                RegionType = regionType,
                Points = new List<Point>()
            };

            string filePath = "Harti//" + fileName + ".txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                if (regionType == RegionType.Judet)
                {
                    var line = reader.ReadLine().Trim().Split('*');
                    Point capitala = new Point(Int32.Parse(line[0]), Int32.Parse(line[1]));

                    regiune.CapitalPosition = capitala;
                    regiune.Capital = line.Count() == 3 ? line[2] : "";
                }
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Trim().Split('*').Select(x => Int32.Parse(x)).ToList();

                    Point temp = new Point(line[0], line[1]);

                    regiune.Points.Add(temp);
                }
            }

            return regiune;
        }
    }
}
