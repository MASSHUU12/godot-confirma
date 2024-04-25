using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmDictionaryTest
{
	private static readonly Dictionary<object, object> _dictionary = new()
	{
		["key"] = "value",
		[2] = 222,
		[3f] = 333f,
		[4d] = 444d,
		['5'] = '5'
	};

	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmContainsKey_WhenKeyExists(object key)
	{
		_dictionary.ConfirmContainsKey(key);
	}

	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4f)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmContainsKey_WhenKeyDoesNotExist(object key)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmContainsKey(key)
		);
	}

	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4f)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmNotContainsKey_WhenKeyDoesNotExist(object key)
	{
		_dictionary.ConfirmNotContainsKey(key);
	}

	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmNotContainsKey_WhenKeyExists(object key)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmNotContainsKey(key)
		);
	}

	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmContainsValue_WhenValueExists(object value)
	{
		_dictionary.ConfirmContainsValue(value);
	}

	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmContainsValue_WhenValueDoesNotExist(object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmContainsValue(value)
		);
	}

	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmNotContainsValue_WhenValueDoesNotExist(object value)
	{
		_dictionary.ConfirmNotContainsValue(value);
	}

	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmNotContainsValue_WhenValueExists(object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmNotContainsValue(value)
		);
	}

	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmContainsKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		_dictionary.ConfirmContainsKeyValuePair(key, value);
	}

	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmContainsKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmContainsKeyValuePair(key, value)
		);
	}

	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		_dictionary.ConfirmNotContainsKeyValuePair(key, value);
	}

	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _dictionary.ConfirmNotContainsKeyValuePair(key, value)
		);
	}
}
