using System;
using System.IO;
using Confirma.Attributes;
using Confirma.Exceptions;
using Confirma.Extensions;
using Godot;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public static class ConfirmFileTest
{
    #region ConfirmIsFile
    [TestCase("./tests/ConfirmFileTest.cs")]
    public static void ConfirmIsFile_WhenFileExists(string path)
    {
        _ = ((StringName)path).ConfirmIsFile();
    }

    [TestCase("./tests/ConfirmFileTest.css")]
    public static void ConfirmIsFile_WhenFileDoesNotExist(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsFile();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsFile

    #region ConfirmIsNotFile
    [TestCase("./tests/ConfirmFileTest.css")]
    public static void ConfirmIsNotFile_WhenFileDoesNotExist(string path)
    {
        _ = ((StringName)path).ConfirmIsNotFile();
    }

    [TestCase("./tests/ConfirmFileTest.cs")]
    public static void ConfirmIsNotFile_WhenFileExists(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsNotFile();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsNotFile

    #region ConfirmIsDirectory
    [TestCase("./tests")]
    public static void ConfirmIsDirectory_WhenDirectoryExists(string path)
    {
        _ = ((StringName)path).ConfirmIsDirectory();
    }

    [TestCase("./test")]
    public static void ConfirmIsDirectory_WhenDirectoryDoesNotExist(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsDirectory();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsDirectory

    #region ConfirmIsNotDirectory
    [TestCase("./test")]
    public static void ConfirmIsNotDirectory_WhenDirectoryDoesNotExist(string path)
    {
        _ = ((StringName)path).ConfirmIsNotDirectory();
    }

    [TestCase("./tests")]
    public static void ConfirmIsNotDirectory_WhenDirectoryExists(string path)
    {
        Action action = () => ((StringName)path).ConfirmIsNotDirectory();

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmIsNotDirectory

    #region ConfirmFileContains
    [TestCase("./tests/ConfirmFileTest.cs", "ConfirmFileTest")]
    public static void ConfirmFileContains_WhenFileContainsContent(string path, string content)
    {
        _ = ((StringName)path).ConfirmFileContains(content);
    }

    [TestCase("./LICENSE", "ConfirmFileTest.cs")]
    public static void ConfirmFileContains_WhenFileDoesNotContainContent(string path, string content)
    {
        Action action = () => ((StringName)path).ConfirmFileContains(content);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileContains

    #region ConfirmFileDoesNotContain
    [TestCase("./LICENSE", "ConfirmFileTest.cs")]
    public static void ConfirmFileDoesNotContain_WhenFileDoesNotContainContent(string path, string content)
    {
        _ = ((StringName)path).ConfirmFileDoesNotContain(content);
    }

    [TestCase("./tests/ConfirmFileTest.cs", "ConfirmFileTest")]
    public static void ConfirmFileDoesNotContain_WhenFileContainsContent(string path, string content)
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotContain(content);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileDoesNotContain

    #region ConfirmFileHasLength
    [TestCase("./LICENSE", 1066)]
    public static void ConfirmFileHasLength_WhenFileHasLength(string path, long length)
    {
        _ = ((StringName)path).ConfirmFileHasLength(length);
    }

    [TestCase("./LICENSE", 1)]
    public static void ConfirmFileHasLength_WhenFileDoesNotHaveLength(string path, long length)
    {
        Action action = () => ((StringName)path).ConfirmFileHasLength(length);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileHasLength

    #region ConfirmFileDoesNotHaveLength
    [TestCase("./LICENSE", 1)]
    public static void ConfirmFileDoesNotHaveLength_WhenFileDoesNotHaveLength(string path, long length)
    {
        _ = ((StringName)path).ConfirmFileDoesNotHaveLength(length);
    }

    [TestCase("./LICENSE", 1066)]
    public static void ConfirmFileDoesNotHaveLength_WhenFileHasLength(string path, long length)
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotHaveLength(length);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileDoesNotHaveLength

    #region ConfirmFileHasAttributes
    [TestCase("./LICENSE", FileAttributes.Normal)]
    public static void ConfirmFileHasAttributes_WhenFileHasAttributes(string path, FileAttributes attributes)
    {
        _ = ((StringName)path).ConfirmFileHasAttributes(attributes);
    }

    [TestCase("./LICENSE", FileAttributes.Hidden)]
    public static void ConfirmFileHasAttributes_WhenFileDoesNotHaveAttributes(string path, FileAttributes attributes)
    {
        Action action = () => ((StringName)path).ConfirmFileHasAttributes(attributes);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileHasAttributes

    #region ConfirmFileDoesNotHaveAttributes
    [TestCase("./LICENSE", FileAttributes.Hidden)]
    public static void ConfirmFileDoesNotHaveAttributes_WhenFileDoesNotHaveAttributes(string path, FileAttributes attributes)
    {
        _ = ((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);
    }

    [TestCase("./LICENSE", FileAttributes.Normal)]
    public static void ConfirmFileDoesNotHaveAttributes_WhenFileHasAttributes(string path, FileAttributes attributes)
    {
        Action action = () => ((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);

        _ = action.ConfirmThrows<ConfirmAssertException>();
    }
    #endregion ConfirmFileDoesNotHaveAttributes
}
