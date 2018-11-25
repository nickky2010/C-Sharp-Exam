using HW11.Collection;
using HW11.SportsmenClasses;
using System;

namespace HW11.Tables
{
    class TableByTask : Table
    {
        public TableByTask(string headTable = "", params Column[] column) : base(headTable, column)
        {
        }
        public void Print(string head, ref SportmenCollection<Sportsmen> sportsmenCollection)
        {
            HeadTable = head;
            PrintHead();
            for (int i = 0, j = 0; i < sportsmenCollection.Lenght; i++)
            {
                if (sportsmenCollection[i] != null)
                {
                    if (sportsmenCollection[i] is Swimmer)
                    {
                        PrintString((++j).ToString(), sportsmenCollection[i].Surname, sportsmenCollection[i].Age.ToString(),
                            sportsmenCollection[i].Sports.ToString(), ((Swimmer)sportsmenCollection[i]).GetResultCompetition().ToString()+" c");
                    }
                    else if (sportsmenCollection[i] is Gymnast)
                    {
                        PrintString((++j).ToString(), sportsmenCollection[i].Surname, sportsmenCollection[i].Age.ToString(),
                            sportsmenCollection[i].Sports.ToString(), ((Gymnast)sportsmenCollection[i]).GetResultCompetition().ToString() + " баллов");
                    }
                }
            }
            PrintBottom();
        }
    }
}
