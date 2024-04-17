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

	private readonly Dictionary<string, LifecycleMethodData> _lifecycleMethods = new();

	public TestClass(Type type)
	{
		Type = type;
		TestMethods = TestDiscovery.DiscoverTestMethods(type);

		InitialLookup();
	}

	public TestClassResult Run()
	{
		uint passed = 0, failed = 0, ignored = 0, warnings = 0;

		warnings += RunLifecycleMethod("BeforeAll");

		foreach (var method in TestMethods)
		{
			warnings += RunLifecycleMethod("SetUp");

			var methodResult = method.Run();

			warnings += RunLifecycleMethod("TearDown");

			passed += methodResult.TestsPassed;
			failed += methodResult.TestsFailed;
			ignored += methodResult.TestsIgnored;
		}

		warnings += RunLifecycleMethod("AfterAll");

		return new(passed, failed, ignored, warnings);
	}

	private void InitialLookup()
	{
		foreach (var method in Reflection.GetMethodsWithAttribute<BeforeAllAttribute>(Type))
		{
			if (method.GetCustomAttribute<BeforeAllAttribute>() is null) continue;

			if (!_lifecycleMethods.TryGetValue("BeforeAll", out var beforeAll))
				_lifecycleMethods.Add("BeforeAll", new(method, "BeforeAll", false));
			else beforeAll.HasMultiple = true;
		}

		foreach (var method in Reflection.GetMethodsWithAttribute<AfterAllAttribute>(Type))
		{
			if (!_lifecycleMethods.TryGetValue("AfterAll", out var afterAll))
				_lifecycleMethods.Add("AfterAll", new(method, "AfterAll", false));
			else afterAll.HasMultiple = true;
		}

		foreach (var method in Reflection.GetMethodsWithAttribute<SetUpAttribute>(Type))
		{
			if (!_lifecycleMethods.TryGetValue("SetUp", out var setUp))
				_lifecycleMethods.Add("SetUp", new(method, "SetUp", false));
			else setUp.HasMultiple = true;
		}

		foreach (var method in Reflection.GetMethodsWithAttribute<TearDownAttribute>(Type))
		{
			if (!_lifecycleMethods.TryGetValue("TearDown", out var tearDown))
				_lifecycleMethods.Add("TearDown", new(method, "TearDown", false));
			else tearDown.HasMultiple = true;
		}
	}

	private byte RunLifecycleMethod(string name)
	{
		if (!_lifecycleMethods.TryGetValue(name, out var method)) return 0;

		if (method.HasMultiple)
			Log.PrintWarning($"Multiple [{name}] methods found in {Type.Name}. Running only the first one.\n");

		Log.PrintLine($"[{name}] {Type.Name}");

		try
		{
			method.Method.Invoke(null, null);
		}
		catch (Exception e)
		{
			Log.PrintError($"- {e.Message}");
		}

		return method.HasMultiple ? (byte)1 : (byte)0;
	}
}
