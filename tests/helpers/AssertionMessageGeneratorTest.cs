using System.Collections.Generic;
using System.Globalization;
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
    [TestCase(
        "Test Assertion",
        "Expected Value",
        "Actual Value",
        "Assertion Test Assertion failed: Expected \"Expected Value\" but was \"Actual Value\"."
    )]
    [TestCase(
        "Test Assertion",
        10,
        20,
        "Assertion Test Assertion failed: Expected 10 but was 20."
    )]
    [TestCase(
        "Test Assertion",
        10.5d,
        20.5d,
        "Assertion Test Assertion failed: Expected 10.5 but was 20.5."
    )]
    public static void GenerateAssertionMessage_ReturnsCorrectMessage(
        string assertionName,
        object expectedValue,
        object actualValue,
        string expectedMessage
    )
    {
        _ = AssertionMessageGenerator.GenerateAssertionMessage(assertionName, expectedValue, actualValue)
            .ConfirmEqual(expectedMessage);
    }

    [TestCase]
    public static void GenerateAssertionMessage_CollectionExpectedAndActual_FormatsCorrectly()
    {
        const string assertion = "Test Assertion";
        List<int> expected = new() { 1, 2, 3 };
        List<int> actual = new() { 4, 5, 6 };
        string result = AssertionMessageGenerator.GenerateAssertionMessage(assertion, expected, actual);

        _ = result.ConfirmEqual(
            string.Format(
                CultureInfo.InvariantCulture,
                "Assertion {0} failed: Expected [{1}] but was [{2}].",
                assertion,
                string.Join(", ", expected),
                string.Join(", ", actual)
            )
        );
    }

    [TestCase]
    public static void GenerateAssertionMessage_MultiDimensionalArrayExpectedAndActual_FormatsCorrectly()
    {
        const string assertion = "Test Assertion";
        int[,] expected = { { 1, 2 }, { 3, 4 } };
        int[,] actual = { { 5, 6 }, { 7, 8 } };
        string result = AssertionMessageGenerator.GenerateAssertionMessage(assertion, expected, actual);

        _ = result.ConfirmEqual(
            string.Format(
                CultureInfo.InvariantCulture,
                "Assertion {0} failed: Expected {1} but was {2}.",
                assertion,
                "[[1, 2], [3, 4]]",
                "[[5, 6], [7, 8]]"
            )
        );
    }

    [TestCase]
    public static void GenerateAssertionMessage_DefaultFormatterUsedForUnknownType_FormatsCorrectly()
    {
        const string assertion = "Test Assertion";
        object expected = new();
        object actual = new();
        string result = AssertionMessageGenerator.GenerateAssertionMessage(assertion, expected, actual);

        _ = result.ConfirmEqual(
            string.Format(
                CultureInfo.InvariantCulture,
                "Assertion {0} failed: Expected {1} but was {2}.",
                assertion,
                expected,
                actual
            )
        );
    }
}
