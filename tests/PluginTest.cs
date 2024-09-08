using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class PluginTest
{
    [TestCase]
    public static void GetPluginLocation_ReturnsCorrectLocation()
    {
        _ = Plugin.GetPluginLocation().ConfirmEqual("res://addons/confirma/");
    }
}
