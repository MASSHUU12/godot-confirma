using System.Collections.Generic;
using System.Reflection;

namespace Confirma.Classes;

public class TestMethodResult
{
    public uint TestsPassed { get; set; }
    public uint TestsFailed { get; set; }
    public uint TestsIgnored { get; set; }
    public uint Warnings { get; set; }
    public List<TestOutput> TestedCases {get; set;}

    public TestMethodResult(uint passed, uint failed, uint ignored, uint warnings,  List<TestOutput> cases)
    {
        TestsPassed = passed;
        TestsFailed = failed;
        TestsIgnored = ignored;
        Warnings = warnings;
        TestedCases= cases;
    }

    public TestMethodResult()
    {
        TestsPassed = 0;
        TestsFailed = 0;
        TestsIgnored = 0;
        Warnings = 0;
        TestedCases = new ();
    }

    public void Reset()
    {
        TestsPassed = 0;
        TestsFailed = 0;
        TestsIgnored = 0;
        Warnings = 0;
        TestedCases = new ();
    }
}
