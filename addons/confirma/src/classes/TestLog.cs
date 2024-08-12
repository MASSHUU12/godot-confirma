using Confirma.Enums;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestLog
{
    private readonly string? _message;
    private readonly string? _name;
    private readonly ETestCaseState _state = ETestCaseState.Ignored;
    private readonly ELogType _type;

    public TestLog(ELogType type)
    {
        _type = type;
    }

    public TestLog(ELogType type, string message)
    {
        _type = type;
        _message = message;
    }

    public TestLog(
        ELogType type,
        string name,
        ETestCaseState state,
        string parameters = "",
        string? message = null
    )
    {
        _type = type;
        _message = message;
        _name = name + (
            parameters?.Length > 0
            ? $"({parameters.EscapeInvisibleCharacters()})"
            : string.Empty
        );
        _state = state;
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
        switch (_type)
        {
            case ELogType.Method:
                switch (verbose)
                {
                    case false when _state == ETestCaseState.Passed:
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
                Log.Print($"> {_message}...");
                break;
            case ELogType.Info:
                if (_message != null)
                {
                    Log.PrintLine(_message);
                }
                break;
            case ELogType.Error:
                if (_message != null)
                {
                    Log.PrintError(_message);
                }
                break;
            case ELogType.Warning:
                if (_message != null)
                {
                    Log.PrintLine(_message);
                }
                break;
            case ELogType.Newline:
                Log.PrintLine();
                break;
        }
    }

    private void PrintMethodDefault()
    {
        string color = GetTestCaseStateColor(_state);
        string sState = GetTestCaseStateString(_state);

        Log.PrintLine($"| {_name}... ");

        if (_state != ETestCaseState.Passed)
        {
            Log.Print($"\\_ {_name}... ");
            Log.PrintLine($"{Colors.ColorText(sState, color)}.");
        }

        if (_message is not null)
        {
            Log.PrintLine($"  |- {Colors.ColorText(_message, color)}");
        }
    }

    private void PrintMethodVerbose()
    {
        string color = GetTestCaseStateColor(_state);
        string sState = GetTestCaseStateString(_state);

        Log.PrintLine($"| {_name}... {Colors.ColorText(sState, color)}.");

        if (_message is not null)
        {
            Log.PrintLine($"- {Colors.ColorText(_message, color)}");
        }
    }
}
