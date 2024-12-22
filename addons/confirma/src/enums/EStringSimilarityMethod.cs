namespace Confirma.Enums;

public enum EStringSimilarityMethod
{
    LevenshteinDistance = 0,
    JaroDistance = 1 << 0,
    JaroWinklerSimilarity = 1 << 1
}
