using System;
using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RandomUuidExtensionsTest
{
    private static readonly Random rg = new();

    [Repeat(3)]
    [TestCase]
    public void NextUuid4()
    {
        _ = rg.NextUuid4().ToString().ConfirmValidUuid4();
    }
}
