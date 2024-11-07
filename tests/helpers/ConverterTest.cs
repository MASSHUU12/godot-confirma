using System;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Extensions;
using Confirma.Helpers;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConverterTest
{
    [TestCase]
    public void ToVariant_Null_ReturnsDefaultVariant()
    {
        Variant result = Converter.ToVariant(null);

        _ = result.ConfirmEqual(default);
    }

    [TestCase(true)]
    [TestCase(123L)]
    [TestCase(3.14d)]
    [TestCase("Hello")]
    public void ToVariant_Value_ReturnsValueVariant(object value)
    {
        _ = value.ConfirmEqual(value.ToVariant().Obj);
    }

    [TestCase]
    public void ToVariant_Int32_ReturnsInt32Variant()
    {
        _ = 42.ConfirmEqual(42.ToVariant().AsInt32());
    }

    [TestCase]
    public void ToVariant_Float_ReturnsFloatVariant()
    {
        _ = 6.283f.ConfirmEqual(6.283f.ToVariant().AsSingle());
    }

    [TestCase]
    public void ToVariant_Char_ReturnsCharVariant()
    {
        _ = 'a'.ConfirmEqual('a'.ToVariant().AsChar());
    }

    [TestCase]
    public void ToVariant_ByteArray_Returns_ByteArrayVariant()
    {
        byte[] arr = new byte[] { 1, 2, 3 };

        _ = arr.ConfirmEqual(arr.ToVariant().AsByteArray());
    }

    [TestCase]
    public void ToVariant_Int32Array_Returns_Int32ArrayVariant()
    {
        int[] arr = new int[] { 3, 2, 1 };

        _ = arr.ConfirmEqual(arr.ToVariant().AsInt32Array());
    }

    [TestCase]
    public void ToVariant_Int64Array_Returns_Int64ArrayVariant()
    {
        long[] arr = new long[] { 1L, 2L, 3L };

        _ = arr.ConfirmEqual(arr.ToVariant().AsInt64Array());
    }

    [TestCase]
    public void ToVariant_FloatArray_Returns_FloatArrayVariant()
    {
        float[] arr = new float[] { 0f, 0.1f, 0.2f };

        _ = arr.ConfirmEqual(arr.ToVariant().AsFloat32Array());
    }

    [TestCase]
    public void ToVariant_DoubleArray_Returns_DoubleArrayVariant()
    {
        double[] arr = new double[] { 5d, 4.99d, 4.98d };

        _ = arr.ConfirmEqual(arr.ToVariant().AsFloat64Array());
    }

    [TestCase]
    public void ToVariant_Vector2_ReturnsVector2Variant()
    {
        _ = new Vector2(1, 2).ConfirmEqual(
            new Vector2(1, 2).ToVariant().AsVector2()
        );
    }

    [TestCase]
    public void ToVariant_Vector2Array_ReturnsVector2ArrayVariant()
    {
        _ = new Vector2[] { new(1, 2), new(3, 4) }.ConfirmEqual(
            new Vector2[] { new(1, 2), new(3, 4) }
                .ToVariant()
                .AsVector2Array()
        );
    }

    [TestCase]
    public void ToVariant_UnknownType_ThrowsNotSupportedException()
    {
        _ = Confirm.Throws<NotSupportedException>(
            static () => Converter.ToVariant(new object())
        );
    }
}
