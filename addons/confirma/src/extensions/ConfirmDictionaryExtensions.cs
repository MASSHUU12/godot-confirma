using System.Collections.Generic;
using Confirma.Exceptions;
using Godot;

namespace Confirma.Extensions;

public static class ConfirmDictionaryExtensions
{
	public static IDictionary<TKey, TValue> ConfirmContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? message = null)
	{
		if (dictionary.ContainsKey(key)) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to contain key '{key}' but it did not.");
	}

	public static IDictionary<TKey, TValue> ConfirmNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string? message = null)
	{
		if (!dictionary.ContainsKey(key)) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to not contain key '{key}' but it did.");
	}

	public static IDictionary<TKey, TValue> ConfirmContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, string? message = null)
	{
		if (dictionary.Values.Contains(value)) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to contain value '{value}' but it did not.");
	}

	public static IDictionary<TKey, TValue> ConfirmNotContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value, string? message = null)
	{
		if (!dictionary.Values.Contains(value)) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to not contain value '{value}' but it did.");
	}

	public static IDictionary<TKey, TValue> ConfirmContainsKeyValuePair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value, string? message = null)
	{
		if (dictionary.TryGetValue(key, out TValue? v) && v?.Equals(value) == true) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to contain key-value pair '{key}': '{value}' but it did not.");
	}

	public static IDictionary<Variant, Variant> ConfirmContainsKeyValuePair(this IDictionary<Variant, Variant> dictionary, Variant key, Variant value, string? message = null)
	{
		if (dictionary.TryGetValue(key, out var val) && val.VariantEquals(value)) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to contain key-value pair '{key}': '{value}' but it did not.");
	}

	public static IDictionary<TKey, TValue> ConfirmNotContainsKeyValuePair<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value, string? message = null)
	{
		if (!dictionary.TryGetValue(key, out TValue? v) || v?.Equals(value) == false) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to not contain key-value pair '{key}': '{value}' but it did.");
	}

	public static IDictionary<Variant, Variant> ConfirmNotContainsKeyValuePair(this IDictionary<Variant, Variant> dictionary, Variant key, Variant value, string? message = null)
	{
		if (!dictionary.TryGetValue(key, out var val) || val.VariantEquals(value) == false) return dictionary;

		throw new ConfirmAssertException(message ?? $"Expected dictionary to not contain key-value pair '{key}': '{value}' but it did.");
	}
}
