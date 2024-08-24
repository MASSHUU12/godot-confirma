using Confirma.Extensions;
using Godot;

namespace Confirma.Wrappers;

public partial class ConfirmBooleanWrapper : CSharpScript
{
    public bool ConfirmTrue(bool actual, string? message = null)
    {
        return actual.ConfirmTrue(message);
    }

    public bool ConfirmFalse(bool actual, string? message = null)
    {
        return actual.ConfirmFalse(message);
    }
}
