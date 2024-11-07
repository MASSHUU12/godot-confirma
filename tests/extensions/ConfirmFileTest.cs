using System;
using System.IO;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class ConfirmFileTest
{
    private const string _thisFileLocation = "./tests/extensions/ConfirmFileTest.cs";

    #region ConfirmIsFile
    [TestCase(_thisFileLocation)]
    public void ConfirmIsFile_WhenFileExists(string path)
    {
        _ = ((StringName)path).ConfirmIsFile();
    }

    [TestCase(_thisFileLocation + ".d")]
    public void ConfirmIsFile_WhenFileDoesNotExist(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsFile();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsFile failed: "
            + $"Expected file \"{path}\" to exist."
        );
    }
    #endregion ConfirmIsFile

    #region ConfirmIsNotFile
    [TestCase(_thisFileLocation + ".d")]
    public void ConfirmIsNotFile_WhenFileDoesNotExist(string path)
    {
        _ = ((StringName)path).ConfirmIsNotFile();
    }

    [TestCase(_thisFileLocation)]
    public void ConfirmIsNotFile_WhenFileExists(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsNotFile();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotFile failed: "
            + $"Expected file \"{path}\" to not exist."
        );
    }
    #endregion ConfirmIsNotFile

    #region ConfirmIsDirectory
    [TestCase("./tests")]
    public void ConfirmIsDirectory_WhenDirectoryExists(string path)
    {
        _ = ((StringName)path).ConfirmIsDirectory();
    }

    [TestCase("./test")]
    public void ConfirmIsDirectory_WhenDirectoryDoesNotExist(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsDirectory();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsDirectory failed: "
            + $"Expected directory \"{path}\" to exist."
        );
    }
    #endregion ConfirmIsDirectory

    #region ConfirmIsNotDirectory
    [TestCase("./test")]
    public void ConfirmIsNotDirectory_WhenDirectoryDoesNotExist(string path)
    {
        _ = ((StringName)path).ConfirmIsNotDirectory();
    }

    [TestCase("./tests")]
    public void ConfirmIsNotDirectory_WhenDirectoryExists(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsNotDirectory();

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmIsNotDirectory failed: "
            + $"Expected directory \"{path}\" to not exist."
        );
    }
    #endregion ConfirmIsNotDirectory

    #region ConfirmFileContains
    [TestCase(_thisFileLocation, "ConfirmFileTest")]
    public void ConfirmFileContains_WhenFileContainsContent(
        string path,
        string content
    )
    {
        _ = ((StringName)path).ConfirmFileContains(content);
    }

    [TestCase("./LICENSE", "ConfirmFileTest.cs")]
    public void ConfirmFileContains_WhenFileDoesNotContainContent(
        string path,
        string content
    )
    {
        Action action = () => ((StringName)path).ConfirmFileContains(content);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileContains failed: "
            + $"Expected file \"{path}\" to contain \"{content}\"."
        );
    }
    #endregion ConfirmFileContains

    #region ConfirmFileDoesNotContain
    [TestCase("./LICENSE", "ConfirmFileTest.cs")]
    public void ConfirmFileDoesNotContain_WhenFileDoesNotContainContent(
        string path,
        string content
    )
    {
        _ = ((StringName)path).ConfirmFileDoesNotContain(content);
    }

    [TestCase(_thisFileLocation, "ConfirmFileTest")]
    public void ConfirmFileDoesNotContain_WhenFileContainsContent(
        string path,
        string content
    )
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotContain(content);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileDoesNotContain failed: "
            + $"Expected file \"{path}\" to not contain \"{content}\"."
        );
    }
    #endregion ConfirmFileDoesNotContain

    #region ConfirmFileHasLength
    [TestCase("./LICENSE", 1066)]
    public void ConfirmFileHasLength_WhenFileHasLength(
        string path,
        long length
    )
    {
        _ = ((StringName)path).ConfirmFileHasLength(length);
    }

    [TestCase("./LICENSE", 1)]
    public void ConfirmFileHasLength_WhenFileDoesNotHaveLength(
        string path,
        long length
    )
    {
        Action action = () => ((StringName)path).ConfirmFileHasLength(length);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileHasLength failed: "
            + $"Expected file \"{path}\" to has length 1, but got 1,066."
        );
    }
    #endregion ConfirmFileHasLength

    #region ConfirmFileDoesNotHaveLength
    [TestCase("./LICENSE", 1)]
    public void ConfirmFileDoesNotHaveLength_WhenFileDoesNotHaveLength(
        string path,
        long length
    )
    {
        _ = ((StringName)path).ConfirmFileDoesNotHaveLength(length);
    }

    [TestCase("./LICENSE", 1066)]
    public void ConfirmFileDoesNotHaveLength_WhenFileHasLength(
        string path,
        long length
    )
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotHaveLength(length);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileDoesNotHaveLength failed: "
            + $"Expected file \"{path}\" to not have length of 1,066"
        );
    }
    #endregion ConfirmFileDoesNotHaveLength

    #region ConfirmFileHasAttributes
    [TestCase("./LICENSE", FileAttributes.Normal)]
    public void ConfirmFileHasAttributes_WhenFileHasAttributes(
        string path,
        FileAttributes attributes
    )
    {
        _ = ((StringName)path).ConfirmFileHasAttributes(attributes);
    }

    [TestCase("./LICENSE", FileAttributes.Hidden)]
    public void ConfirmFileHasAttributes_WhenFileDoesNotHaveAttributes(
        string path,
        FileAttributes attributes
    )
    {
        Action action = () => ((StringName)path).ConfirmFileHasAttributes(attributes);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileDoesNotContain failed: "
            + $"Expected file \"{path}\" to have attributes \"Hidden\"."
        );
    }
    #endregion ConfirmFileHasAttributes

    #region ConfirmFileDoesNotHaveAttributes
    [TestCase("./LICENSE", FileAttributes.Hidden)]
    public void ConfirmFileDoesNotHaveAttributes_WhenFileDoesNotHaveAttributes(
        string path,
        FileAttributes attributes
    )
    {
        _ = ((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);
    }

    [TestCase("./LICENSE", FileAttributes.Normal)]
    public void ConfirmFileDoesNotHaveAttributes_WhenFileHasAttributes(
        string path,
        FileAttributes attributes
    )
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);

        _ = action.ConfirmThrowsWMessage<ConfirmAssertException>(
            "Assertion ConfirmFileDoesNotContain failed: "
            + $"Expected file \"{path}\" to not have attributes \"Normal\"."
        );
    }
    #endregion ConfirmFileDoesNotHaveAttributes
}
