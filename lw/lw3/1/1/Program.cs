using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создание объекта Audience
            Audience obj = null;

            // Цикл для ввода данных об аудитории и создания объекта Audience
            while (true)
            {
                try
                {
                    // Ввод длины аудитории
                    Console.WriteLine("Введите длину аудитории");
                    float length = float.Parse(Console.ReadLine());

                    // Ввод высоты аудитории
                    Console.WriteLine("Введите высоту аудитории");
                    float height = float.Parse(Console.ReadLine());

                    // Ввод ширины аудитории
                    Console.WriteLine("Введите ширину аудитории");
                    float width = float.Parse(Console.ReadLine());

                    // Ввод количества компьютеров в аудитории
                    Console.WriteLine("Введите количество компьютеров в аудитории");
                    int count = int.Parse(Console.ReadLine());

                    // Создание объекта Audience
                    obj = new Audience(length, height, width, count);

                    // Проверка на успешное создание объекта
                    if (obj.isChecked == true)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    // Обработка и вывод ошибки
                    Console.WriteLine($"Error:{e.Message}");
                }
            }

            // Вывод сообщения об успешном создании объекта
            Console.WriteLine("Объект успешно создан");

            // Вызов методов объекта Audience для расчета площади и объема аудитории, а также проверки санитарной нормы
            obj.getSquareOfAduience();
            obj.getVolumeOfAduience();
            obj.validingCountOfComputers();
        }
    }

    // Класс, представляющий аудиторию
    public class Audience
    {
        private float length;   // Длина аудитории
        private float height;   // Высота аудитории
        private float width;    // Ширина аудитории
        private float count;    // Количество компьютеров

        private bool isValid;   // Флаг, указывающий на корректность значений аудитории

        public bool isChecked;  // Флаг, указывающий на успешную проверку значений аудитории

        // Конструктор класса Audience
        public Audience(float length, float height, float width, int count)
        {
            this.length = length;
            this.height = height;
            this.width = width;
            this.count = count;
            this.isValid = false;

            this.isChecked = this.checkValues(); // Проверка значений аудитории
        }

        // Метод для расчета площади аудитории
        public float getSquareOfAduience(bool message = false)
        {
            float square = this.width * this.length;

            if (!message)
            {
                Console.WriteLine($"Площадь аудитории:{square}");
            }

            return square;
        }

        // Метод для расчета объема аудитории
        public float getVolumeOfAduience()
        {
            float volume = this.width * this.length * this.height;

            Console.WriteLine($"Объём аудитории:{volume}");

            return volume;
        }

        // Метод для проверки санитарной нормы количества компьютеров в аудитории
        public bool validingCountOfComputers()
        {
            float result = getSquareOfAduience(true) / this.count;

            if (result >= 6.0f)
            {
                this.isValid = true;
                Console.WriteLine("{0,4:F}", result);
            }

            if (this.isValid)
            {
                Console.Write($"Санитарная норма выполнена:");
            }
            else
            {
                Console.Write($"Санитарная норма не выполнена:");
            }
            Console.WriteLine("{0,4:F}", result);
            return this.isValid;
        }

        // Приватный метод для проверки значений аудитории
        private bool checkValues()
        {
            if (this.length <= 0)
            {
                Console.WriteLine("Длина аудитории должна быть больше, чем 0");
                return false;

            }
            if (this.height <= 0)
            {
                Console.WriteLine("Высота аудитории должна быть больше, чем 0");
                return false;

            }
            if (this.width <= 0)
            {
                Console.WriteLine("Ширина аудитории должна быть больше, чем 0");
                return false;
            }

            if (this.count < 0)
            {
                Console.WriteLine("Количество компьютеров не может быть отрицательным значением");
                return false;
            }

            return true;
        }

    }
}
