using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Formatters;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class AssertionMessageGeneratorTest
{
    [TestCase]
    public void GenerateMessage_ReturnsCorrectMessage()
    {
        _ = new AssertionMessageGenerator(
            "Expected {1} to be less than {2}.",
            "TestAssertion",
            new NumericFormatter(1),
            9.5d,
            5.5d
        )
        .GenerateMessage()
        .ConfirmEqual(
            "Assertion TestAssertion failed: Expected 9.5 to be less than 5.5."
        );
    }
}
