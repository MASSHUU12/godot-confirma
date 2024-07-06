using System;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ArrayHelperTest
{
    [TestCase]
    public static void ToString_WhenArrayIsNull()
    {
        object?[]? array = null;
        string result = ArrayHelper.ToString(array);

        result.ConfirmEqual(string.Empty);
    }

    [TestCase]
    public static void ToString_WhenArrayIsEmpty()
    {
        object?[]? array = Array.Empty<object?>();
        string result = ArrayHelper.ToString(array);

        result.ConfirmEqual(string.Empty);
    }

    [TestCase]
    public static void ToString_WhenArrayHasOneItem()
    {
        object?[]? array = new object?[] { 1 };
        string result = ArrayHelper.ToString(array);

        result.ConfirmEqual(array[0]?.ToString());
    }

    [TestCase(new object?[] { 1, 2 }, "1, 2")]
    [TestCase(new object?[] { 1, null }, "1, null")]
    [TestCase(new object?[] { null, 2 }, "null, 2")]
    [TestCase(new object?[] { new object?[] { 1, 2 } }, "[1, 2]")]
    [TestCase(new object?[] { new object?[] { 1, 2 }, new object?[] { 3, 4 } }, "[1, 2], [3, 4]")]
    [TestCase(new object?[] { "Hello, World", new object?[] { 2, "bbbb" }, 32 }, "Hello, World, [2, bbbb], 32")]
    public static void ToString_WhenArrayHasMultipleItems(object?[] array, string expected)
    {
        string result = ArrayHelper.ToString(array);

        result.ConfirmEqual(expected);
    }
}
