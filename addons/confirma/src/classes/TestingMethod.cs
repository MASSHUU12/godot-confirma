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
        using IEnumerator<System.Attribute> discovered = CsTestDiscovery
            .GetAttributesForTestCaseGeneration(Method)
            .GetEnumerator();

        List<TestCase> cases = new();
        List<FuzzGenerator> generators = new();
        RepeatAttribute? pendingRepeat = null;
        RepeatAttribute? fuzzRepeat = null;

        while (discovered.MoveNext())
        {
            switch (discovered.Current)
            {
                case RepeatAttribute repeat:
                    if (pendingRepeat is not null)
                    {
                        Log.PrintWarning(
                            $"The Repeat attributes for the {Method.Name} "
                            + "cannot occur in succession.\n"
                        );
                        Result.Warnings++;
                        break;
                    }

                    pendingRepeat = repeat;
                    continue;

                case TestCaseAttribute testCase:
                    cases.Add(new(Method, testCase.Parameters, pendingRepeat));
                    pendingRepeat = null;
                    continue;

                case FuzzAttribute fuzz:
                    generators.Add(fuzz.Generator);

                    if (fuzzRepeat is null)
                    {
                        fuzzRepeat = pendingRepeat;
                        pendingRepeat = null;
                    }
                    else if (pendingRepeat is not null)
                    {
                        Log.PrintWarning(
                            "Multiple Repeat attributes were detected associated"
                            + " with the Fuzz attributes for method "
                            + $"{Method.Name}. Only the first one will be used.\n"
                        );
                        pendingRepeat = null;
                        Result.Warnings++;
                    }
                    continue;

                default:
                    // Unexpected attributes
                    continue;
            }
        }

        if (pendingRepeat is not null)
        {
            Log.PrintWarning(
                $"The Repeat attribute for the {Method.Name} method "
                + "will be ignored because it does not have the "
                + "TestCase/Fuzz attribute associated with it.\n"
            );
            Result.Warnings++;
        }

        if (generators.Count != 0)
        {
            int methodParams = Method.GetParameters().Length;

            if (generators.Count > methodParams)
            {
                Log.PrintWarning(
                    $"Detected {generators.Count} Fuzz attributes but "
                    + $"{Method.Name} contains only {methodParams} parameters."
                    + " Excessive Fuzz attributes are ignored.\n"
                );
                Result.Warnings++;
            }

            cases.Add(new(
                Method,
                generators
                    .Take(methodParams) // Ignore excessive attributes
                    .Select(static gen => gen.NextValue()).ToArray(),
                fuzzRepeat
            ));
        }

        return cases;
    }
}
