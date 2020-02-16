using System;
using System.Collections.Generic;
using System.Linq;

namespace Olipmpiada2018Judet.Models
{
    public class MarkModel
    {
        public int Nota { get; set; }
        public DateTime Data { get; set; }

        public static List<int> ToateNotele = new List<int>();

        public static int NotaMedie
        {
            get
            {
                return ToateNotele.Sum() / ToateNotele.Count;
            }
        }
    }
}