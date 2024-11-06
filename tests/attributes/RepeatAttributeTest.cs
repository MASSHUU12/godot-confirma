using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Fuzz;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
public class RepeatAttributeTest
{
    private static int _runCount, _fuzzCount;

    [Repeat(5)]
    [TestCase]
    public void Test_RunsMultipleTimes()
    {
        _runCount++;
        _ = _runCount.ConfirmIsPositive().ConfirmLessThanOrEqual(5);
    }

    [Repeat(2)]
    [Fuzz(typeof(int), minValue: 2, maxValue: 2)]
    public void Test_Fuzz_RunsMultipleTimes(int a)
    {
        _fuzzCount += a;
        _ = _fuzzCount.ConfirmIsPositive().ConfirmLessThanOrEqual(4);
    }

    public void AfterAll()
    {
        _ = _runCount.ConfirmEqual(5);
        _runCount = 0;

        _ = _fuzzCount.ConfirmEqual(4);
        _fuzzCount = 0;
    }
}
