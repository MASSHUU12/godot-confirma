using Godot;
using Confirma.Attributes;
using Confirma.Exceptions;

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

		Confirm.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmEqualApprox(vector2, tolerance));
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

		Confirm.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmNotEqualApprox(vector2, tolerance));
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

		Confirm.ConfirmThrows<ConfirmAssertException>(() => vector.ConfirmLessThan(vector2));
	}
	#endregion
}
