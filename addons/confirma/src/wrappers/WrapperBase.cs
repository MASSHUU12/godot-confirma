using System;
using Confirma.Exceptions;
using Confirma.Types;
using Godot;

namespace Confirma.Wrappers;

public partial class WrapperBase : CSharpScript
{
    public static TestsProps Props { get; set; }
    public static event Action<string>? GdAssertionFailed;

    protected void CallGdAssertionFailed(ConfirmAssertException e)
    {
        GdAssertionFailed?.Invoke(e.InnerException?.Message ?? e.Message);
    }
}
