using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Enums;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class TestMethodResultTest
{
    [TestCase]
    public static void Constructor_WithParameters_SetsPropertiesCorrectly()
    {
        const uint passed = 1;
        const uint failed = 2;
        const uint ignored = 3;
        const uint warnings = 4;
        List<TestLog> logs = new() { new TestLog(ELogType.Info) };

        TestMethodResult result = new(passed, failed, ignored, warnings, logs);

        _ = result.TestsPassed.ConfirmEqual(passed);
        _ = result.TestsFailed.ConfirmEqual(failed);
        _ = result.TestsIgnored.ConfirmEqual(ignored);
        _ = result.Warnings.ConfirmEqual(warnings);
        _ = result.TestLogs.ConfirmEqual(logs);
    }

    [TestCase]
    public static void Constructor_WithoutParameters_SetsPropertiesToDefaultValues()
    {
        TestMethodResult result = new();

        _ = result.TestsPassed.ConfirmEqual(0u);
        _ = result.TestsFailed.ConfirmEqual(0u);
        _ = result.TestsIgnored.ConfirmEqual(0u);
        _ = result.Warnings.ConfirmEqual(0u);
        _ = result.TestLogs.ConfirmEmpty();
    }

    [TestCase]
    public static void Reset_ResetsPropertiesToDefaultValues()
    {
        TestMethodResult result = new()
        {
            TestsPassed = 1,
            TestsFailed = 2,
            TestsIgnored = 3,
            Warnings = 4,
            TestLogs = new List<TestLog> { new(ELogType.Info) }
        };

        result.Reset();

        _ = result.TestsPassed.ConfirmEqual(0u);
        _ = result.TestsFailed.ConfirmEqual(0u);
        _ = result.TestsIgnored.ConfirmEqual(0u);
        _ = result.Warnings.ConfirmEqual(0u);
        _ = result.TestLogs.ConfirmEmpty();
    }
}
