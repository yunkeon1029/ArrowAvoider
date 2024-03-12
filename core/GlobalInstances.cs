using System;
using System.Collections.Generic;
using System.Linq;

public static class GlobalInstances
{
    private static readonly List<object> _instances = new();

    public static void AddInstance(object instance)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        if (_instances.Contains(instance))
            throw new Exception("instance is aready added");

        _instances.Add(instance);
    }

    public static void RemoveInstance(object instance)
    {
        if (instance == null)
            throw new ArgumentNullException(nameof(instance));

        _instances.Remove(instance);
    }

    public static T GetInstance<T>()
    {
        var instancesOfType = _instances.OfType<T>();

        if (!instancesOfType.Any())
            throw new Exception($"there was no instance such as {typeof(T).Name}");

        return instancesOfType.First();
    }

    public static T[] GetAllInstances<T>()
    {
        var instancesOfType = _instances.OfType<T>();

        if (!instancesOfType.Any())
            throw new Exception($"there was no instance such as {typeof(T).Name}");

        return instancesOfType.ToArray();
    }
}