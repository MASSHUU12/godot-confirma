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
        Debug.Assert(
            IntPtr.Size == 8,
            "Expected 64-bit process. Running in 32-bit mode."
        );

        ValidateInterfaceMethods(interfaceType);

        string proxyTypeName = $"Proxy_{interfaceType.Name}";
        AssemblyName assemblyName = new(Guid.NewGuid().ToString());
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            assemblyName,
            AssemblyBuilderAccess.Run // TODO: Check RunAndCollect
        );
        ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(
            "MainModule"
        );
        TypeBuilder typeBuilder = moduleBuilder.DefineType(
            proxyTypeName,
            TypeAttributes.Public | TypeAttributes.Class,
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

            if (method.ReturnType != typeof(void))
            {
                if (method.ReturnType.IsValueType)
                {
                    // Push a default value for value types (e.g., 0 for int)
                    LocalBuilder local = il.DeclareLocal(method.ReturnType);
                    il.Emit(OpCodes.Ldloca_S, local);
                    il.Emit(OpCodes.Initobj, method.ReturnType);
                    il.Emit(OpCodes.Ldloc, local);
                }
                else
                {
                    // Push a null for reference types
                    il.Emit(OpCodes.Ldnull);
                }
            }

            il.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodBuilder, method);
        }

        return typeBuilder.CreateType();
    }

    private static void ValidateInterfaceMethods(Type interfaceType)
    {
        foreach (MethodInfo method in interfaceType.GetMethods())
        {
            if (method.ReturnType is null)
            {
                throw new InvalidOperationException(
                    $"Method '{method.Name}' has an invalid return type."
                );
            }

            if (
                method.IsGenericMethodDefinition
                && method.GetGenericArguments().Any(
                    static arg => arg.IsGenericParameter
                )
            )
            {
                throw new InvalidOperationException(
                    $"Method '{method.Name}' has invalid generic parameters."
                );
            }
        }
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
