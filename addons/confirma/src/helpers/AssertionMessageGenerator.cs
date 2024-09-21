using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Confirma.Formatters;
using Confirma.Interfaces;

namespace Confirma.Helpers;

public static class AssertionMessageGenerator
{
    private static IFormatter GetFormatter(Type type)
    {
        if (type == typeof(string))
        { return new StringFormatter(); }
        else if (type == typeof(IEnumerable<object>))
        { return new CollectionFormatter(); }
        else if (typeof(INumber<>).IsAssignableFrom(type))
        { return new NumericFormatter(); }
        else
        { return new DefaultFormatter(); }
    }

    public static string GenerateAssertionMessage(
        string assertion,
        object expected,
        object actual
    )
    {
        IFormatter expectedFormatter = GetFormatter(expected.GetType());
        IFormatter actualFormatter = GetFormatter(actual.GetType());

        // TODO: Add support for custom messages for different types.
        // TODO: Update null extensions.
        return string.Format(
            CultureInfo.InvariantCulture,
            "Assertion {0} failed: Expected {1} but was {2}.",
            assertion,
            expectedFormatter.Format(expected),
            actualFormatter.Format(actual)
        );
    }
}
