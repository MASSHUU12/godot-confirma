using System.Collections.Generic;
using System.Linq;
using Confirma.Classes.Discovery;
using Confirma.Enums;
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
    private ScriptMethodInfo? _currentMethod;

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
        _props.Autoload!.GdAssertionFailed += OnAssertionFailed;
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<ScriptInfo> testClasses = GdTestDiscovery.GetTestScripts(
            _props.GdTestPath
        );

        if (!string.IsNullOrEmpty(_props.ClassName))
        {
            testClasses = testClasses.Where(tc => tc.Script.GetClass() == _props.ClassName);

            if (!testClasses.Any())
            {
                Log.PrintError($"No test class found with the name '{_props.ClassName}'.");
                return -1;
            }
        }

        _props.ResetStats();

        foreach (ScriptInfo testClass in testClasses)
        {
            ExecuteClass(testClass);
        }

        result = _props.Result;
        return testClasses.Count();
    }

    private void ExecuteClass(ScriptInfo testClass)
    {
        GDScript script = (GDScript)testClass.Script;
        string className = script.GetGlobalName();

        if (string.IsNullOrEmpty(className))
        {
            className = script.ResourcePath.GetFile();
        }

        Log.Print($"> {className}...\n");

        GodotObject instance = script.New().AsGodotObject();

        foreach (ScriptMethodInfo method in testClass.Methods)
        {
            _currentMethod = method;

            _ = instance.Call(method.Name);

            if (_testFailed)
            {
                continue;
            }

            _props.Result.TestsPassed++;
            _testFailed = false;

            PrintTestResult(Passed);
        }

        instance.Dispose();
    }

    private void OnAssertionFailed(string message)
    {
        _props.Result.TestsFailed++;
        _testFailed = true;

        PrintTestResult(Failed, message);
    }

    private void PrintTestResult(ETestCaseState state, string? message = null)
    {
        TestOutput.PrintOutput(
            _currentMethod!.Name,
            ArrayHelper.ToString(
                _currentMethod.Args.Select(static a => a.Name).ToArray()
            ),
            state,
            _props.IsVerbose,
            message
        );
    }
}
