using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmEqualTest
{
	[TestCase(parameters: new object?[] { 1, 1 })]
	[TestCase(parameters: new object?[] { "Lorem ipsum", "Lorem ipsum" })]
	[TestCase(parameters: new object?[] { null, null })]
	[TestCase(parameters: new object?[] { 2d, 2d })]
	[TestCase(parameters: new object?[] { 2f, 2f })]
	public static void ConfirmEqual_WhenEqual(object o1, object o2)
	{
		o1.ConfirmEqual(o2);
	}

	// TODO: Create a test case for ConfirmEqual_WhenNotEqual

	[TestCase(parameters: new object?[] { 1, 2 })]
	[TestCase(parameters: new object?[] { "Lorem ipsum", "Dolor sit amet" })]
	[TestCase(parameters: new object?[] { null, 1 })]
	[TestCase(parameters: new object?[] { 2d, 3d })]
	[TestCase(parameters: new object?[] { 2f, 2 })]
	public static void ConfirmNotEqual_WhenNotEqual(object o1, object o2)
	{
		o1.ConfirmNotEqual(o2);
	}

	// TODO: Create a test case for ConfirmNotEqual_WhenEqual
}
