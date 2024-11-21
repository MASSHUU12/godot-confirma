using System;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class TestCaseTest
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

    public static void EmptyMethod() { }

    public static void MethodWParams(params int[] nums) { }

    public static void MethodWParamsAndOtherArgs(bool b, params int[] nums) { }
    #endregion TestMethods

    [TestCase]
    public void Constructor_SetsPropertiesCorrectly()
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

    #region Run
    [TestCase]
    public void Run_MethodInvokesSuccessfully()
    {
        MethodInfo? method = typeof(TestCaseTest).GetMethod(nameof(TestMethod));
        object[] parameters = new object[] { 1, "test" };
        TestCase testCase = new(method!, parameters, null);

        _ = Confirm.NotThrows<ConfirmAssertException>(() => testCase.Run());
    }

    [TestCase]
    public void Run_ThrowsTargetInvocationException()
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
    public void Run_ThrowsArgumentException()
    {
        MethodInfo method = typeof(TestCaseTest).GetMethod(nameof(TestMethod))!;
        object[] parameters = new[] { "Hello", "test" };
        TestCase testCase = new(method, parameters, null);

        Action action = () => testCase.Run();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            $"- Failed: Invalid test case parameters: {testCase.Params}."
        );
    }

    [TestCase]
    public void Run_ThrowsOtherException()
    {
        MethodInfo? method = typeof(TestCaseTest)
            .GetMethod(nameof(TestMethod));
        TestCase testCase = new(method!, null, null);

        Action action = () => testCase.Run();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "- Failed: Parameter count mismatch."
        );
    }
    #endregion Run

    #region GenerateArguments
    [TestCase]
    public void GenerateArguments_EmptyMethod_NoParameters()
    {
        MethodInfo method = typeof(TestCaseTest).GetMethod(nameof(EmptyMethod))!;

        _ = new TestCase(method, null, null).Parameters.ConfirmNull();
    }

    [TestCase]
    public void GenerateArguments_TestMethod_ReturnsAllParameters()
    {
        MethodInfo method = typeof(TestCaseTest).GetMethod(nameof(TestMethod))!;

        _ = new TestCase(method, new object[] { 5, "five" }, null).Parameters!
            .ConfirmCount(2);
    }

    [TestCase]
    public void GenerateArguments_MethodWParams_ReturnsArray()
    {
        MethodInfo method = typeof(TestCaseTest).GetMethod(nameof(MethodWParams))!;
        object?[] p = new TestCase(method, new object[] { 1, 2, 3 }, null).Parameters!;

        _ = p.ConfirmCount(1);
        _ = p[0].ConfirmEqual(new object[] { 1, 2, 3 });
    }

    [TestCase]
    public void GenerateArguments_MethodWParamsAndOtherArgs_ReturnsGeneratedArgs()
    {
        MethodInfo method = typeof(TestCaseTest)
            .GetMethod(nameof(MethodWParamsAndOtherArgs))!;
        object?[] p = new TestCase(method, new object[] { true, 1, 2, 3 }, null)
            .Parameters!;

        _ = p.ConfirmCount(2);
        _ = p[0].ConfirmEqual(true);
        _ = p[1].ConfirmEqual(new object[] { 1, 2, 3 });
    }
    #endregion GenerateArguments
}
