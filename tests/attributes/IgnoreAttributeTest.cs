using Confirma.Attributes;
using Confirma.Enums;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class IgnoreAttributeTest
{
    [TestCase]
    public void Constructor_IgnoreMode_SetsValuesCorrectly()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always);

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.Always);
        _ = attribute.Reason.ConfirmNull();
    }

    [TestCase]
    public void Constructor_All_SetsValuesCorrectly()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.InEditor, "Hello");

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.InEditor);
        _ = attribute.Reason.ConfirmEqual("Hello");
    }

    [TestCase]
    public void Constructor_EmptyReason_SetsReasonToNull()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always, string.Empty);

        _ = attribute.Mode.ConfirmEqual(EIgnoreMode.Always);
        _ = attribute.Reason.ConfirmNull();
    }

    [TestCase]
    public void IsIgnored_ModeAlways_ReturnsTrue()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.Always);

        _ = attribute.IsIgnored(new()).ConfirmTrue();
    }

    [TestCase]
    public void IsIgnored_ModeInEditor_ReturnsTrueWhenInEditor()
    {
        IgnoreAttribute attribute = new(EIgnoreMode.InEditor);

        _ = attribute.IsIgnored(new()).ConfirmEqual(Engine.IsEditorHint());
    }

    [TestCase]
    public void IsIgnored_WhenNotRunningCategory_ReturnsTrueWhenNotRunning()
    {
        IgnoreAttribute attribute = new(
            EIgnoreMode.InEditor,
            category: "Ipsum"
        );

        _ = attribute
            .IsIgnored(new(Name: "Lorem"))
            .ConfirmEqual(Engine.IsEditorHint());
    }

    [TestCase]
    public void IsIgnored_WhenNotRunningCategory_ReturnsFalseWhenRunning()
    {
        IgnoreAttribute attribute = new(
            EIgnoreMode.InEditor,
            category: "Lorem"
        );

        _ = attribute
            .IsIgnored(new(Name: "Lorem"))
            .ConfirmEqual(Engine.IsEditorHint());
    }

    [TestCase]
    public void IsIgnored_Headless_ReturnsCorrectValue()
    {
        IgnoreAttribute attr = new(EIgnoreMode.InHeadless);

        _ = attr.IsIgnored(new())
            .ConfirmEqual(DisplayServer.GetName() == "headless");
    }
}
