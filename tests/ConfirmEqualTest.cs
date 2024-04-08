using Confirma.Attributes;

namespace Confirma.Tests;

[TestClass]
public static class ConfirmEqualTest
{
	[TestCase(1, 1)]
	[TestCase("Lorem ipsum", "Lorem ipsum")]
	[TestCase(null, null)]
	[TestCase(2d, 2d)]
	[TestCase(2f, 2f)]
	public static void ConfirmEqual_WhenEqual(object o1, object o2)
	{
		o1.ConfirmEqual(o2);
	}

	// TODO: Create a test case for ConfirmEqual_WhenNotEqual

	[TestCase(1, 2)]
	[TestCase("Lorem ipsum", "Dolor sit amet")]
	[TestCase(null, 1)]
	[TestCase(2d, 3d)]
	[TestCase(2f, 2)]
	public static void ConfirmNotEqual_WhenNotEqual(object o1, object o2)
	{
		o1.ConfirmNotEqual(o2);
	}

	// TODO: Create a test case for ConfirmNotEqual_WhenEqual
}
