using System.Collections.Generic;
using System.Text.Json.Serialization;
using Confirma.Classes;

namespace Confirma.Deserialization.Json;

public class HelpFile
{
    [JsonPropertyName("version")]
    public int Version { get; set; }
    [JsonPropertyName("data")]
    public Dictionary<int, TextElement> Data { get; set; } = new Dictionary<int, TextElement>();

}
