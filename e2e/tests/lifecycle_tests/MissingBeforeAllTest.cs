using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[BeforeAll]
[TestClass]
[Parallelizable]
[Category("MissingLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "MissingLifecycleMethodsTest",
    hideFromResults: true
)]
public static class MissingBeforeAllTest
{
    [TestCase]
    public static void TestCase() { }
}
