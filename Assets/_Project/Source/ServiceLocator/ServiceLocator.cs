using System;
using System.Collections.Generic;

public class ServiceLocator
{
    private static readonly ServiceLocator instance = new ServiceLocator();
    private Dictionary<Type, object> services = new Dictionary<Type, object>();

    private ServiceLocator() { }

    public static ServiceLocator Instance
    {
        get { return instance; }
    }

    public void RegisterService<T>(T service)
    {
        Type type = typeof(T);
        if (!services.ContainsKey(type))
        {
            services.Add(type, service);
        }
        else
        {
            services[type] = service;
        }
    }

    public T GetService<T>()
    {
        Type type = typeof(T);
        if (services.ContainsKey(type))
        {
            return (T)services[type];
        }
        else
        {
            throw new Exception($"Service of type {type} is not registered.");
        }
    }
}
