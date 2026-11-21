using System;
class Program
{
    static double ReadDouble() { while(true) { if(double.TryParse(Console.ReadLine(),out double value) && double.IsFinite(value)) return value; Console.Write("Введите число: "); } }
    static void Main()
    {
        double[,] marks=new double[3,5];
        double[] average=new double[3];
        for(int i=0;i<3;i++)
        {
            double sum=0;
            for(int j=0;j<5;j++)
            {
                Console.Write($"Студент {i+1}, оценка {j+1}: ");
                marks[i,j]=ReadDouble();
                sum+=marks[i,j];
            }
            average[i]=sum/5;
        }
        for(int i=0;i<3;i++)Console.WriteLine($"Средняя оценка студента {i+1}: {average[i]}");
    }
}

