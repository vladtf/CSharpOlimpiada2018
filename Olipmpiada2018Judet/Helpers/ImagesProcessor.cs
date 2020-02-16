using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Olipmpiada2018Judet.Helpers
{
    public static class ImagesProcessor
    {
        public static List<Bitmap> GetAllImages()
        {
            List<Bitmap> imagini = new List<Bitmap>();

            string directory = "imaginislideshow";

            string[] files = Directory.GetFiles(directory);

            foreach (string path in files)
            {
                Bitmap imagine = new Bitmap(path);
                imagini.Add(imagine);
            }

            return imagini;
        }
    }
}