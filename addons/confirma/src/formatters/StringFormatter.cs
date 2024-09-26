namespace Confirma.Formatters;

public class StringFormatter : Formatter
{
    private readonly char _quote;

    public StringFormatter(char quote = '"')
    {
        _quote = quote;
    }

    public override string Format(object? value)
    {
        return $"{_quote}{value?.ToString() ?? "null"}{_quote}";
    }
}
