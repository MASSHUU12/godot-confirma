using Confirma.Helpers;
using Godot;

namespace Confirma.Classes;

public static class Help
{
    public static void ShowHelpPage(string pageName)
    {
        FileAccess page = FileAccess.Open($"{Plugin.GetPluginLocation()}/src/help_pages/{pageName}.txt", FileAccess.ModeFlags.Read);

        if (page is not null)
        {
            Log.Print(page.GetAsText());
            return;
        }

        //fixme debug, remove this before merging to master
        Log.PrintError($"File: `{pageName}.txt`, not found\n");
        Log.PrintLine($"{Plugin.GetPluginLocation()}/src/help_pages/{pageName}.txt");
    }
}
