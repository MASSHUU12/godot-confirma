using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmEqualTest
{
    #region ConfirmEqual
    [TestCase(1, 1)]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    [TestCase(null, null)]
    [TestCase(2d, 2d)]
    [TestCase(2f, 2f)]
    public static void ConfirmEqual_WhenEqual(object o1, object o2)
    {
        _ = o1.ConfirmEqual(o2);
    }

    [TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hello,", "world!" })]
    public static void ConfirmEqual_WhenArrEqual(object[] o1, object[] o2)
    {
        _ = o1.ConfirmEqual(o2);
    }

    [TestCase(1, 2)]
    [TestCase("Lorem ipsum", "Dolor sit amet")]
    [TestCase(2d, 3d)]
    [TestCase(2f, 2)]
    public static void ConfirmEqual_WhenNotEqual(object o1, object o2)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => o1.ConfirmEqual(o2));
    }

    [TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hi,", "world!" })]
    public static void ConfirmEqual_WhenArrNotEqual(object[] o1, object[] o2)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => o1.ConfirmEqual(o2));
    }

    [TestCase]
    public static void ConfirmEqual_ArrayAsObject_NotEqual_ThrowsExceptionWMessage()
    {
        Action action = static () =>
        {
            object arr1 = new object[] { 0, 1, 2 };
            object arr2 = new object[] { 1, 2, 3 };

            _ = arr1.ConfirmEqual(arr2);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Expected '1, 2, 3' but got '0, 1, 2'."
        );
    }
    #endregion ConfirmEqual

    #region ConfirmNotEqual
    [TestCase(1, 2)]
    [TestCase("Lorem ipsum", "Dolor sit amet")]
    [TestCase(null, 1)]
    [TestCase(2d, 3d)]
    [TestCase(2f, 2)]
    public static void ConfirmNotEqual_WhenNotEqual(object o1, object o2)
    {
        _ = o1.ConfirmNotEqual(o2);
    }

    [TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hi,", "world!" })]
    public static void ConfirmNotEqual_WhenArrNotEqual(object[] o1, object[] o2)
    {
        _ = o1.ConfirmNotEqual(o2);
    }

    [TestCase(1, 1)]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    [TestCase(2d, 2d)]
    [TestCase(2f, 2f)]
    public static void ConfirmNotEqual_WhenEqual(object o1, object o2)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => o1.ConfirmNotEqual(o2));
    }

    [TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hello,", "world!" })]
    public static void ConfirmNotEqual_WhenArrEqual(object[] o1, object[] o2)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => o1.ConfirmNotEqual(o2));
    }

    [TestCase]
    public static void ConfirmNotEqual_ArrayAsObject_Equal_ThrowsExceptionWMessage()
    {
        Action action = static () =>
        {
            object arr1 = new object[] { 0, 1, 2 };
            object arr2 = new object[] { 0, 1, 2 };

            _ = arr1.ConfirmNotEqual(arr2);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Expected not '0, 1, 2' but got '0, 1, 2'."
        );
    }
    #endregion ConfirmNotEqual
}
