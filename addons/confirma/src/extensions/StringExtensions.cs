using System.Text;

namespace Confirma.Extensions;

public static class StringExtensions
{
    public static string EscapeInvisibleCharacters(this string str)
    {
        StringBuilder sb = new();

        foreach (char c in str)
        {
            _ = sb.Append(
                c switch
                {
                    '\0' => "\\0", // Null
                    '\a' => "\\a", // Alert
                    '\b' => "\\b", // Backspace
                    // '\e' => "\\e", // Escape, unsupported in C# 11.0?
                    '\f' => "\\f", // Form feed
                    '\n' => "\\n", // New line
                    '\r' => "\\r", // Carriage return
                    '\t' => "\\t", // Horizontal tab
                    '\v' => "\\v", // Vertical tab
                    _ => c
                }
            );
        }

        return sb.ToString();
    }
}
