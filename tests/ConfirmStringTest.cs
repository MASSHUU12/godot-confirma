using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmStringTest
{
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
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "not empty".ConfirmEmpty());
	}

	[TestCase]
	public static void ConfirmNotEmpty_WhenNotEmpty()
	{
		"not empty".ConfirmNotEmpty();
	}

	[TestCase]
	public static void ConfirmNotEmpty_WhenEmpty()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "".ConfirmNotEmpty());
	}

	[TestCase]
	public static void ConfirmContains_WhenContains()
	{
		"contains".ConfirmContains("tai");
	}

	[TestCase]
	public static void ConfirmContains_WhenNotContains()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "contains".ConfirmContains("not"));
	}

	[TestCase]
	public static void ConfirmNotContains_WhenNotContains()
	{
		"not contains".ConfirmNotContains("xxx");
	}

	[TestCase]
	public static void ConfirmNotContains_WhenContains()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "contains".ConfirmNotContains("tai"));
	}

	[TestCase]
	public static void ConfirmStartsWith_WhenStartsWith()
	{
		"starts with".ConfirmStartsWith("sta");
	}

	[TestCase]
	public static void ConfirmStartsWith_WhenNotStartsWith()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "starts with".ConfirmStartsWith("xxx"));
	}

	[TestCase]
	public static void ConfirmNotStartsWith_WhenNotStartsWith()
	{
		"not starts with".ConfirmNotStartsWith("xxx");
	}

	[TestCase]
	public static void ConfirmNotStartsWith_WhenStartsWith()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "starts with".ConfirmNotStartsWith("sta"));
	}

	[TestCase]
	public static void ConfirmEndsWith_WhenEndsWith()
	{
		"ends with".ConfirmEndsWith("ith");
	}

	[TestCase]
	public static void ConfirmEndsWith_WhenNotEndsWith()
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => "ends with".ConfirmEndsWith("xxx"));
	}

	[TestCase("Lorem ipsum", 11)]
	[TestCase("Lorem ipsum\n", 12)]
	public static void ConfirmHasLength_WhenHasLength(string value, int length)
	{
		value.ConfirmHasLength(length);
	}
}
