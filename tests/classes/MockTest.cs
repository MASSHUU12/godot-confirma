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
        void TestMethod();
        int TestMethodWithReturn();
    }

    // [TestCase]
    // public void ProxyType_ShouldBeCreatedSuccessfully()
    // {
    //     Mock<ITestInterface> mock = new();
    //     Type proxyType = mock.ProxyType;

    //     _ = proxyType.ConfirmNotNull();
    //     _ = typeof(ITestInterface).IsAssignableFrom(proxyType).ConfirmTrue();
    // }

    // [TestCase]
    // public void ProxyType_ShouldImplementAllInterfaceMethods()
    // {
    //     Mock<ITestInterface> mock = new();
    //     Type proxyType = mock.ProxyType;

    //     MethodInfo[] interfaceMethods = typeof(ITestInterface).GetMethods();
    //     MethodInfo[] proxyMethods = proxyType.GetMethods();

    //     foreach (MethodInfo iMethod in interfaceMethods)
    //     {
    //         _ = Confirm.IsTrue(
    //             proxyMethods.Any(
    //                 m => m.Name == iMethod.Name
    //                 && m.ReturnType == iMethod.ReturnType
    //                 && m.GetParameters().Length == iMethod.GetParameters().Length
    //             ),
    //             $"Method {iMethod.Name} is not implemented in proxy type."
    //         );
    //     }
    // }

    [TestCase]
    public void ProxyType_ShouldHaveCorrectMethodReturnTypes()
    {
        Mock<ITestInterface> mock = new();
        Type proxyType = mock.ProxyType;

        MethodInfo? testMethod = proxyType.GetMethod("TestMethodWithReturn");
        Type? returnType = testMethod?.ReturnType;

        _ = typeof(int).ConfirmEqual(returnType);

        mock.SetDefaultReturnValue("TestMethodWithReturn", 5);
        _ = mock.Instance.TestMethodWithReturn().ConfirmEqual(5);
        // _ = mock.Instance.TestMethodWithReturn();
    }
}
