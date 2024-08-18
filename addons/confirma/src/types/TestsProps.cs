using System;
using Confirma.Enums;
using Confirma.Scenes;

namespace Confirma.Types;

public struct TestsProps
{
    public event Action? ExitOnFailure;

    public RunTarget Target { get; set; }
    public TestResult Result { get; set; } = new();
    public ConfirmaAutoload? Autoload { get; set; }

    public bool RunTests { get; set; }
    public bool IsVerbose { get; set; }
    public bool IsHeadless { get; set; }
    public bool ExitOnFail { get; set; }
    public bool DisableCsharp { get; set; }
    public bool MonitorOrphans { get; set; }
    public bool DisableGdScript { get; set; }
    public bool DisableParallelization { get; set; }
    public string GdTestPath { get; set; } = "./gdtests";
    public string OutputPath { get; set; } = "./test_results.json";
    public ELogOutputType OutputType { get; set; } = ELogOutputType.Log;

    public TestsProps() { }

    public void ResetStats()
    {
        Result = new TestResult();
    }

    public readonly void CallExitOnFailure()
    {
        ExitOnFailure?.Invoke();
    }
}
