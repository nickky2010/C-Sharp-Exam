using HW11.SportsmenClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.Comparer
{
    class AgeComparer : IComparer<Sportsmen>
    {
        public int Compare(Sportsmen x, Sportsmen y)
        {
            if (x == null && x == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;
            else 
                return -(x.Age.CompareTo(y.Age));
        }
    }
}
