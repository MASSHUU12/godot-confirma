using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmAttributeTest
{
    [TestCase(typeof(ConfirmAttributeTest), typeof(TestClassAttribute))]
    [TestCase(typeof(ConfirmAttributeTest), typeof(ParallelizableAttribute))]
    public void ConfirmIsDecoratedWith_WhenIsDecoratedWith(
        Type actual,
        Type expected
    )
    {
        _ = actual.ConfirmIsDecoratedWith(expected);
    }

    [TestCase(typeof(ParallelizableAttribute), typeof(ConfirmAttributeTest))]
    [TestCase(typeof(TestClassAttribute), typeof(ConfirmAttributeTest))]
    public void ConfirmIsDecoratedWith_WhenIsNotDecoratedWith(
        Type actual,
        Type expected
    )
    {
        Action action = () => actual.ConfirmIsDecoratedWith(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsDecoratedWith failed: "
            + $"Expected {expected.Name} to be decorated with {actual.Name}."
        );
    }

    [TestCase]
    public void ConfirmIsNotDecoratedWith_WhenIsNotDecoratedWith()
    {
        _ = typeof(ConfirmAttributeTest)
            .ConfirmIsNotDecoratedWith<AfterAllAttribute>();
    }

    [TestCase]
    public void ConfirmIsNotDecoratedWith_WhenIsDecoratedWith()
    {
        Action action = static () => typeof(ConfirmAttributeTest)
            .ConfirmIsNotDecoratedWith<TestClassAttribute>();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotDecoratedWith failed: "
            + "Expected ConfirmAttributeTest not to be decorated with "
            + "TestClassAttribute."
        );
    }
}
