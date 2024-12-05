using System;
using System.Collections.Generic;
using Confirma.Extensions;
using Godot;

using static System.StringComparison;
using static Confirma.Terminal.EArgumentParseResult;

namespace Confirma.Terminal;

public class Cli
{
    private readonly string _prefix;
    private readonly Dictionary<string, Argument> _arguments = new(
        StringComparer.OrdinalIgnoreCase
    );
    private readonly Dictionary<string, object?> _argumentValues = new();

    public Cli(string prefix = "")
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

    public T? GetArgumentValue<T>(string name)
    {
        if (_argumentValues.TryGetValue(name, out object? value) && value is T typedValue)
        {
            return typedValue;
        }
        return default;
    }

    public bool IsFlagSet(string name)
    {
        return GetArgumentValue<bool>(name) is bool isSet && isSet;
    }

    public int GetValuesCount()
    {
        return _argumentValues.Count;
    }

    public bool RegisterArgument(params Argument[] arguments)
    {
        foreach (Argument argument in arguments)
        {
            string key = argument.UsePrefix
                ? _prefix + argument.Name
                : argument.Name;

            if (!_arguments.TryAdd(key, argument))
            {
                return false;
            }
        }

        return true;
    }

    public bool InvokeArgumentAction(string name)
    {
        Argument? argument = GetArgument(name);

        if (argument is null)
        {
            return false;
        }

        argument.Invoke(GetArgumentValue<object>(name)!);

        return true;
    }

    public List<string> Parse(string[] args, bool invokeActions = false)
    {
        List<string> errors = new();

        for (int i = 0; i < args.Length; i++)
        {
            (string argName, string? argValue) = ParseArgumentString(args[i]);
            string key = _prefix + argName;

            if (!_arguments.ContainsKey(key))
            {
                // Try without the prefix
                key = argName;
                if (!_arguments.ContainsKey(key))
                {
                    if (argName.StartsWith(_prefix, OrdinalIgnoreCase))
                    {
                        errors.Add(GenerateErrorForInvalidArgument(argName));
                    }
                    continue;
                }
            }

            Argument argument = _arguments[key];
            EArgumentParseResult argResult = argument.Parse(
                argValue,
                out object? parsed
            );

            if (argResult is not Success)
            {
                errors.Add(
                    GenerateErrorForArgumentParsingFailure(argument, argResult)
                );
                continue;
            }

            _argumentValues[argName] = parsed;

            if (invokeActions)
            {
                argument.Invoke(parsed!);
            }
        }

        return errors;
    }

    private string? FindSimilarArgument(string name)
    {
        const int maxDistance = 3;
        int minDistance = int.MaxValue;
        string? similarArgument = null;

        foreach (string key in _arguments.Keys)
        {
            int currentDistance = name.LevenshteinDistance(key);

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;
                similarArgument = key;
            }
        }

        return minDistance <= maxDistance
            ? similarArgument
            : null;
    }

    private string GenerateErrorForArgumentParsingFailure(
        Argument arg,
        EArgumentParseResult result
    )
    {
        string fullName = arg.UsePrefix ? _prefix + arg.Name : arg.Name;

        return result switch
        {
            Success => string.Empty,
            ValueRequired => $"Value for {fullName} cannot be empty.",
            UnexpectedValue =>
                $"{fullName} is a flag and doesn't accept any value.",
            _ => $"An error occurred while parsing {fullName}."
        };
    }

    private string GenerateErrorForInvalidArgument(string name)
    {
        string? similarArgument = FindSimilarArgument(name);

        return $"Unknown argument: {name}."
            + (
                string.IsNullOrEmpty(similarArgument)
                    ? string.Empty
                    : $" Did you mean {similarArgument}?"
            );
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
