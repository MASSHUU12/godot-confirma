using Confirma.Attributes;
using Confirma.Exceptions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable()]
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

	[TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hello,", "world!" })]
	public static void ConfirmEqual_WhenArrEqual(object[] o1, object[] o2)
	{
		o1.ConfirmEqual(o2);
	}

	[TestCase(1, 2)]
	[TestCase("Lorem ipsum", "Dolor sit amet")]
	[TestCase(2d, 3d)]
	[TestCase(2f, 2)]
	public static void ConfirmEqual_WhenNotEqual(object o1, object o2)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => o1.ConfirmEqual(o2));
	}

	[TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hi,", "world!" })]
	public static void ConfirmEqual_WhenArrNotEqual(object[] o1, object[] o2)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => o1.ConfirmEqual(o2));
	}

	[TestCase(1, 2)]
	[TestCase("Lorem ipsum", "Dolor sit amet")]
	[TestCase(null, 1)]
	[TestCase(2d, 3d)]
	[TestCase(2f, 2)]
	public static void ConfirmNotEqual_WhenNotEqual(object o1, object o2)
	{
		o1.ConfirmNotEqual(o2);
	}

	[TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hi,", "world!" })]
	public static void ConfirmNotEqual_WhenArrNotEqual(object[] o1, object[] o2)
	{
		o1.ConfirmNotEqual(o2);
	}

	[TestCase(1, 1)]
	[TestCase("Lorem ipsum", "Lorem ipsum")]
	[TestCase(2d, 2d)]
	[TestCase(2f, 2f)]
	public static void ConfirmNotEqual_WhenEqual(object o1, object o2)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => o1.ConfirmNotEqual(o2));
	}

	[TestCase(new string[] { "Hello,", "world!" }, new string[] { "Hello,", "world!" })]
	public static void ConfirmNotEqual_WhenArrEqual(object[] o1, object[] o2)
	{
		Confirm.ConfirmThrows<ConfirmAssertException>(() => o1.ConfirmNotEqual(o2));
	}
}
