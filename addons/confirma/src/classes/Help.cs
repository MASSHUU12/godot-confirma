using Confirma.Deserialization.Json;
using Confirma.Helpers;
using Confirma.Classes.HelpElements;

namespace Confirma.Classes;

public static class Help
{
    public static async void ShowHelpPage(string pageName)
    {
        var file = await Json.LoadFromFile<HelpFile>($"{Plugin.GetPluginLocation()}docs/help_pages/{pageName}.json");

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
                        Log.PrintLine($"\n{ele.GetText()}");
                        break;
                    case CodeElement ele:
                        Log.Print(ele.GetText());
                        break;
                    case LinkElement ele:
                        Log.Print(ele.GetText());
                        break;
                }
            }
            return;
        }

        Log.Print(Colors.ColorText($"Page: `{pageName}`, not found\n", Colors.Error));
    }
}
