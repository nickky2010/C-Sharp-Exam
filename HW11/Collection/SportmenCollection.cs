using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text.RegularExpressions;
using HW11.SportsmenClasses;

namespace HW11.Collection
{
    //  Создать класс-коллекцию(generic) с необходимой функциональностью. 
    [Serializable]
    public class SportmenCollection<T> where T : Sportsmen
    {
        public List<T> collection;
        public SportmenCollection()
        {
            collection = new List<T>();
        }
        public SportmenCollection(List<T> sportsmenCollection)
        {
            collection = sportsmenCollection;
        }
        public SportmenCollection(params T[] sportsmen)
        {
            collection = sportsmen.ToList();
        }
        public int Lenght => collection.Count;          //  количество элементов
        public T this[int i] => collection[i];          //  индексатор доступа
        public void Add(T elem)                         //  добавление элемента
        {
            collection.Add(elem);
        }
        public void Remove(int index)                   //  удаление элемента
        {
            if (index >= 0 && index < collection.Count)
                collection.RemoveAt(index);
            else
                throw new Exception("Error!!! The collection does not contain an item at a given index. Unable to delete");
        }

        public void Sort(IComparer<T> comparer = null)
        {
            collection.Sort(comparer);
        }

        // метод для поиска информации по заданному критерию (критерий передавать через параметр-делегат: стандартный или созданный).
        public static List<T> Find(List<T> collection, Predicate<T> criterion)
        {
            return (collection.FindAll(criterion));
        }
        public List<T> Find(Predicate<T> criterion)
        {
            return (collection.FindAll(criterion));
        }

        //  Предусмотреть метод для сериализации объекта класса в двоичном формате(параметры – имя файла, форматер). 
        public void Serialize(Predicate<T> criterion, string destonation)
        {
            // сериализация
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = File.Create(destonation))
            {
                formatter.Serialize(fs, Find(criterion).ToArray());
            }
        }
        // метод для десериализации в двоичном формате
        public List<T> Deserialize(string source)
        {
            // десериализация 
            BinaryFormatter formatter = new BinaryFormatter();
            T[] sportsmen;
            using (FileStream fs = new FileStream(source, FileMode.Open, FileAccess.Read))
            {
                sportsmen = formatter.Deserialize(fs) as T[];
            }
            return (sportsmen.ToList());
        }

        //  Перегрузить этот метод для сериализации объекта класса в формате XML.
        public void SerializeToXML(Predicate<T> criterion, string destonation)
        {
            // сериализация
            XmlSerializer serializer = new XmlSerializer(typeof(List<Sportsmen>));
            using (FileStream fs = File.Create(destonation))
            {
                serializer.Serialize(fs, Find(criterion));
            }
        }
        // метод для десериализации в формате XML
        public List<T> DeserializeFromXML(string source)
        {
            // десериализация 
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            List<T> sportsmen;
            using (FileStream fs = File.OpenRead(source))
            {
                sportsmen = serializer.Deserialize(fs) as List<T>;
            }
            return (sportsmen);
        }

        //  Предусмотреть метод для сериализации объектов с выбранной информацией в формате SOAP.
        public void SerializeToSOAP(Predicate<T> criterion, string destonation)
        {
            // сериализация
            SoapFormatter formatter = new SoapFormatter();
            using (FileStream fs = File.Create(destonation))
            {
                formatter.Serialize(fs, Find(criterion).ToArray());
            }
        }

        // метод для десериализации
        public List<T> DeserializeFromSOAP(string source)
        {
            // десериализация 
            SoapFormatter formatter = new SoapFormatter();
            T[] films;
            using (FileStream fs = File.OpenRead(source))
            {
                films = formatter.Deserialize(fs) as T[];
            }
            return (films.ToList());
        }

        // метод для чтения из файла
        public static void ReadFromFile(string source, ref SportmenCollection<Sportsmen> SportmenCollection)
        {
            Regex regex = new Regex(";");
            string[] fields;
            using (StreamReader inStream = new StreamReader(source, Encoding.Default))
            {
                try
                {
                    while (!inStream.EndOfStream)
                    {
                        fields = regex.Split(inStream.ReadLine());
                        int sports = int.Parse(fields[2]);
                        switch (sports)
                        {
                            case 0:
                                {
                                    SportmenCollection.Add(new Gymnast(fields[0], int.Parse(fields[1]), (KindOfSport)sports, double.Parse(fields[3]),
                                        double.Parse(fields[4]), double.Parse(fields[5])));
                                }
                                break;
                            case 1:
                                {
                                    double[] costOnSeries = new double[fields.Length - 3];
                                    for (int i = 3, j = 0; i < fields.Length; i++, j++)
                                    {
                                        costOnSeries[j] = double.Parse(fields[i]);
                                    }
                                    SportmenCollection.Add(new Swimmer (fields[0], int.Parse(fields[1]), (KindOfSport)sports, costOnSeries));
                                }
                                break;
                        }
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    SportmenCollection = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    SportmenCollection = null;
                }
            }
        }

        public static int[] ConvertStringIndex (string search, string separator)
        {
            Regex regex = new Regex(separator);
            string[] fields;
            fields = regex.Split(search);
            int n = fields.Count();
            int[] index = new int[n];
            for (int i = 0; i < n; i++)
            {
                index[i] = int.Parse(fields[i]);
            }
            return index;
        }
    }
}
