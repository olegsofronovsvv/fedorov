using System;
class Program
{
    static double ReadDouble() { while(true) { if(double.TryParse(Console.ReadLine(),out double value) && double.IsFinite(value)) return value; Console.Write("Введите число: "); } }
    static void Main()
    {
        Console.Write("Длина: ");
        double a=ReadDouble();
        Console.Write("Ширина: ");
        double b=ReadDouble();
        Console.WriteLine("Площадь: "+a*b);
        Console.WriteLine("Периметр: "+2*(a+b));
    }
}

