using System;
using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[BeforeAll]
[TestClass]
[Parallelizable]
[Category("ExceptionInLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "ExceptionInLifecycleMethodsTest",
    hideFromResults: true
)]
public static class ExceptionInBeforeAllTest
{
    public static void BeforeAll()
    {
        throw new NotImplementedException();
    }
}
