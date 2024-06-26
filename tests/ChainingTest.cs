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
}
