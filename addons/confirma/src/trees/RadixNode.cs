using System;
using System.Collections.Generic;

namespace Confirma.Trees;

// TODO: Tests
public class RadixNode<TValue>
{
    public ReadOnlyMemory<char> Prefix { get; set; }
    public TValue? Value { get; set; }
    public Dictionary<char, RadixNode<TValue>> Children { get; set; }
    public RadixNode<TValue>? Parent { get; set; }

    public RadixNode()
    {
        Prefix = string.Empty.AsMemory();
        Children = new Dictionary<char, RadixNode<TValue>>();
        Parent = null;
    }

    public RadixNode(char[] prefix, RadixNode<TValue>? parent = null) : this()
    {
        Prefix = prefix;
        Parent = parent;
    }

    public bool IsLeaf()
    {
        return Value is not null;
    }

    public override string? ToString()
    {
        return $"RadixNode(Prefix=\"{Prefix}\", Value={Value})";
    }

    public string GetFullKey()
    {
        Stack<string> parts = new();
        RadixNode<TValue>? node = this;
        while (node?.Parent is not null)
        {
            parts.Push(node.Prefix.ToString());
            node = node.Parent;
        }
        return string.Concat(parts);
    }
}
