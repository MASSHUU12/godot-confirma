using System;
using System.Linq;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Wrappers;

public partial class ConfirmArrayWrapper : WrapperBase
{
    public Array ConfirmSize(Array actual, int expected, string? message = null)
    {
        try
        {
            return (Array)actual
                .Cast<Variant>()
                .ConfirmCount(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public Array ConfirmEmpty(Array actual, string? message = null)
    {
        try
        {
            return (Array)actual
                .Cast<Variant>()
                .ConfirmEmpty(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public Array ConfirmNotEmpty(Array actual, string? message = null)
    {
        try
        {
            return (Array)actual
                .Cast<Variant>()
                .ConfirmNotEmpty(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public Array ConfirmContains(
        Array actual,
        Variant expected,
        string? message = null
    )
    {
        try
        {
            return (Array)actual
                .Cast<Variant>()
                .ConfirmContains(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public Array ConfirmNotContains(
        Array actual,
        Variant expected,
        string? message = null
    )
    {
        try
        {
            return (Array)actual
                .Cast<Variant>()
                .ConfirmNotContains(expected, ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }
}
