using System;

namespace Olimpiada2019Judet.Models
{
    public class ImprumutModel
    {
        public int Index { get; set; }
        public int IDCarte { get; set; }
        public string Titlu { get; set; }
        public string Autor { get; set; }
        public DateTime DataImprumut { get; set; }
        public DateTime DataDisponibilitate { get; set; }
    }
}