using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Confirma.Cli;

public class Cli
{
    private readonly string _prefix;
    // TODO: Consider using Dictionary.
    private readonly HashSet<Argument> _arguments = new();

    // TODO: Add support for multiple prefixes
    public Cli(string prefix)
    {
        _prefix = prefix;
    }

    public string? GetArgumentValue(string name)
    {
        Argument? argument = _arguments.FirstOrDefault(a => a.Name == name);
        return argument?.Value;
    }

    public bool RegisterArgument(Argument argument)
    {
        return _arguments.Add(argument);
    }

    public List<string> Parse(string[] args)
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
                if (!argument.Parse(argValue))
                {
                    errors.Add($"Invalid value: {argValue}.\n");
                    continue;
                }

                // TODO: Optionally invoke method when argument is found.
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
