using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olimpiada2015Judet.Forms
{
    public sealed class Singleton<T> where T : class, new()
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private Singleton() { }
    }
}
