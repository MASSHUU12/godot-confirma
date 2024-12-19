using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Confirma.Trees;

/// <summary>
/// Represents a radix tree data structure, also known as a prefix tree or trie.
/// A radix tree is a tree-like data structure that is often used to store a
/// dynamic set or associative array where the keys are usually strings.
/// </summary>
/// <typeparam name="TValue">The type of the values stored in the tree.</typeparam>
public class RadixTree<TValue> : PrefixTree<TValue>
{
    public RadixNode<TValue> Root { get; } = new();

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
        Root.Children.Clear();
        Root.Value = default;
    }

    public override bool Remove(string key)
    {
        return Remove(Root, key.AsSpan(), out bool _);
    }

    public override bool Remove(KeyValuePair<string, TValue> item)
    {
        return Remove(Root, item.Key, out bool _);
    }

    private bool Remove(
        RadixNode<TValue> node,
        ReadOnlySpan<char> key,
        out bool shouldDeleteNode
    )
    {
        shouldDeleteNode = false;

        if (key.IsEmpty)
        {
            if (node.Value is not null)
            {
                // Found the node to remove
                node.Value = default;
                // If node has no children, indicate it can be deleted
                shouldDeleteNode = node.Children.Count == 0;
                return true;
            }
            // Key not found
            return false;
        }

        char firstChar = key[0];

        if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
        {
            // Key not found
            return false;
        }

        ReadOnlySpan<char> label = child.Prefix.Span;
        int matchLength = CommonPrefixLength(key, label);

        if (matchLength < label.Length)
        {
            // Key not found
            return false;
        }

        ReadOnlySpan<char> remainingKey = key[matchLength..];

        bool result = Remove(child, remainingKey, out bool shouldDeleteChild);

        if (shouldDeleteChild)
        {
            _ = node.Children.Remove(firstChar);
        }

        // Check if current node should be merged or deleted
        if (node != Root && node.Value is null && node.Children.Count == 1)
        {
            // Merge with the child node
            RadixNode<TValue> singleChild = node.Children.Values.First();
            node.Prefix = Concat(node.Prefix, singleChild.Prefix);
            node.Value = singleChild.Value;
            node.Children = singleChild.Children;
        }

        shouldDeleteNode = node != Root
            && node.Value is null
            && node.Children.Count == 0;
        return result;
    }

    public override IEnumerable<KeyValuePair<string, TValue>> Search(string prefix)
    {
        List<KeyValuePair<string, TValue>> results = new();
        RadixNode<TValue>? node = Root;
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
        RadixNode<TValue> node = Root;
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
        RadixNode<TValue> node = Root;
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

    public RadixNode<TValue>? FindSuccessor(string key)
    {
        RadixNode<TValue>? node = Root;
        ReadOnlySpan<char> remainingKey = key.AsSpan();
        string accumulatedKey = string.Empty;

        while (!remainingKey.IsEmpty)
        {
            char firstChar = remainingKey[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                break; // No exact match; need to find the next node in order
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingKey, label);

            accumulatedKey += label[..matchLength].ToString();
            remainingKey = remainingKey[matchLength..];

            if (matchLength < label.Length)
            {
                // Partial match; explore this child for successor
                node = child;
                accumulatedKey += label[matchLength..].ToString();
                return FindLeftmost(node, accumulatedKey);
            }

            node = child;
        }

        // If the node has a value and it's not the key itself, return it
        if (node is not null && accumulatedKey != key)
        {
            return node;
        }

        // Find the next node in lexicographical order
        return node is not null ? FindNextNode(node, accumulatedKey) : null;
    }

    private RadixNode<TValue>? FindNextNode(RadixNode<TValue>? node, string accumulatedKey)
    {
        // If there are children, find the leftmost (smallest) child
        if (node?.Children.Count > 0)
        {
            RadixNode<TValue> firstChild = node.Children
                .OrderBy(c => c.Key)
                .First().Value;
            accumulatedKey += firstChild.Prefix.Span.ToString();
            return FindLeftmost(firstChild, accumulatedKey);
        }

        while (node != Root)
        {
            char lastChar = accumulatedKey[^1];
            accumulatedKey = accumulatedKey[..^1];
            node = GetParentNode(Root, accumulatedKey.AsSpan());

            if (node is null)
            {
                return null;
            }

            var siblings = node.Children
                .Where(c => c.Key > lastChar)
                .OrderBy(c => c.Key);
            if (siblings.Any())
            {
                RadixNode<TValue> nextSibling = siblings.First().Value;
                accumulatedKey += nextSibling.Prefix.Span.ToString();
                return FindLeftmost(nextSibling, accumulatedKey);
            }
        }

        return null;
    }

    private static RadixNode<TValue>? FindLeftmost(
        RadixNode<TValue> node,
        string accumulatedKey
    )
    {
        while (true)
        {
            if (node.Value is not null)
            {
                return node;
            }

            if (node.Children.Count == 0)
            {
                return null;
            }

            RadixNode<TValue> firstChild = node.Children
                .OrderBy(static c => c.Key)
                .First().Value;
            accumulatedKey += firstChild.Prefix.Span.ToString();
            node = firstChild;
        }
    }

    private static RadixNode<TValue>? GetParentNode(
        RadixNode<TValue> currentNode,
        ReadOnlySpan<char> key
    )
    {
        RadixNode<TValue> node = currentNode;
        ReadOnlySpan<char> remainingKey = key;

        while (!remainingKey.IsEmpty)
        {
            char firstChar = remainingKey[0];

            if (!node.Children.TryGetValue(firstChar, out RadixNode<TValue>? child))
            {
                return null;
            }

            ReadOnlySpan<char> label = child.Prefix.Span;
            int matchLength = CommonPrefixLength(remainingKey, label);
            remainingKey = remainingKey[matchLength..];
            node = child;
        }

        return node;
    }

    public RadixNode<TValue> FindPredecessor(string key)
    {
        throw new NotImplementedException();
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

    private static char[] Concat(
        ReadOnlyMemory<char> prefix1,
        ReadOnlyMemory<char> prefix2
    )
    {
        char[] newPrefix = new char[prefix1.Length + prefix2.Length];
        prefix1.Span.CopyTo(newPrefix);
        prefix2.Span.CopyTo(newPrefix.AsSpan(prefix1.Length));
        return newPrefix;
    }

    private RadixNode<TValue> GetOrAddNode(
        ReadOnlySpan<char> key,
        TValue value
    )
    {
        RadixNode<TValue> node = Root;
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
                RadixNode<TValue> n = new(remainingKey.ToArray())
                {
                    Value = value,
                    Parent = node
                };
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
            RadixNode<TValue> splitNode = new(commonPrefix)
            {
                Value = value,
                Parent = node
            };
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
            RadixNode<TValue> newNode = new(suffix)
            {
                Value = value,
                Parent = node
            };
            splitNode.Children[suffix[0]] = newNode;

            return newNode;
        }
    }
}