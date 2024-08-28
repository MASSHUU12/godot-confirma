using System;
using System.Collections.Generic;
using System.Linq;
using Confirma.Helpers;
using Confirma.Interfaces;

namespace Confirma.Formatters;

public class CollectionFormatter : IFormatter
{
    public string Format(object? value)
    {
        string result = "null";

        if (value is null)
        {
            return result;
        }

        if (value is IEnumerable<object> e)
        {
            result = ArrayHelper.ToString(e.ToArray());
        }
        else if (value is Array a)
        {
            Log.PrintLine("aaa");
            result = ArrayHelper.ToString((object?[]?)a);
        }
        else
        {
            return new DefaultFormatter().Format(value);
        }

        return $"[{result}]";
    }
}
