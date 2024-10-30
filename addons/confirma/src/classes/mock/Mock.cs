using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Confirma.Classes.Mock;

public class Mock<T> where T : class
{
    private static readonly MethodInfo _invokeVoidMethod = typeof(Mock<T>)
        .GetMethod(
            nameof(InvokeMethodVoid),
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
        ) ?? throw new MissingMethodException("InvokeMethodVoid not found.");

    private static readonly MethodInfo _invokeGenericMethod = typeof(Mock<T>)
        .GetMethod(
            nameof(InvokeMethod),
            BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
        ) ?? throw new MissingMethodException("InvokeMethod<TResult> not found.");

    private static readonly ConstructorInfo _objectCtor = typeof(object)
        .GetConstructor(Type.EmptyTypes)
        ?? throw new MissingMethodException("Object constructor not found.");

    private static readonly Dictionary<Type, Type> _proxyTypeCache = new();

    public T Instance { get; }
    public Type ProxyType { get; }

    private readonly List<CallRecord> _callRecords = new();
    private readonly Dictionary<string, object?> _defaultReturnValues = new();

    public IReadOnlyList<CallRecord> GetCallRecords()
    {
        return _callRecords.AsReadOnly();
    }

    public void ClearCallRecords()
    {
        _callRecords.Clear();
    }

    public Mock()
    {
        ProxyType = CreateProxyType(typeof(T));

        Instance = (T?)Activator.CreateInstance(ProxyType, new object[] { this })
            ?? throw new InvalidOperationException(
                $"Failed to create an instance of the proxy type '{ProxyType.FullName}'."
            );
    }

    public void SetDefaultReturnValue<TResult>(string methodName, TResult? value)
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
        if (_proxyTypeCache.TryGetValue(interfaceType, out Type? cachedType))
        {
            return cachedType;
        }

        ValidateInterfaceMethods(interfaceType);

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
            TypeAttributes.Public | TypeAttributes.Class,
            typeof(object),
            new[] { interfaceType }
        );

        // Define a private field to hold the reference to the Mock instance
        FieldBuilder mockField = typeBuilder.DefineField(
            "_mock",
            typeof(Mock<T>),
            FieldAttributes.Private
        );

        // Define a constructor that takes a Mock<T> instance
        ConstructorBuilder constructor = typeBuilder.DefineConstructor(
            MethodAttributes.Public,
            CallingConventions.Standard,
            new[] { typeof(Mock<T>) }
        );

        ILGenerator ctorIL = constructor.GetILGenerator();
        ctorIL.Emit(OpCodes.Ldarg_0); // Load 'this'
        ctorIL.Emit(OpCodes.Call, _objectCtor); // Call base constructor
        ctorIL.Emit(OpCodes.Ldarg_0); // Load 'this'
        ctorIL.Emit(OpCodes.Ldarg_1); // Load the Mock<T> instance passed in
        ctorIL.Emit(OpCodes.Stfld, mockField); // Store it in _mock
        ctorIL.Emit(OpCodes.Ret);

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

            // Load '_mock' field (Mock<T> instance)
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, mockField);

            // Load 'methodName' string
            il.Emit(OpCodes.Ldstr, method.Name);

            // Prepare and load 'args' array
            LocalBuilder argsArray = il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Ldc_I4, method.GetParameters().Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc, argsArray);

            for (int i = 0; i < method.GetParameters().Length; i++)
            {
                il.Emit(OpCodes.Ldloc, argsArray);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);

                if (method.GetParameters()[i].ParameterType.IsValueType)
                {
                    il.Emit(OpCodes.Box, method.GetParameters()[i].ParameterType);
                }

                il.Emit(OpCodes.Stelem_Ref);
            }

            // Load 'args' array
            il.Emit(OpCodes.Ldloc, argsArray);

            if (method.ReturnType == typeof(void))
            {
                il.Emit(OpCodes.Call, _invokeVoidMethod);
            }
            else
            {
                il.Emit(
                    OpCodes.Call,
                    _invokeGenericMethod?.MakeGenericMethod(method.ReturnType)
                    ?? throw new MissingMethodException(
                        "InvokeMethod<TResult> not found."
                    )
                );

                Type returnType = method.ReturnType;

                if (returnType.IsValueType)
                {
                    // The returned value is already of the correct type
                    // on the stack
                }
                else
                {
                    HandleReferenceType(il, returnType);
                }
            }

            il.Emit(OpCodes.Ret);
            typeBuilder.DefineMethodOverride(methodBuilder, method);
        }

        Type generatedType = typeBuilder.CreateType();
        _proxyTypeCache[interfaceType] = generatedType;
        return generatedType;
    }

    private static void HandleReferenceType(ILGenerator il, Type returnType)
    {
        // Reference types can be null
        Label endLabel = il.DefineLabel();
        il.Emit(OpCodes.Dup);
        il.Emit(OpCodes.Brtrue_S, endLabel);

        il.Emit(OpCodes.Pop);
        il.Emit(OpCodes.Ldnull);

        il.MarkLabel(endLabel);
        il.Emit(OpCodes.Castclass, returnType);
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

    public static TResult? InvokeMethod<TResult>(
        Mock<T> mock,
        string methodName,
        object[] args
    )
    {
        CallRecord callRecord = new(methodName, args);
        mock._callRecords.Add(callRecord);

        if (mock._defaultReturnValues.TryGetValue(methodName, out object? rv))
        {
            return (TResult?)rv;
        }

        return default;
    }

    public static void InvokeMethodVoid(
        Mock<T> mock,
        string methodName,
        object[] args
    )
    {
        CallRecord callRecord = new(methodName, args);
        mock._callRecords.Add(callRecord);
    }
}
