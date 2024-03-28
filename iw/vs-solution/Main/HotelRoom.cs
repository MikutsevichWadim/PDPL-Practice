using AccommodationClass;
using ClientClass;
using ConsoleExtension;
using HotelDataNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelRoomClass
{
    public class HotelRoom : IComparable<HotelRoom>, IHotelDataItem
    {
        // СВОЙСТВА

        public int Number { get; set; }
        public decimal Price { get; set; }
        public int PersonsNumber { get; set; }
        public static string[] availableComfortLevel { get; set; } = new string[] { "Люкс", "Полулюкс", "Обычный" };
        private string comfortLevel;
        public string ComfortLevel
        {
            get
            {
                return comfortLevel;
            }
            set
            {
                if (!availableComfortLevel.Contains(value))
                {
                    throw new ArgumentException("Недопустимое значения атрибута комфорта");
                }
                comfortLevel = value;
            }
        }

        // МЕТОДЫ

        public static HotelRoom[] Load(string path)
        {
            return HotelData.Load<HotelRoom>(path);
        }
        public static HotelRoom[] GetFreeHotelRooms(HotelRoom[] hotelRooms, Accommodation[] accommodations)
        {
            List<HotelRoom> freeHotelRooms = new List<HotelRoom>();
            foreach (HotelRoom hotelRoom in hotelRooms)
            {
                bool hotelRoomIsFree = hotelRoom.PersonsNumber > accommodations
                    .Where(a => a.HotelRoomNumber == hotelRoom.Number)
                    .Count();
                if (hotelRoomIsFree)
                {
                    freeHotelRooms.Add(hotelRoom);
                }
            }
            if (freeHotelRooms.Count == 0)
            {
                Console.WriteLine("Свободные номера отсутствуют");
            }
            return freeHotelRooms.ToArray();
        }
        public static string InputComfortLevel(string message)
        {
            return CE.InputString(
                message,
                input =>
                {
                    if (availableComfortLevel.Contains(input))
                    {
                        return true;
                    }
                    Console.WriteLine($"Введите доступное значение");
                    return false;
                }
            );
        }

        // МЕТОДЫ: КОНСТРУКТОРЫ

        public HotelRoom() { }
        public HotelRoom(int number, int personsNumber, string comfortLevel, decimal price)
        {
            Number = number;
            PersonsNumber = personsNumber;
            ComfortLevel = comfortLevel;
            Price = price;
        }
        public HotelRoom(string[] stringParams)
        {
            Number = int.Parse(stringParams[0]);
            PersonsNumber = int.Parse(stringParams[1]);
            ComfortLevel = stringParams[2];
            Price = decimal.Parse(stringParams[3]);
        }

        // МЕТОДЫ: ДЕЙСТВИЯ ПОЛЬЗОВАТЕЛЯ

        public static void Add(ref HotelRoom[] hotelRooms)
        {
            hotelRooms = HotelData
                .Add<HotelRoom>(
                    hotelRooms,
                    "Добавление номера",
                    "Данные о номере добавлены",
                    new Dictionary<string, object> { { "HotelRooms", hotelRooms } }
                )
                .Cast<HotelRoom>()
                .ToArray();
        }
        public static void Delete(ref HotelRoom[] hotelRooms)
        {
            hotelRooms = HotelData
                .Delete(
                    hotelRooms,
                    "Удаление номера"
                )
                .Cast<HotelRoom>()
                .ToArray();
        }
        public static void Edit(ref HotelRoom[] hotelRooms)
        {
            hotelRooms = HotelData
                .Edit(
                    hotelRooms,
                    "Изменение данных клиента",
                    "Выбор номера - ",
                    "Данные номера изменены",
                    new Dictionary<string, object> {
                        { "hotelRooms", hotelRooms }
                    }
                )
                .Cast<HotelRoom>()
                .ToArray(); ;
        }
        public static void Search(ref HotelRoom[] hotelRooms)
        {
            HotelDataNameSpace.HotelData.Search(
                hotelRooms,
                "Номера - "
            );
            CE.WaitForEsc();
        }
        public static void View(ref HotelRoom[] hotelRooms)
        {
            HotelDataNameSpace.HotelData.View(
                hotelRooms,
                "Просмотр списка номеров"
            );
        }

        // МЕТОДЫ: РЕАЛИЗАЦИЯ ИНТЕРФЕЙСОВ

        int IComparable<HotelRoom>.CompareTo(HotelRoom other)
        {
            return this.Number.CompareTo(other.Number);
        }
        void IHotelDataItem.Edit(object[] input)
        {
            Number = (int)input[0];
            PersonsNumber = (int)input[1];
            ComfortLevel = (string)input[2];
            Price = (decimal)input[3];
        }
        object[] IHotelDataItem.Input(Dictionary<string, object> params_)
        {
            HotelRoom[] hotelRooms = (HotelRoom[])params_["hotelRooms"];
            IEnumerable<int> existingRoomNumbers = hotelRooms.Select(hr => hr.Number);
            return new object[]
            {
                CE.InputIntPositive(
                    "Номер(число): ",
                    input =>
                    {
                        if (input <= 0)
                        {
                            Console.WriteLine("Номер должен быть положительным");
                            return false;
                        }
                        if (existingRoomNumbers.Contains(input))
                        {
                            Console.WriteLine($"Номер уже добавлен. Существующие номера {string.Join(", ", existingRoomNumbers)}");
                            return false;
                        }
                        return true;
                    }
                ),
                CE.InputIntPositive(
                    "Вместимость (кол-во человек): ",
                    input =>
                    {
                        if (input <= 0)
                        {
                            Console.WriteLine("Количество должно быть положительным");
                            return false;
                        }
                        return true;
                    }
                ),
                InputComfortLevel("Комфортность (Люкс, Полулюкс, Обычный): "),
                CE.InputDecimalPositive(
                    "Цена за ночь: ",
                    input =>
                    {
                        if (input <= 0)
                        {
                            Console.WriteLine("Цена должна быть положительной");
                            return false;
                        }
                        return true;
                    }
                )
            };
        }
        string[] IHotelDataItem.ToData()
        {
            return new string[] {
                Number.ToString(),
                PersonsNumber.ToString(),
                ComfortLevel,
                Price.ToString()
            };
        }
        string IHotelDataItem.ToSearchableString()
        {
            return $"{Number}{Price}{PersonsNumber}{ComfortLevel}";
        }
        string IHotelDataItem.ToString()
        {
            string result = $"Номер в отеле \n";
            result += $"Номер: {Number} \n";
            result += $"Количество человек: {PersonsNumber} \n";
            result += $"Комфортность: {ComfortLevel} \n";
            result += $"Цена: {Price:C} \n";
            return result;
        }
    }
}
