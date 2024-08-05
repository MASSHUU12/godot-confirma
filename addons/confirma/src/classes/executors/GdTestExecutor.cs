using System.Collections.Generic;
using System.Linq;
using Confirma.Classes.Discovery;
using Confirma.Helpers;
using Confirma.Interfaces;
using Confirma.Types;
using Godot;

using static Confirma.Enums.ETestCaseState;

namespace Confirma.Classes.Executors;

public class GdTestExecutor : ITestExecutor
{
    private TestsProps _props;
    private bool _testFailed;

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
        _props.Autoload!.GdAssertionFailed += OnAssertionFailed;
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<GDScript> testClasses = GdTestDiscovery.GetTestScripts(
            "./gdtests" // TODO
        );

        if (!string.IsNullOrEmpty(_props.ClassName))
        {
            testClasses = testClasses.Where(tc => tc.GetClass() == _props.ClassName);

            if (!testClasses.Any())
            {
                Log.PrintError($"No test class found with the name '{_props.ClassName}'.");
                return -1;
            }
        }

        _props.ResetStats();

        foreach (GDScript testClass in testClasses)
        {
            ExecuteClass(testClass);
        }

        result = _props.Result;
        return testClasses.Count();
    }

    private void ExecuteClass(GDScript testClass)
    {
        string className = testClass.GetGlobalName();

        if (string.IsNullOrEmpty(className))
        {
            className = testClass.ResourcePath.GetFile();
        }

        Log.Print($"> {className}...\n");

        GodotObject instance = testClass.New().AsGodotObject();

        IEnumerable<string> methods = testClass
            .GetScriptMethodList()
            .Select(static method => method["name"].AsString());

        foreach (string method in methods)
        {
            _ = instance.Call(method);

            if (!_testFailed)
            {
                _props.Result.TestsPassed++;
                TestOutput.PrintOutput(
                    "TODO: Name",
                    "TODO: Params",
                    Passed,
                    _props.IsVerbose
                );
            }

            _testFailed = false;
        }

        instance.Dispose();
    }

    private void OnAssertionFailed(string message)
    {
        _props.Result.TestsFailed++;
        TestOutput.PrintOutput(
            "TODO: Name",
            "TODO: Params",
            Failed,
            _props.IsVerbose,
            message
        );
        _testFailed = true;
    }
}
