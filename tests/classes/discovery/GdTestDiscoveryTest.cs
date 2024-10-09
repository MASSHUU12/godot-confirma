using System;
using System.Collections.Generic;
using System.IO;
using Confirma.Attributes;
using Confirma.Classes;
using Confirma.Classes.Discovery;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class GdTestDiscoveryTest
{
    private string CreateTempDirectory()
    {
        string pathToTests = Path.GetTempPath();
        string tempDir = Path.Combine(pathToTests, Guid.NewGuid().ToString());
        _ = Directory.CreateDirectory(tempDir);

        return tempDir;
    }

    #region GetTestScripts
    [TestCase]
    [Ignore(Reason = "Execution breaks Confirma.")]
    public void GetTestScripts_DirectoryDoesNotExist_ReturnsEmpty()
    {
        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts("non-existent-path");

        _ = testScripts.ConfirmEmpty();
    }

    [TestCase]
    public void GetTestScripts_DirectoryIsEmpty_ReturnsEmpty()
    {
        string tempDir = CreateTempDirectory();

        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts(tempDir);

        _ = testScripts.ConfirmEmpty();

        // Clean up
        Directory.Delete(tempDir);
    }

    [TestCase]
    public void GetTestScripts_DirectoryContainsNonGdFiles_ReturnsEmpty()
    {
        string tempDir = CreateTempDirectory();
        File.Create(Path.Combine(tempDir, "non-gd-file.txt")).Dispose();

        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts(tempDir);

        _ = testScripts.ConfirmEmpty();

        // Clean up
        Directory.Delete(tempDir, true);
    }

    [TestCase]
    public void GetTestScripts_DirectoryContainsGdFileButNotTestClass_ReturnsEmpty()
    {
        string tempDir = CreateTempDirectory();
        File.Create(Path.Combine(tempDir, "gd-file.gd")).Dispose();

        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts(tempDir);

        _ = testScripts.ConfirmEmpty();

        // Clean up
        Directory.Delete(tempDir, true);
    }

    [TestCase]
    public void GetTestScripts_DirectoryContainsTestClass_ReturnsTestClass()
    {
        string tempDir = CreateTempDirectory();
        File.WriteAllText(
            Path.Combine(tempDir, "test-class.gd"),
            "extends TestClass"
        );

        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts(tempDir);

        _ = testScripts.ConfirmCount(1);

        // Clean up
        Directory.Delete(tempDir, true);
    }

    [TestCase]
    public void GetTestScripts_DirectoryContainsTestClassInSubdirectory_ReturnsTestClass()
    {
        string tempDir = CreateTempDirectory();
        string subDir = Path.Combine(tempDir, Guid.NewGuid().ToString());
        _ = Directory.CreateDirectory(subDir);
        File.WriteAllText(
            Path.Combine(subDir, "test-class.gd"),
            "extends TestClass"
        );

        IEnumerable<GdScriptInfo> testScripts = GdTestDiscovery
            .GetTestScripts(tempDir);

        _ = testScripts.ConfirmCount(1);

        // Clean up
        Directory.Delete(tempDir, true);
    }
    #endregion GetTestScripts
}
