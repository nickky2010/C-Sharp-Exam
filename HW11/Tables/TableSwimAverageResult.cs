using HW11.Collection;
using HW11.SportsmenClasses;

namespace HW11.Tables
{
    class TableSwimAverageResult : Table
    {
        public TableSwimAverageResult(string headTable = "", params Column[] column) : base(headTable, column)
        {
        }
        public void Print(string head, int[] number, ref SportmenCollection<Sportsmen> sportsmenCollection)
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
                            sportsmenCollection[i].Sports.ToString(), ((Swimmer)sportsmenCollection[i]).GetAverageResult(number).ToString() + " c");
                    }
                }
            }
            PrintBottom();
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
                            sportsmenCollection[i].Sports.ToString(), ((Swimmer)sportsmenCollection[i]).GetAverageResult().ToString() + " c");
                    }
                }
            }
            PrintBottom();
        }

    }
}
