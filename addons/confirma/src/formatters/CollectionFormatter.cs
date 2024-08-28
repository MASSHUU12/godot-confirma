using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Helpers;
using Confirma.Interfaces;

namespace Confirma.Formatters;

public class CollectionFormatter : IFormatter
{
    public string Format(object value)
    {
        string result = "null";

        ICollection<object?>? c = value as ICollection<object?>;
        if (c is not null)
        {
            return CollectionHelper.ToString(c);
        }
        else if (value is Array a)
        {
            result = CollectionHelper.ToString((ICollection<object?>)a);
        }
        else
        {
            return new DefaultFormatter().Format(value);
        }

        return $"[{result}]";
    }
}
