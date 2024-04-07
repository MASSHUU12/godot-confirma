using Godot;

namespace Confirma.Helpers;

public class Log
{
#nullable disable
	private readonly RichTextLabel _richOutput;
#nullable restore

	private readonly bool _headless = false;

	public Log(RichTextLabel? richOutput)
	{
		_richOutput = richOutput;
		_headless = _richOutput is null;
	}

	public Log()
	{
		_richOutput = null;
		_headless = true;
	}

	public void Print(string message)
	{
		if (_headless) GD.Print(message);
		else _richOutput.AppendText(message);
	}

	public void PrintLine(string message)
	{
		Print($"{message}\n");
	}

	public void PrintError(string message)
	{
		Print(ColorText(message, "red"));
	}

	public void PrintSuccess(string message)
	{
		Print(ColorText(message, "green"));
	}

	public void PrintWarning(string message)
	{
		Print(ColorText(message, "yellow"));
	}

	private string ColorText(string text, string color)
	{
		var c = new Colors(color);

		return _headless ? c.ToTerminal(text) : c.ToGodot(text);
	}
}
