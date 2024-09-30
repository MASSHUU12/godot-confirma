using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
[BeforeAll(nameof(BeforeAll))]
public static class BeforeAllAttributeTest
{
    private static bool _ranBeforeAll = false;

    public static void BeforeAll()
    {
        _ranBeforeAll = true;
        _ = _ranBeforeAll.ConfirmTrue();
        _ranBeforeAll = false;
    }
}
