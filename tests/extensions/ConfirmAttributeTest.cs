using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmAttributeTest
{
    [TestCase(typeof(ConfirmAttributeTest), typeof(TestClassAttribute))]
    [TestCase(typeof(ConfirmAttributeTest), typeof(ParallelizableAttribute))]
    public static void ConfirmIsDecoratedWith_WhenIsDecoratedWith(Type actual, Type expected)
    {
        _ = actual.ConfirmIsDecoratedWith(expected);
    }

    [TestCase(typeof(ParallelizableAttribute), typeof(ConfirmAttributeTest))]
    [TestCase(typeof(TestClassAttribute), typeof(ConfirmAttributeTest))]
    public static void ConfirmIsDecoratedWith_WhenIsNotDecoratedWith(Type actual, Type expected)
    {
        Action action = () => actual.ConfirmIsDecoratedWith(expected);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
}
