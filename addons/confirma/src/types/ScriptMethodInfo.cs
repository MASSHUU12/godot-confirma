namespace Confirma.Types;

public record ScriptMethodInfo(
    string Name,
    string[] Args,
    string[] DefaultArgs,
    int Flags,
    int Id,
    ScriptMethodReturnInfo ReturnInfo
);
