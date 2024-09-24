using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Confirma.Helpers;

namespace Confirma.Formatters;

public class CollectionFormatter : Formatter
{
    public override string Format(object? value)
    {
        ICollection<object?>? c = value as ICollection<object?>;
        if (c is not null)
        {
            return CollectionHelper.ToString(c);
        }
        else if (value is Array a)
        {
            string result = CollectionHelper.ToString((ICollection<object?>)a);
            return $"[{result}]";
        }
        else if (value is IEnumerable b and not string)
        {

            object?[] array = b.Cast<object?>().ToArray();

            return CollectionHelper.ToString(array);
        }
        else
        {
            return new DefaultFormatter().Format(value);
        }
    }
}
