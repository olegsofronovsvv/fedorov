using System;
using System.Collections.Generic;
class Program
{
    static int ReadInt() { while(true) { if(int.TryParse(Console.ReadLine(),out int value)) return value; Console.Write("Введите целое число: "); } }
    static void Main()
    {
        List<int> a=new List<int>();
        while(true)
        {
            Console.Write("Число: ");
            int x=ReadInt();
            if(a.Contains(x))
            {
                Console.WriteLine("Первый повтор: "+x);
                break;
            }
            a.Add(x);
        }
        Console.WriteLine("Массив до повтора: "+string.Join(" ",a));
    }
}

