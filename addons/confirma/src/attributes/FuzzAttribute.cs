using System;
using Confirma.Classes;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class FuzzAttribute : Attribute
{
    public FuzzParameter[] Parameters { get; init; }

    public FuzzAttribute(params FuzzParameter[] parameters)
    {
        Parameters = parameters;
    }
}
