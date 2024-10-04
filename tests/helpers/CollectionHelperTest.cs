using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class CollectionHelperTest
{
    public static void ToString_EmptyEnumerable_ReturnsEmptyArrayString()
    {
        IEnumerable<object> value = Enumerable.Empty<object>();
        _ = CollectionHelper.ToString(value).ConfirmEqual("[]");
    }

    [TestCase]
    public static void ToString_SingleElementEnumerable_ReturnsSingleElementArrayString()
    {
        IEnumerable<string> value = new[] { "Hello" };

        _ = CollectionHelper.ToString(value).ConfirmEqual("[\"Hello\"]");
    }

    [TestCase]
    public static void ToString_MultipleElementList_ReturnsMultipleElementArrayString()
    {
        List<float> value = new() { 20f, 25f };

        _ = CollectionHelper.ToString(value).ConfirmEqual("[20.00000, 25.00000]");
    }

    [TestCase]
    public static void ToString_SingleElementArray_ReturnsSingleElementArrayString()
    {
        object[] value = new[] { "Hello" };

        _ = CollectionHelper.ToString(value).ConfirmEqual("[\"Hello\"]");
    }

    [TestCase]
    public static void ToString_MultipleElementArray_ReturnsMultipleElementArrayString()
    {
        object[] value = new[] { "Hello", "World" };

        _ = CollectionHelper.ToString(value).ConfirmEqual("[\"Hello\", \"World\"]");
    }
}
