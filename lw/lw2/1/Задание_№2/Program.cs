using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Задание__2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Английский алфавит
                char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

                // Гласные буквы
                char[] vowels = { 'a', 'e', 'i', 'o', 'u' };

                // Ввод символа
                char chr = Console.ReadLine()[0];

                // Проверка наличия символа в алфавите
                if (alphabet.Contains(chr))
                {
                    Console.WriteLine($"Введенный символ: {chr}");

                    // Проверка, является ли символ гласной
                    if (vowels.Contains(chr))
                    {
                        int vowel = (int)chr;
                        // Вывод следующего символа после гласной
                        Console.WriteLine("Так как он является гласной, то выведем символ, который стоит после него: " + (char)(vowel + 1));
                    }
                    else
                    {
                        Console.WriteLine("Символ не является гласной");
                    }
                }
                else
                {
                    Console.WriteLine($"Введенный символ: {chr} не является буквой английского алфавита");
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}
