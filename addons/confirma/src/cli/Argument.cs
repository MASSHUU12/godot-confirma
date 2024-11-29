using System;

namespace Confirma.Cli;

public class Argument
{
    public string Name { get; init; }
    public string? Value { get; set; }
    public Action? Action { get; init; }

    public bool IsFlag { get; init; }
    public bool UsePrefix { get; init; }

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
        Action = action;
    }

    public void Invoke()
    {
        Action?.Invoke();
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
