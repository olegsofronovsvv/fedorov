using System;
using System.Collections.Generic;
using System.IO;
class Equipment
{
    public string ModelName
    {
        get;
        set;
    }
    public string SerialNumber
    {
        get;
        set;
    }
}
class Program
{
    static void Main()
    {
        List<Equipment> items=new List<Equipment>
        {
            new Equipment
            {
                ModelName="Сервер SRV-200",SerialNumber="SN-99482"
            }
            ,new Equipment
            {
                ModelName="Коммутатор SW-10",SerialNumber=" "
            }
            ,new Equipment
            {
                ModelName="Маршрутизатор RT-5",SerialNumber="SN-11023"
            }
        }
        ;
        string log=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"equipment_errors.log");
        foreach(Equipment item in items)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(item.SerialNumber))throw new ArgumentException("Серийный номер пустой");
                Console.WriteLine("Зарегистрировано: "+item.ModelName);
            }
            catch(ArgumentException ex)
            {
                string line="["+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+"] Модель: "+item.ModelName+" | Сбой: "+ex.Message;
                try { File.AppendAllText(log,line+Environment.NewLine); } catch(IOException error) { Console.WriteLine("Не удалось записать журнал: "+error.Message); } catch(UnauthorizedAccessException error) { Console.WriteLine("Нет доступа к журналу: "+error.Message); }
                Console.WriteLine(line);
            }
        }
    }
}

