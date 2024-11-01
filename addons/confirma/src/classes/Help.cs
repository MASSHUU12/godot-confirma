using Confirma.Deserialization.Json;
using Confirma.Helpers;

namespace Confirma.Classes;

public static class Help
{
    public static async void ShowHelpPage(string pageName)
    {
        var file = await Json.LoadFromFile<HelpFile>($"{Plugin.GetPluginLocation()}src/help_pages/{pageName}.json");

        if (file is not null)
        {
            foreach(FileElement element in file.Data)
            {
                switch(element)
                {
                    case TextElement ele:

                        Log.Print(ele.GetText());
                        break;
                    case HeaderElement ele:
                        Log.Print(ele.GetText());
                        break;
                }
            }
            return;
        }

        Log.PrintError($"File: `{pageName}.json`, not found\n");
    }
}
