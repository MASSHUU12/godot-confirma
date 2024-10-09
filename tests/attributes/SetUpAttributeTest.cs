using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[BeforeAll]
[TestClass]
[Parallelizable]
[SetUp(nameof(SetUp))]
public class SetUpAttributeTest
{
    private bool _ranSetUp = false;

    public void BeforeAll()
    {
        _ranSetUp = false;
    }

    [TestCase]
    public void TestCase() { }

    public void SetUp()
    {
        _ranSetUp = true;
    }

    public void AfterAll()
    {
        _ = _ranSetUp.ConfirmTrue();
        _ranSetUp = false;
    }
}
