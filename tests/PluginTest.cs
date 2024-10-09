using Confirma.Attributes;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class PluginTest
{
    [TestCase]
    public void GetPluginLocation_ReturnsCorrectLocation()
    {
        _ = Plugin.GetPluginLocation().ConfirmEqual("res://addons/confirma/");
    }
}
