using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Helpers;
using Godot;

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
	private static readonly Godot.Collections.Dictionary<Variant, Variant> _godotDictionary = new()
	{
		["key"] = "value",
		[2] = 222,
		[3f] = 333f,
		[4d] = 444d,
		['5'] = '5'
	};

	#region ConfirmContainsKey
	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmContainsKey_WhenKeyExists(object key)
	{
		_dictionary.ConfirmContainsKey(key);
	}

	// Variant
	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmContainsVariantKey_WhenKeyExists(object key)
	{
		_godotDictionary.ConfirmContainsKey(key.ToVariant());
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

	// Variant
	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmContainsVariantKey_WhenKeyDoesNotExist(object key)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmContainsKey(key.ToVariant())
		);
	}
	#endregion

	#region ConfirmNotContainsKey
	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4f)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmNotContainsKey_WhenKeyDoesNotExist(object key)
	{
		_dictionary.ConfirmNotContainsKey(key);
	}

	// Variant
	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmNotContainsVariantKey_WhenKeyDoesNotExist(object key)
	{
		_godotDictionary.ConfirmNotContainsKey(key.ToVariant());
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

	// Variant
	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmNotContainsVariantKey_WhenKeyExists(object key)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmNotContainsKey(key.ToVariant())
		);
	}
	#endregion

	#region ConfirmContainsValue
	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmContainsValue_WhenValueExists(object value)
	{
		_dictionary.ConfirmContainsValue(value);
	}

	// Variant
	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmContainsVariantValue_WhenValueExists(object value)
	{
		_godotDictionary.ConfirmContainsValue(value.ToVariant());
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

	// Variant
	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmContainsVariantValue_WhenValueDoesNotExist(object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmContainsValue(value.ToVariant())
		);
	}
	#endregion

	#region ConfirmNotContainsValue
	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmNotContainsValue_WhenValueDoesNotExist(object value)
	{
		_dictionary.ConfirmNotContainsValue(value);
	}

	// Variant
	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmNotContainsVariantValue_WhenValueDoesNotExist(object value)
	{
		_godotDictionary.ConfirmNotContainsValue(value.ToVariant());
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

	// Variant
	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmNotContainsVariantValue_WhenValueExists(object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmNotContainsValue(value.ToVariant())
		);
	}
	#endregion

	#region ConfirmContainsKeyValuePair
	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmContainsKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		_dictionary.ConfirmContainsKeyValuePair(key, value);
	}

	// Variant
	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmContainsVariantKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		_godotDictionary.ConfirmContainsKeyValuePair(key.ToVariant(), value.ToVariant());
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

	// Variant
	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmContainsVariantKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmContainsKeyValuePair(key.ToVariant(), value.ToVariant())
		);
	}
	#endregion

	#region ConfirmNotContainsKeyValuePair
	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		_dictionary.ConfirmNotContainsKeyValuePair(key, value);
	}

	// Variant
	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmNotContainsVariantKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		_godotDictionary.ConfirmNotContainsKeyValuePair(key.ToVariant(), value.ToVariant());
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

	// Variant
	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmNotContainsVariantKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(
			() => _godotDictionary.ConfirmNotContainsKeyValuePair(key.ToVariant(), value.ToVariant())
		);
	}
	#endregion
}
