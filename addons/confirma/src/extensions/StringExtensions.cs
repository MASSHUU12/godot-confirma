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

    /// <summary>
    /// Calculates the Jaro distance between two strings.
    /// The Jaro distance is a measure of similarity between two strings.
    /// It is a value between 0 and 1, where 1 means the strings are identical
    /// and 0 means they have no similarity.
    /// </summary>
    /// <param name="a">The first string to compare.</param>
    /// <param name="b">The second string to compare.</param>
    /// <returns>The Jaro distance between the two strings.</returns>
    public static double JaroDistance(this string a, string b)
    {
        int lenA = a.Length;
        int lenB = b.Length;

        if (lenA == 0 && lenB == 0)
        {
            return 1;
        }

        int matchDistance = (Math.Max(lenA, lenB) / 2) - 1;

        bool[] matchesA = new bool[lenA];
        bool[] matchesB = new bool[lenB];

        int matches = 0;
        int transpositions = 0;

        for (int i = 0; i < lenA; i++)
        {
            int start = Math.Max(0, i - matchDistance);
            int end = Math.Min(i + matchDistance + 1, lenB);

            for (int j = start; j < end; j++)
            {
                if (matchesB[j] || a[i] != b[j])
                {
                    continue;
                }

                matchesA[i] = true;
                matchesB[j] = true;
                matches++;
                break;
            }
        }

        if (matches == 0)
        {
            return 0;
        }

        int k = 0;
        for (int i = 0; i < lenA; i++)
        {
            if (!matchesA[i])
            {
                continue;
            }

            while (!matchesB[k])
            {
                k++;
            }

            if (a[i] != b[k])
            {
                transpositions++;
            }

            k++;
        }

        return (
            ((double)matches / lenA)
            + ((double)matches / lenB)
            + ((matches - (transpositions / 2d)) / matches)
        ) / 3d;
    }

    /// <summary>
    /// Calculates the Jaro-Winkler similarity score between two strings.
    /// </summary>
    /// <param name="a">The first string.</param>
    /// <param name="b">The second string.</param>
    /// <param name="p">
    /// The scaling factor for the common prefix (between 0 and 0.25).
    /// Default is 0.1.
    /// </param>
    /// <returns>A similarity score between 0.0 and 1.0.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown when the scaling factor p is out of range.
    /// </exception>
    public static double JaroWinklerSimilarity(
        this string a,
        string b,
        double p = 0.1
    )
    {
        if (p is < 0 or > 0.25)
        {
            throw new ArgumentOutOfRangeException(
                nameof(p),
                "The scaling factor must be between 0 and 0.25."
            );
        }

        if (a.Length == 0 && b.Length == 0)
        {
            return 1.0;
        }

        double distance = a.JaroDistance(b);

        int prefixLength = 0;
        int maxPrefixLength = Math.Min(4, Math.Min(a.Length, b.Length));

        for (int i = 0; i < maxPrefixLength; i++)
        {
            if (a[i] != b[i])
            {
                break;
            }

            prefixLength++;
        }

        return distance + (prefixLength * p * (1 - distance));
    }
}
