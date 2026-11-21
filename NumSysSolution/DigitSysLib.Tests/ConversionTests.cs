using DigitSysLib;
using Xunit;

namespace DigitSysLib.Tests;

public class ConversionTests
{
    private readonly Adapter adapter = new();

    [Theory]
    [InlineData("1101", 2, 10, "13")]
    [InlineData("77", 8, 10, "63")]
    [InlineData("FF", 16, 10, "255")]
    [InlineData("13", 10, 2, "1101")]
    [InlineData("63", 10, 8, "77")]
    [InlineData("255", 10, 16, "FF")]
    public void BasicConversion(string value, int fromBase, int toBase, string expected)
    {
        var result = adapter.Convert(value, fromBase, toBase);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void InvalidBinaryCharacter()
    {
        Assert.Throws<ArgumentException>(() => adapter.Convert("102", 2, 10));
    }

    [Fact]
    public void EmptyInput()
    {
        Assert.Throws<ArgumentException>(() => adapter.Convert("", 2, 10));
    }

    [Fact]
    public void InvalidBase()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => adapter.Convert("10", 1, 2));
    }

    [Fact]
    public void MaximumValueAndOverflow()
    {
        Assert.Equal(long.MaxValue.ToString(), adapter.Convert(new string('1', 63), 2, 10));
        Assert.Throws<OverflowException>(() => adapter.Convert(new string('1', 64), 2, 10));
        Assert.Throws<OverflowException>(() => adapter.Convert("9223372036854775808", 10, 2));
    }
}
