using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class RepeatAttribute : Attribute
{
    public ushort Repeat { get; init; }
    public bool FailFast { get; init; }

    public bool IsFlaky { get; init; }
    public TimeSpan Backoff { get; init; }

    public RepeatAttribute(
        ushort repeat,
        bool failFast = false,
        bool isFlaky = false,
        ushort maxRetries = 1,
        long backoff = 0
    )
    {
        if (repeat == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(repeat));
        }

        Repeat = (ushort)(repeat - 1);
        FailFast = failFast;

        IsFlaky = isFlaky;
        Backoff = backoff == 0 ? TimeSpan.Zero : new(backoff);
    }
}
