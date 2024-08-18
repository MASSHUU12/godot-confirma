namespace Confirma.Enums;

public enum ERunTargetType : byte
{
    All = 0,
    Class = 1 << 0,
    Method = 1 << 1,
    Category = 1 << 2
}
