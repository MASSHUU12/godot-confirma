using System;
using Confirma.Exceptions;
using Godot;

namespace Confirma.Wrappers;

public partial class WrapperBase : CSharpScript
{
    public static event Action<string>? GdAssertionFailed;

    protected static void CallGdAssertionFailed(ConfirmAssertException e)
    {
        GdAssertionFailed?.Invoke(e.InnerException?.Message ?? e.Message);
    }
}
