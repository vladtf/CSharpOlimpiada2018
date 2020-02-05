using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OlimpiadaCsharp2018.Models
{
    public class LectieModel
    {
        public int IdLectie { get; set; }
        public int IdUtilizator { get; set; }
        public string TitluLectie { get; set; }
        public string Regiune { get; set; }
        public DateTime Datacreare { get; set; }
        public string NumeImagine { get; set; }
    }
}
