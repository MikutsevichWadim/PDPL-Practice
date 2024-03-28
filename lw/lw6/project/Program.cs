using System;

// Базовый класс
class Phone
{
    // Закрытое поле
    private int functions;

    // Конструктор с параметрами
    public Phone(int functions)
    {
        this.functions = functions;
    }

    // Метод доступа к закрытому полю
    public int GetFunctions()
    {
        return functions;
    }

    // Метод вывода поля
    public void Display()
    {
        Console.WriteLine("Количество функций: " + functions);
    }

    // Метод вычисления стоимости
    public double Cost()
    {
        return 40 * Math.Log(functions);
    }
}

// Потомок
class CellPhone : Phone
{
    // Новое поле потомка
    private string model;

    // Конструктор с параметрами
    public CellPhone(int functions, string model) : base(functions)
    {
        this.model = model;
    }

    // Метод вывода нового поля потомка
    public new void Display()
    {
        base.Display(); // Вызов метода от базового класса
        Console.WriteLine("Модель: " + model);
    }

    // Переопределение метода вычисления стоимости
    public new double Cost()
    {
        return base.Cost() * 3; // Увеличение стоимости в 3 раза
    }
}

class Program
{
    static void Main()
    {
        try
        {
            // Ввод количества функций для базового телефона
            Console.WriteLine("Введите количество функций для базового телефона:");
            int functionsPhone = int.Parse(Console.ReadLine());

            // Создание объекта базового класса с введенным количеством функций
            Phone phone = new Phone(functionsPhone);
            Console.WriteLine("Информация о базовом телефоне:");
            phone.Display();
            Console.WriteLine("Стоимость базового телефона: $" + phone.Cost());

            Console.WriteLine();

            // Ввод количества функций и модели для сотового телефона
            Console.WriteLine("Введите количество функций для сотового телефона:");
            int functionsCellPhone = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите модель для сотового телефона:");
            string modelCellPhone = Console.ReadLine();

            // Создание объекта производного класса с введенными данными
            CellPhone cellPhone = new CellPhone(functionsCellPhone, modelCellPhone);
            Console.WriteLine("Информация о сотовом телефоне:");
            cellPhone.Display();
            Console.WriteLine("Стоимость сотового телефона: $" + cellPhone.Cost());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}
