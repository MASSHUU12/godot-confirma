using System.Collections.Generic;
using System.Linq;
using Confirma.Classes.Discovery;
using Confirma.Helpers;
using Confirma.Interfaces;
using Confirma.Types;
using Godot;

namespace Confirma.Classes.Executors;

public class GdTestExecutor : ITestExecutor
{
    private TestsProps _props;

    public GdTestExecutor(TestsProps props)
    {
        _props = props;
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
            ExecuteClass(testClass, _props.Result);
        }

        result = _props.Result;
        return testClasses.Count();
    }

    private static void ExecuteClass(GDScript testClass, TestResult result)
    {
        Log.Print($"> {testClass.GetClass()}...");

        GodotObject instance = testClass.New().AsGodotObject();

        IEnumerable<string> methods = testClass
            .GetScriptMethodList()
            .Select(static method => method["name"].AsString());

        foreach (string method in methods)
        {
            _ = instance.Call(method);
        }

        instance.Dispose();
    }
}
