using System;
using System.Collections.Generic;

internal static class Singletons
{
    private static readonly Dictionary<Type, object> _instancesByType = new();

	public static void AddInstance(ISingleton instance)
	{
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

		_instancesByType.Add(instance.GetType(), instance);
	}

	public static void RemoveInstance(ISingleton instance)
	{
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        _instancesByType.Remove(instance.GetType());
	}

	public static T GetInstance<T>() where T : ISingleton
	{
		Type instanceType = typeof(T);

        if (!_instancesByType.ContainsKey(instanceType))
            throw new Exception($"there was no instance such as {typeof(T).Name}");

        return (T)_instancesByType[instanceType];
	}
}