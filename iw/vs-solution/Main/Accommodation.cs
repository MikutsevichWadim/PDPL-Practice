using ClientClass;
using ConsoleExtension;
using HotelDataNameSpace;
using HotelRoomClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AccommodationClass
{
    public class Accommodation : IComparable<Accommodation>, IHotelDataItem
    {
        // СВОЙСТВА

        public string ClientInitials { get; set; }
        public int HotelRoomNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Note { get; set; }

        public decimal HotelRoomPrice { get; set; }
        public decimal Revenue
        {
            get
            {
                return (CheckOutDate - CheckInDate).Days * HotelRoomPrice;
            }
        }

        // МЕТОДЫ

        public static Accommodation[] Load(string path)
        {
            return HotelData.Load<Accommodation>(path);
        }

        // МЕТОДЫ: КОНСТРУКТОРЫ

        public Accommodation() { }
        public Accommodation(string clientInitials, int hotelRoomNumber, DateTime checkInDate, DateTime checkOutDate, string note, decimal hotelRoomPrice)
        {
            ClientInitials = clientInitials;
            HotelRoomNumber = hotelRoomNumber;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Note = note;

            HotelRoomPrice = hotelRoomPrice;
        }
        public Accommodation(string[] stringParams)
        {
            ClientInitials = stringParams[0];
            HotelRoomNumber = int.Parse(stringParams[1]);
            CheckInDate = DateTime.Parse(stringParams[2]);
            CheckOutDate = DateTime.Parse(stringParams[3]);
            Note = stringParams[4];
            HotelRoomPrice = int.Parse(stringParams[5]);
        }

        // МЕТОДЫ: ДЕЙСТВИЯ ПОЛЬЗОВАТЕЛЯ

        public static void Add(ref Accommodation[] accommodations, ref HotelRoom[] hotelRooms)
        {
            // заголовок операции
            accommodations = HotelData
                .Add<Accommodation>(
                    accommodations,
                    "Заселение клиента",
                    "Клиент заселён в комнату",
                    new Dictionary<string, object> {
                        { "accommodations", accommodations},
                        { "hotelRooms", hotelRooms }
                    }
                )
                .ToArray<Accommodation>();
        }
        public static void Delete(ref Accommodation[] accommodations)
        {
            accommodations = HotelData
                .Delete(
                    accommodations,
                    "Удаление записи о заселении"
                )
                .Cast<Accommodation>()
                .ToArray();
        }
        public static void Edit(ref Accommodation[] accommodations, ref HotelRoom[] hotelRooms)
        {
            accommodations = HotelData.Edit(
                    accommodations,
                    "Изменение данных записи о заселении",
                    "Выбор записи о заселении - ",
                    "Данные записи о заселении изменены",
                    new Dictionary<string, object>
                    {
                        { "accommodations", accommodations },
                        { "hotelRooms", hotelRooms }
                    }
                )
                .Cast<Accommodation>()
                .ToArray();
        }
        public static void Search(ref Accommodation[] accommodations)
        {
            HotelData.Search(
                accommodations,
                "Записи о заселениях - "
            );
            CE.WaitForEsc();
        }
        public static void View(ref Accommodation[] accommodations)
        {
            HotelData.View(
                accommodations,
                "Просмотр записей о заселениях"
            );
        }

        // МЕТОДЫ: РЕАЛИЗАЦИЯ ИНТЕРФЕЙСОВ

        int IComparable<Accommodation>.CompareTo(Accommodation other)
        {
            return this.CheckInDate.CompareTo(other.CheckInDate);
        }
        void IHotelDataItem.Edit(object[] input)
        {
            ClientInitials = (string)input[0];
            HotelRoomNumber = (int)input[1];
            CheckInDate = (DateTime)input[2];
            CheckOutDate = (DateTime)input[3];
            Note = (string)input[4];
            HotelRoomPrice = (decimal)input[5];
        }
        object[] IHotelDataItem.Input(Dictionary<string, object> params_)
        {
            HotelRoom[] hotelRooms = (HotelRoom[])params_["hotelRooms"];
            Accommodation[] accommodations = (Accommodation[])params_["accommodations"];
            
            // проверка наличия свободных номеров

            HotelRoom[] freeHotelRooms = HotelRoom.GetFreeHotelRooms(hotelRooms, accommodations);
            if (freeHotelRooms.Length == 0)
            {
                Console.WriteLine("Все номера заняты");
                CE.WaitForEsc();
                return null;
            }

            // проверка соответствия бюджета свободным номерам

            decimal budget = CE.InputDecimalPositive("Желаемая цена за ночь: ");

            HotelRoom[] suitablePriceHotelRooms = freeHotelRooms
                .Where(x => x.Price <= budget)
                .ToArray<HotelRoom>();

            if (suitablePriceHotelRooms.Count() == 0)
            {
                Console.WriteLine("Свободных номеров, подходящих по цене за ночь, не найдено");
                return null;
            }

            // проверка соответствия требований к уровню комфорта

            string desiredComfortLevel = HotelRoom.InputComfortLevel("Желаемый уровень комфорта (Люкс, Полулюкс, Обычный): ");

            HotelRoom[] suitableHotelRooms = suitablePriceHotelRooms
                .Where(x => x.ComfortLevel == desiredComfortLevel)
                .ToArray<HotelRoom>();

            if (suitableHotelRooms.Count() == 0)
            {
                Console.WriteLine("Подходящих номеров не найдено");
                return null;
            }

            // вывод результатов проверки

            int[] suitableHotelRoomsNumbers = suitableHotelRooms
                .Select(x => x.Number)
                .ToArray();

            Console.WriteLine($"Подходящие номера ({suitableHotelRoomsNumbers.Count()}): {string.Join(", ", suitableHotelRoomsNumbers)}");

            // заполнение других данных

            string clientInitials = CE.InputString("ФИО клиента: ");
            int hotelRoomNumber = CE.InputIntPositive(
                "Номер: ",
                input =>
                {
                    if (!suitableHotelRoomsNumbers.Contains(input))
                    {
                        Console.WriteLine($"Введите номер доступного помещения ({string.Join(", ", suitableHotelRoomsNumbers)})");
                        return false;
                    }
                    return true;
                }
            );
            DateTime checkInDate = CE.InputDateTime(
                "Дата заселения: ",
                input => // проверка задних чисел
                {
                    if (input.Subtract(DateTime.Now).Days <= 0)
                    {
                        Console.WriteLine("Нельзя ввести прошедшую дату");
                        return false;
                    }
                    return true;
                }
            );
            DateTime checkOutDate = CE.InputDateTime(
                "Дата выселения: ",
                input => // проверка неположительного периода заселения (задние числа уже не могут быть)
                {
                    if (input.Subtract(checkInDate).Days <= 0)
                    {
                        Console.WriteLine("Дата выселения не может быть в один день или раньше заселения");
                        return false;
                    }
                    return true;
                }
            );
            string note = CE.InputString("Примечание: ");

            // возврат параметров

            return new object[]
            {
                clientInitials,
                hotelRoomNumber,
                checkInDate,
                checkOutDate,
                note,
                hotelRooms.First(hr => hr.Number == hotelRoomNumber).Price
            };
        }
        string[] IHotelDataItem.ToData()
        {
            return new string[] {
                ClientInitials,
                HotelRoomNumber.ToString(),
                CheckInDate.ToString(),
                CheckOutDate.ToString(),
                Note,
                HotelRoomPrice.ToString(),
            };
        }
        string IHotelDataItem.ToSearchableString()
        {
            return $"{ClientInitials}{HotelRoomNumber}{CheckInDate}{CheckOutDate}{Note}";
        }
        string IHotelDataItem.ToString()
        {
            string result = $"Поселение \n";
            result += $"Клиент: {ClientInitials} \n";
            result += $"Номер: {HotelRoomNumber} \n";
            result += $"Дата поселения: {CheckInDate} \n";
            result += $"Дата освобождения: {CheckOutDate} \n";
            result += $"Примечание: {Note} \n";
            return result;
        }
    }
}
