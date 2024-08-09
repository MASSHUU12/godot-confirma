using System;
using System.Collections.Generic;
using System.IO;
using Godot;

namespace Confirma.Classes.Discovery;

public static class GdTestDiscovery
{
    public static IEnumerable<GDScript> GetTestScripts(string pathToTests)
    {
        foreach (string filePath in Directory.EnumerateFiles(pathToTests))
        {
            if (!filePath.EndsWith(".gd", StringComparison.Ordinal))
            {
                continue;
            }

            GDScript script = GD.Load<GDScript>(filePath);

            if (!script.CanInstantiate())
            {
                script.Dispose();
                continue;
            }

            yield return script;
        }
    }
}
