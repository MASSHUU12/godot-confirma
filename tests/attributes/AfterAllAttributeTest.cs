using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
[AfterAll(nameof(AfterAll))]
public static class AfterAllAttributeTest
{
    private static bool _ranAfterAll = false;

    public static void AfterAll()
    {
        _ranAfterAll = true;
        _ = _ranAfterAll.ConfirmTrue();
        _ranAfterAll = false;
    }
}
