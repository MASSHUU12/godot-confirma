using System;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Classes.Mock;
using Confirma.Extensions;

namespace Confirma.Tests;

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
    }

    [TestCase]
    public void ProxyType_ShouldBeCreatedSuccessfully()
    {
        Mock<ITestInterface> mock = new();
        Type proxyType = mock.ProxyType;

        _ = proxyType.ConfirmNotNull();
        _ = typeof(ITestInterface).IsAssignableFrom(proxyType).ConfirmTrue();
    }

    [TestCase]
    public void ProxyType_ShouldImplementAllInterfaceMethods()
    {
        Mock<ITestInterface> mock = new();
        Type proxyType = mock.ProxyType;

        MethodInfo[] interfaceMethods = typeof(ITestInterface).GetMethods();
        MethodInfo[] proxyMethods = proxyType.GetMethods();

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
        Mock<ITestInterface> mock = new();
        Type proxyType = mock.ProxyType;

        MethodInfo? testMethod = proxyType.GetMethod("GetInt");
        Type? returnType = testMethod?.ReturnType;

        _ = typeof(int).ConfirmEqual(returnType);
    }

    [TestCase]
    public void ProxyType_ShouldCorrectlyRunMethods()
    {
        Mock<ITestInterface> mock = new();

        mock.SetDefaultReturnValue("GetInt", 42);
        mock.SetDefaultReturnValue("GetNullableInt", (int?)null);
        mock.SetDefaultReturnValue("GetString", (string?)null);
        mock.SetDefaultReturnValue("GetObject", (object?)null);

        _ = mock.Instance.GetInt().ConfirmEqual(42);
        _ = mock.Instance.GetNullableInt().ConfirmNull();
        _ = mock.Instance.GetString().ConfirmNull();
        _ = mock.Instance.GetObject().ConfirmNull();

        mock.SetDefaultReturnValue("GetNullableInt", 69);
        mock.SetDefaultReturnValue("GetString", "Hello, World!");
        mock.SetDefaultReturnValue("GetObject", new object());

        _ = mock.Instance.GetNullableInt().ConfirmEqual(69);
        _ = mock.Instance.GetString().ConfirmEqual("Hello, World!");
        _ = mock.Instance.GetObject().ConfirmType(typeof(object));
    }
}
