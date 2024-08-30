using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class StringFormatterTest
{
    private static StringFormatter? _formatter;

    [SetUp]
    public static void SetUp()
    {
        _formatter = new();
    }

    [TestCase]
    public static void Format_NullValue_ReturnsNullString()
    {
        object value = new Person { Name = null };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"null\"");
    }

    [TestCase]
    public static void Format_Object_ReturnsNullString()
    {
        object value = new();
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"System.Object\"");
    }

    [TestCase]
    public static void Format_ValueToString_ReturnsExpectedString()
    {
        object value = "Hello, World!";
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"Hello, World!\"");
    }

    [TestCase]
    public static void Format_ValueToStringWithToStringOverride_ReturnsExpectedString()
    {
        object value = new Person { Name = "John Doe" };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"John Doe\"");
    }

    private class Person
    {
        public string? Name { get; set; }

        public override string? ToString()
        {
            return Name;
        }
    }
}
