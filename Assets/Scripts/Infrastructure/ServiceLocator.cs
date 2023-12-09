using System.Collections.Generic;
using System;

namespace Assets.Scripts.Infrastructure
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            Type serviceType = typeof(T);

            if (!services.ContainsKey(serviceType))
            {
                services.Add(serviceType, service);
            }
            else
            {
                services[serviceType] = service;
            }
        }

        public static T GetService<T>()
        {
            Type serviceType = typeof(T);

            if (services.ContainsKey(serviceType))
            {
                return (T)services[serviceType];
            }
            else
            {
                throw new InvalidOperationException($"Service of type {serviceType} not registered.");
            }
        }
    }
}