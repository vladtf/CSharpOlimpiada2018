using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.Models
{
    public class ImprumutModel
    {
        public int IdImprumut { get; set; }
        public int IdCititor { get; set; }
        public int IdCarte { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime DataRestituire { get; set; }
        public BookModel Carte { get; set; }
    }
}