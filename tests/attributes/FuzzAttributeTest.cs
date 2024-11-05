using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Fuzz;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class FuzzAttributeTest
{
    [Fuzz(typeof(int), minValue: 1, maxValue: 1)]
    [Fuzz(typeof(int), minValue: 2, maxValue: 2)]
    public void Fuzz_MultipleInts_GeneratesCorrectly(int a, int b)
    {
        _ = (a + b).ConfirmEqual(3);
    }

    [Fuzz(typeof(double), minValue: 1, maxValue: 1)]
    [Fuzz(typeof(double), minValue: 2, maxValue: 2)]
    public void Fuzz_MultipleDoubles_GeneratesCorrectly(double a, double b)
    {
        _ = (a + b).ConfirmEqual(3d);
    }

    [Fuzz(typeof(float), minValue: 1, maxValue: 1)]
    [Fuzz(typeof(float), minValue: 2, maxValue: 2)]
    public void Fuzz_MultipleFloats_GeneratesCorrectly(float a, float b)
    {
        _ = (a + b).ConfirmEqual(3f);
    }

    [Fuzz(typeof(string), minValue: 1, maxValue: 1)]
    [Fuzz(typeof(string), minValue: 2, maxValue: 2)]
    public void Fuzz_MultipleStrings_GeneratesCorrectly(string a, string b)
    {
        _ = (a + b).Length.ConfirmEqual(3);
    }

    [Fuzz(typeof(bool))]
    [Fuzz(typeof(bool))]
    public void Fuzz_MultipleBools_GeneratesCorrectly(bool a, bool b)
    {
        // Note: XD
        _ = a.ConfirmEqual(a);
        _ = b.ConfirmEqual(b);
    }
}
