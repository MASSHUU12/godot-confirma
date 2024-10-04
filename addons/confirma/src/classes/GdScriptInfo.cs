using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Confirma.Enums;
using Confirma.Types;
using Godot;
using static Confirma.Enums.ELifecycleMethodName;

namespace Confirma.Classes;

public class GdScriptInfo : ScriptInfo
{
    public ImmutableDictionary<ELifecycleMethodName, ScriptMethodInfo>
        LifecycleMethods
    { get; init; }
    private static readonly string[] _lifecycleMethodNames = {
        "after_all",
        "before_all",
        "category",
        "ignore",
        "set_up",
        "tear_down"
    };

    public GdScriptInfo(
        Script script,
        LinkedList<ScriptMethodInfo> methods
    )
    : base(script, methods)
    {
        IEnumerable<ScriptMethodInfo> lifecycleMethods = Methods.Where(
            static m => _lifecycleMethodNames.Contains(m.Name)
        );

        LifecycleMethods =
            lifecycleMethods.ToImmutableDictionary(
                static info => info.Name switch
                {
                    "after_all" => AfterAll,
                    "before_all" => BeforeAll,
                    "set_up" => SetUp,
                    "tear_down" => TearDown,
                    "category" => Category,
                    "ignore" => Ignore,
                    _ => throw new ArgumentException(
                        $"Unknown method name: {info.Name}."
                    ),
                },
                static info => info
            );

        Methods = new(Methods.Except(lifecycleMethods).ToList());
    }

    public static new GdScriptInfo Parse(in Script script)
    {
        return new(script, ScriptInfo.Parse(script).Methods);
    }
}
