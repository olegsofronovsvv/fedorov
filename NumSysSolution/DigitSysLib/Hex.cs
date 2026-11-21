namespace DigitSysLib;
internal sealed class Hex : ConverterBase { internal string ConvertTo10(string value) => ToBase10(value, 16); internal string ConvertFrom10(string value) => FromBase10(value, 16); }
