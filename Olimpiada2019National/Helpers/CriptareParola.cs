using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2019National.Helpers
{
    public class CriptareParola
    {
        public static string Criptare(string parola)
        {
            StringBuilder cryptedPass = new StringBuilder("");

            foreach (char item in parola)
            {
                if (item >= 'a' && item < 'z')
                {
                    cryptedPass.Append((char)(item + 1));
                }
                else if (item == 'z')
                {
                    cryptedPass.Append("a");
                }
                else if (item > 'A' && item <= 'Z')
                {
                    cryptedPass.Append((char)(item - 1));
                }
                else if (item == 'A')
                {
                    cryptedPass.Append("Z");
                }
                else if (item >= '0' && item <= '9')
                {
                    cryptedPass.Append((char)('9' - Int32.Parse(item.ToString())));
                }
                else
                {
                    cryptedPass.Append(item);
                }

            }

            return cryptedPass.ToString();
        }
    }
}
