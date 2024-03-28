using ConsoleExtension;
using HotelDataNameSpace;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientClass
{
    public class Client : IComparable<Client>, IHotelDataItem
    {
        // СВОЙСТВА

        public string Comment { get; set; }
        public string Name { get; set; }
        public string PassportDetails { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }

        // МЕТОДЫ

        public static Client[] Load(string path)
        {
            return HotelData.Load<Client>(path);
        }

        // МЕТОДЫ: КОНСТРУКТОРЫ

        public Client() { }
        public Client(string surname, string name, string patronymic, string passportDetails, string comment)
        {
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            PassportDetails = passportDetails;
            Comment = comment;
        }
        public Client(string[] stringParams)
        {
            Surname = stringParams[0];
            Name = stringParams[1];
            Patronymic = stringParams[2];
            PassportDetails = stringParams[3];
            Comment = stringParams[4];
        }

        // МЕТОДЫ: ДЕЙСТВИЯ ПОЛЬЗОВАТЕЛЯ

        public static void Add(ref Client[] clients)
        {
            clients = HotelData
                .Add<Client>(
                    clients,
                    "Добавление клиента",
                    "Клиент добавлен",
                    new Dictionary<string, object> { { "Client", (object)clients } }
                )
                .Cast<Client>()
                .ToArray();
        }
        public static void Delete(ref Client[] clients)
        {
            clients = HotelData
                .Delete(
                    clients,
                    "Удаление клиента"
                )
                .Cast<Client>()
                .ToArray();
        }
        public static void Edit(ref Client[] clients)
        {
            clients = HotelData
                .Edit(
                    clients,
                    "Изменение данных клиента",
                    "Выбор клиента - ",
                    "Данные клиента изменены",
                    new Dictionary<string, object>
                    {
                        { "clients", clients }
                    }
                )
                .Cast<Client>()
                .ToArray();
        }
        public static void Search(ref Client[] clients)
        {
            HotelData.Search(
                clients,
                "Клиенты - "
            );
            CE.WaitForEsc();
        }
        public static void View(ref Client[] clients)
        {
            HotelData.View(clients, "Просмотр списка клиентов");
        }

        // МЕТОДЫ: РЕАЛИЗАЦИЯ ИНТЕРФЕЙСОВ

        int IComparable<Client>.CompareTo(Client other)
        {
            return this.Surname.CompareTo(other.Surname);
        }
        void IHotelDataItem.Edit(object[] input)
        {
            Surname = (string)input[0];
            Name = (string)input[1];
            Patronymic = (string)input[2];
            PassportDetails = (string)input[3];
            Comment = (string)input[4];
        }
        object[] IHotelDataItem.Input(Dictionary<string, object> params_)
        {
            return new object[] {
                CE.InputString("Фамилия: "),
                CE.InputString("Имя: "),
                CE.InputString("Отчество: "),
                CE.InputString("Паспортные данные: "),
                CE.InputString("Комментарий: ")
            };
        }
        string[] IHotelDataItem.ToData()
        {
            return new string[] { Surname, Name, Patronymic, PassportDetails, Comment };
        }
        string IHotelDataItem.ToSearchableString()
        {
            return $"{Surname}{Name}{Patronymic}{PassportDetails}{Comment}";
        }
        string IHotelDataItem.ToString()
        {
            string result = $"Клиент \n";
            result += $"Фамилия: {Surname} \n";
            result += $"Имя: {Name} \n";
            result += $"Отчество: {Patronymic} \n";
            result += $"Паспортные данные: {PassportDetails} \n";
            result += $"Комментарий: {Comment} \n";
            return result;
        }
    }
}
