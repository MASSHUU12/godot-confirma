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
using static Confirma.Enums.ELifecycleMethodName;

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
        _testLogs = new();

        WrapperBase.GdAssertionFailed += OnAssertionFailed;
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<GdScriptInfo> testClasses = GdTestDiscovery.GetTestScripts(
            _props.GdTestPath
        );

        if (!string.IsNullOrEmpty(_props.Target.Name))
        {
            if (_props.Target.Target is ERunTargetType.Class or ERunTargetType.Method)
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

            if (_props.Target.Target == ERunTargetType.Category)
            {
                testClasses = testClasses.Where(
                    tc => GetCategory(tc) == _props.Target.Name
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

        foreach (GdScriptInfo testClass in testClasses)
        {
            ExecuteClass(testClass);
        }

        result = _props.Result;
        result.TestLogs.AddRange(_testLogs);
        return testClasses.Count();
    }

    private static string GetCategory(GdScriptInfo testClass)
    {
        GDScript script = (GDScript)testClass.Script;
        using GodotObject instance = script.New().AsGodotObject();
        ScriptMethodInfo method = testClass.LifecycleMethods[Category];

        return instance.Call(method.Name).AsString();
    }

    private static string GetClassName(GDScript script)
    {
        string className = script.GetGlobalName();

        if (string.IsNullOrEmpty(className))
        {
            className = script.ResourcePath.GetFile();
        }

        return className;
    }

    private void ExecuteClass(GdScriptInfo testClass)
    {
        GDScript script = (GDScript)testClass.Script;
        string className = GetClassName(script);

        _testLogs.Clear();
        _testLogs.Add(new(ELogType.Class, className));
        _testLogs.Add(new(ELogType.Newline));

        GodotObject instance = script.New().AsGodotObject();

        ExecuteLifecycleMethod(instance, testClass, BeforeAll);

        foreach (ScriptMethodInfo method in testClass.Methods)
        {
            ExecuteLifecycleMethod(instance, testClass, SetUp);

            ExecuteMethod(instance, method);

            ExecuteLifecycleMethod(instance, testClass, TearDown);
        }

        ExecuteLifecycleMethod(instance, testClass, AfterAll);

        instance.Dispose();
    }

    private static void ExecuteLifecycleMethod(
        GodotObject instance,
        GdScriptInfo script,
        ELifecycleMethodName name
    )
    {
        ScriptMethodInfo method = script.LifecycleMethods[name];

        _ = instance.Call(method.Name);
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
