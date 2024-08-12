using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class RandomUuidExtensionsTest
{
    private static readonly Random rg = new();

    [Repeat(3)]
    [TestCase]
    public static void NextUuid4()
    {
        _ = rg.NextUuid4().ToString().ConfirmValidUuid4();
    }
}
