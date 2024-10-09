using System;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Helpers;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class EnumHelperTest
{
    [Flags]
    private enum EEnumHelperTest : ushort
    {
        None = 0,
        Value1 = 1,
        Value2 = 1 << 1,
        Value3 = 1 << 2,
        Value4 = 1 << 3
    }

    [TestCase]
    public void TryParseFlagsEnum_ValidInput_ReturnsTrueAndParsedValue()
    {
        const string input = "Value1,Value2";

        bool result = EnumHelper.TryParseFlagsEnum(input, out EEnumHelperTest parsed);

        _ = result.ConfirmTrue();
        _ = parsed.ConfirmEqual(EEnumHelperTest.Value1 | EEnumHelperTest.Value2);
    }

    [TestCase]
    public void TryParseFlagsEnum_InvalidInput_ReturnsFalseAndDefault()
    {
        const string input = "InvalidValue";

        bool result = EnumHelper.TryParseFlagsEnum(input, out EEnumHelperTest parsed);

        _ = result.ConfirmFalse();
        _ = parsed.ConfirmEqual(EEnumHelperTest.None);
    }

    [TestCase]
    public void TryParseFlagsEnum_MultipleInvalidInputs_ReturnsFalseAndDefault()
    {
        const string input = "InvalidValue1,InvalidValue2";

        bool result = EnumHelper.TryParseFlagsEnum(input, out EEnumHelperTest parsed);

        _ = result.ConfirmFalse();
        _ = parsed.ConfirmEqual(EEnumHelperTest.None);
    }

    [TestCase]
    public void TryParseFlagsEnum_EmptyInput_ReturnsFalseAndDefault()
    {
        string input = string.Empty;

        bool result = EnumHelper.TryParseFlagsEnum(input, out EEnumHelperTest parsed);

        _ = result.ConfirmFalse();
        _ = parsed.ConfirmEqual(EEnumHelperTest.None);
    }

    [TestCase]
    public void TryParseFlagsEnum_SingleValidInput_ReturnsTrueAndParsedValue()
    {
        const string input = "Value1";

        bool result = EnumHelper.TryParseFlagsEnum(input, out EEnumHelperTest parsed);

        _ = result.ConfirmTrue();
        _ = parsed.ConfirmEqual(EEnumHelperTest.Value1);
    }
}
