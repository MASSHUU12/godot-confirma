using System;
using Confirma.Attributes;
using static Confirma.Enums.EIgnoreMode;

[BeforeAll(MethodName = "InvalidMethodName")]
[TestClass]
[Parallelizable]
[Category("InvalidLifecycleMethodsTest")]
[Ignore(
    WhenNotRunningCategory,
    category: "InvalidLifecycleMethodsTest",
    hideFromResults: true
)]
public static class InvalidBeforeAllTest
{
    public static void BeforeAll()
    {
        throw new NotImplementedException();
    }
}
