using System;
using System.Collections.Generic;
using System.Reflection;
using Confirma.Attributes;
using Confirma.Helpers;
using Confirma.Types;

namespace Confirma.Classes;

public class TestClass
{
	public Type Type { get; }
	public IEnumerable<TestMethod> TestMethods { get; }

	private MethodInfo? _beforeAllMethod = null;
	private bool _hasMoreBeforeAll = false;

	public TestClass(Type type)
	{
		Type = type;
		TestMethods = TestDiscovery.DiscoverTestMethods(type);

		InitialLookup();
	}

	public TestClassResult Run(Log log)
	{
		uint passed = 0, failed = 0, ignored = 0;

		if (_hasMoreBeforeAll) log.PrintWarning(
			$"Multiple [BeforeAll] methods found in {Type.Name}. Running only the first one.\n"
		);

		if (_beforeAllMethod is not null)
		{
			log.Print($"[BeforeAll] {Type.Name}");

			try
			{
				_beforeAllMethod.Invoke(null, null);
			}
			catch (ConfirmAssertException e)
			{
				log.PrintError($"- {e.Message}\n");
			}
		}

		foreach (var method in TestMethods)
		{
			var methodResult = method.Run(log);

			passed += methodResult.TestsPassed;
			failed += methodResult.TestsFailed;
			ignored += methodResult.TestsIgnored;
		}

		return new(passed, failed, ignored);
	}

	private void InitialLookup()
	{
		foreach (var method in Reflection.GetMethodsWithAttribute<BeforeAllAttribute>(Type))
		{
			if (method.GetCustomAttribute<BeforeAllAttribute>() is null) continue;

			if (_beforeAllMethod is null) _beforeAllMethod = method;
			else _hasMoreBeforeAll = true;
		}
	}
}
