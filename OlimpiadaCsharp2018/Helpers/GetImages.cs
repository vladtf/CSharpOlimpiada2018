using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace OlimpiadaCsharp2018.Helpers
{
    public class GetImages
    {
        public static List<Bitmap> GetAllImages()
        {
            List<Bitmap> imagini = new List<Bitmap>();

            string filePath = "Captcha\\";

            for (int i = 1; i <= 20; i++)
            {
                Bitmap imagineNoua = new Bitmap((string)(filePath + i.ToString() + ".jpg"));
                imagineNoua.Tag = i;
                imagini.Add(imagineNoua);
            }

            return imagini;
        }

        public static List<int> ImaginiOameni()
        {
            List<string> listOamnei = new List<string>();
            string filePath = "oameni.txt";

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.Peek() >= 0)
                {
                    string str = reader.ReadLine().Split('.').First();
                    listOamnei.Add(str);
                }
            }

            return listOamnei.Select(x=>Int32.Parse(x)).ToList();
        }
    }
}
