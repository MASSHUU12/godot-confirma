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

	private MethodInfo? _afterAllMethod = null;
	private bool _hasMoreAfterAll = false;

	public TestClass(Type type)
	{
		Type = type;
		TestMethods = TestDiscovery.DiscoverTestMethods(type);

		InitialLookup();
	}

	public TestClassResult Run()
	{
		uint passed = 0, failed = 0, ignored = 0;

		if (_beforeAllMethod is not null) RunBeforeAll();
		if (_afterAllMethod is not null) RunAfterAll();

		foreach (var method in TestMethods)
		{
			var methodResult = method.Run();

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

		foreach (var method in Reflection.GetMethodsWithAttribute<AfterAllAttribute>(Type))
		{
			if (method.GetCustomAttribute<AfterAllAttribute>() is null) continue;

			if (_afterAllMethod is null) _afterAllMethod = method;
			else _hasMoreAfterAll = true;
		}
	}

	private void RunLifecycleMethod(MethodInfo method, string name, bool hasMultiple)
	{
		if (hasMultiple)
			Log.PrintWarning($"Multiple [{name}] methods found in {Type.Name}. Running only the first one.\n");

		Log.PrintLine($"[{name}] {Type.Name}");

		try
		{
			method.Invoke(null, null);
		}
		catch (Exception e)
		{
			Log.PrintError($"- {e.Message}");
		}
	}

	private void RunBeforeAll()
	{
		RunLifecycleMethod(
		  _beforeAllMethod!,
		  "BeforeAll",
		  _hasMoreBeforeAll
		);
	}

	private void RunAfterAll()
	{
		RunLifecycleMethod(
		 _afterAllMethod!,
		 "AfterAll",
		 _hasMoreAfterAll
		);
	}
}
