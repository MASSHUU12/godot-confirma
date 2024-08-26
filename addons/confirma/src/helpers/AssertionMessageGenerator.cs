using System;
using System.Numerics;
using System.Collections.Generic;
using System.Globalization;
using Confirma.Formatters;
using Confirma.Interfaces;

namespace Confirma.Helpers;

public class AssertionMessageGenerator
{
    private readonly Dictionary<Type, IFormatter> _formatters;

    public AssertionMessageGenerator()
    {
        _formatters = new Dictionary<Type, IFormatter>
        {
            { typeof(string), new StringFormatter() },
            { typeof(IEnumerable<object>), new CollectionFormatter() }
        };
    }

    private IFormatter GetFormatter(Type type)
    {
        if (typeof(INumber<>).IsAssignableFrom(type))
        {
            return new NumericFormatter();
        }

        return _formatters.TryGetValue(type, out IFormatter? formatter)
            ? formatter
            : new DefaultFormatter();
    }

    public string GenerateAssertionMessage(
        string assertion,
        object expected,
        object actual
    )
    {
        IFormatter expectedFormatter = GetFormatter(expected.GetType());
        IFormatter actualFormatter = GetFormatter(actual.GetType());

        return string.Format(
            CultureInfo.InvariantCulture,
            "Assertion {0} failed: Expected {1} but was {2}.",
            assertion,
            expectedFormatter.Format(expected),
            actualFormatter.Format(actual)
        );
    }
}
