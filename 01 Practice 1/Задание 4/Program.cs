using System;
using System.Collections.Generic;
using System.Text;
class Program
{
    static void Main()
    {
        Console.InputEncoding=Encoding.UTF8;
        Dictionary<string,string> book=new Dictionary<string,string>();
        while(true)
        {
            Console.Write("Команда: добавить, найти, показать, выход: ");
            string cmd=Console.ReadLine();
            if(cmd==null||cmd.ToLower()=="выход")break;
            if(cmd=="добавить")
            {
                Console.Write("Имя: ");
                string name=Console.ReadLine();
                Console.Write("Телефон: ");
                if(string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Имя не может быть пустым"); continue; }
                book[name]=Console.ReadLine() ?? "";
            }
            else if(cmd=="найти")
            {
                Console.Write("Имя: ");
                string name=Console.ReadLine();
                Console.WriteLine(name!=null && book.TryGetValue(name,out string phone)?phone:"Не найдено");
            }
            else if(cmd=="показать")
            {
                foreach(var item in book)Console.WriteLine(item.Key+": "+item.Value);
            }
            else Console.WriteLine("Неизвестная команда");
        }
    }
}

