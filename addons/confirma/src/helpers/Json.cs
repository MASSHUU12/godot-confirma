using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Confirma.Helpers;

public static class Json
{
    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true
    };

    public static async Task DumpToFileAsync<T>(
        string fileName,
        T data,
        bool pretty
    )
    {
        options.WriteIndented = pretty;

        await using FileStream stream = new(fileName, FileMode.Create);
        await JsonSerializer.SerializeAsync(stream, data, options);
    }
}
