using Confirma.Interfaces;

namespace Confirma.Formatters;

public class StringFormatter : IFormatter
{
    public string Format(object value)
    {
        return $"\"{value}\"";
    }
}
