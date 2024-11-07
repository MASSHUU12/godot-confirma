using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[SetUp]
[TestClass]
[Parallelizable]
[Category("MissingLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "MissingLifecycleMethodsTest",
    hideFromResults: true
)]
public static class MissingSetUpTest
{
    [TestCase]
    public static void TestCase() { }
}
