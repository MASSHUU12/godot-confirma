using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
[AfterAll(nameof(AfterAll))]
public class AfterAllAttributeTest
{
    private bool _ranAfterAll = false;

    public void AfterAll()
    {
        _ranAfterAll = true;
        _ = _ranAfterAll.ConfirmTrue();
        _ranAfterAll = false;
    }
}
