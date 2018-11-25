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

    public class Swimmer : Sportsmen
    {
        //  Класс пловцов должен содержать дополнительное 

        //      поле-массив с результатами заплывов, 
        public double[] result;

        //  конструктор с параметрами
        public Swimmer(string surname, int age, KindOfSport sports, double[] result) : base(surname, age, sports)
        {
            this.result = result;
        }
        public Swimmer()
        {

        }
        //  свойства для чтения полей класса
        public double this[int i] { get => result[i]; }

        //      реализацию метода для определения лучшего результата соревнований, 
        public override double GetResultCompetition()
        {
            return (result.Min());
        }

        //      метод с переменным числом параметров, возвращающий средний результат за указанные заплывы
        //      (например, srednee(1,3) – средний результат за 1-й и 3-й заплывы, srednee(1) – время в 1-м заплыве и т.д.).
        public double GetAverageResult (params int[] number)
        {
            List<int> indexFoundResult = number.Select(i => i).Where(i=>i > 0 && i <= result.Length).Distinct().ToList();
            if (indexFoundResult.Count != 0)
            {
                double average = 0;
                for (int i = 0; i < indexFoundResult.Count; i++)
                {
                    average += result[indexFoundResult[i]-1];
                }
                return (average/indexFoundResult.Count);
            }
            else
                throw new Exception("Does not contain entered numbers");
        }
    }
}
