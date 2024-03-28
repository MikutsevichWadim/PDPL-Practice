using ClientClass;
using ConsoleExtension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HotelDataNameSpace
{
    public static class HotelData
    {
        public static T[] Load<T>(string path) where T : class, IHotelDataItem
        {
            // создание файла в случае его отсутствия

            if (!File.Exists(path))
            {
                // создаем пустой файл
                using (FileStream fs = File.Create(path))
                {
                }
            }

            // получение текстовых данных из файла

            string textData;
            using (StreamReader sr = new StreamReader(path))
            {
                textData = sr.ReadToEnd();
            }

            // преобразование текста в строки параметров

            string[] textDataItems = textData.Split(
                new string[] { "\n\n" },
                StringSplitOptions.None
            );

            // Создание массива для хранения объектов класса
            T[] objects = new T[textDataItems.Where(tdi => !(new string[] { "\n", "" }).Contains(tdi.Trim())).ToArray().Length];

            // Преобразование строк параметров в массив строчных параметров и создание объектов
            for (int i = 0; i < objects.Length; i++)
            {
                string[] stringParams = textDataItems[i].Split(
                    new string[] { "\n" },
                    StringSplitOptions.RemoveEmptyEntries
                );

                if (stringParams.Length == 0)
                {
                    Console.WriteLine();
                    return objects.Cast<T>().ToArray();
                }
                Console.WriteLine("lol");
                // Создание экземпляра класса с помощью рефлексии
                objects[i] = (T)Activator.CreateInstance(
                    typeof(T),
                    new object[] { stringParams }
                );
            }

            return objects;
        }
        public static T[] Add<T>(IHotelDataItem[] array, string title, string messageResult, Dictionary<string, object> params_) where T : IHotelDataItem, new()
        {
            Console.WriteLine(title);
            Console.WriteLine();

            List<T> list = array.Cast<T>().ToList();

            // Создание экземпляра класса с помощью рефлексии
            T hotelDataItem = (T)Activator.CreateInstance(
                typeof(T),
                new T().Input(params_)
            );
            if (hotelDataItem == null)
            {
                CE.WaitForEsc();
                return array.Cast<T>().ToArray();
            }
            list.Add(hotelDataItem);
            list.Sort();

            T[] newArray = list.Cast<T>().ToArray();

            Console.WriteLine();
            Console.WriteLine(messageResult);

            CE.WaitForEsc();
            return newArray;
        }
        public static IHotelDataItem[] Search(IHotelDataItem[] array, string searchTitle = "")
        {
            string searchString = CE.InputString($"{searchTitle}Поиск всем параметрам: ");

            searchString = searchString.ToLower();
            IHotelDataItem[] arraySearchResult = array.Where(
                item => item.ToSearchableString().ToLower().Contains(searchString)
            ).ToArray();

            if (array.Length == 0)
            {
                Console.WriteLine("Ничего не найдено");
            }
            else
            {
                Console.Clear();
                View(arraySearchResult);
            }
            return arraySearchResult;
        }
        public static IHotelDataItem SelectiveSearch(IHotelDataItem[] array, string selectionTitle = "")
        {
            IHotelDataItem[] selectionResult = array;
            do
            {
                selectionResult = Search(selectionResult);
                Console.Clear();
                View(selectionResult);
                Console.WriteLine($"Количество результатов: {selectionResult.Length}");
            }
            while (selectionResult.Length > 1);

            Console.Clear();

            if (selectionResult.Length == 0)
            {
                Console.WriteLine("\nНичего не найдено");
                return null;
            }

            Console.WriteLine("Результат поиска: \n");
            IHotelDataItem searchResult = selectionResult[0];
            Console.WriteLine(searchResult.ToString());
            return searchResult;
        }
        public static void Save(IHotelDataItem[] array, string path)
        {
            // составление текстового представления
            StringBuilder textData = new StringBuilder();
            foreach (IHotelDataItem item in array)
            {
                textData.Append($"{string.Join("\n", item.ToData())}\n\n");
            }

            // запись в файл
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(textData);
            }
        }
        public static void View(IHotelDataItem[] array)
        {
            if (array.Length == 0)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            foreach (IHotelDataItem item in array)
            {
                Console.WriteLine(item.ToString());
            }
        }
        public static void View(IHotelDataItem[] array, string message)
        {
            Console.Clear();
            View(array);
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine($"Количество: {array.Length}");
            CE.WaitForEsc();
        }
        public static IHotelDataItem[] Delete(IHotelDataItem[] array, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine();
            IHotelDataItem searchResult = SelectiveSearch(array);

            int userInput = CE.InputIntAvailable(
                "Подтвердите удаление 1) да 2) нет: ",
                Enumerable.Range(1, 2).ToArray()
            );
            Console.WriteLine();
            switch (userInput)
            {
                case 1:
                    List<IHotelDataItem> list = array.ToList();
                    list.Remove(searchResult);
                    array = list.ToArray();
                    Console.WriteLine("Удаление завершено");
                    break;
                case 2:
                    Console.WriteLine("Удаление отменено");
                    break;
            }

            CE.WaitForEsc();
            return array;
        }
        public static IHotelDataItem[] Edit(IHotelDataItem[] array, string title, string searchTitle, string msgCompletion, Dictionary<string, object> params_)
        {
            Console.WriteLine($"{title}\n");
            IHotelDataItem hotelDataItem = SelectiveSearch(
                array,
                searchTitle
            );
            if (hotelDataItem == null)
            {
                CE.WaitForEsc();
                return array;
            }
            Console.WriteLine("\nВведите новые данные\n");
            object[] input = hotelDataItem.Input(params_);
            hotelDataItem.Edit(input);
            Console.WriteLine($"\n{msgCompletion}\n");
            CE.WaitForEsc();
            return array;
        }
    }

    public interface IHotelDataItem
    {
        void Edit(object[] input);
        object[] Input(Dictionary<string, object> params_);
        string[] ToData();
        string ToSearchableString();
        string ToString();
    }
}
