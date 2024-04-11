namespace Confirma.Types;

public struct TestResult
{
	public uint TotalTests { get; set; }
	public uint TestsPassed { get; set; }
	public uint TestsFailed { get; set; }
	public uint TestsIgnored { get; set; }
	public double TotalTime { get; set; }

	public TestResult(
		uint totalTests,
		uint testsPassed,
		uint testsFailed,
		uint testsIgnored,
		double totalTime
	)
	{
		TotalTests = totalTests;
		TestsPassed = testsPassed;
		TestsFailed = testsFailed;
		TestsIgnored = testsIgnored;
		TotalTime = totalTime;
	}
}
