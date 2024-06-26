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
		((StringName)path).ConfirmIsFile();
	}

	[TestCase("./tests/ConfirmFileTest.css")]
	public static void ConfirmIsFile_WhenFileDoesNotExist(string path)
	{
		Action action = () => ((StringName)path).ConfirmIsFile();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmIsNotFile
	[TestCase("./tests/ConfirmFileTest.css")]
	public static void ConfirmIsNotFile_WhenFileDoesNotExist(string path)
	{
		((StringName)path).ConfirmIsNotFile();
	}

	[TestCase("./tests/ConfirmFileTest.cs")]
	public static void ConfirmIsNotFile_WhenFileExists(string path)
	{
		Action action = () => ((StringName)path).ConfirmIsNotFile();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmIsDirectory
	[TestCase("./tests")]
	public static void ConfirmIsDirectory_WhenDirectoryExists(string path)
	{
		((StringName)path).ConfirmIsDirectory();
	}

	[TestCase("./test")]
	public static void ConfirmIsDirectory_WhenDirectoryDoesNotExist(string path)
	{
		Action action = () => ((StringName)path).ConfirmIsDirectory();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmIsNotDirectory
	[TestCase("./test")]
	public static void ConfirmIsNotDirectory_WhenDirectoryDoesNotExist(string path)
	{
		((StringName)path).ConfirmIsNotDirectory();
	}

	[TestCase("./tests")]
	public static void ConfirmIsNotDirectory_WhenDirectoryExists(string path)
	{
		Action action = () => ((StringName)path).ConfirmIsNotDirectory();

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileContains
	[TestCase("./tests/ConfirmFileTest.cs", "ConfirmFileTest")]
	public static void ConfirmFileContains_WhenFileContainsContent(string path, string content)
	{
		((StringName)path).ConfirmFileContains(content);
	}

	[TestCase("./LICENSE", "ConfirmFileTest.cs")]
	public static void ConfirmFileContains_WhenFileDoesNotContainContent(string path, string content)
	{
		Action action = () => ((StringName)path).ConfirmFileContains(content);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileDoesNotContain
	[TestCase("./LICENSE", "ConfirmFileTest.cs")]
	public static void ConfirmFileDoesNotContain_WhenFileDoesNotContainContent(string path, string content)
	{
		((StringName)path).ConfirmFileDoesNotContain(content);
	}

	[TestCase("./tests/ConfirmFileTest.cs", "ConfirmFileTest")]
	public static void ConfirmFileDoesNotContain_WhenFileContainsContent(string path, string content)
	{
		Action action = () => ((StringName)path).ConfirmFileDoesNotContain(content);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileHasLength
	[TestCase("./LICENSE", 1066)]
	public static void ConfirmFileHasLength_WhenFileHasLength(string path, long length)
	{
		((StringName)path).ConfirmFileHasLength(length);
	}

	[TestCase("./LICENSE", 1)]
	public static void ConfirmFileHasLength_WhenFileDoesNotHaveLength(string path, long length)
	{
		Action action = () => ((StringName)path).ConfirmFileHasLength(length);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileDoesNotHaveLength
	[TestCase("./LICENSE", 1)]
	public static void ConfirmFileDoesNotHaveLength_WhenFileDoesNotHaveLength(string path, long length)
	{
		((StringName)path).ConfirmFileDoesNotHaveLength(length);
	}

	[TestCase("./LICENSE", 1066)]
	public static void ConfirmFileDoesNotHaveLength_WhenFileHasLength(string path, long length)
	{
		Action action = () => ((StringName)path).ConfirmFileDoesNotHaveLength(length);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileHasAttributes
	[TestCase("./LICENSE", FileAttributes.Normal)]
	public static void ConfirmFileHasAttributes_WhenFileHasAttributes(string path, FileAttributes attributes)
	{
		((StringName)path).ConfirmFileHasAttributes(attributes);
	}

	[TestCase("./LICENSE", FileAttributes.Hidden)]
	public static void ConfirmFileHasAttributes_WhenFileDoesNotHaveAttributes(string path, FileAttributes attributes)
	{
		Action action = () => ((StringName)path).ConfirmFileHasAttributes(attributes);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion

	#region ConfirmFileDoesNotHaveAttributes
	[TestCase("./LICENSE", FileAttributes.Hidden)]
	public static void ConfirmFileDoesNotHaveAttributes_WhenFileDoesNotHaveAttributes(string path, FileAttributes attributes)
	{
		((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);
	}

	[TestCase("./LICENSE", FileAttributes.Normal)]
	public static void ConfirmFileDoesNotHaveAttributes_WhenFileHasAttributes(string path, FileAttributes attributes)
	{
		Action action = () => ((StringName)path).ConfirmFileDoesNotHaveAttributes(attributes);

		action.ConfirmThrows<ConfirmAssertException>();
	}
	#endregion
}
