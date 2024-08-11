using Confirma.Enums;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestLog
{
    readonly string? message;
    readonly string? name;
    readonly ETestCaseState state = ETestCaseState.Ignored;
    readonly Elogtype type;

    public TestLog (Elogtype type)
    {
        this.type = type;
    }

    public TestLog (Elogtype type, string message)
    {
        this.type = type;
        this.message = message;
    }

    public TestLog (
        Elogtype type,
        string name,
        ETestCaseState state,
        string parameters = "",
        string? message = null
        )
    {
        this.type = type;
        this.message = message;
        this.name = $"{name}{(parameters?.Length > 0 ? $"({parameters})" : string.Empty)}";
        this.state = state;
    }

    public static string GetTestCaseStateString(ETestCaseState state)
    {
        return state.ToString().ToLowerInvariant();
    }

    public static string GetTestCaseStateColor(ETestCaseState state)
    {
        return state switch
        {
            ETestCaseState.Passed => Colors.Success,
            ETestCaseState.Failed => Colors.Error,
            ETestCaseState.Ignored => Colors.Warning,
            _ => "unknown"
        };
    }

    public void PrintOutput(bool verbose = false)
    {
        switch (type)
        {
            case Elogtype.Method:
                switch (verbose)
                {
                    case false when state == ETestCaseState.Passed:
                        return;
                    case true:
                        PrintMethodVerbose();
                        break;
                    default:
                        PrintMethodDefault();
                        break;
                }
                break;
            case Elogtype.Class:
                Log.Print($"> {message}...");
                break;
            case Elogtype.Info:
                if (message != null)
                {
                    Log.PrintLine(message);
                }
                break;
            case Elogtype.Error:
                if (message != null)
                {
                    Log.PrintError(message);
                }
                break;
            case Elogtype.Warning:
                if (message != null)
                {
                    Log.PrintLine(message);
                }
                break;
            case Elogtype.Newline:
                Log.PrintLine();
                break;
        }
    }

    private void PrintMethodDefault()
    {
        string color = GetTestCaseStateColor(state);
        string sState = GetTestCaseStateString(state);

        Log.PrintLine($"| {name}... ");

        if (state != ETestCaseState.Passed)
        {
            Log.Print($"\\_ {name}... ");
            Log.PrintLine($"{Colors.ColorText(sState, color)}.");
        }

        if (message is not null)
        {
            Log.PrintLine($"  |- {Colors.ColorText(message, color)}");
        }
    }

    private void PrintMethodVerbose()
    {
        string color = GetTestCaseStateColor(state);
        string sState = GetTestCaseStateString(state);

        Log.PrintLine($"| {name}... {Colors.ColorText(sState, color)}.");

        if (message is not null)
        {
            Log.PrintLine($"- {Colors.ColorText(message, color)}");
        }
    }
}
