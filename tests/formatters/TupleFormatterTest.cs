using System;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Formatters;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class TupleFormatterTest
{
    [TestCase]
    public static void Format_Float_ReturnsCorrectString()
    {
        _ = new TupleFormatter()
            .Format((4.2f, 5.8f))
            .ConfirmEqual("(4.20000, 5.80000)");
    }

    [TestCase]
    public static void Format_Int_ReturnsCorrectString()
    {
        _ = new TupleFormatter().Format((4, 5)).ConfirmEqual("(4, 5)");
    }

    [TestCase]
    public static void Format_String_ReturnsCorrectString()
    {
        _ = new TupleFormatter()
            .Format(("Hello", ", ", "World", "!"))
            .ConfirmEqual("(\"Hello\", \", \", \"World\", \"!\")");
    }
}
