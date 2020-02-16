using System;
using System.Data;

namespace OlimpiadaCSharp.Models
{
    public class DayModel
    {
        public string Nume { get; set; }
        public DateTime DataStart { get; set; }
        public DateTime DataStop { get; set; }
        public string Frecvneta { get; set; }
        public int Ziua { get; set; }

        //transforma un rand din tabela in DayModel
        //generatoare de excpetii!?!
        public static DayModel Parse(DataRow initial)
        {
            DayModel rez = new DayModel();

            rez.Nume = initial[0].ToString();
            rez.Frecvneta = initial[3].ToString();
            if (initial[3].ToString() == "ocazional")
            {
                rez.DataStart = (DateTime)initial[1];
                rez.DataStop = (DateTime)initial[2];
            }
            else
            {
                rez.Ziua = Int32.Parse(initial[4].ToString());
            }

            return rez;
        }

        //parseaza in daymodel din tabela de itinerariu deja procesata
        public static DayModel ParseFinal(DataRow initial)
        {
            DayModel rez = new DayModel();

            rez.Nume = initial[0].ToString();
            rez.Frecvneta = initial[3].ToString();
            rez.DataStart = (DateTime)initial[1];
            rez.DataStop = (DateTime)initial[2];

            return rez;
        }
    }
}