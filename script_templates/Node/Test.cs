// meta-description: Base template for test class.

using Confirma.Attributes;
using Confirma.Extensions;

[TestClass]
[Parallelizable]
public class _CLASS_
{
    [TestCase]
    public void ExampleTest()
    {
        _ = true.ConfirmTrue();
    }
}
