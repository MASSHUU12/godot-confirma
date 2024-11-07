using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
public class SingleRunTest
{
    private int _runCount;

    [TestCase]
    public void ShouldRunOnce()
    {
        _runCount++;
        _ = _runCount.ConfirmEqual(1);
    }

    public void AfterAll()
    {
        _ = _runCount.ConfirmEqual(1);
        _runCount = 0;
    }
}
