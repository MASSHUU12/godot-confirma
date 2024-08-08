using Confirma.Enums;
using Confirma.Helpers;

namespace Confirma.Classes;

public class TestResultMessage
{
    public Elogtype MessageType {get; }
    public string Message {get; }

    public TestResultMessage (Elogtype type, string message)
    {
        MessageType = type;
        Message = message;
    }

    public void LogMessage ()
    {
        switch (MessageType)
        {
            case Elogtype.Info:
                Log.PrintLine(Message);
                return;
            case Elogtype.Error:
                Log.PrintError(Message);
                return;
            case Elogtype.Warning:
                Log.PrintWarning (Message);
                return;
        }
    }
}
