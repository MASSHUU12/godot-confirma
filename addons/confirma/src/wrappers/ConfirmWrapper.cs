using Confirma.Extensions;
using Confirma.Exceptions;

namespace Confirma.Wrappers;

public partial class ConfirmWrapper : WrapperBase
{
    public static object? ConfirmNull(object? actual, string? message = null)
    {
        try
        {
            return actual.ConfirmNull(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static object? ConfirmNotNull(object? actual, string? message = null)
    {
        try
        {
            return actual.ConfirmNotNull(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }
}
