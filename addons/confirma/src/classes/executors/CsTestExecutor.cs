using Confirma.Types;
using Confirma.Helpers;
using Confirma.Attributes;
using System.Linq;
using System.Reflection;
using Confirma.Classes.Discovery;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Confirma.Interfaces;

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

        if (!string.IsNullOrEmpty(_props.ClassName))
        {
            testClasses = testClasses.Where(tc => tc.Type.Name == _props.ClassName);

            if (!testClasses.Any())
            {
                Log.PrintError($"No test class found with the name '{_props.ClassName}'.");
                return -1;
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

        _props.Result.TotalOrphans += (uint)Godot.Performance.GetMonitor(
            Godot.Performance.Monitor.ObjectOrphanNodeCount
        );

        result = _props.Result;
        return testClasses.Count();
    }

    private void ExecuteClass(TestingClass testClass, TestResult result)
    {
        Log.Print($"> {testClass.Type.Name}...");

        IgnoreAttribute? attr = testClass.Type.GetCustomAttribute<IgnoreAttribute>();
        if (attr?.IsIgnored() == true)
        {
            _props.Result.TestsIgnored += (uint)testClass.TestMethods.Sum(
                static m => m.TestCases.Count()
            );

            Log.PrintWarning(" ignored.\n");

            if (string.IsNullOrEmpty(attr.Reason))
            {
                return;
            }

            Log.PrintWarning($"- {attr.Reason}\n");
        }

        Log.PrintLine();

        TestClassResult classResult = testClass.Run(_props);

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
