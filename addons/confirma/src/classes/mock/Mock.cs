using System;
using System.Collections.Generic;
using System.Reflection;

namespace Confirma.Classes.Mock;

public class Mock<T> where T : class
{
    private readonly List<CallRecord> _callRecords = new();
    private readonly Dictionary<string, object?> _defaultReturnValues = new();

    private static readonly Dictionary<string, MethodInfo?> _methodInfoCache = new();

    public T Instance { get; }

    public Mock()
    {
        Instance = DispatchProxy.Create<T, MockProxy>();
        ((MockProxy)(object)Instance).SetMock(this);
    }

    public IReadOnlyList<CallRecord> GetCallRecords()
    {
        return _callRecords.AsReadOnly();
    }

    public void ClearCallRecords()
    {
        _callRecords.Clear();
    }

    public void SetDefaultReturnValue<TResult>(string methodName, TResult? value)
    {
        MethodInfo? method = Mock<T>.GetMethod(methodName);

        if (method?.ReturnType.IsAssignableFrom(typeof(TResult)) != true)
        {
            throw new ArgumentException(
                $"Method '{methodName}' does not exist or return type mismatch on '{typeof(T).Name}'."
            );
        }
        _defaultReturnValues[methodName] = value;
    }

    private static MethodInfo? GetMethod(string methodName)
    {
        if (!_methodInfoCache.TryGetValue(methodName, out MethodInfo? method))
        {
            method = typeof(T).GetMethod(methodName);
            _methodInfoCache[methodName] = method;
        }
        return method;
    }

    private class MockProxy : DispatchProxy
    {
        private Mock<T>? _mock;

        public void SetMock(Mock<T> mock)
        {
            _mock = mock;
        }

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (_mock is null || targetMethod is null)
            {
                throw new InvalidOperationException("Mock not initialized.");
            }

            string methodName = targetMethod.Name;
            _mock._callRecords.Add(new CallRecord(methodName, args));

            if (_mock._defaultReturnValues.TryGetValue(
                    methodName, out object? returnValue
                )
            )
            {
                return returnValue;
            }

            if (targetMethod.ReturnType == typeof(void))
            {
                return null;
            }

            return targetMethod.ReturnType.IsValueType
                ? Activator.CreateInstance(targetMethod.ReturnType)
                : null;
        }
    }
}
