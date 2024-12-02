using System;

using static Confirma.Terminal.EArgumentParseResult;

namespace Confirma.Terminal;

public class Argument
{
    public string Name { get; init; }

    public bool IsFlag { get; init; }
    public bool UsePrefix { get; init; }
    public bool AllowEmpty { get; init; }

    private readonly Action<string?>? _action;

    public Argument(
        string name,
        bool usePrefix = true,
        bool isFlag = false,
        bool allowEmpty = true,
        Action<string?>? action = null
    )
    {
        Name = name;
        UsePrefix = usePrefix;
        IsFlag = isFlag;
        _action = action;
        AllowEmpty = allowEmpty;
    }

    public EArgumentParseResult Parse(string? value, out string? parsed)
    {
        parsed = null;

        if (!AllowEmpty && string.IsNullOrEmpty(value))
        {
            return ValueRequired;
        }

        if (IsFlag && !string.IsNullOrEmpty(value))
        {
            return UnexpectedValue;
        }

        // TODO: Find a better way to indicate that the flag is set.
        parsed = IsFlag ? "true" : value;

        return Success;
    }

    public void Invoke(string? value)
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
