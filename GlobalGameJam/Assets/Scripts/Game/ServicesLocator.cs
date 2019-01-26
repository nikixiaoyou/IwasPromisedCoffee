using System.Collections;
using System.Collections.Generic;
using System;

namespace ggj
{
    public class ServicesLocator
    {
        private Dictionary<Type, Object> _servicesMap = new Dictionary<Type, Object>();

        public void Register<T>(T instance) where T : new()
        {
            var type = typeof(T);
            if (!_servicesMap.ContainsKey(type))
            {
                _servicesMap.Add(type, instance);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Service is already there");
            }
        }

        public void UnRegister<T>(T instance) where T : new()
        {
            var type = typeof(T);
            if (_servicesMap.ContainsKey(type))
            {
                _servicesMap.Remove(type);
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("Service is already there");
            }
        }

        public T Get<T>() where T : new()
        {
            return (T)_servicesMap[typeof(T)];
        }

        public T TryGet<T>() where T : new()
        {
            _servicesMap.TryGetValue(typeof(T), out object instance);
            return (T)instance;
        }
    }
}