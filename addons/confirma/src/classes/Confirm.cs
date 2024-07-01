using System;
using Confirma.Exceptions;

namespace Confirma.Classes;

public static class Confirm
{
	#region IsEnumValue
	public static int IsEnumValue<T>(int value, string? message = null)
	where T : struct, Enum
	{
		foreach (int v in Enum.GetValues(typeof(T))) if (v == value) return value;

		throw new ConfirmAssertException(
			message ??
			$"Expected {value} to be {typeof(T).Name} enum value."
		);
	}

	public static int IsNotEnumValue<T>(int value, string? message = null)
	where T : struct, Enum
	{
		try
		{
			IsEnumValue<T>(value);
		}
		catch (ConfirmAssertException)
		{
			return value;
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected {value} not to be {typeof(T).Name} enum value."
		);
	}
	#endregion

	#region IsEnumName
	public static string IsEnumName<T>(string name, bool ignoreCase = false, string? message = null)
	where T : struct, Enum
	{
		foreach (string v in Enum.GetNames(typeof(T)))
		{
			var n = v;

			if (ignoreCase)
			{
				n = n.ToLower();
				name = name.ToLower();
			}

			if (n == name) return name;
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected {name} to be {typeof(T).Name} enum name."
		);
	}

	public static string IsNotEnumName<T>(string name, string? message = null)
	where T : struct, Enum
	{
		try
		{
			IsEnumName<T>(name);
		}
		catch (ConfirmAssertException)
		{
			return name;
		}

		throw new ConfirmAssertException(
			message ??
			$"Expected {name} not to be {typeof(T).Name} enum name."
		);
	}
	#endregion
}
