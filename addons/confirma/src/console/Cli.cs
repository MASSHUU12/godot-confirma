using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Confirma.Terminal;

public class Cli
{
    private readonly string _prefix;
    // TODO: Consider using Dictionary.
    private readonly HashSet<Argument> _arguments = new();
    private readonly Dictionary<string, string?> _argumentValues = new();

    // TODO: Add support for multiple prefixes
    public Cli(string prefix)
    {
        _prefix = prefix;
    }

    public Argument? GetArgument(string name)
    {
        return _arguments.FirstOrDefault(a => a.Name == name);
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
        return _arguments.Add(argument);
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

            Argument? argument = _arguments.FirstOrDefault(a =>
                string.Equals(
                    a.UsePrefix ? _prefix + a.Name : a.Name,
                    argName,
                    StringComparison.OrdinalIgnoreCase
                )
            );

            if (argument is not null)
            {
                List<string> argErrors = argument.Parse(
                    argValue,
                    out string? parsed
                );

                if (argErrors.Count > 0)
                {
                    errors.AddRange(argErrors);
                    continue;
                }

                _argumentValues[argument.Name] = parsed;

                if (invokeActions)
                {
                    argument.Invoke(argValue);
                }
            }
            else
            {
                if (!argName.StartsWith(
                    _prefix,
                    StringComparison.OrdinalIgnoreCase
                ))
                {
                    continue;
                }

                // TODO: Display similar arguments.
                errors.Add($"Unknown argument: {argName}.\n");
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
