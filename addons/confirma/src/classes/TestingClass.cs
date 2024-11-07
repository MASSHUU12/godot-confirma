using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes.Discovery;
using Confirma.Enums;
using Confirma.Exceptions;
using Confirma.Helpers;
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
    private object? _instance;

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
        TestResult results = new();
        List<TestLog> testLogs = new();
        bool runAfterAll = true;

        _props = props;

        if (!Type.IsStatic())
        {
            _instance = Activator.CreateInstance(Type);
        }

        try
        {
            RunLifecycleMethod(_beforeAllName, ref testLogs);
        }
        catch (LifecycleMethodException e)
        {
            runAfterAll = false;
            AddError(e.Message, ref testLogs);
            return new(0, 1, 0, 0, testLogs);
        }

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
                    $"No test methods found with the name {props.Target.DetailedName}."
                ));

                return new(0, 1, 0, 0, testLogs);
            }
        }

        foreach (TestingMethod method in TestMethods)
        {
            try
            {
                RunLifecycleMethod(_setUpName, ref testLogs);
            }
            catch (LifecycleMethodException e)
            {
                results.TestsFailed++;
                runAfterAll = false;
                AddError(e.Message, ref testLogs);
                continue;
            }

            TestMethodResult methodResult = method.Run(props, _instance);
            testLogs.AddRange(methodResult.TestLogs);

            try
            {
                RunLifecycleMethod(_tearDownName, ref testLogs);
            }
            catch (LifecycleMethodException e)
            {
                runAfterAll = false;
                methodResult.TestsPassed--;
                method.Result.TestsFailed++;
                AddError(e.Message, ref testLogs);
            }

            results += methodResult;
        }

        try
        {
            if (runAfterAll)
            {
                RunLifecycleMethod(_afterAllName, ref testLogs);
            }
        }
        catch (LifecycleMethodException e)
        {
            results.TestsFailed++;
            AddError(e.Message, ref testLogs);
        }

        return new(
            results.TestsPassed,
            results.TestsFailed,
            results.TestsIgnored,
            results.Warnings,
            testLogs
        );
    }

    private string? FindLifecycleMethodName(Type attributeType)
    {
        Attribute? attribute = Type.GetCustomAttribute(attributeType);

        return attribute is not LifecycleAttribute la
            ? null
            : la.MethodName;
    }

    private void RunLifecycleMethod(string? methodName, ref List<TestLog> testLogs)
    {
        if (methodName is null)
        {
            return;
        }

        if (_props.IsVerbose)
        {
            testLogs.Add(new(ELogType.Info, $"[{methodName}] {Type.Name}"));
        }

        if (Type.GetMethod(methodName) is not MethodInfo method)
        {
            throw new LifecycleMethodException(
                $"Lifecycle method {methodName} not found."
            );
        }

        try
        {
            _ = method.Invoke(_instance, null);
        }
        catch (Exception e)
        {
            string message = e.InnerException?.Message ?? e.Message;

            if (string.IsNullOrEmpty(message))
            {
                message = $"Calling lifecycle method {methodName} failed.";
            }

            throw new LifecycleMethodException(
                $"Error in lifecycle method {methodName}: {message}"
            );
        }
    }

    private void AddError(string error, ref List<TestLog> testLogs)
    {
        testLogs.Add(new(ELogType.Error, $"- {error}\n"));
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
