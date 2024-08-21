using Confirma.Extensions;
using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[TestClass]
[Parallelizable]
[Category("DummyTests")]
[Ignore(WhenNotRunningCategory, category: "DummyTests", hideFromResults: true)]
public static class DummySuccessfulTest
{
    [TestCase]
    public static void SuperTest()
    {
        _ = true.ConfirmTrue();
    }
}
