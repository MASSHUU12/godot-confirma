using Godot;

namespace Confirma.Helpers;

public static class Log
{
	public static RichTextLabel? RichOutput { get; set; }
	public static bool IsHeadless { get; set; } = false;

	public static void Print(string message)
	{
		if (IsHeadless) GD.PrintRich(message);
		else RichOutput?.AppendText(message);
	}

	public static void PrintLine(string message)
	{
		Print($"{message}\n");
	}

	public static void PrintLine()
	{
		Print("\n");
	}

	public static void PrintError(string message)
	{
		Print(Colors.ColorText(message, Colors.Error));
	}

	public static void PrintSuccess(string message)
	{
		Print(Colors.ColorText(message, Colors.Success));
	}

	public static void PrintWarning(string message)
	{
		Print(Colors.ColorText(message, Colors.Warning));
	}
}
