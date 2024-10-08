using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class TypeHelperTest
{
    private interface ITestInterface { }

    private class ImplementingClass : ITestInterface { }

    private class NotImplementingClass { }

    private interface ITestGenericInterface<T> { }

    private class ImplementingGenericClass<T> : ITestGenericInterface<T> { }

    private class NotImplementingGenericClass<T> { }

    #region ImplementsAny
    [TestCase]
    public static void ImplementsAny_InterfaceImplemented_ReturnsTrue()
    {
        _ = typeof(ImplementingClass)
            .ImplementsAny<ITestInterface>()
            .ConfirmTrue();

        _ = typeof(ImplementingClass)
            .ImplementsAny(typeof(ITestInterface))
            .ConfirmTrue();
    }

    [TestCase]
    public static void ImplementsAny_InterfaceNotImplemented_ReturnsFalse()
    {
        _ = typeof(NotImplementingClass)
            .ImplementsAny<ITestInterface>()
            .ConfirmFalse();

        _ = typeof(NotImplementingClass)
            .ImplementsAny(typeof(ITestInterface))
            .ConfirmFalse();
    }

    [TestCase]
    public static void ImplementsAny_GenericInterfaceImplemented_ReturnsTrue()
    {
        _ = typeof(ImplementingGenericClass<>)
            .ImplementsAny(typeof(ITestGenericInterface<>))
            .ConfirmTrue();
    }

    [TestCase]
    public static void ImplementsAny_GenericInterfaceNotImplemented_ReturnsFalse()
    {
        _ = typeof(NotImplementingGenericClass<>)
            .ImplementsAny(typeof(ITestGenericInterface<>))
            .ConfirmFalse();
    }

    [TestCase]
    public static void ImplementsAny_TypeIsInterface_ReturnsFalse()
    {
        _ = typeof(IDisposable)
            .ImplementsAny<ITestInterface>()
            .ConfirmFalse();

        _ = typeof(IDisposable)
            .ImplementsAny(typeof(ITestInterface))
            .ConfirmFalse();
    }
    #endregion ImplementsAny

    #region IsCollection
    [TestCase]
    public static void IsCollection_ReturnsTrue_ForICollection()
    {
        _ = typeof(ICollection<int>).IsCollection().ConfirmTrue();
    }

    [TestCase]
    public static void IsCollection_ReturnsTrue_ForIEnumerable()
    {
        _ = typeof(IEnumerable<int>).IsCollection().ConfirmTrue();
    }

    [TestCase]
    public static void IsCollection_ReturnsTrue_ForConcreteCollectionType()
    {
        _ = typeof(List<int>).IsCollection().ConfirmTrue();
    }

    [TestCase]
    public static void IsCollection_ReturnsFalse_ForNonCollectionType()
    {
        _ = typeof(int).IsCollection().ConfirmFalse();
    }
    #endregion IsCollection

    #region IsStatic
    [TestCase]
    public static void IsStatic_ReturnsTrue_ForStaticClass()
    {
        _ = typeof(TypeHelperTest).IsStatic().ConfirmTrue();
    }

    [TestCase]
    public static void IsStatic_ReturnsFalse_ForRegularClass()
    {
        _ = typeof(ImplementingClass).IsStatic().ConfirmFalse();
    }
    #endregion IsStatic
}
