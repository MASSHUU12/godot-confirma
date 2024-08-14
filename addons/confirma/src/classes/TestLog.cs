using Confirma.Enums;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestLog
{
    public string? Message { get; }
    public string? Name { get; }
    public ETestCaseState State { get; } = ETestCaseState.Ignored;
    public ELogType Type { get; }

    public TestLog(ELogType type)
    {
        Type = type;
    }

    public TestLog(ELogType type, string message)
    {
        Type = type;
        Message = message;
    }

    public TestLog(
        ELogType type,
        string name,
        ETestCaseState state,
        string parameters = "",
        string? message = null
    )
    {
        Type = type;
        Message = message;
        Name = name + (
            parameters?.Length > 0
            ? $"({parameters.EscapeInvisibleCharacters()})"
            : string.Empty
        );
        State = state;
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
        switch (Type)
        {
            case ELogType.Method:
                switch (verbose)
                {
                    case false when State == ETestCaseState.Passed:
                        return;
                    case true:
                        PrintMethodVerbose();
                        break;
                    default:
                        PrintMethodDefault();
                        break;
                }
                break;
            case ELogType.Class:
                Log.Print($"> {Colors.ColorText(Message!, Colors.Class)}...");
                break;
            case ELogType.Info:
                if (Message != null)
                {
                    Log.PrintLine(Message);
                }
                break;
            case ELogType.Error:
                if (Message != null)
                {
                    Log.PrintError(Message);
                }
                break;
            case ELogType.Warning:
                if (Message != null)
                {
                    Log.PrintLine(Message);
                }
                break;
            case ELogType.Newline:
                Log.PrintLine();
                break;
        }
    }

    private void PrintMethodDefault()
    {
        string color = GetTestCaseStateColor(State);
        string sState = GetTestCaseStateString(State);

        Log.PrintLine($"| {Name}... ");

        if (State != ETestCaseState.Passed)
        {
            Log.Print($"\\_ {Name}... ");
            Log.PrintLine($"{Colors.ColorText(sState, color)}.");
        }

        if (Message is not null)
        {
            Log.PrintLine($"  |- {Colors.ColorText(Message, color)}");
        }
    }

    private void PrintMethodVerbose()
    {
        string color = GetTestCaseStateColor(State);
        string sState = GetTestCaseStateString(State);

        Log.PrintLine($"| {Name}... {Colors.ColorText(sState, color)}.");

        if (Message is not null)
        {
            Log.PrintLine($"- {Colors.ColorText(Message, color)}");
        }
    }
}
