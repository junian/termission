using System;
using System.Collections.Generic;

namespace Juniansoft.Termission.Core
{
    public sealed class ServiceLocator
    {
        static readonly Lazy<ServiceLocator> instance = new Lazy<ServiceLocator>(() => new ServiceLocator());
        readonly Dictionary<Type, Dictionary<string, Lazy<object>>> registeredServices = new Dictionary<Type, Dictionary<string, Lazy<object>>>();

        public static ServiceLocator Current => instance.Value;

        public void Register<TContract, TService>(string key = "") where TService : new()
        {
            var contractKey = typeof(TContract);

            Ensure(contractKey);
            
            registeredServices[contractKey][key] =
                new Lazy<object>(() => Activator.CreateInstance(typeof(TService)));
        }

        public void Register<TService>(string key = "") where TService : new()
        {
            Register<TService, TService>(key);
        }

        public T Get<T>(string key="") where T : class
        {
            var contractKey = typeof(T);

            Ensure(contractKey);
            
            if (registeredServices.TryGetValue(typeof(T), out var dict))
            {
                if (dict.TryGetValue(key, out var service))
                {
                    return (T) service.Value;
                }
            }

            throw new Exception($"Couldn't find service with type of {typeof(T).FullName}");
        }

        private void Ensure(Type contractKey)
        {
            if (!registeredServices.ContainsKey(contractKey))
                registeredServices[contractKey] = new Dictionary<string, Lazy<object>>();
        }
    }
}
