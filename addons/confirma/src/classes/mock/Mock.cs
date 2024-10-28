using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Confirma.Classes.Mock;

public class Mock<T> where T : class
{
    public T Instance { get; }
    public Type ProxyType { get; }

    private readonly List<CallRecord> _callRecords = new();
    private readonly Dictionary<string, object?> _defaultReturnValues = new();

    public IReadOnlyList<CallRecord> GetCallRecords()
    {
        return _callRecords.AsReadOnly();
    }

    public Mock()
    {
        ProxyType = CreateProxyType(typeof(T));
        Instance = (T?)Activator.CreateInstance(ProxyType)
            ?? throw new InvalidOperationException(
                $"Failed to create an instance of the proxy type '{ProxyType.FullName}'."
            );
    }

    public void SetDefaultReturnValue<TResult>(string methodName, TResult value)
    {
        if (!typeof(T).GetMethods().Any(m => m.Name == methodName))
        {
            throw new ArgumentException(
                $"Method '{methodName}' does not exist on '{typeof(T).Name}'."
            );
        }
        _defaultReturnValues[methodName] = value;
    }

    private Type CreateProxyType(Type interfaceType)
    {
        string proxyTypeName = $"Proxy_{interfaceType.Name}";
        AssemblyName assemblyName = new(Guid.NewGuid().ToString());
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            assemblyName,
            AssemblyBuilderAccess.RunAndCollect
        );
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
            "MainModule"
        );
        TypeBuilder typeBuilder = moduleBuilder.DefineType(
            proxyTypeName,
            TypeAttributes.Public,
            typeof(object),
            new[] { interfaceType }
        );

        foreach (MethodInfo method in interfaceType.GetMethods())
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                method.Name,
                MethodAttributes.Public | MethodAttributes.Virtual,
                method.ReturnType,
                method.GetParameters()
                    .Select(static p => p.ParameterType).ToArray()
            );

            ILGenerator il = methodBuilder.GetILGenerator();

            // Load the arguments onto the stack (0 is 'this')
            for (int i = 0; i < method.GetParameters().Length; i++)
            {
                il.Emit(OpCodes.Ldarg, i + 1);
            }

            MethodInfo? invokeMethod = typeof(Mock<T>)
                .GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
                .FirstOrDefault(
                    static m => m.Name == nameof(InvokeMethod)
                    && m.GetParameters().Length == 2
                ) ?? throw new MissingMethodException(
                    $"Method {nameof(InvokeMethod)} not found."
                );

            if (
                method.ReturnType == typeof(void)
                || !method.IsGenericMethodDefinition
            )
            {
                il.Emit(OpCodes.Call, invokeMethod);
            }
            else
            {
                il.Emit(
                    OpCodes.Call,
                    invokeMethod.MakeGenericMethod(method.ReturnType)
                );
            }

            il.Emit(OpCodes.Ret);
        }

        return typeBuilder.CreateType();
    }

    private static TResult? InvokeMethod<TResult>(object proxy, object[] args)
    {
        Mock<T> mock = (Mock<T>)proxy;
        string methodName = new StackFrame(1).GetMethod()?.Name ?? "empty";

        CallRecord callRecord = new(methodName, args);
        mock._callRecords.Add(callRecord);

        if (mock._defaultReturnValues.TryGetValue(methodName, out object? rv))
        {
            return (TResult?)rv;
        }

        return default;
    }

    private static void InvokeMethod(object proxy, object[] args)
    {
        Mock<T> mock = (Mock<T>)proxy;
        string methodName = new StackFrame(1).GetMethod()?.Name ?? "empty";

        CallRecord callRecord = new(methodName, args);
        mock._callRecords.Add(callRecord);
    }
}
