using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Fuzz;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class FuzzGeneratorTest
{
    private static FuzzGenerator FuzzIntUniform => GetGenerator<int>();
    private static FuzzGenerator FuzzIntGaussian => GetGenerator<int>(
        EDistributionType.Gaussian
    );
    private static FuzzGenerator FuzzIntPoisson => GetGenerator<int>(
        EDistributionType.Poisson
    );
    private static FuzzGenerator FuzzIntExponential => GetGenerator<int>(
        EDistributionType.Exponential
    );

    private static FuzzGenerator FuzzFloatUniform => GetGenerator<float>();
    private static FuzzGenerator FuzzFloatGaussian => GetGenerator<float>(
        EDistributionType.Gaussian
    );
    private static FuzzGenerator FuzzFloatPoisson => GetGenerator<float>(
        EDistributionType.Poisson
    );
    private static FuzzGenerator FuzzFloatExponential => GetGenerator<float>(
        EDistributionType.Exponential
    );

    private static FuzzGenerator FuzzDoubleUniform => GetGenerator<double>();
    private static FuzzGenerator FuzzDoubleGaussian => GetGenerator<double>(
        EDistributionType.Gaussian
    );
    private static FuzzGenerator FuzzDoublePoisson => GetGenerator<double>(
        EDistributionType.Poisson
    );
    private static FuzzGenerator FuzzDoubleExponential => GetGenerator<double>(
        EDistributionType.Exponential
    );

    private static FuzzGenerator FuzzBool => GetGenerator<bool>();
    private static FuzzGenerator FuzzString => GetGenerator<string>();

    private static FuzzGenerator GetGenerator<T>(
        EDistributionType dist = EDistributionType.Uniform,
        int? seed = null
    )
    {
        return new(
            dataType: typeof(T),
            name: null,
            min: 1,
            max: 10,
            mean: 1,
            standardDeviation: 1,
            lambda: 10d,
            distribution: dist,
            seed: seed
        );
    }

    #region NextValue
    [TestCase]
    public void NextValue_Int_ReturnsIntValue()
    {
        _ = FuzzIntUniform.NextValue().ConfirmInstanceOf<int>();
    }

    [TestCase]
    public void NextValue_Double_ReturnsDoubleValue()
    {
        _ = FuzzDoubleUniform.NextValue().ConfirmInstanceOf<double>();
    }

    [TestCase]
    public void NextValue_Float_ReturnsFloatValue()
    {
        _ = FuzzFloatUniform.NextValue().ConfirmInstanceOf<float>();
    }

    [TestCase]
    public void NextValue_String_ReturnsStringValue()
    {
        _ = FuzzString.NextValue().ConfirmInstanceOf<string>();
    }

    [TestCase]
    public void NextValue_Bool_ReturnsBoolValue()
    {
        _ = FuzzBool.NextValue().ConfirmInstanceOf<bool>();
    }

    [TestCase]
    public void NextValue_UnsupportedDataType_ThrowsArgumentException()
    {
        _ = Confirm.Throws<ArgumentException>(
            static () => GetGenerator<List<int>>().NextValue()
        );
    }

    [TestCase]
    public void NextValue_Seed_ReturnsSameValue()
    {
        FuzzGenerator fuzzValue1 = GetGenerator<int>(seed: 123);
        FuzzGenerator fuzzValue2 = GetGenerator<int>(seed: 123);

        _ = fuzzValue1.NextValue().ConfirmEqual(fuzzValue2.NextValue());
    }

    [Repeat(3, IsFlaky = true)]
    [TestCase]
    public void NextValue_OutSeed_ReturnsDifferentValue()
    {
        FuzzGenerator fuzzValue1 = GetGenerator<int>();
        FuzzGenerator fuzzValue2 = GetGenerator<int>();

        _ = fuzzValue1.NextValue().ConfirmNotEqual(fuzzValue2.NextValue());
    }
    #endregion NextValue

    #region NextInt
    [TestCase]
    public void NextInt_Uniform_ReturnsValueWithinRange()
    {
        _ = ((int)FuzzIntUniform.NextValue()).ConfirmInRange(1, 10);
    }

    [TestCase]
    public void NextInt_Gaussian_ReturnsValueWithinRange()
    {
        _ = ((int)FuzzIntGaussian.NextValue()).ConfirmInRange(-20, 20);
    }

    [TestCase]
    public void NextInt_Exponential_ReturnsValueWithinRange()
    {
        _ = ((int)FuzzIntExponential.NextValue()).ConfirmGreaterThanOrEqual(0);
    }

    [Repeat(3, IsFlaky = true)]
    [TestCase]
    public void NextInt_Poisson_ReturnsValueWithinRange()
    {
        _ = ((int)FuzzIntPoisson.NextValue()).ConfirmInRange(1, 15);
    }
    #endregion NextInt

    #region NextDouble
    [TestCase]
    public void NextDouble_Uniform_ReturnsValueWithinRange()
    {
        _ = ((double)FuzzDoubleUniform.NextValue()).ConfirmInRange(1, 10);
    }

    [TestCase]
    public void NextDouble_Gaussian_ReturnsValueWithinRange()
    {
        _ = ((double)FuzzDoubleGaussian.NextValue()).ConfirmInRange(-20, 20);
    }

    [TestCase]
    public void NextDouble_Exponential_ReturnsValueWithinRange()
    {
        _ = ((double)FuzzDoubleExponential.NextValue())
            .ConfirmGreaterThanOrEqual(0);
    }

    [Repeat(3, IsFlaky = true)]
    [TestCase]
    public void NextDouble_Poisson_ReturnsValueWithinRange()
    {
        _ = ((double)FuzzDoublePoisson.NextValue()).ConfirmInRange(1, 15);
    }
    #endregion NextDouble

    #region NextFloat
    [TestCase]
    public void NextFloat_Uniform_ReturnsValueWithinRange()
    {
        _ = ((float)FuzzFloatUniform.NextValue()).ConfirmInRange(1, 10);
    }

    [TestCase]
    public void NextFloat_Gaussian_ReturnsValueWithinRange()
    {
        _ = ((float)FuzzFloatGaussian.NextValue()).ConfirmInRange(-20, 20);
    }

    [TestCase]
    public void NextFloat_Exponential_ReturnsValueWithinRange()
    {
        _ = ((float)FuzzFloatExponential.NextValue())
            .ConfirmGreaterThanOrEqual(0);
    }

    [Repeat(3, IsFlaky = true)]
    [TestCase]
    public void NextFloat_Poisson_ReturnsValueWithinRange()
    {
        _ = ((float)FuzzFloatPoisson.NextValue()).ConfirmInRange(1, 15);
    }
    #endregion NextFloat

    #region NextString
    [TestCase]
    public void NextString_ReturnsStringValueWithinRange()
    {
        _ = ((string)FuzzString.NextValue()).Length.ConfirmInRange(1, 10);
    }
    #endregion NextString
}
