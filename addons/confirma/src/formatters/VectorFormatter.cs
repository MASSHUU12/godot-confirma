using System;
using Godot;

namespace Confirma.Formatters;

public class VectorFormatter : Formatter
{
    public override string Format(object? value)
    {
        return value?.GetType() switch
        {
            Type t when t == typeof(Vector2)
                => $"Vector2{value}",
            Type t when t == typeof(Vector2I)
                => $"Vector2I{value}",
            Type t when t == typeof(Vector3)
                => $"Vector3{value}",
            Type t when t == typeof(Vector3I)
                => $"Vector3I{value}",
            Type t when t == typeof(Vector4)
                => $"Vector4{value}",
            Type t when t == typeof(Vector4I)
                => $"Vector4I{value}",
            _ => new DefaultFormatter().Format(value),
        };
    }
}
