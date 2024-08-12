using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomEnumTest
{
    private static readonly Random rg = new();

    private enum TestEnum { A, B, C, D, E, F }

    [Repeat(6)]
    [TestCase]
    public static void NextEnumValue()
    {
        int value = rg.NextEnumValue<TestEnum>();

        _ = Confirm.IsEnumValue<TestEnum>(value);
    }

    [Repeat(6)]
    [TestCase]
    public static void NextEnumName()
    {
        string name = rg.NextEnumName<TestEnum>();

        _ = Confirm.IsEnumName<TestEnum>(name);
    }
}
