using System;
class Program
{
    static int ReadInt() { while(true) { if(int.TryParse(Console.ReadLine(),out int value)) return value; Console.Write("Введите целое число: "); } }
    static void Main()
    {
        Console.Write("Размер массива N: ");
        int n = ReadInt();
        if (n <= 0)
        {
            Console.WriteLine("N должно быть положительным");
            return;
        }
        int[] a = new int[n];
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            Console.Write("Число " + (i + 1) + ": ");
            a[i] = ReadInt();
            sum += a[i];
        }
        Console.Write("Обратный порядок: ");
        for (int i = n - 1; i >= 0; i--) Console.Write(a[i] + " ");
        Console.WriteLine();
        double avg = sum / n;
        int best = a[0];
        for (int i = 1; i < n; i++) if (Math.Abs(a[i] - avg) < Math.Abs(best - avg)) best = a[i];
        Console.WriteLine("Среднее: " + avg);
        Console.WriteLine("Ближайшее число: " + best);
    }
}

