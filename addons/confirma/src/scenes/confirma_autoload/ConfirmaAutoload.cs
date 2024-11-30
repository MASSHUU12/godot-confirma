#if TOOLS
using System.Collections.Generic;
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

                    if (string.IsNullOrEmpty(value))
                    {
                        Log.PrintError(
                            "Invalid value: '--confirma-method' cannot be empty.\n"
                        );
                        Props.RunTests = false;
                        return;
                    }

                    Props.Target = Props.Target with
                    {
                        Target = ERunTargetType.Method,
                        DetailedName = value
                    };
                }
            )
        );

        /**
        if (Props.RunTests
            && arg.StartsWith(prefix + "method", InvariantCulture)
        )
        {
            if (string.IsNullOrEmpty(Props.Target.Name))
            {
                Log.PrintError(
                    "Invalid value: argument '--confirma-run' cannot be empty"
                    + " when using argument '--confirma-method'.\n"
                );
                return false;
            }

            string method = ParseArgumentContent(arg);

            if (string.IsNullOrEmpty(method))
            {
                Log.PrintError(
                    "Invalid value: '--confirma-method' cannot be empty.\n"
                );
                return false;
            }

            Props.Target = Props.Target with
            {
                Target = ERunTargetType.Method,
                DetailedName = method
            };

            continue;
        }
        */
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

    /**
private bool CheckArguments()
{
            foreach (string arg in args)
            {
                if (Props.RunTests
                    && arg.StartsWith(prefix + "method", InvariantCulture)
                )
                {
                    if (string.IsNullOrEmpty(Props.Target.Name))
                    {
                        Log.PrintError(
                            "Invalid value: argument '--confirma-run' cannot be empty"
                            + " when using argument '--confirma-method'.\n"
                        );
                        return false;
                    }

                    string method = ParseArgumentContent(arg);

                    if (string.IsNullOrEmpty(method))
                    {
                        Log.PrintError(
                            "Invalid value: '--confirma-method' cannot be empty.\n"
                        );
                        return false;
                    }

                    Props.Target = Props.Target with
                    {
                        Target = ERunTargetType.Method,
                        DetailedName = method
                    };

                    continue;
                }

                if (arg.StartsWith(prefix + "category", InvariantCulture))
                {
                    string category = ParseArgumentContent(arg);

                    if (string.IsNullOrEmpty(category))
                    {
                        Log.PrintError(
                            "Invalid value: '--confirma-category' cannot be empty.\n"
                        );
                        return false;
                    }

                    Props.Target = Props.Target with
                    {
                        Target = ERunTargetType.Category,
                        Name = category
                    };

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

                if (Props.MonitorOrphans && arg == prefix + "disable-orphans-monitor")
                {
                    Props.MonitorOrphans = false;
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

                if (arg.StartsWith(prefix + "output", InvariantCulture)
                    && !arg.StartsWith(prefix + "output-path", InvariantCulture)
                )
                {
                    string value = ParseArgumentContent(arg);

                    if (!EnumHelper.TryParseFlagsEnum(value, out ELogOutputType type)
                    )
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

                    if (!Path.Exists(Path.GetDirectoryName(value))
                        || Path.GetExtension(value) != ".json"
                    )
                    {
                        Log.PrintError($"Invalid output path: {value}.\n");
                        return false;
                    }

                    Props.OutputPath = value;
                }
            }

            return true;
}
            */

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
