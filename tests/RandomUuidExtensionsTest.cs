using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomUuidExtensionsTest
{
	private static readonly Random rg = new();

	[TestCase]
	public static void NextUuid4()
	{
		rg.NextUuid4().ToString().ConfirmValidUuid4();
		rg.NextUuid4().ToString().ConfirmValidUuid4();
		rg.NextUuid4().ToString().ConfirmValidUuid4();
	}
}
