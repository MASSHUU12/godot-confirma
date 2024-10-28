namespace Confirma.Classes.Mock;

public class CallRecord
{
    public string MethodName { get; }
    public object[] Arguments { get; }
    public object? ReturnValue { get; set; }

    public CallRecord(string methodName, object[] arguments)
    {
        MethodName = methodName;
        Arguments = arguments;
    }
}
