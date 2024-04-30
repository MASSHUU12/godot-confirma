using Confirma.Exceptions;
using Godot;

namespace Confirma;

public static class ConfirmVectorExtension
{
	#region ConfirmEqualApprox
	public static void ConfirmEqualApprox(this Vector2 vector, Vector2 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected {expected}, but got {vector}");
	}

	public static void ConfirmEqualApprox(this Vector3 vector, Vector3 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected {expected}, but got {vector}");
	}

	public static void ConfirmEqualApprox(this Vector4 vector, Vector4 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected {expected}, but got {vector}");
	}
	#endregion

	#region ConfirmNotEqualApprox
	public static void ConfirmNotEqualApprox(this Vector2 vector, Vector2 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (!vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected not {expected}, but got {vector}");
	}

	public static void ConfirmNotEqualApprox(this Vector3 vector, Vector3 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (!vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected not {expected}, but got {vector}");
	}

	public static void ConfirmNotEqualApprox(this Vector4 vector, Vector4 expected, float tolerance = 0.0001f, string? message = null)
	{
		if (!vector.IsEqualApprox(expected, tolerance)) return;

		throw new ConfirmAssertException(message ?? $"Expected not {expected}, but got {vector}");
	}
	#endregion

	#region ConfirmLessThan
	public static void ConfirmLessThan(this Vector2 vector, Vector2 expected, string? message = null)
	{
		if (vector < expected) return;

		throw new ConfirmAssertException(message ?? $"Expected {vector} to be less than {expected}");
	}

	public static void ConfirmLessThan(this Vector3 vector, Vector3 expected, string? message = null)
	{
		if (vector < expected) return;

		throw new ConfirmAssertException(message ?? $"Expected {vector} to be less than {expected}");
	}

	public static void ConfirmLessThan(this Vector4 vector, Vector4 expected, string? message = null)
	{
		if (vector < expected) return;

		throw new ConfirmAssertException(message ?? $"Expected {vector} to be less than {expected}");
	}
	#endregion
}
