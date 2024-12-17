using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Confirma.Trees;

/// <summary>
/// https://en.wikipedia.org/wiki/Radix_tree
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class RadixTree<TValue> : PrefixTree<TValue>
{
    private readonly RadixNode<TValue> _root = new(string.Empty);

    public override void Add(string key, TValue value)
    {
        _ = GetOrAddNode(key, value);
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
        RadixNode<TValue> node = _root;
        int index = 0;

        while (index < x.Length)
        {
            bool found = false;

            foreach (RadixNode<TValue> child in node.Children.Values)
            {
                string label = child.Prefix;
                int matchLength = CommonPrefixLength(x, index, label);

                if (matchLength > 0)
                {
                    if (matchLength < label.Length)
                    {
                        // Partial match: key does not exist
                        return false;
                    }

                    index += matchLength;
                    node = child;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return node.Value is not null;
    }

    public override bool TryGetValue(string key, [MaybeNullWhen(false)] out TValue value)
    {
        throw new System.NotImplementedException();
    }

    private static int CommonPrefixLength(string str, int strIndex, string label)
    {
        int i = 0;
        while (
            strIndex + i < str.Length
            && i < label.Length
            && str[strIndex + i] == label[i]
        )
        {
            i++;
        }
        return i;
    }

    protected virtual RadixNode<TValue> GetOrAddNode(string key, TValue value)
    {
        RadixNode<TValue> node = _root;
        string remainingKey = key;

        while (true)
        {
            bool matchFound = false;

            foreach (RadixNode<TValue> child in node.Children.Values)
            {
                string label = child.Prefix;
                int commonLength = CommonPrefixLength(remainingKey, 0, label);

                if (commonLength == 0)
                {
                    continue;
                }

                if (commonLength == label.Length)
                {
                    if (commonLength == remainingKey.Length)
                    {
                        // Exact match: update the value
                        child.Value = value;
                        return child;
                    }

                    // Label is a prefix of the remaining key; continue traversal
                    remainingKey = remainingKey[commonLength..];
                    node = child;
                    matchFound = true;

                    break;
                }

                // Partial match: need to split the node
                RadixNode<TValue> splitNode = new(remainingKey[..commonLength]);
                _ = node.Children.Remove(child.Prefix);
                node.Children[splitNode.Prefix] = splitNode;

                // Adjust the existing child
                child.Prefix = label[commonLength..];
                splitNode.Children[child.Prefix] = child;

                if (commonLength == remainingKey.Length)
                {
                    // Remaining key ends here
                    splitNode.Value = value;
                    return splitNode;
                }

                // Add a new node for the remaining part of the key
                string suffix = remainingKey[commonLength..];
                RadixNode<TValue> newNode = new(suffix) { Value = value };
                splitNode.Children[newNode.Prefix] = newNode;

                return newNode;
            }

            if (!matchFound)
            {
                // No matching child: add a new child node
                RadixNode<TValue> newNode = new(remainingKey) { Value = value };
                node.Children[newNode.Prefix] = newNode;
                return newNode;
            }
        }
    }
}
