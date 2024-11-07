using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Fuzz;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class FuzzAttributeTest
{
    [Fuzz(typeof(int), min: 1, max: 1)]
    [Fuzz(typeof(int), min: 2, max: 2)]
    public void Fuzz_MultipleInts_GeneratesCorrectly(int a, int b)
    {
        _ = (a + b).ConfirmEqual(3);
    }

    [Fuzz(typeof(double), min: 1, max: 1)]
    [Fuzz(typeof(double), min: 2, max: 2)]
    public void Fuzz_MultipleDoubles_GeneratesCorrectly(double a, double b)
    {
        _ = (a + b).ConfirmEqual(3d);
    }

    [Fuzz(typeof(float), min: 1, max: 1)]
    [Fuzz(typeof(float), min: 2, max: 2)]
    public void Fuzz_MultipleFloats_GeneratesCorrectly(float a, float b)
    {
        _ = (a + b).ConfirmEqual(3f);
    }

    [Fuzz(typeof(string), min: 1, max: 1)]
    [Fuzz(typeof(string), min: 2, max: 2)]
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
