using System;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmReferenceTest
{
	#region ConfirmSameReference
	[TestCase(5)]
	[TestCase(new byte[] { 1, 2, 3 })]
	[TestCase("Lorem ipsum dolor sit amet")]
	public static void ConfirmSameReference_WhenSameReference(object obj)
	{
		obj.ConfirmSameReference(obj);
	}

	[TestCase(5, 15)]
	[TestCase(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 })]
	[TestCase("Lorem ipsum", "Lorem ipsum")]
	public static void ConfirmSameReference_WhenDifferentReferences(object obj, object obj2)
	{
		Action action = () => obj.ConfirmSameReference(obj2);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmDifferentReference
	[TestCase(5, 15)]
	[TestCase(new byte[] { 1, 2, 3 }, new byte[] { 1, 2, 3 })]
	[TestCase("Lorem ipsum", "Lorem ipsum")]
	public static void ConfirmDifferentReference_WhenDifferentReferences(object obj, object obj2)
	{
		obj.ConfirmDifferentReference(obj2);
	}

	[TestCase(5)]
	[TestCase(new byte[] { 1, 2, 3 })]
	[TestCase("Lorem ipsum dolor sit amet")]
	public static void ConfirmDifferentReference_WhenSameReference(object obj)
	{
		Action action = () => obj.ConfirmDifferentReference(obj);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
