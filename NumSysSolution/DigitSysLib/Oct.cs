namespace DigitSysLib;
internal sealed class Oct : ConverterBase { internal string ConvertTo10(string value) => ToBase10(value, 8); internal string ConvertFrom10(string value) => FromBase10(value, 8); }
