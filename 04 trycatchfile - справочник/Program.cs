using System;
using System.Collections.Generic;
using System.IO;
class Employee
{
    public string FullName
    {
        get;
        set;
    }
    public string Department
    {
        get;
        set;
    }
}
class Program
{
    static void Main()
    {
        List<Employee> people=new List<Employee>
        {
            new Employee
            {
                FullName="Иванов И.И.",Department="IT"
            }
            ,null,new Employee
            {
                FullName=null,Department="HR"
            }
            ,new Employee
            {
                FullName="Петров П.П.",Department="Finance"
            }
        }
        ;
        string log=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"directory_errors.log");
        for(int i=0;i<people.Count;i++)
        {
            try
            {
                string name=people[i].FullName.ToUpper();
                Console.WriteLine("Сотрудник: "+name+" [Департамент: "+people[i].Department+"]");
            }
            catch(NullReferenceException ex)
            {
                string line=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")+" | Индекс: "+i+" | Ошибка: "+ex.Message;
                try { File.AppendAllText(log,line+Environment.NewLine); } catch(IOException error) { Console.WriteLine("Не удалось записать журнал: "+error.Message); } catch(UnauthorizedAccessException error) { Console.WriteLine("Нет доступа к журналу: "+error.Message); }
                Console.WriteLine(line);
            }
        }
    }
}

