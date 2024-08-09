#if TOOLS
using Confirma.Classes;
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
        CheckArguments();

        if (!Props.RunTests && !Engine.IsEditorHint())
        {
            return;
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

    private void CheckArguments()
    {
        string[] args = OS.GetCmdlineUserArgs();

        if (DisplayServer.GetName() == "headless")
        {
            Props.IsHeadless = true;
        }

        foreach (string arg in args)
        {
            if (!Props.RunTests && arg.StartsWith("--confirma-run", InvariantCulture))
            {
                Props.RunTests = true;

                Props.ClassName = arg.Find('=') == -1
                    ? string.Empty
                    : arg.Split('=')[1];

                continue;
            }
            else if (Props.RunTests
                && !Props.ClassName.Equals(string.Empty, Ordinal)
                && arg.StartsWith("--confirma-method", InvariantCulture)
            )
            {
                Props.MethodName = arg.Find('=') == -1
                                    ? string.Empty
                                    : arg.Split('=')[1];

                continue;
            }

            if (!Props.QuitAfterTests && arg == "--confirma-quit")
            {
                Props.QuitAfterTests = true;
                continue;
            }

            if (!Props.ExitOnFail && arg == "--confirma-exit-on-failure")
            {
                Props.ExitOnFail = true;
                continue;
            }

            if (!Props.IsVerbose && arg == "--confirma-verbose")
            {
                Props.IsVerbose = true;
                continue;
            }

            if (!Props.DisableParallelization && arg == "--confirma-sequential")
            {
                Props.DisableParallelization = true;
                continue;
            }

            if (!Props.MonitorOrphans && arg == "--experimental-monitor-orphans")
            {
                Props.MonitorOrphans = true;
                continue;
            }

            if (!Props.DisableCsharp && arg == "--confirma-disable-cs")
            {
                Props.DisableCsharp = true;
                continue;
            }

            if (!Props.DisableGdScript && arg == "--confirma-disable-gd")
            {
                Props.DisableGdScript = true;
            }
        }
    }

    private void ChangeScene()
    {
        _ = GetTree().CallDeferred("change_scene_to_file", "uid://cq76c14wl2ti3");

        if (Props.QuitAfterTests)
        {
            GetTree().Quit();
        }
    }
}
#endif
