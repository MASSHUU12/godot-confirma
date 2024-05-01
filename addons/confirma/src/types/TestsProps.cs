using System;

namespace Confirma.Types;

public struct TestsProps
{
	public event Action? ExitOnFailure;

	public TestResult Result { get; set; } = new();

	public bool RunTests { get; set; } = false;
	public bool IsVerbose { get; set; } = false;
	public bool IsHeadless { get; set; } = false;
	public bool ExitOnFail { get; set; } = false;
	public string ClassName { get; set; } = string.Empty;
	public bool QuitAfterTests { get; set; } = false;

	public TestsProps() { }

	public TestsProps(
		bool runTests,
		bool isHeadless,
		bool exitOnFail,
		bool quitAfterTests,
		bool isVerbose,
		string className
	)
	{
		Result = new();
		RunTests = runTests;
		ClassName = className;
		IsVerbose = isVerbose;
		ExitOnFail = exitOnFail;
		IsHeadless = isHeadless;
		QuitAfterTests = quitAfterTests;
	}

	public void ResetStats()
	{
		Result = new();
	}

	public readonly void CallExitOnFailure()
	{
		ExitOnFailure?.Invoke();
	}
}
