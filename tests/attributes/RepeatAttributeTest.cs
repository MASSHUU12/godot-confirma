using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
public static class RepeatAttributeTest
{
    private static int _runCount;

    [Repeat(5)]
    [TestCase]
    public static void TestMethod_RunsMultipleTimes()
    {
        _runCount++;
        _ = _runCount.ConfirmIsPositive().ConfirmLessThanOrEqual(5);
    }

    public static void AfterAll()
    {
        _ = _runCount.ConfirmEqual(5);
        _runCount = 0;
    }
}
