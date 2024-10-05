using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmVectorTest
{
    #region ConfirmEqualApprox
    [TestCase(1f, 2f, 1f, 2f, 0.0001f)]
    [TestCase(1f, 2f, 1.00001f, 2.00001f, 0.0001f)]
    [TestCase(1f, 1f, 1.3f, 1.3f, 0.5f)]
    public static void ConfirmEqualApprox_WhenEqual(
        float a,
        float b,
        float c,
        float d,
        float tolerance
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmEqualApprox(vector2, tolerance);
    }

    [TestCase(1f, 2f, 1.1f, 2.1f, 0.0001f)]
    [TestCase(1f, 2f, 1.0001f, 2.0001f, 0.00001f)]
    [TestCase(1f, 1f, 1.3f, 1.3f, 0.1f)]
    public static void ConfirmEqualApprox_WhenNotEqual(
        float a,
        float b,
        float c,
        float d,
        float tolerance
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmEqualApprox(vector2, tolerance);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmEqualApprox failed: "
            + $"Expected Vector2{vector} to be equal Vector2{vector2}."
        );
    }
    #endregion ConfirmEqualApprox

    #region ConfirmNotEqualApprox
    [TestCase(1f, 2f, 1.1f, 2.1f, 0.0001f)]
    [TestCase(1f, 2f, 1.0001f, 2.0001f, 0.00001f)]
    [TestCase(1f, 1f, 1.3f, 1.3f, 0.1f)]
    public static void ConfirmNotEqualApprox_WhenNotEqual(
        float a,
        float b,
        float c,
        float d,
        float tolerance
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmNotEqualApprox(vector2, tolerance);
    }

    [TestCase(1f, 2f, 1f, 2f, 0.0001f)]
    [TestCase(1f, 2f, 1.00001f, 2.00001f, 0.0001f)]
    [TestCase(1f, 1f, 1.3f, 1.3f, 0.5f)]
    public static void ConfirmNotEqualApprox_WhenEqual(
        float a,
        float b,
        float c,
        float d,
        float tolerance
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmNotEqualApprox(vector2, tolerance);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotEqualApprox failed: "
            + $"Expected Vector2{vector} not to be equal Vector2{vector2}."
        );
    }
    #endregion ConfirmNotEqualApprox

    #region ConfirmLessThan
    [TestCase(-5f, -5f, 0f, -2f)]
    [TestCase(1f, 2f, 1.1f, 2.1f)]
    [TestCase(1f, 2f, 1.0001f, 2.0001f)]
    public static void ConfirmLessThan_WhenLessThan(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmLessThan(vector2);
    }

    [TestCase(1f, 2f, -5f, -5f)]
    [TestCase(1f, 2f, 1f, 2f)]
    [TestCase(1.00001f, 2.00001f, 1f, 2f)]
    public static void ConfirmLessThan_WhenNotLessThan(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmLessThan(vector2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmLessThan failed: "
            + $"Expected Vector2{vector} to be less than Vector2{vector2}."
        );
    }
    #endregion ConfirmLessThan

    #region ConfirmLessThanOrEqual
    [TestCase(1f, 2f, 1f, 2f)]
    [TestCase(-5f, -5f, 0f, -2f)]
    [TestCase(1.1f, 2f, 1.1f, 2.1f)]
    [TestCase(1f, 2f, 1.0001f, 2.0001f)]
    public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmLessThanOrEqual(vector2);
    }

    [TestCase(1f, 2f, -5f, -5f)]
    [TestCase(1.1f, 2.1f, 1f, 2f)]
    [TestCase(1.00001f, 2.00001f, 1f, 2f)]
    public static void ConfirmLessThanOrEqual_WhenNotLessThanOrEqual(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmLessThanOrEqual(vector2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmLessThanOrEqual failed: "
            + $"Expected Vector2{vector} to be less than or equal Vector2{vector2}."
        );
    }
    #endregion ConfirmLessThanOrEqual

    #region ConfirmGreaterThan
    [TestCase(0f, -2f, -5f, -5f)]
    [TestCase(1.1f, 2.1f, 1f, 2f)]
    [TestCase(1.0001f, 2.0001f, 1f, 2f)]
    public static void ConfirmGreaterThan_WhenGreaterThan(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmGreaterThan(vector2);
    }

    [TestCase(-5f, -5f, 0f, -2f)]
    [TestCase(1f, 2f, 1.1f, 2.1f)]
    [TestCase(1f, 2f, 1.0001f, 2.0001f)]
    public static void ConfirmGreaterThan_WhenNotGreaterThan(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmGreaterThan(vector2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmGreaterThan failed: "
            + $"Expected Vector2{vector} to be greater than Vector2{vector2}."
        );
    }
    #endregion ConfirmGreaterThan

    #region ConfirmGreaterThanOrEqual
    [TestCase(0f, -2f, -5f, -5f)]
    [TestCase(1f, 2f, 1f, 2f)]
    [TestCase(1f, 2.1f, 1f, 2f)]
    public static void ConfirmGreaterThanOrEqual_WhenGreaterThanOrEqual(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        _ = vector.ConfirmGreaterThanOrEqual(vector2);
    }

    [TestCase(-5f, -5f, 0f, -2f)]
    [TestCase(1.1f, 2.1f, 1.1f, 5f)]
    [TestCase(1.00001f, 0f, 1.0001f, 2f)]
    public static void ConfirmGreaterThanOrEqual_WhenNotGreaterThanOrEqual(
        float a,
        float b,
        float c,
        float d
    )
    {
        Vector2 vector = new(a, b);
        Vector2 vector2 = new(c, d);

        Action action = () => vector.ConfirmGreaterThanOrEqual(vector2);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmGreaterThanOrEqual failed: "
            + $"Expected Vector2{vector} to be greater than or equal Vector2{vector2}."
        );
    }
    #endregion ConfirmGreaterThanOrEqual

    #region ConfirmBetween
    [TestCase(0.5f, 0.5f, 0f, 0f, 1f, 1f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 1f, 1f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f)]
    public static void ConfirmBetween_WhenBetween(
        float a,
        float b,
        float c,
        float d,
        float e,
        float f
    )
    {
        Vector2 vector = new(a, b);
        Vector2 min = new(c, d);
        Vector2 max = new(e, f);

        _ = vector.ConfirmBetween(min, max);
    }

    [TestCase(0.5f, 0.5f, 0f, 0f, 0.4f, 0.4f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f)]
    [TestCase(-0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.6f)]
    public static void ConfirmBetween_WhenNotBetween(
        float a,
        float b,
        float c,
        float d,
        float e,
        float f
    )
    {
        Vector2 vector = new(a, b);
        Vector2 min = new(c, d);
        Vector2 max = new(e, f);

        Action action = () => vector.ConfirmBetween(min, max);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmBetween failed: "
            + $"Expected Vector2{vector} to be between Vector2{min} and Vector2{max}."
        );
    }
    #endregion ConfirmBetween

    #region ConfirmNotBetween
    [TestCase(0.5f, 0.5f, 0f, 0f, 0.4f, 0.4f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f)]
    [TestCase(-0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.6f)]
    public static void ConfirmNotBetween_WhenNotBetween(
        float a,
        float b,
        float c,
        float d,
        float e,
        float f
    )
    {
        Vector2 vector = new(a, b);
        Vector2 min = new(c, d);
        Vector2 max = new(e, f);

        _ = vector.ConfirmNotBetween(min, max);
    }

    [TestCase(0.5f, 0.5f, 0f, 0f, 1f, 1f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 1f, 1f)]
    [TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f)]
    public static void ConfirmNotBetween_WhenBetween(
        float a,
        float b,
        float c,
        float d,
        float e,
        float f
    )
    {
        Vector2 vector = new(a, b);
        Vector2 min = new(c, d);
        Vector2 max = new(e, f);

        Action action = () => vector.ConfirmNotBetween(min, max);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmNotBetween failed: "
            + $"Expected Vector2{vector} not to be between Vector2{min} and Vector2{max}."
        );
    }
    #endregion ConfirmNotBetween
}
