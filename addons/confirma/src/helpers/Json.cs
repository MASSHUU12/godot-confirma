using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Confirma.Helpers;

public static class Json
{
    private static JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public static async Task<bool> DumpToFileAsync<T>(
        string fileName,
        T data,
        bool pretty
    )
    {
        if (options.WriteIndented != pretty)
        {
            options = new()
            {
                WriteIndented = pretty
            };
        }

        try
        {
            await using FileStream stream = new(fileName, FileMode.Create);
            await JsonSerializer.SerializeAsync(stream, data, options);
        }
        catch (Exception e) when (
            e is FileNotFoundException
            or UnauthorizedAccessException
            or IOException
            or DirectoryNotFoundException
            or PathTooLongException
        )
        {
            return false;
        }

        return true;
    }
}
