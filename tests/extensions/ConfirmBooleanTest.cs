using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmBooleanTest
{
    [TestCase]
    public static void ConfirmTrue_WhenTrue()
    {
        _ = true.ConfirmTrue();
    }

    [TestCase]
    public static void ConfirmTrue_WhenFalse()
    {
        _ = Confirm.Throws<ConfirmAssertException>(
            static () => false.ConfirmTrue()
        );
    }

    [TestCase]
    public static void ConfirmFalse_WhenFalse()
    {
        _ = false.ConfirmFalse();
    }

    [TestCase]
    public static void ConfirmFalse_WhenTrue()
    {
        _ = Confirm.Throws<ConfirmAssertException>(() => true.ConfirmFalse());
    }
}
