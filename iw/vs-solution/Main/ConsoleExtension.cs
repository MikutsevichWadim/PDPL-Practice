using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleExtension
{
    public static class CE
    {
        public static void WaitForEsc()
        {
            Console.WriteLine("\nEsc) Назад в меню");

            while (!(Console.ReadKey(true).Key == ConsoleKey.Escape)) ;
            Console.Clear();
        }
        public static int InputInt(string message)
        {
            int input = 0;
            bool correctInput = false;
            do
            {
                try
                {
                    Console.Write(message);
                    input = int.Parse(Console.ReadLine());
                    correctInput = true;
                }
                catch
                {
                    Console.WriteLine("Введите число");
                }
            }
            while (!correctInput);
            return input;
        }
        public static int InputIntPositive(string message)
        {
            int input;
            do
            {
                input = InputInt(message);

                if (input > 0)
                {
                    break;
                }

                Console.WriteLine("Число должны быть положительным");
            }
            while (true);
            return input;
        }
        public static int InputIntPositive(string message, Func<int, bool> checkInput)
        {
            int input = 0;
            do
            {
                input = InputIntPositive(message);
                if (checkInput(input)) {
                    break;
                };
            }
            while (true);
            return input;
        }
        public static int InputIntAvailable(string message, int[] availableInput)
        {
            int input = 0;
            do
            {
                input = InputIntPositive(message);

                if (availableInput.Contains(input))
                {
                    break;
                }

                Console.WriteLine($"Введите допустимое значение ({string.Join(", ", availableInput)})");
            }
            while (true);
            return input;
        }
        public static int InputIntAvailable(string message, int[] availableInput, string messageAvailable = "Введите допустимое значение")
        {
            int input = 0;
            do
            {
                input = InputIntPositive(message);

                if (availableInput.Contains(input))
                {
                    break;
                }

                Console.WriteLine($"{messageAvailable} ({string.Join(", ", availableInput)})");
            }
            while (true);
            return input;
        }
        public static decimal InputDecimal(string message)
        {
            decimal input = 0;
            bool correctInput = false;
            do
            {
                try
                {
                    Console.Write(message);
                    input = decimal.Parse(Console.ReadLine());
                    correctInput = true;
                }
                catch
                {
                    Console.WriteLine("Введите числовое значение");
                }
            }
            while (!correctInput);
            return input;
        }
        public static decimal InputDecimalPositive(string message)
        {
            decimal input = 0;
            do
            {
                input = InputDecimal(message);
                if (input > 0)
                {
                    break;
                }
                Console.WriteLine("Введите положительное число");
            }
            while (true);
            return input;
        }
        public static decimal InputDecimalPositive(string message, Func<decimal, bool> checkInput)
        {
            decimal input;
            do
            {
                input = InputDecimalPositive(message);

                if (checkInput(input))
                {
                    break;
                }
            }
            while (true);
            return input;
        }
        public static DateTime InputDateTime(string message)
        {
            DateTime input = new DateTime();
            bool correctInput = false;
            do
            {
                try
                {
                    Console.Write(message);
                    input = DateTime.ParseExact(Console.ReadLine(), "d", null);
                    correctInput = true;
                }
                catch
                {
                    Console.WriteLine("Введите верный формат даты (дд.мм.гггг)");
                }
            }
            while (!correctInput);
            return input;
        }
        public static DateTime InputDateTime(string message, Func<DateTime, bool> checkInput)
        {
            DateTime input = new DateTime();
            do
            {
                input = InputDateTime(message);
                if (checkInput(input))
                {
                    break;
                }
            }
            while (true);
            return input;
        }
        public static string InputString(string message)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine().Trim();

                if (!(input.Length == 0))
                {
                    break;
                }

                Console.WriteLine("Строка не может быть пустой");
            }
            while (true);
            return input;
        }
        public static string InputString(string message, Func<string, bool> checkInput)
        {
            string input;
            do
            {
                input = InputString(message);
                if (checkInput(input))
                {
                    break;
                }
            }
            while (true);
            return input;
        }
    }
}
