#if TOOLS

using Confirma.Enums;
using Godot;

namespace Confirma.Scenes;

[Tool]
public partial class ConfirmaBottomPanelOptions : Window
{
#nullable disable
    private TreeContent _tree;
    private TreeItem _verbose, _parallelize, _disableOrphansMonitor, _category;
    private ConfirmaAutoload _autoload;
#nullable restore

    public override void _Ready()
    {
        CloseRequested += CloseRequest;

        _tree = GetNode<TreeContent>("%TreeContent");
        _ = _tree.AddRoot();
        _tree.ItemEdited += UpdateSettings;

        _category = _tree.AddTextInput("Category");
        _verbose = _tree.AddCheckBox("Verbose");
        _parallelize = _tree.AddCheckBox("Disable parallelization");
        _disableOrphansMonitor = _tree.AddCheckBox("Disable orphans monitor");

        _ = CallDeferred("LateInit");
    }

    private void LateInit()
    {
        _autoload = GetNode<ConfirmaAutoload>("/root/Confirma");

        InitializePanelOptions();
    }

    private void InitializePanelOptions()
    {
        _verbose.SetChecked(1, _autoload.Props.IsVerbose);
        _parallelize.SetChecked(1, _autoload.Props.DisableParallelization);
        _disableOrphansMonitor.SetChecked(1, !_autoload.Props.MonitorOrphans);
        _category.SetText(1, _autoload.Props.Target.Name);
    }

    private void UpdateSettings()
    {
        _autoload.Props.IsVerbose = _verbose.IsChecked(1);
        _autoload.Props.DisableParallelization = _parallelize.IsChecked(1);
        _autoload.Props.MonitorOrphans = !_disableOrphansMonitor.IsChecked(1);

        string categoryName = _category.GetText(1);
        _autoload.Props.Target = _autoload.Props.Target with
        {
            Target = string.IsNullOrEmpty(categoryName)
                ? ERunTargetType.All
                : ERunTargetType.Category,
            Name = categoryName
        };
    }

    private void CloseRequest()
    {
        Hide();
    }
}
#endif
