using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[AfterAll]
[BeforeAll]
[TestClass]
[Parallelizable]
[SetUp(nameof(SetUp))]
public static class SetUpAttributeTest
{
    private static bool _ranSetUp = false;

    public static void BeforeAll()
    {
        _ranSetUp = false;
    }

    [TestCase]
    public static void TestCase() { }

    public static void SetUp()
    {
        _ranSetUp = true;
    }

    public static void AfterAll()
    {
        _ = _ranSetUp.ConfirmTrue();
        _ranSetUp = false;
    }
}
