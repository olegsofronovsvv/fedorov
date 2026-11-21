using System;
class Program
{
    static int ReadInt() { while(true) { if(int.TryParse(Console.ReadLine(),out int value)) return value; Console.Write("Введите целое число: "); } }
    static void Main()
    {
        Console.Write("Студентов: ");
        int n=ReadInt();
        Console.Write("Предметов: ");
        int m=ReadInt();
        if(n<=0||m<=0)
        {
            Console.WriteLine("Количество должно быть положительным");
            return;
        }
        int[,] marks=new int[n,m];
        for(int i=0;i<n;i++)
        {
            int sum=0;
            for(int j=0;j<m;j++)
            {
                Console.Write($"Студент {i+1}, предмет {j+1}: ");
                marks[i,j]=ReadInt();
                sum+=marks[i,j];
            }
            Console.WriteLine($"Среднее студента {i+1}: {(double)sum/m}");
        }
    }
}

