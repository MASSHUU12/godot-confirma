using System;

namespace Confirma.Types;

public struct TestsProps
{
	public event Action? ExitOnFailure;

	public TestResult Result { get; set; }

	public readonly bool IsHeadless { get; init; }
	public readonly bool ExitOnFail { get; init; }
	public readonly bool QuitAfterTests { get; init; }
	public readonly string ClassName { get; init; }

	public TestsProps(
		bool isHeadless,
		bool exitOnFail,
		bool quitAfterTests,
		string className
	)
	{
		IsHeadless = isHeadless;
		ExitOnFail = exitOnFail;
		QuitAfterTests = quitAfterTests;
		ClassName = className;
		Result = new();
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
