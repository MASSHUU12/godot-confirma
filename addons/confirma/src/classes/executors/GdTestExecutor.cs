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
    private readonly List<TestLog> _testLogs;
    private ScriptMethodInfo? _currentMethod;

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
        _props.Autoload!.GdAssertionFailed += OnAssertionFailed;
        _testLogs = new();
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<ScriptInfo> testClasses = GdTestDiscovery.GetTestScripts(
            _props.GdTestPath
        );

        if (!string.IsNullOrEmpty(_props.Target.Name))
        {
            testClasses = testClasses.Where(
                tc => tc.Script.GetClass() == _props.Target.Name
            );

            if (!testClasses.Any())
            {
                Log.PrintError(
                    $"No test class found with the name '{_props.Target.Name}'."
                );
                return -1;
            }
        }

        _props.ResetStats();

        foreach (ScriptInfo testClass in testClasses)
        {
            ExecuteClass(testClass);
        }

        result = _props.Result;
        result.TestLogs.AddRange(_testLogs);
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

        _testLogs.Clear();
        _testLogs.Add(new(ELogType.Class, className));
        _testLogs.Add(new(ELogType.Newline));

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

            _testLogs.Add(GetTestResult(Passed));
        }

        instance.Dispose();
    }

    private void OnAssertionFailed(string message)
    {
        _props.Result.TestsFailed++;
        _testFailed = true;

        _testLogs!.Add(GetTestResult(Failed, message));
    }

    private TestLog GetTestResult(ETestCaseState state, string? message = null)
    {
        return new(
            ELogType.Method,
            _currentMethod!.Name,
            state,
            ArrayHelper.ToString(
                _currentMethod.Args.Select(static a => a.Name).ToArray()
            ),
            message
        );
    }
}
