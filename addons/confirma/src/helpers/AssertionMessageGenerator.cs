using System.Globalization;
using Confirma.Formatters;

namespace Confirma.Helpers;

public class AssertionMessageGenerator
{
    private readonly string _format;
    private readonly string _assertion;
    private readonly object? _expected;
    private readonly object? _actual;
    private readonly Formatter _formatter;

    public AssertionMessageGenerator(
        string format,
        string assertion,
        Formatter formatter,
        object? expected,
        object? actual
    )
    {
        _format = format;
        _assertion = assertion;
        _formatter = formatter;
        _expected = expected;
        _actual = actual;
    }

    public string GenerateMessage()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            "Assertion {0} failed: " + _format,
            _assertion,
            _expected is not null ? _formatter.Format(_expected) : null,
            _actual is not null ? _formatter.Format(_actual) : null
        );
    }
}
