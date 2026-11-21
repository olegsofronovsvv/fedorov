using System;
using System.Collections.Generic;
using System.Text;
class Program
{
    static void Main()
    {
        Console.InputEncoding=Encoding.UTF8;
        Dictionary<string,int> words=new Dictionary<string,int>();
        for(int i=0;i<3;i++)
        {
            Console.Write("Слово: ");
            string word=Console.ReadLine();
            if(string.IsNullOrWhiteSpace(word)) { Console.WriteLine("Слово не может быть пустым"); i--; continue; }
            if(words.ContainsKey(word))words[word]++;
            else words[word]=1;
            Console.WriteLine(word+": "+words[word]);
        }
        foreach(var item in words)Console.WriteLine(item.Key+" = "+item.Value);
    }
}

