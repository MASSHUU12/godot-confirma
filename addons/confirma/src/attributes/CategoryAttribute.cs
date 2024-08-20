using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class CategoryAttribute : Attribute
{
    public string Category { get; init; }

    public CategoryAttribute(string category)
    {
        Category = category;
    }
}
