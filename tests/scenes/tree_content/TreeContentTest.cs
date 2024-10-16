using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Scenes;
using Godot;
using static Godot.TreeItem;

namespace Confirma.Tests;

[SetUp]
[TearDown]
[TestClass]
// [Parallelizable] // NOTE: Godot hangs when using this attribute here.
public class TreeContentTest
{
#nullable disable
    private TreeContent _tree;
#nullable restore

    public void SetUp()
    {
        _tree = new()
        {
            Columns = 2
        };
    }

    public void TearDown()
    {
        _tree.Free();
    }

    [TestCase]
    public void AddRoot_CreatesNewTreeItem()
    {
        TreeItem root = _tree.AddRoot();

        _ = root.ConfirmNotNull();
        _ = _tree.GetRoot().ConfirmSameReference(root);
    }

    [TestCase]
    public void AddLabel_CreatesNewTreeItemWithText()
    {
        TreeItem label = _tree.AddLabel("Test Label");

        _ = label.ConfirmNotNull();
        _ = label.GetText(0).ConfirmEqual("Test Label");
        _ = label.IsSelectable(0).ConfirmFalse();
        _ = label.IsSelectable(1).ConfirmFalse();
    }

    [TestCase]
    public void AddLabel_CreatesNewTreeItemWithParent()
    {
        TreeItem parent = _tree.AddRoot();
        TreeItem label = _tree.AddLabel("Test Label", parent);

        _ = label.ConfirmNotNull();
        _ = label.GetText(0).ConfirmEqual("Test Label");
        _ = label.IsSelectable(0).ConfirmFalse();
        _ = label.IsSelectable(1).ConfirmFalse();
        _ = label.GetParent().ConfirmSameReference(parent);
    }

    [TestCase]
    public void AddCheckBox_CreatesNewTreeItemWithTextAndCheckBox()
    {
        TreeItem checkBox = _tree.AddCheckBox("Test Check Box");

        _ = checkBox.ConfirmNotNull();
        _ = checkBox.GetText(0).ConfirmEqual("Test Check Box");
        _ = checkBox.IsSelectable(0).ConfirmFalse();
        _ = checkBox.IsSelectable(1).ConfirmFalse();
        _ = checkBox.IsEditable(1).ConfirmTrue();
        _ = checkBox.GetCellMode(1).ConfirmEqual(TreeCellMode.Check);
    }

    [TestCase]
    public void AddCheckBox_CreatesNewTreeItemWithParent()
    {
        TreeItem parent = _tree.AddRoot();
        TreeItem checkBox = _tree.AddCheckBox("Test Check Box", parent);

        _ = checkBox.ConfirmNotNull();
        _ = checkBox.GetText(0).ConfirmEqual("Test Check Box");
        _ = checkBox.IsSelectable(0).ConfirmFalse();
        _ = checkBox.IsSelectable(1).ConfirmFalse();
        _ = checkBox.IsEditable(1).ConfirmTrue();
        _ = checkBox.GetCellMode(1).ConfirmEqual(TreeCellMode.Check);
        _ = checkBox.GetParent().ConfirmSameReference(parent);
    }

    [TestCase]
    public void AddTextInput_CreatesNewTreeItemWithLabelAndTextInput()
    {
        TreeItem textInput = _tree.AddTextInput("Test Text Input");

        _ = textInput.ConfirmNotNull();
        _ = textInput.GetText(0).ConfirmEqual("Test Text Input");
        _ = textInput.IsSelectable(0).ConfirmFalse();
        _ = textInput.IsSelectable(1).ConfirmTrue();
        _ = textInput.IsEditable(1).ConfirmTrue();
        _ = textInput.IsEditMultiline(1).ConfirmTrue();
        _ = textInput.GetCustomBgColor(1).ConfirmEqual(new("#1c2128"));
    }

    [TestCase]
    public void AddTextInput_CreatesNewTreeItemWithParent()
    {
        TreeItem parent = _tree.AddRoot();
        TreeItem textInput = _tree.AddTextInput("Test Text Input", parent);

        _ = textInput.ConfirmNotNull();
        _ = textInput.GetText(0).ConfirmEqual("Test Text Input");
        _ = textInput.IsSelectable(0).ConfirmFalse();
        _ = textInput.IsSelectable(1).ConfirmTrue();
        _ = textInput.IsEditable(1).ConfirmTrue();
        _ = textInput.IsEditMultiline(1).ConfirmTrue();
        _ = textInput.GetParent().ConfirmSameReference(parent);
    }
}
