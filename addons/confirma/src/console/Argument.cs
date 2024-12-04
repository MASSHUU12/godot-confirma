using System;

using static Confirma.Terminal.EArgumentParseResult;

namespace Confirma.Terminal;

public class Argument
{
    public string Name { get; init; }

    public bool IsFlag { get; init; }
    public bool UsePrefix { get; init; }
    public bool AllowEmpty { get; init; }

    private readonly Action<object>? _action;

    public Argument(
        string name,
        bool usePrefix = true,
        bool isFlag = false,
        bool allowEmpty = true,
        Action<object>? action = null
    )
    {
        Name = name;
        UsePrefix = usePrefix;
        IsFlag = isFlag;
        _action = action;
        AllowEmpty = allowEmpty;
    }

    public EArgumentParseResult Parse(string? value, out object? parsed)
    {
        parsed = null;

        if (IsFlag)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return UnexpectedValue;
            }
            parsed = true;
        }
        else
        {
            if (!AllowEmpty && string.IsNullOrEmpty(value))
            {
                return ValueRequired;
            }

            parsed = value;
        }

        return Success;
    }

    public void Invoke(object value)
    {
        _action?.Invoke(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Argument other
            && Name == other.Name
            && UsePrefix == other.UsePrefix
            && IsFlag == other.IsFlag
            && AllowEmpty == other.AllowEmpty;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, UsePrefix);
    }
}
