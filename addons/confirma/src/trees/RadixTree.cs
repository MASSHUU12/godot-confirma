using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Confirma.Trees;

/// <summary>
/// https://en.wikipedia.org/wiki/Radix_tree
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class RadixTree<TValue> : PrefixTree<TValue>
{
    private readonly RadixNode<TValue> _root = new();

    public override void Add(string key, TValue value)
    {
        _ = GetOrAddNode(key, value);
    }

    public override void Add(ReadOnlySpan<char> key, TValue value)
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
        List<KeyValuePair<string, TValue>> results = new();
        RadixNode<TValue>? node = _root;
        ReadOnlySpan<char> remainingPrefix = prefix.AsSpan();
        Stack<(RadixNode<TValue> Node, string KeySoFar)> stack = new();

        while (!remainingPrefix.IsEmpty)
        {
            char firstChar = remainingPrefix[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                // No matching prefix
                return results;
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingPrefix, label);

            if (matchLength < label.Length)
            {
                // Prefix doesn't fully match
                return results;
            }

            remainingPrefix = remainingPrefix[matchLength..];
            node = child;
        }

        // Perform DFS to collect all nodes under the prefix
        stack.Push((node, prefix));

        while (stack.Count > 0)
        {
            (RadixNode<TValue> currentNode, string keySoFar) = stack.Pop();

            if (currentNode.Value is not null)
            {
                results.Add(new KeyValuePair<string, TValue>(keySoFar, currentNode.Value));
            }

            foreach (RadixNode<TValue> child in currentNode.Children.Values)
            {
                string childKey = keySoFar + child.Prefix.ToString();
                stack.Push((child, childKey));
            }
        }

        return results;
    }

    public override bool Lookup(string x)
    {
        RadixNode<TValue> node = _root;
        ReadOnlySpan<char> remainingKey = x.AsSpan();

        while (!remainingKey.IsEmpty)
        {
            char firstChar = remainingKey[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                // No matching child, key doesn't exist
                return false;
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingKey, label);

            if (matchLength < label.Length)
            {
                // Partial match, key doesn't exist
                return false;
            }

            remainingKey = remainingKey[matchLength..];
            node = child;
        }

        return node.IsLeaf();
    }

    public override bool TryGetValue(string key, [MaybeNullWhen(false)] out TValue value)
    {
        RadixNode<TValue> node = _root;
        ReadOnlySpan<char> remainingKey = key.AsSpan();

        while (!remainingKey.IsEmpty)
        {
            char firstChar = remainingKey[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                value = default;
                return false;
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingKey, label);

            if (matchLength < label.Length)
            {
                value = default;
                return false;
            }

            remainingKey = remainingKey[matchLength..];
            node = child;
        }

        if (node.Value is not null)
        {
            value = node.Value;
            return true;
        }

        value = default;
        return false;
    }

    private static int CommonPrefixLength(
        ReadOnlySpan<char> span1,
        ReadOnlySpan<char> span2
    )
    {
        int minLength = Math.Min(span1.Length, span2.Length);
        int i = 0;
        while (i < minLength && span1[i] == span2[i])
        {
            i++;
        }
        return i;
    }

    protected virtual RadixNode<TValue> GetOrAddNode(
        ReadOnlySpan<char> key,
        TValue value
    )
    {
        RadixNode<TValue> node = _root;
        ReadOnlySpan<char> remainingKey = key;

        while (true)
        {
            if (remainingKey.IsEmpty)
            {
                // End of the key reached, assing the value
                node.Value = value;
                return node;
            }

            char firstChar = remainingKey[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                // Create a new node with the remaining key
                RadixNode<TValue> n = new(remainingKey.ToArray()) { Value = value };
                node.Children[firstChar] = n;
                return n;
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingKey, label);

            if (matchLength == label.Length)
            {
                // Full match of the label
                remainingKey = remainingKey[matchLength..];
                node = child;
                continue;
            }

            // Partial match, need to split the node
            char[] commonPrefix = remainingKey[..matchLength].ToArray();
            RadixNode<TValue> splitNode = new(commonPrefix);
            node.Children[firstChar] = splitNode;

            // Adjust the existing child
            char[] childSuffix = label[matchLength..].ToArray();
            child.Prefix = childSuffix;
            splitNode.Children[childSuffix[0]] = child;

            if (matchLength == remainingKey.Length)
            {
                // Remaining key matches the split point
                splitNode.Value = value;
                return splitNode;
            }

            // Create a new node for the remaining key
            char[] suffix = remainingKey[matchLength..].ToArray();
            RadixNode<TValue> newNode = new(suffix) { Value = value };
            splitNode.Children[suffix[0]] = newNode;

            return newNode;
        }
    }
}
