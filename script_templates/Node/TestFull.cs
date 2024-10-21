// meta-description: Template for test class with lifecycle methods defined.

using Confirma.Attributes;
using Confirma.Extensions;

[SetUp]
[AfterAll]
[TearDown]
[BeforeAll]
[TestClass]
[Parallelizable]
public class _CLASS_
{
    [TestCase]
    public void ExampleTest()
    {
        _ = true.ConfirmTrue();
    }

    public void SetUp() { }

    public void AfterAll() { }

    public void TearDown() { }

    public void BeforeAll() { }
}
