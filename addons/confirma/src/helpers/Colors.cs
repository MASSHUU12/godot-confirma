using Godot;

namespace Confirma.Helpers;

public static class Colors
{
	public static readonly string Success = "#8eef97";
	public static readonly string Warning = "#ffdd65";
	public static readonly string Error = "#ff786b";

	public static string ColorText(string text, string color)
	{
		return $"[color={color}]{text}[/color]";
	}

	public static string ColorText(string text, Color color)
	{
		return $"[color=#{color.ToHtml()}]{text}[/color]";
	}
}
