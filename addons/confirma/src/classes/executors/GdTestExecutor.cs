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
    private readonly List<TestLog> _testLogs = new();
    private ScriptMethodInfo? _currentMethod;

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
        WrapperBase.GdAssertionFailed += OnAssertionFailed;
    }

    public int Execute(out TestResult? result)
    {
        result = null;

        IEnumerable<GdScriptInfo> testClasses = GetTestClasses(_props.GdTestPath);

        if (!testClasses.Any())
        {
            LogErrorIfNoTestClassesFound();
            return -1;
        }

        _props.ResetStats();

        foreach (GdScriptInfo testClass in testClasses)
        {
            ExecuteTestClass(testClass);
        }

        result = _props.Result;
        result.TestLogs.AddRange(_testLogs);
        return testClasses.Count();
    }

    private void LogErrorIfNoTestClassesFound()
    {
        string message = _props.Target.Target == ERunTargetType.Category
            ? $"No test classes found with category '{_props.Target.Name}'.\n"
            : $"No test class found with the name '{_props.Target.Name}'.\n";
        Log.PrintError(message);
    }

    private IEnumerable<GdScriptInfo> GetTestClasses(string testPath)
    {
        IEnumerable<GdScriptInfo> testClasses = GdTestDiscovery
            .GetTestScripts(testPath);

        if (!string.IsNullOrEmpty(_props.Target.Name))
        {
            testClasses = FilterTestClasses(testClasses, _props.Target);
        }

        return testClasses;
    }

    private static IEnumerable<GdScriptInfo> FilterTestClasses(
        IEnumerable<GdScriptInfo> testClasses,
        RunTarget target
    )
    {
        if (target.Target is ERunTargetType.Class or ERunTargetType.Method)
        {
            return testClasses.Where(tc => tc.Script.GetClass() == target.Name);
        }

        return target.Target == ERunTargetType.Category
            ? testClasses.Where(tc => GetCategory(tc) == target.Name)
            : testClasses;
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

    private void ExecuteTestClass(GdScriptInfo testClass)
    {
        GDScript script = (GDScript)testClass.Script;
        string className = GetClassName(script);

        _testLogs.Clear();
        _testLogs.Add(new(ELogType.Class, className));
        _testLogs.Add(new(ELogType.Newline));

        using GodotObject instance = script.New().AsGodotObject();

        ExecuteLifecycleMethod(instance, testClass, BeforeAll);

        foreach (ScriptMethodInfo method in testClass.Methods)
        {
            ExecuteLifecycleMethod(instance, testClass, SetUp);

            ExecuteMethod(instance, method);

            ExecuteLifecycleMethod(instance, testClass, TearDown);
        }

        ExecuteLifecycleMethod(instance, testClass, AfterAll);
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
