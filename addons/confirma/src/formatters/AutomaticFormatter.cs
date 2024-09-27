using System;
using System.Linq;
using System.Numerics;
using Confirma.Helpers;
using Godot;

namespace Confirma.Formatters;

public class AutomaticFormatter : Formatter
{
    public override string Format(object? value)
    {
        return value?.GetType() switch
        {
            Type t when t == typeof(string)
                => new StringFormatter().Format(value),
            Type t when t == typeof(char)
                => new StringFormatter('\'').Format(value),
            Type t when t == typeof(Variant)
                => new VariantFormatter().Format(value),
            Type t when t.GetInterfaces().Any(
                static i => i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(INumber<>)
            ) => new NumericFormatter().Format(value),
            Type t when t.IsCollection()
                => new CollectionFormatter().Format(value),
            _ => new DefaultFormatter().Format(value),
        };
    }
}
