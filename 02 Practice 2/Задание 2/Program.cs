using System;
class Program
{
    static double ReadDouble() { while(true) { if(double.TryParse(Console.ReadLine(),out double value) && double.IsFinite(value)) return value; Console.Write("Введите число: "); } }
    static void Main()
    {
        Console.Write("Текущий курс (рублей за доллар): ");
        double rate=ReadDouble();
        if(rate<=0)
        {
            Console.WriteLine("Курс должен быть положительным");
            return;
        }
        Console.Write("Рубли: ");
        double rub=ReadDouble();
        Console.WriteLine("Доллары: "+rub/rate);
    }
}

