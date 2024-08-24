using Confirma.Exceptions;
using Confirma.Extensions;
using Confirma.Types;
using Godot;

namespace Confirma.Wrappers;

public partial class ConfirmBooleanWrapper : CSharpScript
{
    public static TestsProps? Props { get; set; }

    public bool ConfirmTrue(bool actual, string? message = null)
    {
        try
        {
            return actual.ConfirmTrue(message);
        }
        catch (ConfirmAssertException e)
        {
            Props?.CallGdAssertionFailed(e.Message);
        }

        return actual;
    }

    public bool ConfirmFalse(bool actual, string? message = null)
    {
        try
        {
            return actual.ConfirmFalse(message);
        }
        catch (ConfirmAssertException e)
        {
            Props?.CallGdAssertionFailed(e.Message);
        }

        return actual;
    }
}
