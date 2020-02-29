using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.DataProvider
{
    class ImageProvider
    {
        public static Bitmap GetImage(int id)
        {
            string filePath = "Resurse//Imagini//utilizatori//" + id.ToString() + ".jpg";

            Bitmap bitmap = new Bitmap(filePath);

            return bitmap;
        }
    }
}
