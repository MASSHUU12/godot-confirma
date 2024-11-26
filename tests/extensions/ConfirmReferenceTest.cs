using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmReferenceTest
{
    #region ConfirmSameReference
    [TestCase(5)]
    [TestCase(new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum dolor sit amet")]
    public void ConfirmSameReference_WhenSameReference(object obj)
    {
        _ = obj.ConfirmSameReference(obj);
    }

    [TestCase(5, "5", 15, "15")]
    [TestCase(
        new byte[] { 1, 2, 3 },
        "Byte[1, 2, 3]",
        new byte[] { 1, 2, 3 },
        "Byte[1, 2, 3]"
    )]
    [TestCase(
        "Lorem ipsum",
        "\"Lorem ipsum\"",
        "Lorem ipsum",
        "\"Lorem ipsum\""
    )]
    public void ConfirmSameReference_WhenDifferentReferences(
        object obj,
        string fobj1,
        object obj2,
        string fobj2
    )
    {
        Action action = () => obj.ConfirmSameReference(obj2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmSameReference failed: "
            + $"Expected {fobj1} and {fobj2} to be of the same reference."
        );
    }
    #endregion ConfirmSameReference

    #region ConfirmDifferentReference
    [TestCase(5, 15)]
    [TestCase(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    public void ConfirmDifferentReference_WhenDifferentReferences(
        object obj,
        object obj2
    )
    {
        _ = obj.ConfirmDifferentReference(obj2);
    }

    [TestCase(5, "5")]
    [TestCase(new byte[] { 1, 2, 3 }, "Byte[1, 2, 3]")]
    [TestCase(
        "Lorem ipsum dolor sit amet",
        "\"Lorem ipsum dolor sit amet\""
    )]
    public void ConfirmDifferentReference_WhenSameReference(
        object obj,
        string fobj
    )
    {
        Action action = () => obj.ConfirmDifferentReference(obj);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmDifferentReference failed: "
            + $"Expected {fobj} and {fobj} to be of different references."
        );
    }
    #endregion ConfirmDifferentReference
}
