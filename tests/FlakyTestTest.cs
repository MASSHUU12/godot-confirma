using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
public class FlakyTestTest
{
    private static int _count;

    [Repeat(2, IsFlaky = true)]
    [TestCase]
    public void FailFirst_PassOnRetry()
    {
        if (_count == 0)
        {
            _count++;
            throw new NotSupportedException();
        }

        _count++;
    }

    public void AfterAll()
    {
        _ = _count.ConfirmEqual(3);
    }
}
