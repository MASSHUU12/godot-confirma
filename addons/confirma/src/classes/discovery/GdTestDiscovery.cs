using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Confirma.Classes.Discovery;

public static class GdTestDiscovery
{
    private static bool? _testScriptsDirectoryCached = null;

    public static IEnumerable<ScriptInfo> GetTestScripts(
        string pathToTests,
        int maxDepth = 16
    )
    {
        _testScriptsDirectoryCached ??= Directory.Exists(pathToTests);

        if (_testScriptsDirectoryCached == false)
        {
            yield break;
        }

        foreach (string filePath in Directory.EnumerateFiles(pathToTests))
        {
            if (!filePath.EndsWith(".gd", StringComparison.Ordinal))
            {
                continue;
            }

            GDScript script = GD.Load<GDScript>(filePath);

            // For some reason, when running plugin in the editor,
            // the scripts are considered impossible to instantiate.
            if (!script.CanInstantiate() && !Engine.IsEditorHint())
            {
                script.Dispose();
                continue;
            }

            yield return ScriptInfo.Parse(script);
        }

        if (maxDepth > 1)
        {
            foreach (string dirPath in Directory.EnumerateDirectories(pathToTests))
            {
                foreach (ScriptInfo scriptInfo in GetTestScripts(dirPath, maxDepth - 1))
                {
                    yield return scriptInfo;
                }
            }
        }
    }
}
