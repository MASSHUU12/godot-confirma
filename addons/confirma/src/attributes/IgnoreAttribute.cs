using System;
using Confirma.Enums;
using static System.AttributeTargets;

namespace Confirma.Attributes;

[AttributeUsage(Class | Method, AllowMultiple = false)]
public class IgnoreAttribute : Attribute
{
	public EIgnoreMode Mode { get; private set; }
	public string? Reason { get; private set; }

	public IgnoreAttribute(EIgnoreMode mode = EIgnoreMode.Always, string? reason = null)
	{
		Mode = mode;
		Reason = string.IsNullOrEmpty(reason) ? null : reason;
	}
}
