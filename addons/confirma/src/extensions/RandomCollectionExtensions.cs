using System;
using System.Collections.Generic;
using System.Linq;

namespace Confirma.Extensions;

public static class RandomCollectionExtensions
{
    public static T NextElement<T>(this Random rg, IEnumerable<T> elements)
    {
        if (!elements.Any())
        {
            throw new InvalidOperationException(
                "Cannot get a random element from an empty collection."
            );
        }

        return elements.ElementAt(rg.Next(0, elements.Count()));
    }

}
