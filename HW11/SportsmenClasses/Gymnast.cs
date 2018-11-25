using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW11.SportsmenClasses
{
    [Serializable]
    [XmlInclude(typeof(Gymnast))]
    [XmlInclude(typeof(Swimmer))]

    public class Gymnast : Sportsmen
    {
        //  Класс гимнастов должен содержать дополнительные поля: 

        //      оценка за упражнения на кольцах
        public double markRings;
        //      оценка на брусьях
        public double markBars;
        //      оценка за опорный прыжок
        public double vault;
        // конструктор с параметрами
        public Gymnast(string surname, int age, KindOfSport sports, double markRings, double markBars, double vault) :
            base(surname, age, sports)
        {
            this.markRings = markRings;
            this.markBars = markBars;
            this.vault = vault;
        }
        public Gymnast()
        {

        }
        //      реализацию метода для определения лучшего результата соревнований, 
        public override double GetResultCompetition()
        {
            double[] arr = new double[] { markRings, markBars, vault };
            return (arr.Max());
        }
        //      операции <  и  > для сравнения гимнастов по результатам.

        public static bool operator <(Gymnast g1, Gymnast g2)
        {
            if (g1.GetResultCompetition() < g2.GetResultCompetition()) return true;
            else return false;
        }
        public static bool operator >(Gymnast g1, Gymnast g2)
        {
            if (g1.GetResultCompetition() > g2.GetResultCompetition()) return true;
            else return false;
        }
    }
}
