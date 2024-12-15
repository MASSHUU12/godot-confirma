using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Confirma.Dictionary;

/// <summary>
/// https://en.wikipedia.org/wiki/Radix_tree
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class RadixTree<TValue> : PrefixTree<TValue>
{
    private readonly RadixNode<TValue> _root = new(string.Empty);

    public override void Add(string key, TValue value)
    {
        throw new System.NotImplementedException();
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    public override bool Remove(string key)
    {
        throw new System.NotImplementedException();
    }

    public override bool Remove(KeyValuePair<string, TValue> item)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerable<KeyValuePair<string, TValue>> Search(string prefix)
    {
        throw new System.NotImplementedException();
    }

    public override bool Lookup(string x)
    {
        RadixNode<TValue> traverse = _root;
        int index = 0;

        while (traverse?.IsLeaf() == false && index < x.Length)
        {
            bool found = false;

            foreach (KeyValuePair<string, RadixNode<TValue>> child in traverse.Children)
            {
                string label = child.Key;
                int matchLength = CommonPrefixLength(x, index, label);

                if (matchLength > 0)
                {
                    if (matchLength != label.Length)
                    {
                        // Partial match but label is longer than the remaining key
                        return false;
                    }

                    traverse = child.Value;
                    index += matchLength;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return traverse?.IsLeaf() == true && index == x.Length;
    }

    public override bool TryGetValue(string key, [MaybeNullWhen(false)] out TValue value)
    {
        throw new System.NotImplementedException();
    }

    private int CommonPrefixLength(string str, int strIndex, string label)
    {
        int i = 0;
        while (strIndex + i < str.Length && i < label.Length && str[strIndex + i] == label[i])
        {
            i++;
        }
        return i;
    }
}
