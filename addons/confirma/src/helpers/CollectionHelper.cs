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
        bool addBrackets = true

    )
    {
        if (depth > maxDepth || !collection.Any())
        {
            return "[]";
        }

        List<string> list = new();

        foreach (T item in collection)
        {
            if (item is null)
            {
                list.Add("null");
                continue;
            }

            if (item is IEnumerable<object> e && !(item is string))
            {
                if (depth + 1 > maxDepth)
                {
                    list.Add("[...]");
                    continue;
                }

                string result = ToString(e, depth + 1, maxDepth);
                list.Add($"[{string.Join(", ", result)}]");
                continue;
            }

            list.Add(new AutomaticFormatter().Format(item));
        }
        string text = string.Join(", ", list);

        return addBrackets ? $"[{text}]" : text;
    }
}
