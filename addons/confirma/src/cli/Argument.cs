using System;

namespace Confirma.Cli;

public class Argument
{
    public string Name { get; init; }
    public string? Value { get; private set; }

    public bool IsFlag { get; init; }
    public bool UsePrefix { get; init; }

    private readonly Action? _action;

    public Argument(
        string name,
        bool usePrefix,
        bool isFlag = false,
        Action? action = null
    )
    {
        Name = name;
        UsePrefix = usePrefix;
        IsFlag = isFlag;
        _action = action;
    }

    public bool Parse(string? value)
    {
        Value = IsFlag ? "true" : value;

        return true;
    }

    public void Invoke()
    {
        _action?.Invoke();
    }

    public override bool Equals(object? obj)
    {
        return obj is Argument other
            && Name == other.Name
            && UsePrefix == other.UsePrefix;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, UsePrefix);
    }
}
