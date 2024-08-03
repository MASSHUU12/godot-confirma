using System.Reflection;
using Confirma.Classes;
using Confirma.Helpers;
using Godot;

namespace Confirma.Scenes;

public partial class TestRunner : Control
{
    [Signal]
    public delegate void TestsExecutionStartedEventHandler();

    [Signal]
    public delegate void TestsExecutionFinishedEventHandler();

#nullable disable
    protected ConfirmaAutoload Autoload { get; set; }
    protected RichTextLabel Output { get; set; }
#nullable restore

    private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

    public override void _Ready()
    {
        Output = GetNode<RichTextLabel>("%Output");
        Log.RichOutput = Output;

        Autoload = GetNode<ConfirmaAutoload>("/root/Confirma");
    }

    public void RunAllTests()
    {
        _ = EmitSignal(SignalName.TestsExecutionStarted);

        Output.Clear();

        TestManager.Props = Autoload.Props;
        TestManager.Run();

        _ = EmitSignal(SignalName.TestsExecutionFinished);
    }
}
