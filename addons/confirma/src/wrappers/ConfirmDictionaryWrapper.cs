using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;
using Godot.Collections;

namespace Confirma.Wrappers;

public partial class ConfirmDictionaryWrapper : WrapperBase
{
    public static Dictionary ConfirmContainsKey(
        Dictionary dict,
        Variant key,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmContainsKey(
                key,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }

    public static Dictionary ConfirmNotContainsKey(
        Dictionary dict,
        Variant key,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmNotContainsKey(
                key,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }

    public static Dictionary ConfirmContainsValue(
        Dictionary dict,
        Variant value,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmContainsValue(
                value,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }

    public static Dictionary ConfirmNotContainsValue(
        Dictionary dict,
        Variant value,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmNotContainsValue(
                value,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }

    public static Dictionary ConfirmContainsKeyValuePair(
        Dictionary dict,
        Variant key,
        Variant value,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmContainsKeyValuePair(
                key,
                value,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }

    public static Dictionary ConfirmNotContainsKeyValuePair(
        Dictionary dict,
        Variant key,
        Variant value,
        string? message = null
    )
    {
        try
        {
            return (Dictionary)dict.ConfirmNotContainsKeyValuePair(
                key,
                value,
                ParseMessage(message)
            );
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return dict;
    }
}
