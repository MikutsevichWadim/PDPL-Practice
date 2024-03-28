using System;

// Абстрактный класс для печатной продукции
abstract class PrintProduct
{
    protected string title; // Название печатной продукции

    // Конструктор с параметром
    public PrintProduct(string title)
    {
        this.title = title;
    }

    // Абстрактный метод для вывода информации о продукции
    public abstract void PrintInfo();

    // Абстрактный метод для вычисления общей стоимости продукции
    public abstract double GetTotalPrice();
}

// Класс для журналов, наследуется от PrintProduct
class Magazine : PrintProduct
{
    private int circulation; // Тираж
    private double price; // Цена за экземпляр

    // Конструктор с параметрами
    public Magazine(string title, int circulation, double price) : base(title)
    {
        this.circulation = circulation;
        this.price = price;
    }

    // Переопределение метода для вывода информации о журнале
    public override void PrintInfo()
    {
        Console.WriteLine($"Журнал: {title}");
        Console.WriteLine($"Тираж: {circulation}");
        Console.WriteLine($"Цена: {price}");
    }

    // Переопределение метода для вычисления общей стоимости журнала
    public override double GetTotalPrice()
    {
        return circulation * price;
    }
}

// Класс для газет, наследуется от PrintProduct
class Newspaper : PrintProduct
{
    private int pageCount; // Количество страниц
    private double pageCost; // Стоимость одной страницы
    private int circulation; // Тираж

    // Конструктор с параметрами
    public Newspaper(string title, int pageCount, double pageCost, int circulation) : base(title)
    {
        this.pageCount = pageCount;
        this.pageCost = pageCost;
        this.circulation = circulation;
    }

    // Переопределение метода для вывода информации о газете
    public override void PrintInfo()
    {
        Console.WriteLine($"Газета: {title}");
        Console.WriteLine($"Количество листов: {pageCount}");
        Console.WriteLine($"Стоимость листа: {pageCost}");
        Console.WriteLine($"Тираж: {circulation}");
    }

    // Переопределение метода для вычисления общей стоимости газеты
    public override double GetTotalPrice()
    {
        return pageCount * pageCost * circulation;
    }
}

// Основной класс программы
class Program
{
    static void Main()
    {
        try
        {
            // Создание массива объектов печатной продукции
            PrintProduct[] products = new PrintProduct[2];

            // Ручной ввод данных для журналов
            Console.WriteLine("Введите данные для журнала:");
            Console.Write("Название: ");
            string magazineTitle = Console.ReadLine();
            Console.Write("Тираж: ");
            int magazineCirculation = int.Parse(Console.ReadLine());
            Console.Write("Цена: ");
            double magazinePrice = double.Parse(Console.ReadLine());
            products[0] = new Magazine(magazineTitle, magazineCirculation, magazinePrice);

            // Ручной ввод данных для газет
            Console.WriteLine("\nВведите данные для газеты:");
            Console.Write("Название: ");
            string newspaperTitle = Console.ReadLine();
            Console.Write("Количество страниц: ");
            int newspaperPageCount = int.Parse(Console.ReadLine());
            Console.Write("Стоимость одной страницы: ");
            double newspaperPageCost = double.Parse(Console.ReadLine());
            Console.Write("Тираж: ");
            int newspaperCirculation = int.Parse(Console.ReadLine());
            products[1] = new Newspaper(newspaperTitle, newspaperPageCount, newspaperPageCost, newspaperCirculation);

            double totalMagazinePrice = 0;
            int magazineCount = 0;
            double totalNewspaperPrice = 0;
            int newspaperCount = 0;

            // Вывод информации о каждой продукции и её общей стоимости
            foreach (var product in products)
            {
                product.PrintInfo();
                Console.WriteLine($"Стоимость тиража: {product.GetTotalPrice()}");
                Console.WriteLine();

                // Подсчёт общей стоимости журналов и газет
                if (product is Magazine)
                {
                    totalMagazinePrice += product.GetTotalPrice();
                    magazineCount++;
                }
                else if (product is Newspaper)
                {
                    totalNewspaperPrice += product.GetTotalPrice();
                    newspaperCount++;
                }
            }

            // Вычисление средней стоимости журналов и газет
            double avgMagazinePrice = totalMagazinePrice / magazineCount;
            double avgNewspaperPrice = totalNewspaperPrice / newspaperCount;

            // Вывод средней стоимости журналов и газет
            Console.WriteLine($"Средняя стоимость журналов: {avgMagazinePrice}");
            Console.WriteLine($"Средняя стоимость газет: {avgNewspaperPrice}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}