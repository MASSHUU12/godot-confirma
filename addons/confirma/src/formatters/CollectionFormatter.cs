using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Confirma.Helpers;

namespace Confirma.Formatters;

public class CollectionFormatter : Formatter
{
    private readonly bool _addTypeHint;

    public CollectionFormatter(bool addTypeHint = true)
    {
        _addTypeHint = addTypeHint;
    }

    public override string Format(object? value)
    {
        if (value is null)
        {
            return new DefaultFormatter().Format(value);
        }

        if (value is IEnumerable enumerable && value is not string)
        {
            Type? elementType = value.GetType().GetInterfaces()
                .Where(static t => t.IsGenericType
                    && t.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                )
                .Select(static t => t.GetGenericArguments()[0])
                .FirstOrDefault();

            if (elementType is null)
            {
                // Fallback if unable to determine element type
                return CollectionHelper.ToString(
                    enumerable.Cast<object?>(),
                    depth: 0,
                    maxDepth: 1,
                    addBrackets: true,
                    addTypeHint: _addTypeHint
                );
            }

            // The signature of the CollectionHelper.ToString method
            Type[] methodParamTypes = new Type[]
            {
                typeof(IEnumerable<>).MakeGenericType(elementType), // IEnumerable<T>
                typeof(uint),                                       // depth
                typeof(uint),                                       // maxDepth
                typeof(bool),                                       // addBrackets
                typeof(bool)                                        // addTypeHint
            };

            MethodInfo? toStringMethod = typeof(CollectionHelper)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(
                    static m => m.Name == nameof(CollectionHelper.ToString)
                    && m.IsGenericMethod
                );

            if (toStringMethod is not null)
            {
                toStringMethod = toStringMethod.MakeGenericMethod(elementType);

                object[] parameters = new object[]
                {
                    value,       // The collection
                    (uint)0,     // depth
                    (uint)1,     // maxDepth
                    true,        // addBrackets
                    _addTypeHint // addTypeHint
                };

                object? result = toStringMethod.Invoke(null, parameters);

                if (result is string strResult)
                {
                    return strResult;
                }
            }
        }

        return new DefaultFormatter().Format(value);
    }
}
