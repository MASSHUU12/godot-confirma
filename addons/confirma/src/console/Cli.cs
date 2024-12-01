using System;
using System.Collections.Generic;
using Godot;

namespace Confirma.Terminal;

public class Cli
{
    private readonly string _prefix;
    // TODO: Consider using Dictionary.
    private readonly Dictionary<string, Argument> _arguments = new(
        StringComparer.OrdinalIgnoreCase
    );
    private readonly Dictionary<string, string?> _argumentValues = new();

    // TODO: Add support for multiple prefixes
    public Cli(string prefix)
    {
        _prefix = prefix;
    }

    public Argument? GetArgument(string name)
    {
        if (!_arguments.TryGetValue(_prefix + name, out Argument? argument))
        {
            _ = _arguments.TryGetValue(name, out argument);
        }
        return argument;
    }

    public string? GetArgumentValue(string name)
    {
        _ = _argumentValues.TryGetValue(name, out string? value);
        return value;
    }

    public int GetValuesCount()
    {
        return _argumentValues.Count;
    }

    public bool RegisterArgument(Argument argument)
    {
        string key = argument.UsePrefix ? _prefix + argument.Name : argument.Name;

        return _arguments.TryAdd(key, argument);
    }

    public bool InvokeArgumentAction(string name)
    {
        Argument? argument = GetArgument(name);

        if (argument is null)
        {
            return false;
        }

        argument.Invoke(GetArgumentValue(name));

        return true;
    }

    public List<string> Parse(string[] args, bool invokeActions = false)
    {
        List<string> errors = new();

        for (int i = 0; i < args.Length; i++)
        {
            (string argName, string? argValue) = ParseArgumentString(args[i]);
            string key = _prefix + argName;

            // Check if argument exists in the dictionary
            if (!_arguments.ContainsKey(key))
            {
                // Try without the prefix
                key = argName;
                if (!_arguments.ContainsKey(key))
                {
                    // Unknown argument
                    if (argName.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
                    {
                        // TODO: Display similar arguments.
                        errors.Add($"Unknown argument: {argName}.");
                    }
                    continue;
                }
            }

            Argument argument = _arguments[key];

            List<string> argErrors = argument.Parse(argValue, out string? parsed);

            if (argErrors.Count > 0)
            {
                errors.AddRange(argErrors);
                continue;
            }

            _argumentValues[argument.Name] = parsed;

            if (invokeActions)
            {
                argument.Invoke(parsed);
            }
        }

        return errors;
    }

    private static (string, string?) ParseArgumentString(string argument)
    {
        if (argument.Find('=') == -1)
        {
            return (argument, null);
        }

        string[] split = argument.Split('=', 2);

        return (split[0], split[1]);
    }
}
