using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    public static ServiceLocator Instance = _instance ??= _instance = new ServiceLocator();

    private static readonly ServiceLocator _instance;

    private readonly Dictionary<Type, object> _services;

    private ServiceLocator()
    {
        _services = new Dictionary<Type, object>();
        Debug.Log("ServiceLocator initialized");
    }

    public void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (_services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type} is already registered. Overwriting.");
        }
        _services.Add(type, service);
    }
    
    public void UnregisterService<T>()
    {
        var type = typeof(T);
        if (!_services.Remove(type))
        {
            Debug.LogWarning($"Service of type {type} is not registered. Cannot unregister.");
        }
    }

    public T GetService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        throw new Exception($"Service of type {type} is not registered.");
    }

    public bool Contains<T>()
    {
        return _services.ContainsKey(typeof(T));
    }

}
