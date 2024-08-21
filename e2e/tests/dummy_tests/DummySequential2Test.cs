using Confirma.Extensions;
using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[TestClass]
[Parallelizable]
[Category("DummySequentialTests")]
[Ignore(
    WhenNotRunningCategory,
    category: "DummySequentialTests",
    hideFromResults: true
)]
public static class DummySequential2Test
{
    [TestCase]
    public static void SuperTest()
    {
        _ = true.ConfirmTrue();
    }

    [TestCase]
    public static void SuperExtraTest()
    {
        _ = true.ConfirmTrue();
    }

    [TestCase]
    public static void SuperExtraProTest()
    {
        _ = true.ConfirmTrue();
    }
}
