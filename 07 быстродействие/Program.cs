using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
class Program
{
    static void Main(string[] args)
    {
        if(args.Length==0)
        {
            Console.WriteLine("Команды: generate [число], upper-char, upper-buffer, compare, count");
            return;
        }
        string input=Path.Combine(AppContext.BaseDirectory,"lorem.txt"),output=Path.Combine(AppContext.BaseDirectory,"lorem_output.txt");
        Stopwatch sw=Stopwatch.StartNew();
        if(args[0]=="generate")
        {
            int parsed=10000;
            if(args.Length>1 && (!int.TryParse(args[1],out parsed)||parsed<0))
            {
                Console.WriteLine("Количество должно быть неотрицательным целым числом");
                return;
            }
            int n=parsed;
            using StreamWriter writer=new StreamWriter(input);
            for(int i=0;i<n;i++)writer.WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
        }
        else if(!File.Exists(input))
        {
            Console.WriteLine("Сначала запустите generate");
            return;
        }
        else if(args[0]=="compare")
        {
            string charOutput=Path.Combine(AppContext.BaseDirectory,"lorem_char.txt");
            string bufferOutput=Path.Combine(AppContext.BaseDirectory,"lorem_buffer.txt");
            sw.Restart();
            UpperChar(input,charOutput);
            sw.Stop();
            long charMs=sw.ElapsedMilliseconds;
            sw.Restart();
            UpperBuffer(input,bufferOutput);
            sw.Stop();
            Console.WriteLine("Посимвольно: "+charMs+" мс; блоками: "+sw.ElapsedMilliseconds+" мс");
            Console.WriteLine("Результаты совпадают: "+(File.ReadAllText(charOutput)==File.ReadAllText(bufferOutput)));
            Console.WriteLine("Оба способа используют внутренние буферы StreamReader и StreamWriter.");
            return;
        }
        else if(args[0]=="upper-char")
        {
            UpperChar(input,output);
        }
        else if(args[0]=="upper-buffer")
        {
            UpperBuffer(input,output);
        }
        else if(args[0]=="count")
        {
            Dictionary<char,long> counts=new Dictionary<char,long>();
            using StreamReader reader=new StreamReader(input);
            int ch;
            while((ch=reader.Read())!=-1)
            {
                char c=(char)ch;
                if(counts.ContainsKey(c))counts[c]++;
                else counts[c]=1;
            }
            foreach(var pair in counts)Console.WriteLine(pair.Key+": "+pair.Value);
        }
        else
        {
            Console.WriteLine("Неизвестная команда");
            return;
        }
        sw.Stop();
        Console.WriteLine("Время: "+sw.ElapsedMilliseconds+" мс");
    }
    static void UpperChar(string input,string output)
    {
        using StreamReader reader=new StreamReader(input);
        using StreamWriter writer=new StreamWriter(output);
        int ch;
        while((ch=reader.Read())!=-1)writer.Write(char.ToUpperInvariant((char)ch));
    }
    static void UpperBuffer(string input,string output)
    {
        char[] buffer=new char[8192];
        using StreamReader reader=new StreamReader(input);
        using StreamWriter writer=new StreamWriter(output);
        int count;
        while((count=reader.Read(buffer,0,buffer.Length))>0)
        {
            for(int i=0;i<count;i++)buffer[i]=char.ToUpperInvariant(buffer[i]);
            writer.Write(buffer,0,count);
        }
    }
}
