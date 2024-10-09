using Confirma.Attributes;
using Godot;
using static Confirma.Enums.EIgnoreMode;

[TestClass]
[Parallelizable]
[Category("OrphansMonitorTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "OrphansMonitorTest",
    hideFromResults: true
)]
public static class OrphansMonitorTest
{
    [TestCase]
    public static void TestCase()
    {
        new Node();
        new Node();
        new Node();
        new Node();
        new Node();
    }
}
