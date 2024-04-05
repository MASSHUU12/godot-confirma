using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class TestCaseAttribute : Attribute
{
	public string? Name { get; }
	public object[] Parameters { get; }

	public TestCaseAttribute(string? name = null, params object[] parameters)
	{
		Name = name;
		Parameters = parameters;
	}
}
