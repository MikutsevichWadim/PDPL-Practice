using System;

// Класс для работы с матрицами вещественных чисел
class RealMatrix
{
    private double[,] data; // Двумерный массив для хранения данных
    private int rows; // Количество строк
    private int columns; // Количество столбцов

    // Конструктор класса
    public RealMatrix(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        data = new double[rows, columns]; // Инициализация двумерного массива
    }

    // Индексатор для доступа к элементам матрицы
    public double this[int i, int j]
    {
        get { return data[i, j]; }
        set { data[i, j] = value; }
    }

    // Перегрузка оператора "!" для получения матрицы с противоположными значениями элементов
    public static RealMatrix operator !(RealMatrix matrix)
    {
        RealMatrix result = new RealMatrix(matrix.rows, matrix.columns);
        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.columns; j++)
            {
                result[i, j] = -matrix[i, j]; // Присваивание противоположного значения элементу
            }
        }
        return result;
    }

    // Метод для вывода матрицы на экран
    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(data[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

// Основной класс программы
class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Введите количество строк:");
            int rows = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество столбцов:");
            int columns = int.Parse(Console.ReadLine());

            RealMatrix matrix = new RealMatrix(rows, columns); // Создание объекта матрицы

            Console.WriteLine("Введите элементы матрицы:");

            // Заполнение матрицы элементами, введенными пользователем
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"Введите элемент на позиции ({i}, {j}): ");
                    matrix[i, j] = double.Parse(Console.ReadLine());
                }
            }

            Console.WriteLine("\nИсходная матрица:");
            matrix.Print(); // Вывод исходной матрицы на экран

            RealMatrix negatedMatrix = !matrix; // Получение матрицы с противоположными значениями элементов
            Console.WriteLine("\nМатрица с противоположными значениями элементов:");
            negatedMatrix.Print(); // Вывод матрицы с противоположными значениями на экран
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString()); // Вывод сообщения об ошибке
        }
    }
}
