using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojDziennikv4.Models
{
    public class Pair<T,V>
    {
        public T first { get; set; }
        public V secound { get; set; }
        public Pair(T firstValue,V secoundValue)
        {
            first=firstValue;
            secound = secoundValue;
        }
    }
}