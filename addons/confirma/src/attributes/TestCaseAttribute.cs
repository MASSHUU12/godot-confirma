using System;

namespace Confirma.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class TestCaseAttribute : Attribute
{
	public string? Name { get; }
	public object[] Parameters { get; }

	public TestCaseAttribute(string? name = null, params object[] parameters)
	{
		Name = name;
		Parameters = parameters;
	}

	public TestCaseAttribute(params object[] parameters)
	{
		Parameters = parameters;
	}
}
