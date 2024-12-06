using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Terminal;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ArgumentTests
{
    #region Parse
    [TestCase]
    public void Parse_ReturnsSuccess_ForNonFlagArgumentWithValidValue()
    {
        Argument argument = new("testArg");

        EArgumentParseResult result = argument.Parse("value", out object? parsed);

        _ = result.ConfirmEqual(EArgumentParseResult.Success);
        _ = parsed.ConfirmEqual("value");
    }

    [TestCase]
    public void Parse_ReturnsValueRequired_WhenValueIsNullAndAllowEmptyIsFalse()
    {
        Argument argument = new("testArg", allowEmpty: false);

        EArgumentParseResult result = argument.Parse(null, out object? parsed);

        _ = result.ConfirmEqual(EArgumentParseResult.ValueRequired);
        _ = parsed.ConfirmNull();
    }

    [TestCase]
    public void Parse_ReturnsUnexpectedValue_ForFlagArgumentWithValue()
    {
        Argument argument = new("flagArg", isFlag: true);

        EArgumentParseResult result = argument.Parse("unexpected", out object? parsed);

        _ = result.ConfirmEqual(EArgumentParseResult.UnexpectedValue);
        _ = parsed.ConfirmNull();
    }

    [TestCase]
    public void Parse_SetsParsedToTrue_ForFlagArgumentWithoutValue()
    {
        Argument argument = new("flagArg", isFlag: true);

        EArgumentParseResult result = argument.Parse(null, out object? parsed);

        _ = result.ConfirmEqual(EArgumentParseResult.Success);
        _ = parsed.ConfirmEqual(true);
    }
    #endregion Parse

    #region Invoke
    [TestCase]
    public void Invoke_CallsActionWithCorrectValue()
    {
        object? actionValue = null;
        void Action(object value)
        {
            actionValue = value;
        }

        Argument argument = new("testArg", action: Action);

        argument.Invoke("value");

        _ = actionValue.ConfirmEqual("value");
    }
    #endregion Invoke

    #region Equals
    [TestCase]
    public void Equals_ReturnsTrue_ForIdenticalArguments()
    {
        Argument arg1 = new("testArg");
        Argument arg2 = new("testArg");

        _ = arg1.Equals(arg2).ConfirmTrue();
    }

    [TestCase]
    public void Equals_ReturnsFalse_ForDifferentArguments()
    {
        Argument arg1 = new("testArg1");
        Argument arg2 = new("testArg2");

        _ = arg1.Equals(arg2).ConfirmFalse();
    }
    #endregion Equals

    #region GetHashCode
    [TestCase]
    public void GetHashCode_ReturnsSameValue_ForIdenticalArguments()
    {
        Argument arg1 = new("testArg");
        Argument arg2 = new("testArg");

        _ = arg1.GetHashCode().ConfirmEqual(arg2.GetHashCode());
    }
    #endregion GetHashCode
}
