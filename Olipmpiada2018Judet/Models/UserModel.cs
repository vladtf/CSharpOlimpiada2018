using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olipmpiada2018Judet.Models
{
    public class UserModel
    {
        public int IDUtilizator { get; set; }
        public string Nume { get; set; }
        public string Parola { get; set; }
        public string Email { get; set; }
        public string Clasa { get; set; }
    }
}
