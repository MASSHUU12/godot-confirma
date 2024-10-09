using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
[BeforeAll(nameof(BeforeAll))]
public class BeforeAllAttributeTest
{
    private bool _ranBeforeAll = false;

    public void BeforeAll()
    {
        _ranBeforeAll = true;
        _ = _ranBeforeAll.ConfirmTrue();
        _ranBeforeAll = false;
    }
}
