using Confirma.Attributes;
using Confirma.Enums;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class IgnoreAttributeTest
{
    [TestCase]
    public static void Constructor_IgnoreMode_SetsValuesCorrectly()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always);

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.Always);
        _ = attribute.Reason.ConfirmNull();
    }

    [TestCase]
    public static void Constructor_All_SetsValuesCorrectly()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.InEditor, "Hello");

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.InEditor);
        _ = attribute.Reason.ConfirmEqual("Hello");
    }

    [TestCase]
    public static void Constructor_EmptyReason_SetsReasonToNull()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always, string.Empty);

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.Always);
        _ = attribute.Reason.ConfirmNull();
    }

    [TestCase]
    public static void IsIgnored_ModeAlways_ReturnsTrue()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always);

        _ = attribute.IsIgnored().ConfirmTrue();
    }

    [TestCase]
    public static void IsIgnored_ModeInEditor_ReturnsTrueWhenInEditor()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.InEditor);

        _ = attribute.IsIgnored().ConfirmEqual(Engine.IsEditorHint());
    }
}
