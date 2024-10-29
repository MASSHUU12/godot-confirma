using System.Text.Json.Serialization;
using Confirma.Classes;

namespace Confirma.Deserialization.Json;

public class HelpFile
{
    public int Version { get; set; }
    public TextElement[] Data { get; set; } = System.Array.Empty<TextElement>();
}
