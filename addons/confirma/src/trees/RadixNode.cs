using System;
using System.Collections.Generic;

namespace Confirma.Trees;

// TODO: Tests
public class RadixNode<T>
{
    public ReadOnlyMemory<char> Prefix { get; set; }
    public T? Value { get; set; }
    public Dictionary<char, RadixNode<T>> Children { get; set; }

    public RadixNode()
    {
        Prefix = string.Empty.AsMemory();
        Children = new Dictionary<char, RadixNode<T>>();
    }

    public RadixNode(ReadOnlyMemory<char> prefix)
    {
        Prefix = prefix;
        Children = new Dictionary<char, RadixNode<T>>();
    }

    public bool IsLeaf()
    {
        return Value is not null;
    }

    public override string? ToString()
    {
        return $"RadixNode(Prefix=\"{Prefix}\", Value={Value})";
    }
}
