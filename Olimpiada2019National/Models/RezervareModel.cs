using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.Models
{
    public class RezervareModel
    {
        public int IdRezerare { get; set; }
        public int IdCititor { get; set; }
        public int IdCarte { get; set; }
        public DateTime DataRezervare { get; set; }
        public int StatusRezervare { get; set; }
    }
}