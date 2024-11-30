using System;
using System.Collections.Generic;

namespace Confirma.Terminal;

public class Argument
{
    public string Name { get; init; }
    public string? Value { get; private set; }

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

    public List<string> Parse(string? value)
    {
        List<string> errors = new();

        if (!AllowEmpty && string.IsNullOrEmpty(value))
        {
            errors.Add($"Value for {Name} cannot be empty.");
            return errors;
        }

        Value = IsFlag ? "true" : value;

        return errors;
    }

    public void Invoke()
    {
        _action?.Invoke(Value);
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
