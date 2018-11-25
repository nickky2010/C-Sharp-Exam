using HW11.SportsmenClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW11
{
    [Serializable]
    [XmlInclude(typeof(Gymnast))]
    [XmlInclude(typeof(Swimmer))]
    //  Класс «Спортсмен» должен быть абстрактным и содержать следующие элементы: 
    public abstract class Sportsmen: IComparable<Sportsmen>
    {
        //      поле-фамилия, 
        public string surname;
        //      поле - возраст, 
        public int age;
        //      поле вид спорта (гимнастика, бокс, плавание и т.д.), 
        public KindOfSport sports;
        // конструктор с параметрами
        public Sportsmen(string surname, int age, KindOfSport sports)
        {
            this.surname = surname;
            this.age = age;
            this.sports = sports;
        }
        public Sportsmen()
        {

        }
        // свойства для чтения полей класса
        public string Surname { get => surname; }
        public int Age { get => age; }
        public KindOfSport Sports { get => sports; }

        // метод для сравнения 
        public int CompareTo(Sportsmen other)
        {
            return (surname.CompareTo(other.surname));
        }

        //      абстрактный метод для определения лучшего результата соревнований.  
        public abstract double GetResultCompetition();
    }
}
