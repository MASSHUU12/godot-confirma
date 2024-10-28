using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Confirma.Classes;

public class Mock<T>
{
    public T Instance { get; private set; }
    public Type ProxyType { get; }

    public Mock()
    {
        ProxyType = CreateProxyType(typeof(T));
        Instance = (T?)Activator.CreateInstance(ProxyType)
            ?? throw new InvalidOperationException(
                $"Failed to create an instance of the proxy type '{ProxyType.FullName}'."
            );
    }

    private Type CreateProxyType(Type interfaceType)
    {
        string proxyTypeName = $"Proxy_{interfaceType.Name}";
        Type? proxyType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.Name == proxyTypeName);

        if (proxyType is not null)
        {
            return proxyType;
        }

        AssemblyName assemblyName = new(Guid.NewGuid().ToString());
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
            assemblyName,
            AssemblyBuilderAccess.Run
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

        proxyType = moduleBuilder.DefineType(
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
                method.GetParameters().Select(p => p.ParameterType).ToArray()
            );

            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            MethodInfo? invokeMethod = typeof(MockGenerator).GetMethod(
                nameof(InvokeMethod),
                BindingFlags.Static | BindingFlags.NonPublic
            );
            if (invokeMethod is not null)
            {
                il.Emit(
                    OpCodes.Call,
                    invokeMethod.MakeGenericMethod(method.ReturnType)
                );
            }
            else
            {
                throw new MissingMethodException(
                    $"Method {nameof(InvokeMethod)} not found."
                );
            }
            il.Emit(OpCodes.Ret);
        }

        return typeBuilder.CreateType();
    }

    private TResult? InvokeMethod<TResult>(object proxy, object[] args)
    {
        // Implement method invocation logic here
        // You can store the method call information and configure the return value
        return default;
    }
}
