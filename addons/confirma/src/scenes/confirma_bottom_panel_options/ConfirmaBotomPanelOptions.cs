#if TOOLS

using Confirma.Enums;
using Confirma.Scenes;
using Godot;

[Tool]
public partial class ConfirmaBotomPanelOptions : Window
{
    #nullable disable
    private CheckBox _verbose, _parallelize;
    private TextEdit _category;
    private ConfirmaAutoload _autoload;
    #nullable restore

    public override void _Ready()
    {
        CloseRequested += CloseRequest;

        _verbose = GetNode<CheckBox>("%Verbose");
        _parallelize = GetNode<CheckBox>("%Parallelize");
        _category = GetNode<TextEdit>("%Category");

        _ = CallDeferred("LateInit");
    }

    private void LateInit()
    {
        _autoload = GetNode<ConfirmaAutoload>("/root/Confirma");

        _verbose.Toggled += (bool on) => _autoload.Props.IsVerbose = on;

        _parallelize.Toggled += (bool on) => _autoload.Props.DisableParallelization = on;

        _category.TextChanged += () =>
        {
            _autoload.Props.Target = _autoload.Props.Target with
            {
                Target = ERunTargetType.Category,
                Name = _category.Text
            };
        };
    }

    void CloseRequest()
    {
        Hide();
    }
}

#endif
