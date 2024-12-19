using System;
using System.Collections.Generic;

namespace Confirma.Trees;

public class RadixNode<TValue>
{
    public ReadOnlyMemory<char> Prefix { get; set; }
    public TValue? Value { get; set; }
    public bool HasValue { get; set; }
    public Dictionary<char, RadixNode<TValue>> Children { get; set; }
    public RadixNode<TValue>? Parent { get; set; }

    public RadixNode()
    {
        Prefix = string.Empty.AsMemory();
        Children = new Dictionary<char, RadixNode<TValue>>();
        Parent = null;
        HasValue = false;
    }

    public RadixNode(char[] prefix, RadixNode<TValue>? parent = null) : this()
    {
        Prefix = prefix;
        Children = new Dictionary<char, RadixNode<TValue>>();
        Parent = parent;
        HasValue = false;
    }

    public bool IsLeaf()
    {
        return HasValue;
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
