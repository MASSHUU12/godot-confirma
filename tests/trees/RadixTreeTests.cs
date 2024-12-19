using System.Collections.Generic;
using System.Linq;
using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Trees;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RadixTreeTests
{
    #region Add
    [TestCase]
    public void Add_SingleElement_ShouldBeFound()
    {
        RadixTree<int> tree = new()
        {
            { "test", 1 }
        };

        _ = tree.Lookup("test").ConfirmTrue("Element 'test' should exist in the tree.");
    }

    [TestCase]
    public void Add_MultipleElements_ShouldContainAll()
    {
        RadixTree<int> tree = new()
        {
            { "test", 1 },
            { "testing", 2 },
            { "tester", 3 }
        };

        _ = tree.Lookup("test").ConfirmTrue("'test' should exist.");
        _ = tree.Lookup("testing").ConfirmTrue("'testing' should exist.");
        _ = tree.Lookup("tester").ConfirmTrue("'tester' should exist.");
        _ = tree.Lookup("tes").ConfirmFalse("'tes' should not exist.");
    }

    [TestCase]
    public void Add_DuplicateKey_ShouldUpdateValue()
    {
        RadixTree<string> tree = new()
        {
            { "duplicate", "First Value" },
            { "duplicate", "Second Value" }
        };

        _ = tree.TryGetValue("duplicate", out string? value).ConfirmTrue();
        _ = value.ConfirmEqual("Second Value", "Value should be updated to 'Second Value'.");
    }
    #endregion Add

    #region Lookup
    [TestCase]
    public void Lookup_ExistingElement_ShouldReturnTrue()
    {
        RadixTree<int> tree = new()
        {
            { "test", 1 },
            { "testing", 2 },
            { "tester", 3 }
        };

        _ = tree.Lookup("testing").ConfirmTrue(
            "Element 'testing' should exist in the tree."
        );
    }

    [TestCase]
    public void Lookup_NonExistentElement_ShouldReturnFalse()
    {
        RadixTree<int> tree = new();

        _ = tree.Lookup("nonexistent").ConfirmFalse(
            "Element 'nonexistent' should not exist in the tree."
        );
    }
    #endregion Lookup

    #region TryGetValue
    [TestCase]
    public void TryGetValue_ExistingKey_ShouldReturnValue()
    {
        RadixTree<int> tree = new()
        {
            { "key", 42 }
        };

        _ = tree.TryGetValue("key", out int value).ConfirmTrue("Key 'key' should be found.");
        _ = value.ConfirmEqual(42, "Value should be 42.");
    }

    [TestCase]
    public void TryGetValue_NonExistingKey_ShouldReturnFalse()
    {
        RadixTree<int> tree = new();

        _ = tree.TryGetValue("missing", out int _).ConfirmFalse(
            "Key 'missing' should not be found."
        );
    }
    #endregion TryGetValue

    #region Remove
    [TestCase]
    public void Remove_ExistingKey_ShouldRemoveKey()
    {
        RadixTree<int> tree = new()
        {
            { "remove", 10 }
        };

        bool removed = tree.Remove("remove");
        bool exists = tree.Lookup("remove");

        _ = removed.ConfirmTrue("Key 'remove' should be removed.");
        _ = exists.ConfirmFalse("Key 'remove' should no longer exist.");
    }

    [TestCase]
    public void Remove_NonExistingKey_ShouldReturnFalse()
    {
        RadixTree<int> tree = new();

        _ = tree.Remove("nonexistent").ConfirmFalse(
            "Removing a non-existent key should return false."
        );
    }
    #endregion Remove

    #region Clear
    [TestCase]
    public void Clear_ShouldRemoveAllElements()
    {
        RadixTree<int> tree = new()
        {
            { "one", 1 },
            { "two", 2 }
        };

        tree.Clear();

        _ = tree.Lookup("one").ConfirmFalse(
            "Key 'one' should no longer exist after Clear."
        );
        _ = tree.Lookup("two").ConfirmFalse(
            "Key 'two' should no longer exist after Clear."
        );
    }
    #endregion Clear

    #region Search
    [TestCase]
    public void Search_WithPrefix_ShouldReturnMatchingElements()
    {
        RadixTree<string> tree = new()
        {
            { "apple", "fruit" },
            { "app", "application" },
            { "apricot", "fruit" },
            { "banana", "fruit" }
        };

        List<KeyValuePair<string, string>> results = tree.Search("ap").ToList();
        List<string> expectedKeys = new() { "ap", "app", "apple", "apricot" };

        _ = results.ConvertAll(static kvp => kvp.Key).ConfirmElementsAreEquivalent(
                expectedKeys,
                "Should return all keys starting with 'ap'."
            );
    }

    [TestCase]
    public void Search_NonExistingWithPrefix_ShoudReturnEmptyEnumerable()
    {
        RadixTree<string> tree = new()
        {
            { "apple", "fruit" },
            { "app", "application" },
            { "apricot", "fruit" },
            { "banana", "fruit" }
        };

        List<KeyValuePair<string, string>> results = tree.Search("ra").ToList();
        List<string> expectedKeys = new() { };

        _ = results.ConvertAll(static kvp => kvp.Key).ConfirmElementsAreEquivalent(
                expectedKeys,
                "Should return empty enumerable."
            );
    }
    #endregion Search

    #region FindSuccessor
    [TestCase]
    public void FindSuccessor_ExistingKey_ReturnsNextKey()
    {
        RadixTree<int> tree = new()
        {
            { "apple", 1 },
            { "application", 2 },
            { "banana", 3 },
        };

        RadixNode<int>? node = tree.FindSuccessor("apple");

        _ = node.ConfirmNotNull();
        _ = node!.GetFullKey().ConfirmEqual("application");
        _ = node!.Value.ConfirmEqual(2);
    }

    [TestCase]
    public void FindSuccessor_NonExistingKey_ReturnsCorrectSuccessor()
    {
        RadixTree<int> tree = new()
        {
            { "apple", 1 },
            { "application", 2 },
            { "banana", 3 },
        };

        RadixNode<int>? node = tree.FindSuccessor("app");

        _ = node.ConfirmNotNull();
        _ = node!.GetFullKey().ConfirmEqual("appl");
        _ = node!.Value.ConfirmEqual(2);
    }

    [TestCase]
    public void FindSuccessor_LastKey_ReturnsNull()
    {
        RadixTree<int> tree = new()
        {
            { "apple", 1 },
            { "application", 2 },
            { "banana", 3 },
            { "catalog", 7 },
            { "dog", 8 },
            { "dove", 9 },
        };

        RadixNode<int>? node = tree.FindSuccessor("dove");

        _ = node.ConfirmNull();
    }

    [TestCase]
    public void FindSuccessor_EmptyTree_ReturnsNull()
    {
        RadixTree<string> tree = new();

        _ = tree.FindSuccessor("anykey").ConfirmNull();
    }
    #endregion FindSuccessor

    #region FindPredecessor
    [TestCase]
    public void FindPredecessor_ExistingKey_ReturnsCorrectPredecessor()
    {
        RadixTree<string> tree = new()
        {
            { "apple", "Apple" },
            { "banana", "Banana" },
            { "cherry", "Cherry" },
            { "date", "Date" },
            { "fig", "Fig" }
        };

        RadixNode<string>? predecessor = tree.FindPredecessor("date");

        _ = predecessor.ConfirmNotNull();
        _ = predecessor!.GetFullKey().ConfirmEqual("cherry");
        _ = predecessor!.Value.ConfirmEqual("Cherry");
    }

    [TestCase]
    public void FindPredecessor_NonExistingKey_ReturnsNearestPredecessor()
    {
        RadixTree<string> tree = new()
        {
            { "alpha", "Alpha" },
            { "beta", "Beta" },
            { "delta", "Delta" },
            { "epsilon", "Epsilon" }
        };

        RadixNode<string>? predecessor = tree.FindPredecessor("gamma");

        _ = predecessor.ConfirmNotNull();
        _ = predecessor!.GetFullKey().ConfirmEqual("epsilon");
        _ = predecessor!.Value.ConfirmEqual("Epsilon");
    }

    [TestCase]
    public void FindPredecessor_SmallestKey_ReturnsNull()
    {
        RadixTree<string> tree = new()
        {
            { "aardvark", "Aardvark" },
            { "zebra", "Zebra" }
        };

        RadixNode<string>? predecessor = tree.FindPredecessor("aardvark");

        _ = predecessor.ConfirmNull();
    }

    [TestCase]
    public void FindPredecessor_EmptyTree_ReturnsNull()
    {
        RadixTree<string> tree = new();

        _ = tree.FindPredecessor("anykey").ConfirmNull();
    }

    [TestCase]
    public void FindPredecessor_KeyGreaterThanAll_ReturnsLastKey()
    {
        RadixTree<string> tree = new()
        {
            { "apple", "Apple" },
            { "banana", "Banana" },
            { "cherry", "Cherry" }
        };

        RadixNode<string>? predecessor = tree.FindPredecessor("date");

        _ = predecessor.ConfirmNotNull();
        _ = predecessor!.GetFullKey().ConfirmEqual("cherry");
        _ = predecessor!.Value.ConfirmEqual("Cherry");
    }
    #endregion FindPredecessor
}
