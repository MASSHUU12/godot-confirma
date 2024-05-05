using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmStringTest
{
	#region ConfirmEmpty
	[TestCase]
	public static void ConfirmEmpty_WhenEmpty()
	{
		const string? empty = null;

		"".ConfirmEmpty();
		empty.ConfirmEmpty();
	}

	[TestCase]
	public static void ConfirmEmpty_WhenNotEmpty()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "not empty".ConfirmEmpty());
	}
	#endregion

	#region ConfirmNotEmpty
	[TestCase]
	public static void ConfirmNotEmpty_WhenNotEmpty()
	{
		"not empty".ConfirmNotEmpty();
	}

	[TestCase]
	public static void ConfirmNotEmpty_WhenEmpty()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "".ConfirmNotEmpty());
	}
	#endregion

	#region ConfirmContains
	[TestCase]
	public static void ConfirmContains_WhenContains()
	{
		"contains".ConfirmContains("tai");
	}

	[TestCase]
	public static void ConfirmContains_WhenNotContains()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "contains".ConfirmContains("not"));
	}
	#endregion

	#region ConfirmNotContains
	[TestCase]
	public static void ConfirmNotContains_WhenNotContains()
	{
		"not contains".ConfirmNotContains("xxx");
	}

	[TestCase]
	public static void ConfirmNotContains_WhenContains()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "contains".ConfirmNotContains("tai"));
	}
	#endregion

	#region ConfirmStartsWith
	[TestCase]
	public static void ConfirmStartsWith_WhenStartsWith()
	{
		"starts with".ConfirmStartsWith("sta");
	}

	[TestCase]
	public static void ConfirmStartsWith_WhenNotStartsWith()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "starts with".ConfirmStartsWith("xxx"));
	}
	#endregion

	#region ConfirmNotStartsWith
	[TestCase]
	public static void ConfirmNotStartsWith_WhenNotStartsWith()
	{
		"not starts with".ConfirmNotStartsWith("xxx");
	}

	[TestCase]
	public static void ConfirmNotStartsWith_WhenStartsWith()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "starts with".ConfirmNotStartsWith("sta"));
	}
	#endregion

	#region ConfirmEndsWith
	[TestCase]
	public static void ConfirmEndsWith_WhenEndsWith()
	{
		"ends with".ConfirmEndsWith("ith");
	}

	[TestCase]
	public static void ConfirmEndsWith_WhenNotEndsWith()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "ends with".ConfirmEndsWith("xxx"));
	}
	#endregion

	#region ConfirmNotEndsWith
	[TestCase]
	public static void ConfirmNotEndsWith_WhenNotEndsWith()
	{
		"not ends with".ConfirmNotEndsWith("xxx");
	}

	[TestCase]
	public static void ConfirmNotEndsWith_WhenEndsWith()
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => "ends with".ConfirmNotEndsWith("ith"));
	}
	#endregion

	#region ConfirmHasLength
	[TestCase("Lorem ipsum", 11)]
	[TestCase("Lorem ipsum\n", 12)]
	public static void ConfirmHasLength_WhenHasLength(string value, int length)
	{
		value.ConfirmHasLength(length);
	}

	[TestCase("Lorem ipsum", 12)]
	[TestCase("Lorem ipsum\n", 11)]
	public static void ConfirmHasLength_WhenNotHasLength(string value, int length)
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => value.ConfirmHasLength(length));
	}
	#endregion

	#region ConfirmNotHasLength
	[TestCase("Lorem ipsum", 12)]
	[TestCase("Lorem ipsum\n", 11)]
	public static void ConfirmNotHasLength_WhenNotHasLength(string value, int length)
	{
		value.ConfirmNotHasLength(length);
	}

	[TestCase("Lorem ipsum", 11)]
	[TestCase("Lorem ipsum\n", 12)]
	public static void ConfirmNotHasLength_WhenHasLength(string value, int length)
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => value.ConfirmNotHasLength(length));
	}
	#endregion

	#region ConfirmEqualsCaseInsensitive
	[TestCase("Lorem ipsum", "lorem ipsum")]
	[TestCase("Lorem ipsum", "LOREM IPSUM")]
	[TestCase("Lorem ipsum", "lOrEm IpSuM")]
	public static void ConfirmEqualsCaseInsensitive_WhenEquals(string value, string expected)
	{
		value.ConfirmEqualsCaseInsensitive(expected);
	}

	[TestCase("Lorem ipsum", "lorem")]
	[TestCase("Lorem ipsum", "ipsum")]
	[TestCase("Lorem ipsum", "lorem ipsum ")]
	public static void ConfirmEqualsCaseInsensitive_WhenNotEquals(string value, string expected)
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => value.ConfirmEqualsCaseInsensitive(expected));
	}
	#endregion

	#region ConfirmNotEqualsCaseInsensitive
	[TestCase("Lorem ipsum", "lorem")]
	[TestCase("Lorem ipsum", "ipsum")]
	[TestCase("Lorem ipsum", "lorem ipsum ")]
	public static void ConfirmNotEqualsCaseInsensitive_WhenNotEquals(string value, string expected)
	{
		value.ConfirmNotEqualsCaseInsensitive(expected);
	}

	[TestCase("Lorem ipsum", "lorem ipsum")]
	[TestCase("Lorem ipsum", "LOREM IPSUM")]
	[TestCase("Lorem ipsum", "lOrEm IpSuM")]
	public static void ConfirmNotEqualsCaseInsensitive_WhenEquals(string value, string expected)
	{
		ConfirmExceptionExtensions.ConfirmThrows<ConfirmAssertException>(() => value.ConfirmNotEqualsCaseInsensitive(expected));
	}
	#endregion
}
