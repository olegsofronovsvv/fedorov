using System;
class Program
{
    static void Main()
    {
        int[,] a=new int[3,3],b=new int[3,3];
        for(int i=0;i<3;i++)
        {
            Console.Write($"Строка {i+1}, три числа через пробел: ");
            string[] parts=(Console.ReadLine() ?? "").Split(' ',StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length!=3)
            {
                Console.WriteLine("Нужно три числа");
                return;
            }
            for(int j=0;j<3;j++) { if(!int.TryParse(parts[j],out a[i,j])) { Console.WriteLine("Нужно ввести целые числа"); return; } }
        }
        for(int i=0;i<3;i++)for(int j=0;j<3;j++)b[j,i]=a[i,j];
        Console.WriteLine("Новая матрица:");
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)Console.Write(b[i,j]+" ");
            Console.WriteLine();
        }
    }
}

