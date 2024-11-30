#if TOOLS
using System.Collections.Generic;
using System.IO;
using Confirma.Classes;
using Confirma.Enums;
using Confirma.Helpers;
using Confirma.Terminal;
using Confirma.Types;
using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class ConfirmaAutoload : Node
{
    public TestsProps Props = new();

    private bool _usedConfirmaApi;
    private readonly Cli _cli = new("--confirma-");

    public override void _Ready()
    {
        Initialize();

        if (!ParseArguments())
        {
            Props.RunTests = false;
            GetTree().Quit(1);
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

    private void Initialize()
    {
        if (DisplayServer.GetName() == "headless")
        {
            Props.IsHeadless = true;
        }

        _ = _cli.RegisterArgument(
            new(
                "run",
                action: (value) =>
                {
                    Props.RunTests = true;
                    Props.Target = Props.Target with
                    {
                        Target = string.IsNullOrEmpty(value)
                            ? ERunTargetType.All
                            : ERunTargetType.Class,
                        Name = value ?? string.Empty
                    };
                }
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "help",
                action: (value) =>
                {
                    Props.ShowHelp = true;
                    Props.SelectedHelpPage = string.IsNullOrEmpty(value)
                        ? "default"
                        : value;
                }
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "method",
                allowEmpty: false,
                action: (value) =>
                {
                    if (string.IsNullOrEmpty(Props.Target.Name))
                    {
                        Log.PrintError(
                            "Invalid value: argument '--confirma-run' cannot be empty"
                            + " when using argument '--confirma-method'.\n"
                        );
                        Props.RunTests = false;
                        return;
                    }

                    Props.Target = Props.Target with
                    {
                        Target = ERunTargetType.Method,
                        DetailedName = value!
                    };
                }
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "category",
                allowEmpty: false,
                action: (value) =>
                {
                    Props.Target = Props.Target with
                    {
                        Target = ERunTargetType.Category,
                        Name = value!
                    };
                }
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "exit-on-failure",
                isFlag: true,
                action: (_) => Props.ExitOnFail = true
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "verbose",
                isFlag: true,
                action: (_) => Props.IsVerbose = true
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "sequential",
                isFlag: true,
                action: (_) => Props.DisableParallelization = true
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "disable-orphans-monitor",
                isFlag: true,
                action: (_) => Props.MonitorOrphans = false
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "disable-cs",
                isFlag: true,
                action: (_) => Props.DisableCsharp = true
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "disable-gd",
                isFlag: true,
                action: (_) => Props.DisableGdScript = true
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "gd-path",
                allowEmpty: false,
                action: (value) => Props.GdTestPath = value!
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "output",
                allowEmpty: false,
                action: (value) =>
                {
                    if (!EnumHelper.TryParseFlagsEnum(
                        value!,
                        out ELogOutputType type
                    ))
                    {
                        Log.PrintError(
                            $"Invalid value '{value}' for '--confirma-output' argument.\n"
                        );
                        Props.RunTests = false;
                        return;
                    }

                    Props.OutputType = type;
                }
            )
        );
        _ = _cli.RegisterArgument(
            new(
                "output-path",
                allowEmpty: false,
                action: (value) =>
                {
                    if (!Path.Exists(Path.GetDirectoryName(value))
                        || Path.GetExtension(value) != ".json"
                    )
                    {
                        Log.PrintError($"Invalid output path: {value}.\n");
                        Props.RunTests = false;
                        return;
                    }

                    Props.OutputPath = value!;
                }
            )
        );
    }

    private bool ParseArguments()
    {
        List<string> errors = _cli.Parse(OS.GetCmdlineUserArgs(), true);

        _usedConfirmaApi = _cli.GetValuesCount() != 0;

        if (errors.Count == 0)
        {
            return true;
        }

        foreach (string error in errors)
        {
            Log.PrintError(error + "\n");
        }

        return false;
    }

    private void ChangeScene()
    {
        if (Props.ShowHelp)
        {
            _ = GetTree().CallDeferred("change_scene_to_file",
            $"{Plugin.GetPluginLocation()}src/scenes/help_panel/help_panel.tscn");
            return;
        }

        if (!Props.RunTests)
        {
            if (_usedConfirmaApi)
            {
                Log.PrintWarning(
                    "You're trying to use Confirma without '--confirma-run' argument."
                    + " The game continues normally.\n"
                );
            }
            return;
        }

        _ = GetTree().CallDeferred("change_scene_to_file", "uid://cq76c14wl2ti3");

        if (!Engine.IsEditorHint())
        {
            GetTree().Quit();
        }
    }
}
#endif
