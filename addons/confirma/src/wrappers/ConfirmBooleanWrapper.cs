using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Wrappers;

public partial class ConfirmBooleanWrapper : WrapperBase
{
    public static bool ConfirmTrue(bool actual, string? message = null)
    {
        try
        {
            return actual.ConfirmTrue(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }

    public static bool ConfirmFalse(bool actual, string? message = null)
    {
        try
        {
            return actual.ConfirmFalse(ParseMessage(message));
        }
        catch (ConfirmAssertException e)
        {
            CallGdAssertionFailed(e);
        }

        return actual;
    }
}
