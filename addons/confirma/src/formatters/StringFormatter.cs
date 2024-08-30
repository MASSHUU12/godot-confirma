namespace Confirma.Formatters;

public class StringFormatter : Formatter
{
    public override string Format(object value)
    {
        return $"\"{value.ToString() ?? "null"}\"";
    }

    public override string? GetPattern(object value)
    {
        return null;
    }
}
