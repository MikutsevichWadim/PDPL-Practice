using System;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Obj obj = new Obj();
            Console.Write("Объект.F1:");
            Console.WriteLine(obj.F1(5));
            Console.Write("Объект.IxF0:");

            Console.WriteLine(obj.IxF0(5));
            Console.Write("Объект.IxF1:");

            Console.WriteLine(obj.IxF1(5));
            Console.Write("IZ Объект.F1:");

            Console.WriteLine((obj as IZ).F1(5));
            Console.Write("IY Объект.F1:");

            Console.WriteLine((obj as IY).F1(5));
            IZ L_IZ;
            L_IZ = obj;
            Console.Write("Ссылка на интерфейс IZ, вызов метода F1:");
            Console.WriteLine(L_IZ.F1(5));
        }

    }
    // Интерфейс IX с методами IxF0 и IxF1
    interface IX
    {
        int IxF0(int w);
        int IxF1(int w);
    }
    // Интерфейс IY с методами F0 и F1
    interface IY
    {
        int F0(int w);
        int F1(int w);
    }
    // Интерфейс IZ с методами F0 и F1
    interface IZ
    {
        int F0(int w);
        int F1(int w);
    }
    // Класс Obj, реализующий интерфейсы IX, IY, IZ
    class Obj : IX, IY, IZ
    {
        // Реализация метода IxF0 интерфейса IX
        public int IxF0(int w)
        {
            return w * w - w;
        }

        // Реализация метода IxF1 интерфейса IX
        public int IxF1(int w)
        {
            return w * w - w;
        }

        // Реализация метода F0 интерфейса IY
        public int F0(int w)
        {
            return 15 / w;
        }

        // Реализация метода F1 интерфейса IY
        public int F1(int w)
        {
            return 15 / w;
        }

        // Реализация метода F0 интерфейса IZ
        int IZ.F0(int w)
        {
            return 2 * w - 3;
        }

        // Реализация метода F1 интерфейса IZ
        int IZ.F1(int w)
        {
            return 2 * w - 3;
        }

    }
}
