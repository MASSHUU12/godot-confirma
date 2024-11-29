using System.Collections.Generic;
using System.Linq;
using Confirma.Helpers;
using Godot;

namespace Confirma.Cli;

public class Cli
{
    private readonly string _prefix;
    private readonly HashSet<Argument> _arguments = new();

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

    public void Parse(string[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            (string argName, string? argValue) = ParseArgumentString(args[i]);

            Argument? argument = _arguments.FirstOrDefault(a =>
                (a.UsePrefix ? _prefix + a.Name : a.Name) == argName
            );

            if (argument is not null)
            {
                argument.Value = argument.IsFlag ? "true" : argValue;
            }
            else
            {
                if (!argName.StartsWith(_prefix))
                {
                    continue;
                }

                Log.PrintError($"Unknown argument: {argName}.\n");
            }
        }
    }

    private static (string, string?) ParseArgumentString(string argument)
    {
        if (argument.Find('=') == -1)
        {
            return (argument, null);
        }

        string[] split = argument.Split('=');

        return (split[0], split[1]);
    }
}
