using System.Reflection;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class MethodInfoExtensionsTest
{
    private readonly MethodInfo _methodWithoutParams =
        typeof(MethodInfoExtensionsTest)
        .GetMethod(nameof(MethodWithoutParams))!;

    private readonly MethodInfo _methodWithParams =
        typeof(MethodInfoExtensionsTest)
        .GetMethod(nameof(MethodWithParams))!;

    private readonly MethodInfo _methodWithParamsMethodWithParamsAndOtherArgs =
        typeof(MethodInfoExtensionsTest)
        .GetMethod(nameof(MethodWithParamsAndOtherArgs))!;

    public void MethodWithoutParams() { }

    public void MethodWithParams(params object[] args) { }

    public void MethodWithParamsAndOtherArgs(object arg1, params object[] args) { }

    #region IsUsingParamsModifier
    [TestCase]
    public void IsUsingParamsModifier_NoParams_ReturnsFalse()
    {
        _ = _methodWithoutParams.IsUsingParamsModifier().ConfirmFalse();
    }

    [TestCase]
    public void IsUsingParamsModifier_WithParams_ReturnsTrue()
    {
        _ = _methodWithParams.IsUsingParamsModifier().ConfirmTrue();
    }

    [TestCase]
    public void IsUsingParamsModifier_MethodWithParamsAndOtherArgs_ReturnsTrue()
    {
        _ = _methodWithParamsMethodWithParamsAndOtherArgs
            .IsUsingParamsModifier().ConfirmTrue();
    }
    #endregion IsUsingParamsModifier

    #region GetParamsArgument
    [TestCase]
    public void GetParamsArgument_NoParams_ReturnsNull()
    {
        _ = _methodWithoutParams.GetParamsArgument().ConfirmNull();
    }

    [TestCase]
    public void GetParamsArgument_WithParams_ReturnsParameterInfo()
    {
        _ = _methodWithParams.GetParamsArgument()
            .ConfirmInstanceOf<ParameterInfo>();
    }

    [TestCase]
    public void GetParamsArgument_MethodWithParamsAndOtherArgs_ReturnsParameterInfo()
    {
        _ = _methodWithParamsMethodWithParamsAndOtherArgs.GetParamsArgument()
            .ConfirmInstanceOf<ParameterInfo>();
    }
    #endregion GetParamsArgument
}
