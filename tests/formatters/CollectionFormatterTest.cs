using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[SetUp]
[TestClass]
[Parallelizable]
public static class CollectionFormatterTest
{
    private static CollectionFormatter? _formatter;

    public static void SetUp()
    {
        _formatter = new();
    }

    [TestCase]
    public static void Format_EmptyEnumerable_ReturnsEmptyArrayString()
    {
        IEnumerable<object> value = Enumerable.Empty<object>();
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[]");
    }

    [TestCase]
    public static void Format_SingleElementEnumerable_ReturnsSingleElementArrayString()
    {
        IEnumerable<string> value = new[] { "Hello" };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[\"Hello\"]");
    }

    [TestCase]
    public static void Format_MultipleElementList_ReturnsMultipleElementArrayString()
    {
        List<float> value = new() { 20f, 25f };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[20.00000, 25.00000]");
    }

    [TestCase]
    public static void Format_EmptyArray_ReturnsEmptyArrayString()
    {
        float[] value = Array.Empty<float>();
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[]");
    }

    [TestCase]
    public static void Format_SingleElementArray_ReturnsSingleElementArrayString()
    {
        object[] value = new[] { "Hello" };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[\"Hello\"]");
    }

    [TestCase]
    public static void Format_MultipleElementArray_ReturnsMultipleElementArrayString()
    {
        object[] value = new[] { "Hello", "World" };
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("[\"Hello\", \"World\"]");
    }

    [TestCase]
    public static void Format_NonCollectionValue_UsesDefaultFormatter()
    {
        object value = "Hello";
        string result = _formatter!.Format(value);

        _ = result.ConfirmEqual("Hello");
    }
}
