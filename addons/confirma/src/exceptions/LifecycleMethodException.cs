using System;
using System.Runtime.Serialization;

namespace Confirma.Exceptions;

public class LifecycleMethodException : Exception
{
    public LifecycleMethodException() { }

    public LifecycleMethodException(string message) : base(message) { }

    public LifecycleMethodException(
        string? message,
        Exception? innerException
    ) : base(message, innerException) { }

    protected LifecycleMethodException(
        SerializationInfo info,
        StreamingContext context
    ) : base(info, context) { }
}
