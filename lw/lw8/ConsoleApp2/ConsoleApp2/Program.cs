using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите количество экспонатов:");
                int amountOfShowPieces = int.Parse(Console.ReadLine());
                List<ShowPiece> showPieces = new List<ShowPiece>();

                Random rand = new Random();

                // Создание экспонатов и добавление их в список
                for (int i = 0; i < amountOfShowPieces; i++)
                {
                    showPieces.Add(new ShowPiece($"Экспонат_{i}", $"Автор_{i}", rand.Next(2, 7)));
                }

                // Вывод всех экспонатов
                ShowPiece.Print(showPieces);

                // Создание списка экспонатов с количеством билетов более 3
                List<ShowPiece> threeshowPieces = new List<ShowPiece>();
                threeshowPieces.Sort();

                foreach (ShowPiece showPiece in showPieces)
                {
                    if (showPiece.QuantityOfTickets > 3)
                    {
                        threeshowPieces.Add(showPiece);
                    }
                }

                // Вывод экспонатов с количеством билетов более 3
                ShowPiece.Print(threeshowPieces);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Класс экспоната
        class ShowPiece : IComparable<ShowPiece>, IComparer<ShowPiece>
        {
            protected string title;
            protected string author;
            public int QuantityOfTickets;

            // Конструктор
            public ShowPiece(string title, string author, int quantityOfTickets)
            {
                this.title = title;
                this.author = author;
                this.QuantityOfTickets = quantityOfTickets;
            }

            // Статический метод для вывода списка экспонатов
            public static void Print(List<ShowPiece> showPieces)
            {
                Console.WriteLine("______Начало_списка______");
                showPieces.ForEach(delegate (ShowPiece item)
                {
                    Console.WriteLine("Название: {0}", item.title);
                    Console.WriteLine("Автор: {0}", item.author);
                    Console.WriteLine("Количество билетов: {0}", item.QuantityOfTickets);
                    Console.WriteLine();
                });
                Console.WriteLine("______Конец__списка______");
                Console.WriteLine();
            }

            // Реализация интерфейса IComparable для сравнения экспонатов
            public int CompareTo(ShowPiece other)
            {
                if (other == null) throw new ArgumentException("Некорректное значение параметра");
                return QuantityOfTickets - other.QuantityOfTickets;
            }

            // Реализация интерфейса IComparer для сравнения экспонатов
            public int Compare(ShowPiece sp1, ShowPiece sp2)
            {
                if (sp1 == null || sp2 == null) throw new ArgumentNullException();
                return sp1.QuantityOfTickets - sp2.QuantityOfTickets;
            }
        }
    }
}
