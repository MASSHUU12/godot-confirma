using System.Collections.Generic;
using System.Linq;
using Confirma.Classes.Discovery;
using Confirma.Enums;
using Confirma.Helpers;
using Confirma.Interfaces;
using Confirma.Types;
using Confirma.Wrappers;
using Godot;

using static Confirma.Enums.ETestCaseState;

namespace Confirma.Classes.Executors;

public class GdTestExecutor : ITestExecutor
{
    private TestsProps _props;
    private bool _testFailed;
    private readonly List<TestLog> _testLogs;
    private ScriptMethodInfo? _currentMethod;
    private static readonly string[] _lifecycleMethodNames = {
        "after_all",
        "before_all",
        "category",
        "ignore",
        "set_up",
        "tear_down"
    };

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
        _testLogs = new();

        WrapperBase.GdAssertionFailed += OnAssertionFailed;
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
            if (_lifecycleMethodNames.Contains(method.Name))
            {
                continue;
            }

            ExecuteMethod(instance, method);
        }

        instance.Dispose();
    }

    private void ExecuteMethod(GodotObject instance, ScriptMethodInfo method)
    {
        _currentMethod = method;

        _ = instance.Call(method.Name);

        if (_testFailed)
        {
            _testFailed = false;
            return;
        }

        _props.Result.TestsPassed++;
        _testFailed = false;

        _testLogs.Add(GetTestResult(Passed));
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
