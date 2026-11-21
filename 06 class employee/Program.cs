using System;
using System.Collections.Generic;
using System.IO;
namespace EmployeeTask;
class Program
{
    static void Main()
    {
        string dir=AppContext.BaseDirectory;
        string input=Path.Combine(dir,"employee.txt");
        if(!File.Exists(input))
        {
            Console.WriteLine("Нет файла employee.txt");
            return;
        }
        List<Employee> people;
        try { people=new DataReader().Read(input,Path.Combine(dir,"log.txt")); }
        catch(IOException ex) { Console.WriteLine("Ошибка чтения или записи файла: "+ex.Message); return; }
        catch(UnauthorizedAccessException ex) { Console.WriteLine("Нет доступа к файлу: "+ex.Message); return; }
        if(people.Count==0)
        {
            Console.WriteLine("Нет корректных строк");
            return;
        }
        decimal sum=0;
        Employee oldest=people[0];
        foreach(Employee e in people)
        {
            sum+=e.Salary;
            if(e.Age>oldest.Age)oldest=e;
        }
        try
        {
            using StreamWriter writer=new StreamWriter(Path.Combine(dir,"result.txt"));
            writer.WriteLine("Записей: "+people.Count);
            writer.WriteLine("Средняя зарплата: "+sum/people.Count);
            writer.WriteLine("Самый старший: "+oldest.Name+", "+oldest.Age+" лет");
        }
        catch(IOException ex) { Console.WriteLine("Ошибка записи результата: "+ex.Message); return; }
        catch(UnauthorizedAccessException ex) { Console.WriteLine("Нет доступа к файлу результата: "+ex.Message); return; }
        Console.WriteLine("Успешно: "+people.Count);
    }
}
