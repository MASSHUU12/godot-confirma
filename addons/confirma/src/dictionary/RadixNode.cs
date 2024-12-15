using System.Collections.Generic;

namespace Confirma.Dictionary;

public class RadixNode<T>
{
    public string Prefix { get; set; }
    public T? Value { get; set; }
    public Dictionary<string, RadixNode<T>> Children { get; set; }

    public RadixNode(string prefix)
    {
        Prefix = prefix;
        Children = new Dictionary<string, RadixNode<T>>();
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
