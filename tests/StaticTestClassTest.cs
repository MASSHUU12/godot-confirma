using Confirma.Attributes;

namespace Confirma.Tests;

[BeforeAll]
[TestClass]
[Parallelizable]
public static class StaticTestClassTest
{
    public static void BeforeAll() { }

    [TestCase]
    public static void TestMethod() { }
}
