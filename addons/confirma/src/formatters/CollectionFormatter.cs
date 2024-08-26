using System.Collections.Generic;
using Confirma.Interfaces;

namespace Confirma.Formatters;

public class CollectionFormatter : IFormatter
{
    public string Format(object value)
    {
        // TODO: Improve & consider using ArrayHelper.ToString.
        IEnumerable<object> collection = (IEnumerable<object>)value;
        return $"[{string.Join(", ", collection)}]";
    }
}
