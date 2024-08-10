using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Types;
using Godot;
using Godot.Collections;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ScriptInfoTest
{
    private static CSharpScript? _script;

    [BeforeAll]
    public static void BeforeAll()
    {
        _script = GD.Load<CSharpScript>(
            "res://tests/ClassToRunTestsOn.cs"
        ).New().As<CSharpScript>();
    }

    [AfterAll]
    public static void AfterAll()
    {
        _script!.Dispose();
    }

    [TestCase]
    public static void Constructor_SetsPropertiesCorrectly()
    {
        LinkedList<ScriptMethodInfo> methods = new();
        ScriptInfo scriptInfo = new(_script!, methods);

        _ = scriptInfo.Script.ConfirmEqual(_script);
        _ = scriptInfo.Methods.ConfirmEqual(methods);
    }

    [TestCase]
    public static void Parse_MethodsAreParsedCorrectly()
    {
        Array<Dictionary> methodList = _script!.GetMethodList();

        ScriptInfo scriptInfo = ScriptInfo.Parse(_script!);

        _ = scriptInfo.Methods.Count.ConfirmEqual(methodList.Count);

        int i = 0;
        foreach (ScriptMethodInfo methodActual in scriptInfo.Methods)
        {
            Dictionary methodExpected = methodList[i];
            Dictionary returnExpected = (Dictionary)methodExpected["return"];

            _ = methodActual.Name.ConfirmEqual(
                methodExpected["name"].AsString()
            );
            _ = methodActual.Args.ConfirmEqual(
                methodExpected["args"].AsStringArray()
            );
            _ = methodActual.DefaultArgs.ConfirmEqual(
                methodExpected["default_args"].AsStringArray()
            );
            _ = methodActual.Flags.ConfirmEqual(
                methodExpected["flags"].AsInt32()
            );
            _ = methodActual.Id.ConfirmEqual(
                methodExpected["id"].AsInt32()
            );

            _ = methodActual.ReturnInfo.Name.ConfirmEqual(
                returnExpected["name"].AsString()
            );
            _ = methodActual.ReturnInfo.ClassName.ConfirmEqual(
                returnExpected["class_name"].AsString()
            );
            _ = methodActual.ReturnInfo.Type.ConfirmEqual(
                returnExpected["type"].AsInt32()
            );
            _ = methodActual.ReturnInfo.Hint.ConfirmEqual(
                returnExpected["hint"].AsInt32()
            );
            _ = methodActual.ReturnInfo.HintString.ConfirmEqual(
                returnExpected["hint_string"].AsString()
            );
            _ = methodActual.ReturnInfo.Usage.ConfirmEqual(
                returnExpected["usage"].AsInt32()
            );

            i++;
        }
    }
}
