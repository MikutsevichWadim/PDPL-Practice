using System;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Просим пользователя ввести значения x, y, z
                Console.WriteLine("Введите x, y, z по очереди");

                // Считываем введенные значения
                double x = double.Parse(Console.ReadLine());
                double y = double.Parse(Console.ReadLine());
                double z = double.Parse(Console.ReadLine());

                // Вычисляем значение переменной a
                double a = Math.Sin(x) / Math.Abs(x) + 1;
                // Выводим значение переменной a
                Console.WriteLine("a = {0,4:F}", a);

                // Вычисляем значение переменной b
                double b = -Math.Sqrt(Math.Abs(Math.Sin(x))) / (2 + y * y + z * z);
                // Выводим значение переменной b
                Console.WriteLine("b = {0,4:F} ", b);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }
    }
}