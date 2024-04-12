namespace Confirma.Types;

public struct TestResult
{
	public uint TotalTests { get; set; } = 0;
	public uint TestsPassed { get; set; } = 0;
	public uint TestsFailed { get; set; } = 0;
	public uint TestsIgnored { get; set; } = 0;
	public double TotalTime { get; set; } = 0;
	public uint Warnings { get; set; } = 0;

	public TestResult() { }
}
