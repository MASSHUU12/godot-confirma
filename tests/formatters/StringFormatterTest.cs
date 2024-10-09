using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[SetUp]
[TestClass]
[Parallelizable]
public class StringFormatterTest
{
    private static StringFormatter? _formatter;

    public void SetUp()
    {
        _formatter = new();
    }

    [TestCase]
    public void Format_NullValue_ReturnsNullString()
    {
        object value = new Person { Name = null };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"null\"");
    }

    [TestCase]
    public void Format_Object_ReturnsNullString()
    {
        object value = new();
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"System.Object\"");
    }

    [TestCase]
    public void Format_Char_ReturnsValidString()
    {
        _ = new StringFormatter('\'')!.Format('c').ConfirmEqual("'c'");
    }

    [TestCase]
    public void Format_ValueToString_ReturnsExpectedString()
    {
        object value = "Hello, World!";
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("\"Hello, World!\"");
    }

    [TestCase]
    public void Format_ValueToStringWithToStringOverride_ReturnsExpectedString()
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
