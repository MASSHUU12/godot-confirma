using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Enums;
using Confirma.Extensions;
using Confirma.Types;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class TestResultTest
{
    [TestCase]
    public void Constructor_SetsDefaultValues()
    {
        TestResult testResult = new();

        _ = testResult.TotalTests.ConfirmEqual(0u);
        _ = testResult.TestsPassed.ConfirmEqual(0u);
        _ = testResult.TestsFailed.ConfirmEqual(0u);
        _ = testResult.TestsIgnored.ConfirmEqual(0u);
        _ = testResult.TotalOrphans.ConfirmEqual(0u);
        _ = testResult.TotalTime.ConfirmEqual(0u);
        _ = testResult.Warnings.ConfirmEqual(0u);
        _ = testResult.TestLogs.ConfirmEmpty();
    }

    [TestCase]
    public void OperatorPlus_AddsTestClassResult()
    {
        TestResult testResult = new();
        TestClassResult testClassResult = new(
            TestsPassed: 2,
            TestsFailed: 1,
            TestsIgnored: 0,
            Warnings: 1,
            TestLogs: new List<TestLog> { new(ELogType.Class) }
        );

        testResult += testClassResult;

        _ = testResult.TotalTests.ConfirmEqual(3u);
        _ = testResult.TestsPassed.ConfirmEqual(2u);
        _ = testResult.TestsFailed.ConfirmEqual(1u);
        _ = testResult.TestsIgnored.ConfirmEqual(0u);
        _ = testResult.TotalOrphans.ConfirmEqual(0u);
        _ = testResult.TotalTime.ConfirmEqual(0u);
        _ = testResult.Warnings.ConfirmEqual(1u);
        _ = testResult.TestLogs.ConfirmCount(1);
    }

    [TestCase]
    public void OperatorPlus_AddsTestResult()
    {
        TestResult testResult1 = new()
        {
            TotalTests = 2,
            TestsPassed = 1,
            TestsFailed = 1,
            TestsIgnored = 0,
            TotalOrphans = 1,
            TotalTime = 10,
            Warnings = 1,
            TestLogs = new List<TestLog> { new(ELogType.Class) }
        };
        TestResult testResult2 = new()
        {
            TotalTests = 3,
            TestsPassed = 2,
            TestsFailed = 1,
            TestsIgnored = 0,
            TotalOrphans = 2,
            TotalTime = 20,
            Warnings = 2,
            TestLogs = new List<TestLog> { new(ELogType.Class) }
        };

        testResult1 += testResult2;

        _ = testResult1.TotalTests.ConfirmEqual(5u);
        _ = testResult1.TestsPassed.ConfirmEqual(3u);
        _ = testResult1.TestsFailed.ConfirmEqual(2u);
        _ = testResult1.TestsIgnored.ConfirmEqual(0u);
        _ = testResult1.TotalOrphans.ConfirmEqual(3u);
        _ = testResult1.TotalTime.ConfirmEqual(10u);
        _ = testResult1.Warnings.ConfirmEqual(3u);
        _ = testResult1.TestLogs.ConfirmCount(2);
    }
}
