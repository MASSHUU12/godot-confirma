using System;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class TestCaseTest
{
    #region TestMethods
    public static void TestMethod(int param1, string param2)
    {
        // Do nothing
    }

    public static void TestMethodThatThrows()
    {
        throw new Exception("Test exception");
    }
    #endregion TestMethods

    [TestCase]
    public static void Constructor_SetsPropertiesCorrectly()
    {
        MethodInfo? method = typeof(TestCaseTest).GetMethod("TestMethod");
        object[] parameters = new object[] { 1, "test" };
        RepeatAttribute repeat = new(2);

        TestCase testCase = new(method!, parameters, repeat);

        _ = testCase.Method.ConfirmEqual(method);
        _ = testCase.Parameters!.ConfirmEqual(parameters);
        _ = testCase.Params.ConfirmEqual("1, \"test\"");
        _ = testCase.Repeat.ConfirmEqual(repeat);
    }

    [TestCase]
    public static void Run_MethodInvokesSuccessfully()
    {
        MethodInfo? method = typeof(TestCaseTest).GetMethod(nameof(TestMethod));
        object[] parameters = new object[] { 1, "test" };
        TestCase testCase = new(method!, parameters, null);

        _ = Confirm.NotThrows<ConfirmAssertException>(testCase.Run);
    }

    [TestCase]
    public static void Run_ThrowsTargetInvocationException()
    {
        MethodInfo? method = typeof(TestCaseTest)
            .GetMethod(nameof(TestMethodThatThrows));
        TestCase testCase = new(method!, null, null);

        Action action = () => testCase.Run();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Test exception"
        );
    }

    [TestCase]
    public static void Run_ThrowsArgumentException()
    {
        MethodInfo? method = typeof(TestCaseTest)
            .GetMethod(nameof(TestMethod));
        object[] parameters = new[] { "Hello", "test" };
        TestCase testCase = new(method!, parameters, null);

        Action action = () => testCase.Run();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            $"- Failed: Invalid test case parameters: {testCase.Params}."
        );
    }

    [TestCase]
    public static void Run_ThrowsOtherException()
    {
        MethodInfo? method = typeof(TestCaseTest)
            .GetMethod(nameof(TestMethod));
        TestCase testCase = new(method!, null, null);

        Action action = () => testCase.Run();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "- Failed: Parameter count mismatch."
        );
    }
}
