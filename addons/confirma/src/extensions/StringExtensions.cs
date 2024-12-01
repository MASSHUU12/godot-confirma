using System;
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

    /// <summary>
    /// Calculates the Levenshtein distance between two strings.
    /// The Levenshtein distance is a measure of the minimum number
    /// of single-character edits (insertions, deletions or substitutions)
    /// required to change one word into the other.
    /// </summary>
    /// <param name="a">The first string to compare.</param>
    /// <param name="b">The second string to compare.</param>
    /// <returns>The Levenshtein distance between the two strings.</returns>
    public static int LevenshteinDistance(this string a, string b)
    {
        if (string.IsNullOrEmpty(a))
        {
            return b.Length;
        }

        if (string.IsNullOrEmpty(b))
        {
            return a.Length;
        }

        if (a.Length < b.Length)
        {
            (b, a) = (a, b);
        }

        int[] previousRow = new int[b.Length + 1];
        int[] currentRow = new int[b.Length + 1];

        for (int j = 0; j <= b.Length; j++)
        {
            previousRow[j] = j;
        }

        for (int i = 1; i <= a.Length; i++)
        {
            currentRow[0] = i;
            for (int j = 1; j <= b.Length; j++)
            {
                int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                currentRow[j] = Math.Min(
                    Math.Min(currentRow[j - 1] + 1, previousRow[j] + 1),
                    previousRow[j - 1] + cost);
            }
            (currentRow, previousRow) = (previousRow, currentRow);
        }

        return previousRow[b.Length];
    }
}
