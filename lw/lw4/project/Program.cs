using System;

namespace lw4
{
    // Класс для работы с двумерным массивом
    class TwoDimensionalArray
    {
        private int[,] array; // Двумерный массив

        // Конструктор класса
        public TwoDimensionalArray(int rows, int columns)
        {
            // Проверка на положительные размеры массива
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentException("Размеры массива должны быть положительными числами.");
            }

            // Инициализация массива
            array = new int[rows, columns];
        }

        // Индексатор для доступа к элементам массива
        public int this[int row, int column]
        {
            get
            {
                ValidateIndices(row, column);
                return array[row, column];
            }
            set
            {
                ValidateIndices(row, column);
                array[row, column] = value;
            }
        }

        // Проверка индексов на выход за пределы массива
        private void ValidateIndices(int row, int column)
        {
            if (row < 0 || row >= array.GetLength(0) || column < 0 || column >= array.GetLength(1))
            {
                throw new IndexOutOfRangeException("Индексы выходят за пределы массива.");
            }
        }

        // Вывод элемента массива по индексам
        public void PrintElement(int row, int column)
        {
            ValidateIndices(row, column);
            Console.WriteLine($"Элемент [{row}, {column}]: {array[row, column]}");
        }

        // Вывод всего массива
        public void PrintArray()
        {
            Console.WriteLine("Двумерный массив:");

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // Поиск значения в массиве
        public string FindValue(int value)
        {
            string result = "";
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value)
                    {
                        result += $"[{i},{j}], ";
                    }
                }
            }

            return result;
        }
    }

    internal class Program
    {
        static void Main()
        {
            try
            {
                Console.Write("Введите количество строк массива: ");
                int rows = int.Parse(Console.ReadLine());

                Console.Write("Введите количество столбцов массива: ");
                int columns = int.Parse(Console.ReadLine());

                TwoDimensionalArray myArray = new TwoDimensionalArray(rows, columns);

                // Заполняем массив
                Console.WriteLine("Введите значения для заполнения массива:");

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Console.Write($"Элемент [{i}, {j}]: ");
                        myArray[i, j] = int.Parse(Console.ReadLine());
                    }
                }

                // Выводим массив
                myArray.PrintArray();

                // Выводим конкретный элемент
                Console.Write("Введите индекс строки: ");
                int rowIndex = int.Parse(Console.ReadLine());

                Console.Write("Введите индекс столбца: ");
                int columnIndex = int.Parse(Console.ReadLine());

                myArray.PrintElement(rowIndex, columnIndex);

                // Ищем значение в массиве
                Console.Write("Введите значение для поиска: ");
                int searchValue = int.Parse(Console.ReadLine());

                string foundIndices = myArray.FindValue(searchValue);

                if (foundIndices != "")
                {
                    Console.WriteLine($"Значение {searchValue} найдено под индексами {foundIndices}");
                }
                else
                {
                    Console.WriteLine($"Значение {searchValue} не найдено в массиве.");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
