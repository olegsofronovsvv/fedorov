namespace DigitSysLib;
internal abstract class ConverterBase
{
    private const string Digits = "0123456789ABCDEF";
    protected string ToBase10(string value, int radix)
    {
        if (radix is < 2 or > 16) throw new ArgumentOutOfRangeException(nameof(radix));
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Введите число.", nameof(value));
        long result = 0;
        foreach (char c in value)
        {
            int digit = Digits.IndexOf(char.ToUpperInvariant(c));
            if (digit < 0 || digit >= radix) throw new ArgumentException("Недопустимый символ.", nameof(value));
            result = checked(result * radix + digit);
        }
        return result.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
    protected string FromBase10(string value, int radix)
    {
        if (radix is < 2 or > 16) throw new ArgumentOutOfRangeException(nameof(radix));
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Введите число.", nameof(value));
        if (!long.TryParse(value, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out long number))
            throw new OverflowException("Число вне диапазона Int64.");
        if (number == 0) return "0";
        var result = new System.Text.StringBuilder();
        while (number > 0) { result.Insert(0, Digits[(int)(number % radix)]); number /= radix; }
        return result.ToString();
    }
}
