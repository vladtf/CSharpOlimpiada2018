using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public int TipUtilizator { get; set; }
        public string NumePenume { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
    }
}
