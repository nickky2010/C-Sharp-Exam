using HW11.SportsmenClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11.Comparer
{
    class GymnastResultComparer : IComparer<Sportsmen>
    {
        public int Compare(Sportsmen x, Sportsmen y)
        {
            Gymnast g1 = x as Gymnast;
            Gymnast g2 = y as Gymnast;
            if (g1 == null && g2 == null)
                return 0;
            else if (g1 == null)
                return -1;
            else if (g2 == null)
                return 1;
            else
                return -(g1.GetResultCompetition().CompareTo(g2.GetResultCompetition()));
        }
    }
}
