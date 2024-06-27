using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class RepeatAttribute : Attribute
{
	public ushort Repeat { get; init; }

	public RepeatAttribute(ushort repeat)
	{
		Repeat = repeat;
	}
}
