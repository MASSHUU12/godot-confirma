using System.Collections.Generic;
using System.Linq;
using Confirma.Formatters;

namespace Confirma.Helpers;

public static class CollectionHelper
{
    public static string ToString<T>(
        IEnumerable<T> collection,
        uint depth = 0,
        uint maxDepth = 1,
        bool addBrackets = true,
        bool addTypeHint = true
    )
    {
        string typeName = addTypeHint ? typeof(T).Name : string.Empty;

        if (depth > maxDepth || !collection.Any())
        {
            return addBrackets ? typeName + "[]" : string.Empty;
        }

        List<string> list = new();

        foreach (T item in collection)
        {
            if (item is null)
            {
                list.Add("null");
                continue;
            }

            if (item is IEnumerable<object> e && item is not string)
            {
                if (depth + 1 > maxDepth)
                {
                    list.Add(typeName + "[...]");
                    continue;
                }

                string result = ToString(e, depth + 1, maxDepth, addBrackets: false);
                list.Add(typeName + $"[{string.Join(", ", result)}]");
                continue;
            }

            list.Add(new AutomaticFormatter().Format(item));
        }
        string text = string.Join(", ", list);

        return addBrackets ? typeName + $"[{text}]" : text;
    }
}
