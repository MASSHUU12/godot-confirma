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
        _ = _dictionary.ConfirmContainsKey(key);
    }

    [TestCase("key")]
    [TestCase(2)]
    [TestCase(3f)]
    [TestCase(4d)]
    [TestCase('5')]
    public static void ConfirmContainsKey_WhenKeyExists_Variant(object key)
    {
        _ = _godotDictionary.ConfirmContainsKey(key.ToVariant());
    }

    [TestCase("key2")]
    [TestCase(3)]
    [TestCase(4f)]
    [TestCase(5d)]
    [TestCase('6')]
    public static void ConfirmContainsKey_WhenKeyDoesNotExist(object key)
    {
        Action action = () => _dictionary.ConfirmContainsKey(key);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("key2")]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5d)]
    [TestCase('6')]
    public static void ConfirmContainsKey_WhenKeyDoesNotExist_Variant(object key)
    {
        Action action = () => _godotDictionary.ConfirmContainsKey(key.ToVariant());

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmContainsKey

    #region ConfirmNotContainsKey
    [TestCase("key2")]
    [TestCase(3)]
    [TestCase(4f)]
    [TestCase(5d)]
    [TestCase('6')]
    public static void ConfirmNotContainsKey_WhenKeyDoesNotExist(object key)
    {
        _ = _dictionary.ConfirmNotContainsKey(key);
    }

    [TestCase("key2")]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5d)]
    [TestCase('6')]
    public static void ConfirmNotContainsKey_WhenKeyDoesNotExist_Variant(object key)
    {
        _ = _godotDictionary.ConfirmNotContainsKey(key.ToVariant());
    }

    [TestCase("key")]
    [TestCase(2)]
    [TestCase(3f)]
    [TestCase(4d)]
    [TestCase('5')]
    public static void ConfirmNotContainsKey_WhenKeyExists(object key)
    {
        Action action = () => _dictionary.ConfirmNotContainsKey(key);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("key")]
    [TestCase(2)]
    [TestCase(3f)]
    [TestCase(4d)]
    [TestCase('5')]
    public static void ConfirmNotContainsKey_WhenKeyExists_Variant(object key)
    {
        Action action = () => _godotDictionary.ConfirmNotContainsKey(key.ToVariant());

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotContainsKey

    #region ConfirmContainsValue
    [TestCase("value")]
    [TestCase(222)]
    [TestCase(333f)]
    [TestCase(444d)]
    [TestCase('5')]
    public static void ConfirmContainsValue_WhenValueExists(object value)
    {
        _ = _dictionary.ConfirmContainsValue(value);
    }

    [TestCase("value")]
    [TestCase(222)]
    [TestCase(333f)]
    [TestCase(444d)]
    [TestCase('5')]
    public static void ConfirmContainsValue_WhenValueExists_Variant(object value)
    {
        _ = _godotDictionary.ConfirmContainsValue(value.ToVariant());
    }

    [TestCase("value2")]
    [TestCase(69)]
    [TestCase(2137f)]
    [TestCase(420d)]
    [TestCase('0')]
    public static void ConfirmContainsValue_WhenValueDoesNotExist(object value)
    {
        Action action = () => _dictionary.ConfirmContainsValue(value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("value2")]
    [TestCase(69)]
    [TestCase(2137f)]
    [TestCase(420d)]
    [TestCase('0')]
    public static void ConfirmContainsValue_WhenValueDoesNotExist_Variant(object value)
    {
        Action action = () => _godotDictionary.ConfirmContainsValue(value.ToVariant());

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmContainsValue

    #region ConfirmNotContainsValue
    [TestCase("value2")]
    [TestCase(69)]
    [TestCase(2137f)]
    [TestCase(420d)]
    [TestCase('0')]
    public static void ConfirmNotContainsValue_WhenValueDoesNotExist(object value)
    {
        _ = _dictionary.ConfirmNotContainsValue(value);
    }

    [TestCase("value2")]
    [TestCase(69)]
    [TestCase(2137f)]
    [TestCase(420d)]
    [TestCase('0')]
    public static void ConfirmNotContainsValue_WhenValueDoesNotExist_Variant(object value)
    {
        _ = _godotDictionary.ConfirmNotContainsValue(value.ToVariant());
    }

    [TestCase("value")]
    [TestCase(222)]
    [TestCase(333f)]
    [TestCase(444d)]
    [TestCase('5')]
    public static void ConfirmNotContainsValue_WhenValueExists(object value)
    {
        Action action = () => _dictionary.ConfirmNotContainsValue(value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("value")]
    [TestCase(222)]
    [TestCase(333f)]
    [TestCase(444d)]
    [TestCase('5')]
    public static void ConfirmNotContainsValue_WhenValueExists_Variant(object value)
    {
        Action action = () => _godotDictionary.ConfirmNotContainsValue(value.ToVariant());

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotContainsValue

    #region ConfirmContainsKeyValuePair
    [TestCase("key", "value")]
    [TestCase(2, 222)]
    [TestCase(3f, 333f)]
    [TestCase(4d, 444d)]
    [TestCase('5', '5')]
    public static void ConfirmContainsKeyValuePair_WhenKeyValuePairExists(
        object key,
        object value
    )
    {
        _ = _dictionary.ConfirmContainsKeyValuePair(key, value);
    }

    [TestCase("key", "value")]
    [TestCase(2, 222)]
    [TestCase(3f, 333f)]
    [TestCase(4d, 444d)]
    [TestCase('5', '5')]
    public static void ConfirmContainsKeyValuePair_WhenKeyValuePairExists_Variant(
        object key,
        object value
    )
    {
        _ = _godotDictionary.ConfirmContainsKeyValuePair(
            key.ToVariant(),
            value.ToVariant()
        );
    }

    [TestCase("kdey", "vdalue")]
    [TestCase(2, -222f)]
    [TestCase("3f", 333f)]
    [TestCase(5d, 444d)]
    [TestCase('5', '-')]
    public static void ConfirmContainsKeyValuePair_WhenKeyValuePairDoesNotExist(
        object key,
        object value
    )
    {
        Action action = () => _dictionary.ConfirmContainsKeyValuePair(key, value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("kdey", "vdalue")]
    [TestCase(2, -222f)]
    [TestCase("3f", 333f)]
    [TestCase(5d, 444d)]
    [TestCase('5', '-')]
    public static void ConfirmContainsKeyValuePair_WhenKeyValuePairDoesNotExist_Variant(
        object key,
        object value
    )
    {
        Action action = () => _godotDictionary.ConfirmContainsKeyValuePair(
            key.ToVariant(),
            value.ToVariant()
        );

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmContainsKeyValuePair

    #region ConfirmNotContainsKeyValuePair
    [TestCase("kdey", "vdalue")]
    [TestCase(2, -222f)]
    [TestCase("3f", 333f)]
    [TestCase(5d, 444d)]
    [TestCase('5', '-')]
    public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairDoesNotExist(
        object key,
        object value
    )
    {
        _ = _dictionary.ConfirmNotContainsKeyValuePair(key, value);
    }

    [TestCase("kdey", "vdalue")]
    [TestCase(2, -222f)]
    [TestCase("3f", 333f)]
    [TestCase(5d, 444d)]
    [TestCase('5', '-')]
    public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairDoesNotExist_Variant(
        object key,
        object value
    )
    {
        _ = _godotDictionary.ConfirmNotContainsKeyValuePair(
            key.ToVariant(),
            value.ToVariant()
        );
    }

    [TestCase("key", "value")]
    [TestCase(2, 222)]
    [TestCase(3f, 333f)]
    [TestCase(4d, 444d)]
    [TestCase('5', '5')]
    public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairExists(
        object key,
        object value
    )
    {
        Action action = () => _dictionary.ConfirmNotContainsKeyValuePair(key, value);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }

    [TestCase("key", "value")]
    [TestCase(2, 222)]
    [TestCase(3f, 333f)]
    [TestCase(4d, 444d)]
    [TestCase('5', '5')]
    public static void ConfirmNotContainsKeyValuePair_WhenKeyValuePairExists_Variant(
        object key,
        object value
    )
    {
        Action action = () => _godotDictionary.ConfirmNotContainsKeyValuePair(
            key.ToVariant(),
            value.ToVariant()
        );

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmNotContainsKeyValuePair
}
