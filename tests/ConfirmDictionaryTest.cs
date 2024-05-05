using System;
using System.Collections.Generic;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
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
		Action action = () => _dictionary.ConfirmContainsKey(key);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("key2")]
	[TestCase(3)]
	[TestCase(4)]
	[TestCase(5d)]
	[TestCase('6')]
	public static void ConfirmContainsVariantKey_WhenKeyDoesNotExist(object key)
	{
		Action action = () => _godotDictionary.ConfirmContainsKey(key.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => _dictionary.ConfirmNotContainsKey(key);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("key")]
	[TestCase(2)]
	[TestCase(3f)]
	[TestCase(4d)]
	[TestCase('5')]
	public static void ConfirmNotContainsVariantKey_WhenKeyExists(object key)
	{
		Action action = () => _godotDictionary.ConfirmNotContainsKey(key.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => _dictionary.ConfirmContainsValue(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("value2")]
	[TestCase(69)]
	[TestCase(2137f)]
	[TestCase(420d)]
	[TestCase('0')]
	public static void ConfirmContainsVariantValue_WhenValueDoesNotExist(object value)
	{
		Action action = () => _godotDictionary.ConfirmContainsValue(value.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => _dictionary.ConfirmNotContainsValue(value);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("value")]
	[TestCase(222)]
	[TestCase(333f)]
	[TestCase(444d)]
	[TestCase('5')]
	public static void ConfirmNotContainsVariantValue_WhenValueExists(object value)
	{
		Action action = () => _godotDictionary.ConfirmNotContainsValue(value.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => _dictionary.ConfirmContainsKeyValuePair(key, value);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("kdey", "vdalue")]
	[TestCase(2, -222f)]
	[TestCase("3f", 333f)]
	[TestCase(5d, 444d)]
	[TestCase('5', '-')]
	public static void ConfirmContainsVariantKeyValuePair_WhenKeyValuePairDoesNotExist(object key, object value)
	{
		Action action = () => _godotDictionary.ConfirmContainsKeyValuePair(key.ToVariant(), value.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
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
		Action action = () => _dictionary.ConfirmNotContainsKeyValuePair(key, value);

		action.ConfirmThrows<ConfirmAssertException>();
	}

	// Variant
	[TestCase("key", "value")]
	[TestCase(2, 222)]
	[TestCase(3f, 333f)]
	[TestCase(4d, 444d)]
	[TestCase('5', '5')]
	public static void ConfirmNotContainsVariantKeyValuePair_WhenKeyValuePairExists(object key, object value)
	{
		Action action = () => _godotDictionary.ConfirmNotContainsKeyValuePair(key.ToVariant(), value.ToVariant());

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
