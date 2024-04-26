using System;

namespace Confirma.Types;

public struct TestsProps
{
	public event Action? ExitOnFailure;

	public TestResult Result { get; set; }

	public TestsProps()
	{
		Result = new();
	}

	public void ResetStats()
	{
		Result = new();
	}
}
