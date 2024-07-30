using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ChainingTest
{
    [Parallelizable]
    private static class TestClass { }

    [TestCase]
    public static void ChainingOnArray()
    {
        _ = new int[] { 0, 1, 2, 3, 4 }.ConfirmNotEmpty().ConfirmContains(2);
    }

    [TestCase]
    public static void ChainingOnAttribute()
    {
        _ = typeof(TestClass)
            .ConfirmIsDecoratedWith<ParallelizableAttribute>()
            .ConfirmIsNotDecoratedWith<TestClassAttribute>();
    }

    [TestCase]
    public static void ChainingOnBoolean()
    {
        _ = true.ConfirmTrue().ConfirmTrue();
    }

    [TestCase]
    public static void ChainingOnDictionary()
    {
        _ = new Dictionary<string, int>()
        {
            {"key1", 0},
            {"key2", 0},
            {"key3", 0},
        }
        .ConfirmContainsKey("key1")
        .ConfirmNotContainsKey("key4")
        .ConfirmNotContainsValue(1);
    }

    [TestCase]
    public static void ChainingOnEqual()
    {
        _ = 5.ConfirmEqual(5).ConfirmNotEqual(6);
    }

    [TestCase]
    public static void ChainingOnException()
    {
        Action action = static () => throw new IndexOutOfRangeException();

        _ = action
            .ConfirmThrows<IndexOutOfRangeException>()
            .ConfirmNotThrows<OutOfMemoryException>();
    }

    [TestCase]
    public static void ChainingOnFile()
    {
        new StringName("./LICENSE")
            .ConfirmIsFile()
            .ConfirmFileContains("MIT License")
            .Dispose();
    }

    [TestCase]
    public static void ChainingOnIEnumerable()
    {
        _ = new int[] { 0, 1, 2, 3, 4 }
            .ConfirmNotEmpty()
            .ConfirmContains(2)
            .ConfirmElementsAreUnique();
    }

    [TestCase]
    public static void ChainingOnConfirmNull()
    {
        _ = 5.ConfirmNotNull().ConfirmNotNull();
    }

    [TestCase]
    public static void ChainingOnNumeric()
    {
        _ = 5.ConfirmIsNotZero().ConfirmIsPositive();
    }

    [TestCase]
    public static void ChainingOnRange()
    {
        _ = 5.ConfirmGreaterThan(3).ConfirmInRange(0, 10);
    }

    [TestCase]
    public static void ChainingOnSignal()
    {
        new Button()
            .ConfirmSignalExists("pressed")
            .ConfirmSignalDoesNotExist("Lorem ipsum")
            .Free();
    }

    [TestCase]
    public static void ChainingOnString()
    {
        _ = "Lorem ipsum dolor sit amet"
            .ConfirmStartsWith("Lorem")
            .ConfirmEndsWith("amet");
    }

    [TestCase]
    public static void ChainingOnType()
    {
        _ = "Lorem ipsum dolor sit amet"
            .ConfirmType<string>()
            .ConfirmNotType<int>();
    }

    [TestCase]
    public static void ChainingOnUuid()
    {
        _ = Guid.NewGuid().ToString().ConfirmValidUuid4().ConfirmValidUuid4();
    }

    [TestCase]
    public static void ChainingOnVector()
    {
        _ = Vector2.Up.ConfirmEqual(Vector2.Up).ConfirmNotEqual(Vector2.Down);
    }
}
