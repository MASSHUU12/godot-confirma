using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class VectorFormatterTest
{
    [TestCase]
    public void Format_Vector2_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector2(4.2f, 5.8f))
            .ConfirmEqual("Vector2(4.2, 5.8)");
    }

    [TestCase]
    public void Format_Vector2I_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector2I(4, 5))
            .ConfirmEqual("Vector2I(4, 5)");
    }

    [TestCase]
    public void Format_Vector3_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector3(4.2f, 5.8f, 6.9f))
            .ConfirmEqual("Vector3(4.2, 5.8, 6.9)");
    }

    [TestCase]
    public void Format_Vector3I_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector3I(4, 5, 6))
            .ConfirmEqual("Vector3I(4, 5, 6)");
    }

    [TestCase]
    public void Format_Vector4_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector4(4.2f, 5.8f, 6.9f, 2.1f))
            .ConfirmEqual("Vector4(4.2, 5.8, 6.9, 2.1)");
    }

    [TestCase]
    public void Format_Vector4I_ReturnsCorrectString()
    {
        _ = new VectorFormatter()
            .Format(new Vector4I(4, 5, 6, 2))
            .ConfirmEqual("Vector4I(4, 5, 6, 2)");
    }
}
