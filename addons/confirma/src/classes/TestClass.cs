using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Confirma.Attributes;
using Confirma.Helpers;
using Confirma.Types;

namespace Confirma.Classes;

public class TestClass
{
	public Type Type { get; }
	public bool IsParallelizable { get; }
	public IEnumerable<TestMethod> TestMethods { get; }

	private readonly Dictionary<string, LifecycleMethodData> _lifecycleMethods = new();

	public TestClass(Type type)
	{
		Type = type;
		TestMethods = TestDiscovery.DiscoverTestMethods(type);
		IsParallelizable = type.GetCustomAttribute<ParallelizableAttribute>() is not null;

		InitialLookup();
	}

	public async Task<TestClassResult> RunAsync()
	{
		uint passed = 0, failed = 0, ignored = 0, warnings = 0;

		warnings += await RunLifecycleMethodAsync("BeforeAll");

		foreach (var method in TestMethods)
		{
			warnings += await RunLifecycleMethodAsync("SetUp");

			var methodResult = await method.RunAsync();

			warnings += await RunLifecycleMethodAsync("TearDown");

			passed += methodResult.TestsPassed;
			failed += methodResult.TestsFailed;
			ignored += methodResult.TestsIgnored;
		}

		warnings += await RunLifecycleMethodAsync("AfterAll");

		return new(passed, failed, ignored, warnings);
	}

	private void InitialLookup()
	{
		AddLifecycleMethod("BeforeAll", Reflection.GetMethodsWithAttribute<BeforeAllAttribute>(Type));
		AddLifecycleMethod("AfterAll", Reflection.GetMethodsWithAttribute<AfterAllAttribute>(Type));
		AddLifecycleMethod("SetUp", Reflection.GetMethodsWithAttribute<SetUpAttribute>(Type));
		AddLifecycleMethod("TearDown", Reflection.GetMethodsWithAttribute<TearDownAttribute>(Type));
	}

	private void AddLifecycleMethod(string name, IEnumerable<MethodInfo> methods)
	{
		if (!methods.Any()) return;

		_lifecycleMethods.Add(name, new(methods.First(), name, methods.Count() > 1));
	}

	private async Task<byte> RunLifecycleMethodAsync(string name)
	{
		if (!_lifecycleMethods.TryGetValue(name, out var method)) return 0;

		if (method.HasMultiple)
			Log.PrintWarning($"Multiple [{name}] methods found in {Type.Name}. Running only the first one.\n");

		Log.PrintLine($"[{name}] {Type.Name}");

		try
		{
			await Task.Run(() => method.Method.Invoke(null, null));
		}
		catch (Exception e)
		{
			Log.PrintError($"- {e.Message}");
		}

		return method.HasMultiple ? (byte)1 : (byte)0;
	}
}
