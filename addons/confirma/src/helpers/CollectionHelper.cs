using System.Collections.Generic;

namespace Confirma.Helpers;

public static class CollectionHelper
{
    public static string ToString(
        ICollection<object?> collection,
        uint depth = 0,
        uint maxDepth = 1
    )
    {
        if (depth > maxDepth || collection.Count == 0)
        {
            return "[]";
        }

        List<string> list = new();

        foreach (object? item in collection)
        {
            if (item is null)
            {
                list.Add("null");
                continue;
            }

            if (item is ICollection<object?> e)
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

            list.Add(item.ToString() ?? "null");
        }

        return $"[{string.Join(", ", list)}]";
    }
}
