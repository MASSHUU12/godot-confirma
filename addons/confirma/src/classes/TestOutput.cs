using Confirma.Enums;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestOutput
{
    public string name;
    public string parameters;
    public ETestCaseState state;
    public string? message;

    public TestOutput(
        string name,
        string parameters,
        ETestCaseState state,
        string? message = ""
    )
    {
        this.name = name;
        this.parameters = parameters;
        this.state = state;
        this.message = message;
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
        switch (verbose)
        {
            case false when state == ETestCaseState.Passed:
                return;
            case true:
                PrintVerbose();
                break;
            default:
                PrintDefault();
                break;
        }
    }

    private void PrintDefault()
    {
        string color = GetTestCaseStateColor(state);
        string sState = GetTestCaseStateString(state);

        Log.PrintLine($"| {name}... ");

        if (state != ETestCaseState.Passed)
        {
            Log.Print($"\\_ {name}{(parameters.Length > 0 ? $"({parameters})" : string.Empty)}... ");
            Log.PrintLine($"{Colors.ColorText(sState, color)}.");
        }

        if (message is not null)
        {
            Log.PrintLine($"  |- {Colors.ColorText(message, color)}");
        }
    }

    private void PrintVerbose()
    {
        string color = GetTestCaseStateColor(state);
        string sState = GetTestCaseStateString(state);

        Log.PrintLine($"| {name}{(
            parameters.Length > 0
            ? $"({parameters})"
            : string.Empty)}... {Colors.ColorText(sState, color)}.");

        if (message is not null)
        {
            Log.PrintLine($"- {Colors.ColorText(message, color)}");
        }
    }
}
