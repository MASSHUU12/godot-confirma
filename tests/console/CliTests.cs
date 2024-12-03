using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Terminal;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class CliTests
{
    #region RegisterArgument
    [TestCase]
    public void RegisterArgument_AddsArgumentsSuccessfully()
    {
        Cli cli = new();

        Argument arg1 = new("arg1");
        Argument arg2 = new("arg2");

        _ = cli.RegisterArgument(arg1, arg2).ConfirmTrue();
        _ = arg1.ConfirmEqual(cli.GetArgument("arg1"));
        _ = arg2.ConfirmEqual(cli.GetArgument("arg2"));
    }
    #endregion RegisterArgument

    #region GetArgument
    [TestCase]
    public void GetArgument_ReturnsNull_WhenArgumentNotRegistered()
    {
        _ = new Cli().GetArgument("nonexistent").ConfirmNull();
    }
    #endregion GetArgument

    #region Parse
    [TestCase]
    public void Parse_ParsesArgumentsCorrectly()
    {
        Cli cli = new(prefix: "--");
        Argument arg1 = new("help", isFlag: true);
        Argument arg2 = new("version", isFlag: true);
        Argument arg3 = new("output", allowEmpty: false);

        _ = cli.RegisterArgument(arg1, arg2, arg3);

        string[] args = { "--help", "--output=log.txt" };

        List<string> errors = cli.Parse(args);

        _ = errors.ConfirmEmpty();
        _ = cli.IsFlagSet("--help").ConfirmTrue();
        _ = cli.IsFlagSet("--version").ConfirmFalse();
        _ = cli.GetArgumentValue("output").ConfirmEqual("log.txt");
        _ = cli.GetValuesCount().ConfirmEqual(2);
    }

    [TestCase]
    public void Parse_ReturnsErrors_ForUnknownArguments()
    {
        Cli cli = new(prefix: "--");
        Argument arg1 = new("input");

        _ = cli.RegisterArgument(arg1);

        string[] args = { "--unknown" };

        List<string> errors = cli.Parse(args);

        _ = errors.ConfirmCount(1);
        _ = errors.ConfirmContains("Unknown argument: --unknown.");
    }

    [TestCase]
    public void Parse_ReturnsErrors_ForInvalidArgumentValue()
    {
        Cli cli = new(prefix: "--");
        Argument arg1 = new("input", allowEmpty: false);

        _ = cli.RegisterArgument(arg1);

        string[] args = { "--input=" };

        List<string> errors = cli.Parse(args);

        _ = errors.ConfirmCount(1);
        _ = errors.ConfirmContains("Value for --input cannot be empty.");
    }

    [TestCase]
    public void Parse_SuggestsSimilarArgument_OnUnknownArgument()
    {
        Cli cli = new();
        Argument arg = new("start");

        _ = cli.RegisterArgument(arg);

        string[] args = { "stat" };
        List<string> errors = cli.Parse(args);

        _ = errors.ConfirmCount(1);
        _ = errors.ConfirmContains("Unknown argument: stat. Did you mean start?");
    }

    [TestCase]
    public void Parse_DoesNotInvokeActions_WhenInvokeActionsIsFalse()
    {
        Cli cli = new();
        bool actionInvoked = false;
        void Action(object value)
        {
            actionInvoked = true;
        }

        Argument arg = new("test", action: Action);
        _ = cli.RegisterArgument(arg);

        string[] args = { "test=value" };
        _ = cli.Parse(args, invokeActions: false);

        _ = actionInvoked.ConfirmFalse();
    }

    [TestCase]
    public void Parse_InvokesActions_WhenInvokeActionsIsTrue()
    {
        Cli cli = new();
        object? actionValue = null;
        void Action(object value)
        {
            actionValue = value;
        }

        Argument arg = new("test", action: Action);
        _ = cli.RegisterArgument(arg);

        string[] args = { "test=value" };

        _ = cli.Parse(args, invokeActions: true);

        _ = actionValue.ConfirmEqual("value");
    }
    #endregion Parse

    #region InvokeArgumentAction
    [TestCase]
    public void InvokeArgumentAction_InvokesActionSuccessfully()
    {
        Cli cli = new();
        object? actionValue = null;
        void Action(object value)
        {
            actionValue = value;
        }

        Argument arg = new("run", action: Action);
        _ = cli.RegisterArgument(arg);

        string[] args = { "run=test" };
        _ = cli.Parse(args);

        _ = cli.InvokeArgumentAction("run").ConfirmTrue();
        _ = actionValue.ConfirmEqual("test");
    }
    #endregion InvokeArgumentAction

    #region GetValuesCount
    [TestCase]
    public void GetValuesCount_ReturnsCorrectCount()
    {
        Cli cli = new();
        Argument arg1 = new("arg1");
        Argument arg2 = new("arg2");

        _ = cli.RegisterArgument(arg1, arg2);
        string[] args = { "arg1=value1", "arg2=value2" };

        _ = cli.Parse(args);
        _ = cli.GetValuesCount().ConfirmEqual(2);
    }
    #endregion GetValuesCount

    #region IsFlagSet
    [TestCase]
    public void IsFlagSet_ReturnsTrue_WhenFlagIsSet()
    {
        Cli cli = new();
        Argument flagArg = new("verbose", isFlag: true);

        _ = cli.RegisterArgument(flagArg);
        string[] args = { "verbose" };

        _ = cli.Parse(args);
        _ = cli.IsFlagSet("verbose").ConfirmTrue();
    }
    #endregion IsFlagSet
}
