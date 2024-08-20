using System;

namespace Confirma.Enums;

[Flags]
public enum EIgnoreMode
{
    Always = 0,
    InEditor = 1 << 0,
    WhenNotRunningCategory = 1 << 1
}
