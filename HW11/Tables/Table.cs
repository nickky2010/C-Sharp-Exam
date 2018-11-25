using System;
using System.Linq;

namespace HW11.Tables
{
    //  Создать класс «Таблица», описывающий объекты-таблицы из двух столбцов, содержащий следующие элементы:
    abstract class Table
    {
        //  Создаем класс «Столбец», описывающий объекты-столбцы
        public class Column
        {
            public string HeadCol { get; set; }  // заголовок столбца
            public int LenghtCol { get; set; }   // ширина столбца
                                                 //  Конструктор с параметрами
            public Column(string headCol = "", int lenghtCol = 1)
            {
                HeadCol = headCol;
                //if (lenghtCol>1)
                //    LenghtCol = lenghtCol;
                if (headCol.Length >= lenghtCol)      
                    LenghtCol = headCol.Length;       
                else 
                    LenghtCol = lenghtCol;
            }
        }
        //  Закрытые поля: заголовок таблицы, заголовки столбцов, ширина первого столбца, ширина второго столбца.
        protected string HeadTable { get; set; }       // заголовок таблицы
        protected int TotalLenghtCol { get; set; }     // общая длина колонок
        protected int CountCol { get; set; }           // количество колонок
        protected int LenghtTable { get; set; }        // длина таблицы (без учета 2 боковых линий=+2)
        protected Column[] column;
        //  Конструктор с параметрами.
        protected Table(string headTable = "", params Column[] column)
        {
            HeadTable = headTable;
            // считаем данные по колонкам
            CountCol = column.Count();
            this.column = new Column[CountCol];
            for (int i = 0; i < CountCol; i++)
            {
                this.column[i] = new Column();
                this.column[i] = column[i];
                TotalLenghtCol += this.column[i].LenghtCol;
            }
            LenghtTable = TotalLenghtCol + CountCol - 1;
        }
        //  Метод для вывода шапки таблицы.
        protected void PrintHead(bool printHeadTable = true)
        {
            // первая строчка
            if (printHeadTable)
            {
                Console.Write("╔");
                for (int i = 0; i < LenghtTable; i++)
                    Console.Write("═");
                Console.WriteLine("╗");
                // вторая строчка - заголовок таблицы
                ShowCol(LenghtTable - ShowColLeft(LenghtTable, HeadTable) - HeadTable.Length, HeadTable, LenghtTable);
                Console.WriteLine("║");
                // третья строчка
                Console.Write("╠");
            }
            else
                Console.Write("╔");
            for (int i = 0; i < CountCol; i++)
            {
                for (int m = 0; m < column[i].LenghtCol; m++)
                    Console.Write("═");
                if (i!= CountCol-1)
                    Console.Write("╦");
            }
            if (printHeadTable)
                Console.WriteLine("╣");
            else
                Console.WriteLine("╗");
            // четвертая строчка заголовки столбцов
            for (int i = 0; i < CountCol; i++)
            {
                ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, column[i].HeadCol) - column[i].HeadCol.Length, column[i].HeadCol, column[i].LenghtCol);
            }
            Console.WriteLine("║");
            // пятая строчка
            Console.Write("╠");
            for (int i = 0; i < CountCol; i++)
            {
                for (int m = 0; m < column[i].LenghtCol; m++)
                    Console.Write("═");
                if (i != CountCol - 1)
                    Console.Write("╬");
                else
                    Console.WriteLine("╣");
            }
        }

        //  Метод для вывода строки таблицы (возможные входные параметры: string, int, double, decimal).
        /// <summary>
        /// Метод для вывода строки таблицы (возможные входные параметры: string, int, double, decimal).
        /// </summary>
        /// <param name="value"></param>
        protected void PrintString(params string [] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountCol; i++)
            {
                // если количество колонок меньше или равно количеству переданных значений то записываем все значения (остальные будут отброшены)
                if(i< countValue)
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, value[i]) - value[i].Length, value[i], column[i].LenghtCol);
                // если количество переданных значений меньше количества колонок, то колонки пустые
                else
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, "") - 0, "", column[i].LenghtCol);
            }
            Console.WriteLine("║");
        }

        protected void PrintString(params int[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountCol; i++)
            {
                if (i < countValue)
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), column[i].LenghtCol);
                else
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, "") - 0, "", column[i].LenghtCol);
            }
            Console.WriteLine("║");
        }

        //  Переопределенный метод для вывода строки таблицы (входные параметры – double).
        protected void PrintString(params double[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountCol; i++)
            {
                if (i < countValue)
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), column[i].LenghtCol);
                else
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, "") - 0, "", column[i].LenghtCol);
            }
            Console.WriteLine("║");
        }

        //  Переопределенный метод для вывода строки таблицы (входные параметры – decimal).
        protected void PrintString(params decimal[] value)
        {
            int countValue = value.Count();
            for (int i = 0; i < CountCol; i++)
            {
                if (i < countValue)
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, value[i].ToString()) - value[i].ToString().Length, value[i].ToString(), column[i].LenghtCol);
                else
                    ShowCol(column[i].LenghtCol - ShowColLeft(column[i].LenghtCol, "") - 0, "", column[i].LenghtCol);
            }
            Console.WriteLine("║");
        }

        //  Метод для вывода низа таблицы.
        protected void PrintBottom()
        {
            Console.Write("╚");
            for (int i = 0; i < CountCol; i++)
            {
                for (int m = 0; m < column[i].LenghtCol; m++)
                    Console.Write("═");
                if (i != CountCol - 1)
                    Console.Write("╩");
            }
            Console.WriteLine("╝");
        }

        // находим количество пробелов слева, печатаем их и возвращаем их количество
        static int ShowColLeft(int COL, string str)
        {
            int spLeft = (COL - str.Length) / 2;
            Console.Write("║");
            for (int i = 0; i < spLeft; i++)
                Console.Write(" ");
            return spLeft;
        }

        // печатаем значение и пробелы справа
        static void ShowCol(int spRight, string str, int COL)
        {
            if (str.Length > COL)
                Console.Write(str.Substring(0, COL));
            else
                Console.Write(str);
            for (int i = 0; i < spRight; i++)
                Console.Write(" ");
        }
    }
}
