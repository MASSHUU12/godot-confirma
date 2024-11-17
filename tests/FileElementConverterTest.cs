using System;
using System.Text.Json;
using Confirma.Attributes;
using Confirma.Classes.HelpElements;
using Confirma.Deserialization.Json;
using Confirma.Extensions;

namespace Confirma.Tests;

[TestClass]
[Parallelizable]
public class FileElementConverterTest
{
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        Converters = { new FileElementConverter() },
        PropertyNameCaseInsensitive = true
    };

    [TestCase]
    public void Converter_text_returnsTextElement ()
    {
        string TestData = @"
        {
            ""type"":""text"",
            ""text"":""lorem"",
            ""bg_color"":""#ff00ff"",
            ""color"":""#00ff00"",
            ""format"": [
                ""s"",
                ""c"",
                ""b"",
                ""i"",
                ""u"",
                ""f""
            ]
        }";

        FileElement? result = JsonSerializer.Deserialize<FileElement>(TestData, _options);

        result.ConfirmNotNull();
        result.ConfirmType(typeof(TextElement));

        TextElement element = result as TextElement ?? throw new NullReferenceException(); //it should be always not null

        element.Text.ConfirmEqual("lorem");
        _ = element.FormatOverride.ToArray().ConfirmEqual(new string[] { "s", "c", "b", "i", "u", "f" });
        element.Color.ConfirmEqual("#00ff00");
        element.BgColor.ConfirmEqual("#ff00ff");
    }

    [TestCase]
    public void Converter_header_returnsHeaderElement ()
    {
        string TestData = @"
        {
            ""type"":""header"",
            ""text"":""lorem"",
            ""bg_color"":""#ff00ff"",
            ""color"":""#00ff00"",
            ""level"":1
        }";

        FileElement? result = JsonSerializer.Deserialize<FileElement>(TestData, _options);

        result.ConfirmNotNull();
        result.ConfirmType(typeof(HeaderElement));

        HeaderElement element = result as HeaderElement ?? throw new NullReferenceException(); //it should be always not null

        element.Text.ConfirmEqual("lorem");
        element.Level.ConfirmEqual(1);
        element.Color.ConfirmEqual("#00ff00");
        element.BgColor.ConfirmEqual("#ff00ff");
    }

    [TestCase]
    public void Converter_code_returnsCodeElement ()
    {
        string TestData = @"
        {
            ""type"":""code"",
            ""lines"":[""line1"",""line2""]
        }";

        FileElement? result = JsonSerializer.Deserialize<FileElement>(TestData, _options);

        result.ConfirmNotNull();
        result.ConfirmType(typeof(CodeElement));

        CodeElement element = result as CodeElement ?? throw new NullReferenceException(); //it should be always not null

        _ = element.Lines.ToArray().ConfirmEqual(new string[] { "line1", "line2" });
    }

    [TestCase]
    public void Converter_link_returnsLinkElement ()
    {
        string TestData = @"
        {
            ""type"":""link"",
            ""text"":""ExampleText"",
            ""url"":""https://example.com""
        }";

        FileElement? result = JsonSerializer.Deserialize<FileElement>(TestData, _options);

        result.ConfirmNotNull();
        result.ConfirmType(typeof(LinkElement));

        LinkElement element = result as LinkElement ?? throw new NullReferenceException(); //it should be always not null

        element.Text.ConfirmEqual("ExampleText");
        element.Url.ConfirmEqual("https://example.com");
    }

    [TestCase("randomType")]
    [TestCase("type")]
    [TestCase("line")]
    public void Converter_unsupported_throwsNotSupportedException (string type)
    {
        string TestData = $@"
        {{
            ""type"":""{type}""
        }}";

        Action action = () => JsonSerializer.Deserialize<FileElement>(TestData, _options);

        action.ConfirmThrows<NotSupportedException>();
    }

    [TestCase]
    public void Converter_misplacedTypeProperty_throwsJsonException ()
    {
        string TestData = $@"
        {{
            ""otherProperty"":1,
            ""type"":""code""
        }}";

        Action action = () => JsonSerializer.Deserialize<FileElement>(TestData, _options);

        action.ConfirmThrows<JsonException>();
    }

    [TestCase(typeof(TextElement))]
    [TestCase(typeof(HeaderElement))]
    [TestCase(typeof(LinkElement))]
    [TestCase(typeof(CodeElement))]
    public void CanConvert_FileElementSubClass_returnsTrue (Type type)
    {
        FileElementConverter converter = new FileElementConverter();

        converter.CanConvert(type).ConfirmTrue();
    }

    [TestCase(typeof(string))]
    [TestCase(typeof(int))]
    [TestCase(typeof(float))]
    public void CanConvert_OtherClass_returnsFalse (Type type)
    {
        FileElementConverter converter = new FileElementConverter();

        converter.CanConvert(type).ConfirmFalse();
    }
}
