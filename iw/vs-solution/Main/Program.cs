using AccommodationClass;
using ClientClass;
using ConsoleExtension;
using HotelDataNameSpace;
using HotelRoomClass;
using System;
using System.Linq;

namespace Program
{
    internal class Program
    {
        static Accommodation[] Accommodations;
        static Client[] Clients;
        static HotelRoom[] HotelRooms;

        static void Main() {            
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Title = "Гостиница - финансы";
            Console.Clear();

            // загрузка данных

            Accommodations = Accommodation.Load("accommodations.txt");
            Clients = Client.Load("clients.txt");
            HotelRooms = HotelRoom.Load("hotelrooms.txt");

            // название программы

            Console.WriteLine("HotelFinances");
            Console.WriteLine("Консольное приложение отслеживания финансов гостиницы");
            Console.WriteLine();

            // главный цикл программы
            while (true)
            {
                Console.WriteLine("Главная");
                Console.WriteLine();
                Console.WriteLine("Клиенты");
                Console.WriteLine("1)  Добавить");
                Console.WriteLine("2)  Изменить");
                Console.WriteLine("3)  Список");
                Console.WriteLine("4)  Поиск");
                Console.WriteLine("5)  Удалить");
                Console.WriteLine();
                Console.WriteLine("Номера");
                Console.WriteLine("6)  Добавить");
                Console.WriteLine("7)  Изменить");
                Console.WriteLine("8)  Список");
                Console.WriteLine("9)  Поиск");
                Console.WriteLine("10) Удалить");
                Console.WriteLine();
                Console.WriteLine("Поселение");
                Console.WriteLine("11) Добавить");
                Console.WriteLine("12) Изменить");
                Console.WriteLine("13) Список");
                Console.WriteLine("14) Поиск");
                Console.WriteLine("15) Удалить");
                Console.WriteLine();
                Console.WriteLine("16) Отчёт о выручке");
                Console.WriteLine();
                Console.WriteLine("17) Выход из приложения");
                Console.WriteLine();

                // выбор действия пользователем

                int userInput = CE.InputIntAvailable(
                    "Введите номер пункта меню: ",
                    Enumerable.Range(1, 17).ToArray(),
                    "Введите номер доступного действия"
                );
                Console.Clear();
                switch (userInput)
                {
                    case 1: Client.Add(ref Clients); break;
                    case 2: Client.Edit(ref Clients); break;
                    case 3: Client.View(ref Clients); break;
                    case 4: Client.Search(ref Clients); break;
                    case 5: Client.Delete(ref Clients); break;

                    case 6: HotelRoom.Add(ref HotelRooms); break;
                    case 7: HotelRoom.Edit(ref HotelRooms); break;
                    case 8: HotelRoom.View(ref HotelRooms); break;
                    case 9: HotelRoom.Search(ref HotelRooms); break;
                    case 10: HotelRoom.Delete(ref HotelRooms); break;

                    case 11: Accommodation.Add(ref Accommodations, ref HotelRooms); break;
                    case 12: Accommodation.Edit(ref Accommodations, ref HotelRooms); break;
                    case 13: Accommodation.View(ref Accommodations); break;
                    case 14: Accommodation.Search(ref Accommodations); break;
                    case 15: Accommodation.Delete(ref Accommodations); break;
                    
                    case 16: CalculateRevenue(ref Accommodations, ref HotelRooms); break;

                    case 17:
                        HotelData.Save(Accommodations, "Accommodations.txt");
                        HotelData.Save(Clients, "Clients.txt");
                        HotelData.Save(HotelRooms, "HotelRooms.txt");
                        Environment.Exit(0);
                        break;
                }
            }
        }
        public static void CalculateRevenue(ref Accommodation[] accommodations, ref HotelRoom[] hotelRooms)
        {
            DateTime currentDateTime = CE.InputDateTime("Введите текущую дату: ");

            decimal totalRevenue = accommodations
                .Where(a => a.CheckOutDate <= currentDateTime)
                .Sum(a => a.Revenue);
            Console.WriteLine($"Общая выручка со всех номеров в период до {currentDateTime.ToString("dd.MM.yyyy")}: {totalRevenue}");
            CE.WaitForEsc();
        }
    }
}
