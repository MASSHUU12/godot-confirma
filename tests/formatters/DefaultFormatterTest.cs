using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[SetUp]
[TestClass]
[Parallelizable]
public class DefaultFormatterTest
{
    private static DefaultFormatter? _formatter;

    public void SetUp()
    {
        _formatter = new DefaultFormatter();
    }

    [TestCase]
    public void Format_NullValue_ReturnsNullString()
    {
        object? value = null;
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("null");
    }

    [TestCase]
    public void Format_NonNullValue_ReturnsToStringResult()
    {
        object value = "Hello, World!";
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("Hello, World!");
    }

    [TestCase]
    public void Format_ValueWithToStringOverride_ReturnsToStringResult()
    {
        object value = new TestObject { Name = "John" };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("John");
    }

    [TestCase]
    public void Format_ValueWithoutToStringOverride_ReturnsTypeAndHashcode()
    {
        object value = new();
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("System.Object");
    }

    private class TestObject
    {
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
