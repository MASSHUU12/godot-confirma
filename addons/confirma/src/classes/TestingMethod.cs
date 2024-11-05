using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Classes.Discovery;
using Confirma.Enums;
using Confirma.Exceptions;
using Confirma.Fuzz;
using Confirma.Helpers;
using Confirma.Types;

using static Confirma.Enums.ETestCaseState;

namespace Confirma.Classes;

public class TestingMethod
{
    public MethodInfo Method { get; }
    public IEnumerable<TestCase> TestCases { get; }
    public string Name { get; }
    public TestMethodResult Result { get; }

    public TestingMethod(MethodInfo method)
    {
        Result = new();
        Method = method;
        TestCases = DiscoverTestCases();
        Name = Method.GetCustomAttribute<TestNameAttribute>()?.Name ?? Method.Name;
    }

    public TestMethodResult Run(TestsProps props, object? instance)
    {
        foreach (TestCase test in TestCases)
        {
            for (ushort i = 0; i <= (test.Repeat?.Repeat ?? 0); i++)
            {
                IgnoreAttribute? attr = test.Method.GetCustomAttribute<IgnoreAttribute>();

                if (attr?.IsIgnored(props.Target) == true)
                {
                    if (attr.HideFromResults == true)
                    {
                        continue;
                    }

                    Result.TestsIgnored++;

                    TestLog log = new(
                        ELogType.Method,
                        Name,
                        Ignored,
                        test.Params,
                        attr.Reason
                    );
                    Result.TestLogs.Add(log);
                    continue;
                }

                try
                {
                    test.Run(instance);
                    Result.TestsPassed++;

                    TestLog log = new(
                        ELogType.Method,
                        Name,
                        Passed,
                        test.Params,
                        null,
                        ELangType.CSharp
                    );
                    Result.TestLogs.Add(log);
                }
                catch (ConfirmAssertException e)
                {
                    Result.TestsFailed++;

                    TestLog log = new(
                        ELogType.Method,
                        Name,
                        Failed,
                        test.Params,
                        e.Message,
                        ELangType.CSharp
                    );
                    Result.TestLogs.Add(log);

                    if (test.Repeat?.FailFast == true)
                    {
                        break;
                    }

                    if (props.ExitOnFail)
                    {
                        props.CallExitOnFailure();
                    }
                }
            }
        }

        return Result;
    }

    private List<TestCase> DiscoverTestCases()
    {
        List<System.Attribute> testCases = CsTestDiscovery
            .GetTestCasesFromMethod(Method)
            .ToList();

        testCases.AddRange(CsTestDiscovery.GetFuzzersFromMethod(Method));

        using IEnumerator<System.Attribute> discovered = testCases.GetEnumerator();

        List<TestCase> cases = new();
        List<FuzzGenerator> generators = new();
        RepeatAttribute? fuzzRepeat = null;

        while (discovered.MoveNext())
        {
            switch (discovered.Current)
            {
                case FuzzAttribute fuzz:
                    generators.Add(fuzz.Generator);
                    continue;
                case TestCaseAttribute testCase:
                    cases.Add(new(Method, testCase.Parameters, null));
                    continue;
                // I rely on the order in which the attributes are defined
                // to determine which TestCase attributes should be assigned values
                // from the Repeat attributes.
                case RepeatAttribute when !discovered.MoveNext():
                    Log.PrintWarning(
                        $"The Repeat attribute for the \"{Method.Name}\" method "
                        + "will be ignored because it does not have the "
                        + "TestCase/Fuzz attribute after it.\n"
                    );
                    Result.Warnings++;
                    continue;
                case RepeatAttribute when discovered.Current is RepeatAttribute:
                    Log.PrintWarning(
                        $"The Repeat attributes for the \"{Method.Name}\" "
                        + "cannot occur in succession.\n"
                    );
                    Result.Warnings++;
                    continue;
                case RepeatAttribute repeat:
                    {
                        if (discovered.Current is TestCaseAttribute tc)
                        {
                            cases.Add(new(Method, tc.Parameters, repeat));
                            continue;
                        }

                        if (discovered.Current is FuzzAttribute)
                        {
                            fuzzRepeat = repeat;
                        }

                        break;
                    }
            }
        }

        if (generators.Count != 0)
        {
            cases.Add(
                new(
                    Method,
                    generators.Select(static gen => gen.NextValue()).ToArray(),
                    fuzzRepeat
                )
            );
        }

        return cases;
    }
}
