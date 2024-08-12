using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmReferenceTest
{
    #region ConfirmSameReference
    [TestCase(5)]
    [TestCase(new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum dolor sit amet")]
    public static void ConfirmSameReference_WhenSameReference(object obj)
    {
        _ = obj.ConfirmSameReference(obj);
    }

    [TestCase(5, 15)]
    [TestCase(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    public static void ConfirmSameReference_WhenDifferentReferences(
        object obj,
        object obj2
    )
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => obj.ConfirmSameReference(obj2)
        );
    }
    #endregion ConfirmSameReference

    #region ConfirmDifferentReference
    [TestCase(5, 15)]
    [TestCase(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum", "Lorem ipsum")]
    public static void ConfirmDifferentReference_WhenDifferentReferences(
        object obj,
        object obj2
    )
    {
        _ = obj.ConfirmDifferentReference(obj2);
    }

    [TestCase(5)]
    [TestCase(new byte[] { 1, 2, 3 })]
    [TestCase("Lorem ipsum dolor sit amet")]
    public static void ConfirmDifferentReference_WhenSameReference(object obj)
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            () => obj.ConfirmDifferentReference(obj)
        );
    }
    #endregion ConfirmDifferentReference
}
