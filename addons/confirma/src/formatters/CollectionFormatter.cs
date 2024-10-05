using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Confirma.Helpers;

namespace Confirma.Formatters;

public class CollectionFormatter : Formatter
{
    public override string Format(object? value)
    {
        if (value is ICollection<object?> c)
        {
            return CollectionHelper.ToString(c);
        }
        else if (value is IEnumerable b and not string)
        {
            IEnumerable<object?> array = b.Cast<object?>();

            return CollectionHelper.ToString(array);
        }
        return new DefaultFormatter().Format(value);
    }
}
