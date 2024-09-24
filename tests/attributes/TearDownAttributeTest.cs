using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
[TearDown(nameof(TearDown))]
public static class TearDownAttributeTest
{
    private static bool _ranTearDown = false;

    [TestCase]
    public static void TestCase()
    {
        _ranTearDown = false;
    }

    public static void TearDown()
    {
        _ranTearDown = true;
    }

    public static void AfterAll()
    {
        _ = _ranTearDown.ConfirmTrue();
        _ranTearDown = false;
    }
}
