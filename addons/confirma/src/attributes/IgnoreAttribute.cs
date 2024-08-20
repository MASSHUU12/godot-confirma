using System;
using Confirma.Enums;
using Confirma.Types;
using Godot;
using static System.AttributeTargets;

namespace Confirma.Attributes;

[AttributeUsage(Class | Method, AllowMultiple = false)]
public class IgnoreAttribute : Attribute
{
    public EIgnoreMode Mode { get; init; }
    public string? Reason { get; init; }
    public bool HideFromResults { get; init; }
    public string Category { get; init; }

    public IgnoreAttribute(
        EIgnoreMode mode = EIgnoreMode.Always,
        string? reason = null,
        bool hideFromResults = false,
        string category = ""
    )
    {
        Mode = mode;
        Reason = string.IsNullOrEmpty(reason) ? null : reason;
        HideFromResults = hideFromResults;
        Category = category;
    }

    public bool IsIgnored(in RunTarget target)
    {
        return Mode switch
        {
            EIgnoreMode.Always => true,
            EIgnoreMode.InEditor => Engine.IsEditorHint(),
            EIgnoreMode.WhenNotRunningCategory => (
                (target.Target == ERunTargetType.Category
                && target.Name != Category)
                || string.IsNullOrEmpty(target.Name)
            ),
            _ => false
        };
    }
}
