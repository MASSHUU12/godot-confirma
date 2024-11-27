using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmEqualTest
{
    #region ConfirmEqual
    [TestCase(1, 1)]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    [TestCase(null, null)]
    [TestCase(2d, 2d)]
    [TestCase(2f, 2f)]
    public void ConfirmEqual_WhenEqual(object o1, object o2)
    {
        _ = o1.ConfirmEqual(o2);
    }

    [TestCase(
        new string[] { "Hello,", "world!" },
        new string[] { "Hello,", "world!" })
    ]
    public void ConfirmEqual_WhenArrEqual(object[] o1, object[] o2)
    {
        _ = o1.ConfirmEqual(o2);
    }

    [TestCase(1, "1", 2, "2")]
    [TestCase(
        "Lorem ipsum",
        "\"Lorem ipsum\"",
        "Dolor sit amet",
        "\"Dolor sit amet\""
    )]
    [TestCase(2d, "2.00000", 3d, "3.00000")]
    [TestCase(2f, "2.00000", 2, "2")]
    public void ConfirmEqual_WhenNotEqual(
        object o1,
        string fo1,
        object o2,
        string fo2
    )
    {
        Action action = () => o1.ConfirmEqual(o2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqual failed: "
            + $"Expected {fo2}, but got {fo1}."
        );
    }

    [TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hi,", "world!" })]
    public void ConfirmEqual_WhenArrNotEqual(object[] o1, object[] o2)
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => o1.ConfirmEqual(o2));
    }

    [TestCase]
    public void ConfirmEqual_Null_ThrowsExceptionWMessage()
    {
        Action action = static () => "".ConfirmEqual(null);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqual failed: Expected null, but got \"\"."
        );
    }

    [TestCase]
    public void ConfirmEqual_Array_ReturnsActualArray()
    {
        Array actual = new int[] { 1, 2, 3 };
        Array expected = new int[] { 1, 2, 3 };

        _ = actual.ConfirmEqual(expected);
    }

    [TestCase]
    public void ConfirmEqual_Array_DifferentLengths_ThrowsConfirmAssertException()
    {
        Array actual = new int[] { 1, 2, 3 };
        Array expected = new int[] { 1, 2 };

        Action action = () => actual.ConfirmEqual(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqual failed: "
            + "Expected array of length 2, but got array of length 3."
        );
    }

    [TestCase]
    public void ConfirmEqual_Array_DifferentValue_ThrowsConfirmAssertException()
    {
        Array actual = new int[] { 1, 2, 3 };
        Array expected = new int[] { 1, 2, 4 };

        Action action = () => actual.ConfirmEqual(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqual failed: "
            + "Arrays differ at index 2. Expected 4, but got 3."
        );
    }

    [TestCase]
    public void ConfirmEqual_Array_DifferentTypes_ThrowsConfirmAssertException()
    {
        Array actual = new int[] { 1, 2, 3 };
        Array expected = new double[] { 1, 2, 3 };

        Action action = () => actual.ConfirmEqual(expected);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqual failed: "
            + "Arrays differ at index 0. Expected 1.00000, but got 1."
        );
    }
    #endregion ConfirmEqual

    #region ConfirmNotEqual
    [TestCase(1, 2)]
    [TestCase("Lorem ipsum", "Dolor sit amet")]
    [TestCase(null, 1)]
    [TestCase(2d, 3d)]
    [TestCase(2f, 2)]
    public void ConfirmNotEqual_WhenNotEqual(object o1, object o2)
    {
        _ = o1.ConfirmNotEqual(o2);
    }

    [TestCase(
        new string[] { "Hello,", "world!" },
        new string[] { "Hi,", "world!" }
    )]
    public void ConfirmNotEqual_WhenArrNotEqual(object[] o1, object[] o2)
    {
        _ = o1.ConfirmNotEqual(o2);
    }

    [TestCase(1, 1, "1")]
    [TestCase(
        "Lorem ipsum",
        "Lorem ipsum",
        "\"Lorem ipsum\""
    )]
    [TestCase(3d, 3d, "3.00000")]
    [TestCase(2f, 2f, "2.00000")]
    public void ConfirmNotEqual_WhenEqual(
        object o1,
        object o2,
        string formatted
    )
    {
        Action action = () => o1.ConfirmNotEqual(o2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEqual failed: "
            + $"Expected not {formatted}."
        );
    }

    [TestCase]
    public void ConfirmNotEqual_WhenArrEqual()
    {
        Action action = static () =>
        {
            object arr1 = new string[] { "Hello,", "world!" };
            object arr2 = new string[] { "Hello,", "world!" };

            _ = arr1.ConfirmNotEqual(arr2);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEqual failed: "
            + "Expected not String[\"Hello,\", \"world!\"]."
        );
    }

    [TestCase]
    public void ConfirmNotEqual_ArrayAsObject_Equal_ThrowsExceptionWMessage()
    {
        Action action = static () =>
        {
            object arr1 = new object[] { 0, 1, 2 };
            object arr2 = new object[] { 0, 1, 2 };

            _ = arr1.ConfirmNotEqual(arr2);
        };

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEqual failed: Expected not Object[0, 1, 2]."
        );
    }
    #endregion ConfirmNotEqual
}
