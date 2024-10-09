using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes.Discovery;
using Confirma.Enums;
using Confirma.Helpers;
using Confirma.Interfaces;
using Confirma.Types;

namespace Confirma.Classes.Executors;

public class CsTestExecutor : ITestExecutor
{
    private TestsProps _props;

    public CsTestExecutor(TestsProps props)
    {
        _props = props;
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<TestingClass> testClasses = CsTestDiscovery.DiscoverTestClasses(
            Assembly.GetExecutingAssembly()
        );

        if (!string.IsNullOrEmpty(_props.Target.Name))
        {
            if (_props.Target.Target is ERunTargetType.Class or ERunTargetType.Method)
            {
                testClasses = testClasses.Where(tc => tc.Type.Name == _props.Target.Name);

                if (!testClasses.Any())
                {
                    Log.PrintError($"No test class found with the name '{_props.Target.Name}'.\n");
                    return -1;
                }
            }

            if (_props.Target.Target == ERunTargetType.Category)
            {
                testClasses = testClasses.Where(
                    tc => tc.Type.GetCustomAttributes<CategoryAttribute>().Count(
                        c => c.Category == _props.Target.Name
                    ) == 1
                );

                if (!testClasses.Any())
                {
                    Log.PrintError(
                        $"No test classes found with category '{_props.Target.Name}'.\n"
                    );
                    return -1;
                }
            }
        }

        _props.ResetStats();

        if (_props.DisableParallelization)
        {
            foreach (TestingClass testClass in testClasses)
            {
                ExecuteClass(testClass, _props.Result);
            }
        }
        else
        {
            var (parallelTestClasses, sequentialTestClasses) = ClassifyTests(testClasses);
            ConcurrentBag<TestResult> results = new();

            parallelTestClasses.AsParallel().ForAll(testClass =>
            {
                TestResult localResult = new();
                ExecuteClass(testClass, localResult);
                results.Add(localResult);
            });

            foreach (TestingClass testClass in sequentialTestClasses)
            {
                ExecuteClass(testClass, _props.Result);
            }

            // Aggregate results
            foreach (TestResult res in results)
            {
                _props.Result += res;
            }
        }

        result = _props.Result;
        return testClasses.Count();
    }

    private void ExecuteClass(TestingClass testClass, TestResult result)
    {
        List<TestLog> testLogs = new()
        {
            new(ELogType.Class, ELangType.CSharp, testClass.Type.Name)
        };

        IgnoreAttribute? attr = testClass.Type.GetCustomAttribute<IgnoreAttribute>();

        if (attr?.IsIgnored(_props.Target) == true)
        {
            if (attr.HideFromResults == true)
            {
                return;
            }

            _props.Result.TestsIgnored += (uint)testClass.TestMethods.Sum(
                static m => m.TestCases.Count()
            );

            testLogs.Add(new(ELogType.Warning, ELangType.CSharp, "  ignored.\n"));

            if (string.IsNullOrEmpty(attr.Reason))
            {
                return;
            }

            testLogs.Add(new(ELogType.Warning, ELangType.CSharp, $"- {attr.Reason}\n"));
        }

        testLogs.Add(new(ELogType.Newline));

        TestClassResult classResult = testClass.Run(_props);

        classResult.TestLogs.InsertRange(0, testLogs);
        result += classResult;
    }

    private static (IEnumerable<TestingClass>, IEnumerable<TestingClass>)
    ClassifyTests(IEnumerable<TestingClass> tests)
    {
        return (
          CsTestDiscovery.GetParallelTestClasses(tests),
          CsTestDiscovery.GetSequentialTestClasses(tests)
        );
    }
}
