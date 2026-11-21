namespace DigitSysLib;
public sealed class Adapter
{
    private readonly Binary binary = new();
    private readonly Oct oct = new();
    private readonly Hex hex = new();
    private readonly Generic generic = new();
    public string Convert(string value, int fromBase, int toBase)
    {
        if (fromBase is < 2 or > 16) throw new ArgumentOutOfRangeException(nameof(fromBase));
        if (toBase is < 2 or > 16) throw new ArgumentOutOfRangeException(nameof(toBase));
        string decimalValue = fromBase switch { 2 => binary.ConvertTo10(value), 8 => oct.ConvertTo10(value), 16 => hex.ConvertTo10(value), _ => generic.ConvertTo10(value, fromBase) };
        return toBase switch { 2 => binary.ConvertFrom10(decimalValue), 8 => oct.ConvertFrom10(decimalValue), 16 => hex.ConvertFrom10(decimalValue), _ => generic.ConvertFrom10(decimalValue, toBase) };
    }
    private sealed class Generic : ConverterBase
    {
        internal string ConvertTo10(string value, int radix) => ToBase10(value, radix);
        internal string ConvertFrom10(string value, int radix) => FromBase10(value, radix);
    }
}
