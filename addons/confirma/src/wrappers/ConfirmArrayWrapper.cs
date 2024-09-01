using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;
using Godot.Collections;

// Wrappers:
// Dictionary
// Numeric
// Range
// Signal
// String
// Vector

namespace Confirma.Wrappers;

public partial class ConfirmArrayWrapper : WrapperBase
{
    public static Array ConfirmSize(
        Array actual,
        int expected,
        string? message = null
    )
    {
        try
        {
            return (Array)actual
                .ConfirmCount(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static Array ConfirmEmpty(Array actual, string? message = null)
    {
        try
        {
            return (Array)actual
                .ConfirmEmpty(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static Array ConfirmNotEmpty(Array actual, string? message = null)
    {
        try
        {
            return (Array)actual
                .ConfirmNotEmpty(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static Array ConfirmContains(
        Array actual,
        Variant expected,
        string? message = null
    )
    {
        try
        {
            return (Array)actual
                .ConfirmContains(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static Array ConfirmNotContains(
        Array actual,
        Variant expected,
        string? message = null
    )
    {
        try
        {
            return (Array)actual
                .ConfirmNotContains(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }
}
