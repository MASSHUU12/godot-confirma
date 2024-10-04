using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes.Discovery;
using Confirma.Enums;
using Confirma.Types;
using Godot;

namespace Confirma.Classes;

public class TestingClass
{
    public Type Type { get; init; }
    public bool IsParallelizable { get; init; }
    public IEnumerable<TestingMethod> TestMethods { get; set; }

    private readonly string? _setUpName;
    private readonly string? _tearDownName;
    private readonly string? _afterAllName;
    private readonly string? _beforeAllName;

    private TestsProps _props;

    public TestingClass(Type type)
    {
        Type = type;
        TestMethods = CsTestDiscovery.DiscoverTestMethods(type);
        IsParallelizable = type.GetCustomAttribute<ParallelizableAttribute>() is not null;

        _afterAllName = FindLifecycleMethodName(typeof(AfterAllAttribute));
        _beforeAllName = FindLifecycleMethodName(typeof(BeforeAllAttribute));

        _setUpName = FindLifecycleMethodName(typeof(SetUpAttribute));
        _tearDownName = FindLifecycleMethodName(typeof(TearDownAttribute));
    }

    public TestClassResult Run(TestsProps props)
    {
        uint passed = 0, failed = 0, ignored = 0, warnings = 0;
        List<TestLog> testLogs = new();

        _props = props;

        warnings += RunLifecycleMethod(_beforeAllName, ref testLogs);

        if (_props.Target.Target == ERunTargetType.Method
            && !string.IsNullOrEmpty(props.Target.DetailedName)
        )
        {
            TestMethods = TestMethods.Where(
                tm => tm.Name == props.Target.DetailedName
            );

            if (!TestMethods.Any())
            {
                testLogs.Add(new(ELogType.Error,
                    ELangType.CSharp,
                    $"No test methods found with the name '{props.Target.DetailedName}'."
                ));

                return new(0, 1, 0, 0, testLogs);
            }
        }

        foreach (TestingMethod method in TestMethods)
        {
            warnings += RunLifecycleMethod(_setUpName, ref testLogs);

            int currentOrphans = GetOrphans();

            TestMethodResult methodResult = method.Run(props);
            testLogs.AddRange(methodResult.TestLogs);

            warnings += RunLifecycleMethod(_tearDownName, ref testLogs);

            int newOrphans = GetOrphans();
            if (currentOrphans < newOrphans)
            {
                warnings++;
                testLogs.Add(new(ELogType.Warning,
                    ELangType.CSharp,
                    $"Calling {method.Name} created {newOrphans - currentOrphans} new orphan/s.\n"
                ));
            }

            passed += methodResult.TestsPassed;
            failed += methodResult.TestsFailed;
            ignored += methodResult.TestsIgnored;
            warnings += methodResult.Warnings;
        }

        warnings += RunLifecycleMethod(_afterAllName, ref testLogs);

        return new(passed, failed, ignored, warnings, testLogs);
    }

    private string? FindLifecycleMethodName(Type attributeType)
    {
        Attribute? attribute = Type.GetCustomAttribute(attributeType);

        return attribute is not LifecycleAttribute la
            ? null
            : la.MethodName;
    }

    private byte RunLifecycleMethod(string? methodName, ref List<TestLog> testLogs)
    {
        if (methodName is null)
        {
            return 0;
        }

        if (_props.IsVerbose)
        {
            testLogs.Add(new(ELogType.Info, $"[{methodName}] {Type.Name}"));
        }

        if (Type.GetMethod(methodName) is not MethodInfo method)
        {
            testLogs.Add(new(
                ELogType.Error,
                $"- There is no lifecycle method named '{methodName}'.\n"
            ));
            return 1;
        }

        try
        {
            _ = method.Invoke(null, null);
        }
        catch (Exception e)
        {
            testLogs.Add(new(
                ELogType.Error,
                $"- {e.InnerException?.Message ?? e.Message}\n"
            ));
            return 1;
        }

        return 0;
    }

    private int GetOrphans()
    {
        return !_props.MonitorOrphans
            ? 0
            : (int)Performance.Singleton.GetMonitor(
                Performance.Monitor.ObjectOrphanNodeCount
            );
    }
}
