using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[TearDown]
[TestClass]
[Parallelizable]
[Category("MissingLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "MissingLifecycleMethodsTest",
    hideFromResults: true
)]
public static class MissingTearDownTest
{
    [TestCase]
    public static void TestCase() { }
}
