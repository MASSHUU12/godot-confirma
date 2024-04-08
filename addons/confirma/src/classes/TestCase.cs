using System;
using System.Reflection;

namespace Confirma.Classes;

public class TestCase
{
	public MethodInfo Method { get; }
	public object?[]? Parameters { get; }

	public TestCase(MethodInfo method, object?[]? parameters)
	{
		Method = method;
		Parameters = parameters;
	}

	public void Run()
	{
		var strParams = string.Join(", ", Parameters ?? Array.Empty<object>());

		try
		{
			Method.Invoke(null, Parameters);
		}
		catch (TargetInvocationException tie)
		{
			throw new ConfirmAssertException(tie.InnerException?.Message ?? tie.Message);
		}
		catch (Exception e) when (e
			is ArgumentException
			or ArgumentNullException
		)
		{
			throw new ConfirmAssertException($"- Failed: Invalid test case parameters: {strParams}.");
		}
		catch (Exception e)
		{
			throw new ConfirmAssertException($"- Failed: {e.Message}");
		}
	}
}
