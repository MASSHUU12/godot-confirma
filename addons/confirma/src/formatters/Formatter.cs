using Confirma.Interfaces;

namespace Confirma.Formatters;

public abstract class Formatter : IFormatter
{
    public abstract string Format(object value);
}
