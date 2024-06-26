using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ChainingTest
{
	[TestCase]
	public static void ChainingOnNumeric()
	{
		5.ConfirmIsNotZero().ConfirmIsPositive();
	}

	[TestCase]
	public static void ChainingOnRange()
	{
		5.ConfirmGraterThan(3).ConfirmInRange(0, 10);
	}
}
