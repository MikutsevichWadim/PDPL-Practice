using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ввод значения x
            double x = float.Parse(Console.ReadLine());

            // Ввод значения n
            int n = int.Parse(Console.ReadLine());

            // Вычисление синуса x и присвоение результата переменной result
            double result = Math.Sin(x);

            // Цикл для добавления к результату синуса предыдущего результата n раз
            while (n > 0)
            {
                n--;
                result += Math.Sin(result);
            }

            // Вывод результата
            Console.WriteLine(result);
        }
    }
}
