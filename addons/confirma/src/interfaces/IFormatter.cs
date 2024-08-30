namespace Confirma.Interfaces;

public interface IFormatter
{
    public string Format(object value);
    public string? GetPattern(object value);
}
