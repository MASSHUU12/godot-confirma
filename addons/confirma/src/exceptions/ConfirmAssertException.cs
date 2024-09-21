using System;
using Confirma.Formatters;
using Confirma.Helpers;

namespace Confirma.Exceptions;

public class ConfirmAssertException : Exception
{
    public ConfirmAssertException(string message) : base(message) { }

    public ConfirmAssertException(string message, Exception innerException)
    : base(message, innerException) { }

    public ConfirmAssertException(
        string assertion,
        object expected,
        object actual
    )
    : base(
        new AssertionMessageGenerator(
            Formatter.DefaultFormat,
            assertion,
            new DefaultFormatter(),
            expected,
            actual
        ).GenerateMessage()
    )
    { }

    public ConfirmAssertException(
        string format,
        string assertion,
        Formatter formatter,
        object actual,
        object? expected,
        string? customMessage
    )
    : base(
        customMessage
        ?? new AssertionMessageGenerator(
            format,
            assertion,
            formatter,
            actual,
            expected
        ).GenerateMessage()
    )
    { }

    public ConfirmAssertException() { }
}
