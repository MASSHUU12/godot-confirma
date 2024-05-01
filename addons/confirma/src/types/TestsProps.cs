using System;

namespace Confirma.Types;

public struct TestsProps
{
	public event Action? ExitOnFailure;

	public TestResult Result { get; set; }

	public bool IsVerbose { get; init; }
	public readonly bool IsHeadless { get; init; }
	public readonly bool ExitOnFail { get; init; }
	public readonly string ClassName { get; init; }
	public readonly bool QuitAfterTests { get; init; }

	public TestsProps(
		bool isHeadless,
		bool exitOnFail,
		bool quitAfterTests,
		bool isVerbose,
		string className
	)
	{
		Result = new();
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
