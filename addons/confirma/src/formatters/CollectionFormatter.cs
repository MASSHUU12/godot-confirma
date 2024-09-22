using System;
using System.Collections.Generic;
using Confirma.Helpers;

namespace Confirma.Formatters;

public class CollectionFormatter : Formatter
{
    public override string Format(object value)
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
        else
        {
            return new DefaultFormatter().Format(value);
        }
    }
}
