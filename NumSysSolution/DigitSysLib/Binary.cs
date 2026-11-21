namespace DigitSysLib;
internal sealed class Binary : ConverterBase { internal string ConvertTo10(string value) => ToBase10(value, 2); internal string ConvertFrom10(string value) => FromBase10(value, 2); }
