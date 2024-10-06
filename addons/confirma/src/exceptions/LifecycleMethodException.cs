using System;

namespace Confirma.Exceptions;

public class LifecycleMethodException : Exception
{
    public LifecycleMethodException(string message) : base(message) { }
}
