2using DigitSysLib;

var converter = new Adapter();
Console.WriteLine("Перевод чисел между системами счисления от 2 до 16.");
Console.WriteLine("Пример: 13 из системы 10 в систему 2 = 1101.");

while (true)
{
    Console.Write("Введите число (пустая строка — выход): ");
    string? value = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(value)) break;

    int fromBase = ReadBase("Исходная система счисления (2–16): ");
    int toBase = ReadBase("Нужная система счисления (2–16): ");

    try
    {
        string result = converter.Convert(value, fromBase, toBase);
        Console.WriteLine($"Результат: {value} ({fromBase}) = {result} ({toBase})");
    }
    catch (ArgumentException error)
    {
        Console.WriteLine("Ошибка ввода: " + error.Message);
    }
    catch (OverflowException)
    {
        Console.WriteLine("Ошибка: число слишком большое для типа long.");
    }
}

static int ReadBase(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int radix) && radix is >= 2 and <= 16)
            return radix;
        Console.WriteLine("Введите целое число от 2 до 16.");
    }
}
