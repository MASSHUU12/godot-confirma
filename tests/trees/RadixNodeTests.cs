using Confirma.Attributes;
using Confirma.Extensions;
using Confirma.Trees;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class RadixNodeTests
{
    #region Constructor
    [TestCase]
    public void Constructor_Default_InitializesCorrectly()
    {
        RadixNode<int> node = new();

        _ = node.Prefix.Length.ConfirmEqual(0);
        _ = node.Value.ConfirmDefaultValue();
        _ = node.Children.ConfirmNotNull();
        _ = node.Children.Count.ConfirmEqual(0);
        _ = node.Parent.ConfirmNull();
    }

    [TestCase]
    public void Constructor_WithPrefix_InitializesCorrectly()
    {
        char[] prefix = "test".ToCharArray();
        RadixNode<int> node = new(prefix);

        _ = node.Prefix.ToString().ConfirmEqual("test");
        _ = node.Value.ConfirmDefaultValue();
        _ = node.Children.ConfirmNotNull();
        _ = node.Children.Count.ConfirmEqual(0);
        _ = node.Parent.ConfirmNull();
    }

    [TestCase]
    public void Constructor_WithPrefixAndParent_InitializesCorrectly()
    {
        char[] prefix = "child".ToCharArray();

        RadixNode<int> parent = new("parent".ToCharArray());
        RadixNode<int> node = new(prefix, parent);

        _ = node.Prefix.ToString().ConfirmEqual("child");
        _ = parent.Prefix.ToString().ConfirmEqual("parent");

        _ = node.Value.ConfirmDefaultValue();
        _ = parent.Value.ConfirmDefaultValue();

        _ = parent.ConfirmSameReference(node.Parent!);
    }
    #endregion Constructor

    #region IsLeaf
    [TestCase]
    public void IsLeaf_NoChildren_ReturnsTrue()
    {
        RadixNode<int> node = new("key".ToCharArray())
        {
            Value = 42,
            HasValue = true
        };

        _ = node.IsLeaf().ConfirmTrue();
    }

    [TestCase]
    public void IsLeaf_HasChildren_ReturnsFalse()
    {
        RadixNode<int> node = new();
        node.Children.Add('a', new("a".ToCharArray(), node));
        node.HasValue = false;

        _ = node.IsLeaf().ConfirmFalse();
    }
    #endregion IsLeaf

    #region GetFullKey
    [TestCase]
    public void RadixNode_GetFullKey_ReturnsCorrectKey()
    {
        RadixNode<int> root = new();
        RadixNode<int> node1 = new("pre".ToCharArray(), root);
        RadixNode<int> node2 = new("fix".ToCharArray(), node1);
        RadixNode<int> node3 = new("es".ToCharArray(), node2);

        _ = node3.GetFullKey().ConfirmEqual("prefixes");
    }
    #endregion GetFullKey
}
