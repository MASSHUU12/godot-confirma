using System;
using Confirma.Helpers;

namespace Confirma.Exceptions;

public class ConfirmAssertException : Exception
{
    private static readonly AssertionMessageGenerator _generator = new();

    public ConfirmAssertException(string message) : base(message) { }

    public ConfirmAssertException(string message, Exception innerException)
    : base(message, innerException) { }

    public ConfirmAssertException(string assertion, object expected, object actual)
    : base(_generator.GenerateAssertionMessage(assertion, expected, actual)) { }

    public ConfirmAssertException() { }
}
