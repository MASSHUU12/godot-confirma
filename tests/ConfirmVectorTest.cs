using Godot;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmVectorTest
{
	#region ConfirmEqualApprox
	[TestCase(1f, 2f, 1f, 2f, 0.0001f)]
	[TestCase(1f, 2f, 1.00001f, 2.00001f, 0.0001f)]
	[TestCase(1f, 1f, 1.3f, 1.3f, 0.5f)]
	public static void ConfirmEqualApprox_WhenEqual(float a, float b, float c, float d, float tolerance)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmEqualApprox(vector2, tolerance);
	}

	[TestCase(1f, 2f, 1.1f, 2.1f, 0.0001f)]
	[TestCase(1f, 2f, 1.0001f, 2.0001f, 0.00001f)]
	[TestCase(1f, 1f, 1.3f, 1.3f, 0.1f)]
	public static void ConfirmEqualApprox_WhenNotEqual(float a, float b, float c, float d, float tolerance)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmEqualApprox(vector2, tolerance));
	}
	#endregion

	#region ConfirmNotEqualApprox
	[TestCase(1f, 2f, 1.1f, 2.1f, 0.0001f)]
	[TestCase(1f, 2f, 1.0001f, 2.0001f, 0.00001f)]
	[TestCase(1f, 1f, 1.3f, 1.3f, 0.1f)]
	public static void ConfirmNotEqualApprox_WhenNotEqual(float a, float b, float c, float d, float tolerance)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmNotEqualApprox(vector2, tolerance);
	}

	[TestCase(1f, 2f, 1f, 2f, 0.0001f)]
	[TestCase(1f, 2f, 1.00001f, 2.00001f, 0.0001f)]
	[TestCase(1f, 1f, 1.3f, 1.3f, 0.5f)]
	public static void ConfirmNotEqualApprox_WhenEqual(float a, float b, float c, float d, float tolerance)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmNotEqualApprox(vector2, tolerance));
	}
	#endregion

	#region ConfirmLessThan
	[TestCase(-5f, -5f, 0f, -2f)]
	[TestCase(1f, 2f, 1.1f, 2.1f)]
	[TestCase(1f, 2f, 1.0001f, 2.0001f)]
	public static void ConfirmLessThan_WhenLessThan(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmLessThan(vector2);
	}

	[TestCase(1f, 2f, -5f, -5f)]
	[TestCase(1f, 2f, 1f, 2f)]
	[TestCase(1.00001f, 2.00001f, 1f, 2f)]
	public static void ConfirmLessThan_WhenNotLessThan(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmLessThan(vector2));
	}
	#endregion

	#region ConfirmLessThanOrEqual
	[TestCase(1f, 2f, 1f, 2f)]
	[TestCase(-5f, -5f, 0f, -2f)]
	[TestCase(1.1f, 2f, 1.1f, 2.1f)]
	[TestCase(1f, 2f, 1.0001f, 2.0001f)]
	public static void ConfirmLessThanOrEqual_WhenLessThanOrEqual(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmLessThanOrEqual(vector2);
	}

	[TestCase(1f, 2f, -5f, -5f)]
	[TestCase(1.1f, 2.1f, 1f, 2f)]
	[TestCase(1.00001f, 2.00001f, 1f, 2f)]
	public static void ConfirmLessThanOrEqual_WhenNotLessThanOrEqual(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmLessThanOrEqual(vector2));
	}
	#endregion

	#region ConfirmGreaterThan
	[TestCase(0f, -2f, -5f, -5f)]
	[TestCase(1.1f, 2.1f, 1f, 2f)]
	[TestCase(1.0001f, 2.0001f, 1f, 2f)]
	public static void ConfirmGreaterThan_WhenGreaterThan(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmGreaterThan(vector2);
	}

	[TestCase(-5f, -5f, 0f, -2f)]
	[TestCase(1f, 2f, 1.1f, 2.1f)]
	[TestCase(1f, 2f, 1.0001f, 2.0001f)]
	public static void ConfirmGreaterThan_WhenNotGreaterThan(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmGreaterThan(vector2));
	}
	#endregion

	#region ConfirmGreaterThanOrEqual
	[TestCase(0f, -2f, -5f, -5f)]
	[TestCase(1f, 2f, 1f, 2f)]
	[TestCase(1f, 2.1f, 1f, 2f)]
	public static void ConfirmGreaterThanOrEqual_WhenGreaterThanOrEqual(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		vector.ConfirmGreaterThanOrEqual(vector2);
	}

	[TestCase(-5f, -5f, 0f, -2f)]
	[TestCase(1.1f, 2.1f, 1.1f, 5f)]
	[TestCase(1.00001f, 0f, 1.0001f, 2f)]
	public static void ConfirmGreaterThanOrEqual_WhenNotGreaterThanOrEqual(float a, float b, float c, float d)
	{
		var vector = new Vector2(a, b);
		var vector2 = new Vector2(c, d);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmGreaterThanOrEqual(vector2));
	}
	#endregion

	#region ConfirmBetween
	[TestCase(0.5f, 0.5f, 0f, 0f, 1f, 1f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 1f, 1f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f)]
	public static void ConfirmBetween_WhenBetween(float a, float b, float c, float d, float e, float f)
	{
		var vector = new Vector2(a, b);
		var min = new Vector2(c, d);
		var max = new Vector2(e, f);

		vector.ConfirmBetween(min, max);
	}

	[TestCase(0.5f, 0.5f, 0f, 0f, 0.4f, 0.4f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f)]
	[TestCase(-0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.6f)]
	public static void ConfirmBetween_WhenNotBetween(float a, float b, float c, float d, float e, float f)
	{
		var vector = new Vector2(a, b);
		var min = new Vector2(c, d);
		var max = new Vector2(e, f);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmBetween(min, max));
	}
	#endregion

	#region ConfirmNotBetween
	[TestCase(0.5f, 0.5f, 0f, 0f, 0.4f, 0.4f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.4f, 0.4f)]
	[TestCase(-0.5f, 0.5f, 0.5f, 0.5f, 0.6f, 0.6f)]
	public static void ConfirmNotBetween_WhenNotBetween(float a, float b, float c, float d, float e, float f)
	{
		var vector = new Vector2(a, b);
		var min = new Vector2(c, d);
		var max = new Vector2(e, f);

		vector.ConfirmNotBetween(min, max);
	}

	[TestCase(0.5f, 0.5f, 0f, 0f, 1f, 1f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 1f, 1f)]
	[TestCase(0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f)]
	public static void ConfirmNotBetween_WhenBetween(float a, float b, float c, float d, float e, float f)
	{
		var vector = new Vector2(a, b);
		var min = new Vector2(c, d);
		var max = new Vector2(e, f);

		ConfirmExtensions.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmNotBetween(min, max));
	}
	#endregion
}
