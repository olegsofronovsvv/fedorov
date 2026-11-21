using System;
class Program
{
    static double ReadDouble() { while(true) { if(double.TryParse(Console.ReadLine(),out double value) && double.IsFinite(value)) return value; Console.Write("Введите число: "); } }
    static void Main()
    {
        double[] days=new double[7];
        double sum=0;
        for(int i=0;i<7;i++)
        {
            Console.Write($"День {i+1}: ");
            days[i]=ReadDouble();
            sum+=days[i];
        }
        Console.WriteLine("Средняя температура: "+sum/7);
    }
}

