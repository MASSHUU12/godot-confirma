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
		Print(ColorText(message, Colors.Error));
	}

	public static void PrintSuccess(string message)
	{
		Print(ColorText(message, Colors.Success));
	}

	public static void PrintWarning(string message)
	{
		Print(ColorText(message, Colors.Warning));
	}

	public static string ColorText(string text, string color)
	{
		return $"[color={color}]{text}[/color]";
	}

	public static string ColorText(string text, Color color)
	{
		return $"[color=#{color.ToHtml()}]{text}[/color]";
	}
}
