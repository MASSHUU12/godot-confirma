#if TOOLS
using System;
using System.IO;
using Confirma.Classes;
using Confirma.Enums;
using Confirma.Helpers;
using Confirma.Types;
using Godot;
using static System.StringComparison;

namespace Confirma.Scenes;

[Tool]
public partial class ConfirmaAutoload : Node
{
    [Signal]
    public delegate void GdAssertionFailedEventHandler(string message);

    public TestsProps Props = new();

    public override void _Ready()
    {
        if (!CheckArguments())
        {
            Props.RunTests = false;
            GetTree().Quit(1);
            return;
        }

        if (!Props.RunTests && !Engine.IsEditorHint())
        {
            GetTree().Quit();
        }

        Props.Autoload = this;
        SetupGlobals();

        if (!Engine.IsEditorHint())
        {
            ChangeScene();
        }
    }

    private void SetupGlobals()
    {
        Log.IsHeadless = Props.IsHeadless;
        Global.Root = GetTree().Root;
    }

    private bool CheckArguments()
    {
        string[] args = OS.GetCmdlineUserArgs();
        const string prefix = "--confirma-";

        if (DisplayServer.GetName() == "headless")
        {
            Props.IsHeadless = true;
        }

        foreach (string arg in args)
        {
            if (!Props.RunTests && arg.StartsWith(prefix + "run", InvariantCulture))
            {
                Props.RunTests = true;
                Props.ClassName = ParseArgumentContent(arg);

                continue;
            }
            else if (Props.RunTests
                && !Props.ClassName.Equals(string.Empty, Ordinal)
                && arg.StartsWith(prefix + "method", InvariantCulture)
            )
            {
                Props.MethodName = ParseArgumentContent(arg);

                continue;
            }

            if (!Props.ExitOnFail && arg == prefix + "exit-on-failure")
            {
                Props.ExitOnFail = true;
                continue;
            }

            if (!Props.IsVerbose && arg == prefix + "verbose")
            {
                Props.IsVerbose = true;
                continue;
            }

            if (!Props.DisableParallelization && arg == prefix + "sequential")
            {
                Props.DisableParallelization = true;
                continue;
            }

            if (!Props.MonitorOrphans && arg == "--experimental-monitor-orphans")
            {
                Props.MonitorOrphans = true;
                continue;
            }

            if (!Props.DisableCsharp && arg == prefix + "disable-cs")
            {
                Props.DisableCsharp = true;
                continue;
            }

            if (!Props.DisableGdScript && arg == prefix + "disable-gd")
            {
                Props.DisableGdScript = true;
                continue;
            }

            if (arg.StartsWith(prefix + "gd-path", InvariantCulture))
            {
                Props.GdTestPath = ParseArgumentContent(arg);
                continue;
            }

            if (arg.StartsWith(prefix + "output=", InvariantCulture))
            {
                string value = ParseArgumentContent(arg);

                if (!EnumHelper.TryParseFlagsEnum(value, out ELogOutputType type))
                {
                    Log.PrintError($"Invalid value '{value}' for '{prefix}output' argument.\n");
                    return false;
                }

                Props.OutputType = type;
                continue;
            }

            if (arg.StartsWith(prefix + "output-path", InvariantCulture))
            {
                string value = ParseArgumentContent(arg);

                if (!Path.Exists(value) || Path.GetExtension(value) != ".json")
                {
                    Log.PrintError($"Invalid output path: {value}.\n");
                    return false;
                }

                Props.OutputPath = value;
            }
        }

        return true;
    }

    private static string ParseArgumentContent(string argument)
    {
        return argument.Find('=') == -1
            ? string.Empty
            : argument.Split('=')[1];
    }

    private void ChangeScene()
    {
        _ = GetTree().CallDeferred("change_scene_to_file", "uid://cq76c14wl2ti3");

        if (!Engine.IsEditorHint())
        {
            GetTree().Quit();
        }
    }
}
#endif
