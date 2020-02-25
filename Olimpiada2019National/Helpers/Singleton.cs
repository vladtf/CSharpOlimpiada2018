using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2019National
{
    public class Singleton<T> where T : class, new()
    {
        private static Lazy<T> instance = new Lazy<T>(() => new T());

        private Singleton()
        {

        }

        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
