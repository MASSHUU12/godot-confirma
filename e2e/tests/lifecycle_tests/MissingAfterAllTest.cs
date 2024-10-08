using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[AfterAll]
[TestClass]
[Parallelizable]
[Category("MissingLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "MissingLifecycleMethodsTest",
    hideFromResults: true
)]
public static class MissingAfterAllTest
{
    [TestCase]
    public static void TestCase() { }
}
