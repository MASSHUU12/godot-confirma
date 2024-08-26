using Confirma.Interfaces;

namespace Confirma.Formatters;

public class DefaultFormatter : IFormatter
{
    public string Format(object? value)
    {
        return value?.ToString() ?? "null";
    }
}
