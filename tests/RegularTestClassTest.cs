using Confirma.Attributes;

namespace Confirma.Tests;

[BeforeAll]
[TestClass]
[Parallelizable]
public class RegularTestClassTest
{
    public void BeforeAll() { }

    [TestCase]
    public void TestMethod() { }
}
