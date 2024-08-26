using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class AssertionMessageGeneratorTest
{
    [TestCase(
        "ConfirmTrue",
        true,
        false,
        "Assertion ConfirmTrue failed: Expected True but was False."
    )]
    public static void GenerateAssertionMessage_ReturnsCorrectMessage(
        string assertionName,
        object expectedValue,
        object actualValue,
        string expectedMessage
    )
    {
        _ = new AssertionMessageGenerator()
            .GenerateAssertionMessage(assertionName, expectedValue, actualValue)
            .ConfirmEqual(expectedMessage);
    }
}
