using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Exceptions;

namespace Confirma.Extensions;

public static class ConfirmIEnumerableExtensions
{
	public static IEnumerable<T> ConfirmEmpty<T>(this IEnumerable<T> actual, string? message = null)
	{
		if (!actual.Any()) return actual;

		throw new ConfirmAssertException(message ?? "Expected empty but was not.");
	}

	public static IEnumerable<T> ConfirmNotEmpty<T>(this IEnumerable<T> actual, string? message = null)
	{
		if (actual.Any()) return actual;

		throw new ConfirmAssertException(message ?? "Expected not empty but was.");
	}

	public static IEnumerable<T> ConfirmCount<T>(this IEnumerable<T> actual, int expected, string? message = null)
	{
		if (actual.Count() == expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected count of {expected} but was {actual.Count()}.");
	}

	public static IEnumerable<T> ConfirmCountGreaterThan<T>(this IEnumerable<T> actual, int expected, string? message = null)
	{
		if (actual.Count() > expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected count to be greater than {expected} but was {actual.Count()}.");
	}

	public static IEnumerable<T> ConfirmCountLessThan<T>(this IEnumerable<T> actual, int expected, string? message = null)
	{
		if (actual.Count() < expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected count to be less than {expected} but was {actual.Count()}.");
	}

	public static IEnumerable<T> ConfirmCountGreaterThanOrEqual<T>(this IEnumerable<T> actual, int expected, string? message = null)
	{
		if (actual.Count() >= expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected count to be greater than or equal to {expected} but was {actual.Count()}.");
	}

	public static IEnumerable<T> ConfirmCountLessThanOrEqual<T>(this IEnumerable<T> actual, int expected, string? message = null)
	{
		if (actual.Count() <= expected) return actual;

		throw new ConfirmAssertException(message ?? $"Expected count to be less than or equal to {expected} but was {actual.Count()}.");
	}

	public static IEnumerable<T> ConfirmContains<T>(this IEnumerable<T> actual, T expected, string? message = null)
	{
		if (actual.Contains(expected)) return actual;

		throw new ConfirmAssertException(message ?? $"Expected to contain '{expected}' but did not.");
	}

	public static IEnumerable<T> ConfirmNotContains<T>(this IEnumerable<T> actual, T expected, string? message = null)
	{
		if (!actual.Contains(expected)) return actual;

		throw new ConfirmAssertException(message ?? $"Expected not to contain '{expected}' but did.");
	}

	public static IEnumerable<T> ConfirmAllMatch<T>(this IEnumerable<T> actual, Func<T, bool> predicate, string? message = null)
	{
		if (actual.All(predicate)) return actual;

		throw new ConfirmAssertException(message ?? "Expected all to match but did not.");
	}

	public static IEnumerable<T> ConfirmAnyMatch<T>(this IEnumerable<T> actual, Func<T, bool> predicate, string? message = null)
	{
		if (actual.Any(predicate)) return actual;

		throw new ConfirmAssertException(message ?? "Expected any to match but did not.");
	}

	public static IEnumerable<T> ConfirmNoneMatch<T>(this IEnumerable<T> actual, Func<T, bool> predicate, string? message = null)
	{
		if (actual.All(x => !predicate(x))) return actual;

		throw new ConfirmAssertException(message ?? "Expected none to match but did.");
	}

	public static IEnumerable<T> ConfirmElementsAreUnique<T>(this IEnumerable<T> actual, string? message = null)
	{
		if (actual.Distinct().Count() == actual.Count()) return actual;

		throw new ConfirmAssertException(message ?? "Expected elements to be unique but were not.");
	}

	public static IEnumerable<T> ConfirmElementsAreDistinct<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string? message = null)
	{
		if (actual.Distinct().Count() == expected.Distinct().Count()) return actual;

		throw new ConfirmAssertException(message ?? "Expected elements to be distinct but were not.");
	}

	public static IEnumerable<T> ConfirmElementsAreOrdered<T>(this IEnumerable<T> actual, string? message = null)
	{
		if (actual.OrderBy(x => x).SequenceEqual(actual)) return actual;

		throw new ConfirmAssertException(message ?? "Expected elements to be ordered but were not.");
	}

	public static IEnumerable<T> ConfirmElementsAreInRange<T>(this IEnumerable<T> actual, T from, T to, string? message = null)
		where T : IComparable<T>
	{
		if (actual.All(x => x.CompareTo(from) >= 0 && x.CompareTo(to) <= 0)) return actual;

		throw new ConfirmAssertException(message ?? $"Expected elements to be in range [{from}, {to}] but were not.");
	}

	public static IEnumerable<T> ConfirmElementsAreEquivalent<T>(this IEnumerable<T> actual, IEnumerable<T> expected, string? message = null)
	{
		if (actual.OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x))) return actual;

		throw new ConfirmAssertException(message ?? "Expected elements to be equivalent but were not.");
	}
}
