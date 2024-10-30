using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Classes.Mock;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[SetUp]
[TestClass]
public class MockTest
{
    public interface ITestInterface
    {
        int GetInt();
        int? GetNullableInt();
        string? GetString();
        object? GetObject();
        void DoSomething();
        string StringMethod(string input);
    }

    private Mock<ITestInterface> _mock = new();

    public void SetUp()
    {
        _mock = new();
    }

    [TestCase]
    public void Instance_ShouldNotBeNull()
    {
        _ = _mock.Instance.ConfirmNotNull();
    }

    [TestCase]
    public void Instance_ShouldImplementInterface()
    {
        _ = _mock.Instance.GetType()
            .ImplementsAny<ITestInterface>()
            .ConfirmTrue();
    }

    [TestCase]
    public void MethodCalls_ShouldBeRecorded()
    {
        _mock.Instance.DoSomething();

        IReadOnlyCollection<CallRecord> callRecords = _mock.GetCallRecords();

        _ = callRecords.ConfirmCount(1);

        CallRecord record = callRecords.ElementAt(0);
        _ = record.MethodName.ConfirmEqual("DoSomething");
        _ = record.Arguments!.ConfirmEmpty();
    }

    [TestCase]
    public void MethodArguments_ShouldBeRecorded()
    {
        _ = _mock.Instance.StringMethod("test");

        IReadOnlyCollection<CallRecord> callRecords = _mock.GetCallRecords();

        _ = callRecords.ConfirmCount(1);

        CallRecord record = callRecords.ElementAt(0);
        _ = record.MethodName.ConfirmEqual("StringMethod");
        _ = record.Arguments!.ConfirmCount(1);
        _ = record.Arguments![0].ConfirmEqual("test");
    }

    [TestCase]
    public void DefaultReturnValue_ShouldBeReturned()
    {
        _mock.SetDefaultReturnValue("GetInt", 42);
        _ = _mock.Instance.GetInt().ConfirmEqual(42);
    }

    [TestCase]
    public void UndefinedMethod_ShouldThrowException()
    {
        _ = Confirm.Throws<ArgumentException>(
            () => _mock.SetDefaultReturnValue("NonExistentMethod", 0)
        );
    }

    [TestCase]
    public void DoSomething_ShouldHandleProperly()
    {
        _mock.Instance.DoSomething();

        IReadOnlyCollection<CallRecord> callRecords = _mock.GetCallRecords();

        _ = callRecords.ConfirmCount(1);
        _ = callRecords.ElementAt(0).MethodName.ConfirmEqual("DoSomething");
    }

    [TestCase]
    public void MethodWithoutDefaultReturn_ShouldReturnDefault()
    {
        string result = _mock.Instance.StringMethod("input");
        _ = result.ConfirmEmpty();
    }

    [TestCase]
    public void ProxyType_ShouldBeCreatedSuccessfully()
    {
        _ = typeof(ITestInterface)
            .IsAssignableFrom(_mock.Instance.GetType())
            .ConfirmTrue();
    }

    [TestCase]
    public void ProxyType_ShouldImplementAllInterfaceMethods()
    {
        MethodInfo[] interfaceMethods = typeof(ITestInterface).GetMethods();
        MethodInfo[] proxyMethods = _mock.Instance.GetType().GetMethods();

        foreach (MethodInfo iMethod in interfaceMethods)
        {
            _ = Confirm.IsTrue(
                proxyMethods.Any(
                    m => m.Name == iMethod.Name
                    && m.ReturnType == iMethod.ReturnType
                    && m.GetParameters().Length == iMethod.GetParameters().Length
                ),
                $"Method {iMethod.Name} is not implemented in proxy type."
            );
        }
    }

    [TestCase]
    public void ProxyType_ShouldHaveCorrectMethodReturnTypes()
    {
        MethodInfo? testMethod = _mock.Instance.GetType().GetMethod("GetInt");
        Type? returnType = testMethod?.ReturnType;

        _ = typeof(int).ConfirmEqual(returnType);
    }

    [TestCase]
    public void ProxyType_ShouldCorrectlyRunMethods()
    {
        _mock.SetDefaultReturnValue("GetInt", 42);
        _mock.SetDefaultReturnValue("GetNullableInt", (int?)null);
        _mock.SetDefaultReturnValue("GetString", (string?)null);
        _mock.SetDefaultReturnValue("GetObject", (object?)null);

        _ = _mock.Instance.GetInt().ConfirmEqual(42);
        _ = _mock.Instance.GetNullableInt().ConfirmNull();
        _ = _mock.Instance.GetString().ConfirmNull();
        _ = _mock.Instance.GetObject().ConfirmNull();

        _mock.SetDefaultReturnValue("GetNullableInt", 69);
        _mock.SetDefaultReturnValue("GetString", "Hello, World!");
        _mock.SetDefaultReturnValue("GetObject", new object());

        _ = _mock.Instance.GetNullableInt().ConfirmEqual(69);
        _ = _mock.Instance.GetString().ConfirmEqual("Hello, World!");
        _ = _mock.Instance.GetObject().ConfirmType(typeof(object));
    }

    [TestCase]
    public void ClearCallRecords_ShouldClearCallRecords()
    {
        _ = _mock.Instance.GetInt();
        _ = _mock.Instance.GetNullableInt();
        _ = _mock.Instance.GetString();
        _ = _mock.Instance.GetObject();

        _ = _mock.GetCallRecords().ConfirmCount(4);
        _mock.ClearCallRecords();
        _ = _mock.GetCallRecords().ConfirmEmpty();
    }

    [TestCase]
    public void VerifyCalled_ReturnsCorrectValue()
    {
        _ = _mock.VerifyCalled("DoSomething", 0).ConfirmTrue();
        _mock.Instance.DoSomething();
        _ = _mock.VerifyCalled("DoSomething", 1).ConfirmTrue();
        _mock.Instance.DoSomething();
        _ = _mock.VerifyCalled("DoSomething", 2).ConfirmTrue();
    }

    [TestCase]
    public void VerifyCalledWith_ReturnsCorrectValue()
    {
        _ = _mock
            .VerifyCalledWith("StringMethod", "Hello, World!")
            .ConfirmFalse();
        _ = _mock.Instance.StringMethod("Hi");
        _ = _mock
            .VerifyCalledWith("StringMethod", "Hello, World!")
            .ConfirmFalse();
        _ = _mock.Instance.StringMethod("Hello, World!");
        _ = _mock
            .VerifyCalledWith("StringMethod", "Hello, World!")
            .ConfirmTrue();
        _ = _mock
            .VerifyCalledWith("StringMethod", "Hi")
            .ConfirmTrue();
    }
}
