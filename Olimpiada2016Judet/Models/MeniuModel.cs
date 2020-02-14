using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2016Judet.Models
{
    public class MeniuModel
    {
        public int Id { get; set; }
        public string DenumereaProdus { get; set; }
        public string Descriere { get; set; }
        public int Pret { get; set; }
        public int KCal { get; set; }
        public int Felul { get; set; }
    }
}
