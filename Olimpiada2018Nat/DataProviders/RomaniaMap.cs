using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace OlimpiadaCsharp2018.DataProviders
{
    class RomaniaMap
    {
        public static List<Point> GetPoints()
        {
            List<Point> points = new List<Point>();

            string filePath = "Harti//RomaniaMare.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine().Trim().Split('*').Select(x => Int32.Parse(x)).ToList();

                    Point temp = new Point(line[0], line[1]);

                    points.Add(temp);
                }
            }

            return points;
        }
    }
}
