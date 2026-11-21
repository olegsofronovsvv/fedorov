using System;
using System.Collections.Generic;
using System.IO;
namespace EmployeeTask;
public class DataReader
{
    public List<Employee> Read(string input,string log)
    {
        List<Employee> people=new List<Employee>();
        using StreamReader reader=new StreamReader(input);
        using StreamWriter writer=new StreamWriter(log);
        string line;
        int n=0;
        while((line=reader.ReadLine())!=null)
        {
            n++;
            string[] p=line.Split(';');
            if(p.Length!=3)
            {
                writer.WriteLine("Строка "+n+": требуется три поля");
                continue;
            }
            if(string.IsNullOrWhiteSpace(p[0]))
            {
                writer.WriteLine("Строка "+n+": пустое имя");
                continue;
            }
            if(!int.TryParse(p[1],out int age)||age<0)
            {
                writer.WriteLine("Строка "+n+": неверный возраст");
                continue;
            }
            if(!decimal.TryParse(p[2],out decimal salary)||salary<0)
            {
                writer.WriteLine("Строка "+n+": неверная зарплата");
                continue;
            }
            people.Add(new Employee
            {
                Name=p[0],Age=age,Salary=salary
            }
            );
        }
        return people;
    }
}
