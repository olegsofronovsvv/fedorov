using DigitSysLib;

var converter = new Adapter();
Console.WriteLine("Перевод чисел между системами счисления от 2 до 16.");
Console.WriteLine("Примеры: 1101 (2) -> 13 (10), 255 (10) -> FF (16).");

while (true)
{
    Console.Write("Число (пустая строка — выход): ");
    string? value = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(value)) break;

    int fromBase = ReadBase("Из какой системы (2–16): ");
    int toBase = ReadBase("В какую систему (2–16): ");

    try
    {
        Console.WriteLine($"Результат: {value} ({fromBase}) = {converter.Convert(value, fromBase, toBase)} ({toBase})");
    }
    catch (ArgumentException error)
    {
        Console.WriteLine("Ошибка: " + error.Message);
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
