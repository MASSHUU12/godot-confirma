namespace Confirma.Classes;

public class TestMethodResult
{
	uint TestsPassed { get; set; }
	uint TestsFailed { get; set; }
	uint TestsIgnored { get; set; }

	public TestMethodResult(uint passed, uint failed, uint ignored)
	{
		TestsPassed = passed;
		TestsFailed = failed;
		TestsIgnored = ignored;
	}

	public TestMethodResult()
	{
		TestsPassed = 0;
		TestsFailed = 0;
		TestsIgnored = 0;
	}

	public void Reset()
	{
		TestsPassed = 0;
		TestsFailed = 0;
		TestsIgnored = 0;
	}
}
