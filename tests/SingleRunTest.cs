using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
public static class SingleRunTest
{
    private static int _runCount;

    [TestCase]
    public static void ShouldRunOnce()
    {
        _runCount++;
        _ = _runCount.ConfirmEqual(1);
    }

    public static void AfterAll()
    {
        _ = _runCount.ConfirmEqual(1);
        _runCount = 0;
    }
}
