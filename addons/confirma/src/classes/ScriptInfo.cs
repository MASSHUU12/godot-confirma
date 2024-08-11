using System.Collections.Generic;
using System.Linq;
using Confirma.Types;
using Godot;
using Godot.Collections;

namespace Confirma.Classes;

public class ScriptInfo
{
    public Script Script { get; init; }
    public LinkedList<ScriptMethodInfo> Methods { get; init; }

    public ScriptInfo(Script script, LinkedList<ScriptMethodInfo> methods)
    {
        Script = script;
        Methods = methods;
    }

    public static ScriptInfo Parse(in Script script)
    {
        LinkedList<ScriptMethodInfo> list = new();

        foreach (Dictionary method in script.GetScriptMethodList())
        {
            Dictionary returnInfo = (Dictionary)method["return"];
            LinkedList<ScriptMethodArgumentInfo> arg = new();

            foreach (Dictionary argInfo in method["args"].AsGodotArray<Dictionary>())
            {
                _ = arg.AddLast(
                    new ScriptMethodArgumentInfo(
                        argInfo["name"].AsString(),
                        argInfo["class_name"].AsString(),
                        argInfo["type"].AsInt32(),
                        argInfo["hint"].AsInt32(),
                        argInfo["hint_string"].AsString(),
                        argInfo["usage"].AsInt32()
                    )
                );
            }

            _ = list.AddLast(
                new ScriptMethodInfo(
                    method["name"].AsString(),
                    arg.ToArray(),
                    method["default_args"].AsStringArray(),
                    method["flags"].AsInt32(),
                    method["id"].AsInt32(),
                    new(
                        returnInfo["name"].AsString(),
                        returnInfo["class_name"].AsString(),
                        returnInfo["type"].AsInt32(),
                        returnInfo["hint"].AsInt32(),
                        returnInfo["hint_string"].AsString(),
                        returnInfo["usage"].AsInt32()
                    )
                )
            );
        }

        return new(script, list);
    }
}
