using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Confirma.Classes.HelpElements;

namespace Confirma.Deserialization.Json;

public class FileElementConverter : JsonConverter<FileElement>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(FileElement).IsAssignableFrom(typeToConvert);
    }

    public override FileElement? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        reader.Read();

        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException($"Expected \"property\", but got \"{reader.TokenType}\"");
        }

        string? propertyName = reader.GetString();

        if (propertyName != "type")
        {
            throw new JsonException($"Expected \"type\" property, but got \"{propertyName}\"");
        }

        reader.Read();

        switch(reader.GetString())
        {
            case "text":
                return ConvertText(ref reader);
            case "header":
                return ConvertHeader(ref reader);
            case "code":
                return ConvertCode(ref reader, options);
            case "link":
                return ConvertLink(ref reader);
            default:
                throw new NotSupportedException($"Unsupported type, \"{reader.GetString()}\"");
        }
    }

    public override void Write(Utf8JsonWriter writer, FileElement value, JsonSerializerOptions options)
    {
        //todo
        throw new NotImplementedException();
    }

#region Type converters
    public static FileElement ConvertText (ref Utf8JsonReader reader)
    {
        TextElement element = new();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string? property = reader.GetString();
                reader.Read();

                switch (property)
                {
                    case "text":
                        element.Text = reader.GetString() ?? throw new JsonException();
                        break;
                    case "color":
                        element.Color = reader.GetString() ?? throw new JsonException();
                        break;
                    case "bg_color":
                        element.BgColor = reader.GetString() ?? throw new JsonException();
                        break;
                    case "format":
                        if (reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }

                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            var test = reader.GetString();
                            element.FormatOverride.Add(test ?? throw new JsonException());
                        }
                        break;
                }
            }
        }
        return element;
    }

    public FileElement ConvertHeader (ref Utf8JsonReader reader)
    {
        HeaderElement element = new ();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string? property = reader.GetString();
                reader.Read();

                switch (property)
                {
                    case "text":
                        element.Text = reader.GetString() ?? throw new JsonException();
                        break;
                    case "color":
                        element.Color = reader.GetString() ?? throw new JsonException();
                        break;
                    case "bg_color":
                        element.BgColor = reader.GetString() ?? throw new JsonException();
                        break;
                    case "level":
                        element.Level = reader.GetInt32();
                        break;
                }
            }
        }
        return element;
    }

    public FileElement ConvertCode (ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        CodeElement element = new();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (
                reader.TokenType == JsonTokenType.PropertyName
                && reader.GetString() == "lines"
            )
            {
                reader.Read();
                element = new CodeElement()
                {
                    Lines = JsonSerializer.Deserialize<List<string>>(ref reader, options)
                    ?? throw new JsonException()
                };
            }
        }
        return element;
    }

    public FileElement ConvertLink (ref Utf8JsonReader reader)
    {
        LinkElement element = new();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string? property = reader.GetString();
                reader.Read();

                switch (property)
                {
                    case "text":
                        element.Text = reader.GetString() ?? throw new JsonException();
                        break;
                    case "url":
                        element.Url = reader.GetString() ?? throw new JsonException();
                        break;
                }
            }
        }
        return element;
    }
    #endregion;
}
