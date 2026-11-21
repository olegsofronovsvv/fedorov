using System;
using System.Collections.Generic;
using System.IO;
class Program
{
    static void Main()
    {
        string[] stock=
        {
            "Корпус A-10","Плата ЦП-v2","Модуль ОЗУ-8G","Блок питания-500W"
        }
        ;
        int[] indices=
        {
            0,2,1,4,3
        }
        ;
        List<string> order=new List<string>();
        string log=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"warehouse_errors.log");
        foreach(int index in indices)
        {
            try
            {
                string item=stock[index];
                order.Add(item);
                Console.WriteLine("Добавлен: "+item);
            }
            catch(IndexOutOfRangeException ex)
            {
                string line=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" | Индекс: "+index+" | Ошибка: "+ex.Message;
                try { File.AppendAllText(log,line+Environment.NewLine); } catch(IOException error) { Console.WriteLine("Не удалось записать журнал: "+error.Message); } catch(UnauthorizedAccessException error) { Console.WriteLine("Нет доступа к журналу: "+error.Message); }
                Console.WriteLine(line);
            }
        }
        Console.WriteLine("Подготовлено: "+order.Count);
    }
}

