using System.Collections.Generic;

namespace OlimpiadaCSharp.Models
{
    public class LocationModel
    {
        // Modelul echivalent tabelei localitati
        public int Id { get; set; }
        public string Localitate { get; set; }

        //imaginile corespunzatoare fiecarei localitati
        public List<string> Imagini { get; set; }

    }
}
