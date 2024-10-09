using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[TestClass]
[Parallelizable]
[TearDown(nameof(TearDown))]
public class TearDownAttributeTest
{
    private bool _ranTearDown = false;

    [TestCase]
    public void TestCase()
    {
        _ranTearDown = false;
    }

    public void TearDown()
    {
        _ranTearDown = true;
    }

    public void AfterAll()
    {
        _ = _ranTearDown.ConfirmTrue();
        _ranTearDown = false;
    }
}
