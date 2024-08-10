using System.Collections.Generic;
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

        foreach (Dictionary method in script.GetMethodList())
        {
            Dictionary returnInfo = (Dictionary)method["return"];

            _ = list.AddLast(
                new ScriptMethodInfo(
                    method["name"].AsString(),
                    method["args"].AsStringArray(),
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
