using HW11.Collection;
using HW11.SportsmenClasses;
using HW11.Tables;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using HW11.Comparer;

namespace HW11
{
    class Menu
    {
        //Вертикальное меню
        private string[] menu;
        // исходные данные
        private Table[] table;
        private SportmenCollection<Sportsmen> sportsmenCollection;
        // служебные поля
        private bool isRun = true;
        private int current;
        private int last;

        public Menu(ref string[] menu, ref SportmenCollection<Sportsmen> filmCollection, ref Table[] table)
        {
            this.menu = new string[menu.Length + 1];
            for (int i = 0; i < menu.Length; i++)
            {
                this.menu[i] = menu[i];
            }
            this.menu[menu.Length] = "Выход";
            this.table = table;
            this.sportsmenCollection = filmCollection;
            isRun = true;
            current = 0;
            last = 0;
        }
        public void Show()
        {
            while (isRun)
            {
                //вывод меню
                BaseColor();
                Console.Clear();
                Console.CursorVisible = false;
                for (int i = 0; i < menu.Length; i++)
                {
                    ShowMenuItem(i, menu[i]);
                }
                // выбор пункта меню
                bool isNoEnter = true;
                while (isNoEnter)
                {
                    BaseColor();
                    ShowMenuItem(last, menu[last]);
                    LightColor();
                    ShowMenuItem(current, menu[current]);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Enter)
                        isNoEnter = false;
                    else
                        if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        last = current;
                        current = (current == menu.Length - 1) ? 0 : current + 1;
                    }
                    else
                        if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        last = current;
                        current = (current == 0) ? menu.Length - 1 : current - 1;
                    }
                }// конец тела цикла while (isNoEnter)

                // действие в соответствии с выбором пользователя
                switch (current)
                {
                    case 0: // Считать данные о фильмах
                        {
                            Clear();
                            Console.Write("Введите название считываемого файла: ");
                            //string source = Console.ReadLine();
                            string source = "Sportsmen.txt";
                            SportmenCollection<Sportsmen>.ReadFromFile(source, ref sportsmenCollection);
                            if(sportsmenCollection!=null)
                                Console.WriteLine("Данные из файла "+ source + " считаны успешно!");
                            else
                                Console.WriteLine("Данные из файла " + source + " не считаны!");
                            Wait();
                            break;
                        }
                    case 1: //Вывод информации на экран
                        {
                            Clear();
                            if (sportsmenCollection != null)
                                ((TableByTask)table[0]).Print("Информация о всех спортсменах", ref sportsmenCollection);
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 2: // Сортировка информации по убыванию возраста
                        {
                            Clear();
                            if (sportsmenCollection != null)
                            {
                                sportsmenCollection.Sort(new AgeComparer());
                                ((TableByTask)table[0]).Print("Сортировка информации по убыванию возраста", ref sportsmenCollection);
                            }
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 3: // Сериализация коллекции в двоичном формате
                        {
                            Clear();
                            if (sportsmenCollection != null)
                            {
                                string destonation = "bynary.dat";
                                try
                                {
                                    sportsmenCollection.Serialize(x => x is Sportsmen, destonation);
                                    Console.WriteLine("Объект успешно сериализован в файл " + destonation);
                                    SportmenCollection<Sportsmen> newCollection = new SportmenCollection<Sportsmen>(sportsmenCollection.Deserialize(destonation));
                                    Console.WriteLine("Объект успешно десериализован из файла " + destonation);
                                    ((TableByTask)table[0]).Print("Информация об объекте после десериализации из файла " + destonation, ref newCollection);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 4: // Информация о пловцах моложе 20 лет с указанием среднего результата в 1-м, 2-м и 5-м заплывах
                        {
                            Clear();
                            if (sportsmenCollection != null)
                            {
                                List<Sportsmen> sportsmenFound = sportsmenCollection.Find(x => x is Swimmer).Select(x => x).Where(x => x.Age < 20).ToList();
                                if (sportsmenFound.Count() != 0)
                                {
                                    SportmenCollection<Sportsmen> sportsman = new SportmenCollection<Sportsmen>(sportsmenFound);
                                    string searchSwim = "1 2 5";
                                    ((TableSwimAverageResult)table[1]).
                                        Print("Информация о пловцах моложе 20 лет с указанием среднего результата в 1-м, 2-м и 5-м заплывах", 
                                        SportmenCollection<Sportsmen>.ConvertStringIndex(searchSwim," "), ref sportsman);
                                }
                                else
                                    Console.WriteLine("Данные о пловцах моложе 20 лет отсутствуют.");
                            }
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 5: // Сравнение двух указанных гимнастов по результатам
                        {
                            Clear();
                            if (sportsmenCollection != null)
                            {
                                List<Sportsmen> sportsmenFound = sportsmenCollection.Find(x => x is Gymnast).Select(x => x).ToList();
                                if (sportsmenFound.Count() != 0)
                                {
                                    SportmenCollection<Sportsmen> sportsman = new SportmenCollection<Sportsmen>(sportsmenFound);

                                    sportsmenFound.Sort(new GymnastResultComparer());
                                    ((TableByTask)table[0]).Print("Сравнение двух указанных гимнастов по результатам (сортировка гимнастов)", ref sportsman);
                                }
                                else
                                    Console.WriteLine("Данные о гимнастах отсутствуют.");
                            }
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 6: // Сериализация информации о гимнастах в формате XML
                        {
                            Clear();
                            if (sportsmenCollection != null)
                            {
                                string destonation = "bynaryXML.xml";
                                try
                                {
                                    sportsmenCollection.SerializeToXML(x => x is Gymnast, destonation);
                                    Console.WriteLine("Объект успешно сериализован в файл " + destonation);
                                    SportmenCollection<Sportsmen> newCollection = new SportmenCollection<Sportsmen>(sportsmenCollection.DeserializeFromXML(destonation));
                                    Console.WriteLine("Объект успешно десериализован из файла " + destonation);
                                    ((TableByTask)table[0]).Print("Информация об объекте после десериализации из файла " + destonation, ref newCollection);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                                Console.WriteLine("Данные о спортсменах отсутствуют!");
                            Wait();
                            break;
                        }
                    case 7:
                        {
                            isRun = false;
                            break;
                        }
                }
            }// конец цикла while (isRun)
        }
        private static void BaseColor()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void LightColor()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        private static void ShowMenuItem(int itemIndex, string item)
        {
            Console.SetCursorPosition(25, 8 + itemIndex);
            Console.WriteLine(item);
        }
        private static void Clear()
        {
            BaseColor();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }
        private static void Wait(string message = "Для продолжения нажмите любую клавишу")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ReadKey();
        }
    }
}
