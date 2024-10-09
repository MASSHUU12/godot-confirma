using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ChainingTest
{
    [Parallelizable]
    private class TestClass { }

    [TestCase]
    public void ChainingOnArray()
    {
        _ = new int[] { 0, 1, 2, 3, 4 }.ConfirmNotEmpty().ConfirmContains(2);
    }

    [TestCase]
    public void ChainingOnAttribute()
    {
        _ = typeof(TestClass)
            .ConfirmIsDecoratedWith<ParallelizableAttribute>()
            .ConfirmIsNotDecoratedWith<TestClassAttribute>();
    }

    [TestCase]
    public void ChainingOnBoolean()
    {
        _ = true.ConfirmTrue().ConfirmTrue();
    }

    [TestCase]
    public void ChainingOnDictionary()
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
    public void ChainingOnEqual()
    {
        _ = 5.ConfirmEqual(5).ConfirmNotEqual(6);
    }

    [TestCase]
    public void ChainingOnException()
    {
        Action action = static () => throw new IndexOutOfRangeException();

        _ = action
            .ConfirmThrows<IndexOutOfRangeException>()
            .ConfirmNotThrows<OutOfMemoryException>();
    }

    [TestCase]
    public void ChainingOnFile()
    {
        new StringName("./LICENSE")
            .ConfirmIsFile()
            .ConfirmFileContains("MIT License")
            .Dispose();
    }

    [TestCase]
    public void ChainingOnIEnumerable()
    {
        _ = new int[] { 0, 1, 2, 3, 4 }
            .ConfirmNotEmpty()
            .ConfirmContains(2)
            .ConfirmElementsAreUnique();
    }

    [TestCase]
    public void ChainingOnConfirmNull()
    {
        _ = 5.ConfirmNotNull().ConfirmNotNull();
    }

    [TestCase]
    public void ChainingOnNumeric()
    {
        _ = 5.ConfirmIsNotZero().ConfirmIsPositive();
    }

    [TestCase]
    public void ChainingOnRange()
    {
        _ = 5.ConfirmGreaterThan(3).ConfirmInRange(0, 10);
    }

    [TestCase]
    public void ChainingOnSignal()
    {
        new Button()
            .ConfirmSignalExists("pressed")
            .ConfirmSignalDoesNotExist("Lorem ipsum")
            .Free();
    }

    [TestCase]
    public void ChainingOnString()
    {
        _ = "Lorem ipsum dolor sit amet"
            .ConfirmStartsWith("Lorem")
            .ConfirmEndsWith("amet");
    }

    [TestCase]
    public void ChainingOnType()
    {
        _ = "Lorem ipsum dolor sit amet"
            .ConfirmType<string>()
            .ConfirmNotType<int>();
    }

    [TestCase]
    public void ChainingOnUuid()
    {
        _ = Guid.NewGuid().ToString().ConfirmValidUuid4().ConfirmValidUuid4();
    }

    [TestCase]
    public void ChainingOnVector()
    {
        _ = Vector2.Up.ConfirmEqual(Vector2.Up).ConfirmNotEqual(Vector2.Down);
    }
}
